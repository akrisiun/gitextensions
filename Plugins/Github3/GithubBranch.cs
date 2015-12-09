// using Git.hub;
using GitUIPluginInterfaces.RepositoryHosts;

namespace Github3
{
    class GithubBranch : IHostedBranch
    {
        private object // Branch 
            branch;

        public GithubBranch(object // Branch 
            branch)
        {
            this.branch = branch;
        }

        public string Name {get; set; } // { get { return branch.Name; } }
        public string Sha { get { return null; }} // branch.Commit.Sha; } }
    }
}
