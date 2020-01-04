using GitUI;
using GitUI.CommandsDialogs;
using System;
using System.Windows.Forms;

namespace GitExtensionsTest.GitUI.Forms
{
    public static class TestStatic
    {
        public static FormBrowse StartBrowseForm(GitUICommands commands, Form form)
        {
            if (instance == null)
                instance = commands.StartBrowseForm(form, "", run: false);

            return instance;
        }

        private static FormBrowse instance;
        public static FormBrowse Instance { get { return instance; } }
    }
}