using GitCommands;
using GitUI;
using GitUI.HelperDialogs;
using System;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{
    public class ResetFormTest : FormResetCurrentBranch, IGitUICommandsSource
    {
        public ResetFormTest(GitUICommands aCommands = null, GitRevision revision = null)
            : base(aCommands, revision)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.ApplicationExitCall)
                e.Cancel = true;
            else 
                base.OnFormClosing(e);
        }

        public void LoadModule(GitModule module)
        {
            if (UICommands != null && UICommands.Module == module)
            {
                return;
            }

            UICommands = new GitUICommands(module);
        }
    }
}
