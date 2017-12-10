﻿using System.Collections.Generic;
using System.Linq;
using GitCommands;
using GitCommands.Config;
using GitCommands.Git;
using GitUIPluginInterfaces;

namespace GitUI.UserControls
{
    class AuthorEmailBasedRevisionHighlighting
    {
        public enum SelectionChangeAction
        {
            NoAction,
            RefreshUserInterface,
        }

        private GitRevision _currentSelectedRevision;

        public string AuthorEmailToHighlight { get; private set; }

        public SelectionChangeAction ProcessRevisionSelectionChange(GitModule currentModule, ICollection<IGitItem> selectedRevisions)
        {
            if (selectedRevisions.Count > 1)
                return SelectionChangeAction.NoAction;

            var newSelectedRevision = selectedRevisions.FirstOrDefault() as GitRevision;
            bool differentRevisionAuthorSelected =
                !AuthorEmailEqualityComparer.Instance.Equals(_currentSelectedRevision, newSelectedRevision);
 
            if (differentRevisionAuthorSelected)
            {
                AuthorEmailToHighlight = newSelectedRevision != null
                                             ? newSelectedRevision.AuthorEmail
                                             : currentModule.GetEffectiveSetting(SettingKeyString.UserEmail);
            }

            _currentSelectedRevision = newSelectedRevision;
            return differentRevisionAuthorSelected
                       ? SelectionChangeAction.RefreshUserInterface
                       : SelectionChangeAction.NoAction;
        }
    }
}
