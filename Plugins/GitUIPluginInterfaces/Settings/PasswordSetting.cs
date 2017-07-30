﻿//using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    public class PasswordSetting : ISetting
    {
        public PasswordSetting(string aName, string aDefaultValue)
            : this(aName, aName, aDefaultValue)
        {
        }

        public PasswordSetting(string aName, string aCaption, string aDefaultValue)
        {
            Name = aName;
            Caption = aCaption;
            DefaultValue = aDefaultValue;
        }

        public string Name { get; private set; }
        public string Caption { get; private set; }
        public string DefaultValue { get; set; }
        public ITextBox CustomControl { get; set; }

        public ISettingControlBinding CreateControlBinding()
        {
            return new TextBoxBinding(this, CustomControl);
        }

        private class TextBoxBinding : SettingControlBinding<PasswordSetting, ITextBox>
        {
            public TextBoxBinding(PasswordSetting aSetting, ITextBox aCustomControl)
                : base(aSetting, aCustomControl)
            { }

            public override ITextBox CreateControl()
            {
                var box = StringSetting.CreateTextBox();
                // new TextBox {PasswordChar = '\u25CF'};
                box.PasswordChar = '\u25CF';
                return box;
            }

            public override void LoadSetting(ISettingsSource settings, bool areSettingsEffective, ITextBox control)
            {
                string settingVal;
                if (areSettingsEffective)
                {
                    settingVal = Setting.ValueOrDefault(settings);
                }
                else
                {
                    settingVal = Setting[settings];
                }

                control.Text = settingVal;
            }

            public override void SaveSetting(ISettingsSource settings, bool areSettingsEffective, ITextBox control)
            {
                var controlValue = control.Text;
                if (areSettingsEffective)
                {
                    if (Setting.ValueOrDefault(settings) == controlValue)
                    {
                        return;
                    }
                }

                Setting[settings] = controlValue;
            }
        }

        public string this[ISettingsSource settings]
        {
            get
            {
                return settings.GetString(Name, null);
            }

            set
            {
                settings.SetString(Name, value);
            }
        }

        public string ValueOrDefault(ISettingsSource settings)
        {
            return this[settings] ?? DefaultValue;
        }
    }
}