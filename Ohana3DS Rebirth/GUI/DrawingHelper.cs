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
        ///     Draws the face of a button.
        /// </summary>
        /// <param name="g">The graphics you want to render to</param>
        /// <param name="rect">Rectangle with position/size the face will be rendered</param>
        /// <param name="color1">First color of the gradient (Outline)</param>
        /// <param name="color2">2nd color of the gradient (Outline)</param>
        /// <param name="bgColor1">First color of the gradient (Background)</param>
        /// <param name="bgColor2">2nd color of the gradient (Background)</param>
        public static void drawButtonFace(Graphics g, Rectangle rect, Color color1, Color color2, Color bgColor1, Color bgColor2)
        {
            Rectangle rect2 = new Rectangle(rect.X + 1, rect.Y, rect.Width - 2, rect.Height - 1);
            g.FillRectangle(new LinearGradientBrush(rect2, bgColor1, bgColor2, LinearGradientMode.Vertical), rect2);
            g.DrawLine(new Pen(color1), new Point(rect.X + 1, rect.Y), new Point((rect.X + rect.Width) - 2, rect.Y));
            g.DrawLine(new Pen(new LinearGradientBrush(rect, color1, color2, LinearGradientMode.Vertical)), new Point(rect.X, rect.Y + 1), new Point(rect.X, (rect.Y + rect.Height) - 2));
            g.DrawLine(new Pen(new LinearGradientBrush(rect, color1, color2, LinearGradientMode.Vertical)), new Point((rect.X + rect.Width) - 1, rect.Y + 1), new Point((rect.X + rect.Width) - 1, (rect.Y + rect.Height) - 2));
            g.DrawLine(new Pen(color2), new Point(rect.X + 1, (rect.Y + rect.Height) - 1), new Point((rect.X + rect.Width) - 2, (rect.Y + rect.Height) - 1));
        }

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
            while (TextRenderer.MeasureText(outText, font).Width > maxWidth)
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
