
using GitUIPluginInterfaces;
using System;

namespace GitUI
{
    public interface IFormCommit
    {
        void Show();
        IFormBrowse Caller { get; set; }
        bool DoActionOnRepo(GitUIPluginInterfaces.IWin32Window owner, bool requiresValidWorkingDir, bool changesRepo, Func<bool> action);
    }
}

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
        // IRevisionGrid RevisionsGrid { get; }
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
