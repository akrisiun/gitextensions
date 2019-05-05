using GitCommands;
using GitUI;
using GitUI.CommandsDialogs;
using GitUI.CommandsDialogs.BrowseDialog;
using GitUI.Hotkey;
using GitUI.Plugin;
using GitUIPluginInterfaces;
using ResourceManager;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GitUI.CommandsDialogs
{
    using IWin32Window = GitUI.IWin32Window;
    using Settings = GitCommands.AppSettings;

    [CLSCompliant(false)]
    public partial class FormBrowseDarkCommands : IDisposable
    {

        #region Translation

        public readonly TranslationString _stashCount =
            new TranslationString("{0} saved {1}");
        public readonly TranslationString _stashPlural =
            new TranslationString("stashes");
        public readonly TranslationString _stashSingular =
            new TranslationString("stash");

        public readonly TranslationString _warningMiddleOfBisect =
            new TranslationString("You are in the middle of a bisect");
        public readonly TranslationString _warningMiddleOfRebase =
            new TranslationString("You are in the middle of a rebase");
        public readonly TranslationString _warningMiddleOfPatchApply =
            new TranslationString("You are in the middle of a patch apply");

        public readonly TranslationString _hintUnresolvedMergeConflicts =
            new TranslationString("There are unresolved merge conflicts!");

        public readonly TranslationString _noBranchTitle =
            new TranslationString("no branch");

        public readonly TranslationString _noSubmodulesPresent =
            new TranslationString("No submodules");
        public readonly TranslationString _topProjectModuleFormat =
            new TranslationString("Top project: {0}");
        public readonly TranslationString _superprojectModuleFormat =
            new TranslationString("Superproject: {0}");

        public readonly TranslationString _saveFileFilterCurrentFormat =
            new TranslationString("Current format");
        public readonly TranslationString _saveFileFilterAllFiles =
            new TranslationString("All files");

        public readonly TranslationString _indexLockDeleted =
            new TranslationString("index.lock deleted.");
        public readonly TranslationString _indexLockNotFound =
            new TranslationString("index.lock not found at:");

        public readonly TranslationString _errorCaption =
            new TranslationString("Error");
        public readonly TranslationString _loading =
            new TranslationString("Loading...");

        public readonly TranslationString _noReposHostPluginLoaded =
            new TranslationString("No repository host plugin loaded.");

        public readonly TranslationString _noReposHostFound =
            new TranslationString("Could not find any relevant repository hosts for the currently open repository.");

        public readonly TranslationString _configureWorkingDirMenu =
            new TranslationString("Configure this menu");

        public readonly TranslationString directoryIsNotAValidRepositoryCaption =
            new TranslationString("Open");

        public readonly TranslationString directoryIsNotAValidRepository =
            new TranslationString("The selected item is not a valid git repository.\n\nDo you want to abort and remove it from the recent repositories list?");

        public readonly TranslationString _updateCurrentSubmodule =
            new TranslationString("Update current submodule");

        public readonly TranslationString _nodeNotFoundNextAvailableParentSelected =
            new TranslationString("Node not found. The next available parent node will be selected.");

        public readonly TranslationString _nodeNotFoundSelectionNotChanged =
            new TranslationString("Node not found. File tree selection was not changed.");

        public readonly TranslationString _diffNoSelection =
            new TranslationString("Diff (no selection)");

        public readonly TranslationString _diffParentWithSelection =
            new TranslationString("Diff (A: parent --> B: selection)");

        public readonly TranslationString _diffTwoSelected =
            new TranslationString("Diff (A: first --> B: second)");

        public readonly TranslationString _diffNotSupported =
            new TranslationString("Diff (not supported)");

        public readonly TranslationString _pullFetch =
            new TranslationString("Pull - fetch");
        public readonly TranslationString _pullFetchAll =
            new TranslationString("Pull - fetch all");
        public readonly TranslationString _pullMerge =
            new TranslationString("Pull - merge");
        public readonly TranslationString _pullRebase =
            new TranslationString("Pull - rebase");
        public readonly TranslationString _pullOpenDialog =
            new TranslationString("Open pull dialog");

        public readonly TranslationString _resetFileCaption =
            new TranslationString("Reset");
        public readonly TranslationString _resetFileText =
            new TranslationString("Are you sure you want to reset this file or directory?");
        public readonly TranslationString _resetFileError =
            new TranslationString("Exactly one revision must be selected. Abort.");

        #endregion

        public FormBrowseDark Form {
            [DebuggerStepThrough]
            get; protected set;
        }

        public GitUICommands UICommands { [DebuggerStepThrough] get => Form.UICommands; }
        public IBrowseRepo BrowseRepo { [DebuggerStepThrough] get => UICommands?.BrowseRepo; }
        public GitModule Module { [DebuggerStepThrough] get => Form.Module; }

        public FormBrowseDarkCommands(FormBrowseDark form)
        {
            Form = form;
        }
        public void Dispose()
        {
            Form = null;
        }

        public void UpdatePluginMenu(bool validWorkingDir)
        {
            foreach (ToolStripItem item in Form.pluginsToolStripMenuItem.DropDownItems) {
                var plugin = item.Tag as IGitPluginForRepository;

                item.Enabled = plugin == null || validWorkingDir;
            }
        }

        public void RegisterPlugins()
        {
            foreach (var plugin in LoadedPlugins.Plugins)
                plugin.Register(UICommands);

            UICommands.RaisePostRegisterPlugin(Form);
        }

        public void UnregisterPlugins()
        {
            foreach (var plugin in LoadedPlugins.Plugins)
                plugin.Unregister(UICommands);
        }


        #region Commit, Push, Pull with arguments

        //public void Commit(Dictionary<string, string> arguments)
        //{
        //    FormBrowseDark.StartCommitDialog(arguments.ContainsKey("quiet"));
        //}

        public void Push(Dictionary<string, string> arguments)
        {
            UICommands.StartPushDialog(arguments.ContainsKey("quiet"));
        }

        public void Pull(Dictionary<string, string> arguments)
        {
            FormBrowseDark.UpdateSettingsBasedOnArguments(arguments);

            string remoteBranch = null;
            if (arguments.ContainsKey("remotebranch"))
                remoteBranch = arguments["remotebranch"];

            UICommands.StartPullDialog(arguments.ContainsKey("quiet"), remoteBranch);
        }


        public void RaisePreBrowseInitialize(IWin32Window owner)
            => UICommands.RaisePreBrowseInitialize(owner);
        public void RaisePostBrowseInitialize(IWin32Window owner)
           => UICommands.RaisePostBrowseInitialize(owner);
        public void RaisePostRegisterPlugin(IWin32Window owner)
           => UICommands.RaisePostRegisterPlugin(owner);

        public void BrowseGoToRef(string refName, bool showNoRevisionMsg)
        {
            if (BrowseRepo != null)
                BrowseRepo.GoToRef(refName, showNoRevisionMsg);
        }

        #endregion


        public void SaveAsOnClick(object sender, EventArgs e)
        {
            var item = Form.GitTree.SelectedNode.Tag as GitItem;

            if (item == null)
                return;
            if (!item.IsBlob)
                return;

            var Module = Form.Module;

            var fullName = Path.Combine(Module.WorkingDir, item.FileName);
            using (var fileDialog =
                new SaveFileDialog {
                    InitialDirectory = Path.GetDirectoryName(fullName),
                    FileName = Path.GetFileName(fullName),
                    DefaultExt = GitCommandHelpers.GetFileExtension(fullName),
                    AddExtension = true
                }) {
                fileDialog.Filter =
                    _saveFileFilterCurrentFormat.Text + " (*." +
                    GitCommandHelpers.GetFileExtension(fileDialog.FileName) + ")|*." +
                    GitCommandHelpers.GetFileExtension(fileDialog.FileName) +
                    "|" + _saveFileFilterAllFiles.Text + " (*.*)|*.*";

                if (fileDialog.ShowDialog(Form) == DialogResult.OK) {
                    Module.SaveBlobAs(fileDialog.FileName, item.Guid);
                }
            }
        }

        public void ResetToThisRevisionOnClick(object sender, EventArgs e)
        {
            IList<GitRevision> revisions = Form.RevisionGrid.GetSelectedRevisions().Cast<GitRevision>().ToList();

            if (!revisions.Any() || revisions.Count != 1) {
                MessageBox.Show(_resetFileError.Text, _resetFileCaption.Text);
                return;
            }

            if (MessageBox.Show(_resetFileText.Text, _resetFileCaption.Text, MessageBoxButtons.OKCancel)
                == System.Windows.Forms.DialogResult.OK) {
                var item = Form.GitTree.SelectedNode.Tag as GitItem;
                var files = new List<string> { item.FileName };
                Module.CheckoutFiles(files, revisions.First().Guid, false);
            }
        }

        public void TabControl1SelectedIndexChanged(object sender, EventArgs e)
        {
            Form.FillFileTree();
            Form.FillDiff();
            Form.FillCommitInfo();
            Form.FillBuildReport();
        }

        #region Clicks 1

        public void ChangelogToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frm = new FormChangeLog()) {
                frm.ShowDialog(Form);
            }
        }

        public void DiffFilesDoubleClick(object sender, EventArgs e)
        {
            var DiffFiles = Form.DiffFiles;
            if (DiffFiles.SelectedItem == null)
                return;

            UICommands.StartFileHistoryDialog(Form, (DiffFiles.SelectedItem).Name);
        }

        public void ToolStripButtonPushClick(object sender, EventArgs e)
        {
            PushToolStripMenuItemClick(sender, e);
        }

        public void PushToolStripMenuItemClick(object sender, EventArgs e)
        {
            bool bSilent = (Control.ModifierKeys & Keys.Shift) != 0;
            UICommands.StartPushDialog(Form, bSilent);
        }

        public void PullToolStripMenuItemClick(object sender, EventArgs e)
        {
            bool bSilent;
            if (sender == Form.toolStripButtonPull || sender == Form.pullToolStripMenuItem) {
                if (Module.LastPullAction == Settings.PullAction.None) {
                    bSilent = (Control.ModifierKeys & Keys.Shift) != 0;
                } else if (Module.LastPullAction == Settings.PullAction.FetchAll) {
                    Form.fetchAllToolStripMenuItem_Click(sender, e);
                    return;
                } else {
                    bSilent = (sender == Form.toolStripButtonPull);
                    Module.LastPullActionToFormPullAction();
                }
            } else {
                bSilent = sender != Form.pullToolStripMenuItem1;

                Module.LastPullActionToFormPullAction();
            }

            UICommands.StartPullDialog(Form, bSilent);
        }

        public void RefreshToolStripMenuItemClick(object sender, EventArgs e)
        {
            Form.RefreshRevisions();
        }

        public void RefreshDashboardToolStripMenuItemClick(object sender, EventArgs e)
        {
            Form._dashboard.Refresh();
        }

        public void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frm = new AboutBox())
                frm.ShowDialog(Form);
        }

        public void PatchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartViewPatchDialog(Form);
        }

        public void ApplyPatchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartApplyPatchDialog(Form);
        }

        public void GitBashToolStripMenuItemClick1(object sender, EventArgs e)
        {
            Module.RunBash();
        }

        public void GitGuiToolStripMenuItemClick(object sender, EventArgs e)
        {
            Module.RunGui();
        }

        public void FormatPatchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartFormatPatchDialog(Form);
        }

        public void GitcommandLogToolStripMenuItemClick(object sender, EventArgs e)
        {
            FormGitLog.ShowOrActivate(Form);
        }

        public void CheckoutBranchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartCheckoutBranch(Form);
        }

        public void StashToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartStashDialog(Form);
        }

        public void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UICommands.StartResetChangesDialog(Form);
        }

        public void RunMergetoolToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartResolveConflictsDialog(Form);
        }

        public void WarningClick(object sender, EventArgs e)
        {
            UICommands.StartResolveConflictsDialog(Form);
        }

        public void WorkingdirClick(object sender, EventArgs e)
        {
            Form._NO_TRANSLATE_Workingdir.ShowDropDown();
        }

        public void CurrentBranchClick(object sender, EventArgs e)
        {
            Form.branchSelect.ShowDropDown();
        }

        public void DeleteBranchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartDeleteBranchDialog(Form, string.Empty);
        }

        public void DeleteTagToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartDeleteTagDialog(Form, null);
        }

        public void CherryPickToolStripMenuItemClick(object sender, EventArgs e)
        {
            var revisions = Form.RevisionGrid.GetSelectedRevisions(System.DirectoryServices.SortDirection.Descending);

            UICommands.StartCherryPickDialog(Form, revisions.First() as GitRevision);
        }

        public void MergeBranchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartMergeBranchDialog(Form, null);
        }

        public void TagToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartCreateTagDialog(Form);
        }

        public void RefreshButtonClick(object sender, EventArgs e)
        {
            RefreshToolStripMenuItemClick(sender, e);
        }

        public void CommitcountPerUserToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frm = new FormCommitCount(UICommands))
                frm.ShowDialog(Form);
        }

        public void KGitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Module.RunGitK();
        }

        public void DonateToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frm = new FormDonate())
                frm.ShowDialog(Form);
        }

        #endregion Clicks 1
        #region Changed Events 1

        public void DiffFiles_DataSourceChanged(object sender, EventArgs e)
        {
            var DiffFiles = Form.DiffFiles;
            if (DiffFiles.GitItemStatuses == null || !DiffFiles.GitItemStatuses.Any())
                Form.DiffText.ViewPatch(String.Empty);
        }

        public void DiffFilesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_dontUpdateOnIndexChange) {
                ShowSelectedFileDiff();
            }
        }

        public void ShowSelectedFileDiff()
        {
            var DiffFiles = Form.DiffFiles;
            var DiffText = Form.DiffText;

            if (Form.DiffFiles.SelectedItem == null) {
                DiffText.ViewPatch("");
                return;
            }

            var revItems = Form.RevisionGrid.GetSelectedRevisions();
            IList<GitRevision> items = revItems.Cast<GitRevision>().ToList();

            if (items.Count() == 1) {
                items.Add(new GitRevision(Module, Form.DiffFiles.SelectedItemParent));

                if (!string.IsNullOrWhiteSpace(Form.DiffFiles.SelectedItemParent)
                    && DiffFiles.SelectedItemParent == DiffFiles.CombinedDiff.Text) {
                    var diffOfConflict = Module.GetCombinedDiffContent(items.First(), DiffFiles.SelectedItem.Name,
                        DiffText.GetExtraDiffArguments(), DiffText.Encoding);

                    if (string.IsNullOrWhiteSpace(diffOfConflict)) {
                        diffOfConflict = Strings.GetUninterestingDiffOmitted();
                    }

                    DiffText.ViewPatch(diffOfConflict);
                    return;
                }
            }
            DiffText.ViewChanges(revItems, DiffFiles.SelectedItem, String.Empty);
        }


        public void DiffTextExtraDiffArgumentsChanged(object sender, EventArgs e)
        {
            ShowSelectedFileDiff();
        }

        #endregion Changed Events 1

        public void CleanupToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartCleanupRepositoryDialog(Form);
        }

        public void openWithDifftoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form.DiffFiles.SelectedItem == null)
                return;

            var RevisionGrid = Form.RevisionGrid;

            var selectedItem = Form.DiffFiles.SelectedItem;
            GitUIExtensions.DiffWithRevisionKind diffKind;

            if (sender == Form.aLocalToolStripMenuItem)
                diffKind = GitUIExtensions.DiffWithRevisionKind.DiffALocal;
            else if (sender == Form.bLocalToolStripMenuItem)
                diffKind = GitUIExtensions.DiffWithRevisionKind.DiffBLocal;
            else if (sender == Form.parentOfALocalToolStripMenuItem)
                diffKind = GitUIExtensions.DiffWithRevisionKind.DiffAParentLocal;
            else if (sender == Form.parentOfBLocalToolStripMenuItem)
                diffKind = GitUIExtensions.DiffWithRevisionKind.DiffBParentLocal;
            else {
                Debug.Assert(sender == Form.aBToolStripMenuItem, "Not implemented DiffWithRevisionKind: " + sender);
                diffKind = GitUIExtensions.DiffWithRevisionKind.DiffAB;
            }

            string parentGuid = RevisionGrid.GetSelectedRevisions().Count() == 1 ?
                   Form.DiffFiles.SelectedItemParent : null;

            RevisionGrid.OpenWithDifftool(selectedItem.Name, selectedItem.OldName, diffKind, parentGuid);
        }

        public void GitTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.IsExpanded)
                return;

            var item = (IGitItem)e.Node.Tag;

            e.Node.Nodes.Clear();
            Form.LoadInTree(item.SubItems, e.Node.Nodes);
        }

        public void CreateBranchToolStripMenuItemClick(object sender, EventArgs e)
        {
            UICommands.StartCreateBranchDialog(Form,
                Form.RevisionGrid.GetSelectedRevisions().FirstOrDefault() as GitRevision);
        }

        public void GitBashClick(object sender, EventArgs e)
        {
            GitBashToolStripMenuItemClick1(sender, e);
        }

        public void ToolStripButtonPullClick(object sender, EventArgs e)
        {
            PullToolStripMenuItemClick(sender, e);
        }

        public void editgitattributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UICommands.StartEditGitAttributesDialog(Form);
        }

        public void copyFilenameToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gitItem = Form.GitTree.SelectedNode.Tag as GitItem;
            if (gitItem == null)
                return;

            var fileName = Path.Combine(Module.WorkingDir, (gitItem).FileName);
            Clipboard.SetText(fileName.ToNativePath());
        }

        public void copyFilenameToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CopyFullPathToClipboard(Form.DiffFiles, Module);
        }

        public static void CopyFullPathToClipboard(FileStatusList diffFiles, GitModule module)
        {
            if (!diffFiles.SelectedItems.Any())
                return;

            var fileNames = new StringBuilder();
            foreach (var item in diffFiles.SelectedItems) {
                //Only use append line when multiple items are selected.
                //This to make it easier to use the text from clipboard when 1 file is selected.
                if (fileNames.Length > 0)
                    fileNames.AppendLine();

                fileNames.Append(Path.Combine(module.WorkingDir, item.Name).ToNativePath());
            }
            Clipboard.SetText(fileNames.ToString());
        }

        public void deleteIndexlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Module.GetGitDirectory(), "index.lock");

            if (File.Exists(fileName)) {
                File.Delete(fileName);
                MessageBox.Show(Form, _indexLockDeleted.Text);
            } else
                MessageBox.Show(Form, _indexLockNotFound.Text + " " + fileName);
        }

        public void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IList<GitRevision> revisions = Form.RevisionGrid.GetSelectedRevisions().Cast<GitRevision>().ToList();

            if (revisions.Count == 0)
                return;

            var DiffFiles = Form.DiffFiles;
            if (DiffFiles.SelectedItem == null)
                return;

            GitItemStatus item = DiffFiles.SelectedItem;

            var fullName = Path.Combine(Module.WorkingDir, item.Name);
            using (var fileDialog =
                new SaveFileDialog {
                    InitialDirectory = Path.GetDirectoryName(fullName),
                    FileName = Path.GetFileName(fullName),
                    DefaultExt = GitCommandHelpers.GetFileExtension(fullName),
                    AddExtension = true
                }) {
                fileDialog.Filter =
                    _saveFileFilterCurrentFormat.Text + " (*." +
                    fileDialog.DefaultExt + ")|*." +
                    fileDialog.DefaultExt +
                    "|" + _saveFileFilterAllFiles.Text + " (*.*)|*.*";

                if (fileDialog.ShowDialog(Form) == DialogResult.OK) {
                    Module.SaveBlobAs(fileDialog.FileName, string.Format("{0}:\"{1}\"", revisions[0].Guid, item.Name));
                }
            }
        }

        public void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Form.statusStrip.Hide();
        }

        public void openWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = Form.GitTree.SelectedNode.Tag;

            var gitItem = item as GitItem;
            if (gitItem == null || !(gitItem).IsBlob)
                return;

            var fileName = Path.Combine(Module.WorkingDir, (gitItem).FileName);
            OsShellUtil.OpenAs(fileName.ToNativePath());
        }

        public void pluginsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            Form.LoadPluginsInPluginMenu();
        }

        public void BisectClick(object sender, EventArgs e)
        {
            using (var frm = new FormBisect(Form.RevisionGrid)) {
                frm.ShowDialog(Form);
            }
            UICommands.RepoChangedNotifier.Notify();
        }

        public void fileHistoryDiffToolstripMenuItem_Click(object sender, EventArgs e)
        {
            GitItemStatus item = Form.DiffFiles.SelectedItem;

            if (item.IsTracked) {
                IList<GitRevision> revisions = Form.RevisionGrid.GetSelectedRevisions().Cast<GitRevision>().ToList();

                if (revisions.Count == 0 || GitRevision.IsArtificial(revisions[0].Guid))
                    UICommands.StartFileHistoryDialog(Form, item.Name);
                else
                    UICommands.StartFileHistoryDialog(Form, item.Name, revisions[0], false);
            }
        }

        public void blameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GitItemStatus item = Form.DiffFiles.SelectedItem;

            if (item.IsTracked) {
                IList<GitRevision> revisions = Form.RevisionGrid.GetSelectedRevisions().Cast<GitRevision>().ToList();

                if (revisions.Count == 0 || GitRevision.IsArtificial(revisions[0].Guid))
                    UICommands.StartFileHistoryDialog(Form, item.Name, null, false, true);
                else
                    UICommands.StartFileHistoryDialog(Form, item.Name, revisions[0], true, true);
            }
        }

        public void CurrentBranchDropDownOpening(object sender, EventArgs e)
        {
            var branchSelect = Form.branchSelect;
            branchSelect.DropDownItems.Clear();

            ToolStripMenuItem item = new ToolStripMenuItem(Form.checkoutBranchToolStripMenuItem.Text);
            item.ShortcutKeys = Form.checkoutBranchToolStripMenuItem.ShortcutKeys;
            item.ShortcutKeyDisplayString = Form.checkoutBranchToolStripMenuItem.ShortcutKeyDisplayString;
            branchSelect.DropDownItems.Add(item);
            item.Click += (hs, he) => CheckoutBranchToolStripMenuItemClick(hs, he);

            Form.branchSelect.DropDownItems.Add(new ToolStripSeparator());

            foreach (var branch in Module.GetRefs(false)) {
                var toolStripItem = branchSelect.DropDownItems.Add(branch.Name);
                toolStripItem.Click += BranchSelectToolStripItem_Click;

                //Make sure there are never more than 100 branches added to the menu
                //GitExtensions will hang when the drop down is to large...
                if (branchSelect.DropDownItems.Count > 100)
                    break;
            }

        }

        void BranchSelectToolStripItem_Click(object sender, EventArgs e)
        {
            var toolStripItem = (ToolStripItem)sender;
            UICommands.StartCheckoutBranch(Form, toolStripItem.Text, false);
        }

        public void _forkCloneMenuItem_Click(object sender, EventArgs e)
        {
            if (RepoHosts.GitHosters.Count > 0) {
                UICommands.StartCloneForkFromHoster(Form, RepoHosts.GitHosters[0], Form.SetGitModule);
                UICommands.RepoChangedNotifier.Notify();
            } else {
                MessageBox.Show(Form, _noReposHostPluginLoaded.Text, _errorCaption.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _viewPullRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var repoHost = RepoHosts.TryGetGitHosterForModule(Module);
            if (repoHost == null) {
                MessageBox.Show(Form, _noReposHostFound.Text, _errorCaption.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UICommands.StartPullRequestsDialog(Form, repoHost);
        }

        public void _createPullRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var repoHost = RepoHosts.TryGetGitHosterForModule(Module);
            if (repoHost == null) {
                MessageBox.Show(Form, _noReposHostFound.Text, _errorCaption.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UICommands.StartCreatePullRequest(Form, repoHost);
        }

        #region Hotkey commands

        public string HotkeySettingsName { get => "Browse"; }

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

        public void AddNotes()
        {
            Module.EditNotes(Form.RevisionGrid.GetSelectedRevisions().Count > 0 ?
                             Form.RevisionGrid.GetSelectedRevisions()[0].Guid : string.Empty);
            Form.FillCommitInfo();
        }

        public void FindFileInSelectedCommit()
        {
            Form.CommitInfoTabControl.SelectedTab = Form.TreeTabPage;
            EnabledSplitViewLayout(true);
            Form.GitTree.Focus();
            Form.FindFileOnClick(null, null);
        }

        public void QuickFetch()
        {
            FormProcess.ShowDialog(Form, Module.FetchCmd(string.Empty, string.Empty, string.Empty));
            UICommands.RepoChangedNotifier.Notify();
        }

        protected bool ExecuteCommand(int cmd)
        {
            switch ((Commands)cmd) {
                case Commands.GitBash:
                Module.RunBash();
                break;
                case Commands.GitGui:
                Module.RunGui();
                break;
                case Commands.GitGitK:
                Module.RunGitK();
                break;
                case Commands.FocusRevisionGrid:
                Form.RevisionGrid.Focus();
                break;
                case Commands.FocusCommitInfo:
                Form.CommitInfoTabControl.SelectedTab = Form.CommitInfoTabPage;
                break;
                case Commands.FocusFileTree:
                Form.CommitInfoTabControl.SelectedTab = Form.TreeTabPage;
                Form.GitTree.Focus();
                break;
                case Commands.FocusDiff:
                Form.CommitInfoTabControl.SelectedTab = Form.DiffTabPage;
                Form.DiffFiles.Focus();
                break;
                case Commands.Commit:
                Form.CommitToolStripMenuItemClick(null, null);
                break;
                case Commands.AddNotes:
                AddNotes();
                break;
                case Commands.FindFileInSelectedCommit:
                FindFileInSelectedCommit();
                break;
                case Commands.CheckoutBranch:
                CheckoutBranchToolStripMenuItemClick(null, null);
                break;
                case Commands.QuickFetch:
                QuickFetch();
                break;
                case Commands.QuickPull:
                UICommands.StartPullDialog(Form, true);
                break;
                case Commands.QuickPush:
                UICommands.StartPushDialog(Form, true);
                break;
                case Commands.RotateApplicationIcon:
                RotateApplicationIcon();
                break;
                case Commands.CloseRepositry:
                Form.CloseToolStripMenuItemClick(null, null);
                break;
                default:
                return Form.DoExecuteCommand(cmd);
            }

            return true;
        }

        protected void RotateApplicationIcon()
        {
            if (Debugger.IsAttached) Debugger.Break();
            //Form.ApplicationIcon = GetApplicationIcon(Settings.IconStyle, Settings.IconColor);
            //Form.Icon = Form.ApplicationIcon;
        }

        public static Icon GetApplicationIcon(string iconStyle, string iconColor)
               => GitExtensionsForm.GetApplicationIcon(iconStyle, iconColor);

        internal bool ExecuteCommand(Commands cmd)
        {
            return ExecuteCommand((int)cmd);
        }

        #endregion


        public void SettingsClick(object sender, EventArgs e)
        {
            //Settings = Form.Settings;
            //var translation = Settings.Translation;
            UICommands.StartSettingsDialog(Form);
            //if (translation != Settings.Translation)
            //    Form.Translate();

            Form.HotkeysSet(HotkeySettingsManager.LoadHotkeys(HotkeySettingsName));
            Form.RevisionGrid.ReloadHotkeys();
            Form.RevisionGrid.ReloadTranslation();
        }


        public void toggleSplitViewLayout_Click(object sender, EventArgs e)
        {
            EnabledSplitViewLayout(Form.RightSplitContainer.Panel2.Height == 0 && Form.RightSplitContainer.Height > 0);
        }

        public void EnabledSplitViewLayout(bool enabled)
        {
            if (enabled)
                Form.RightSplitContainer.SplitterDistance = (Form.RightSplitContainer.Height / 5) * 2;
            else
                Form.RightSplitContainer.SplitterDistance = Form.RightSplitContainer.Height;
        }

        public void editCheckedOutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = Form.GitTree.SelectedNode.Tag;

            var gitItem = item as GitItem;
            if (gitItem == null || !gitItem.IsBlob)
                return;

            var fileName = Path.Combine(Module.WorkingDir, (gitItem).FileName);
            UICommands.StartFileEditorDialog(fileName);
        }

        #region Git file tree drag-drop

        public Rectangle gitTreeDragBoxFromMouseDown;

        public void GitTree_MouseDown(object sender, MouseEventArgs e)
        {
            //DRAG
            if (e.Button == MouseButtons.Left) {
                // Remember the point where the mouse down occurred.
                // The DragSize indicates the size that the mouse can move
                // before a drag event should be started.
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                gitTreeDragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                                e.Y - (dragSize.Height / 2)),
                                                                dragSize);
            }
        }

        public void GitTree_MouseMove(object sender, MouseEventArgs e)
        {
            TreeView gitTree = (TreeView)sender;

            //DRAG
            // If the mouse moves outside the rectangle, start the drag.
            if (gitTreeDragBoxFromMouseDown != Rectangle.Empty &&
                !gitTreeDragBoxFromMouseDown.Contains(e.X, e.Y))
            {
                var fileList = new StringCollection();

                //foreach (GitItemStatus item in SelectedItems)
                if (gitTree.SelectedNode != null) {
                    GitItem item = gitTree.SelectedNode.Tag as GitItem;
                    if (item != null) {
                        string fileName = Path.Combine(Module.WorkingDir, item.FileName);

                        fileList.Add(fileName.ToNativePath());
                    }

                    DataObject obj = new DataObject();
                    obj.SetFileDropList(fileList);

                    // Proceed with the drag and drop, passing in the list item.
                    Form.DoDragDrop(obj, DragDropEffects.Copy);
                    gitTreeDragBoxFromMouseDown = Rectangle.Empty;
                }
            }
        }
        #endregion

        private int getNextIdx(int curIdx, int maxIdx, bool searchBackward)
        {
            if (searchBackward) {
                if (curIdx == 0) {
                    curIdx = maxIdx;
                } else {
                    curIdx--;
                }
            } else {
                if (curIdx == maxIdx) {
                    curIdx = 0;
                } else {
                    curIdx++;
                }
            }
            return curIdx;
        }

        public Tuple<int, string> getNextPatchFile(bool searchBackward)
        {
            var revisions = Form.RevisionGrid.GetSelectedRevisions();
            if (revisions.Count == 0)
                return null;
            int idx = Form.DiffFiles.SelectedIndex;
            if (idx == -1)
                return new Tuple<int, string>(idx, null);

            idx = getNextIdx(idx, Form.DiffFiles.GitItemStatuses.Count() - 1, searchBackward);
            _dontUpdateOnIndexChange = true;
            Form.DiffFiles.SelectedIndex = idx;
            _dontUpdateOnIndexChange = false;
            return new Tuple<int, string>(idx, Form.DiffText.GetSelectedPatch(Form.RevisionGrid, Form.DiffFiles.SelectedItem));
        }

        public bool _dontUpdateOnIndexChange;

        //
        // diff context menu
        //
        public void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form.OpenContainingFolder(Form.DiffFiles, Module);
        }

        public void ShowLog()
        {
            FormGitLog.ShowOrActivate(Form);
        }

        public void UpdateLog()
        {
            FormGitLog.Update(Form);
        }
    }

}