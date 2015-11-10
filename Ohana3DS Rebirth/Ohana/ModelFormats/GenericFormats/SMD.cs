//Ohana3DS Source Model Importer/Exporter by gdkchan

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats.GenericFormats
{
    class SMD
    {
        /// <summary>
        ///     Exports a Model to the Source Model format.
        ///     Note: SMD model specification doesnt support Model and Skeletal Animation on the same SMD.
        ///     See: https://developer.valvesoftware.com/wiki/Studiomdl_Data for more information.
        /// </summary>
        /// <param name="model">The Model that will be exported</param>
        /// <param name="fileName">The output File Name</param>
        /// <param name="modelIndex">Index of the model to be exported</param>
        /// <param name="skeletalAnimationIndex">(Optional) Index of the skeletal animation</param>
        public static void export(RenderBase.OModelGroup model, string fileName, int modelIndex, int skeletalAnimationIndex = -1)
        {
            RenderBase.OModel mdl = model.model[modelIndex];
            StringBuilder output = new StringBuilder();

            output.AppendLine("version 1");
            output.AppendLine("nodes");
            for (int i = 0; i < mdl.skeleton.Count; i++)
            {
                output.AppendLine(i + " \"" + mdl.skeleton[i].name + "\" " + mdl.skeleton[i].parentId);
            }
            output.AppendLine("end");
            output.AppendLine("skeleton");
            if (skeletalAnimationIndex == -1)
            {
                output.AppendLine("time 0");
                int index = 0;
                foreach (RenderBase.OBone bone in mdl.skeleton)
                {
                    string line = index.ToString();
                    line += " " + getString(bone.translation.x);
                    line += " " + getString(bone.translation.y);
                    line += " " + getString(bone.translation.z);
                    line += " " + getString(bone.rotation.x);
                    line += " " + getString(bone.rotation.y);
                    line += " " + getString(bone.rotation.z);
                    output.AppendLine(line);
                    index++;
                }
            }
            else
            {
                bool error = false;
                for (float frame = 0; frame < model.skeletalAnimation.list[skeletalAnimationIndex].frameSize; frame += 1)
                {
                    output.AppendLine("time " + ((int)frame).ToString());
                    for (int index = 0; index < mdl.skeleton.Count; index++)
                    {
                        RenderBase.OBone newBone = new RenderBase.OBone();
                        newBone.parentId = mdl.skeleton[index].parentId;
                        newBone.rotation = new RenderBase.OVector3(mdl.skeleton[index].rotation);
                        newBone.translation = new RenderBase.OVector3(mdl.skeleton[index].translation);
                        foreach (RenderBase.OSkeletalAnimationBone b in ((RenderBase.OSkeletalAnimation)model.skeletalAnimation.list[skeletalAnimationIndex]).bone)
                        {
                            if (b.isFrameFormat || b.isFullBakedFormat) error = true;
                            if (b.name == mdl.skeleton[index].name && !b.isFrameFormat && !b.isFullBakedFormat)
                            {
                                if (b.rotationExists)
                                {
                                    newBone.rotation.x = AnimationUtils.getKey(b.rotationX, frame);
                                    newBone.rotation.y = AnimationUtils.getKey(b.rotationY, frame);
                                    newBone.rotation.z = AnimationUtils.getKey(b.rotationZ, frame);
                                }

                                if (b.translationExists)
                                {
                                    newBone.translation.x = AnimationUtils.getKey(b.translationX, frame);
                                    newBone.translation.y = AnimationUtils.getKey(b.translationY, frame);
                                    newBone.translation.z = AnimationUtils.getKey(b.translationZ, frame);
                                    
                                    newBone.translation.x *= mdl.skeleton[index].absoluteScale.x;
                                    newBone.translation.y *= mdl.skeleton[index].absoluteScale.y;
                                    newBone.translation.z *= mdl.skeleton[index].absoluteScale.z;
                                }

                                break;
                            }
                        }

                        string line = index.ToString();
                        line += " " + getString(newBone.translation.x);
                        line += " " + getString(newBone.translation.y);
                        line += " " + getString(newBone.translation.z);
                        line += " " + getString(newBone.rotation.x);
                        line += " " + getString(newBone.rotation.y);
                        line += " " + getString(newBone.rotation.z);
                        output.AppendLine(line);
                    }
                }

                if (error) MessageBox.Show("One or more bones uses an animation type unsupported by Source Model!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            output.AppendLine("end");

            if (skeletalAnimationIndex == -1)
            {
                output.AppendLine("triangles");
                uint triangleCount = 0;
                int objectIndex = 0;
                foreach (RenderBase.OModelObject obj in mdl.modelObject)
                {
                    string textureName = mdl.material[obj.materialId].name0 ?? "material_" + objectIndex.ToString();

                    foreach (RenderBase.OVertex vertex in obj.obj)
                    {
                        if (triangleCount == 0) output.AppendLine(textureName);

                        string line = mdl.skeleton.Count.ToString();

                        line += " " + getString(vertex.position.x);
                        line += " " + getString(vertex.position.y);
                        line += " " + getString(vertex.position.z);
                        line += " " + getString(vertex.normal.x);
                        line += " " + getString(vertex.normal.y);
                        line += " " + getString(vertex.normal.z);
                        line += " " + getString(vertex.texture0.x);
                        line += " " + getString(vertex.texture0.y);

                        line += " " + vertex.node.Count;
                        for (int i = 0; i < vertex.node.Count; i++)
                        {
                            line += " " + vertex.node[i];
                            if (i < vertex.weight.Count)
                                line += " " + getString(vertex.weight[i]);
                            else
                                line += " 1";
                        }

                        output.AppendLine(line);
                        triangleCount = (triangleCount + 1) % 3;
                    }

                    objectIndex++;
                }
                output.AppendLine("end");
            }

            File.WriteAllText(fileName, output.ToString());
        }

        private static string getString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private struct smdNode
        {
            public int index;
            public string name;
            public int parentId;
        }

        /// <summary>
        ///     Imports a Source Model from file.
        /// </summary>
        /// <param name="fileName">The complete file name</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup import(string fileName)
        {
            StreamReader reader = File.OpenText(fileName);

            RenderBase.OModelGroup model = new RenderBase.OModelGroup();
            RenderBase.OModel mdl = new RenderBase.OModel();
            mdl.name = Path.GetFileNameWithoutExtension(fileName);
            mdl.transform = new RenderBase.OMatrix();

            List<smdNode> nodeList = new List<smdNode>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string line = readLine(reader);
                string[] parameters = Regex.Split(line, "\\s+");

                switch (parameters[0])
                {
                    case "version":
                        if (parameters.Length == 1)
                        {
                            MessageBox.Show("Corrupted SMD file! The version isn't specified!", "SMD Importer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                        else if (parameters[1] != "1")
                        {
                            MessageBox.Show("Unknow SMD version!", "SMD Importer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                        break;
                    case "nodes":
                        line = readLine(reader);
                        
                        parameters = Regex.Split(line, "\\s+");

                        while (parameters[0] != "end")
                        {
                            if (parameters.Length == 3)
                            {
                                smdNode node = new smdNode();
                                node.index = int.Parse(parameters[0]);
                                int nameStart = parameters[1].IndexOf("\"") + 1;
                                node.name = parameters[1].Substring(nameStart, parameters[1].LastIndexOf("\"") - nameStart);
                                node.parentId = int.Parse(parameters[2]);
                                nodeList.Add(node);
                            }

                            line = readLine(reader);
                            parameters = Regex.Split(line, "\\s+");
                        }

                        break;
                    case "skeleton":
                        bool isReference = false;
                        int timeIndex = -1;

                        line = readLine(reader);
                        parameters = Regex.Split(line, "\\s+");

                        RenderBase.OSkeletalAnimationBone[] boneArray = null;

                        while (parameters[0] != "end")
                        {
                            if (parameters[0] == "time")
                            {
                                timeIndex = int.Parse(parameters[1]);
                            }
                            else
                            {
                                if (timeIndex > -1 && parameters.Length == 7)
                                {
                                    int nodeIndex = int.Parse(parameters[0]);
                                    float translationX = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                                    float translationY = float.Parse(parameters[2], CultureInfo.InvariantCulture);
                                    float translationZ = float.Parse(parameters[3], CultureInfo.InvariantCulture);
                                    float rotationX = float.Parse(parameters[4], CultureInfo.InvariantCulture);
                                    float rotationY = float.Parse(parameters[5], CultureInfo.InvariantCulture);
                                    float rotationZ = float.Parse(parameters[6], CultureInfo.InvariantCulture);

                                    if (timeIndex == 0)
                                    {
                                        RenderBase.OBone bone = new RenderBase.OBone();
                                        foreach (smdNode node in nodeList) if (node.index == nodeIndex) { bone.name = node.name; bone.parentId = (short)node.parentId; }

                                        bone.translation = new RenderBase.OVector3(translationX, translationY, translationZ);
                                        bone.rotation = new RenderBase.OVector3(rotationX, rotationY, rotationZ);
                                        bone.scale = new RenderBase.OVector3(1, 1, 1);
                                        bone.absoluteScale = new RenderBase.OVector3(bone.scale);

                                        mdl.addBone(bone);
                                    }
                                    else
                                    {
                                        if (!isReference)
                                        {
                                            boneArray = new RenderBase.OSkeletalAnimationBone[mdl.skeleton.Count];
                                            
                                            int index = 0;
                                            foreach (RenderBase.OBone b in mdl.skeleton)
                                            {
                                                RenderBase.OSkeletalAnimationBone bone = new RenderBase.OSkeletalAnimationBone();

                                                bone.name = b.name;
                                                bone.rotationExists = true;
                                                bone.translationExists = true;

                                                //Translation
                                                bone.translationX.interpolation = bone.translationY.interpolation = bone.translationZ.interpolation = RenderBase.OInterpolationMode.linear;
                                                bone.translationX.keyFrames.Add(new RenderBase.OInterpolationFloat(b.translation.x, 0));
                                                bone.translationY.keyFrames.Add(new RenderBase.OInterpolationFloat(b.translation.y, 0));
                                                bone.translationZ.keyFrames.Add(new RenderBase.OInterpolationFloat(b.translation.z, 0));

                                                //Rotation
                                                bone.rotationX.interpolation = bone.rotationY.interpolation = bone.rotationZ.interpolation = RenderBase.OInterpolationMode.linear;
                                                bone.rotationX.keyFrames.Add(new RenderBase.OInterpolationFloat(b.rotation.x, 0));
                                                bone.rotationY.keyFrames.Add(new RenderBase.OInterpolationFloat(b.rotation.y, 0));
                                                bone.rotationZ.keyFrames.Add(new RenderBase.OInterpolationFloat(b.rotation.z, 0));

                                                boneArray[index++] = bone;
                                            }

                                            isReference = true;
                                        }

                                        boneArray[nodeIndex].translationX.keyFrames.Add(new RenderBase.OInterpolationFloat(translationX, timeIndex));
                                        boneArray[nodeIndex].translationY.keyFrames.Add(new RenderBase.OInterpolationFloat(translationY, timeIndex));
                                        boneArray[nodeIndex].translationZ.keyFrames.Add(new RenderBase.OInterpolationFloat(translationZ, timeIndex));

                                        boneArray[nodeIndex].rotationX.keyFrames.Add(new RenderBase.OInterpolationFloat(rotationX, timeIndex));
                                        boneArray[nodeIndex].rotationY.keyFrames.Add(new RenderBase.OInterpolationFloat(rotationY, timeIndex));
                                        boneArray[nodeIndex].rotationZ.keyFrames.Add(new RenderBase.OInterpolationFloat(rotationZ, timeIndex));
                                    }
                                }
                            }

                            line = readLine(reader);
                            parameters = Regex.Split(line, "\\s+");
                        }

                        if (isReference)
                        {
                            RenderBase.OSkeletalAnimation anim = new RenderBase.OSkeletalAnimation();
                            anim.frameSize = timeIndex;
                            anim.name = Path.GetFileNameWithoutExtension(fileName);
                            for (int i = 0; i < boneArray.Length; i++)
                            {
                                boneArray[i].translationX.endFrame = timeIndex;
                                boneArray[i].translationY.endFrame = timeIndex;
                                boneArray[i].translationZ.endFrame = timeIndex;
                                boneArray[i].rotationX.endFrame = timeIndex;
                                boneArray[i].rotationY.endFrame = timeIndex;
                                boneArray[i].rotationZ.endFrame = timeIndex;
                            }
                            anim.bone.AddRange(boneArray);
                            model.skeletalAnimation.list.Add(anim);
                        }

                        break;
                    case "triangles":
                        line = readLine(reader);
                        parameters = Regex.Split(line, "\\s+");

                        RenderBase.OModelObject obj = new RenderBase.OModelObject();
                        List<RenderBase.CustomVertex> buffer = new List<RenderBase.CustomVertex>();
                        int materialId = 0;
                        string oldTexture = null;

                        obj.hasNormal = true;

                        int count = 0;
                        while (parameters[0] != "end")
                        {
                            if (count == 0)
                            {
                                string texture = parameters[0];

                                if (texture != oldTexture && oldTexture != null)
                                {
                                    mdl.addMaterial(getMaterial(oldTexture));
                                    obj.materialId = (ushort)materialId++;
                                    obj.renderBuffer = buffer.ToArray();
                                    buffer.Clear();
                                    mdl.addObject(obj);
                                    obj = new RenderBase.OModelObject();
                                }
                                oldTexture = texture;
                            }
                            else
                            {
                                if (parameters.Length >= 10)
                                {
                                    int parentBone;
                                    float x, y, z;
                                    float nx, ny, nz;
                                    float u, v;
                                    int joints;

                                    parentBone = int.Parse(parameters[0]);
                                    x = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                                    y = float.Parse(parameters[2], CultureInfo.InvariantCulture);
                                    z = float.Parse(parameters[3], CultureInfo.InvariantCulture);
                                    nx = float.Parse(parameters[4], CultureInfo.InvariantCulture);
                                    ny = float.Parse(parameters[5], CultureInfo.InvariantCulture);
                                    nz = float.Parse(parameters[6], CultureInfo.InvariantCulture);
                                    u = float.Parse(parameters[7], CultureInfo.InvariantCulture);
                                    v = float.Parse(parameters[8], CultureInfo.InvariantCulture);
                                    joints = int.Parse(parameters[9]);

                                    RenderBase.OVertex vertex = new RenderBase.OVertex();
                                    vertex.diffuseColor = 0xffffffff;

                                    int j = 10;
                                    for (int i = 0; i < joints; i++)
                                    {
                                        int joint = int.Parse(parameters[j++]);
                                        float weight = float.Parse(parameters[j++], CultureInfo.InvariantCulture);

                                        vertex.addNode(joint);
                                        vertex.addWeight(weight);

                                        obj.hasNode = true;
                                        obj.hasWeight = true;
                                    }

                                    vertex.position = new RenderBase.OVector3(x, y, z);
                                    vertex.normal = new RenderBase.OVector3(nx, ny, nz);
                                    vertex.texture0 = new RenderBase.OVector2(u, v);

                                    obj.addVertex(vertex);
                                    buffer.Add(RenderBase.convertVertex(vertex));
                                }
                            }

                            line = readLine(reader);
                            parameters = Regex.Split(line, "\\s+");

                            count = (count + 1) % 4;
                        }

                        //Add the last object
                        mdl.addMaterial(getMaterial(oldTexture));
                        obj.materialId = (ushort)materialId++;
                        obj.renderBuffer = buffer.ToArray();
                        buffer.Clear();
                        mdl.addObject(obj);

                        break;
                }
            }

            model.addModel(mdl);
            return model;
        }

        private static RenderBase.OMaterial getMaterial(string name)
        {
            RenderBase.OMaterial material = new RenderBase.OMaterial();
            material.name = material.name0 = name;
            material.fragmentOperation.depth.isTestEnabled = true;
            material.fragmentOperation.depth.testFunction = RenderBase.OTestFunction.lessOrEqual;
            material.fragmentOperation.depth.isMaskEnabled = true;
            return material;
        }

        private static string readLine(StreamReader reader)
        {
            return reader.ReadLine().Trim();
        }
    }
}
