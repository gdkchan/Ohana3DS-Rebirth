using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Ohana3DS_Rebirth.Ohana
{
    class TextureUtils
    {
        /// <summary>
        ///     Gets a Bitmap from a RGBA8 Texture buffer.
        /// </summary>
        /// <param name="array">The Buffer</param>
        /// <param name="width">Width of the Texture</param>
        /// <param name="height">Height of the Texture</param>
        /// <returns></returns>
        public static Bitmap getBitmap(byte[] array, int width, int height)
        {
            Bitmap img = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(array, 0, imgData.Scan0, array.Length);
            img.UnlockBits(imgData);
            return img;
        }

        /// <summary>
        ///     Gets a RGBA8 Texture Buffer from a Bitmap.
        /// </summary>
        /// <param name="img">The Bitmap</param>
        /// <returns></returns>
        public static byte[] getArray(Bitmap img)
        {
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] array = new byte[imgData.Stride * img.Height];
            Marshal.Copy(imgData.Scan0, array, 0, array.Length);
            img.UnlockBits(imgData);
            return array;
        }
    }
}
