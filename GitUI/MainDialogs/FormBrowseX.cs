using GitUI.UserControls.RevisionGridClasses;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitCommands;
using GitCommands.Repository;
using GitCommands.Utils;
using GitUI.CommandsDialogs.BrowseDialog;
using GitUI.CommandsDialogs.BrowseDialog.DashboardControl;
using GitUI.Hotkey;
using GitUI.Plugin;
using GitUI.Properties;
using GitUIPluginInterfaces;
using Microsoft.Win32;
using ResourceManager;
using Settings = GitCommands.AppSettings;
#if !__MonoCS__ && !NOAPICODE
using Microsoft.WindowsAPICodePack.Taskbar;
#endif

namespace GitUI.CommandsDialogs
{
    public partial class FormBrowseX : FormBrowse, IBrowseRepo
    {
        #region Translation

        private readonly TranslationString _stashCount =
            new TranslationString("{0} saved {1}");
        private readonly TranslationString _stashPlural =
            new TranslationString("stashes");
        private readonly TranslationString _stashSingular =
            new TranslationString("stash");

        private readonly TranslationString _warningMiddleOfBisect =
            new TranslationString("You are in the middle of a bisect");
        private readonly TranslationString _warningMiddleOfRebase =
            new TranslationString("You are in the middle of a rebase");
        private readonly TranslationString _warningMiddleOfPatchApply =
            new TranslationString("You are in the middle of a patch apply");

        private readonly TranslationString _hintUnresolvedMergeConflicts =
            new TranslationString("There are unresolved merge conflicts!");

        private readonly TranslationString _noBranchTitle =
            new TranslationString("no branch");

        private readonly TranslationString _noSubmodulesPresent =
            new TranslationString("No submodules");
        private readonly TranslationString _topProjectModuleFormat =
            new TranslationString("Top project: {0}");
        private readonly TranslationString _superprojectModuleFormat =
            new TranslationString("Superproject: {0}");

        private readonly TranslationString _saveFileFilterCurrentFormat =
            new TranslationString("Current format");
        private readonly TranslationString _saveFileFilterAllFiles =
            new TranslationString("All files");

        private readonly TranslationString _indexLockDeleted =
            new TranslationString("index.lock deleted.");
        private readonly TranslationString _indexLockNotFound =
            new TranslationString("index.lock not found at:");

        private readonly TranslationString _errorCaption =
            new TranslationString("Error");
        private readonly TranslationString _loading =
            new TranslationString("Loading...");

        private readonly TranslationString _noReposHostPluginLoaded =
            new TranslationString("No repository host plugin loaded.");

        private readonly TranslationString _noReposHostFound =
            new TranslationString("Could not find any relevant repository hosts for the currently open repository.");

        private readonly TranslationString _configureWorkingDirMenu =
            new TranslationString("Configure this menu");

        private readonly TranslationString directoryIsNotAValidRepositoryCaption =
            new TranslationString("Open");

        private readonly TranslationString directoryIsNotAValidRepository =
            new TranslationString("The selected item is not a valid git repository.\n\nDo you want to abort and remove it from the recent repositories list?");

        private readonly TranslationString _updateCurrentSubmodule =
            new TranslationString("Update current submodule");

        private readonly TranslationString _nodeNotFoundNextAvailableParentSelected =
            new TranslationString("Node not found. The next available parent node will be selected.");

        private readonly TranslationString _nodeNotFoundSelectionNotChanged =
            new TranslationString("Node not found. File tree selection was not changed.");

        private readonly TranslationString _diffNoSelection =
            new TranslationString("Diff (no selection)");

        private readonly TranslationString _diffParentWithSelection =
            new TranslationString("Diff (A: parent --> B: selection)");

        private readonly TranslationString _diffTwoSelected =
            new TranslationString("Diff (A: first --> B: second)");

        private readonly TranslationString _diffNotSupported =
            new TranslationString("Diff (not supported)");

