﻿using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GitExtensions
{
    public static class CommitProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            if (OS.IsWindowsVistaOrAbove())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }

            if (!IsMonoRuntime)
            {
                try
                {
                    NBug.Settings.UIMode = NBug.Enums.UIMode.Full;

                    // Uncomment the following after testing to see that NBug is working as configured
                    NBug.Settings.ReleaseMode = true;
                    NBug.Settings.ExitApplicationImmediately = false;
                    NBug.Settings.WriteLogToDisk = false;
                    NBug.Settings.MaxQueuedReports = 10;
                    NBug.Settings.StopReportingAfter = 90;
                    NBug.Settings.SleepBeforeSend = 30;
                    NBug.Settings.StoragePath = NBug.Enums.StoragePath.WindowsTemp;

                    AppDomain.CurrentDomain.UnhandledException += NBug.Handler.UnhandledException;
                    Application.ThreadException += NBug.Handler.ThreadException;

                }
                catch (TypeInitializationException tie)
                {
                    // is this exception caused by the configuration?
                    if (tie.InnerException != null
                        && tie.InnerException.GetType()
                            .IsSubclassOf(typeof(System.Configuration.ConfigurationException)))
                    {
                        HandleConfigurationException((System.Configuration.ConfigurationException)tie.InnerException);
                    }
                }
            }

            UI.Loader.Run();
        }

        public static bool IsMonoRuntime => Type.GetType("Mono.Runtime") != null;

        static void HandleConfigurationException(System.Configuration.ConfigurationException ce)
        {
            try
            {
                // perhaps this should be checked for if it is null
                var in3 = ce.InnerException.InnerException;

                // saves having to have a reference to System.Xml just to check that we have an XmlException
                if (in3.GetType().Name.Equals("XmlException"))
                {
                    var localSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GitExtensions");

                    //assume that if we are having this error and the installation is not a portable one then the folder will exist.
                    if (Directory.Exists(localSettingsPath))
                    {
                        string messageContent = String.Format("There is a problem with the user.xml configuration file.{0}{0}The error message was: {1}{0}{0}The configuration file is usually found in: {2}{0}{0}Problems with configuration can usually be solved by deleting the configuration file. Would you like to delete the file?", Environment.NewLine, in3.Message, localSettingsPath);

                        if (DialogResult.Yes.Equals(MessageBox.Show(messageContent, "Configuration Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)))
                        {
                            try
                            {
                                Directory.Delete(localSettingsPath, true); //deletes all application settings not just for this instance - but should work
                                //Restart GitExtensions with the same arguments after old config is deleted?
                                if (DialogResult.OK.Equals(MessageBox.Show(String.Format("Files have been deleted.{0}{0}Would you like to attempt to restart GitExtensions?", Environment.NewLine), "Configuration Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)))
                                {
                                    var args = Environment.GetCommandLineArgs();
                                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                                    p.StartInfo.FileName = args[0];
                                    if (args.Length > 1)
                                    {
                                        args[0] = "";
                                        p.StartInfo.Arguments = String.Join(" ", args);
                                    }
                                    p.Start();
                                }
                            }
                            catch (IOException)
                            {
                                MessageBox.Show(String.Format("Could not delete all files and folders in {0}!", localSettingsPath), "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //assuming that there is no localSettingsPath directory in existence we probably have a portable installation.
                    else
                    {
                        string messageContent = String.Format("There is a problem with the application settings XML configuration file.{0}{0}The error message was: {1}{0}{0}Problems with configuration can usually be solved by deleting the configuration file.", Environment.NewLine, in3.Message);
                        MessageBox.Show(messageContent, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally // if we fail in this somehow at least this message might get somewhere
            {
                System.Console.WriteLine("Configuration Error");
                System.Environment.Exit(1);
            }
        }

    }

    internal class OS
    {
        public static bool IsWindowsVistaOrAbove()
        {
            OperatingSystem os = Environment.OSVersion;
            return os.Platform == PlatformID.Win32NT && os.Version.Major >= 6;
        }
    }

    public class Env
    {
        public static void DoEvents() {
            if (OS.IsWindowsVistaOrAbove())
                Application.DoEvents();
        }
    }
}

namespace GitExtensions.UI
{
    using GitCommands;
    using GitCommands.Utils;
    using GitUI;
    // using GitUI.CommandsDialogs.SettingsDialog.Pages;

    static class Loader
    {
        public static void Run()
        {
            string[] args = Environment.GetCommandLineArgs();

            GitUIExtensions.UISynchronizationContext = SynchronizationContext.Current;
            Env.DoEvents();

            AppSettings.LoadSettings();
            if (EnvUtils.RunningOnWindows())
            {
                //  Quick HOME check:
                FormSplash.SetAction("Checking home path...");
                Env.DoEvents();

                //  FormFixHome.CheckHomePath();
            }

            Env.DoEvents();
 
            FormSplash.HideSplash();

            if (EnvUtils.RunningOnWindows())
                MouseWheelRedirector.Active = true;

            var uCommands = new GitUICommandsCommit(GetWorkingDir(args));

            if (args.Length <= 1)
            {
                uCommands.StartCommitDialog();
            }
            else  // if we are here args.Length > 1
            {
                uCommands.RunCommand(args);
            }

            AppSettings.SaveSettings();
        }

        private static string GetWorkingDir(string[] args)
        {
            string workingDir = string.Empty;
            if (args.Length >= 3)
            {
                if (Directory.Exists(args[2]))
                    workingDir = GitModule.FindGitWorkingDir(args[2]);
                else
                {
                    workingDir = Path.GetDirectoryName(args[2]);
                    workingDir = GitModule.FindGitWorkingDir(workingDir);
                }

                //Do not add this working directory to the recent repositories. It is a nice feature, but it
                //also increases the startup time
                //if (Module.ValidWorkingDir())
                //    Repositories.RepositoryHistory.AddMostRecentRepository(Module.WorkingDir);
            }

            if (args.Length <= 1 && string.IsNullOrEmpty(workingDir) && AppSettings.StartWithRecentWorkingDir)
            {
                if (GitModule.IsValidGitWorkingDir(AppSettings.RecentWorkingDir))
                    workingDir = AppSettings.RecentWorkingDir;
            }

            if (string.IsNullOrEmpty(workingDir))
            {
                string findWorkingDir = GitModule.FindGitWorkingDir(Directory.GetCurrentDirectory());
                if (GitModule.IsValidGitWorkingDir(findWorkingDir))
                    workingDir = findWorkingDir;
            }

            return workingDir;
        }

    }
}
