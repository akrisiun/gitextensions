using GitCommands;
using GitUI.UserControls;
using GitUIPluginInterfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GitUI.CommandsDialogs
{
    public abstract class FormBrowse : GitModuleForm, IBrowseRepo, IWin32Window2
    {
        public FormBrowse(GitUICommands aCommands, string filter) : this(true, aCommands, filter) { }
        public FormBrowse(bool positionRestore, GitUICommands aCommands, string filter)
             : base(positionRestore, aCommands)
        { }

        public static Lazy<IRepoObjectsTree> LazyTree { get; set; }
        public static Action<string> StartCommit { get; set; }
        public ITree Tree { get; set; } // IRepoObjectsTree
        // IGitUICommands IFormBrowse.UICommands { get { return UICommands; } } // -> GitModuleForm

        public abstract void InitializeComponent();

        public abstract void GoToRef(string refName, bool showNoRevisionMsg);
        public abstract void SetWorkingDir(string path);

        public const string HotkeySettingsName = "Browse";

        internal enum Commands
        {
            GitBash,
            GitGui,
            GitGitK,
            FocusRevisionGrid,
            FocusCommitInfo,
            FocusFileTree,
            FocusDiff,
            Commit,
            AddNotes,
            FindFileInSelectedCommit,
            CheckoutBranch,
            QuickFetch,
            QuickPull,
            QuickPush,
            RotateApplicationIcon,
            CloseRepositry,
        }

        public static void CopyFullPathToClipboard(FileStatusList diffFiles, GitModule module)
        {
            if (!diffFiles.SelectedItems.Any())
                return;

            var fileNames = new StringBuilder();
            foreach (var item in diffFiles.SelectedItems)
            {
                //Only use append line when multiple items are selected.
                //This to make it easier to use the text from clipboard when 1 file is selected.
                if (fileNames.Length > 0)
                    fileNames.AppendLine();

                fileNames.Append(Path.Combine(module.WorkingDir, item.Name).ToNativePath());
            }
            Clipboard.SetText(fileNames.ToString());
        }
    }
}
