using GitCommands;
using GitUI;
using GitUI.CommandsDialogs;
using GitUI.UserControls;
using GitUIPluginInterfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GitExtensions
{
    using IWin32Window = GitUI.IWin32Window;

    [CLSCompliant(false)]
    public class GitUICommandsThread : GitUICommands, IGitUICommands
    {
        public GitUICommandsThread(string dir) : base(dir)
        {
        }

        public override bool StartBrowseDialog(string filter) => StartBrowseDialog(null, filter);

        public override bool StartBrowseDialog(IWin32Window owner, string filter)
        {
            // if (!InvokeEvent(owner, PreBrowse))
            //      return false;

            FormBrowseDark.LazyTree = new Lazy<GitUI.UserControls.IRepoObjectsTree>(
                () => new RepoObjectsTree());

            FormBrowse.LazyTree = FormBrowseDark.LazyTree;

            var form = new FormBrowseDark(this, filter);
            bool showDebug = Debugger.IsAttached;
#if DEBUG
            showDebug = true;
#endif

            if (showDebug) {
                form.Cmd.ShowLog();

                AppSettings.GitLog.Log("form created");
                form.Cmd.UpdateLog(); // invalidate
            }

            form.InitForm();

            AppSettings.GitLog.Log("form after InitForm");
            form.Cmd.UpdateLog(); // invalidate

            if (Application.MessageLoop)
            {
                form.Show();
            }
            else
            {
                Application.Run(form);
            }

            return true;
        }

        public override bool StartCommitDialog(IWin32Window owner, bool showOnlyWhenChanges)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var exe = baseDir + "GitExtensions.Commit.exe";

            if (!File.Exists(exe))
                return base.StartCommitDialog(owner, showOnlyWhenChanges);

            var proc = new Process();
            proc.StartInfo = new ProcessStartInfo
            {
                WorkingDirectory = Directory.GetCurrentDirectory(),
                FileName = exe,
                Arguments = "",
                WindowStyle = ProcessWindowStyle.Normal
            };
            proc.Start();
            return true;
        }
    }
}
