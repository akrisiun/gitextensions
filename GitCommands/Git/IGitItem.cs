using GitUIPluginInterfaces;
using System.Collections.Generic;

namespace GitCommands
{
    public interface IGitItem2
    {
        string Guid { get; }
        string Name { get; }

        IEnumerable<IGitItem> SubItems { get; }
    }
}