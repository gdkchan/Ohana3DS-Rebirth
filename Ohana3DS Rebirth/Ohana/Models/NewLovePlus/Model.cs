/*
 * Importer for SMES, SMAT, SBONE, TEX and such formats used by New Love+.
 * Made by gdkchan for Ohana3DS.
 */

using System.Diagnostics;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.NewLovePlus
{
    class Model
    {
        public static RenderBase.OModelGroup load(string fileName)
        {
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            string basePath = Path.GetDirectoryName(fileName);
            FileStream input = new FileStream(fileName, FileMode.Open);
            Serialization.SERI mdl = Serialization.getSERI(input);
            input.Close();

            string skeletonFileName = Path.Combine(basePath, mdl.getStringParameter("bone"));
            string meshFileName = Path.Combine(basePath, mdl.getStringParameter("smes"));
            string materialFileName = Path.Combine(basePath, mdl.getStringParameter("smat"));
            string[] textureNames = mdl.getStringArrayParameter("texi");

            if (File.Exists(meshFileName))
            {
                RenderBase.OModel model = new RenderBase.OModel();
                model.name = Path.GetFileNameWithoutExtension(meshFileName);

                if (File.Exists(skeletonFileName))
                {
                    using (FileStream data = new FileStream(skeletonFileName, FileMode.Open))
                    {
                        BinaryReader bone = new BinaryReader(data);

                        string boneMagic = IOUtils.readStringWithLength(bone, 4);
                        uint bonesCount = bone.ReadUInt32();
                        uint unknownTableOffset = bone.ReadUInt32();
                        uint parentTableOffset = bone.ReadUInt32();
                        uint boneTableOffset = bone.ReadUInt32();

                        for (int i = 0; i < bonesCount; i++)
                        {
                            RenderBase.OBone b = new RenderBase.OBone();
                            b.name = "bone_" + i;

                            data.Seek(parentTableOffset + i * 4, SeekOrigin.Begin);
                            b.parentId = (short)bone.ReadInt32();

                            data.Seek(boneTableOffset + i * 0x24, SeekOrigin.Begin);
                            b.translation = new RenderBase.OVector3(bone.ReadSingle(), bone.ReadSingle(), bone.ReadSingle());
                            b.rotation = new RenderBase.OVector3(bone.ReadSingle(), bone.ReadSingle(), bone.ReadSingle());
                            b.scale = new RenderBase.OVector3(bone.ReadSingle(), bone.ReadSingle(), bone.ReadSingle());
                            b.absoluteScale = new RenderBase.OVector3(b.scale);

                            model.skeleton.Add(b);
                        }
                    }
                }

                Mesh.load(meshFileName, model);

                if (File.Exists(materialFileName))
                {
                    using (FileStream data = new FileStream(materialFileName, FileMode.Open))
                    {
                        BinaryReader smat = new BinaryReader(data);

                        string smatMagic = IOUtils.readStringWithLength(smat, 4);
                        uint materialsCount = smat.ReadUInt32();
                        for (int mtl = 0; mtl < materialsCount; mtl++)
                        {
                            RenderBase.OMaterial material = new RenderBase.OMaterial();
                            material.name = "material_" + mtl;

                            data.Seek(8 + mtl * 4, SeekOrigin.Begin);
                            data.Seek(smat.ReadUInt32(), SeekOrigin.Begin);

                            bool hasData = true;
                            while (hasData)
                            {
                                string magic = IOUtils.readStringWithLength(smat, 4);
                                uint sectionLength = smat.ReadUInt32();
                                long startOffset = data.Position;

                                switch (magic)
                                {
                                    case "STAT": break; //Stencil Test and Alpha Test
                                    case "MATC": //Material Color
                                        material.materialColor.emission = MeshUtils.getColor(smat);
                                        material.materialColor.ambient = MeshUtils.getColor(smat);
                                        material.materialColor.diffuse = MeshUtils.getColor(smat);
                                        material.materialColor.specular0 = MeshUtils.getColor(smat);
                                        material.materialColor.specular1 = MeshUtils.getColor(smat);
                                        material.materialColor.constant0 = MeshUtils.getColor(smat);
                                        material.materialColor.constant1 = MeshUtils.getColor(smat);
                                        material.materialColor.constant2 = MeshUtils.getColor(smat);
                                        material.materialColor.constant3 = MeshUtils.getColor(smat);
                                        material.materialColor.constant4 = MeshUtils.getColor(smat);
                                        material.materialColor.constant5 = MeshUtils.getColor(smat);
                                        break;
                                    case "TEXU": //Texture Unit
                                        uint unitsCount = smat.ReadUInt32();
                                        for (int unit = 0; unit < unitsCount; unit++)
                                        {
                                            string name = Path.GetFileNameWithoutExtension(textureNames[smat.ReadUInt32()]);
                                            switch (unit)
                                            {
                                                case 0: material.name0 = name; break;
                                                case 1: material.name1 = name; break;
                                                case 2: material.name2 = name; break;
                                            }

                                            material.textureMapper[unit].borderColor = MeshUtils.getColor(smat);
                                            material.textureMapper[unit].minFilter = (RenderBase.OTextureMinFilter)smat.ReadByte();
                                            material.textureMapper[unit].magFilter = (RenderBase.OTextureMagFilter)smat.ReadByte();
                                            material.textureMapper[unit].wrapU = (RenderBase.OTextureWrap)smat.ReadByte();
                                            material.textureMapper[unit].wrapV = (RenderBase.OTextureWrap)smat.ReadByte();
                                            smat.ReadUInt32(); //0x0
                                            smat.ReadUInt32(); //0x0
                                        }
                                        break;
                                    case "COMB": break; //Combiner
                                    case "LUTS": break; //LookUp Tables
                                    default: hasData = false; break;
                                }

                                data.Seek(startOffset + sectionLength, SeekOrigin.Begin);
                            }

                            model.material.Add(material);
                        }
                    }
                }

                models.model.Add(model);
            }

            if (textureNames != null)
            {
                foreach (string textureName in textureNames)
                {
                    string fullTextureName = Path.Combine(basePath, textureName);
                    string descriptorName = fullTextureName + ".xml";

                    if (File.Exists(fullTextureName) && File.Exists(descriptorName))
                    {
                        Serialization.SERI tex = Serialization.getSERI(descriptorName);

                        int width = tex.getIntegerParameter("w");
                        int height = tex.getIntegerParameter("h");
                        int mipmap = tex.getIntegerParameter("mipmap");
                        int format = tex.getIntegerParameter("format");

                        RenderBase.OTextureFormat fmt = RenderBase.OTextureFormat.dontCare;
                        switch (format)
                        {
                            case 0: fmt = RenderBase.OTextureFormat.l4; break;
                            case 1: fmt = RenderBase.OTextureFormat.l8; break;
                            case 7: fmt = RenderBase.OTextureFormat.rgb565; break;
                            case 8: fmt = RenderBase.OTextureFormat.rgba5551; break;
                            case 9: fmt = RenderBase.OTextureFormat.rgba4; break;
                            case 0xa: fmt = RenderBase.OTextureFormat.rgba8; break;
                            case 0xb: fmt = RenderBase.OTextureFormat.rgb8; break;
                            case 0xc: fmt = RenderBase.OTextureFormat.etc1; break;
                            case 0xd: fmt = RenderBase.OTextureFormat.etc1a4; break;
                            default: Debug.WriteLine("NLP Model: Unknown Texture format 0x" + format.ToString("X8")); break;
                        }

                        string name = Path.GetFileNameWithoutExtension(textureName);
                        byte[] buffer = File.ReadAllBytes(fullTextureName);
                        models.texture.Add(new RenderBase.OTexture(TextureCodec.decode(buffer, width, height, fmt), name));
                    }
                }
            }

            return models;
        }
    }
}
