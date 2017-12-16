﻿using System;
using System.Windows.Forms;
// using System.Windows.Forms;
// using GitUI;

namespace GitUIPluginInterfaces
{
    public interface ICheckBox : IControl
    {
        bool  ThreeState { get; set; }
        bool? IsChecked { get; set; }
    }

    public class BoolSetting: ISetting
    {
        public BoolSetting(string aName, bool aDefaultValue)
            : this(aName, aName, aDefaultValue) { }

        public static Func<CheckBox> CreateCheckBox { get; set; }

        public BoolSetting(string aName, string aCaption, bool aDefaultValue)
        {
            Name = aName;
            Caption = aCaption;
            DefaultValue = aDefaultValue;
        }

        public string Name { get; private set; }
        public string Caption { get; private set; }
        public bool DefaultValue { get; set; }

        public ISettingControlBinding CreateControlBinding()
        {
            return new CheckBoxBinding(this);
        }

        public bool? this[ISettingsSource settings]
        {
            get
            {
                return settings.GetBool(Name);
            }

            set
            {
                settings.SetBool(Name, value);
            }
        }

        public bool ValueOrDefault(ISettingsSource settings)
        {
            return this[settings] ?? DefaultValue;
        }


        private class CheckBoxBinding : SettingControlBinding<BoolSetting, CheckBox>
        {
            public CheckBoxBinding(BoolSetting aSetting)
                 : base(aSetting, BoolSetting.CreateCheckBox()) { }

            public override CheckBox CreateControl()
            {
                CheckBox result = BoolSetting.CreateCheckBox(); // =  new CheckBox();
                result.ThreeState = true;
                return result;
            }

            public override void LoadSetting(ISettingsSource settings, bool areSettingsEffective, CheckBox control)
            {
                bool? settingVal;
                if (areSettingsEffective)
                {
                    settingVal = Setting.ValueOrDefault(settings);
                }
                else
                {
                    settingVal = Setting[settings];
                }

                //control.SetNullableChecked(settingVal, (c, val) => 
                //    control.CheckState  = val == null ? CheckState.Indeterminate : 
                //        val ?? true ? CheckState.Checked : CheckState.Unchecked);
            }

            public override void SaveSetting(ISettingsSource settings, bool areSettingsEffective, CheckBox control)
            {
                bool? isChecked = // control.IsChecked; //
                        (control.CheckState == CheckState.Indeterminate) ? (bool?)null : (control.CheckState == CheckState.Checked);
                //Setting[settings] = UIExtensions.GetNullableChecked(control, isChecked);
            }
        }
    }
}
