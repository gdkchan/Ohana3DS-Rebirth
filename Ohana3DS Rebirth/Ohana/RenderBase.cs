using System;
using System.Collections.Generic;
using System.Drawing;

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
            /// <param name="_x">The X position</param>
            /// <param name="_y">The Y position</param>
            public OVector2(float _x, float _y)
            {
                x = _x;
                y = _y;
            }

            /// <summary>
            ///     Creates a new 2-D Vector.
            /// </summary>
            /// <param name="vector">The 2-D Vector</param>
            public OVector2(OVector2 vector)
            {
                x = vector.x;
                y = vector.y;
            }

            /// <summary>
            ///     Creates a new 2-D Vector.
            /// </summary>
            public OVector2()
            {
            }

            public override string ToString()
            {
                return String.Format("X:{0}; Y:{1}", x, y);
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
            /// <param name="_x">The X position</param>
            /// <param name="_y">The Y position</param>
            /// <param name="_z">The Z position</param>
            public OVector3(float _x, float _y, float _z)
            {
                x = _x;
                y = _y;
                z = _z;
            }

            /// <summary>
            ///     Creates a new 3-D Vector.
            /// </summary>
            /// <param name="vector">The 3-D vector</param>
            public OVector3(OVector3 vector)
            {
                x = vector.x;
                y = vector.y;
                z = vector.z;
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
            /// <param name="input">Input vector</param>
            /// <param name="matrix">The matrix</param>
            /// <returns></returns>
            public static OVector3 transform(OVector3 input, OMatrix matrix)
            {
                OVector3 output = new OVector3();
                output.x = input.x * matrix.M11 + input.y * matrix.M21 + input.z * matrix.M31 + matrix.M41;
                output.y = input.x * matrix.M12 + input.y * matrix.M22 + input.z * matrix.M32 + matrix.M42;
                output.z = input.x * matrix.M13 + input.y * matrix.M23 + input.z * matrix.M33 + matrix.M43;
                return output;
            }

            public override string ToString()
            {
                return String.Format("X:{0}; Y:{1}; Z:{2}", x, y, z);
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
            /// <param name="_x">The X position</param>
            /// <param name="_y">The Y position</param>
            /// <param name="_z">The Z position</param>
            /// <param name="_w">The W position</param>
            public OVector4(float _x, float _y, float _z, float _w)
            {
                x = _x;
                y = _y;
                z = _z;
                w = _w;
            }

            /// <summary>
            ///     Creates a new 4-D Vector.
            /// </summary>
            /// <param name="vector">The 4-D vector</param>
            public OVector4(OVector4 vector)
            {
                x = vector.x;
                y = vector.y;
                z = vector.z;
                w = vector.w;
            }

            /// <summary>
            ///     Creates a new 4-D Vector.
            /// </summary>
            public OVector4()
            {
            }

            public override string ToString()
            {
                return String.Format("X:{0}; Y:{1}; Z:{2}; W:{3}", x, y, z, w);
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
            public float u0, v0;
            public float u1, v1;
            public float u2, v2;
        }

        /// <summary>
        ///     Converts OVertex to CustomVertex.
        ///     A CustomVertex buffer can be directly passed to the renderer.
        /// </summary>
        /// <param name="input">The OVertex to be converted</param>
        /// <returns>The CustomVertex</returns>
        public static CustomVertex convertVertex(OVertex input)
        {
            CustomVertex vertex;

            vertex.x = input.position.x;
            vertex.y = input.position.y;
            vertex.z = input.position.z;

            vertex.nx = input.normal.x;
            vertex.ny = input.normal.y;
            vertex.nz = input.normal.z;

            vertex.u0 = input.texture0.x;
            vertex.v0 = input.texture0.y;

            vertex.u1 = input.texture1.x;
            vertex.v1 = input.texture1.y;

            vertex.u2 = input.texture2.x;
            vertex.v2 = input.texture2.y;

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
            /// <param name="scale">The Scale proportions</param>
            /// <returns></returns>
            public static OMatrix scale(OVector3 scale)
            {
                OMatrix output = new OMatrix
                {
                    M11 = scale.x, 
                    M22 = scale.y, 
                    M33 = scale.z
                };

                return output;
            }

            /// <summary>
            ///     Uniform scales the X/Y/Z axis with the same value.
            /// </summary>
            /// <param name="scale">The Scale proportion</param>
            /// <returns></returns>
            public static OMatrix scale(float scale)
            {
                OMatrix output = new OMatrix
                {
                    M11 = scale, 
                    M22 = scale, 
                    M33 = scale
                };

                return output;
            }

            /// <summary>
            ///     Rotates about the X axis.
            /// </summary>
            /// <param name="angle">Angle in radians</param>
            /// <returns></returns>
            public static OMatrix rotateX(float angle)
            {
                OMatrix output = new OMatrix
                {
                    M22 = (float)Math.Cos(angle),
                    M32 = -(float)Math.Sin(angle),
                    M23 = (float)Math.Sin(angle),
                    M33 = (float)Math.Cos(angle)
                };

                return output;
            }

            /// <summary>
            ///     Rotates about the Y axis.
            /// </summary>
            /// <param name="angle">Angle in radians</param>
            /// <returns></returns>
            public static OMatrix rotateY(float angle)
            {
                OMatrix output = new OMatrix
                {
                    M11 = (float)Math.Cos(angle),
                    M31 = (float)Math.Sin(angle),
                    M13 = -(float)Math.Sin(angle),
                    M33 = (float)Math.Cos(angle)
                };

                return output;
            }

            /// <summary>
            ///     Rotates about the Z axis.
            /// </summary>
            /// <param name="angle">Angle in radians</param>
            /// <returns></returns>
            public static OMatrix rotateZ(float angle)
            {
                OMatrix output = new OMatrix
                {
                    M11 = (float)Math.Cos(angle),
                    M21 = -(float)Math.Sin(angle),
                    M12 = (float)Math.Sin(angle),
                    M22 = (float)Math.Cos(angle)
                };

                return output;
            }

            /// <summary>
            ///     Creates a translation Matrix with the given position offset.
            /// </summary>
            /// <param name="position">The Position offset</param>
            /// <returns></returns>
            public static OMatrix translate(OVector3 position)
            {
                OMatrix output = new OMatrix
                {
                    M41 = position.x, 
                    M42 = position.y, 
                    M43 = position.z
                };

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

        public enum OTranslucencyKind
        {
            opaque = 0,
            translucent = 1,
            subtractive = 2,
            additive = 3
        }

        public enum OSkinningMode
        {
            none = 0,
            smoothSkinning = 1,
            rigidSkinning = 2
        }

        public class OModelObject
        {
            public List<OVertex> obj;
            public CustomVertex[] renderBuffer;
            public bool hasNormal;
            public int texUVCount;
            public ushort materialId;
            public ushort renderPriority;
            public bool visible;
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

        public enum OBillboardMode
        {
            off = 0,
            world = 2,
            worldViewpoint = 3,
            screen = 4,
            screenViewpoint = 5,
            yAxial = 6,
            yAxialViewpoint = 7
        }

        public class OBone
        {
            public OVector3 translation;
            public OVector3 rotation;
            public OVector3 scale;
            public OVector3 absoluteScale;
            public short parentId;
            public string name = null;
            public OBillboardMode billboardMode;
            public bool isSegmentScaleCompensate;

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

        public enum OCullMode
        {
            never = 0,
            frontFace = 1,
            backFace = 2
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

        public enum OConstantColor
        {
            constant0 = 0,
            constant1 = 1,
            constant2 = 2,
            constant3 = 3,
            constant4 = 4,
            constant5 = 5,
            emission = 6,
            ambient = 7,
            diffuse = 8,
            specular0 = 9,
            specular1 = 0xa,
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
            alpha = 2,
            oneMinusAlpha = 3,
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
            oneMinusAlpha = 1,
            red = 2,
            oneMinusRed = 3,
            green = 4,
            oneMinusGreen = 5,
            blue = 6,
            oneMinusBlue = 7
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

        public enum OBumpTexture
        {
            texture0 = 0,
            texture1 = 1,
            texture2 = 2,
            texture3 = 3
        }

        public enum OBumpMode
        {
            notUsed = 0,
            asBump = 1,
            asTangent = 2
        }

        public struct OFragmentBump
        {
            public OBumpTexture texture;
            public OBumpMode mode;
            public bool isBumpRenormalize;
        }

        public enum OFresnelConfig
        {
            none = 0,
            primary = 1,
            secondary = 2,
            primarySecondary = 3
        }

        public enum OFragmentSamplerInput
        {
            halfNormalCosine = 0, //N·H
            halfViewCosine = 1, //V·H
            viewNormalCosine = 2, //N·V
            normalLightCosine = 3, //L·N
            spotLightCosine = 4, //-L·P
            phiCosine = 5 //cosϕ
        }

        public enum OFragmentSamplerScale
        {
            one = 0,
            two = 1,
            four = 2,
            eight = 3,
            quarter = 6,
            half = 7
        }

        public struct OFragmentSampler
        {
            public OFragmentSamplerInput input;
            public OFragmentSamplerScale scale;
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

            public OFragmentSampler reflectanceRSampler;
            public OFragmentSampler reflectanceGSampler;
            public OFragmentSampler reflectanceBSampler;
            public OFragmentSampler distribution0Sampler;
            public OFragmentSampler distribution1Sampler;
            public OFragmentSampler fresnelSampler;
        }

        public class OTextureCombiner
        {
            public ushort rgbScale, alphaScale;
            public OConstantColor constantColor;
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
            public uint testReference; 
        }

        public class OFragmentShader
        {
            public uint layerConfig;
            public Color bufferColor;
            public OFragmentBump bump;
            public OFragmentLighting lighting;
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

        public enum OBlendMode
        {
            logical = 0,
            notUsed = 2,
            blend = 3
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

        public struct OBlendOperation
        {
            public OBlendMode mode;
            public OLogicalOperation logicalOperation;
            public OBlendFunction rgbFunctionSource, rgbFunctionDestination;
            public OBlendEquation rgbBlendEquation;
            public OBlendFunction alphaFunctionSource, alphaFunctionDestination;
            public OBlendEquation alphaBlendEquation;
            public Color blendColor;
        }

        public enum OStencilOp
        {
            keep = 0,
            zero = 1,
            replace = 2,
            increase = 3,
            decrease = 4,
            increaseWrap = 5,
            decreaseWrap = 6
        }

        public struct OStencilOperation
        {
            public bool isTestEnabled;
            public OTestFunction testFunction;
            public uint testReference;
            public uint testMask;
            public OStencilOp failOperation;
            public OStencilOp zFailOperation;
            public OStencilOp passOperation;
        }

        public struct OFragmentOperation
        {
            public ODepthOperation depth;
            public OBlendOperation blend;
            public OStencilOperation stencil;
        }

        public class OMaterial
        {
            public String name, name0, name1, name2;

            public OMaterialColor materialColor;
            public ORasterization rasterization;
            public List<OTextureCoordinator> textureCoordinator;
            public List<OTextureMapper> textureMapper;
            public OFragmentShader fragmentShader;
            public OFragmentOperation fragmentOperation;

            public uint lightSetIndex;
            public uint fogIndex;
            public bool isFragmentLightEnabled;
            public bool isVertexLightEnabled;
            public bool isHemiSphereLightEnabled;
            public bool isHemiSphereOcclusionEnabled;
            public bool isFogEnabled;

            public OMaterial()
            {
                textureCoordinator = new List<OTextureCoordinator>();
                textureMapper = new List<OTextureMapper>();
                fragmentShader = new OFragmentShader();
            }
        }

        public class OModel
        {
            public string name;
            public List<OModelObject> modelObject;
            public List<OBone> skeleton;
            public List<OMaterial> material;
            public OMatrix transform;
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
            /// <param name="_texture">The texture, size must be a power of 2</param>
            /// <param name="_name">Texture name</param>
            public OTexture(Bitmap _texture, String _name)
            {
                texture = new Bitmap(_texture);
                _texture.Dispose();
                name = _name;
            }
        }

        public class OLookUpTableSampler
        {
            public string name;
            public float[] table;

            public OLookUpTableSampler()
            {
                table = new float[256];
            }
        }

        public class OLookUpTable
        {
            public string name;
            public List<OLookUpTableSampler> sampler;

            public OLookUpTable()
            {
                sampler = new List<OLookUpTableSampler>();
            }
        }

        public enum OLightType
        {
            directional = 0,
            point = 1,
            spot = 2
        }

        public class OLight
        {
            public string name;

            public OVector3 transformScale;
            public OVector3 transformRotate;
            public OVector3 transformTranslate;

            public Color ambient;
            public Color diffuse;
            public Color specular0;
            public Color specular1;
            public OVector3 direction;

            public float attenuationStart;
            public float attenuationEnd;

            public bool isLightEnabled;
            public bool isTwoSideDiffuse;
            public bool isDistanceAttenuationEnabled;
            public OLightType lightType;

            public OFragmentSampler angleSampler;
        }

        public enum OCameraView
        {
            aimTarget = 0,
            lookAtTarget = 1,
            rotate = 2
        }

        public enum OCameraProjection
        {
            perspective = 0,
            orthogonal = 1
        }

        public class OCamera
        {
            public string name;

            public OVector3 transformScale;
            public OVector3 transformRotate;
            public OVector3 transformTranslate;
            public OVector3 target;
            public OVector3 rotation;
            public OVector3 upVector;
            public float twist;
            public OCameraView view;
            public OCameraProjection projection;
            public float zNear, zFar;
            public float fieldOfViewY;
            public float height;
            public float aspectRatio;

            public bool isInheritingTargetRotate;
            public bool isInheritingTargetTranslate;
            public bool isInheritingUpRotate;
        }

        public class OFog
        {
            public string name;

            public OVector3 transformScale;
            public OVector3 transformRotate;
            public OVector3 transformTranslate;

            public Color fogColor;

            public float minFogDepth, maxFogDepth;
            public float fogDensity;

            public bool isZFlip;
            public bool isAttenuateDistance;
        }

        public enum ORepeatMethod
        {
            none = 0,
            repeat = 1,
            mirror = 2,
            relativeRepeat = 3
        }

        public class OHermiteFloat
        {
            public float value;
            public float inSlope;
            public float outSlope;
            public float frame;

            /// <summary>
            ///     Creates a new Hermite Float.
            /// </summary>
            /// <param name="_value">The point value</param>
            /// <param name="_inSlope">The input slope</param>
            /// <param name="_outSlope">The output slope</param>
            /// <param name="_frame">The frame number</param>
            public OHermiteFloat(float _value, float _inSlope, float _outSlope, float _frame)
            {
                value = _value;
                inSlope = _inSlope;
                outSlope = _outSlope;
                frame = _frame;
            }

            /// <summary>
            ///     Creates a new Hermite Float.
            /// </summary>
            public OHermiteFloat()
            {
            }
        }

        public class OLinearFloat
        {
            public float value;
            public float frame;

            /// <summary>
            ///     Creates a new Linear Float.
            /// </summary>
            /// <param name="_value">The point value</param>
            /// <param name="_frame">The frame number</param>
            public OLinearFloat(float _value, float _frame)
            {
                value = _value;
                frame = _frame;
            }

            /// <summary>
            ///     Creates a new Linear Float.
            /// </summary>
            public OLinearFloat()
            {
            }
        }

        public enum OInterpolationMode
        {
            step = 0,
            linear = 1,
            hermite = 2
        }

        public class OAnimationKeyFrame
        {
            public List<OHermiteFloat> hermiteFrame;
            public List<OLinearFloat> linearFrame;
            public OInterpolationMode interpolation;
            public float startFrame, endFrame;
            public bool exists;

            public ORepeatMethod preRepeat;
            public ORepeatMethod postRepeat;

            public OAnimationKeyFrame()
            {
                hermiteFrame = new List<OHermiteFloat>();
                linearFrame = new List<OLinearFloat>();
            }
        }

        public class OAnimationFrame
        {
            public List<OVector4> vector;
            public float startFrame, endFrame;
            public bool exists;

            public ORepeatMethod preRepeat;
            public ORepeatMethod postRepeat;

            public OAnimationFrame()
            {
                vector = new List<OVector4>();
            }
        }

        public enum OSegmentType
        {
            single = 0,
            boolean = 1,
            vector2 = 2,
            vector3 = 3,
            transform = 4,
            rgbaColor = 5,
            integer = 6
        }

        public class OSkeletalAnimationBone
        {
            public string name;
            public OAnimationKeyFrame rotationX, rotationY, rotationZ;
            public OAnimationKeyFrame translationX, translationY, translationZ;

            public OAnimationFrame rotationQuaternion;
            public OAnimationFrame translation;
            public bool isFrameFormat;

            public List<OMatrix> transform;
            public bool isFullBakedFormat;

            public OSkeletalAnimationBone()
            {
                rotationX = new OAnimationKeyFrame();
                rotationY = new OAnimationKeyFrame();
                rotationZ = new OAnimationKeyFrame();
                translationX = new OAnimationKeyFrame();
                translationY = new OAnimationKeyFrame();
                translationZ = new OAnimationKeyFrame();

                rotationQuaternion = new OAnimationFrame();
                translation = new OAnimationFrame();

                transform = new List<OMatrix>();
            }
        }

        public enum OLoopMode
        {
            oneTime = 0,
            loop = 1
        }

        public class OAnimationBase
        {
            public virtual string name { get; set; }
            public virtual float frameSize { get; set; }
            public virtual OLoopMode loopMode { get; set; }
        }

        public class OAnimationListBase
        {
            public List<OAnimationBase> list;

            public OAnimationListBase()
            {
                list = new List<OAnimationBase>();
            }
        }

        public class OSkeletalAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public List<OSkeletalAnimationBone> bone;

            public OSkeletalAnimation()
            {
                bone = new List<OSkeletalAnimationBone>();
            }
        }

        public enum OMaterialAnimationType
        {
            constant0 = 1,
            constant1 = 2,
            constant2 = 3,
            constant3 = 4,
            constant4 = 5,
            constant5 = 6,
            emission = 7,
            ambient = 8,
            diffuse = 9,
            specular0 = 0xa,
            specular1 = 0xb,
            borderColorMapper0 = 0xc,
            textureMapper0 = 0xd,
            borderColorMapper1 = 0xe,
            textureMapper1 = 0xf,
            borderColorMapper2 = 0x10,
            textureMapper2 = 0x11,
            blendColor = 0x12,
            scaleCoordinator0 = 0x13,
            rotateCoordinator0 = 0x14,
            translateCoordinator0 = 0x15,
            scaleCoordinator1 = 0x16,
            rotateCoordinator1 = 0x17,
            translateCoordinator1 = 0x18,
            scaleCoordinator2 = 0x19,
            rotateCoordinator2 = 0x1a,
            translateCoordinator2 = 0x1b
        }

        public class OMaterialAnimationData
        {
            public string name;
            public OMaterialAnimationType type;
            public List<OAnimationKeyFrame> frameList;

            public OMaterialAnimationData()
            {
                frameList = new List<OAnimationKeyFrame>();
            }
        }

        public class OMaterialAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public List<OMaterialAnimationData> data;
            public List<string> textureName;

            public OMaterialAnimation()
            {
                data = new List<OMaterialAnimationData>();
                textureName = new List<string>();
            }
        }

        public enum OCameraAnimationType
        {
            transform = 5,
            vuTargetPosition = 6,
            vuTwist = 7,
            vuUpwardVector = 8,
            vuViewRotate = 9,
            puNear = 0xa,
            puFar = 0xb,
            puFovy = 0xc,
            puAspectRatio = 0xd,
            puHeight = 0xe
        }
        
        public class OCameraAnimationData
        {
            public string name;
            public OCameraAnimationType type;
            public List<OAnimationKeyFrame> frameList;

            public OCameraAnimationData()
            {
                frameList = new List<OAnimationKeyFrame>();
            }
        }

        public class OCameraAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public OCameraView viewMode;
            public OCameraProjection projectionMode;
            public List<OCameraAnimationData> data;

            public OCameraAnimation()
            {
                data = new List<OCameraAnimationData>();
            }
        }

        public class OModelGroup
        {
            public List<OModel> model;
            public List<OTexture> texture;
            public List<OLookUpTable> lookUpTable;
            public List<OLight> light;
            public List<OCamera> camera;
            public List<OFog> fog;
            public OAnimationListBase skeletalAnimation;
            public OAnimationListBase materialAnimation;
            public OAnimationListBase cameraAnimation;
            public OVector3 minVector, maxVector;

            public OModelGroup()
            {
                model = new List<OModel>();
                texture = new List<OTexture>();
                lookUpTable = new List<OLookUpTable>();
                light = new List<OLight>();
                camera = new List<OCamera>();
                fog = new List<OFog>();
                skeletalAnimation = new OAnimationListBase();
                materialAnimation = new OAnimationListBase();
                cameraAnimation = new OAnimationListBase();
                minVector = new OVector3();
                maxVector = new OVector3();
            }

            /// <summary>
            ///     Adds a new Model.
            /// </summary>
            /// <param name="_model">The Model</param>
            public void addModel(OModel _model)
            {
                model.Add(_model);
            }

            /// <summary>
            ///     Adds a new Texture.
            /// </summary>
            /// <param name="_texture">The Texture</param>
            public void addTexture(OTexture _texture)
            {
                texture.Add(_texture);
            }

            /// <summary>
            ///     Adds a new LookUp Table.
            /// </summary>
            /// <param name="_lookUpTable">The LUT</param>
            public void addLUT(OLookUpTable _lookUpTable)
            {
                lookUpTable.Add(_lookUpTable);
            }

            /// <summary>
            ///     Adds a new Light.
            /// </summary>
            /// <param name="_light">The Light</param>
            public void addLight(OLight _light)
            {
                light.Add(_light);
            }

            /// <summary>
            ///     Adds a new Camera.
            /// </summary>
            /// <param name="_camera">The Camera</param>
            public void addCamera(OCamera _camera)
            {
                camera.Add(_camera);
            }

            /// <summary>
            ///     Adds a new Fog.
            /// </summary>
            /// <param name="_fog">The Fog</param>
            public void addFog(OFog _fog)
            {
                fog.Add(_fog);
            }

            /// <summary>
            ///     Adds a new Skeletal Animation.
            /// </summary>
            /// <param name="_skeletalAnimation">The Skeletal Animation</param>
            public void addSekeletalAnimaton(OSkeletalAnimation _skeletalAnimation)
            {
                skeletalAnimation.list.Add(_skeletalAnimation);
            }

            /// <summary>
            ///     Adds a new Material Animation.
            /// </summary>
            /// <param name="_materialColorAnimation">The Material Animation</param>
            public void addMaterialAnimation(OMaterialAnimation _materialAnimation)
            {
                materialAnimation.list.Add(_materialAnimation);
            }

            /// <summary>
            ///     Adds a new Camera Animation.
            /// </summary>
            /// <param name="_cameraAnimation">The Camera Animation</param>
            public void addCameraAnimation(OCameraAnimation _cameraAnimation)
            {
                cameraAnimation.list.Add(_cameraAnimation);
            }
        }
    }
}
