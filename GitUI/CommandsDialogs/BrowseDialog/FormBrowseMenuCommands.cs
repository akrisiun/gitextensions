using GitUI.CommandsDialogs.BrowseDialog;
using GitUIPluginInterfaces;
using System;
using System.Collections.Generic;

namespace GitUI.CommandsDialogs
{
    public class FormBrowseMenuCommands : MenuCommandsBase, IDisposable
    {
        IFormBrowse _formBrowse;
        GitUICommands UICommands { get { return _formBrowse?.UICommands as GitUICommands; } }

        // must be created only once because of translation
        IEnumerable<MenuCommand> _navigateMenuCommands;

        public FormBrowseMenuCommands(IFormBrowse formBrowse)
        {
            TranslationCategoryName = "FormBrowse";
            Translate();

            _formBrowse = formBrowse;
        }

        public void Dispose() {
            _formBrowse = null;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDispose) { }

        public IEnumerable<MenuCommand> GetNavigateMenuCommands()
        {
            if (_navigateMenuCommands == null)
            {
                _navigateMenuCommands = CreateNavigateMenuCommands();
            }

            return _navigateMenuCommands;
        }

        private IEnumerable<MenuCommand> CreateNavigateMenuCommands()
        {
            var resultList = new List<MenuCommand>();

            // no additional MenuCommands that are not defined in the RevisionGrid

            return resultList;
        }

        protected override IEnumerable<MenuCommand> GetMenuCommandsForTranslation()
        {
            return GetNavigateMenuCommands();
        }
    }
}