        private readonly TranslationString _pullFetch =
            new TranslationString("Pull - fetch");
        private readonly TranslationString _pullFetchAll =
            new TranslationString("Pull - fetch all");
        private readonly TranslationString _pullMerge =
            new TranslationString("Pull - merge");
        private readonly TranslationString _pullRebase =
            new TranslationString("Pull - rebase");
        private readonly TranslationString _pullOpenDialog =
            new TranslationString("Open pull dialog");

        private readonly TranslationString _resetFileCaption =
            new TranslationString("Reset");
        private readonly TranslationString _resetFileText =
            new TranslationString("Are you sure you want to reset this file or directory?");
        private readonly TranslationString _resetFileError =
            new TranslationString("Exactly one revision must be selected. Abort.");
        #endregion

        #region Properties

        private Dashboard _dashboard;
        private ToolStripItem _rebase;
        private ToolStripItem _bisect;
        private ToolStripItem _warning;

#if !__MonoCS__ && !NOAPICODE
        private ThumbnailToolBarButton _commitButton;
        private ThumbnailToolBarButton _pushButton;
        private ThumbnailToolBarButton _pullButton;
        private bool _toolbarButtonsCreated;
#endif
        private bool _dontUpdateOnIndexChange;
        private readonly ToolStripGitStatus _toolStripGitStatus;
        private readonly FilterRevisionsHelper _filterRevisionsHelper;
        private readonly FilterBranchHelper _filterBranchHelper;

        private string _diffTabPageTitleBase = "";

        private readonly FormBrowseMenus _formBrowseMenus;
#pragma warning disable 0414
        private readonly FormBrowseMenuCommands _formBrowseMenuCommands;
#pragma warning restore 0414

        #endregion

        /// <summary>
        /// For VS designer
        /// </summary>
        private FormBrowseX() : base(null, null)
        {
            InitializeComponent();
            Translate();
        }

        public override void InitializeComponent() => DoInitializeComponent();

