using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana
{
    public class RenderBase
    {
        public class OVector2
        {
            public float x;
            public float y;

            /// <summary>
            ///     Creates a new <i>2-D</i> Vector.
            /// </summary>
            /// <param name="X">The X position</param>
            /// <param name="Y">The Y position</param>
            public OVector2(float X, float Y)
            {
                x = X;
                y = Y;
            }

            /// <summary>
            ///     Creates a new <i>2-D Vector</i>.
            /// </summary>
            /// <param name="Vector">The 2-D Vector</param>
            public OVector2(OVector2 Vector)
            {
                x = Vector.x;
                y = Vector.y;
            }

            /// <summary>
            ///     Creates a new <i>2-D Vector</i>.
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
            ///     Creates a new <i>3-D Vector</i>.
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
            ///     Creates a new <i>3-D Vector</i>.
            /// </summary>
            /// <param name="Vector">The 3-D vector</param>
            public OVector3(OVector3 Vector)
            {
                x = Vector.x;
                y = Vector.y;
                z = Vector.z;
            }

            /// <summary>
            ///     Creates a new <i>3-D Vector</i>.
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
            ///     Creates a new <i>Vertex</i>.
            /// </summary>
            public OVertex()
            {
                node = new List<int>();
                weight = new List<float>();

                position = new OVector3();
                normal = new OVector3();
                texture = new OVector2();
            }

            /// <summary>
            ///     Creates a new <i>Vertex</i>.
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
            ///     Add a <i>Node</i> to the Vertex.
            ///     It may contain multiple nodes, and each node must be the Id of a Bone on the Skeleton.
            /// </summary>
            /// <param name="Node"></param>
            public void addNode(int Node)
            {
                node.Add(Node);
            }

            /// <summary>
            ///     Add <i>Weighting</i> information of the Vertex.
            /// </summary>
            /// <param name="Weight"></param>
            public void addWeight(float Weight)
            {
                weight.Add(Weight);
            }
        }

        public struct CustomVertex
        {
            public float x, y, z;
            public float nx, ny, nz;
            public uint color;
            public float u, v;
        }

        public static CustomVertex convertVertex(OVertex input)
        {
            CustomVertex vertex;

            vertex.x = input.position.x;
            vertex.y = input.position.y;
            vertex.z = input.position.z;

            vertex.nx = input.normal.x;
            vertex.ny = input.normal.y;
            vertex.nz = input.normal.z;

            vertex.u = input.texture.x;
            vertex.v = input.texture.y;

            vertex.color = input.diffuseColor;

            return vertex;
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
            public CustomVertex[] renderBuffer;
            public int textureId = 0;
            public String objName = null;

            public OModelObject()
            {
                obj = new List<OVertex>();
            }

            /// <summary>
            ///     Add a new <i>Vertex</i> to the Object.
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
            public short parentId;
            public String name = null;

            /// <summary>
            ///     Creates a new <i>Bone</i>.
            /// </summary>
            public OBone()
            {
                translation = new OVector3();
                rotation = new OVector3();
                scale = new OVector3();
            }
        }

        public enum OTextureFilter
        {
            nearestMipmapNearest,
            nearestMipmapLinear,
            linearMipmapNearest,
            linearMipmapLinear,
            nearest,
            linear
        }

        public enum OTextureWrap
        {
            repeat,
            mirroredRepeat,
            clampToEdge,
            clampToBorder
        }

        public enum OTextureProjection
        {
            uvMap,
            cameraCubeMap,
            cameraSphereMap,
            projectionMap
        }

        public enum OCombine
        {
            none,
            modulate,
            add,
            addSigned,
            interpolate,
            subtract,
            dot3Rgb,
            dot3Rgba,
            multiplyAdd,
            addMultiply
        }

        public enum OCombineSource
        {
            none,
            constant,
            primaryColor,
            fragmentPrimaryColor,
            fragmentSecondaryColor,
            previousBuffer,
            previous,
            texture0,
            texture1,
            texture2,
            texture3
        }

        public enum OCombineOperand
        {
            none,
            color,
            oneMinusColor,
            alpha,
            oneMinusAlpha,
            red,
            oneMinusRed,
            green,
            oneMinusGreen,
            blue,
            oneMinusBlue
        }

        public class OCoordinator
        {
            public OTextureFilter minFilter;
            public OTextureFilter magFilter;
            public OTextureWrap wrapU, wrapV;
            public uint minLOD;
            public float LODBias;
            public Color borderColor;
        }

        public class OTextureParameter
        {
            public String name0, name1, name2;
            public List<OCoordinator> coordinator;

            public OVector3 sourceCoordinate;
            public OTextureProjection projection;
            public uint referenceCamera;
            public float scaleU, scaleV;
            public float rotate;
            public float translateU, translateV;

            public uint constantColorIndex;
            public ushort rgbScale, alphaScale;
            public OCombine combineRgb, combineAlpha;
            public List<OCombineSource> rgbSource;
            public List<OCombineOperand> rgbOperand;
            public List<OCombineSource> alphaSource;
            public List<OCombineOperand> alphaOperand;

            public OTextureParameter()
            {
                coordinator = new List<OCoordinator>();
                rgbSource = new List<OCombineSource>();
                rgbOperand = new List<OCombineOperand>();
                alphaSource = new List<OCombineSource>();
                alphaOperand = new List<OCombineOperand>();
            }
        }

        public class OModel
        {
            public List<OModelObject> modelObject;
            public List<OBone> skeleton;
            public List<OTextureParameter> textureParameters;
            public float height;

            public OModel()
            {
                modelObject = new List<OModelObject>();
                skeleton = new List<OBone>();
                textureParameters = new List<OTextureParameter>();
            }

            /// <summary>
            ///     Adds a <i>Object</i> to the model.
            /// </summary>
            /// <param name="obj">The Object</param>
            public void addObject(OModelObject obj)
            {
                modelObject.Add(obj);
            }

            /// <summary>
            ///     Adds a <i>Bone</i> to the skeleton.
            /// </summary>
            /// <param name="bone">The Bone</param>
            public void addBone(OBone bone)
            {
                skeleton.Add(bone);
            }

            /// <summary>
            ///     Adds a <i>Texture Parameter</i> to the model
            /// </summary>
            /// <param name="param">The Parameter</param>
            public void addTextureParameter(OTextureParameter param)
            {
                textureParameters.Add(param);
            }
        }

        public class OTexture
        {
            public Bitmap texture;
            public String name;

            /// <summary>
            ///     Creates a new <i>Texture</i>.
            /// </summary>
            /// <param name="Texture">The texture, size must be a power of 2</param>
            /// <param name="Name">Texture name</param>
            public OTexture(Bitmap Texture, String Name)
            {
                texture = new Bitmap(Texture);
                name = Name;
            }
        }

        public class OMaterial
        {
            public Color emission;
            public Color ambient;
            public Color diffuse;
            public Color specular0;
            public Color specular1;
            public Color constant0;
            public Color constant1;
            public Color constant2;
            public Color constant3;
            public Color constant4;
            public Color constant5;
        }

        public static Color getMaterialColor(BinaryReader input)
        {
            byte r = (byte)input.ReadByte();
            byte g = (byte)input.ReadByte();
            byte b = (byte)input.ReadByte();
            byte a = (byte)input.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }

        public class OModelGroup
        {
            public List<OModel> model;
            public List<OTexture> texture;
            public List<OMaterial> material;
            public float minimumY, maximumY;

            public OModelGroup()
            {
                model = new List<OModel>();
                texture = new List<OTexture>();
                material = new List<OMaterial>();
            }

            /// <summary>
            ///     Adds a <i>Model</i>.
            /// </summary>
            /// <param name="mdl">The Model</param>
            public void addModel(OModel mdl)
            {
                model.Add(mdl);
            }

            /// <summary>
            ///     Adds a new <i>Texture</i>.
            /// </summary>
            /// <param name="tex">The Texture</param>
            public void addTexture(OTexture tex)
            {
                texture.Add(tex);
            }

            /// <summary>
            ///     Adds a new <i>Material</i>.
            /// </summary>
            /// <param name="mat">The Material</param>
            public void addMaterial(OMaterial mat)
            {
                material.Add(mat);
            }
        }
    }
}
