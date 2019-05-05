using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GitCommands;
using System;
using System.Threading.Tasks;
using GitUIPluginInterfaces;

namespace GitUI.UserControls
{

    public interface IRepoObjectsTree : ITree, IWin32Window, IComponent, IDisposable
    {
        IGitUICommandsSource UICommandsSource { get; set; } // -> GitModuleForm
        void Reload();
        Task Reload(object caller);
        void UpdateRevision(GitModuleRevisionEventArgs e);

        TreeView TreeView { get; }

        int TabIndex { get; set; }
        Size Size { get; set; }
        DockStyle Dock { get; set; }
        Point Location { get; set; }
        Padding Margin { get; set; }
        string Name { get; set; }

        ContextMenuStrip menuBranch { get; }

        TableLayoutPanel repoTreePanel { get; }
        TableLayoutPanel branchFilterPanel { get; }
        Label lblSearchBranch { get; }
        Button btnSearch { get; }
        Button btnSettings { get; }
        ContextMenuStrip menuSettings { get; }
    }
}