        public FormBrowseX(GitUICommands aCommands, string filter)
            : base(aCommands, filter)
        {
            InitializeComponent();

            // set tab page images
            {
                var imageList = new ImageList();
                CommitInfoTabControl.ImageList = imageList;
                imageList.ColorDepth = ColorDepth.Depth8Bit;
                imageList.Images.Add(global::GitUI.Properties.Resources.IconCommit);
                imageList.Images.Add(global::GitUI.Properties.Resources.IconFileTree);
                imageList.Images.Add(global::GitUI.Properties.Resources.IconDiff);
                CommitInfoTabControl.TabPages[0].ImageIndex = 0;
                CommitInfoTabControl.TabPages[1].ImageIndex = 1;
                CommitInfoTabControl.TabPages[2].ImageIndex = 2;
            }

            RevisionGrid.UICommandsSource = this;
            Repositories.LoadRepositoryHistoryAsync();
            Task.Factory.StartNew(PluginLoader.Load)
                .ContinueWith((task) => RegisterPlugins(), TaskScheduler.FromCurrentSynchronizationContext());
            RevisionGrid.GitModuleChanged += SetGitModule;

            // TODO: ToolStripButton showFirstParentButton
            Debugger.Break();
            ToolStripButton showFirstParentButton = null;
            _filterRevisionsHelper = new FilterRevisionsHelper(toolStripTextBoxFilter, toolStripDropDownButton1,
                RevisionGrid, toolStripLabel2,
                showFirstParentButton: showFirstParentButton,
                form: this);

            _filterBranchHelper = new FilterBranchHelper(toolStripBranches, toolStripDropDownButton2, RevisionGrid);
            toolStripBranches.DropDown += toolStripBranches_DropDown_ResizeDropDownWidth;

            Translate ();

            if (Settings.ShowGitStatusInBrowseToolbar)
            {
                _toolStripGitStatus = new ToolStripGitStatus
                                 {
                                     ImageTransparentColor = Color.Magenta
                                 };
                if (aCommands != null)
                    _toolStripGitStatus.UICommandsSource = this;
                _toolStripGitStatus.Click += StatusClick;
                ToolStrip.Items.Insert(ToolStrip.Items.IndexOf(toolStripButton1), _toolStripGitStatus);
                ToolStrip.Items.Remove(toolStripButton1);
                _toolStripGitStatus.CommitTranslatedString = toolStripButton1.Text;
            }

            if (!EnvUtils.RunningOnWindows())
            {
                toolStripSeparator6.Visible = false;
                PuTTYToolStripMenuItem.Visible = false;
            }

            RevisionGrid.SelectionChanged += RevisionGridSelectionChanged;
            DiffText.ExtraDiffArgumentsChanged += DiffTextExtraDiffArgumentsChanged;
            _filterRevisionsHelper.SetFilter(filter);
            DiffText.SetFileLoader(getNextPatchFile);

            GitTree.ImageList = new ImageList();
            GitTree.ImageList.Images.Add(Properties.Resources.New); //File
            GitTree.ImageList.Images.Add(Properties.Resources.Folder); //Folder
            GitTree.ImageList.Images.Add(Properties.Resources.IconFolderSubmodule); //Submodule

            GitTree.MouseDown += GitTree_MouseDown;
            GitTree.MouseMove += GitTree_MouseMove;

            this.HotkeysEnabled = true;
            this.Hotkeys = HotkeySettingsManager.LoadHotkeys(HotkeySettingsName);
            this.toolPanel.SplitterDistance = this.ToolStrip.Height;
            this._dontUpdateOnIndexChange = false;
            GitUICommandsChanged += (a, e) =>
            {
                var oldcommands = e.OldCommands;
                RefreshPullIcon();
                oldcommands.PostRepositoryChanged -= UICommands_PostRepositoryChanged;
                UICommands.PostRepositoryChanged += UICommands_PostRepositoryChanged;
                oldcommands.BrowseRepo = null;
                UICommands.BrowseRepo = this;
            };
            if (aCommands != null)
            {
                RefreshPullIcon();
                UICommands.PostRepositoryChanged += UICommands_PostRepositoryChanged;
                UICommands.BrowseRepo = this;
            }

            FillBuildReport();  // Ensure correct page visibility
            RevisionGrid.ShowBuildServerInfo = true;

            _formBrowseMenuCommands = new FormBrowseMenuCommands(this);
            _formBrowseMenus = new FormBrowseMenus(menuStrip1);
            RevisionGrid.MenuCommands.MenuChanged += (sender, e) => _formBrowseMenus.OnMenuCommandsPropertyChanged();
            SystemEvents.SessionEnding += (sender, args) => SaveApplicationSettings();

			FillTerminalTab();
        }

        private new void Translate()
        {
            base.Translate();
            _diffTabPageTitleBase = DiffTabPage.Text;
        }

        void UICommands_PostRepositoryChanged(object sender, GitUIBaseEventArgs e)
        {
            this.InvokeAsync(RefreshRevisions);
        }

        private string _oldRevision;
        private GitItemStatus _oldDiffItem;
        private void RefreshRevisions()
        {
            if (_dashboard == null || !_dashboard.Visible)
            {
                var revisions = RevisionGrid.GetSelectedRevisions();
                if (revisions.Count != 0)
                {
                    _oldRevision = revisions[0].Guid;
                    _oldDiffItem = DiffFiles.SelectedItem;
                }
                else
                {
                    _oldRevision = null;
                    _oldDiffItem = null;
                }
                RevisionGrid.ForceRefreshRevisions();
                InternalInitialize(true);
            }
        }

        #region IBrowseRepo

        public override void GoToRef(string refName, bool showNoRevisionMsg)
        {
            RevisionGrid.GoToRef(refName, showNoRevisionMsg);
        }

        public override void SetWorkingDir(string path)
        {
            SetGitModule(this, new GitModuleEventArgs(new GitModule(path)));
        }

        #endregion

