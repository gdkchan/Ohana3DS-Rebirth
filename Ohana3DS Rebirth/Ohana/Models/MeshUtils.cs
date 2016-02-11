using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models
{
    class MeshUtils
    {
        /// <summary>
        ///     Calculates the minimun and maximum vector values for a Model.
        /// </summary>
        /// <param name="mdl">The target model</param>
        /// <param name="vertex">The current mesh vertex</param>
        public static void calculateBounds(RenderBase.OModel mdl, RenderBase.OVertex vertex)
        {
            if (vertex.position.x < mdl.minVector.x) mdl.minVector.x = vertex.position.x;
            if (vertex.position.x > mdl.maxVector.x) mdl.maxVector.x = vertex.position.x;
            if (vertex.position.y < mdl.minVector.y) mdl.minVector.y = vertex.position.y;
            if (vertex.position.y > mdl.maxVector.y) mdl.maxVector.y = vertex.position.y;
            if (vertex.position.z < mdl.minVector.z) mdl.minVector.z = vertex.position.z;
            if (vertex.position.z > mdl.maxVector.z) mdl.maxVector.z = vertex.position.z;
        }

        /// <summary>
        ///     Reads a Color from the Data.
        /// </summary>
        /// <param name="input">CGFX reader</param>
        /// <returns></returns>
        public static Color getColor(BinaryReader input)
        {
            byte r = input.ReadByte();
            byte g = input.ReadByte();
            byte b = input.ReadByte();
            byte a = input.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        ///     Reads a Color stored in Float format from the Data.
        /// </summary>
        /// <param name="input">CGFX reader</param>
        /// <returns></returns>
        public static Color getColorFloat(BinaryReader input)
        {
            byte r = (byte)(input.ReadSingle() * 0xff);
            byte g = (byte)(input.ReadSingle() * 0xff);
            byte b = (byte)(input.ReadSingle() * 0xff);
            byte a = (byte)(input.ReadSingle() * 0xff);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        ///     Clamps a Float value between 0 and 255 and return as Byte.
        /// </summary>
        /// <param name="value">The float value</param>
        /// <returns></returns>
        public static byte saturate(float value)
        {
            if (value > 0xff) return 0xff;
            if (value < 0) return 0;
            return (byte)value;
        }

        const uint optimizerLookBack = 32;

        public class optimizedMesh
        {
            public List<RenderBase.OVertex> vertices = new List<RenderBase.OVertex>();
            public List<uint> indices = new List<uint>();

            public bool hasNormal;
            public bool hasTangent;
            public bool hasColor;
            public bool hasNode;
            public bool hasWeight;
            public int texUVCount;
        }

        /// <summary>
        ///     Creates a Index Buffer for a Mesh, trying to repeat as less Vertices as possible.
        /// </summary>
        /// <param name="mesh">The Mesh that should be optimized</param>
        /// <returns></returns>
        public static optimizedMesh optimizeMesh(RenderBase.OMesh mesh)
        {
            optimizedMesh output = new optimizedMesh();

            output.hasNormal = mesh.hasNormal;
            output.hasTangent = mesh.hasTangent;
            output.hasColor = mesh.hasColor;
            output.hasNode = mesh.hasNode;
            output.hasWeight = mesh.hasWeight;
            output.texUVCount = mesh.texUVCount;

            for (int i = 0; i < mesh.vertices.Count; i++)
            {
                bool found = false;
                for (int j = 1; j <= optimizerLookBack; j++)
                {
                    int p = output.vertices.Count - j;
                    if (p < 0 || p >= output.vertices.Count) break;
                    if (output.vertices[p].Equals(mesh.vertices[i]))
                    {
                        output.indices.Add((uint)p);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    output.vertices.Add(mesh.vertices[i]);
                    output.indices.Add((uint)(output.vertices.Count - 1));
                }
            }

            return output;
        }
    }
}
