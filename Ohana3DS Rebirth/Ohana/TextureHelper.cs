using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Ohana3DS_Rebirth.Ohana
{
    class TextureHelper
    {
        public static Bitmap getBitmap(byte[] array, int width, int height)
        {
            Bitmap img = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(array, 0, imgData.Scan0, array.Length);
            img.UnlockBits(imgData);
            return img;
        }

        public static byte[] getArray(Bitmap img, int width, int height)
        {
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] array = new byte[imgData.Stride * height];
            Marshal.Copy(imgData.Scan0, array, 0, array.Length);
            img.UnlockBits(imgData);
            return array;
        }
    }
}
