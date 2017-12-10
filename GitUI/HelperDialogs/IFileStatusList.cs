using GitCommands;
using GitUIPluginInterfaces;
using System;
using System.Collections.Generic;

namespace GitUI
{
    public interface IFileStatusList
    {
        GitItemStatus SelectedItem { get; set; }
        string SelectedItemParent { get; }
        IEnumerable<GitItemStatus> SelectedItems { get;  }

        IList<GitItemStatus> GitItemStatuses { get; set; }
        event EventHandler SelectedIndexChanged;

        void Focus();
        void SetDiffs(IList<IGitItem> revs);
        void SetDiff(GitRevision revision);
    }
}

