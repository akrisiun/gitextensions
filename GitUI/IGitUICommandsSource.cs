using System;

namespace GitUI
{
    public class GitUICommandsChangedEventArgs : EventArgs
    {
        public GitUICommandsChangedEventArgs(GitUICommands oldCommands)
        {
            OldCommands = oldCommands;
        }

        public GitUICommands OldCommands { get; private set; }
    }

    //public class GitUICommandsSourceEventArgs2 : EventArgs
    //{
    //    public GitUICommandsSourceEventArgs2(IGitUICommandsSource gitUiCommandsSource)
    //    {
    //        GitUICommandsSource = gitUiCommandsSource;
    //    }

    //    public IGitUICommandsSource GitUICommandsSource { get; private set; }
    //}

    /// <summary>Provides <see cref="GitUICommands"/> and a change notification.</summary>
    public interface IGitUICommandsSource
    {
        /// <summary>Raised after <see cref="UICommands"/> changes.</summary> UICommandsChanged
        event EventHandler<GitUICommandsChangedEventArgs> GitUICommandsChanged;
        /// <summary>Gets the <see cref="GitUICommands"/> value.</summary>
        GitUICommands UICommands { get; }
    }
}
