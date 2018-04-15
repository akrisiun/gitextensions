﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using GitCommands.Config;
using GitUIPluginInterfaces;

namespace GitCommands.Remote
{
    public interface IGitRemoteManager
    {
        void ConfigureRemotes(string remoteName);

        /// <summary>
        /// Returns the default remote for push operation.
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="branch"></param>
        /// <returns>The <see cref="GitRef.Name"/> if found, otheriwse <see langword="null"/>.</returns>
        string GetDefaultPushRemote(GitRemote remote, string branch);

        /// <summary>
        /// Loads the list of remotes configured in .git/config file.
        /// </summary>
        /// <param name="loadDisabled"></param>
        IEnumerable<GitRemote> LoadRemotes(bool loadDisabled);

        /// <summary>
        /// Removes the specified remote from .git/config file.
        /// </summary>
        /// <param name="remote">Remote to remove.</param>
        /// <returns>Output of the operation.</returns>
        string RemoveRemote(GitRemote remote);

        /// <summary>
        ///   Saves the remote details by creating a new or updating an existing remote entry in .git/config file.
        /// </summary>
        /// <param name="remote">An existing remote instance or <see langword="null"/> if creating a new entry.</param>
        /// <param name="remoteName">
        ///   <para>The remote name.</para>
        ///   <para>If updating an existing remote and the name changed, it will result in remote name change and prompt for "remote update".</para>
        /// </param>
        /// <param name="remoteUrl">
        ///   <para>The remote URL.</para>
        ///   <para>If updating an existing remote and the URL changed, it will result in remote URL change and prompt for "remote update".</para>
        /// </param>
        /// <param name="remotePushUrl">An optional alternative remote push URL.</param>
        /// <param name="remotePuttySshKey">An optional Putty SSH key.</param>
        /// <returns>Result of the operation.</returns>
        GitRemoteSaveResult SaveRemote(GitRemote remote, string remoteName, string remoteUrl, string remotePushUrl, string remotePuttySshKey);

        /// <summary>
        ///  Marks the remote as enabled or disabled in .git/config file.
        /// </summary>
        /// <param name="remoteName">The name of the remote.</param>
        /// <param name="disabled"></param>
        void ToggleRemoteState(string remoteName, bool disabled);
    }

    public class GitRemoteManager : IGitRemoteManager
    {
        internal static readonly string DisabledSectionPrefix = "-";
        internal static readonly string SectionRemote = "remote";
        private readonly IGitModule _module;


        public GitRemoteManager(IGitModule module)
        {
            _module = module;
        }


        // TODO: moved verbatim from FormRemotes.cs, perhaps needs refactoring
        [SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
        public void ConfigureRemotes(string remoteName)
        {
            //var localConfig = _module.LocalConfigFile;

            //foreach (var remoteHead in _module.GetRefs(true, true))
            //{
            //    foreach (var localHead in _module.GetRefs(true, true))
            //    {
            //        if (!remoteHead.IsRemote ||
            //            localHead.IsRemote ||
            //            !string.IsNullOrEmpty(localHead.GetTrackingRemote(localConfig)) ||
            //            remoteHead.IsTag ||
            //            localHead.IsTag ||
            //            !remoteHead.Name.ToLower().Contains(localHead.Name.ToLower()) ||
            //            !remoteHead.Name.ToLower().Contains(remoteName.ToLower()))
            //        {
            //            continue;
            //        }

            //        localHead.TrackingRemote = remoteName;
            //        localHead.MergeWith = localHead.Name;
            //    }
            //}
        }

        /// <summary>
        /// Returns the default remote for push operation.
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="branch"></param>
        /// <returns>The <see cref="GitRef.Name"/> if found, otheriwse <see langword="null"/>.</returns>
        // TODO: moved verbatim from FormPush.cs, perhaps needs refactoring
        public string GetDefaultPushRemote(GitRemote remote, string branch)
        {
            if (remote == null)
            {
                throw new ArgumentNullException("remote");
            }

            Func<string, string, bool> isSettingForBranch = (setting, branchName) =>
            {
                var head = new GitRef(_module, string.Empty, setting);
                return head.IsHead && head.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase);
            };

            var remoteHead = remote.Push
                                   .Select(s => s.Split(':'))
                                   .Where(t => t.Length == 2)
                                   .Where(t => isSettingForBranch(t[0], branch))
                                   .Select(t => new GitRef(_module, string.Empty, t[1]))
                                   .FirstOrDefault(h => h.IsHead);

            return remoteHead == null ? null : remoteHead.Name;
        }

