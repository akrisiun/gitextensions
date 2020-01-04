using GitCommands;
using GitUI;
using GitUI.CommandsDialogs;
using GitUI.HelperDialogs;
using System;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{
    class BrowseFormTest : FormBrowse, IGitUICommandsSource
    {
        private BrowseFormTest(GitUICommands aCommands = null, string filter = "")
            : base(aCommands, filter)
        {
        }
    }

    public class SubmodulesTreeTest : FormBrowse, IGitUICommandsSource
    {
        public SubmodulesTreeTest(GitUICommands aCommands = null, string filter = "")
            : base(aCommands, filter)
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
