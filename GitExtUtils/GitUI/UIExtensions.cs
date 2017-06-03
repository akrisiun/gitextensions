//using System.Windows.Forms;

using System;

namespace GitUI
{
    public static class UIExtensions
    {
        public static bool? GetNullableChecked(this object chx, bool? IsChecked) // CheckBox chx)
        {
            if (IsChecked == null)  // chx.CheckState == CheckState.Indeterminate)
                return null;
            else
                return IsChecked.Value; // chx.Checked;

        }

        // public static void SetNullableChecked(this CheckBox chx, bool? Checked)
        public static void SetNullableChecked(this object chx, bool? Checked, Action<object, bool?> setState)
        {
            if (Checked.HasValue)
                setState(chx, Checked.Value); // chx.CheckState = Checked.Value ? CheckState.Checked : CheckState.Unchecked;
            else
                setState(chx, null); //  chx.CheckState = CheckState.Indeterminate;
        }
    }
}
