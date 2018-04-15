
using GitUIPluginInterfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.Diagnostics;
using GitUIPluginInterfaces;

namespace GitCommands.Git
{
    [DebuggerDisplay("GitItem( {FileName} )")]
    public class GitItem : IGitItem
    {
        public GitItem(string mode, string objectType, string guid, string name)
        {
            Mode = mode;
            GitObjectType type;
            Enum.TryParse(objectType, true, out type);
            ObjectType = type;
            Guid = guid;
            FileName = Name = name;
        }



        public static List<GitItem> CreateGitItemsFromString(GitModule aModule, string tree)
        {
            var itemsStrings = tree.Split(new char[] { '\0', '\n' });

            var items = new List<GitItem>();

            foreach (var itemsString in itemsStrings)
            {
                if (itemsString.Length <= 53)
                    continue;

                var item = GitItem.CreateGitItemFromString(aModule, itemsString);

                items.Add(item);
            }

            return items;
        }

        public static IList<IGitItem> CreateIGitItemsFromString(GitModule aModule, string tree)
        {
            var items = new List<IGitItem>();

            foreach (var item in CreateGitItemsFromString(aModule, tree))
                items.Add(item);
        public string Guid { get; }
        public GitObjectType ObjectType { get; }
        public string Name { get; }
        public string FileName { get; set; }
        public string Mode { get; }
    }

    public enum GitObjectType
    {
        None = 0,
        Commit,
        Tree,
        Blob
    }
}
