using GitCommands;
using GitUI;
using GitUI.RevisionGridClasses;
using NUnit.Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{
    [NUnit.Framework.TestFixture]
    // [TestClass]
    public class RevisionGridTest
    {
        public RevisionGrid RevisionGrid { get; set; }
        public DvcsGraph Revisions { get; set; }
        public FormRevisionGrid Form { get; set; }
        ApplicationContext app;

        [Test]  // TestMethod
        [STAThread]
        public void RevisionGrid_Form1()
        {
            app = new ApplicationContext();

            Form = new FormRevisionGrid();
            RevisionGrid = Form.Grid;
            Revisions = RevisionGrid.RevisionsGraph;

            app.MainForm = Form;

            var currentDir = Directory.GetCurrentDirectory();
            string[] args = new string[] { "", "", currentDir };    // GitExtensionsTest\bin\Debug
            if (args[2].EndsWith(@"bin\Debug"))
                args[2] = Path.GetFullPath(currentDir + @"\..\..\..");

            var dir = GetWorkingDir(args);
            Directory.SetCurrentDirectory(dir);

            GitUICommands uCommands = new GitUICommands(dir);
            var browseForm = TestStatic.StartBrowseForm(uCommands, Form);

            Form.LoadModule(browseForm.Module);
            RevisionGrid.UICommandsSource = Form;

            var _revisionGraphCommand = new RevisionGraph(browseForm.Module)
            {
                BranchFilter = null
            };
            _revisionGraphCommand.Updated += _revisionGraphCommand_Updated;
            _revisionGraphCommand.Error += _revisionGraphCommand_Error;

            _revisionGraphCommand.Exited += _revisionGraphCommand_Exited;

            RevisionGrid.RevisionsGraph.Columns[1].Visible = false;
            RevisionGrid.RemovePainting(); // RevisionsGraph.RemovePainting();

            Form.Shown += Form_Shown;
            Form.Show();

            _revisionGraphCommand.Execute();

            Application.Run(app);
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            (sender as Form).Focus();
        }

        void _revisionGraphCommand_Error(object sender, AsyncErrorEventArgs e)
        {
            this.RevisionGrid.RevisionsGraph.Dispose();
            (this.RevisionGrid as IDisposable).Dispose();

            var obj = sender as AsyncLoader;
            obj.Cancel();
        }

        void _revisionGraphCommand_Updated(object sender, EventArgs e)
        {
            var obj = sender as RevisionGraph;
            var updatedEvent = (RevisionGraph.RevisionGraphUpdatedEventArgs)e;

            var revision = updatedEvent.Revision;
            if (revision != null)
            {
                if (Revisions.Rows.Count > 100)
                    return; // enough

                GitRevision rev = updatedEvent.Revision;

                var dataType = DvcsGraph.DataType.Normal;
                //if (rev.Guid == filtredCurrentCheckout)
                //    dataType = DvcsGraph.DataType.Active;
                //else 
                if (System.Linq.Enumerable.Any(rev.Refs))
                    dataType = DvcsGraph.DataType.Special;

                Revisions.Add(rev.Guid, rev.ParentGuids, dataType, rev);
            }
        }

        void _revisionGraphCommand_Exited(object sender, EventArgs ea)
        {
            var obj = sender as RevisionGraph;

            Assert.IsTrue(Revisions.LanesCollect.Count > 0);

            var lanesNum = Revisions.LanesCollect.GetEnumerator();
            Revisions.VirtualMode = false;
            Revisions.EnumerateRows(maxRows: 20);

            Revisions.Height = Form.Height;
            Revisions.Width = Form.Width;
            Revisions.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Revisions.Visible = true;

            if (Revisions.Rows.Count == 0)
            {
                return;
            }

            Graphics g = System.Drawing.Graphics.FromHwnd(Revisions.Handle);

            var cell = Revisions.Rows[0].Cells[0];
            Rectangle cellBounds = cell.ContentBounds;
            DataGridViewAdvancedBorderStyle advancedBorderStyle = new DataGridViewAdvancedBorderStyle();

            DataGridViewCellPaintingEventArgs e =
                new DataGridViewCellPaintingEventArgs(Revisions, g, Revisions.Bounds, cellBounds,
                    0, 0, DataGridViewElementStates.Visible, null, cell.FormattedValue,
                    null, cell.Style, advancedBorderStyle: advancedBorderStyle, paintParts: DataGridViewPaintParts.All);
            RevisionGrid.BackColor = Color.Silver;
            Exception err;
            try
            {
                RevisionGrid.RevisionsCellPainting(RevisionGrid, e);

                Revisions.dataGrid_CellPainting(RevisionGrid, e);
            }
            catch (Exception ex) { err = ex; }
        }

        // GitExtensions.Program.GetWorkingDir(args);
        public static string GetWorkingDir(string[] args)
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