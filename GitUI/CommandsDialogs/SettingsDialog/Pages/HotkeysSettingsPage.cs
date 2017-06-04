namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    public partial class HotkeysSettingsPage : SettingsPageWithHeader
    {
        public HotkeysSettingsPage()
        {
            InitializeComponent();
            Text = "Hotkeys";
            Translate();
        }

#if !SKIN   // ORIGIN git
        protected override void SettingsToPage()
        {
            controlHotkeys.ReloadSettings();
        }

        protected override void PageToSettings()
        {
            controlHotkeys.SaveSettings();
        }
#endif

    }
}
