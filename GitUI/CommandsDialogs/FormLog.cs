﻿using System;

namespace GitUI.CommandsDialogs
{
    public partial class FormLog : GitModuleForm
    {
        [Obsolete("For VS designer and translation test only. Do not remove.")]
        private FormLog()
        {
            InitializeComponent();
        }

        public FormLog(GitUICommands commands)
            : base(commands)
        {
            InitializeComponent();
            InitializeComplete();

            diffViewer.ExtraDiffArgumentsChanged += DiffViewerExtraDiffArgumentsChanged;
        }

        private void FormDiffLoad(object sender, EventArgs e)
        {
            RevisionGrid.Load();
        }

        private void DiffFilesSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewSelectedFileDiff();
        }

        private void ViewSelectedFileDiff()
        {
            if (DiffFiles.SelectedItem == null)
            {
                diffViewer.Clear();
                return;
            }

            using (WaitCursorScope.Enter())
            {
#pragma warning disable VSTHRD102
                ThreadHelper.JoinableTaskFactory.Run(async () =>
                    await diffViewer.ViewChangesAsync(RevisionGrid.GetSelectedRevisions(), DiffFiles.SelectedItem, string.Empty)
                    );
            }
        }

        private void RevisionGridSelectionChanged(object sender, EventArgs e)
        {
            using (WaitCursorScope.Enter())
            {
                DiffFiles.SetDiffs(RevisionGrid.GetSelectedRevisions());
            }
        }

        private void DiffViewerExtraDiffArgumentsChanged(object sender, EventArgs e)
        {
            ViewSelectedFileDiff();
        }
    }
}