        /// <summary>
        /// Retrieves disabled remotes from the .git/config file.
        /// </summary>
        public string[] GetDisabledRemotes()
        {
            return _module.LocalConfigFile.GetConfigSections()
                                          .Where(s => s.SectionName == $"{DisabledSectionPrefix}remote")
                                          .Select(s => s.SubSection)
                                          .ToArray();
        }

        /// <summary>
        /// Loads the list of remotes configured in .git/config file.
        /// </summary>
        // TODO: candidate for Async implementations
        public IEnumerable<GitRemote> LoadRemotes(bool loadDisabled)
        {
            var remotes = new List<GitRemote>();
            if (_module == null)
            {
                return remotes;
            }

            PopulateRemotes(remotes, true);
            if (loadDisabled)
            {
                Remotes.Clear();

                IEnumerable<GitRemote> gitRemotes = null; // _module.GetRemotes().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                if (gitRemotes.Any())
                {
                    var remotes = gitRemotes.Select(remote => new GitRemote
                    {
                        // Name = remote,
                        Url = _module.GetSetting(string.Format(SettingKeyString.RemoteUrl, remote)),
                        PushUrl = _module.GetSetting(string.Format(SettingKeyString.RemotePushUrl, remote)),
                        //Push = _module.GetSettings(string.Format(SettingKeyString.RemotePush, remote)).ToList(),
                        //PuttySshKey = _module.GetSetting(string.Format(SettingKeyString.RemotePuttySshKey, remote)),
                    }).ToList();

                    Remotes.AddAll(remotes.OrderBy(x => x.Name));
                }
                PopulateRemotes(remotes, false);
            }

            return remotes.OrderBy(x => x.Name);
        }

        /// <summary>
        /// Removes the specified remote from .git/config file.
        /// </summary>
        /// <param name="remote">Remote to remove.</param>
        /// <returns>Output of <see cref="IGitModule.RemoveRemote"/> operation, if the remote is active; otherwise <see cref="string.Empty"/>.</returns>
        public string RemoveRemote(GitRemote remote)
        {
            if (remote == null)
            {
                throw new ArgumentNullException(nameof(remote));
            }
<<<<<<< HEAD:GitUI/Objects/GitRemoteController.cs
            return null; // _module.RemoveRemote(remote.Name);
=======

            if (!remote.Disabled)
            {
                return _module.RemoveRemote(remote.Name);
            }

            var sectionName = $"{DisabledSectionPrefix}{SectionRemote}.{remote.Name}";
            _module.LocalConfigFile.RemoveConfigSection(sectionName, true);
            return string.Empty;
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d:GitCommands/Remote/GitRemoteManager.cs
        }

        /// <summary>
        ///   Saves the remote details by creating a new or updating an existing remote entry in .git/config file.
        /// </summary>
        /// <param name="remote">An existing remote instance or <see langword="null"/> if creating a new entry.</param>
        /// <param name="remoteName">
        ///   <para>The remote name.</para>
        ///   <para>If updating an existing remote and the name changed, it will result in remote name change and prompt for "remote update".</para>
        /// </param>
        /// <param name="remoteUrl">
        ///   <para>The remote URL.</para>
        ///   <para>If updating an existing remote and the URL changed, it will result in remote URL change and prompt for "remote update".</para>
        /// </param>
        /// <param name="remotePushUrl">An optional alternative remote push URL.</param>
        /// <param name="remotePuttySshKey">An optional Putty SSH key.</param>
        /// <returns>Result of the operation.</returns>
        public GitRemoteSaveResult SaveRemote(GitRemote remote, string remoteName, string remoteUrl, string remotePushUrl, string remotePuttySshKey)
        {
            if (string.IsNullOrWhiteSpace(remoteName))
            {
                throw new ArgumentNullException(nameof(remoteName));
            }

            remoteName = remoteName.Trim();

            // if create a new remote or updated the url - we may need to perform "update remote"
            bool updateRemoteRequired = false;
            // if operation return anything back, relay that to the user
            var output = string.Empty;

            bool creatingNew = remote == null;
            bool remoteDisabled = false;
            if (creatingNew)
            {
                //output = _module.AddRemote(remoteName, remoteUrl);
                updateRemoteRequired = true;
            }
            else
            {
                if (remote.Disabled)
                {
                    // disabled branches can't updated as it poses to many problems, i.e. 
                    // - verify that the branch name is valid, and 
                    // - it does not duplicate an active branch name etc.
                    return new GitRemoteSaveResult(null, false);
                }

                remoteDisabled = remote.Disabled;
                if (!string.Equals(remote.Name, remoteName, StringComparison.OrdinalIgnoreCase))
                {
                    // the name of the remote changed - perform rename
                    //output = _module.RenameRemote(remote.Name, remoteName);
                }

                if (!string.Equals(remote.Url, remoteUrl, StringComparison.OrdinalIgnoreCase))
                {
                    // the remote url changed - we may need to update remote
                    updateRemoteRequired = true;
                }
            }

<<<<<<< HEAD:GitUI/Objects/GitRemoteController.cs
            UpdateSettings(string.Format(SettingKeyString.RemoteUrl, remoteName), remoteUrl);
            UpdateSettings(string.Format(SettingKeyString.RemotePushUrl, remoteName), remotePushUrl);
            //UpdateSettings(string.Format(SettingKeyString.RemotePuttySshKey, remoteName), remotePuttySshKey);
=======
            UpdateSettings(remoteName, remoteDisabled, SettingKeyString.RemoteUrl, remoteUrl);
            UpdateSettings(remoteName, remoteDisabled, SettingKeyString.RemotePushUrl, remotePushUrl);
            UpdateSettings(remoteName, remoteDisabled, SettingKeyString.RemotePuttySshKey, remotePuttySshKey);
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d:GitCommands/Remote/GitRemoteManager.cs

            return new GitRemoteSaveResult(output, updateRemoteRequired);
        }

