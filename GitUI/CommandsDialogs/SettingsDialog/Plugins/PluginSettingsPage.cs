using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GitUIPluginInterfaces;

namespace GitUI.CommandsDialogs.SettingsDialog.Plugins
{
    public partial class PluginSettingsPage : SettingsPageBase, ISettingsPage, IDisposable // : AutoLayoutSettingsPage
    {
        private IGitPlugin _gitPlugin;
        private GitPluginSettingsContainer settingsCointainer;

        // public Control GuiControl { get; protected set; }

        public PluginSettingsPage() : this(null) { }

        public PluginSettingsPage(Control ui = null)
        {
            InitializeComponent();
        }

        private void CreateSettingsControls()
        {
            //var settings = GetSettings();
            //TODO
            //foreach (var setting in settings)
            //{
            //   this.AddSetting(setting);
            //}
        }

        private void Init(IGitPlugin _gitPlugin)
        {
            this._gitPlugin = _gitPlugin;
            settingsCointainer = new GitPluginSettingsContainer(_gitPlugin.Name);
            CreateSettingsControls();
            // Translate();
        }

        // CreateSettingsPageFromPlugin(this, gitPlugin);
        public static PluginSettingsPage CreateSettingsPageFromPlugin(ISettingsPageHost aPageHost, IGitPlugin gitPlugin)
        {
            var result = SettingsPageBase.Create<PluginSettingsPage>(aPageHost);
            result.Init(gitPlugin);
            return result;

            //Debugger.Break();  // TODO

            //return null;
        }

        
        protected override bool AreEffectiveSettings { get => true; }

        protected override ISettingsSource GetCurrentSettings()
        {
            // settingsCointainer.SetSettingsSource(base.GetCurrentSettings());

            return settingsCointainer;
        }

        //public override string GetTitle()
        //{
        //    return _gitPlugin == null ? string.Empty : _gitPlugin.Description;
        //}

        private IEnumerable<ISetting> GetSettings()
        {
            if (_gitPlugin == null)
                throw new ApplicationException();

            return _gitPlugin.GetSettings();
        }

        public override 
            SettingsPageReference PageReference
        {
            get { return new SettingsPageReferenceByType(_gitPlugin.GetType()); }
        }

        //protected  // override 
        //    object // SettingsLayout 
        //           CreateSettingsLayout()
        //{
        //    labelNoSettings.Visible = !GetSettings().Any();

        //    var layout = base.CreateSettingsLayout();

        //    this.tableLayoutPanel1.Controls.Add(layout.GetControl(), 0, 1);

        //    return layout;
        //}

        //void IDisposable.Dispose()
        //{
        //    this.Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool isDispose)
        //{
        //    tableLayoutPanel1.Dispose();
        //    labelNoSettings.Dispose();
        //}

        //public void OnPageShown() { }
        //public void LoadSettings() { }
        //public void SaveSettings() { }

        //public string GetTitle() { return null; }

        /// <summary>
        /// true if the page cannot properly react to cancel or discard
        /// </summary>
        //public bool IsInstantSavePage { get; protected set; }

        //public IEnumerable<string> GetSearchKeywords() { return null; }

    }
}