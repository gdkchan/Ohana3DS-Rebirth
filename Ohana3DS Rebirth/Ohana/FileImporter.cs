using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.Ohana
{
    class FileImporter
    {
        public enum importFileType
        {
            model,
            texture,
            skeletalAnimation,
            materialAnimation
        }

        /// <summary>
        ///     Imports a file of the given type.
        ///     Returns data relative to the chosen type.
        /// </summary>
        /// <param name="type">The type of the data</param>
        /// <returns></returns>
        public static Object import(importFileType type)
        {
            OpenFileDialog openDlg = new OpenFileDialog();

            switch (type)
            {
                case importFileType.model:
                    openDlg.Title = "Import Model";
                    openDlg.Filter = "Binary CTR H3D|*.bch";
                    if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).model;
                    break;
                case importFileType.texture:
                    openDlg.Title = "Import Texture";
                    openDlg.Filter = "Binary CTR H3D|*.bch";
                    if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).texture;
                    break;
                case importFileType.skeletalAnimation:
                    openDlg.Title = "Import Skeletal Animation";
                    openDlg.Filter = "Binary CTR H3D|*.bch";
                    if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).skeletalAnimation;
                    break;
                case importFileType.materialAnimation:
                    openDlg.Title = "Import Material Animation";
                    openDlg.Filter = "Binary CTR H3D|*.bch";
                    if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).materialAnimation;
                    break;
            }

            return null;
        }
    }
}
