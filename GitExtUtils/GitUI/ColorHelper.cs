﻿using System.Drawing;

namespace GitUI
{
    public static class ColorHelper
    {
        public static Color GetForeColorForBackColor(Color backColor)
        {
            return IsLightColor(backColor) ? Color.Black : Color.White;
        }

        public static bool IsLightColor(this Color color)
        {
            return true; // Dark side
            // return new HslColor(color).L > 0.5;
        }

        /*
        /// <remarks>0.05 is subtle. 0.3 is quite strong.</remarks>
        public static Color MakeColorDarker(this Color color, double amount)
        {
            var hsl = new HslColor(color);
            return hsl.WithBrightness(hsl.L - amount).ToColor();
        } 
        
        public static Color GetSplitterColor()
        {
            Color c = MakeColorDarker(SystemColors.ControlLight, 0.035);
            return c;
        }
        */
        
        public static Color GetSplitterColor()
            => (Color)SystemColors.ControlLight; // TODO: MakeColorDarker += 0.035;

        public static bool IsLightTheme()
        {
            return true; // IsLightColor(SystemColors.Window);
        }

    }
}