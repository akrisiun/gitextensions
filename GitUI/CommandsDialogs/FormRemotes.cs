﻿using System;

=======
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
using System.Windows.Forms;
using GitCommands;
using GitCommands.Config;
using GitCommands.Repository;

=======
using GitCommands.Remote;
using GitUIPluginInterfaces;
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
using ResourceManager;

namespace GitUI.CommandsDialogs
{
    public partial class FormRemotes : GitModuleForm
    {

        private string _remote = "";
=======
        private IGitRemoteManager _remoteManager;
        private GitRemote _selectedRemote;
        private readonly ListViewGroup _lvgEnabled, _lvgDisabled;
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d

        private readonly TranslationString _remoteBranchDataError =
            new TranslationString("Invalid ´{1}´ found for branch ´{0}´." + Environment.NewLine +
                                  "Value has been reset to empty value.");

        private readonly TranslationString _questionAutoPullBehaviour =
            new TranslationString("You have added a new remote repository." + Environment.NewLine +
                                  "Do you want to automatically configure the default push and pull behavior for this remote?");

        private readonly TranslationString _questionAutoPullBehaviourCaption =
            new TranslationString("New remote");

        private readonly TranslationString _warningValidRemote =
            new TranslationString("You need to configure a valid url for this remote");

        private readonly TranslationString _warningValidRemoteCaption =
            new TranslationString("Url needed");

        private readonly TranslationString _hintDelete =
            new TranslationString("Delete");

        private readonly TranslationString _questionDeleteRemote =
            new TranslationString("Are you sure you want to delete this remote?");

        private readonly TranslationString _questionDeleteRemoteCaption =
            new TranslationString("Delete");

        private readonly TranslationString _sshKeyOpenFilter =
            new TranslationString("Private key (*.ppk)");

        private readonly TranslationString _sshKeyOpenCaption =
            new TranslationString("Select ssh key file");

        private readonly TranslationString _warningNoKeyEntered =
            new TranslationString("No SSH key file entered");

        private readonly TranslationString _labelUrlAsFetch =
            new TranslationString("Fetch Url");
        private readonly TranslationString _labelUrlAsFetchPush =
            new TranslationString("Url");


        public delegate void EventRemoteChange(string remoteName);
        public delegate void EventRemoteRenamed(string orgName, string newName);

=======
        private readonly TranslationString _gbMgtPanelHeaderNew =
            new TranslationString("Create New Remote");

        private readonly TranslationString _gbMgtPanelHeaderEdit =
            new TranslationString("Edit Remote Details");

        private readonly TranslationString _btnDeleteTooltip =
            new TranslationString("Delete the selected remote");

        private readonly TranslationString _btnNewTooltip =
            new TranslationString("Add a new remote");

        private readonly TranslationString _btnToggleStateTooltip_Activate =
            new TranslationString("Activate the selected remote");

        private readonly TranslationString _btnToggleStateTooltip_Deactivate =
            new TranslationString(@"Deactivate the selected remote.
Inactive remote is completely invisible to git.");

        private readonly TranslationString _lvgEnabledHeader =
            new TranslationString("Active");

        private readonly TranslationString _lvgDisabledHeader =
            new TranslationString("Inactive");
        #endregion

        public delegate void EventRemoteChange(string remoteName);
        public delegate void EventRemoteRenamed(string orgName, string newName);

>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        public EventRemoteChange OnRemoteDeleted;
        public EventRemoteRenamed OnRemoteRenamedOrAdded;

        public FormRemotes(GitUICommands aCommands)
            : base(aCommands)
        {
            InitializeComponent();
            Translate();

        }

        private void FormRemotesLoad(object sender, EventArgs e)
=======

            // remove text from 'new' and 'delete' buttons because now they are represented by icons
            New.Text = string.Empty;
            Delete.Text = string.Empty;
            toolTip1.SetToolTip(New, _btnNewTooltip.Text);
            toolTip1.SetToolTip(Delete, _btnDeleteTooltip.Text);

            _lvgEnabled = new ListViewGroup(_lvgEnabledHeader.Text, HorizontalAlignment.Left);
            _lvgDisabled = new ListViewGroup(_lvgDisabledHeader.Text, HorizontalAlignment.Left);
            Remotes.Groups.AddRange(new[] { _lvgEnabled, _lvgDisabled });

            Application.Idle += application_Idle;
        }

