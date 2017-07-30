
namespace GitUIPluginInterfaces
{
    public interface IFormBrowse
    {
        bool StartBrowseDialog(string args);
        void FormCommit_Shown();
        void DoStartCommit(string path);
        ITree Tree { get; set; }

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
