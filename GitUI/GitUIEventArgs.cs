using System.Windows.Forms;
using GitUIPluginInterfaces;

namespace GitUI
{
    // using System.Windows.Forms;
    using IWin32Window = System.Windows.Forms.IWin32Window;
    // GitUI.IWin32Window
    public interface IWin32Window2 : GitUIPluginInterfaces.IWin32Window1, System.Windows.Forms.IWin32Window
    { }

    public class GitUIEventArgs : GitUIBaseEventArgs
    {
        public GitUIEventArgs(IWin32Window ownerForm, IGitUICommands gitUICommands, string arguments = null)
            : base(ownerForm, gitUICommands, arguments)
        {
        }
    }
}
