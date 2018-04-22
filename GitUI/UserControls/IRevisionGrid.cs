using GitCommands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GitUI
{

    #region Enums
    public enum RevisionGridLayout
    {
        FilledBranchesSmall = 1,
        FilledBranchesSmallWithGraph = 2,
        Small = 3,
        SmallWithGraph = 4,
        Card = 5,
        CardWithGraph = 6,
        LargeCard = 7,
        LargeCardWithGraph = 8
    }

    public enum RevisionGraphDrawStyleEnum
    {
        Normal,
        DrawNonRelativesGray,
        HighlightSelected
    }
    #endregion

    public class GitModuleRevisionEventArgs : GitModuleEventArgs
    {
        public GitModuleRevisionEventArgs(GitRevision revision, GitModule gitModule, IGitUICommandsSource uiSource)
            : base(gitModule)
        {
            Revision = revision;
            UICommandsSource = uiSource;
        }

        public GitRevision Revision { get; private set; }
        public IGitUICommandsSource UICommandsSource { get; private set; }
    }

    public interface IGitModuleControl2
    {
        bool UICommandsSourceParentSearch { get; }

        IGitUICommandsSource UICommandsSource { get; set; }
        GitUICommands UICommands { get; }

        bool IsUICommandsInitialized { get; }
        GitModule Module { get; }
    }

#pragma warning disable 3002 // CS3002

    [CLSCompliant(true)]
    public interface IRevisionGrid2 : IRevisionGrid, IGitModuleControl2, IWin32Window
    { }

    public interface IRevisionGrid
    {
        int TrySearchRevision(string initRevision);

        void RevisionsCellPainting(object sender, DataGridViewCellPaintingEventArgs e);
        float DrawRef(DrawRefArgs drawRefArgs, float offset, string name,
            Color headColor, ArrowType arrowType); // , bool dashedLine = false, bool fill = false)

        event EventHandler<IRevisionGrid> ShowFirstParentsToggled;

        bool SetAndApplyBranchFilter(string text);
        void ForceRefreshRevisions();
        void ShowFirstParent_ToolStripMenuItemClick(object sender, EventArgs e);
        void SetLimit(int limit);

        IList<GitRevision> GetSelectedRevisions();
        GitRevision GetCurrentRevision();
        GitRevision GetRevision(int aRow);
        int LastRowIndex { get; }
    }


    public struct DrawRefArgs
    {
        public Graphics Graphics;
        public Rectangle CellBounds;
        public bool IsRowSelected;
        public Font RefsFont;
    }

    public enum ArrowType
    {
        None,
        Filled,
        NotFilled
    }
}

namespace GitUI.Revision
{
    public enum Commands
    {
        ToggleRevisionGraph,
        RevisionFilter,
        ToggleAuthorDateCommitDate,
        ToggleOrderRevisionsByDate,
        ToggleShowRelativeDate,
        ToggleDrawNonRelativesGray,
        ToggleShowGitNotes,
        ToggleRevisionCardLayout,
        ShowAllBranches,
        ShowCurrentBranchOnly,

        GoToParent,
        GoToChild,
        ToggleHighlightSelectedBranch,
        NextQuickSearch,
        PrevQuickSearch,
        SelectCurrentRevision,
        GoToCommit,
        NavigateBackward,
        NavigateForward,

        ToggleShowMergeCommits,
        ShowFilteredBranches,
        ShowRemoteBranches,
        ShowFirstParent,
        SelectAsBaseToCompare,
        CompareToBase,
        ToggleLeftPanel,
    }
}

