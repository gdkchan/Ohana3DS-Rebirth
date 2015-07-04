//A bunch of functions to help drawing of the other controls
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Ohana3DS_Rebirth.GUI
{
    class DrawingHelper
    {
        /// <summary>
        ///     Clamp the text to fit on a limited space.
        ///     Should be only used with single line Strings.
        /// </summary>
        /// <param name="text">The string with the text to be clamped</param>
        /// <param name="font">The font that will be used to render the text</param>
        /// <param name="maxWidth">The maximum space the text can use</param>
        /// <returns></returns>
        public static String clampText(String text, Font font, int maxWidth)
        {
            String outText = text;
            int i = 1;
            while (TextRenderer.MeasureText(outText, font).Width - 1 > maxWidth)
            {
                if (text.Length - i <= 0) return null;
                while (text.Substring(text.Length - (i + 1), 1) == " ")
                {
                    i++;
                    if (i > text.Length - 1) return null;
                }
                outText = text.Substring(0, text.Length - i) + "...";
                i++;
            }

            return outText;
        }
    }
}
