using GitCommands;
using GitUI.CommandsDialogs;
using GitUIPluginInterfaces;
using System;
using System.Collections.Generic;
// using System.Windows.Forms;

namespace GitUI
{
    public interface IFormCommit
    {
        void Show();
        IFormBrowse Caller { get; set; }
        bool DoActionOnRepo(IWin32Window owner, bool requiresValidWorkingDir, bool changesRepo, Func<bool> action);

        IFileStatusList Unstaged { get; }
        IFileStatusList Staged { get; }
        IFileStatusList CurrentFilesList { get; }
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

