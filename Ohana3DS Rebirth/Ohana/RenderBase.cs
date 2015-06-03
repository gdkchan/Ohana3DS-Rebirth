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
            /// <param name="W">The W position</param>
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
            /// <param name="_position">The position of the Vertex on the 3-D space</param>
            /// <param name="_normal">The normal Vector (optional)</param>
            /// <param name="_texture0">The texture U/V coordinates (optional)</param>
            /// <param name="_color">The diffuse color (optional)</param>
            public OVertex(OVector3 _position, OVector3 _normal, OVector2 _texture0, uint _color)
            {
                node = new List<int>();
                weight = new List<float>();

                position = new OVector3(_position);
                normal = new OVector3(_normal);
                texture0 = new OVector2(_texture0);
                diffuseColor = _color;
            }

            /// <summary>
            ///     Add a Node to the Vertex.
            ///     It may contain multiple nodes, and each node must be the Id of a Bone on the Skeleton.
            /// </summary>
            /// <param name="_node"></param>
            public void addNode(int _node)
            {
                node.Add(_node);
            }

            /// <summary>
            ///     Add Weighting information of the Vertex.
            /// </summary>
            /// <param name="_weight"></param>
            public void addWeight(float _weight)
            {
                weight.Add(_weight);
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

        public class OOrientedBoundingBox
        {
            public OVector3 centerPosition;
            public OMatrix orientationMatrix;
            public OVector3 size;

            public OOrientedBoundingBox()
            {
                orientationMatrix = new OMatrix();
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

        public struct OMaterialColor
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

            public float colorScale;
        }

        public static Color getColor(BinaryReader input)
        {
            byte r = (byte)input.ReadByte();
            byte g = (byte)input.ReadByte();
            byte b = (byte)input.ReadByte();
            byte a = (byte)input.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }

        public enum OCullMode
        {
            frontFace = 0,
            backFace = 1,
            always = 2,
            never = 3
        }

        public struct ORasterization
        {
            public OCullMode cullMode;
            public bool isPolygonOffsetEnabled;
            public float polygonOffsetUnit;
        }

        public enum OTextureMinFilter
        {
            nearestMipmapNearest = 1,
            nearestMipmapLinear = 2,
            linearMipmapNearest = 4,
            linearMipmapLinear = 5
        }

        public enum OTextureMagFilter
        {
            nearest = 0,
            linear = 1
        }

        public enum OTextureWrap
        {
            clampToEdge = 0,
            clampToBorder = 1,
            repeat = 2,
            mirroredRepeat = 3
        }

        public enum OTextureProjection
        {
            uvMap = 0,
            cameraCubeMap = 1,
            cameraSphereMap = 2,
            projectionMap = 3
        }

        public enum OCombineOperator
        {
            replace = 0,
            modulate = 1,
            add = 2,
            addSigned = 3,
            interpolate = 4,
            subtract = 5,
            dot3Rgb = 6,
            dot3Rgba = 7,
            multiplyAdd = 8,
            addMultiply = 9
        }

        public enum OCombineSource
        {
            
            primaryColor = 0,
            fragmentPrimaryColor = 1,
            fragmentSecondaryColor = 2,
            texture0 = 3,
            texture1 = 4,
            texture2 = 5,
            texture3 = 6,
            previousBuffer = 0xd,
            constant = 0xe,
            previous = 0xf
        }

        public enum OCombineOperandRgb
        {
            color = 0,
            oneMinusColor = 1,
            red = 4,
            oneMinusRed = 5,
            green = 8,
            oneMinusGreen = 9,
            blue = 0xc,
            oneMinusBlue = 0xd
        }

        public enum OCombineOperandAlpha
        {
            alpha = 0,
            oneMinusAlpha = 1
        }

        public struct OTextureCoordinator
        {
            public OTextureProjection projection;
            public uint referenceCamera;
            public float scaleU, scaleV;
            public float rotate;
            public float translateU, translateV;
        }

        public struct OTextureMapper
        {
            public OTextureMinFilter minFilter;
            public OTextureMagFilter magFilter;
            public OTextureWrap wrapU, wrapV;
            public uint minLOD;
            public float LODBias;
            public Color borderColor;
        }

        public enum OBumpMode
        {
            notUsed = 0,
            asBump = 1,
            asTangent = 2
        }

        public struct OFragmentBump
        {
            public uint bumpTextureIndex;
            public OBumpMode bumpMode;
            public bool isBumpRenormalize;
        }

        public enum OFresnelConfig
        {
            none = 0,
            primary = 1,
            secondary = 2,
            primarySecondary = 3
        }

        public enum OReflectanceSamplerInput
        {
            halfNormalCosine = 0,
            halfViewCosine = 1,
            viewNormalCosine = 2,
            normalLightCosine = 3,
            spotLightCosine = 4,
            phiCosine = 5
        }

        public enum OReflectanceSamplerScale
        {
            one = 0,
            two = 1,
            four = 2,
            eight = 3,
            quarter = 6,
            half = 7
        }

        public struct OReflectanceSampler
        {
            public OReflectanceSamplerInput input;
            public OReflectanceSamplerScale scale;
            public string samplerName;
            public string materialLUTName; //LookUp Table
        }

        public struct OFragmentLighting
        {
            public OFresnelConfig fresnelConfig;
            public bool isClampHighLight;
            public bool isDistribution0Enabled;
            public bool isDistribution1Enabled;
            public bool isGeometryFactor0Enabled;
            public bool isGeometryFactor1Enabled;
            public bool isReflectionEnabled;

            public OReflectanceSampler reflectanceRSampler;
            public OReflectanceSampler reflectanceGSampler;
            public OReflectanceSampler reflectanceBSampler;
            public OReflectanceSampler distribution0Sampler;
            public OReflectanceSampler distribution1Sampler;
            public OReflectanceSampler fresnelSampler;
        }

        public class OTextureCombiner
        {
            public ushort rgbScale, alphaScale;
            public OCombineOperator combineRgb, combineAlpha;
            public List<OCombineSource> rgbSource;
            public List<OCombineOperandRgb> rgbOperand;
            public List<OCombineSource> alphaSource;
            public List<OCombineOperandAlpha> alphaOperand;

            public OTextureCombiner()
            {
                rgbSource = new List<OCombineSource>();
                rgbOperand = new List<OCombineOperandRgb>();
                alphaSource = new List<OCombineSource>();
                alphaOperand = new List<OCombineOperandAlpha>();
            }
        }

        public struct OAlphaTest
        {
            public bool isTestEnabled;
            public OTestFunction testFunction;
        }

        public class OFragmentShader
        {
            public uint layerConfig;
            public Color bufferColor;
            public OFragmentBump fragmentBump;
            public OFragmentLighting fragmentLighting;
            public List<OTextureCombiner> textureCombiner;
            public OAlphaTest alphaTest;

            public OFragmentShader()
            {
                textureCombiner = new List<OTextureCombiner>();
            }
        }

        public enum OTestFunction
        {
            never = 0,
            always = 1,
            equal = 2,
            notEqual = 3,
            less = 4,
            lessOrEqual = 5,
            greater = 6,
            greaterOrEqual = 7
        }

        public struct ODepthOperation
        {
            public bool isTestEnabled;
            public OTestFunction testFunction;
            public bool isMaskEnabled;
        }

        public enum OBlendFunction
        {
            zero = 0,
            one = 1,
            sourceColor = 2,
            oneMinusSourceColor = 3,
            destinationColor = 4,
            oneMinusDestinationColor = 5,
            sourceAlpha = 6,
            oneMinusSourceAlpha = 7,
            destinationAlpha = 8,
            oneMinusDestinationAlpha = 9,
            constantColor = 0xa,
            oneMinusConstantColor = 0xb,
            constantAlpha = 0xc,
            oneMinusConstantAlpha = 0xd,
            sourceAlphaSaturate = 0xe
        }

        public enum OBlendEquation
        {
            add = 0,
            subtract = 1,
            reverseSubtract = 2,
            min = 3,
            max = 4
        }

        public enum OLogicalOperation
        {
            clear = 0,
            and = 1,
            andReverse = 2,
            copy = 3,
            set = 4,
            copyInverted = 5,
            noOperation = 6,
            invert = 7,
            notAnd = 8,
            or = 9,
            notOr = 0xa,
            exclusiveOr = 0xb,
            equiv = 0xc,
            andInverted = 0xd,
            orReverse = 0xe,
            orInverted = 0xf
        }

        public struct OBlendOperation
        {
            public OLogicalOperation logicalOperation;
            public OBlendFunction rgbFunctionSource, rgbFunctionDestination;
            public OBlendEquation rgbBlendEquation;
            public OBlendFunction alphaFunctionSource, alphaFunctionDestination;
            public OBlendEquation alphaBlendEquation;
            public Color blendColor;
        }

        public enum OStencilOperation
        {
            keep = 0,
            zero = 1,
            replace = 2,
            increase = 3,
            decrease = 4,
            increaseWrap = 5,
            decreaseWrap = 6
        }

        public struct OStencilTests
        {
            public bool isTestEnabled;
            public OTestFunction testFunction;
            public uint testReference;
            public uint testMask;
            public OStencilOperation failOperation;
            public OStencilOperation zFailOperation;
            public OStencilOperation passOperation;
        }

        public struct OFragmentOperation
        {
            public ODepthOperation depthOperation;
            public OBlendOperation blendOperation;
            public OStencilTests stencilOperation;
        }

        public class OMaterial
        {
            public String name0, name1, name2;

            public OMaterialColor materialColor;
            public ORasterization rasterization;
            public OTextureCoordinator textureCoordinator;
            public List<OTextureMapper> textureMapper;
            public OFragmentShader fragmentShader;
            public OFragmentOperation fragmentOperation;
            public uint constantColorIndex;

            public uint lightSetIndex;
            public uint fogIndex;
            public bool isFragmentLightEnabled;
            public bool isVertexLightEnabled;
            public bool isHemiSphereLightEnabled;
            public bool isHemiSphereOcclusionEnabled;
            public bool isFogEnabled;

            public OMaterial()
            {
                textureMapper = new List<OTextureMapper>();
                fragmentShader = new OFragmentShader();
            }
        }

        public class OModel
        {
            public List<OModelObject> modelObject;
            public List<OBone> skeleton;
            public List<OMaterial> material;
            public float height;

            public OModel()
            {
                modelObject = new List<OModelObject>();
                skeleton = new List<OBone>();
                material = new List<OMaterial>();
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
            ///     Adds a Material to the model.
            /// </summary>
            /// <param name="mat">The Material</param>
            public void addMaterial(OMaterial mat)
            {
                material.Add(mat);
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

        public class OModelGroup
        {
            public List<OModel> model;
            public List<OTexture> texture;
            public float minimumSize, maximumSize;

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