        private void ShowDashboard()
        {
            if (_dashboard == null)
            {
                _dashboard = new Dashboard();
                _dashboard.GitModuleChanged += SetGitModule;
                toolPanel.Panel2.Controls.Add(_dashboard);
                _dashboard.Dock = DockStyle.Fill;
            }
            else
                _dashboard.Refresh();
            _dashboard.Visible = true;
            _dashboard.BringToFront();
            _dashboard.ShowRecentRepositories();
        }

        private void HideDashboard()
        {
            if (_dashboard != null && _dashboard.Visible)
                _dashboard.Visible = false;
        }

        private void GitTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var item = e.Node.Tag as GitItem;
            if (item == null)
                return;

            if (item.IsBlob)
                FileText.ViewGitItem(item.FileName, item.Guid);
            else if (item.IsCommit)
                FileText.ViewText(item.FileName,
                    LocalizationHelpers.GetSubmoduleText(Module, item.FileName, item.Guid));
            else
                FileText.ViewText("", "");
        }

        #region Load, Watcher methods 1

        private void BrowseLoad(object sender, EventArgs e)
        {
#if !__MonoCS__
            if (EnvUtils.RunningOnWindows() && TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.ApplicationId = "GitExtensions";
            }
#endif
            HideVariableMainMenuItems();

            RevisionGrid.Load();
            _filterBranchHelper.InitToolStripBranchFilter();

            Cursor.Current = Cursors.WaitCursor;
            InternalInitialize(false);
            RevisionGrid.Focus();
            RevisionGrid.IndexWatcher.Reset();

            RevisionGrid.IndexWatcher.Changed += _indexWatcher_Changed;

            Cursor.Current = Cursors.Default;


            try
            {
                if (Settings.PlaySpecialStartupSound)
                {
                    using (var cowMoo = Resources.cow_moo)
                        new System.Media.SoundPlayer(cowMoo).Play();
                }
            }
            catch // This code is just for fun, we do not want the program to crash because of it.
            {
            }
        }

        void _indexWatcher_Changed(object sender, IndexChangedEventArgs e)
        {
            bool indexChanged = e.IsIndexChanged;
            this.InvokeAsync(() =>
            {
                RefreshButton.Image = indexChanged && Settings.UseFastChecks && Module.IsValidGitWorkingDir()
                                          ? Resources.arrow_refresh_dirty
                                          : Resources.arrow_refresh;
            });
        }

        private bool _pluginsLoaded;
        private void LoadPluginsInPluginMenu()
        {
            if (_pluginsLoaded)
                return;
            foreach (var plugin in LoadedPlugins.Plugins)
            {
                var item = new ToolStripMenuItem { Text = plugin.Description, Tag = plugin };
                item.Click += ItemClick;
                pluginsToolStripMenuItem.DropDownItems.Insert(pluginsToolStripMenuItem.DropDownItems.Count - 2, item);
            }
            _pluginsLoaded = true;
            UpdatePluginMenu(Module.IsValidGitWorkingDir());
        }

        /// <summary>
        ///   Execute plugin
        /// </summary>
        private void ItemClick(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            var plugin = menuItem.Tag as IGitPlugin;
            if (plugin == null)
                return;

            var eventArgs = new GitUIEventArgs(this, UICommands);

            bool refresh = plugin.Execute(eventArgs);
            if (refresh)
                RefreshToolStripMenuItemClick(null, null);
        }

        private void UpdatePluginMenu(bool validWorkingDir)
        {
            foreach (ToolStripItem item in pluginsToolStripMenuItem.DropDownItems)
            {
                var plugin = item.Tag as IGitPluginForRepository;

                item.Enabled = plugin == null || validWorkingDir;
            }
        }

        private void RegisterPlugins()
        {
            foreach (var plugin in LoadedPlugins.Plugins)
                plugin.Register(UICommands);

            UICommands.RaisePostRegisterPlugin(this);
        }

        private void UnregisterPlugins()
        {
            foreach (var plugin in LoadedPlugins.Plugins)
                plugin.Unregister(UICommands);
        }

