using System;
using System.ComponentModel;
//using System.Windows.Forms;


namespace GitUIPluginInterfaces
{
    //using IWin32Window = System.Windows.Forms.IWin32Window;

    public interface IWin32Window2
    {
        IntPtr Handle { get; }
    }

    public class WinForm : IWin32Window2
    {
        public static IWin32Window2 Win32(object ownerForm, IntPtr handle)
            => new WinForm { OwnerForm = ownerForm, Handle = handle };

        public object OwnerForm { get; set; }
        public IntPtr Handle { get; set; }
    }

    public abstract class GitUIBaseEventArgs : CancelEventArgs
    {
        protected GitUIBaseEventArgs(IGitUICommands gitUICommands, string arguments = null)
            : this(null, gitUICommands, arguments)
        {
        }

        protected GitUIBaseEventArgs(IWin32Window2 ownerForm, IGitUICommands gitUICommands, string arguments = null)
            : base(false)
        {
            this.OwnerForm = ownerForm;
            this.GitUICommands = gitUICommands;
            this.Arguments = arguments;
        }

        public IGitUICommands GitUICommands { get; private set; }

        public IWin32Window2 OwnerForm { get; private set; }

        public IGitModule GitModule
        {
            get
            {
                return GitUICommands.GitModule;
            }
            
        }

        public string Arguments { get; private set; }

    }

}
