using GitCommands;
using GitCommands.Settings;
using GitUI.CommandsDialogs;
using GitUI.CommandsDialogs.RepoHosting;
using GitUI.CommandsDialogs.SettingsDialog;
using GitUIPluginInterfaces;
using GitUIPluginInterfaces.RepositoryHosts;
using GitUIPluginInterfaces.Notifications;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace GitUI
{
    public interface IFormCommit
    {
        void Show();
        bool DoActionOnRepo(IWin32Window owner, bool requiresValidWorkingDir, bool changesRepo, Func<bool> action);
    }

    public interface IFileStatusList
    {
        GitItemStatus SelectedItem { get; set; }
        string SelectedItemParent { get; }
        IEnumerable<GitItemStatus> SelectedItems { get;  }

        IList<GitItemStatus> GitItemStatuses { get; set; }
        event EventHandler SelectedIndexChanged;

        void Focus();
        void SetDiffs(IList<GitRevision> revs);
        void SetDiff(GitRevision revision);
    }
}

