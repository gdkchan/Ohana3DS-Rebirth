//A bunch of functions to help drawing of the other controls
using System.Drawing;

namespace Ohana3DS_Rebirth.GUI
{
    class DrawingUtils
    {
        /// <summary>
        ///     Clamp the text to fit on a limited space.
        ///     Should be only used with single line Strings.
        /// </summary>
        /// <param name="g">Graphics object used to draw the text</param>
        /// <param name="text">The string with the text to be clamped</param>
        /// <param name="font">The font that will be used to render the text</param>
        /// <param name="maxWidth">The maximum space the text can use</param>
        /// <returns></returns>
        public static string clampText(Graphics g, string text, Font font, int maxWidth)
        {
            string outText = text;
            int i = 1;
            while (measureText(g, outText, font).Width > maxWidth)
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

        /// <summary>
        ///     Measures the Size of a Text.
        /// </summary>
        /// <param name="g">Graphics object used to draw the text</param>
        /// <param name="text">The text</param>
        /// <param name="font">Font used to draw the text</param>
        /// <returns></returns>
        public static SizeF measureText(Graphics g, string text, Font font)
        {
            if (text == null) return Size.Empty;
            StringFormat format = new StringFormat();
            RectangleF rect = new RectangleF(0, 0, 1000, 1000);
            CharacterRange[] ranges = { new CharacterRange(0, text.Length) };

            format.SetMeasurableCharacterRanges(ranges);
            Region[] regions = g.MeasureCharacterRanges(text, font, rect, format);
            if (regions.Length == 0) return SizeF.Empty; 
            rect = regions[0].GetBounds(g);

            return new SizeF(rect.Right + 1f, rect.Bottom + 1f);
        }
    }
}
