
namespace GitUIPluginInterfaces
{
    public interface IFormBrowse
    {
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
