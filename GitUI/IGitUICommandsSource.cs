using GitUIPluginInterfaces;
using System;

namespace GitUI
{
    /// <summary>Provides <see cref="GitUICommands"/> and a change notification.</summary>
    public interface IGitUICommandsSource
    {
        /// <summary>Raised after <see cref="UICommands"/> changes.</summary>
        /// GitUICommandsChangedEventArgs
        /// GitUICommandsSourceEventArgs
        event EventHandler<GitUICommandsSourceEventArgs> GitUICommandsChanged;

        /// <summary>Gets the <see cref="GitUICommands"/> value.</summary>
        GitUICommands UICommands { get; }
        IGitUICommands ICommands { get; }
    }

    public class GitUICommandsChangedEventArgs : EventArgs
    {
        public GitUICommandsChangedEventArgs(GitUICommands oldCommands)
        {
            OldCommands = oldCommands;
        }

        public GitUICommands OldCommands { get; private set; }
    }

    public class GitUICommandsSourceEventArgs : EventArgs
    {
        public GitUICommandsSourceEventArgs(IGitUICommandsSource gitUiCommandsSource)
        {
            GitUICommandsSource = gitUiCommandsSource;
        }

        public IGitUICommandsSource GitUICommandsSource { get; private set; }
    }

    public interface IGitUICommandsSource2 : IGitUICommandsSource { } 

    
}
