using System;

namespace GitCommands.Git
{
    // sealed
    public class GitModuleEventArgs : EventArgs
    {
        public GitModuleEventArgs(GitModule gitModule)
        {
            GitModule = gitModule;
        }

        public GitModule GitModule { get; }
    }
}