        /// <summary>
        /// If this is not null before showing the dialog the given
        /// remote name will be preselected in the listbox
        /// </summary>
        public string PreselectRemoteOnLoad { get; set; }

        /// <summary>
        /// Gets the list of remotes configured in .git/config file.
        /// </summary>
        private List<GitRemote> UserGitRemotes { get; set; }


        private void BindRemotes(string preselectRemote)
        {
            // we need to unwire and rewire the events to avoid excessive flickering
            Remotes.SelectedIndexChanged -= Remotes_SelectedIndexChanged;
            Remotes.Items.Clear();
            Remotes.Items.AddRange(UserGitRemotes.Select(remote =>
            {
                var group = remote.Disabled ? _lvgDisabled : _lvgEnabled;
                var color = remote.Disabled ? SystemColors.GrayText : SystemColors.WindowText;
                return new ListViewItem(group) { Text = remote.Name, Tag = remote, ForeColor = color };
            }).ToArray());
            Remotes.SelectedIndexChanged += Remotes_SelectedIndexChanged;

            Remotes.SelectedIndices.Clear();
            if (UserGitRemotes.Any())
            {
                if (!string.IsNullOrEmpty(preselectRemote))
                {
                    var lvi = Remotes.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == preselectRemote);
                    if (lvi != null)
                    {
                        lvi.Selected = true;
                        flpnlRemoteManagement.Enabled = !((GitRemote)lvi.Tag).Disabled;
                    }
                }
                // default fallback - if the preselection didn't work select the first available one
                if (Remotes.SelectedIndices.Count < 1)
                {
                    var group = _lvgEnabled.Items.Count > 0 ? _lvgEnabled : _lvgDisabled;
                    group.Items[0].Selected = true;
                }
                Remotes.Select();
            }
            else
            {
                RemoteName.Focus();
            }
        }

        private void BindBtnToggleState(bool disabled)
        {
            if (disabled)
            {
                btnToggleState.Image = Properties.Resources.light_bulb_icon_off_16;
                toolTip1.SetToolTip(btnToggleState, (_btnToggleStateTooltip_Activate.Text ?? "").Trim());
            }
            else
            {
                btnToggleState.Image = Properties.Resources.light_bulb_icon_on_16;
                toolTip1.SetToolTip(btnToggleState, (_btnToggleStateTooltip_Deactivate.Text ?? "").Trim());
            }
        }

        private IGitRef GetHeadForSelectedRemoteBranch()
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        {
            Initialize();

            if (!string.IsNullOrEmpty(PreselectRemoteOnLoad))
            {
                Remotes.Text = PreselectRemoteOnLoad;
            }

            RemotesSelectedIndexChanged(null, null);
        }

        private void RemoteBranchesDataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show(this,
                string.Format(_remoteBranchDataError.Text, RemoteBranches.Rows[e.RowIndex].Cells[0].Value,
                    RemoteBranches.Columns[e.ColumnIndex].HeaderText));
=======
            // refresh registered git remotes
            UserGitRemotes = _remoteManager.LoadRemotes(true).ToList();
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d

            RemoteBranches.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
        }


        /// <summary>
        /// If this is not null before showing the dialog the given
        /// remote name will be preselected in the listbox
        /// </summary>
        public string PreselectRemoteOnLoad { get; set; }
