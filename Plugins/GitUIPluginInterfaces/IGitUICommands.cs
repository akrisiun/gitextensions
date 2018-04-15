using System;
using System.Drawing;
<<<<<<< HEAD
using GitUIPluginInterfaces.Notifications;
=======
using System.Windows.Forms;
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d

namespace GitUIPluginInterfaces
{
    [Serializable]
    public delegate void GitUIEventHandler(object sender, GitUIBaseEventArgs e);
    [Serializable]
    public delegate void GitUIPostActionEventHandler(object sender, GitUIPostActionEventArgs e);

    public interface IGitUICommandsBase
    {
        event GitUIEventHandler PostBrowse;

        event GitUIPostActionEventHandler PostCheckoutBranch;
        event GitUIPostActionEventHandler PostCheckoutRevision;
        event GitUIPostActionEventHandler PostCherryPick;
        event GitUIPostActionEventHandler PostClone;
        event GitUIPostActionEventHandler PostCommit;


        event GitUIPostActionEventHandler PostAddFiles;
        event GitUIPostActionEventHandler PostEditGitIgnore;
        event GitUIPostActionEventHandler PostFileHistory;
        event GitUIPostActionEventHandler PostFormatPatch;
        event GitUIPostActionEventHandler PostInitialize;
        event GitUIPostActionEventHandler PostPull;
        event GitUIPostActionEventHandler PostPush;

        event GitUIPostActionEventHandler PostResolveConflicts;
        event GitUIPostActionEventHandler PostSettings;
        event GitUIPostActionEventHandler PostRevertCommit;
        event GitUIPostActionEventHandler PostStash;
        event GitUIEventHandler PostRepositoryChanged;

        event GitUIEventHandler PreSubmodulesEdit;
        event GitUIEventHandler PreSyncSubmodules;
        event GitUIEventHandler PreUpdateSubmodules;
        event GitUIEventHandler PreBrowseInitialize;
        event GitUIEventHandler PreSparseWorkingCopy;

        bool StartCommandLineProcessDialog(object ownerForm, string command, string arguments);
        bool StartCommandLineProcessDialog(string command, string arguments);
        bool StartBatchFileProcessDialog(object ownerForm, string batchFile);
        bool StartBatchFileProcessDialog(string batchFile);

        void StartFileHistoryDialog(string fileName);
        bool StartPullDialog();
        bool StartPushDialog();
        bool StartRebaseDialog(string branch);
        bool StartRemotesDialog();
        bool StartResolveConflictsDialog();
        bool StartSettingsDialog();
        bool StartSettingsDialog(IGitPlugin gitPlugin);
        bool StartStashDialog();

        IGitModule GitModule { get; }
        string GitCommand(string arguments);
        string CommandLineCommand(string cmd, string arguments);
        IGitRemoteCommand CreateRemoteCommand();
        void CacheAvatar(string email);
        Icon FormIcon { get; }
        
        /// <summary>Gets notifications implementation.</summary>
        INotifications Notifications { get; }
        IBrowseRepo BrowseRepo { get; set;  }

        /// <summary>
        /// RepoChangedNotifier.Notify() should be called after each action that changess repo state
        /// </summary>
        ILockableNotifier RepoChangedNotifier { get; }
    }

    public interface IGitUICommands : IGitUICommandsBase
    {
        event GitUIPostActionEventHandler PostApplyPatch;
        event GitUIPostActionEventHandler PostArchive;
        event GitUIPostActionEventHandler PostBlame;

        event GitUIPostActionEventHandler PostCompareRevisions;
        event GitUIPostActionEventHandler PostCreateBranch;
        event GitUIPostActionEventHandler PostCreateTag;
        event GitUIPostActionEventHandler PostDeleteBranch;
        event GitUIPostActionEventHandler PostDeleteTag;
        event GitUIPostActionEventHandler PostEditGitAttributes;

        event GitUIPostActionEventHandler PostMailMap;
        event GitUIPostActionEventHandler PostMergeBranch;
        event GitUIPostActionEventHandler PostRebase;
        event GitUIPostActionEventHandler PostRename;
        event GitUIPostActionEventHandler PostRemotes;

