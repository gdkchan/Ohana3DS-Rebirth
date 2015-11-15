using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats.GenericFormats
{
    public class DAE
    {
        [XmlRootAttribute("COLLADA", Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
        public class COLLADA
        {
            [XmlAttribute]
            public string version = "1.4.1";

            public daeAsset asset = new daeAsset();

            [XmlArrayItem("image")]
            public List<daeImage> library_images = new List<daeImage>();

            [XmlArrayItem("material")]
            public List<daeMaterial> library_materials = new List<daeMaterial>();

            [XmlArrayItem("effect")]
            public List<daeEffect> library_effects = new List<daeEffect>();

            [XmlArrayItem("geometry")]
            public List<daeGeometry> library_geometries = new List<daeGeometry>();

            [XmlArrayItem("visual_scene")]
            public List<daeVisualScene> library_visual_scenes = new List<daeVisualScene>();

            [XmlArrayItem("instance_visual_scene")]
            public List<daeInstaceVisualScene> scene = new List<daeInstaceVisualScene>();
        }

        public class daeAsset
        {
            public string created;
            public string modified;
            public daeContributor contributor = new daeContributor();
        }

        public class daeContributor
        {
            public string author;
            public string authoring_tool;
        }

        public class daeImage
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            public string init_from;
        }

        public class daeInstanceEffect
        {
            [XmlAttribute]
            public string url;
        }

        public class daeMaterial
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            public daeInstanceEffect instance_effect = new daeInstanceEffect();
        }

        public class daeParamSurfaceElement
        {
            [XmlAttribute]
            public string type;

            public string init_from;
            public string format;
        }

        public class daeParamSampler2DElement
        {
            public string source;
            public string wrap_s;
            public string wrap_t;
            public string minfilter;
            public string magfilter;
            public string mipfilter;
        }

        public class daeParam
        {
            [XmlAttribute]
            public string sid;

            [XmlElement(IsNullable = false)]
            public daeParamSurfaceElement surface;

            [XmlElement(IsNullable = false)]
            public daeParamSampler2DElement sampler2D;
        }

        public class daePhongDiffuseTexture
        {
            [XmlAttribute]
            public string texture;

            [XmlAttribute]
            public string texcoord;
        }

        public class daePhongDiffuse
        {
            public daePhongDiffuseTexture texture = new daePhongDiffuseTexture();
        }

        public class daeColor
        {
            public string color;

            public void set(Color col)
            {
                color = String.Format("{0} {1} {2} {3}", 
                    getString(col.R / 255f), 
                    getString(col.G / 255f), 
                    getString(col.B / 255f), 
                    getString(col.A / 255f));
            }

            private string getString(float value)
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public class daePhong
        {
            public daeColor emission = new daeColor();
            public daeColor ambient = new daeColor();
            public daePhongDiffuse diffuse = new daePhongDiffuse();
            public daeColor specular = new daeColor();
        }

        public class daeTechnique
        {
            public daePhong phong = new daePhong();
        }

        public class daeProfile
        {
            [XmlAttribute]
            public string sid;

            [XmlElement("newparam")]
            public List<daeParam> newparam = new List<daeParam>();
            public daeTechnique technique = new daeTechnique();
        }

        public class daeEffect
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            public daeProfile profile_COMMON = new daeProfile();
        }

        public class daeFloatArray
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public uint count;

            [XmlText]
            public string data;

            public void set(List<float> content)
            {
                StringBuilder strArray = new StringBuilder();
                for (int i = 0; i < content.Count; i++)
                {
                    if (i < content.Count - 1)
                        strArray.Append(content[i].ToString(CultureInfo.InvariantCulture) + " ");
                    else
                        strArray.Append(content[i].ToString(CultureInfo.InvariantCulture));
                }
                count = (uint)content.Count;
                data = strArray.ToString();
            }

            public List<float> get()
            {
                List<float> output = new List<float>();
                string[] values = data.Split(Convert.ToChar(" "));
                for (int i = 0; i < values.Length; i++) output.Add(float.Parse(values[i]));
                return output;
            }
        }

        public class daeAccessorParam
        {
            [XmlAttribute]
            public string name;

            [XmlAttribute]
            public string type;
        }

        public class daeAccessor
        {
            [XmlAttribute]
            public string source;

            [XmlAttribute]
            public uint count;

            [XmlAttribute]
            public uint stride;

            [XmlElement("param")]
            public List<daeAccessorParam> param = new List<daeAccessorParam>();

            public void addParam(string name, string type)
            {
                daeAccessorParam prm = new daeAccessorParam();

                prm.name = name;
                prm.type = type;

                param.Add(prm);
            }
        }

        public class daeMeshTechnique
        {
            public daeAccessor accessor = new daeAccessor();
        }

        public class daeSource
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            public daeFloatArray float_array = new daeFloatArray();
            public daeMeshTechnique technique_common = new daeMeshTechnique();
        }

        public class daeVerticesInput
        {
            [XmlAttribute]
            public string semantic;

            [XmlAttribute]
            public string source;
        }

        public class daeVertices
        {
            [XmlAttribute]
            public string id;

            [XmlElement("input")]
            public List<daeVerticesInput> input = new List<daeVerticesInput>();

            public void addInput(string semantic, string source)
            {
                daeVerticesInput i = new daeVerticesInput();

                i.semantic = semantic;
                i.source = source;

                input.Add(i);
            }
        }

        public class daeTrianglesInput
        {
            [XmlAttribute]
            public string semantic;

            [XmlAttribute]
            public string source;

            [XmlAttribute]
            public uint offset;
        }

        public class daeTriangles
        {
            [XmlAttribute]
            public string material;

            [XmlAttribute]
            public uint count;

            [XmlElement("input")]
            public List<daeTrianglesInput> input = new List<daeTrianglesInput>();

            public string p;

            public void addInput(string semantic, string source, uint offset = 0)
            {
                daeTrianglesInput i = new daeTrianglesInput();

                i.semantic = semantic;
                i.source = source;
                i.offset = offset;

                input.Add(i);
            }

            public void set(List<uint> indices)
            {
                StringBuilder strArray = new StringBuilder();
                for (int i = 0; i < indices.Count; i++)
                {
                    if (i < indices.Count - 1)
                        strArray.Append(indices[i].ToString(CultureInfo.InvariantCulture) + " ");
                    else
                        strArray.Append(indices[i].ToString(CultureInfo.InvariantCulture));
                }
                count = (uint)(indices.Count / 3);
                p = strArray.ToString();
            }

            public List<uint> get()
            {
                List<uint> output = new List<uint>();
                string[] values = p.Split(Convert.ToChar(" "));
                for (int i = 0; i < values.Length; i++) output.Add(uint.Parse(values[i]));
                return output;
            }
        }

        public class daeMesh
        {
            [XmlElement("source")]
            public List<daeSource> source = new List<daeSource>();

            public daeVertices vertices = new daeVertices();
            public daeTriangles triangles = new daeTriangles();
        }

        public class daeGeometry
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            public daeMesh mesh = new daeMesh();
        }

        public class daeMatrix
        {
            [XmlText]
            public string data;

            public void set(RenderBase.OMatrix mtx)
            {
                StringBuilder strArray = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i == 3 && j == 3)
                            strArray.Append(mtx[i, j].ToString(CultureInfo.InvariantCulture));
                        else
                            strArray.Append(mtx[i, j].ToString(CultureInfo.InvariantCulture) + " ");
                    }

                }
                data = strArray.ToString();
            }

            public RenderBase.OMatrix get()
            {
                RenderBase.OMatrix output = new RenderBase.OMatrix();
                string[] values = data.Split(Convert.ToChar(" "));
                int k = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        output[i, j] = float.Parse(values[k++]);
                    }

                }
                return output;
            }
        }

        public class daeBindMaterialInstace
        {
            [XmlAttribute]
            public string symbol;

            [XmlAttribute]
            public string target;
        }

        public class daeBindMaterial
        {
            public daeBindMaterialInstace instance_material = new daeBindMaterialInstace();
        }

        public class daeBindMaterialTechnique
        {
            public daeBindMaterial technique_common = new daeBindMaterial();
        }

        public class daeInstanceGeometry
        {
            [XmlAttribute]
            public string url;

            public daeBindMaterialTechnique bind_material = new daeBindMaterialTechnique();
        }

        public class daeNode
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            [XmlAttribute]
            public string type = "NODE";

            public daeMatrix matrix = new daeMatrix();
            public daeInstanceGeometry instance_geometry = new daeInstanceGeometry();
        }

        public class daeVisualScene
        {
            [XmlAttribute]
            public string id;

            [XmlAttribute]
            public string name;

            [XmlElement("node")]
            public List<daeNode> node = new List<daeNode>();
        }

        public class daeInstaceVisualScene
        {
            [XmlAttribute]
            public string url;
        }

        /// <summary>
        ///     Exports a Model to the Collada format.
        ///     See: https://www.khronos.org/files/collada_spec_1_4.pdf for more information.
        /// </summary>
        /// <param name="model">The Model that will be exported</param>
        /// <param name="fileName">The output File Name</param>
        /// <param name="modelIndex">Index of the model to be exported</param>
        /// <param name="skeletalAnimationIndex">(Optional) Index of the skeletal animation</param>
        public static void export(RenderBase.OModelGroup model, string fileName, int modelIndex, int skeletalAnimationIndex = -1)
        {
            RenderBase.OModel mdl = model.model[modelIndex];
            COLLADA dae = new COLLADA();

            dae.asset.created = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ");
            dae.asset.modified = dae.asset.created;
            dae.asset.contributor.authoring_tool = "Ohana3DS";
            dae.asset.contributor.author = Environment.UserName;

            foreach (RenderBase.OTexture tex in model.texture)
            {
                daeImage img = new daeImage();
                img.id = tex.name + "_id";
                img.name = tex.name;
                img.init_from = tex.name + ".png";

                dae.library_images.Add(img);
            }

            foreach (RenderBase.OMaterial mat in mdl.material)
            {
                daeMaterial mtl = new daeMaterial();
                mtl.id = mat.name + "_id";
                mtl.name = mat.name;
                mtl.instance_effect.url = "#eff_" + mat.name;

                dae.library_materials.Add(mtl);

                daeEffect eff = new daeEffect();
                eff.id = "eff_" + mat.name + "_id";
                eff.name = "eff_" + mat.name;

                daeParam surface = new daeParam();
                surface.surface = new daeParamSurfaceElement();
                surface.sid = "img_surface";
                surface.surface.type = "2D";
                surface.surface.init_from = mat.name0;
                surface.surface.format = "PNG";
                eff.profile_COMMON.newparam.Add(surface);

                daeParam sampler = new daeParam();
                sampler.sampler2D = new daeParamSampler2DElement();
                sampler.sid = "img_sampler";
                sampler.sampler2D.source = "img_surface";

                switch (mat.textureMapper[0].wrapU)
                {
                    case RenderBase.OTextureWrap.repeat: sampler.sampler2D.wrap_s = "WRAP"; break;
                    case RenderBase.OTextureWrap.mirroredRepeat: sampler.sampler2D.wrap_s = "MIRROR"; break;
                    case RenderBase.OTextureWrap.clampToEdge: sampler.sampler2D.wrap_s = "CLAMP"; break;
                    case RenderBase.OTextureWrap.clampToBorder: sampler.sampler2D.wrap_s = "BORDER"; break;
                    default: sampler.sampler2D.wrap_s = "NONE"; break;
                }

                switch (mat.textureMapper[0].wrapV)
                {
                    case RenderBase.OTextureWrap.repeat: sampler.sampler2D.wrap_t = "WRAP"; break;
                    case RenderBase.OTextureWrap.mirroredRepeat: sampler.sampler2D.wrap_t = "MIRROR"; break;
                    case RenderBase.OTextureWrap.clampToEdge: sampler.sampler2D.wrap_t = "CLAMP"; break;
                    case RenderBase.OTextureWrap.clampToBorder: sampler.sampler2D.wrap_t = "BORDER"; break;
                    default: sampler.sampler2D.wrap_t = "NONE"; break;
                }

                switch (mat.textureMapper[0].minFilter)
                {
                    case RenderBase.OTextureMinFilter.linearMipmapLinear: sampler.sampler2D.minfilter = "LINEAR_MIPMAP_LINEAR"; break;
                    case RenderBase.OTextureMinFilter.linearMipmapNearest: sampler.sampler2D.minfilter = "LINEAR_MIPMAP_NEAREST"; break;
                    case RenderBase.OTextureMinFilter.nearestMipmapLinear: sampler.sampler2D.minfilter = "NEAREST_MIPMAP_LINEAR"; break;
                    case RenderBase.OTextureMinFilter.nearestMipmapNearest: sampler.sampler2D.minfilter = "NEAREST_MIPMAP_NEAREST"; break;
                    default: sampler.sampler2D.minfilter = "NONE"; break;
                }

                switch (mat.textureMapper[0].magFilter)
                {
                    case RenderBase.OTextureMagFilter.linear: sampler.sampler2D.magfilter = "LINEAR"; break;
                    case RenderBase.OTextureMagFilter.nearest: sampler.sampler2D.magfilter = "NEAREST"; break;
                    default: sampler.sampler2D.magfilter = "NONE"; break;
                }

                sampler.sampler2D.mipfilter = sampler.sampler2D.magfilter;

                eff.profile_COMMON.newparam.Add(sampler);

                eff.profile_COMMON.technique.phong.emission.set(Color.Black);
                eff.profile_COMMON.technique.phong.ambient.set(Color.Black);
                eff.profile_COMMON.technique.phong.specular.set(Color.White);
                eff.profile_COMMON.technique.phong.diffuse.texture.texture = "img_sampler";

                dae.library_effects.Add(eff);
            }

            int meshIndex = 0;
            daeVisualScene vs = new daeVisualScene();
            vs.name = mdl.name;
            vs.id = vs.name + "_id";
            foreach (RenderBase.OModelObject obj in mdl.modelObject)
            {
                daeGeometry geometry = new daeGeometry();

                string meshName = "mesh_" + meshIndex++ + "_" + obj.name;
                geometry.id = meshName + "_id";
                geometry.name = meshName;

                MeshUtils.optimizedMesh mesh = MeshUtils.optimizeMesh(obj);
                List<float> positions = new List<float>();
                foreach (RenderBase.OVertex vtx in mesh.vertices)
                {
                    positions.Add(vtx.position.x);
                    positions.Add(vtx.position.y);
                    positions.Add(vtx.position.z);
                }

                daeSource position = new daeSource();
                position.name = meshName + "_position";
                position.id = position.name + "_id";
                position.float_array.id = meshName + "_position_array_id";
                position.float_array.set(positions);
                position.technique_common.accessor.source = "#" + position.float_array.id;
                position.technique_common.accessor.count = (uint)mesh.vertices.Count;
                position.technique_common.accessor.stride = 3;
                position.technique_common.accessor.addParam("X", "float");
                position.technique_common.accessor.addParam("Y", "float");
                position.technique_common.accessor.addParam("Z", "float");

                geometry.mesh.source.Add(position);
                geometry.mesh.vertices.id = meshName + "_vertices_id";
                geometry.mesh.vertices.addInput("POSITION", "#" + position.id);

                geometry.mesh.triangles.material = mdl.material[obj.materialId].name;
                geometry.mesh.triangles.addInput("VERTEX", "#" + geometry.mesh.vertices.id);
                geometry.mesh.triangles.set(mesh.indices);

                dae.library_geometries.Add(geometry);

                daeNode node = new daeNode();
                node.name = "vsn_" + meshName;
                node.id = node.name + "_id";
                node.matrix.set(new RenderBase.OMatrix());
                node.instance_geometry.url = "#" + geometry.id;
                node.instance_geometry.bind_material.technique_common.instance_material.symbol = mdl.material[obj.materialId].name;
                node.instance_geometry.bind_material.technique_common.instance_material.target = "#" + mdl.material[obj.materialId].name;

                vs.node.Add(node);
            }
            dae.library_visual_scenes.Add(vs);

            daeInstaceVisualScene scene = new daeInstaceVisualScene();
            scene.url = "#" + vs.id;
            dae.scene.Add(scene);

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://www.collada.org/2005/11/COLLADASchema");
            XmlSerializer serializer = new XmlSerializer(typeof(COLLADA));
            using (FileStream output = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(output, dae, ns);
            }
        }

        /// <summary>
        ///     Transforms a Float into a String that will always have "." into decimal places,
        ///     even if the region uses ",".
        /// </summary>
        /// <param name="value">The Float value</param>
        /// <returns></returns>
        private static string getString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Transforms a Skeleton from relative to absolute positions.
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the bone to convert</param>
        /// <param name="target">Target matrix to save bone transformation</param>
        private static void transformSkeleton(List<RenderBase.OBone> skeleton, int index, ref RenderBase.OMatrix target)
        {
            target *= RenderBase.OMatrix.rotateX(skeleton[index].rotation.x);
            target *= RenderBase.OMatrix.rotateY(skeleton[index].rotation.y);
            target *= RenderBase.OMatrix.rotateZ(skeleton[index].rotation.z);
            target *= RenderBase.OMatrix.translate(skeleton[index].translation);
            if (skeleton[index].parentId > -1) transformSkeleton(skeleton, skeleton[index].parentId, ref target);
        }

        /// <summary>
        ///     Writes the skeleton hierarchy to the DAE.
        /// </summary>
        /// <param name="xml">XML Writer</param>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the current bone (root bone when it's not a recursive call)</param>
        /// <param name="acc">Bone index accumulator</param>
        private static void writeSkeleton(XmlWriter xml, List<RenderBase.OBone> skeleton, int index)
        {
            xml.WriteStartElement("node"); xml.WriteAttributeString("id", skeleton[index].name); xml.WriteAttributeString("name", skeleton[index].name); xml.WriteAttributeString("sid", skeleton[index].name); xml.WriteAttributeString("type", "JOINT");
            RenderBase.OMatrix mtx = new RenderBase.OMatrix();
            mtx *= RenderBase.OMatrix.rotateX(skeleton[index].rotation.x);
            mtx *= RenderBase.OMatrix.rotateY(skeleton[index].rotation.y);
            mtx *= RenderBase.OMatrix.rotateZ(skeleton[index].rotation.z);
            mtx *= RenderBase.OMatrix.translate(skeleton[index].translation);

            xml.WriteStartElement("matrix"); xml.WriteString(
                getString(mtx.M11) + " " + getString(mtx.M21) + " " + getString(mtx.M31) + " " + getString(mtx.M41) + " " +
                getString(mtx.M12) + " " + getString(mtx.M22) + " " + getString(mtx.M32) + " " + getString(mtx.M42) + " " +
                getString(mtx.M13) + " " + getString(mtx.M23) + " " + getString(mtx.M33) + " " + getString(mtx.M43) + " " +
                getString(mtx.M14) + " " + getString(mtx.M24) + " " + getString(mtx.M34) + " " + getString(mtx.M44));
            xml.WriteEndElement();

            for (int i = 0; i < skeleton.Count; i++) if (skeleton[i].parentId == index) writeSkeleton(xml, skeleton, i);
            xml.WriteEndElement();
        }
    }
}
