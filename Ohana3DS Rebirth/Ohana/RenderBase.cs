using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ohana3DS_Rebirth.Ohana
{
    class RenderBase
    {
        public class OVector2
        {
            public float x;
            public float y;

            /// <summary>
            ///     Creates a new 2-D Vector.
            /// </summary>
            /// <param name="X">The X position</param>
            /// <param name="Y">The Y position</param>
            public OVector2(float X, float Y)
            {
                x = X;
                y = Y;
            }

            /// <summary>
            ///     Creates a new 2-D Vector.
            /// </summary>
            /// <param name="Vector">The 2-D Vector</param>
            public OVector2(OVector2 Vector)
            {
                x = Vector.x;
                y = Vector.y;
            }

            /// <summary>
            ///     Creates a new 2-D Vector.
            /// </summary>
            public OVector2()
            {
            }
        }

        public class OVector3
        {
            public float x;
            public float y;
            public float z;

            /// <summary>
            ///     Creates a new 3-D Vector.
            /// </summary>
            /// <param name="X">The X position</param>
            /// <param name="Y">The Y position</param>
            /// <param name="Z">The Z position</param>
            public OVector3(float X, float Y, float Z)
            {
                x = X;
                y = Y;
                z = Z;
            }

            /// <summary>
            ///     Creates a new 3-D Vector.
            /// </summary>
            /// <param name="Vector">The 3-D vector</param>
            public OVector3(OVector3 Vector)
            {
                x = Vector.x;
                y = Vector.y;
                z = Vector.z;
            }

            /// <summary>
            ///     Creates a new 3-D Vector.
            /// </summary>
            public OVector3()
            {
            }
        }

        public class OVertex
        {
            public OVector3 position;
            public OVector3 normal;
            public OVector2 texture;
            public List<int> node;
            public List<float> weight;
            public uint diffuseColor;

            /// <summary>
            ///     Creates a new Vertex.
            /// </summary>
            public OVertex()
            {
                node = new List<int>();
                weight = new List<float>();
            }

            /// <summary>
            ///     Creates a new Vertex.
            /// </summary>
            /// <param name="Position">The position of the Vertex on the 3-D space</param>
            /// <param name="Normal">The normal Vector (optional)</param>
            /// <param name="UV">The texture U/V coordinates (optional)</param>
            /// <param name="Color">The diffuse color (optional)</param>
            public OVertex(OVector3 Position, OVector3 Normal, OVector2 UV, uint Color)
            {
                node = new List<int>();
                weight = new List<float>();

                position = new OVector3(Position);
                normal = new OVector3(Normal);
                texture = new OVector2(UV);
                diffuseColor = Color;
            }

            /// <summary>
            ///     Add a Node to the Vertex.
            ///     It may contain multiple nodes, and each node must be the Id of a Bone on the Skeleton.
            /// </summary>
            /// <param name="Node"></param>
            public void addNode(int Node)
            {
                node.Add(Node);
            }

            /// <summary>
            ///     Add weighting information of the Vertex.
            /// </summary>
            /// <param name="Weight"></param>
            public void addWeight(float Weight)
            {
                weight.Add(Weight);
            }
        }

        public class OMatrix
        { //4x4
            public float M11 = 1; public float M12 = 0; public float M13 = 0; public float M14 = 0;
            public float M21 = 0; public float M22 = 1; public float M23 = 0; public float M24 = 0;
            public float M31 = 0; public float M32 = 0; public float M33 = 1; public float M34 = 0;
            public float M41 = 0; public float M42 = 0; public float M43 = 0; public float M44 = 1;
        }

        public class OModelObject
        {
            public List<OVertex> obj;
            public int textureId = 0;
            public String objName = null;

            public OModelObject()
            {
                obj = new List<OVertex>();
            }

            /// <summary>
            ///     Add a new Vertex to the Object.
            /// </summary>
            /// <param name="Vertex">The Vertex</param>
            public void addVertex(OVertex Vertex)
            {
                obj.Add(Vertex);
            }
        }

        public class OBone
        {
            public OVector3 translation;
            public OVector3 rotation;
            public OVector3 scale;
            public String name = null;

            /// <summary>
            ///     Creates a new bone.
            /// </summary>
            public OBone()
            {
                translation = new OVector3();
                rotation = new OVector3();
                scale = new OVector3();
            }
        }

        public class OTexture
        {
            public Bitmap texture;
            public String name;

            /// <summary>
            ///     Creates a new Texture.
            /// </summary>
            /// <param name="Texture">The texture, size must be a power of 2</param>
            /// <param name="Name">Texture name</param>
            public OTexture(Bitmap Texture, String Name)
            {
                texture = new Bitmap(Texture);
                name = Name;
            }
        }

        public class OModel
        {
            public List<OModelObject> modelObject;
            public List<OBone> skeleton;

            public OModel()
            {
                modelObject = new List<OModelObject>();
                skeleton = new List<OBone>();
            }

            /// <summary>
            ///     Adds a Object to the model.
            /// </summary>
            /// <param name="obj">The Object</param>
            public void addObject(OModelObject obj)
            {
                modelObject.Add(obj);
            }
        }

        public class OModelGroup
        {
            public List<OModel> model;
            public List<OTexture> texture;

            public OModelGroup()
            {
                model = new List<OModel>();
                texture = new List<OTexture>();
            }

            /// <summary>
            ///     Adds a Model.
            /// </summary>
            /// <param name="mdl">The Model</param>
            public void addModel(OModel mdl)
            {
                model.Add(mdl);
            }

            /// <summary>
            ///     Adds a new Texture.
            /// </summary>
            /// <param name="tex">The Texture</param>
            public void addTexture(OTexture tex)
            {
                texture.Add(tex);
            }
        }
    }
}
