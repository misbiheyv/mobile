using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GradientFontApp
{
    public class GradientStackLayout : StackLayout
    {
        public Color StartColor
        {
            get; set;
        }

        public Color EndColor
        {
            get; set;
        }

        public GradientOrientation GradientColorOrientation
        {
            get; set;
        }

        public enum GradientOrientation
        {
            Vertical,
            Horizontal
        }

    }
}
