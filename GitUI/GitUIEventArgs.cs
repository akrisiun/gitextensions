using System.Windows.Forms;
using GitUIPluginInterfaces;

namespace GitUI
{
    using IWin32Window = System.Windows.Forms.IWin32Window;
    public class GitUIEventArgs : GitUIBaseEventArgs
    {
        public GitUIEventArgs(IWin32Window ownerForm, IGitUICommands gitUICommands)
            : base(ownerForm, gitUICommands)
        {
        }
    }
}
