using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ConEmu.WinForms;
using GitCommands;
using GitCommands.Utils;

#pragma warning disable IDE0018, IDE0019, IDE0060, VSTHRD012, VSTHRD110, CS1998

namespace GitUI
{
    public class ConsoleHelper // : IConsoleHelper
    {
        public static ConsoleHelper Instance { get; protected set; }
        static ConsoleHelper()
        {
            Instance = Instance ?? new ConsoleHelper();
        }

        //GitUIPluginInterfaces.
        public IWin32Window Window { get; set; }
        public GitUIPluginInterfaces.IGitUICommands UICommands { get; set; }
        public string WorkingDir => Directory.GetCurrentDirectory();

        public static Process RunPS(string bashCommand = null, params string[] parm)
            => Instance.RunConsolePS(bashCommand, parm);

        public Process RunConsolePS(string bashCommand = null, params string[] parm)
        {
            if (EnvUtils.RunningOnUnix())
            {
                return RunBash(bashCommand, "powershell");
            }

            // Windows: @powershell
            return RunExternalCmdDetachedShowConsole("powershell", @"/K echo cmd.exe command error!");
        }

        public Process RunBash(string cmd, string arguments)
        {
            return null;
        }

        public Process StartProccess(string cmd, string arguments, string workingDir, bool showConsole)
        {
            return null;
        }

        public Process RunExternalCmdDetachedShowConsole(string cmd, string arguments)
        {
            try
            {
                return StartProccess(cmd, arguments, WorkingDir, showConsole: true);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return null;
        }

        public void FillTerminalTab(Control terminalCtrl, GitUIPluginInterfaces.IGitModule Module)
        {
            var terminal = terminalCtrl as ConEmuControl;
            if (terminal == null || terminal.IsConsoleEmulatorOpen) // If user has typed "exit" in there, restart the shell; otherwise just return
                return;

            // Create the terminal
            var startinfo = new ConEmuStartInfo();
            startinfo.StartupDirectory = Module.WorkingDir;
            startinfo.WhenConsoleProcessExits = WhenConsoleProcessExits.CloseConsoleEmulator;

            // Choose the console: bash from git with fallback to cmd
            string sGitBashFromUsrBin = "";/*This is not a console program and is not reliable yet, suppress for now.*/ //Path.Combine(Path.Combine(Path.Combine(AppSettings.GitBinDir, ".."), ".."), "git-bash.exe"); // Git bin dir is /usr/bin under git installdir, so go 2x up
            string sGitBashFromBinOrCmd = "";/*This is not a console program and is not reliable yet, suppress for now.*/ //Path.Combine(Path.Combine(AppSettings.GitBinDir, ".."), "git-bash.exe"); // In case we're running off just /bin or /cmd

            sGitBashFromBinOrCmd = @"c:\Windows\System32\bash.exe";

            var directoryName = AppSettings.GitCommandValue;
            var gitDir = directoryName.Length > 0 ? Path.GetDirectoryName(directoryName) : "";
            string sJustBash = Path.Combine(gitDir, "bash.exe"); // Generic bash, should generally be in the git dir, less configured than the specific git-bash

            string sJustSh = Path.Combine(gitDir, "sh.exe"); // Fallback to SH
            var cmdLine = new[] { sGitBashFromUsrBin, sGitBashFromBinOrCmd, sJustBash, sJustSh }
                    .Where(File.Exists).FirstOrDefault()
                    ?? ConEmuConstants.DefaultConsoleCommandLine; // Choose whatever exists, or default CMD shell

            cmdLine = "{Shells::PowerShell}"; // {PowerShell}

            startinfo.ConsoleProcessCommandLine = cmdLine;

            if (!cmdLine.Contains(@"\System32") && !cmdLine.Contains("{")
                && startinfo.ConsoleProcessCommandLine != ConEmuConstants.DefaultConsoleCommandLine)
            {
                startinfo.ConsoleProcessCommandLine += " --login -i";
            }

            // Set path to git in this window (actually, effective with CMD only)
            if (!string.IsNullOrEmpty(AppSettings.GitCommand))
            {
                string dirGit = Path.GetDirectoryName(AppSettings.GitCommand);
                if (!string.IsNullOrEmpty(dirGit))
                    startinfo.SetEnv("PATH", dirGit + ";" + "%PATH%");
            }

            startinfo.AllowUsedUp = true;

            // terminal as ConEmuControl
            ConEmuSession session = null;

            try
            {
                session = terminal.Start(startinfo);
                if (session == null)
                {
                    // Retry with cmd:
                    var startinfo2 = new ConEmuStartInfo();
                    startinfo2.StartupDirectory = Module.WorkingDir;
                    startinfo2.WhenConsoleProcessExits = WhenConsoleProcessExits.CloseConsoleEmulator;
                    startinfo2.ConsoleProcessCommandLine = "{cmd}";
                    session = terminal.Start(startinfo2);
                }

                /* problem:
                 *    at ConEmu.WinForms.Resources.get_ConEmuSettingsTemplate()
                at ConEmu.WinForms.ConEmuStartInfo.get_BaseConfiguration()
                at ConEmu.WinForms.ConEmuSession.Init_MakeConEmuCommandLine_EmitConfigFile(DirectoryInfo dirForConfigFile, ConEmuStartInfo startinfo, HostContext hostcontext)
                at ConEmu.WinForms.ConEmuSession.Init_MakeConEmuCommandLine(ConEmuStartInfo startinfo, HostContext hostcontext, AnsiLog ansilog, DirectoryInfo dirLocalTempRoot)
                at ConEmu.WinForms.ConEmuSession..ctor(ConEmuStartInfo startinfo, HostContext hostcontext)
                at ConEmu.WinForms.ConEmuControl.Start(ConEmuStartInfo startinfo)
                at GitUI.ConsoleHelper.FillTerminalTab(Control terminalCtrl, IGitModule Module) in D:\Beta\GitExt2\gitextensions\GitUI\MainDialogs\ConsoleHelper.cs:line 121
                */
            }
            catch (Exception ex) {
                MessageBox.Show($"{ex.Message} \n {ex.StackTrace}");
            }

            // posible error:
            // Could not run the console emulator. The path to ConEmu.exe could not be detected.
        }

    }
}
