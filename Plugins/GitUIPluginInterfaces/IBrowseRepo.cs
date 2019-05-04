
using GitUIPluginInterfaces;
using System;
using System.Collections.Generic;

namespace GitUIPluginInterfaces
{
    using GitUI;

    public interface IFormBrowse
    {
        bool StartBrowseDialog(string args);
        void FormCommit_Shown(IFormCommit form);
        void DoStartCommit(string path);
        ITree Tree { get; set; }
        bool StartCommitDialog(IWin32Window owner, bool showOnlyWhenChanges);

        IGitUICommands UICommands { get; } // -> GitModuleForm

        //IFormBrowse.
        //IRevisionGrid RevisionsGrid { get; }
        //Stack<string> LastSelectedNodes { get; }
        //void RevisionGrid_ContextMenuOpening(CancelEventArgs e);
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

        IEnumerable<IGitItem> Revisions();
        int LastRowIndex { get; }

        // GitRevision
        IGitItem GetCurrentRevision();
        IGitItem GetRevision(int aRow);
        IList<IGitItem> GetSelectedRevisions();
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
        bool DoActionOnRepo(GitUIPluginInterfaces.IWin32Window owner, bool requiresValidWorkingDir, bool changesRepo, Func<bool> action);
    }
}