        /// <summary>
        /// to avoid showing menu items that should not be there during
        /// the transition from dashboard to repo browser and vice versa
        ///
        /// and reset hotkeys that are shared between mutual exclusive menu items
        /// </summary>
        private void HideVariableMainMenuItems()
        {
            dashboardToolStripMenuItem.Visible = false;
            repositoryToolStripMenuItem.Visible = false;
            commandsToolStripMenuItem.Visible = false;
            refreshToolStripMenuItem.ShortcutKeys = Keys.None;
            refreshDashboardToolStripMenuItem.ShortcutKeys = Keys.None;
            _repositoryHostsToolStripMenuItem.Visible = false;
            _formBrowseMenus.RemoveAdditionalMainMenuItems();
            menuStrip1.Refresh();
        }


        #endregion

        private void InternalInitialize(bool hard)
        {
            Cursor.Current = Cursors.WaitCursor;

            UICommands.RaisePreBrowseInitialize(this);

            // check for updates
            if (Settings.LastUpdateCheck.AddDays(7) < DateTime.Now)
            {
                Settings.LastUpdateCheck = DateTime.Now;
                var updateForm = new FormUpdates(Module.AppVersion);
                updateForm.SearchForUpdatesAndShow(Owner, false);
            }

            bool bareRepository = Module.IsBareRepository();
            bool validWorkingDir = Module.IsValidGitWorkingDir();
            bool hasWorkingDir = !string.IsNullOrEmpty(Module.WorkingDir);
            branchSelect.Text = validWorkingDir ? Module.GetSelectedBranch() : "";
            if (hasWorkingDir)
                HideDashboard();
            else
                ShowDashboard();

            toolStripButtonLevelUp.Enabled = hasWorkingDir && !bareRepository;
            CommitInfoTabControl.Visible = validWorkingDir;
            fileExplorerToolStripMenuItem.Enabled = validWorkingDir;
            manageRemoteRepositoriesToolStripMenuItem1.Enabled = validWorkingDir;
            branchSelect.Enabled = validWorkingDir;
            toolStripButton1.Enabled = validWorkingDir && !bareRepository;
            if (_toolStripGitStatus != null)
                _toolStripGitStatus.Enabled = validWorkingDir;
            toolStripButtonPull.Enabled = validWorkingDir;
            toolStripButtonPush.Enabled = validWorkingDir;
            dashboardToolStripMenuItem.Visible = !validWorkingDir;
            repositoryToolStripMenuItem.Visible = validWorkingDir;
            commandsToolStripMenuItem.Visible = validWorkingDir;
            if (validWorkingDir)
            {
                refreshToolStripMenuItem.ShortcutKeys = Keys.F5;
            }
            else
            {
                refreshDashboardToolStripMenuItem.ShortcutKeys = Keys.F5;
            }
            UpdatePluginMenu(validWorkingDir);
            gitMaintenanceToolStripMenuItem.Enabled = validWorkingDir;
            editgitignoreToolStripMenuItem1.Enabled = validWorkingDir;
            editgitattributesToolStripMenuItem.Enabled = validWorkingDir;
            editmailmapToolStripMenuItem.Enabled = validWorkingDir;
            toolStripSplitStash.Enabled = validWorkingDir && !bareRepository;
            commitcountPerUserToolStripMenuItem.Enabled = validWorkingDir;
            _createPullRequestsToolStripMenuItem.Enabled = validWorkingDir;
            _viewPullRequestsToolStripMenuItem.Enabled = validWorkingDir;
            //Only show "Repository hosts" menu item when there is at least 1 repository host plugin loaded
            _repositoryHostsToolStripMenuItem.Visible = RepoHosts.GitHosters.Count > 0;
            if (RepoHosts.GitHosters.Count == 1)
                _repositoryHostsToolStripMenuItem.Text = RepoHosts.GitHosters[0].Description;
            _filterBranchHelper.InitToolStripBranchFilter();

            if (repositoryToolStripMenuItem.Visible)
            {
                manageSubmodulesToolStripMenuItem.Enabled = !bareRepository;
                updateAllSubmodulesToolStripMenuItem.Enabled = !bareRepository;
                synchronizeAllSubmodulesToolStripMenuItem.Enabled = !bareRepository;
                editgitignoreToolStripMenuItem1.Enabled = !bareRepository;
                editgitattributesToolStripMenuItem.Enabled = !bareRepository;
                editmailmapToolStripMenuItem.Enabled = !bareRepository;
            }

            if (commandsToolStripMenuItem.Visible)
            {
                commitToolStripMenuItem.Enabled = !bareRepository;
                mergeToolStripMenuItem.Enabled = !bareRepository;
                rebaseToolStripMenuItem1.Enabled = !bareRepository;
                pullToolStripMenuItem1.Enabled = !bareRepository;
                resetToolStripMenuItem.Enabled = !bareRepository;
                cleanupToolStripMenuItem.Enabled = !bareRepository;
                stashToolStripMenuItem.Enabled = !bareRepository;
                checkoutBranchToolStripMenuItem.Enabled = !bareRepository;
                mergeBranchToolStripMenuItem.Enabled = !bareRepository;
                rebaseToolStripMenuItem.Enabled = !bareRepository;
                runMergetoolToolStripMenuItem.Enabled = !bareRepository;
                cherryPickToolStripMenuItem.Enabled = !bareRepository;
                checkoutToolStripMenuItem.Enabled = !bareRepository;
                bisectToolStripMenuItem.Enabled = !bareRepository;
                applyPatchToolStripMenuItem.Enabled = !bareRepository;
                SvnRebaseToolStripMenuItem.Enabled = !bareRepository;
                SvnDcommitToolStripMenuItem.Enabled = !bareRepository;
            }

            stashChangesToolStripMenuItem.Enabled = !bareRepository;
            gitGUIToolStripMenuItem.Enabled = !bareRepository;

            SetShortcutKeyDisplayStringsFromHotkeySettings();

            if (hard && hasWorkingDir)
                ShowRevisions();
            RefreshWorkingDirCombo();
            Text = GenerateWindowTitle(Module.WorkingDir, validWorkingDir, branchSelect.Text);
            DiffText.Font = Settings.DiffFont;
            UpdateJumplist(validWorkingDir);

            OnActivate();
            // load custom user menu
            LoadUserMenu();

            if (validWorkingDir)
            {
                // add Navigate and View menu
                _formBrowseMenus.ResetMenuCommandSets();
                //// _formBrowseMenus.AddMenuCommandSet(MainMenuItem.NavigateMenu, _formBrowseMenuCommands.GetNavigateMenuCommands()); // not used at the moment
                _formBrowseMenus.AddMenuCommandSet(MainMenuItem.NavigateMenu, RevisionGrid.MenuCommands.GetNavigateMenuCommands());
                _formBrowseMenus.AddMenuCommandSet(MainMenuItem.ViewMenu, RevisionGrid.MenuCommands.GetViewMenuCommands());

                _formBrowseMenus.InsertAdditionalMainMenuItems(repositoryToolStripMenuItem);
            }

            UICommands.RaisePostBrowseInitialize(this);

            Cursor.Current = Cursors.Default;
        }

        private void OnActivate()
        {
            CheckForMergeConflicts();
            UpdateStashCount();
            UpdateSubmodulesList();
        }


        internal Keys GetShortcutKeys(Commands cmd)
        {
            return GetShortcutKeys((int)cmd);
        }

        /// <summary>
        ///
        /// </summary>
        private void SetShortcutKeyDisplayStringsFromHotkeySettings()
        {
            gitBashToolStripMenuItem.ShortcutKeyDisplayString = GetShortcutKeys(Commands.GitBash).ToShortcutKeyDisplayString();
            commitToolStripMenuItem.ShortcutKeyDisplayString = GetShortcutKeys(Commands.Commit).ToShortcutKeyDisplayString();
            // TODO: add more
        }
        
    }
}
