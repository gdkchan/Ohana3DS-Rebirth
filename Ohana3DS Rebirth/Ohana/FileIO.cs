using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana
{
    public class FileIO
    {
        public enum fileType
        {
            none,
            model,
            texture,
            light,
            camera,
            skeletalAnimation,
            materialAnimation,
            visibilityAnimation
        }

        /// <summary>
        ///     Imports a file of the given type.
        ///     Returns data relative to the chosen type.
        /// </summary>
        /// <param name="type">The type of the data</param>
        /// <returns></returns>
        public static Object import(fileType type)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                switch (type)
                {
                    case fileType.model:
                        openDlg.Title = "Import Model";
                        openDlg.Filter = "Binary CTR H3D|*.bch|Source Model|*.smd";
                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (openDlg.FilterIndex)
                            {
                                case 1: return BCH.load(openDlg.FileName).model;
                                case 2: return GenericFormats.SMD.import(openDlg.FileName).model;
                                default: return null;
                            }
                        }
                        break;
                    case fileType.texture:
                        openDlg.Title = "Import Texture";
                        openDlg.Filter = "Binary CTR H3D|*.bch";
                        if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).texture;
                        break;
                    case fileType.camera:
                        openDlg.Title = "Import Camera";
                        openDlg.Filter = "Binary CTR H3D|*.bch";
                        if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).camera;
                        break;
                    case fileType.skeletalAnimation:
                        openDlg.Title = "Import Skeletal Animation";
                        openDlg.Filter = "Binary CTR H3D|*.bch|Source Model|*.smd";
                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (openDlg.FilterIndex)
                            {
                                case 1: return BCH.load(openDlg.FileName).skeletalAnimation;
                                case 2: return GenericFormats.SMD.import(openDlg.FileName).skeletalAnimation;
                                default: return null;
                            }
                        }
                        break;
                    case fileType.materialAnimation:
                        openDlg.Title = "Import Material Animation";
                        openDlg.Filter = "Binary CTR H3D|*.bch";
                        if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).materialAnimation;
                        break;
                }
            }

            return null;
        }

        /// <summary>
        ///     Exports a file of a given type.
        ///     Formats available to export will depend on the type of the data.
        /// </summary>
        /// <param name="type">Type of the data to be exported</param>
        /// <param name="data">The data</param>
        /// <param name="arguments">Optional arguments to be used by the exporter</param>
        public static void export(fileType type, Object data, List<int> arguments = null)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                switch (type)
                {
                    case fileType.model:
                        saveDlg.Title = "Export Model";
                        saveDlg.Filter = "Source Model|*.smd";
                        saveDlg.Filter += "|Collada Model|*.dae";
                        saveDlg.Filter += "|CTR Model|*.cmdl";
                        if (saveDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (saveDlg.FilterIndex)
                            {
                                case 1:
                                    GenericFormats.SMD.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                                case 2:
                                    GenericFormats.DAE.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                                case 3:
                                    GenericFormats.CMDL.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                            }
                        }
                        break;
                    case fileType.texture:
                        RenderBase.OModelGroup model = (RenderBase.OModelGroup)data;
                        saveDlg.Title = "Export Texture";
                        saveDlg.FileName = "dummy";

                        if (arguments[0] > -1)
                        {
                            saveDlg.Filter = "Selected texture|*.png|All textures|*.png";

                            if (saveDlg.ShowDialog() == DialogResult.OK)
                            {
                                switch (saveDlg.FilterIndex)
                                {
                                    case 1: model.texture[arguments[0]].texture.Save(saveDlg.FileName); break;
                                    case 2:
                                        foreach (RenderBase.OTexture texture in model.texture)
                                        {
                                            texture.texture.Save(Path.Combine(Path.GetDirectoryName(saveDlg.FileName), texture.name + ".png"));
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            saveDlg.Filter = "All textures|*.png";

                            if (saveDlg.ShowDialog() == DialogResult.OK)
                            {
                                foreach (RenderBase.OTexture texture in model.texture)
                                {
                                    texture.texture.Save(Path.Combine(Path.GetDirectoryName(saveDlg.FileName), texture.name + ".png"));
                                }
                            }
                        }

                        break;
                    case fileType.skeletalAnimation:
                        saveDlg.Title = "Export Skeletal Animation";
                        saveDlg.Filter = "Source Model|*.smd";
                        if (saveDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (saveDlg.FilterIndex)
                            {
                                case 1:
                                    GenericFormats.SMD.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0], arguments[1]);
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
