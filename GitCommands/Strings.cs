﻿using System;
using System.Diagnostics;
using SmartFormat;

namespace ResourceManager
{
    /// <summary>Contains common string literals which are translated.</summary>
    public class Strings // : Translate
    {
        // public only because of FormTranslate
        public Strings()
        {
            // Translator.Translate(this, AppSettings.CurrentTranslation);
        }

        private static Lazy<Strings> _instance = new Lazy<Strings>();

        /// <summary>Lazy-initialized instance of the <see cref="Strings"/> class.</summary>
        public static Strings Instance { get { return _instance.Value; } }

        public static void Reinit()
        {
            if (_instance.IsValueCreated)
            {
                _instance = new Lazy<Strings>();
            }
        }

        public static string GetDateText()
        {
            return Instance._dateText.Text;
        }

        public static string GetAuthorText()
        {
            return Instance._authorText.Text;
        }

        public static string GetAuthorDateText()
        {
            return Instance._authorDateText.Text;
        }

        public static string GetCommitterText()
        {
            return Instance._committerText.Text;
        }

        public static string GetCommitDateText()
        {
            return Instance._commitDateText.Text;
        }

        public static string GetCommitHashText()
        {
            return Instance._commitHashText.Text;
        }

        public static string GetMessageText()
        {
            return Instance._messageText.Text;
        }

        public static string GetParentsText()
        {
            return Instance._parentsText.Text;
        }

        public static string GetChildrenText()
        {
            return Instance._childrenText.Text;
        }

        public static string GetCurrentUnstagedChanges()
        {
            return Instance._currentUnstagedChanges.Text;
        }

        public static string GetCurrentIndex()
        {
            return Instance._currentIndex.Text;
        }

        public static string GetLoadingData()
        {
            return Instance._loadingData.Text;
        }

        /// <summary>"branches" translation.</summary>
        public static readonly TranslationString branches = new TranslationString("Branches");
        /// <summary>"remotes" translation.</summary>
        public static readonly TranslationString remotes = new TranslationString("Remotes");
        /// <summary>"tags" translation.</summary>
        public static readonly TranslationString tags = new TranslationString("Tags");
        /// <summary>"stashes" translation.</summary>
        public static readonly TranslationString stashes = new TranslationString("Stashes");
        /// <summary>"submodules" translation.</summary>
        public static readonly TranslationString submodules = new TranslationString("Submodules");
        /// <summary>"favorites" translation.</summary>
        public static readonly TranslationString favorites = new TranslationString("Favorites");
        /// <summary>"'{0}' no longer exists on remote repo and can be pruned."</summary>
        public static TranslationString RemoteBranchStaleTipFormat = new TranslationString("'{0}' no longer exists on remote repo and can be pruned");
        /// <summary>"'{0}' is new and may be fetched."</summary>
        public static TranslationString RemoteBranchNewTipFormat = new TranslationString("'{0}' is new and may be fetched");
        /// <summary>"Mirrors '{0}'"</summary>
        public static TranslationString RemoteMirrorsTipFormat = new TranslationString("Mirrors '{0}'");
        /// <summary>"Fetch and Push URLs differ"</summary>
        public static TranslationString RemoteDifferingUrlsTip = new TranslationString("Fetch and Push URLs differ");


        private readonly TranslationString _dateText = new TranslationString("Date");
        private readonly TranslationString _authorText = new TranslationString("Author");
        private readonly TranslationString _authorDateText = new TranslationString("Author date");
        private readonly TranslationString _committerText = new TranslationString("Committer");
        private readonly TranslationString _commitDateText = new TranslationString("Commit date");
        private readonly TranslationString _commitHashText = new TranslationString("Commit hash");
        private readonly TranslationString _messageText = new TranslationString("Message");
        private readonly TranslationString _parentsText = new TranslationString("Parent(s)");
        private readonly TranslationString _childrenText = new TranslationString("Children");
        private readonly TranslationString _currentUnstagedChanges = new TranslationString("Current unstaged changes");
        private readonly TranslationString _currentIndex = new TranslationString("Commit index");
        private readonly TranslationString _loadingData = new TranslationString("Loading data...");

        public static string GetNSecondsAgoText(int value)
        {
            return Smart.Format(Instance._secondsAgo.Text, value, Math.Abs(value));
        }

        public static string GetNMinutesAgoText(int value)
        {
            return Smart.Format(Instance._minutesAgo.Text, value, Math.Abs(value));
        }

        public static string GetNHoursAgoText(int value)
        {
            return Smart.Format(Instance._hoursAgo.Text, value, Math.Abs(value));
        }

        public static string GetNDaysAgoText(int value)
        {
            return Smart.Format(Instance._daysAgo.Text, value, Math.Abs(value));
        }

        public static string GetNWeeksAgoText(int value)
        {
            return Smart.Format(Instance._weeksAgo.Text, value, Math.Abs(value));
        }

        public static string GetNMonthsAgoText(int value)
        {
            return Smart.Format(Instance._monthsAgo.Text, value, Math.Abs(value));
        }

        public static string GetNYearsAgoText(int value)
        {
            return Smart.Format(Instance._yearsAgo.Text, value, Math.Abs(value));
        }

        private readonly TranslationString _secondsAgo = new TranslationString("{0} {1:second|seconds} ago");
        private readonly TranslationString _minutesAgo = new TranslationString("{0} {1:minute|minutes} ago");
        private readonly TranslationString _hoursAgo = new TranslationString("{0} {1:hour|hours} ago");
        private readonly TranslationString _daysAgo = new TranslationString("{0} {1:day|days} ago");
        private readonly TranslationString _weeksAgo = new TranslationString("{0} {1:week|weeks} ago");
        private readonly TranslationString _monthsAgo = new TranslationString("{0} {1:month|months} ago");
        private readonly TranslationString _yearsAgo = new TranslationString("{0} {1:year|years} ago");

        private readonly TranslationString _uninterestingDiffOmitted = new TranslationString("Uninteresting diff hunks are omitted.");

        public static string GetUninterestingDiffOmitted()
        {
            return Instance._uninterestingDiffOmitted.Text;
        }
    }

    public class TranslationString
    {
        public TranslationString(object str)
        {
            if (str != null)
                this.Text = str as string ?? str.ToString();
            else
                this.Text = null;
        }

        /// <summary>Gets the translated text.</summary>
        public string Text { [DebuggerStepThrough] get; private set; }

        /// <summary>Returns <see cref="Text"/> value.</summary>
        public override string ToString() { return Text; }
    }

}

//namespace GitCommands.Texts
//{
//}
