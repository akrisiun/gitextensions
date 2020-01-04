using GitCommands;
using GitUI;
using System;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{

    public class FormRevisionGrid : Form, IGitUICommandsSource
    {
        public RevisionGrid Grid { get; set; }

        public FormRevisionGrid()
        {
            GitUICommandsChanged = null;
            Grid = new RevisionGrid();

            var RevisionsGraph = Grid.RevisionsGraph;

            RevisionsGraph.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            RevisionsGraph.Visible = true;
            Grid.Visible = true;
            Controls.Add(Grid);
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

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        public void LoadModule(GitModule module)
        {
            UICommands = new GitUICommands(module);
        }

        public event EventHandler<GitUICommandsChangedEventArgs> GitUICommandsChanged;

        public void Execute(GitUICommandsChangedEventArgs e)
        {
            if (GitUICommandsChanged != null)
                GitUICommandsChanged(this, e);
        }

        public GitUICommands UICommands
        {
            get;
            protected set;
        }
    }
}
