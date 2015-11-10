//Dragon Quest VII Container parser made by gdkchan for Ohana3DS
using System.Collections.Generic;
using System.IO;

using Ohana3DS_Rebirth.Ohana.ModelFormats;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class DQVIIPack
    {
        private struct sectionEntry
        {
            public uint offset;
            public uint length;
        }

        private struct node
        {
            public string name;
            public int parentId;
            public RenderBase.OMatrix transform;
        }

        /// <summary>
        ///     Reads the Model PACKage from Dragon Quest VII.
        /// </summary>
        /// <param name="fileName">The File Name where the data is located</param>
        /// <returns></returns>
        public static GenericContainer.OContainer load(string fileName)
        {
            return load(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        ///     Reads the Model PACKage from Dragon Quest VII.
        /// </summary>
        /// <param name="data">Stream of the data</param>
        /// <returns></returns>
        public static GenericContainer.OContainer load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);
            GenericContainer.OContainer output = new GenericContainer.OContainer();

            List<sectionEntry> mainSection = getSection(input);

            //World nodes section
            data.Seek(mainSection[0].offset, SeekOrigin.Begin);
            List<node> nodes = new List<node>();
            List<sectionEntry> worldNodesSection = getSection(input);
            foreach (sectionEntry entry in worldNodesSection)
            {
                data.Seek(entry.offset, SeekOrigin.Begin);

                node n = new node();

                //Geometry node
                input.ReadUInt32(); //GNOD magic number
                input.ReadUInt32();
                input.ReadUInt32();
                n.parentId = input.ReadInt32();
                n.name = IOUtils.readString(input, (uint)data.Position);

                data.Seek(entry.offset + 0x20, SeekOrigin.Begin);
                n.transform = new RenderBase.OMatrix();
                RenderBase.OVector4 t = new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                RenderBase.OVector4 r = new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                RenderBase.OVector4 s = new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                n.transform *= RenderBase.OMatrix.scale(new RenderBase.OVector3(s.x, s.y, s.z));
                n.transform *= RenderBase.OMatrix.rotateX(r.x);
                n.transform *= RenderBase.OMatrix.rotateY(r.y);
                n.transform *= RenderBase.OMatrix.rotateZ(r.z);
                n.transform *= RenderBase.OMatrix.translate(new RenderBase.OVector3(t.x, t.y, t.z));

                nodes.Add(n);
            }

            RenderBase.OMatrix[] nodesTransform = new RenderBase.OMatrix[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                RenderBase.OMatrix transform = new RenderBase.OMatrix();
                transformNode(nodes, i, ref transform);
                nodesTransform[i] = transform;
            }

            //Models section
            data.Seek(mainSection[1].offset, SeekOrigin.Begin);
            List<sectionEntry> modelsSection = getSection(input);
            foreach (sectionEntry entry in modelsSection)
            {
                data.Seek(entry.offset, SeekOrigin.Begin);
                

                //Field Data section
                /*
                 * Usually have 3 entries.
                 * 1st entry: Model CGFX
                 * 2nd entry: Unknow CGFX, possibly animations
                 * 3rd entry: Another FieldData section, possibly child object
                 */

                List<sectionEntry> fieldDataSection = getSection(input);
                data.Seek(fieldDataSection[0].offset, SeekOrigin.Begin);
                uint length = fieldDataSection[0].length;
                while ((length & 0x7f) != 0) length++; //Align
                byte[] buffer = new byte[length];
                input.Read(buffer, 0, buffer.Length);

                GenericContainer.OContainerEntry file = new GenericContainer.OContainerEntry();
                file.name = CGFX.getName(new MemoryStream(buffer)) + ".bcmdl";
                file.data = buffer;

                output.content.Add(file);
            }

            //FILE section
            data.Seek(mainSection[2].offset, SeekOrigin.Begin);
            //TODO

            //Collision section
            data.Seek(mainSection[3].offset, SeekOrigin.Begin);
            //TODO

            //PARM(???) section
            data.Seek(mainSection[4].offset, SeekOrigin.Begin);
            //TODO

            //Textures CGFX
            data.Seek(mainSection[5].offset, SeekOrigin.Begin);
            byte[] texBuffer = new byte[mainSection[5].length];
            input.Read(texBuffer, 0, texBuffer.Length);

            GenericContainer.OContainerEntry texFile = new GenericContainer.OContainerEntry();
            texFile.name = "textures.bctex";
            texFile.data = texBuffer;

            output.content.Add(texFile);

            data.Close();

            return output;
        }

        /// <summary>
        ///     Gets a generic section from the file.
        /// </summary>
        /// <param name="input">BinaryReader of the data</param>
        /// <returns></returns>
        private static List<sectionEntry> getSection(BinaryReader input)
        {
            uint baseAddress = (uint)input.BaseStream.Position;
            input.BaseStream.Seek(0x10, SeekOrigin.Current); //Section magic number (padded with 0x0)
            uint addressCount = input.ReadUInt32();
            baseAddress += input.ReadUInt32();
            uint addressSectionLength = input.ReadUInt32();
            input.ReadUInt32(); //Padding

            List<sectionEntry> sections = new List<sectionEntry>();
            for (int i = 0; i < addressCount; i++)
            {
                sectionEntry entry = new sectionEntry();

                entry.offset = input.ReadUInt32() + baseAddress;
                entry.length = input.ReadUInt32();

                sections.Add(entry);
            }

            return sections;
        }

        /// <summary>
        ///     Transforms a Node from relative to absolute positions.
        /// </summary>
        /// <param name="nodes">A list with all nodes</param>
        /// <param name="index">Index of the node to convert</param>
        /// <param name="target">Target matrix to save node transformation</param>
        private static void transformNode(List<node> nodes, int index, ref RenderBase.OMatrix target)
        {
            target *= nodes[index].transform;
            if (nodes[index].parentId > -1) transformNode(nodes, nodes[index].parentId, ref target);
        }
    }
}