=======
        private void InitialiseTabRemotes(string preselectRemote = null)
        {
            // because the binding the same BindingList to multiple controls,
            // and changes in one of the bound control automatically get reflected
            // in the other control, which causes rather frustrating UX.
            // to address that, re-create binding lists for each individual control
            var repos = Repositories.RemoteRepositoryHistory.Repositories;
            try
            {
                // to stop the flicker binding the lists and
                // when the selected remote is getting reset and then selected again
                Url.BeginUpdate();
                comboBoxPushUrl.BeginUpdate();
                Remotes.BeginUpdate();

                Url.DataSource = new BindingList<Repository>(repos);
                Url.DisplayMember = "Path";
                Url.SelectedItem = null;

                comboBoxPushUrl.DataSource = new BindingList<Repository>(repos);
                comboBoxPushUrl.DisplayMember = "Path";
                comboBoxPushUrl.SelectedItem = null;

                BindRemotes(preselectRemote);
            }
            finally
            {
                Remotes.EndUpdate();
                Url.EndUpdate();
                comboBoxPushUrl.EndUpdate();
            }
        }
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d

        private void Initialize()
        {
            FillUrlDropDown();
            FillPushUrlDropDown();


            Remotes.DataSource = Module.GetRemotes(false);
=======
            RemoteRepositoryCombo.Sorted = false;
            RemoteRepositoryCombo.DataSource = new[] { new GitRemote() }.Union(UserGitRemotes).ToList();
            RemoteRepositoryCombo.DisplayMember = "Name";
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d

            var heads = Module.GetRefs(false, true);
            RemoteBranches.DataSource = heads;

            RemoteBranches.DataError += RemoteBranchesDataError;

            PuTTYSSH.Visible = GitCommandHelpers.Plink();
        }

        private void FillUrlDropDown()
        {
            Url.DataSource = Repositories.RemoteRepositoryHistory.Repositories;
            Url.DisplayMember = "Path";
        }

        private void FillPushUrlDropDown()
        {

            comboBoxPushUrl.DataSource = Repositories.RemoteRepositoryHistory.Repositories;
            comboBoxPushUrl.DisplayMember = "Path";
=======
            // we need this event only once, so unwire
            Application.Idle -= application_Idle;

            pnlMgtPuttySsh.Visible = GitCommandHelpers.Plink();
            // if Putty SSH isn't enabled, reduce the minimum height of the form
            MinimumSize = new Size(MinimumSize.Width, pnlMgtPuttySsh.Visible ? MinimumSize.Height : MinimumSize.Height - pnlMgtPuttySsh.Height);

            // adjust width of the labels if required
            // this may be necessary if the translated labels require more space than English versions
            // the longest label is likely to be lebel3 (Private key file), so use it as a guide
            var widestLabelMinSize = new Size(label3.Width, 0);
            label1.MinimumSize = widestLabelMinSize;        // Name
            label2.MinimumSize = widestLabelMinSize;        // Url
            labelPushUrl.MinimumSize = widestLabelMinSize;  // Push URL

            if (Module == null)
            {
                return;
            }
            _remoteManager = new GitRemoteManager(Module);
            // load the data for the very first time
            Initialize(PreselectRemoteOnLoad);
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        }

        private void btnToggleState_Click(object sender, EventArgs e)
        {
            if (_selectedRemote == null)
            {
                btnToggleState.Visible = false;
                return;
            }
            _selectedRemote.Disabled = !_selectedRemote.Disabled;
            _remoteManager.ToggleRemoteState(_selectedRemote.Name, _selectedRemote.Disabled);
            BindBtnToggleState(_selectedRemote.Disabled);
            BindRemotes(_selectedRemote.Name);
        }

        private void SaveClick(object sender, EventArgs e)
        {
            var output = "";

            if ((string.IsNullOrEmpty(comboBoxPushUrl.Text) && checkBoxSepPushUrl.Checked) ||
                (comboBoxPushUrl.Text == Url.Text))
            {
                checkBoxSepPushUrl.Checked = false;
            }

            if (string.IsNullOrEmpty(_remote))
            {
                if (string.IsNullOrEmpty(RemoteName.Text) && string.IsNullOrEmpty(Url.Text))
                {
                    return;
                }

                output = Module.AddRemote(RemoteName.Text, Url.Text);
                if (output.IsNullOrWhiteSpace() && OnRemoteRenamedOrAdded != null)
                {
                    OnRemoteRenamedOrAdded(RemoteName.Text, RemoteName.Text);
                }


                if (checkBoxSepPushUrl.Checked)
                {
                    Module.SetPathSetting(string.Format(SettingKeyString.RemotePushUrl, RemoteName.Text), comboBoxPushUrl.Text);
=======
                // update all other remote properties
                var result = _remoteManager.SaveRemote(_selectedRemote,
                                                             RemoteName.Text,
                                                             Url.Text,
                                                             checkBoxSepPushUrl.Checked ? comboBoxPushUrl.Text : null,
                                                             PuttySshKey.Text);
                if (OnRemoteRenamedOrAdded != null)
                {
                    OnRemoteRenamedOrAdded(
                        _selectedRemote != null? _selectedRemote.Name : RemoteName.Text, RemoteName.Text);
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
                }

                if (MessageBox.Show(this, _questionAutoPullBehaviour.Text, _questionAutoPullBehaviourCaption.Text,
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var remoteUrl = Url.Text;

                    if (!string.IsNullOrEmpty(remoteUrl))
                    {
                        FormRemoteProcess.ShowDialog(this, "remote update");
                        ConfigureRemotes();
                    }
                    else
                    {
                        MessageBox.Show(this, _warningValidRemote.Text, _warningValidRemoteCaption.Text);
                    }
                }
            }
            else
            {
                if (RemoteName.Text != _remote)
                {
                    output = Module.RenameRemote(_remote, RemoteName.Text);
                    if (output.IsNullOrWhiteSpace())
                    {
                        if (OnRemoteRenamedOrAdded != null)
                        {
                            OnRemoteRenamedOrAdded(_remote, RemoteName.Text);
                        }
                    }
                }


                Module.SetPathSetting(string.Format(SettingKeyString.RemoteUrl, RemoteName.Text), Url.Text);
                Module.SetPathSetting(string.Format("remote.{0}.puttykeyfile", RemoteName.Text), PuttySshKey.Text);
                if (checkBoxSepPushUrl.Checked)
                {
                    Module.SetPathSetting(string.Format(SettingKeyString.RemotePushUrl, RemoteName.Text), comboBoxPushUrl.Text);
                }
                else
                {
                    Module.UnsetSetting(string.Format(SettingKeyString.RemotePushUrl, RemoteName.Text));
=======
                // if the user has just created a fresh new remote
                // there may be a need to configure it
                if (result.ShouldUpdateRemote && !string.IsNullOrEmpty(Url.Text) &&
                    DialogResult.Yes == MessageBox.Show(this,
                                                        _questionAutoPullBehaviour.Text,
                                                        _questionAutoPullBehaviourCaption.Text,
                                                        MessageBoxButtons.YesNo))
                {
                    FormRemoteProcess.ShowDialog(this, "remote update");
                    _remoteManager.ConfigureRemotes(RemoteName.Text);
                    UICommands.RepoChangedNotifier.Notify();
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
                }
            }

            if (!string.IsNullOrEmpty(output))
            {
                MessageBox.Show(this, output, _hintDelete.Text);
            }

            Initialize();
        }

        private void ConfigureRemotes()
        {
            var localConfig = Module.LocalConfigFile;

            foreach (var remoteHead in Module.GetRefs(true, true))
            {
                foreach (var localHead in Module.GetRefs(true, true))
                {
                    if (!remoteHead.IsRemote ||
                        localHead.IsRemote ||
                        !string.IsNullOrEmpty(localHead.GetTrackingRemote(localConfig)) ||
                        !string.IsNullOrEmpty(localHead.GetTrackingRemote(localConfig)) ||
                        remoteHead.IsTag ||
                        localHead.IsTag ||
                        !remoteHead.Name.ToLower().Contains(localHead.Name.ToLower()) ||
                        !remoteHead.Name.ToLower().Contains(_remote.ToLower()))
                        continue;
                    localHead.TrackingRemote = RemoteName.Text;
                    localHead.MergeWith = localHead.Name;
                }
            }
        }

        private void NewClick(object sender, EventArgs e)
        {

            var output = Module.AddRemote("<new>", "");
            if (!string.IsNullOrEmpty(output))
            {
                MessageBox.Show(this, output, _hintDelete.Text);
            }
            Initialize();
=======
            Remotes.SelectedIndices.Clear();
            RemoteName.Focus();
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_remote))
            {
                return;
            }

            if (MessageBox.Show(this, _questionDeleteRemote.Text, _questionDeleteRemoteCaption.Text, MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {

                var output = Module.RemoveRemote(_remote);
=======
                var output = _remoteManager.RemoveRemote(_selectedRemote);
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
                if (!string.IsNullOrEmpty(output))
                {
                    MessageBox.Show(this, output, _hintDelete.Text);
                }
                else
                {
                    if (OnRemoteDeleted != null)
                    {
                        OnRemoteDeleted(_remote);
                    }
                }

=======
                else
                {
                    if (OnRemoteDeleted != null)
                    {
                        OnRemoteDeleted(_selectedRemote.Name);
                    }
                }

                Initialize();
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
            }

            Initialize();
        }

        private void SshBrowseClick(object sender, EventArgs e)
        {

            using (var dialog =
                new OpenFileDialog
                    {
                        Filter = _sshKeyOpenFilter.Text + "|*.ppk",
                        InitialDirectory = ".",
                        Title = _sshKeyOpenCaption.Text
                    })
=======
            using (var dialog = new OpenFileDialog
            {
                Filter = _sshKeyOpenFilter.Text + @"|*.ppk",
                InitialDirectory = ".",
                Title = _sshKeyOpenCaption.Text
            })
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    PuttySshKey.Text = dialog.FileName;
            }
        }

        private void LoadSshKeyClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PuttySshKey.Text))
                MessageBox.Show(this, _warningNoKeyEntered.Text);
            else
                GitModule.StartPageantWithKey(PuttySshKey.Text);
        }

        private void TestConnectionClick(object sender, EventArgs e)
        {
            string url = GitCommandHelpers.GetPlinkCompatibleUrl(Url.Text);

            Module.RunExternalCmdDetachedShowConsole(
                "cmd.exe",
                string.Format("/k \"\"{0}\" -T {1}\"", AppSettings.Plink, url));
        }

        private void PruneClick(object sender, EventArgs e)
        {
            FormRemoteProcess.ShowDialog(this, "remote prune " + _remote);
        }

        private void RemoteBranchesSelectionChanged(object sender, EventArgs e)
        {
            if (RemoteBranches.SelectedRows.Count != 1)
                return;

            var head = RemoteBranches.SelectedRows[0].DataBoundItem as GitRef;

            if (head == null)
                return;

            LocalBranchNameEdit.Text = head.Name;
            LocalBranchNameEdit.ReadOnly = true;

            RemoteRepositoryCombo.Items.Clear();
            RemoteRepositoryCombo.Items.Add("");
            foreach (var remote in Module.GetRemotes())
                RemoteRepositoryCombo.Items.Add(remote);

            RemoteRepositoryCombo.Text = head.TrackingRemote;

=======
            RemoteRepositoryCombo.SelectedItem = UserGitRemotes.FirstOrDefault(x => x.Name.Equals(head.TrackingRemote, StringComparison.OrdinalIgnoreCase));
            if (RemoteRepositoryCombo.SelectedItem == null)
            {
                RemoteRepositoryCombo.SelectedIndex = 0;
            }
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
            DefaultMergeWithCombo.Text = head.MergeWith;
        }

        private void DefaultMergeWithComboDropDown(object sender, EventArgs e)
        {
            if (RemoteBranches.SelectedRows.Count != 1)
                return;

            var head = RemoteBranches.SelectedRows[0].DataBoundItem as GitRef;

            if (head == null)
                return;

            DefaultMergeWithCombo.Items.Clear();
            DefaultMergeWithCombo.Items.Add("");

            var currentSelectedRemote = RemoteRepositoryCombo.Text.Trim();

            if (string.IsNullOrEmpty(head.TrackingRemote) || string.IsNullOrEmpty(currentSelectedRemote))
                return;


            var remoteUrl = Module.GetPathSetting(string.Format(SettingKeyString.RemoteUrl, currentSelectedRemote));

=======
            var remoteUrl = Module.GetSetting(string.Format(SettingKeyString.RemoteUrl, currentSelectedRemote));
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
            if (string.IsNullOrEmpty(remoteUrl))
                return;

            foreach (var remoteHead in Module.GetRefs(true, true))
            {
                if (remoteHead.IsRemote &&
                    remoteHead.Name.ToLower().Contains(currentSelectedRemote.ToLower()) /*&&
                    string.IsNullOrEmpty(remoteHead.MergeWith)*/)
                    DefaultMergeWithCombo.Items.Add(remoteHead.LocalName);
            }
        }

        private void RemoteRepositoryComboValidated(object sender, EventArgs e)
        {
            if (RemoteBranches.SelectedRows.Count != 1)
                return;

            var head = RemoteBranches.SelectedRows[0].DataBoundItem as GitRef;
            if (head == null)
                return;

            head.TrackingRemote = RemoteRepositoryCombo.Text;
        }

        private void DefaultMergeWithComboValidated(object sender, EventArgs e)
        {
            if (RemoteBranches.SelectedRows.Count != 1)
                return;

            var head = RemoteBranches.SelectedRows[0].DataBoundItem as GitRef;
            if (head == null)
                return;

            head.MergeWith = DefaultMergeWithCombo.Text;
        }

        private void SaveDefaultPushPullClick(object sender, EventArgs e)
        {
            Initialize();
        }

        private void RemotesSelectedIndexChanged(object sender, EventArgs e)
        {

            if (!(Remotes.SelectedItem is string))
=======
            Save.Enabled = RemoteName.Text.Trim().Length > 0;
        }

        private void Remotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Remotes.SelectedIndices.Count > 0 && _selectedRemote == Remotes.SelectedItems[0].Tag)
            {
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
                return;


            _remote = (string)Remotes.SelectedItem;
            RemoteName.Text = _remote;

            Url.Text = Module.GetPathSetting(string.Format(SettingKeyString.RemoteUrl, _remote));

            comboBoxPushUrl.Text = Module.GetPathSetting(string.Format(SettingKeyString.RemotePushUrl, _remote));
            if (string.IsNullOrEmpty(comboBoxPushUrl.Text))
                checkBoxSepPushUrl.Checked = false;
            else
                checkBoxSepPushUrl.Checked = true;

            PuttySshKey.Text =
                Module.GetPathSetting(string.Format("remote.{0}.puttykeyfile", RemoteName.Text));
=======
            New.Enabled = Delete.Enabled = btnToggleState.Enabled = false;
            RemoteName.Text = string.Empty;
            Url.Text = string.Empty;
            comboBoxPushUrl.Text = string.Empty;
            checkBoxSepPushUrl.Checked = false;
            PuttySshKey.Text = string.Empty;
            gbMgtPanel.Text = _gbMgtPanelHeaderNew.Text;

            if (Remotes.SelectedIndices.Count < 1)
            {
                _selectedRemote = null;
                return;
            }

            // reset all controls and disable all buttons until we have a selection
            _selectedRemote = Remotes.SelectedItems[0].Tag as GitRemote;
            if (_selectedRemote == null)
            {
                return;
            }

            New.Enabled = Delete.Enabled = btnToggleState.Enabled = true;
            RemoteName.Text = _selectedRemote.Name;
            Url.Text = _selectedRemote.Url;
            comboBoxPushUrl.Text = _selectedRemote.PushUrl;
            checkBoxSepPushUrl.Checked = !string.IsNullOrEmpty(_selectedRemote.PushUrl);
            PuttySshKey.Text = _selectedRemote.PuttySshKey;
            gbMgtPanel.Text = _gbMgtPanelHeaderEdit.Text;
            BindBtnToggleState(_selectedRemote.Disabled);
            btnToggleState.Visible = true;
            flpnlRemoteManagement.Enabled = !_selectedRemote.Disabled;
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        }

        private void UpdateBranchClick(object sender, EventArgs e)
        {
            FormRemoteProcess.ShowDialog(this, "remote update");
        }

        private void checkBoxSepPushUrl_CheckedChanged(object sender, EventArgs e)
        {
            ShowSeperatePushUrl(checkBoxSepPushUrl.Checked);
        }


        private void ShowSeperatePushUrl(bool visible)
=======
        private void Remotes_MouseUp(object sender, MouseEventArgs e)
        {
            flpnlRemoteManagement.Enabled = !_selectedRemote?.Disabled ?? true;
        }

        private void ShowSeparatePushUrl(bool visible)
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        {
            labelPushUrl.Visible = visible;
            comboBoxPushUrl.Visible = visible;
            folderBrowserButtonPushUrl.Visible = visible;

            if (!visible)
                label2.Text = _labelUrlAsFetchPush.Text;
            else
                label2.Text = _labelUrlAsFetch.Text;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}