        event GitUIPostActionEventHandler PostSvnClone;
        event GitUIPostActionEventHandler PostSvnDcommit;
        event GitUIPostActionEventHandler PostSvnFetch;
        event GitUIPostActionEventHandler PostSvnRebase;
        event GitUIPostActionEventHandler PostSubmodulesEdit;
        event GitUIPostActionEventHandler PostSyncSubmodules;
        event GitUIPostActionEventHandler PostUpdateSubmodules;
        event GitUIPostActionEventHandler PostVerifyDatabase;
        event GitUIPostActionEventHandler PostViewPatch;
        event GitUIPostActionEventHandler PostSparseWorkingCopy;

        event GitUIEventHandler PostBrowseInitialize;
        event GitUIEventHandler PostRegisterPlugin;
        event GitUIEventHandler PreAddFiles;
        event GitUIEventHandler PreApplyPatch;
        event GitUIEventHandler PreArchive;
        event GitUIEventHandler PreBlame;
        event GitUIEventHandler PreCheckoutBranch;
        event GitUIEventHandler PreCheckoutRevision;
        event GitUIEventHandler PreCherryPick;
        event GitUIEventHandler PreClone;

        event GitUIEventHandler PreBrowse;
        event GitUIEventHandler PreCommit;
        event GitUIEventHandler PreCompareRevisions;
        event GitUIEventHandler PreCreateBranch;
        event GitUIEventHandler PreCreateTag;
        event GitUIEventHandler PreDeleteBranch;
        event GitUIEventHandler PreDeleteTag;
        event GitUIEventHandler PreFileHistory;
        event GitUIEventHandler PreInitialize;
        event GitUIEventHandler PreMergeBranch;
        event GitUIEventHandler PrePull;
        event GitUIEventHandler PrePush;

        event GitUIEventHandler PreEditGitAttributes;
        event GitUIEventHandler PreEditGitIgnore;
        event GitUIEventHandler PreFormatPatch;
        event GitUIEventHandler PreMailMap;
        event GitUIEventHandler PreRebase;
        event GitUIEventHandler PreRename;
        event GitUIEventHandler PreRemotes;
        event GitUIEventHandler PreResolveConflicts;
        event GitUIEventHandler PreRevertCommit;
        event GitUIEventHandler PreSettings;
        event GitUIEventHandler PreStash;
        event GitUIEventHandler PreSvnClone;
        event GitUIEventHandler PreSvnDcommit;
        event GitUIEventHandler PreSvnFetch;
        event GitUIEventHandler PreSvnRebase;
        event GitUIEventHandler PreVerifyDatabase;
        event GitUIEventHandler PreViewPatch;

<<<<<<< HEAD
=======
        bool StartCommandLineProcessDialog(object ownerForm, string command, string arguments);
        bool StartCommandLineProcessDialog(IGitCommand cmd, IWin32Window parentForm);
        bool StartCommandLineProcessDialog(string command, string arguments);
        bool StartBatchFileProcessDialog(object ownerForm, string batchFile);
        bool StartBatchFileProcessDialog(string batchFile);
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d

        bool StartAddFilesDialog();
        bool StartApplyPatchDialog();
        bool StartArchiveDialog();
        bool StartBrowseDialog();
        bool StartCheckoutBranch();
        bool StartCheckoutRevisionDialog();
        bool StartCherryPickDialog();
        bool StartCloneDialog();
        bool StartCloneDialog(string url);
        bool StartCommitDialog();
        bool StartCompareRevisionsDialog();
        bool StartCreateBranchDialog();
        bool StartCreateTagDialog();
        bool StartDeleteBranchDialog(string branch);
        bool StartDeleteTagDialog();
        bool StartEditGitIgnoreDialog(bool localExcludes);
<<<<<<< HEAD


=======
        void StartFileHistoryDialog(string fileName);
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
        bool StartFormatPatchDialog();
        bool StartGitCommandProcessDialog(string arguments);
        bool StartInitializeDialog();
        bool StartInitializeDialog(string dir);
        bool StartMailMapDialog();
        bool StartMergeBranchDialog(string branch);
        bool StartPluginSettingsDialog();

        bool StartSvnCloneDialog();
        bool StartSvnDcommitDialog();
        bool StartSvnFetchDialog();
        bool StartSvnRebaseDialog();
        bool StartSubmodulesDialog();
        bool StartSyncSubmodulesDialog();
        bool StartUpdateSubmodulesDialog();
        bool StartVerifyDatabaseDialog();
        bool StartViewPatchDialog();
        bool StartSparseWorkingCopyDialog();
        void AddCommitTemplate(string key, Func<string> addingText);
        void RemoveCommitTemplate(string key);
    }
}