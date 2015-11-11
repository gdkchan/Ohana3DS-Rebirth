using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Ohana3DS_Rebirth.Ohana
{
    public class RenderBase
    {
        /// <summary>
        ///     2-D Vector.
        /// </summary>
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

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                return this == (OVector2)obj;
            }

            public override int GetHashCode()
            {
                return x.GetHashCode() ^
                    y.GetHashCode();
            }

            public static bool operator ==(OVector2 a, OVector2 b)
            {
                return a.x == b.x &&
                    a.y == b.y;
            }

            public static bool operator !=(OVector2 a, OVector2 b)
            {
                return !(a == b);
            }

            public override string ToString()
            {
                return String.Format("X:{0}; Y:{1}", x, y);
            }
        }

        /// <summary>
        ///     3-D Vector.
        /// </summary>
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

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                return this == (OVector3)obj;
            }

            public override int GetHashCode()
            {
                return x.GetHashCode() ^
                    y.GetHashCode() ^
                    z.GetHashCode();
            }

            public static bool operator ==(OVector3 a, OVector3 b)
            {
                return a.x == b.x &&
                    a.y == b.y &&
                    a.z == b.z;
            }

            public static bool operator !=(OVector3 a, OVector3 b)
            {
                return !(a == b);
            }

            public override string ToString()
            {
                return String.Format("X:{0}; Y:{1}; Z:{2}", x, y, z);
            }
        }

        /// <summary>
        ///     4-D Vector.
        /// </summary>
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

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                return this == (OVector4)obj;
            }

            public override int GetHashCode()
            {
                return x.GetHashCode() ^
                    y.GetHashCode() ^
                    z.GetHashCode() ^
                    w.GetHashCode();
            }

            public static bool operator ==(OVector4 a, OVector4 b)
            {
                return a.x == b.x &&
                    a.y == b.y &&
                    a.z == b.z &&
                    a.w == b.w;
            }

            public static bool operator !=(OVector4 a, OVector4 b)
            {
                return !(a == b);
            }

            public override string ToString()
            {
                return String.Format("X:{0}; Y:{1}; Z:{2}; W:{3}", x, y, z, w);
            }
        }

        /// <summary>
        ///     Vertex structure, used to specify data of the various attributes of a vertice on a mesh.
        /// </summary>
        public class OVertex : IEquatable<OVertex>
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
            public OVertex(OVertex vertex)
            {
                node = new List<int>();
                weight = new List<float>();

                node.AddRange(vertex.node);
                weight.AddRange(vertex.weight);

                position = new OVector3(vertex.position);
                normal = new OVector3(vertex.normal);
                tangent = new OVector3(vertex.tangent);
                texture0 = new OVector2(vertex.texture0);
                texture1 = new OVector2(vertex.texture1);
                texture2 = new OVector2(vertex.texture2);
            }

            /// <summary>
            ///     Creates a new Vertex.
            /// </summary>
            /// <param name="_position">The position of the Vertex on the 3-D space</param>
            /// <param name="_normal">The normal Vector</param>
            /// <param name="_texture0">The texture U/V coordinates</param>
            /// <param name="_color">The diffuse color</param>
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

            /// <summary>
            ///     Checks if two vertex are equal by comparing each element.
            /// </summary>
            /// <param name="vertex">Vertex to compare</param>
            /// <returns></returns>
            public bool Equals(OVertex vertex)
            {
                return position == vertex.position &&
                       normal == vertex.normal &&
                       tangent == vertex.tangent &&
                       texture0 == vertex.texture0 &&
                       texture1 == vertex.texture1 &&
                       texture2 == vertex.texture2 &&
                       node.SequenceEqual(vertex.node) &&
                       weight.SequenceEqual(vertex.weight) &&
                       diffuseColor == vertex.diffuseColor;
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

        /// <summary>
        ///     Matrix, used to transform vertices on a model.
        ///     Transformations includes rotation, translation and scaling.
        /// </summary>
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

            public float this[int a, int b]
            {
                get
                {
                    return matrix[a, b];
                }
                set
                {
                    matrix[a, b] = value;
                }
            }

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
                            sum += a[i, k] * b[k, j];
                        }
                        c[i, j] = sum;
                    }
                }

                return c;
            }

            /// <summary>
            ///     Gets the Inverse of the Matrix.
            /// </summary>
            /// <returns></returns>
            public OMatrix invert()
            {
                float[,] opMatrix = new float[4, 8];

                for (int N = 0; N < 4; N++)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        opMatrix[m, N] = matrix[m, N];
                    }
                }

                //Creates Identity at right side
                for (int N = 0; N < 4; N++)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        if (N == m)
                            opMatrix[m, N + 4] = 1;
                        else
                            opMatrix[m, N + 4] = 0;
                    }
                }
                
                for (int k = 0; k < 4; k++)
                {
                    if (opMatrix[k, k] == 0)
                    {
                        int row = 0;
                        for (int N = k; N < 4; N++) if (opMatrix[N, k] != 0) { row = N; break; }
                        for (int m = k; m < 8; m++)
                        {
                            float temp = opMatrix[k, m];
                            opMatrix[k, m] = opMatrix[row, m];
                            opMatrix[row, m] = temp;
                        }
                    }

                    float element = opMatrix[k, k];
                    for (int N = k; N < 8; N++) opMatrix[k, N] /= element;
                    for (int N = 0; N < 4; N++)
                    {
                        if (N == k && N == 3) break;
                        if (N == k && N < 3) N++;

                        if (opMatrix[N, k] != 0)
                        {
                            float multiplier = opMatrix[N, k] / opMatrix[k, k];
                            for (int m = k; m < 8; m++) opMatrix[N, m] -= opMatrix[k, m] * multiplier;
                        }
                    }
                }

                OMatrix output = new OMatrix();

                output.M11 = opMatrix[0, 4];
                output.M12 = opMatrix[0, 5];
                output.M13 = opMatrix[0, 6];
                output.M14 = opMatrix[0, 7];

                output.M21 = opMatrix[1, 4];
                output.M22 = opMatrix[1, 5];
                output.M23 = opMatrix[1, 6];
                output.M24 = opMatrix[1, 7];

                output.M31 = opMatrix[2, 4];
                output.M32 = opMatrix[2, 5];
                output.M33 = opMatrix[2, 6];
                output.M34 = opMatrix[2, 7];

                output.M41 = opMatrix[3, 4];
                output.M42 = opMatrix[3, 5];
                output.M43 = opMatrix[3, 6];
                output.M44 = opMatrix[3, 7];

                return output;
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

        /// <summary>
        ///     Oriented Bounding Box, can be used for collision testing.
        /// </summary>
        public class OOrientedBoundingBox
        {
            public string name;
            public OVector3 centerPosition;
            public OMatrix orientationMatrix;
            public OVector3 size;

            public OOrientedBoundingBox()
            {
                orientationMatrix = new OMatrix();
            }
        }

        /// <summary>
        ///     Translucency kind of a mesh.
        /// </summary>
        public enum OTranslucencyKind
        {
            opaque = 0,
            translucent = 1,
            subtractive = 2,
            additive = 3
        }

        /// <summary>
        ///     Skinning mode used on the Skeleton. Smooth skinning allows multiple bones (max 4) per vertex.
        ///     Other bones allows only one bone, and meshes are on their relative positions by default.
        /// </summary>
        public enum OSkinningMode
        {
            none = 0,
            smoothSkinning = 1,
            rigidSkinning = 2
        }

        /// <summary>
        ///     Mesh of a model. A model is usually composed of several meshes.
        ///     For example, a human character may have one mesh for the head, other for the body, other for members and so on...
        /// </summary>
        public class OModelObject
        {
            public List<OVertex> obj;
            public CustomVertex[] renderBuffer;
            public ushort materialId;
            public ushort renderPriority;
            public string name;
            public bool isVisible;
            public List<OOrientedBoundingBox> boundingBox;

            public bool hasNormal;
            public bool hasTangent;
            public bool hasColor;
            public bool hasNode;
            public bool hasWeight;
            public int texUVCount;

            public OModelObject()
            {
                obj = new List<OVertex>();
                boundingBox = new List<OOrientedBoundingBox>();
                isVisible = true;
            }

            /// <summary>
            ///     Adds a new Vertex to the Object.
            /// </summary>
            /// <param name="vertex">The Vertex</param>
            public void addVertex(OVertex vertex)
            {
                obj.Add(vertex);
            }

            /// <summary>
            ///     Adds a new Bounding Box to the Object.
            /// </summary>
            /// <param name="bBox">The Bounding Box</param>
            public void addBoundingBox(OOrientedBoundingBox bBox)
            {
                boundingBox.Add(bBox);
            }
        }

        /// <summary>
        ///     Billboard mode used on the Skeleton.
        /// </summary>
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

        /// <summary>
        ///     Bone of the skeleton. All values are relative to the parent bone.
        /// </summary>
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

            public List<OMetaData> userData;

            /// <summary>
            ///     Creates a new Bone.
            /// </summary>
            public OBone()
            {
                translation = new OVector3();
                rotation = new OVector3();
                scale = new OVector3();

                userData = new List<OMetaData>();
            }
        }

        /// <summary>
        ///     The several colors contained in one Material.
        /// </summary>
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

        /// <summary>
        ///     Culling mode of a mesh.
        ///     It is used to draw in clockwise or counter-clockwise direction.
        /// </summary>
        public enum OCullMode
        {
            never = 0,
            frontFace = 1,
            backFace = 2
        }

        /// <summary>
        ///     Rasterization stage parameters.
        /// </summary>
        public struct ORasterization
        {
            public OCullMode cullMode;
            public bool isPolygonOffsetEnabled;
            public float polygonOffsetUnit;
        }

        /// <summary>
        ///     Filtering mode used when the rendered texture is smaller than the normal texture.
        ///     ex: Object is too far from the Point of View.
        /// </summary>
        public enum OTextureMinFilter
        {
            nearestMipmapNearest = 1,
            nearestMipmapLinear = 2,
            linearMipmapNearest = 4,
            linearMipmapLinear = 5
        }

        /// <summary>
        ///     Filtering mode used when the rendered texture is bigger than the normal texture.
        ///     ex: Object is too close to the Point of View.
        /// </summary>
        public enum OTextureMagFilter
        {
            nearest = 0,
            linear = 1
        }

        /// <summary>
        ///     Wrapping mode when the UV is outside of 0...1 range.
        /// </summary>
        public enum OTextureWrap
        {
            clampToEdge = 0,
            clampToBorder = 1,
            repeat = 2,
            mirroredRepeat = 3
        }

        /// <summary>
        ///     Projection used on texture mapping.
        /// </summary>
        public enum OTextureProjection
        {
            uvMap = 0,
            cameraCubeMap = 1,
            cameraSphereMap = 2,
            projectionMap = 3,
            shadowMap = 4,
            shadowCubeMap = 5
        }

        /// <summary>
        ///     Type of a constant color.
        /// </summary>
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

        /// <summary>
        ///     Operation done on the TevStage on Fragment Shader.
        /// </summary>
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

        /// <summary>
        ///     Input color of the operation done on Fragment Shader.
        /// </summary>
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

        /// <summary>
        ///     Input components of the color on operation done on Fragment Shader.
        /// </summary>
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

        /// <summary>
        ///     Input components of the alpha color on operation done on Fragment Shader.
        /// </summary>
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

        /// <summary>
        ///     Parameters used to transform textures.
        /// </summary>
        public struct OTextureCoordinator
        {
            public OTextureProjection projection;
            public uint referenceCamera;
            public float scaleU, scaleV;
            public float rotate;
            public float translateU, translateV;
        }

        /// <summary>
        ///     Parameters used to map textures on the surface.
        /// </summary>
        public struct OTextureMapper
        {
            public OTextureMinFilter minFilter;
            public OTextureMagFilter magFilter;
            public OTextureWrap wrapU, wrapV;
            public uint minLOD;
            public float LODBias;
            public Color borderColor;
        }

        /// <summary>
        ///     Which texture is used as Bump Map?
        /// </summary>
        public enum OBumpTexture
        {
            texture0 = 0,
            texture1 = 1,
            texture2 = 2,
            texture3 = 3
        }

        /// <summary>
        ///     Bump mode, how the bump texture is used.
        /// </summary>
        public enum OBumpMode
        {
            notUsed = 0,
            asBump = 1,
            asTangent = 2
        }

        /// <summary>
        ///     Fragment Shader bump parameters.
        /// </summary>
        public struct OFragmentBump
        {
            public OBumpTexture texture;
            public OBumpMode mode;
            public bool isBumpRenormalize;
        }

        /// <summary>
        ///     Fresnel related configuration.
        /// </summary>
        public enum OFresnelConfig
        {
            none = 0,
            primary = 1,
            secondary = 2,
            primarySecondary = 3
        }

        /// <summary>
        ///     Value used as input of 1-D LUT on the Fragment Shader lighting.
        /// </summary>
        public enum OFragmentSamplerInput
        {
            halfNormalCosine = 0, //N·H
            halfViewCosine = 1, //V·H
            viewNormalCosine = 2, //N·V
            normalLightCosine = 3, //L·N
            spotLightCosine = 4, //-L·P
            phiCosine = 5 //cosϕ
        }

        /// <summary>
        ///     Multiplier for the Input value on Fragment Shader.
        /// </summary>
        public enum OFragmentSamplerScale
        {
            one = 0,
            two = 1,
            four = 2,
            eight = 3,
            quarter = 6,
            half = 7
        }

        /// <summary>
        ///     Fragment Shader lighting LUT parameters.
        /// </summary>
        public struct OFragmentSampler
        {
            public OFragmentSamplerInput input;
            public OFragmentSamplerScale scale;
            public string samplerName;
            public string tableName; //LookUp Table
        }

        /// <summary>
        ///     Fragment Shader lighting parameters.
        /// </summary>
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

        /// <summary>
        ///     Texture blending TevStages parameters.
        /// </summary>
        public class OTextureCombiner
        {
            public ushort rgbScale, alphaScale;
            public OConstantColor constantColor;
            public OCombineOperator combineRgb, combineAlpha;
            public OCombineSource[] rgbSource;
            public OCombineOperandRgb[] rgbOperand;
            public OCombineSource[] alphaSource;
            public OCombineOperandAlpha[] alphaOperand;

            public OTextureCombiner()
            {
                rgbSource = new OCombineSource[3];
                rgbOperand = new OCombineOperandRgb[3];
                alphaSource = new OCombineSource[3];
                alphaOperand = new OCombineOperandAlpha[3];
            }
        }

        /// <summary>
        ///     Alpha testing parameters.
        /// </summary>
        public struct OAlphaTest
        {
            public bool isTestEnabled;
            public OTestFunction testFunction;
            public uint testReference; 
        }

        /// <summary>
        ///     Fragment shader parameters.
        /// </summary>
        public class OFragmentShader
        {
            public uint layerConfig;
            public Color bufferColor;
            public OFragmentBump bump;
            public OFragmentLighting lighting;
            public OTextureCombiner[] textureCombiner;
            public OAlphaTest alphaTest;

            public OFragmentShader()
            {
                textureCombiner = new OTextureCombiner[6];
                for (int i = 0; i < 6; i++) textureCombiner[i] = new OTextureCombiner();
            }
        }

        /// <summary>
        ///     Comparator used on test functions.
        /// </summary>
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

        /// <summary>
        ///     Depth operation parameters.
        /// </summary>
        public struct ODepthOperation
        {
            public bool isTestEnabled;
            public OTestFunction testFunction;
            public bool isMaskEnabled;
        }

        /// <summary>
        ///     Blending mode used on Alpha Blending.
        /// </summary>
        public enum OBlendMode
        {
            logical = 0,
            notUsed = 2,
            blend = 3
        }

        /// <summary>
        ///     Binary logical operations.
        /// </summary>
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
        
        /// <summary>
        ///     Alpha blending functions.
        /// </summary>
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

        /// <summary>
        ///     Blending equations.
        /// </summary>
        public enum OBlendEquation
        {
            add = 0,
            subtract = 1,
            reverseSubtract = 2,
            min = 3,
            max = 4
        }

        /// <summary>
        ///     Blending operation parameters.
        /// </summary>
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
        
        /// <summary>
        ///     Stencil operation operation.
        /// </summary>
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

        /// <summary>
        ///     Stencil operation parameters.
        /// </summary>
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

        /// <summary>
        ///     Fragment operations (Stencil, blending, depth).
        /// </summary>
        public struct OFragmentOperation
        {
            public ODepthOperation depth;
            public OBlendOperation blend;
            public OStencilOperation stencil;
        }
        /// <summary>
        ///     The input format is the following: Id@Name.
        ///     The Id is used to choose data inside the group "Name", Id can be another name or an index.
        ///     The group may be a model, with Id being a Material, a Shader group with Id being the shader, and so on...
        /// </summary>
        public class OReference
        {
            public string id;
            public string name;

            /// <summary>
            ///     Creates a new Reference.
            /// </summary>
            /// <param name="_id">Reference Identification string</param>
            /// <param name="_name">Reference name</param>
            public OReference(string _id, string _name)
            {
                id = _id;
                name = _name;
            }

            /// <summary>
            ///     Creates a new Reference.
            /// </summary>
            /// <param name="_name">String of reference on Id@Name format</param>
            public OReference(string _name)
            {
                if (_name == null) return;
                if (_name.Contains("@"))
                {
                    string[] names = _name.Split(Convert.ToChar("@"));
                    id = names[0];
                    name = names[1];
                }
                else
                {
                    name = _name;
                }
            }

            /// <summary>
            ///     Creates a new Reference.
            /// </summary>
            public OReference()
            {
            }

            public override string ToString()
            {
                return id + "@" + name;
            }
        }

        /// <summary>
        ///     Material parameters.
        ///     It have references to textures, parameters used for blending said textures, and other Fragment Shader related data.
        /// </summary>
        public class OMaterial
        {
            public string name, name0, name1, name2;
            public OReference shaderReference, modelReference;
            public List<OMetaData> userData;

            public OMaterialColor materialColor;
            public ORasterization rasterization;
            public OTextureCoordinator[] textureCoordinator;
            public OTextureMapper[] textureMapper;
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
                userData = new List<OMetaData>();
                textureCoordinator = new OTextureCoordinator[3];
                textureMapper = new OTextureMapper[3];
                fragmentShader = new OFragmentShader();
            }
        }

        /// <summary>
        ///     Type of the value on Meta Data.
        /// </summary>
        public enum OMetaDataValueType
        {
            integer = 0,
            single = 1,
            utf8String = 2,
            utf16String = 3
        }

        /// <summary>
        ///     Meta Data.
        ///     If type is integer, each value in "values" should be casted to (int).
        ///     If is type is single, value should be casted to (float).
        ///     If is utf8String or utf16String, value should be casted to (string).
        ///     The string codification doesn't matter at this point, but may be useful on exporting.
        /// </summary>
        public class OMetaData
        {
            public string name;
            public OMetaDataValueType type;
            public List<Object> values;

            public OMetaData()
            {
                values = new List<Object>();
            }
        }

        /// <summary>
        ///     Culling mode of the Model.
        /// </summary>
        public enum OModelCullingMode
        {
            dynamic = 0,
            always = 1,
            never = 2
        }

        /// <summary>
        ///     Model data, such as the meshes, materials and skeleton.
        /// </summary>
        public class OModel
        {
            public string name;
            public uint layerId;
            public List<OModelObject> modelObject;
            public List<OBone> skeleton;
            public List<OMaterial> material;
            public List<OMetaData> userData;
            public OMatrix transform;
            public float height;

            public OModel()
            {
                modelObject = new List<OModelObject>();
                skeleton = new List<OBone>();
                material = new List<OMaterial>();
                userData = new List<OMetaData>();
                transform = new RenderBase.OMatrix();
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

        /// <summary>
        ///     Texture, contains the texture name and Bitmap image.
        /// </summary>
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

        /// <summary>
        ///     Sampler of a LookUp Table, contains the data.
        /// </summary>
        public class OLookUpTableSampler
        {
            public string name;
            public float[] table;

            public OLookUpTableSampler()
            {
                table = new float[256];
            }
        }

        /// <summary>
        ///     1-D LookUp table.
        /// </summary>
        public class OLookUpTable
        {
            public string name;
            public List<OLookUpTableSampler> sampler;

            public OLookUpTable()
            {
                sampler = new List<OLookUpTableSampler>();
            }
        }

        /// <summary>
        ///     Type of a light.
        /// </summary>
        public enum OLightType
        {
            directional = 0,
            point = 1,
            spot = 2
        }

        /// <summary>
        ///  Where and how the light data is used.
        /// </summary>
        public enum OLightUse
        {
            hemiSphere = 0,
            vertex = 1,
            fragment = 2,
            ambient = 3
        }

        /// <summary>
        ///     Represents a light source.
        /// </summary>
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
            public OLightUse lightUse;

            //Vertex
            public float distanceAttenuationConstant;
            public float distanceAttenuationLinear;
            public float distanceAttenuationQuadratic;
            public float spotExponent;
            public float spotCutoffAngle;

            //HemiSphere
            public Color groundColor;
            public Color skyColor;
            public float lerpFactor;

            public OFragmentSampler angleSampler;
            public OFragmentSampler distanceSampler;
        }

        /// <summary>
        ///     View mode of a camera.
        /// </summary>
        public enum OCameraView
        {
            aimTarget = 0,
            lookAtTarget = 1,
            rotate = 2
        }

        /// <summary>
        ///     Projection mode of a camera.
        /// </summary>
        public enum OCameraProjection
        {
            perspective = 0,
            orthogonal = 1
        }

        /// <summary>
        ///     Represents a camera on a scene.
        /// </summary>
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

        /// <summary>
        ///     Fog update method.
        /// </summary>
        public enum OFogUpdater
        {
            linear = 0,
            exponent = 1,
            exponentSquare = 2
        }

        /// <summary>
        ///     Represents a fog on a scene.
        /// </summary>
        public class OFog
        {
            public string name;

            public OVector3 transformScale;
            public OVector3 transformRotate;
            public OVector3 transformTranslate;

            public Color fogColor;

            public OFogUpdater fogUpdater;
            public float minFogDepth, maxFogDepth, fogDensity;

            public bool isZFlip;
            public bool isAttenuateDistance;
        }

        /// <summary>
        ///     Repeat method of a animation.
        /// </summary>
        public enum ORepeatMethod
        {
            none = 0,
            repeat = 1,
            mirror = 2,
            relativeRepeat = 3
        }

        /// <summary>
        ///     Value used on each Frame element of the animation
        /// </summary>
        public class OAnimationKeyFrame
        {
            public float frame;
            public float value;
            public float inSlope;
            public float outSlope;
            public bool bValue;

            /// <summary>
            ///     Creates a new Key Frame.
            ///     This Key Frame can be used on Hermite Interpolation.
            /// </summary>
            /// <param name="_value">The point value</param>
            /// <param name="_inSlope">The input slope</param>
            /// <param name="_outSlope">The output slope</param>
            /// <param name="_frame">The frame number</param>
            public OAnimationKeyFrame(float _value, float _inSlope, float _outSlope, float _frame)
            {
                value = _value;
                inSlope = _inSlope;
                outSlope = _outSlope;
                frame = _frame;
            }

            /// <summary>
            ///     Creates a new Key Frame.
            ///     This Key Frame can be used on Linear or Step interpolation.
            /// </summary>
            /// <param name="_value">The point value</param>
            /// <param name="_frame">The frame number</param>
            public OAnimationKeyFrame(float _value, float _frame)
            {
                value = _value;
                frame = _frame;
            }

            /// <summary>
            ///     Creates a new Key Frame.
            ///     This Key Frame can be used on Boolean values animation.
            /// </summary>
            /// <param name="_value">The point value</param>
            /// <param name="_frame">The frame number</param>
            public OAnimationKeyFrame(bool _value, float _frame)
            {
                bValue = _value;
                frame = _frame;
            }

            /// <summary>
            ///     Creates a new Key Frame.
            /// </summary>
            public OAnimationKeyFrame()
            {
            }

            public override string ToString()
            {
                return String.Format("Frame:{0}; Value (float):{1}; Value (boolean):{2}; InSlope:{3}; OutSlope:{4}", frame, value, bValue, inSlope, outSlope);
            }
        }

        /// <summary>
        ///     Interpolation mode of the animation.
        ///     Step = Jump from key frames, like the big pointer of a clock.
        ///     Linear =  Linear interpolation between values.
        ///     Hermite = Hermite interpolation between values, have two slope values too.
        /// </summary>
        public enum OInterpolationMode
        {
            step = 0,
            linear = 1,
            hermite = 2
        }

        /// <summary>
        ///     Key frame of an animation.
        /// </summary>
        public class OAnimationKeyFrameGroup
        {
            public List<OAnimationKeyFrame> keyFrames;
            public OInterpolationMode interpolation;
            public float startFrame, endFrame;
            public bool exists;
            public bool defaultValue;

            public ORepeatMethod preRepeat;
            public ORepeatMethod postRepeat;

            public OAnimationKeyFrameGroup()
            {
                keyFrames = new List<OAnimationKeyFrame>();
            }
        }

        /// <summary>
        ///     Normal frame of an animation.
        /// </summary>
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

        /// <summary>
        ///     Type of an animation segment.
        /// </summary>
        public enum OSegmentType
        {
            single = 0,
            vector2 = 2,
            vector3 = 3,
            transform = 4,
            rgbaColor = 5,
            integer = 6,
            transformQuaternion = 7,
            boolean = 8,
            transformMatrix = 9
        }

        /// <summary>
        ///     Type of the segment quantization.
        /// </summary>
        public enum OSegmentQuantization
        {
            hermite128 = 0,
            hermite64 = 1,
            hermite48 = 2,
            unifiedHermite96 = 3,
            unifiedHermite48 = 4,
            unifiedHermite32 = 5,
            stepLinear64 = 6,
            stepLinear32 = 7
        }

        /// <summary>
        ///     Bone of an Skeletal Animation.
        /// </summary>
        public class OSkeletalAnimationBone
        {
            public string name;
            public OAnimationKeyFrameGroup rotationX, rotationY, rotationZ;
            public OAnimationKeyFrameGroup translationX, translationY, translationZ;
            public bool rotationExists, translationExists;

            public OAnimationFrame rotationQuaternion;
            public OAnimationFrame translation;
            public bool isFrameFormat;

            public List<OMatrix> transform;
            public bool isFullBakedFormat;

            public OSkeletalAnimationBone()
            {
                rotationX = new OAnimationKeyFrameGroup();
                rotationY = new OAnimationKeyFrameGroup();
                rotationZ = new OAnimationKeyFrameGroup();
                translationX = new OAnimationKeyFrameGroup();
                translationY = new OAnimationKeyFrameGroup();
                translationZ = new OAnimationKeyFrameGroup();

                rotationQuaternion = new OAnimationFrame();
                translation = new OAnimationFrame();

                transform = new List<OMatrix>();
            }
        }

        /// <summary>
        ///     Animation loop mode.
        /// </summary>
        public enum OLoopMode
        {
            oneTime = 0,
            loop = 1
        }

        /// <summary>
        ///     Base animation class, all animations should inherit from this class.
        /// </summary>
        public class OAnimationBase
        {
            public virtual string name { get; set; }
            public virtual float frameSize { get; set; }
            public virtual OLoopMode loopMode { get; set; }
        }

        /// <summary>
        ///     Base class of list with animation.
        ///     It is used as a generic way to access all animation, casting each list element as appropriate.
        /// </summary>
        public class OAnimationListBase
        {
            public List<OAnimationBase> list;

            public OAnimationListBase()
            {
                list = new List<OAnimationBase>();
            }
        }

        /// <summary>
        ///     Represents a Skeletal Animation.
        /// </summary>
        public class OSkeletalAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public List<OSkeletalAnimationBone> bone;

            public List<OMetaData> userData;

            public OSkeletalAnimation()
            {
                bone = new List<OSkeletalAnimationBone>();

                userData = new List<OMetaData>();
            }
        }

        /// <summary>
        ///     Model data affected by the given Material Animation.
        /// </summary>
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

        /// <summary>
        ///     Data of the Material Animation.
        /// </summary>
        public class OMaterialAnimationData
        {
            public string name;
            public OMaterialAnimationType type;
            public List<OAnimationKeyFrameGroup> frameList;

            public OMaterialAnimationData()
            {
                frameList = new List<OAnimationKeyFrameGroup>();
            }
        }

        /// <summary>
        ///     Represents a Material Animation.
        /// </summary>
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

        /// <summary>
        ///     Data used on visibility animation.
        /// </summary>
        public class OVisibilityAnimationData
        {
            public string name;
            public OAnimationKeyFrameGroup visibilityList;

            public OVisibilityAnimationData()
            {
                visibilityList = new OAnimationKeyFrameGroup();
            }
        }

        /// <summary>
        ///     Represents visibility animation of a mesh.
        /// </summary>
        public class OVisibilityAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public List<OVisibilityAnimationData> data;

            public OVisibilityAnimation()
            {
                data = new List<OVisibilityAnimationData>();
            }
        }

        /// <summary>
        ///     Light data affected by the Light Animation.
        /// </summary>
        public enum OLightAnimationType
        {
            transform = 0x1c,
            ambient = 0x1d,
            diffuse = 0x1e,
            specular0 = 0x1f,
            specular1 = 0x20,
            direction = 0x21,
            distanceAttenuationStart = 0x22,
            distanceAttenuationEnd = 0x23,
            isLightEnabled = 0x24
        }

        /// <summary>
        ///     Data of the light animation.
        /// </summary>
        public class OLightAnimationData
        {
            public string name;
            public OLightAnimationType type;
            public List<OAnimationKeyFrameGroup> frameList;

            public OLightAnimationData()
            {
                frameList = new List<OAnimationKeyFrameGroup>();
            }
        }

        /// <summary>
        ///     Represents a Light Animation.
        /// </summary>
        public class OLightAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public OLightType lightType;
            public OLightUse lightUse;
            public List<OLightAnimationData> data;

            public OLightAnimation()
            {
                data = new List<OLightAnimationData>();
            }
        }

        /// <summary>
        ///     Camera data affected by the Camera Animation.
        /// </summary>
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
        
        /// <summary>
        ///     Camera animation data.
        /// </summary>
        public class OCameraAnimationData
        {
            public string name;
            public OCameraAnimationType type;
            public List<OAnimationKeyFrameGroup> frameList;

            public OCameraAnimationData()
            {
                frameList = new List<OAnimationKeyFrameGroup>();
            }
        }

        /// <summary>
        ///     Represents a Camera Animation.
        /// </summary>
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

        /// <summary>
        ///     Data used on Fog Animation.
        /// </summary>
        public class OFogAnimationData
        {
            public string name;
            public List<OAnimationKeyFrameGroup> colorList;

            public OFogAnimationData()
            {
                colorList = new List<OAnimationKeyFrameGroup>();
            }
        }

        /// <summary>
        ///     Represents a Fog Animation.
        /// </summary>
        public class OFogAnimation : OAnimationBase
        {
            public override string name { get; set; }
            public override float frameSize { get; set; }
            public override OLoopMode loopMode { get; set; }
            public List<OFogAnimationData> data;

            public OFogAnimation()
            {
                data = new List<OFogAnimationData>();
            }
        }

        /// <summary>
        ///     Reference of a scene element.
        /// </summary>
        public struct OSceneReference
        {
            public uint slotIndex;
            public string name;
        }

        /// <summary>
        ///     Represents a Scene Environment.
        ///     It references other data used to compose a scene.
        /// </summary>
        public class OScene
        {
            public string name;
            public List<OSceneReference> cameras;
            public List<OSceneReference> lights;
            public List<OSceneReference> fogs;

            public OScene()
            {
                cameras = new List<OSceneReference>();
                lights = new List<OSceneReference>();
                fogs = new List<OSceneReference>();
            }
        }

        /// <summary>
        ///     Model Group, contains everything related to 3-D rendering.
        /// </summary>
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
            public OAnimationListBase visibilityAnimation;
            public OAnimationListBase lightAnimation;
            public OAnimationListBase cameraAnimation;
            public OAnimationListBase fogAnimation;
            public List<OScene> scene;
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
                visibilityAnimation = new OAnimationListBase();
                lightAnimation = new OAnimationListBase();
                cameraAnimation = new OAnimationListBase();
                fogAnimation = new OAnimationListBase();
                scene = new List<OScene>();
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
            public void addSkeletalAnimaton(OSkeletalAnimation _skeletalAnimation)
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
            ///     Adds a new Visibility Animation.
            /// </summary>
            /// <param name="_visibilityAnimation">The Visibility Animation</param>
            public void addVisibilityAnimation(OVisibilityAnimation _visibilityAnimation)
            {
                visibilityAnimation.list.Add(_visibilityAnimation);
            }

            /// <summary>
            ///     Adds a new Light Animation.
            /// </summary>
            /// <param name="_lightAnimation">The Light Animation</param>
            public void addLightAnimation(OLightAnimation _lightAnimation)
            {
                lightAnimation.list.Add(_lightAnimation);
            }

            /// <summary>
            ///     Adds a new Camera Animation.
            /// </summary>
            /// <param name="_cameraAnimation">The Camera Animation</param>
            public void addCameraAnimation(OCameraAnimation _cameraAnimation)
            {
                cameraAnimation.list.Add(_cameraAnimation);
            }

            /// <summary>
            ///     Adds a new Fog Animation.
            /// </summary>
            /// <param name="_fogAnimation">The Fog Animation</param>
            public void addFogAnimation(OFogAnimation _fogAnimation)
            {
                fogAnimation.list.Add(_fogAnimation);
            }

            /// <summary>
            ///     Adds a new Scene Environment.
            /// </summary>
            /// <param name="_scene">The Scene Environment</param>
            public void addScene(OScene _scene)
            {
                scene.Add(_scene);
            }
        }
    }
}
