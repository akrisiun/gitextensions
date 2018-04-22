using GitCommands;
using GitCommands.Config;
using GitUIPluginInterfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bitbucket
{
    class Settings
    {
<<<<<<< HEAD:Plugins/Stash/Settings.cs
        private const string StashHttpRegex = 
            @"https?:\/\/([\w\.\:]+\@)?(?<url>([a-zA-Z0-9\.\-]+)):?(\d+)?\/scm\/(?<project>~?([\w\-]+))\/(?<repo>([\w\-]+)).git";
        private const string StashSshRegex =
            @"ssh:\/\/([\w\.]+\@)(?<url>([a-zA-Z0-9\.\-]+)):?(\d+)?\/(?<project>~?([\w\-]+))\/(?<repo>([\w\-]+)).git";

        public static Settings Parse(IGitModule gitModule, ISettingsSource settings)
        {
            var result = new Settings
                             {
                                 Username = StashPlugin.StashUsername.ValueOrDefault(settings),
                                 Password = StashPlugin.StashPassword.ValueOrDefault(settings),
                                 StashUrl = StashPlugin.StashBaseURL.ValueOrDefault(settings),
                                 DisableSSL = StashPlugin.StashDisableSSL.ValueOrDefault(settings)
=======
        private const string BitbucketHttpRegex =
            @"https?:\/\/([\w\.\:]+\@)?(?<url>([a-zA-Z0-9\.\-\/]+?)):?(\d+)?\/scm\/(?<project>~?([\w\-]+?))\/(?<repo>([\w\-]+)).git";
        private const string BitbucketSshRegex =
            @"ssh:\/\/([\w\.]+\@)(?<url>([a-zA-Z0-9\.\-]+)):?(\d+)?\/(?<project>~?([\w\-]+))\/(?<repo>([\w\-]+)).git";

        public static Settings Parse(IGitModule gitModule, ISettingsSource settings, BitbucketPlugin plugin)
        {
            var result = new Settings
                             {
                                 Username = plugin.BitbucketUsername.ValueOrDefault(settings),
                                 Password = plugin.BitbucketPassword.ValueOrDefault(settings),
                                 BitbucketUrl = plugin.BitbucketBaseUrl.ValueOrDefault(settings),
                                 DisableSSL = plugin.BitbucketDisableSsl.ValueOrDefault(settings)
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d:Plugins/Bitbucket/Settings.cs
                             };

            var module = ((GitModule)gitModule);

            var remotes = module.GetRemotes()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .Select(r => module.GetSetting(string.Format(SettingKeyString.RemoteUrl, r)))
                .ToArray();

            foreach (var url in remotes)
            {
                var pattern = url.Contains("http") ? BitbucketHttpRegex : BitbucketSshRegex;
                var match = Regex.Match(url, pattern);
                if (match.Success && result.BitbucketUrl.Contains(match.Groups["url"].Value))
                {
                    result.ProjectKey = match.Groups["project"].Value;
                    result.RepoSlug = match.Groups["repo"].Value;
                    return result;
                }
            }

            return null;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool DisableSSL { get; private set; }
        public string ProjectKey { get; private set; }
        public string RepoSlug { get; private set; }
        public string BitbucketUrl { get; private set; }
    }
}
