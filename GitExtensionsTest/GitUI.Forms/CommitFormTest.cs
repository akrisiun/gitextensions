using GitCommands;
using GitUI;
using GitUI.RevisionGridClasses;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{
    //using NUnit.Framework;
    [NUnit.Framework.TestFixture] 
    // [TestClass]
    public class CommitFormTest
    {
        public FormCommitTest Form { get; set; }
        public FileStatusList Unstaged { get; set; }
        public FileStatusList Staged { get; set; }

        ApplicationContext app;

        [NUnit.Framework.Test]
        [STAThread]
        //[TestMethod]
        public void FormCommit_Form1()
        {
            app = new ApplicationContext();
            var currentDir = Directory.GetCurrentDirectory();
            string[] args = new string[] { "", "", currentDir };    // GitExtensionsTest\bin\Debug
            if (args[2].EndsWith(@"bin\Debug"))
                args[2] = Path.GetFullPath(currentDir + @"\..\..\..");

            var dir = RevisionGridTest.GetWorkingDir(args);
            Directory.SetCurrentDirectory(dir);

            GitUICommands uCommands = new GitUICommands(dir);

            // TODO Sta apartment (for Sharp code TextEditorControl)
            Form = new FormCommitTest(uCommands);

            Unstaged = Form.ListUnstaged;
            Staged = Form.ListStaged;

            app.MainForm = Form;

            var browseForm = TestStatic.StartBrowseForm(uCommands, Form);

            Form.LoadModule(browseForm.Module);

            this.Unstaged.Subscribe();
            this.Staged.Subscribe();

            Form.Shown += Form_Shown;

            Form.Show();

            Application.Run(app);
        }

        void Form_Shown(object sender, EventArgs e)
        {
            Unstaged.Focus();
        }
         
    }
}