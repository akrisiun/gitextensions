using System.Reflection;

[assembly: AssemblyDescription("GitExtensions is a GUI for git")]

partial class ThisAssembly2
{
    /// <summary>Provides access to the git information for the current assembly.</summary>
    public partial class Git
    {
        /// <summary>IsDirty: true</summary>
        public const bool IsDirty = true;

        /// <summary>IsDirtyString: true</summary>
        public const string IsDirtyString = "true";

        /// <summary>Branch: master</summary>
        public const string Branch = "master";

        /// <summary>Commit: fcaf16782</summary>
        public const string Commit = "fcaf16782";

        /// <summary>Sha: fcaf1678231c86cf44deaf882d9bb945018068b3</summary>
        public const string Sha = "fcaf1678231c86cf44deaf882d9bb945018068b3";

        /// <summary>Commits on top of base version: 896</summary>
        public const string Commits = "896";

        /// <summary>Tag: v3.3.1-896-gfcaf16782</summary>
        public const string Tag = "v3.3.1-896-gfcaf16782";

        /// <summary>Base tag: v3.3.1</summary>
        public const string BaseTag = "v3.3.1";

        /// <summary>Provides access to the base version information used to determine the <see cref="SemVer" />.</summary>
        public partial class BaseVersion
        {
            /// <summary>Major: 3</summary>
            public const string Major = "3";

            /// <summary>Minor: 3</summary>
            public const string Minor = "3";

            /// <summary>Patch: 1</summary>
            public const string Patch = "1";
        }

        /// <summary>Provides access to SemVer information for the current assembly.</summary>
        public partial class SemVer
        {
            /// <summary>Major: 3</summary>
            public const string Major = "3";

            /// <summary>Minor: 3</summary>
            public const string Minor = "3";

            /// <summary>Patch: 897</summary>
            public const string Patch = "897";

            /// <summary>Label: </summary>
            public const string Label = "";

            /// <summary>Label with dash prefix: </summary>
            public const string DashLabel = "";

            /// <summary>Source: Tag</summary>
            public const string Source = "Tag";
        }
    }
}
