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

            /// <summary>
            ///     Transform the 3-D Vector with a matrix.
            /// </summary>
            /// <param name="matrix">The matrix</param>
            /// <returns></returns>
            public void transform(OMatrix matrix)
            {
                x = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + matrix.M41;
                y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + matrix.M42;
                z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + matrix.M43;
            }
        }

        public class OVector4
        {
            public float x;
            public float y;
            public float z;
            public float w;

            /// <summary>
            ///     Creates a new 4-D Vector.
            /// </summary>
            /// <param name="X">The X position</param>
            /// <param name="Y">The Y position</param>
            /// <param name="Z">The Z position</param>
            /// <param name="Z">The W position</param>
            public OVector4(float X, float Y, float Z, float W)
            {
                x = X;
                y = Y;
                z = Z;
                w = W;
            }

            /// <summary>
            ///     Creates a new 4-D Vector.
            /// </summary>
            /// <param name="Vector">The 4-D vector</param>
            public OVector4(OVector4 Vector)
            {
                x = Vector.x;
                y = Vector.y;
                z = Vector.z;
                w = Vector.w;
            }

            /// <summary>
            ///     Creates a new 4-D Vector.
            /// </summary>
            public OVector4()
            {
            }
        }

        public class OVertex
        {
            public OVector3 position;
            public OVector3 normal;
            public OVector3 tangent;
            public OVector2 texture0;
            public OVector2 texture1;
            public OVector2 texture2;
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

                position = new OVector3();
                normal = new OVector3();
                tangent = new OVector3();
                texture0 = new OVector2();
                texture1 = new OVector2();
                texture2 = new OVector2();
            }

            /// <summary>
            ///     Creates a new Vertex.
            /// </summary>
            /// <param name="Position">The position of the Vertex on the 3-D space</param>
            /// <param name="Normal">The normal Vector (optional)</param>
            /// <param name="UV">The texture U/V coordinates (optional)</param>
            /// <param name="Color">The diffuse color (optional)</param>
            public OVertex(OVector3 Position, OVector3 Normal, OVector2 Texture0, uint Color)
            {
                node = new List<int>();
                weight = new List<float>();

                position = new OVector3(Position);
                normal = new OVector3(Normal);
                texture0 = new OVector2(Texture0);
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
            ///     Add Weighting information of the Vertex.
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

            vertex.u = input.texture0.x;
            vertex.v = input.texture0.y;

            vertex.color = input.diffuseColor;

            return vertex;
        }

        public class OMatrix
        { //4x4
            float[,] matrix;

            public OMatrix()
            {
                matrix = new float[4, 4];
                matrix[0, 0] = 1.0f;
                matrix[1, 1] = 1.0f;
                matrix[2, 2] = 1.0f;
                matrix[3, 3] = 1.0f;
            }

            public float M11 { get { return matrix[0, 0]; } set { matrix[0, 0] = value; } }
            public float M12 { get { return matrix[0, 1]; } set { matrix[0, 1] = value; } }
            public float M13 { get { return matrix[0, 2]; } set { matrix[0, 2] = value; } }
            public float M14 { get { return matrix[0, 3]; } set { matrix[0, 3] = value; } }

            public float M21 { get { return matrix[1, 0]; } set { matrix[1, 0] = value; } }
            public float M22 { get { return matrix[1, 1]; } set { matrix[1, 1] = value; } }
            public float M23 { get { return matrix[1, 2]; } set { matrix[1, 2] = value; } }
            public float M24 { get { return matrix[1, 3]; } set { matrix[1, 3] = value; } }

            public float M31 { get { return matrix[2, 0]; } set { matrix[2, 0] = value; } }
            public float M32 { get { return matrix[2, 1]; } set { matrix[2, 1] = value; } }
            public float M33 { get { return matrix[2, 2]; } set { matrix[2, 2] = value; } }
            public float M34 { get { return matrix[2, 3]; } set { matrix[2, 3] = value; } }

            public float M41 { get { return matrix[3, 0]; } set { matrix[3, 0] = value; } }
            public float M42 { get { return matrix[3, 1]; } set { matrix[3, 1] = value; } }
            public float M43 { get { return matrix[3, 2]; } set { matrix[3, 2] = value; } }
            public float M44 { get { return matrix[3, 3]; } set { matrix[3, 3] = value; } }

            public static OMatrix operator *(OMatrix a, OMatrix b)
            {
                OMatrix c = new OMatrix();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            sum += a.matrix[i, k] * b.matrix[k, j];
                        }
                        c.matrix[i, j] = sum;
                    }
                }

                return c;
            }

            /// <summary>
            ///     Creates a scaling Matrix with a given proportion size.
            /// </summary>
            /// <param name="scale">The scale proportions</param>
            /// <returns></returns>
            public static OMatrix scale(OVector3 scale)
            {
                OMatrix output = new OMatrix();

                output.M11 = scale.x;
                output.M22 = scale.y;
                output.M33 = scale.z;

                return output;
            }

            /// <summary>
            ///     Creates a 2-D rotation Matrix. Only X and Y axis are affected.
            /// </summary>
            /// <param name="angle">Angle in PI radians (max 2 * PI)</param>
            /// <returns></returns>
            public static OMatrix rotate2D(double angle)
            {
                OMatrix output = new OMatrix();

                output.M11 = (float)Math.Cos(angle);
                output.M21 = (float)-Math.Sin(angle);
                output.M12 = (float)Math.Sin(angle);
                output.M22 = (float)Math.Cos(angle);

                return output;
            }

            /// <summary>
            ///     Creates a translation Matrix with the given position offset.
            /// </summary>
            /// <param name="position">The position offset</param>
            /// <returns></returns>
            public static OMatrix translate(OVector3 position)
            {
                OMatrix output = new OMatrix();

                output.M41 = position.x;
                output.M42 = position.y;
                output.M43 = position.z;

                return output;
            }
        }

        public class OModelObject
        {
            public List<OVertex> obj;
            public CustomVertex[] renderBuffer;
            public ushort materialId = 0;
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
            public short parentId;
            public String name = null;

            /// <summary>
            ///     Creates a new Bone.
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

        public class OCombiner
        {
            public ushort rgbScale, alphaScale;
            public OCombine combineRgb, combineAlpha;
            public List<OCombineSource> rgbSource;
            public List<OCombineOperand> rgbOperand;
            public List<OCombineSource> alphaSource;
            public List<OCombineOperand> alphaOperand;

            public OCombiner()
            {
                rgbSource = new List<OCombineSource>();
                rgbOperand = new List<OCombineOperand>();
                alphaSource = new List<OCombineSource>();
                alphaOperand = new List<OCombineOperand>();
            }
        }

        public class OTextureParameter
        {
            public String name0, name1, name2;
            public List<OCoordinator> coordinator;
            public List<OCombiner> combiner;
            public uint constantColorIndex;

            public OVector3 sourceCoordinate;
            public OTextureProjection projection;
            public uint referenceCamera;
            public float scaleU, scaleV;
            public float rotate;
            public float translateU, translateV;

            public OTextureParameter()
            {
                coordinator = new List<OCoordinator>();
                combiner = new List<OCombiner>();
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
            ///     Adds a Object to the model.
            /// </summary>
            /// <param name="obj">The Object</param>
            public void addObject(OModelObject obj)
            {
                modelObject.Add(obj);
            }

            /// <summary>
            ///     Adds a Bone to the skeleton.
            /// </summary>
            /// <param name="bone">The Bone</param>
            public void addBone(OBone bone)
            {
                skeleton.Add(bone);
            }

            /// <summary>
            ///     Adds a Texture Parameter to the model
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

            /// <summary>
            ///     Adds a new Material.
            /// </summary>
            /// <param name="mat">The Material</param>
            public void addMaterial(OMaterial mat)
            {
                material.Add(mat);
            }
        }
    }
}
