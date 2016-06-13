using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResourceManager;

namespace GitUI.UserControls
{
    /// <summary>Tree-like structure for a repo's objects.</summary>
    public partial class RepoObjectsTree : GitModuleControl
    {
        List<Tree> rootNodes = new List<Tree>();
        public Tree RootTree { get { return rootNodes == null || rootNodes.Count == 0 ? null : rootNodes[0]; } }
        public bool IsEmpty { get { return RootTree == null || RootTree.Nodes.Count == 0; } }

        /// <summary>Image key for a head branch.</summary>
        static readonly string headBranchKey = Guid.NewGuid().ToString();
        private object treeLock = new object();

        public RepoObjectsTree()
        {
            InitializeComponent();

            Translate();

            RegisterContextActions();

            treeMain.ShowNodeToolTips = true;
            treeMain.HideSelection = false;
            treeMain.NodeMouseClick += OnNodeClick;
            treeMain.NodeMouseDoubleClick += OnNodeDoubleClick;
        }

        protected override void OnUICommandsSourceChanged(object sender, IGitUICommandsSource newSource)
        {
            base.OnUICommandsSourceChanged(sender, newSource);

            DragDrops();

            lock (treeLock)
            {
                AddTree(new BranchTree(new TreeNode(BranchTree.Branches), newSource));


                /* // TODO
                AddTreeSet(new TreeNode(Strings.remotes.Text)
                    {
                        ContextMenuStrip = menuRemotes,
                        ImageKey = remotesKey
                    },
                    () => Module.GetRemotesInfo().Select(remote => new RemoteNode(remote, UICommands)).ToList(),
                    OnReloadRemotes,
                    OnAddRemote
                );
                */
            }
        }

        void AddTree(Tree aTree)
        {
            aTree.TreeViewNode.SelectedImageKey = aTree.TreeViewNode.ImageKey;
            aTree.TreeViewNode.Tag = aTree;

            if (treeMain.Nodes.Count > 0)
            {
                treeMain.Nodes.Clear();
                rootNodes.Clear();
            }
            treeMain.Nodes.Add(aTree.TreeViewNode);
            rootNodes.Add(aTree);
        }

        private CancellationTokenSource _cancelledTokenSource;
        private void Cancel()
        {
            if (_cancelledTokenSource != null)
            {
                _cancelledTokenSource.Dispose();
                _cancelledTokenSource = null;
            }
        }

        /// <summary>Reloads the repo's objects tree.</summary>
        /// 
        public void DoReload()
        {
            var task = Reload();
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());
        }

        public Task Reload()
        {
            // todo: task exception handling
            Cancel();
            _cancelledTokenSource = new CancellationTokenSource();
            Task previousTask = null;

            if (rootNodes.Count == 0)
            {
                var newSource = this.UICommandsSource;
                lock (treeLock)
                {
                    AddTree(new BranchTree(new TreeNode(BranchTree.Branches), newSource));
                }
            }

            foreach (Tree rootNode in rootNodes)
            {
                Task task = rootNode.ReloadTask(_cancelledTokenSource.Token);
                if (previousTask == null)
                {
                     // task.Start(TaskScheduler.Default);
                    previousTask =  task;
                }
                else
                {
                    // previousTask.ContinueWith((t) => task.Start(Task.Factory.Scheduler));
                    // ???
                }
            }
            return previousTask;
        }

        public void ClearEmpty()
        {
            HashSet<Tree> toRemoved = new HashSet<Tree>();

            foreach (var tree in rootNodes)
            {
                if (tree.TreeViewNode.Nodes.Count == 0)
                    toRemoved.Add(tree);
            }

            foreach(var tree in toRemoved)
            {
                rootNodes.Remove(tree);
            }

            if (rootNodes.Count == 0)
                DoReload();
        }
    }
}
