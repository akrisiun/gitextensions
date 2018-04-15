//using System.Windows.Forms;

using System;
using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    public interface IControl
    {
        IntPtr Handle { get; }
    }

    public interface ITextBox: IControl
    {
        string Text { get; set; }
        char PasswordChar { get; set; }
}

    public class StringSetting: ISetting
    {
        public StringSetting(string aName, string aDefaultValue)
            : this(aName, aName, aDefaultValue)
        {
        }

        public StringSetting(string aName, string aCaption, string aDefaultValue)
        {
            Name = aName;
            Caption = aCaption;
            DefaultValue = aDefaultValue;
        }

        public string Name { get; private set; }
        public string Caption { get; private set; }
        public string DefaultValue { get; set; }
        public TextBox CustomControl { get; set; }

        public TextBox CustomControl { get; set; }

        public static Func<TextBox> CreateTextBox { get; set; }

        public ISettingControlBinding CreateControlBinding()
        {
            return new TextBoxBinding(this, CustomControl);
    }

        private class TextBoxBinding : SettingControlBinding<StringSetting, TextBox>
        {
            public TextBoxBinding(StringSetting aSetting, TextBox aCustomControl)
                : base(aSetting, aCustomControl)
            { }

            public override TextBox CreateControl()
            {
                return StringSetting.CreateTextBox();
            }

            public override void LoadSetting(ISettingsSource settings, bool areSettingsEffective, TextBox control)
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

            public override void SaveSetting(ISettingsSource settings, bool areSettingsEffective, TextBox control)
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
