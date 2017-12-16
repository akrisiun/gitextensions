using GitUIPluginInterfaces;
using System;

namespace GitUI
{
    public class GitUICommandsChangedEventArgs2 : EventArgs
    {
        public GitUICommandsChangedEventArgs2(GitUICommands oldCommands)
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

    /// <summary>Provides <see cref="GitUICommands"/> and a change notification.</summary>
    public interface IGitUICommandsSource2
    {
        /// <summary>Raised after <see cref="UICommands"/> changes.</summary>
        event EventHandler<GitUICommandsChangedEventArgs> GitUICommandsChanged;
        /// <summary>Gets the <see cref="GitUICommands"/> value.</summary>
        GitUICommands UICommands { get; }
        IGitUICommands ICommands { get; }
    }
}
