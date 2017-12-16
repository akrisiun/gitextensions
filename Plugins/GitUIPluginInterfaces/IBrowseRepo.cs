﻿
using GitUIPluginInterfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    using GitUI;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    public interface IFormBrowse
    {
        bool StartBrowseDialog(string args);
        bool StartCommitDialog(object owner, bool showOnlyWhenChanges);
        // void FormCommit_Shown(IFormCommit form);
        // void DoStartCommit(string path);

        ITree Tree { get; set; }
        System.Windows.Forms.TreeView GitTreeObj { get; }


        IGitUICommands ICommands { get; } // -> GitModuleForm

        //IFormBrowse.
        IRevisionGrid RevisionGridObj { get; }
        //Stack<string> LastSelectedNodes { get; }
        //void RevisionGrid_ContextMenuOpening(CancelEventArgs e);
    }

    public interface ITree
    {
    }

    public interface IBrowseRepo
    {
        void GoToRef(string refName, bool showNoRevisionMsg);
        void SetWorkingDir(string path);
    }
}

namespace GitUI
{
    public class GitUICommandsChangedEventArgs : EventArgs
    {
        public GitUICommandsChangedEventArgs(IGitUICommands oldCommands)
        {
            OldCommands = oldCommands;
        }

        public IGitUICommands OldCommands { get; private set; }
    }

    public interface IGitModuleControl
    {
        bool UICommandsSourceParentSearch { get; }
        bool IsUICommandsInitialized { get; }

        IGitUICommandsSource UICommandsSource { get; set; }
        IGitUICommands ICommands { get; }

        IGitModule Module { get; }
        // IGitModule IGitModuleControl.Module { get => base.Module; }
    }

    public interface IRevisionGrid : IGitModuleControl, IWin32Window
    {
        int TrySearchRevision(string initRevision);

        bool SetAndApplyBranchFilter(string text);
        void ForceRefreshRevisions();
        void ShowFirstParent_ToolStripMenuItemClick(object sender, EventArgs e);
        void SetLimit(int limit);

        IEnumerable<IGitItem> GetRevisions();
        int LastRowIndex { get; }
    }

    /// <summary>Provides <see cref="GitUICommands"/> and a change notification.</summary>
    public interface IGitUICommandsSource
    {
        /// <summary>Raised after <see cref="UICommands"/> changes.</summary>
        event EventHandler<GitUICommandsChangedEventArgs> GitUICommandsChanged;
        /// <summary>Gets the <see cref="GitUICommands"/> value.</summary>

        // IGitUICommands UICommands { get; }
        IGitUICommands ICommands { get; }
    }

    public interface IFormCommit
    {
        void Show();
        IFormBrowse Caller { get; set; }
        bool DoActionOnRepo(// GitUIPluginInterfaces.
             IWin32Window owner, bool requiresValidWorkingDir, bool changesRepo, Func<bool> action);
    }
}
