using System.Windows.Forms;

namespace GitUIPluginInterfaces // GitUI
{
    using IWin32Window = System.Windows.Forms.IWin32Window;

    public class GitUIEventArgs2 // : GitUIBaseEventArgs
    {
        public GitUIEventArgs2(IWin32Window ownerForm, IGitUICommands gitUICommands)
            // : base(ownerForm, gitUICommands)
        {
        }

        public bool Cancel { get; set; }
    }
}