        /// <summary>
        ///  Marks the remote as enabled or disabled in .git/config file.
        /// </summary>
        /// <param name="remoteName">An existing remote instance.</param>
        /// <param name="disabled">The new state of the remote. <see langword="true"/> to disable the remote; otherwise <see langword="false"/>.</param>
        public void ToggleRemoteState(string remoteName, bool disabled)
        {
            if (string.IsNullOrWhiteSpace(remoteName))
            {
                throw new ArgumentNullException(nameof(remoteName));
            }

            // disabled is the new state, so if the new state is 'false' (=enabled), then the existing state is 'true' (=disabled, i.e. '-remote')
            var sectionName = (disabled ? "" : DisabledSectionPrefix) + SectionRemote;

            var sections = _module.LocalConfigFile.GetConfigSections();
            var section = sections.FirstOrDefault(s => s.SectionName == sectionName && s.SubSection == remoteName);
            if (section == null)
            {
                // we didn't find it, nothing we can do
                return;
            }

            if (disabled)
            {
                _module.RemoveRemote(remoteName);
            }
            else
            {
                _module.LocalConfigFile.RemoveConfigSection($"{sectionName}.{remoteName}");
            }

            // rename the remote
            section.SectionName = (disabled ? DisabledSectionPrefix : "") + SectionRemote;

            _module.LocalConfigFile.AddConfigSection(section);
            _module.LocalConfigFile.Save();
        }


        // pass the list in to minimise allocations
        private void PopulateRemotes(List<GitRemote> allRemotes, bool enabled)
        {
<<<<<<< HEAD:GitUI/Objects/GitRemoteController.cs
            //if (!string.IsNullOrWhiteSpace(value))
            //{
            //    _module.SetSetting(settingName, value);
            //}
            //else
            //{
            //    _module.UnsetSetting(settingName);
            //}
=======
            Func<string[]> func;
            if (enabled)
            {
                func = _module.GetRemotes;
            }
            else
            {
                func = GetDisabledRemotes;
            }


            var gitRemotes = func().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (gitRemotes.Any())
            {
                allRemotes.AddRange(gitRemotes.Select(remote => new GitRemote
                {
                    Disabled = !enabled,
                    Name = remote,
                    Url = _module.GetSetting(GetSettingKey(SettingKeyString.RemoteUrl, remote, enabled)),
                    Push = _module.GetSettings(GetSettingKey(SettingKeyString.RemotePush, remote, enabled)).ToList(),
                    PushUrl = _module.GetSetting(GetSettingKey(SettingKeyString.RemotePushUrl, remote, enabled)),
                    PuttySshKey = _module.GetSetting(GetSettingKey(SettingKeyString.RemotePuttySshKey, remote, enabled)),
                }));
            }
        }

        private string GetSettingKey(string settingKey, string remoteName, bool remoteEnabled)
        {
            var key = string.Format(settingKey, remoteName);
            return remoteEnabled ? key : DisabledSectionPrefix + key;
        }

        private void UpdateSettings(string remoteName, bool remoteDisabled, string settingName, string value)
        {
            var preffix = remoteDisabled ? DisabledSectionPrefix : string.Empty;
            var fullSettingName = preffix + string.Format(settingName, remoteName);

            if (!string.IsNullOrWhiteSpace(value))
            {
                _module.SetSetting(fullSettingName, value);
            }
            else
            {
                _module.UnsetSetting(fullSettingName);
            }
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d:GitCommands/Remote/GitRemoteManager.cs
        }
    }
}
