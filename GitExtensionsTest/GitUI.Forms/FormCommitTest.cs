using GitCommands;
using GitUI;
using GitUI.CommandsDialogs;
using System;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{

    public class FormCommitTest : FormCommit, IGitUICommandsSource
    {
        public FileStatusList ListUnstaged { get; set; }
        public FileStatusList ListStaged { get; set; }

        public FormCommitTest(GitUICommands aCommands = null)
            : base(aCommands)
        {
            ListUnstaged = this.Unstaged;
            ListStaged = this.Staged;
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
