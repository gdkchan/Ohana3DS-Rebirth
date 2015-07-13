//Ohana3DS Source Model Importer/Exporter by gdkchan

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.Ohana.GenericFormats
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
                                    newBone.rotation.x = AnimationHelper.getKey(b.rotationX, AnimationHelper.getFrame(b.rotationX, frame));
                                    newBone.rotation.y = AnimationHelper.getKey(b.rotationY, AnimationHelper.getFrame(b.rotationY, frame));
                                    newBone.rotation.z = AnimationHelper.getKey(b.rotationZ, AnimationHelper.getFrame(b.rotationZ, frame));
                                }

                                if (b.translationExists)
                                {
                                    newBone.translation.x = AnimationHelper.getKey(b.translationX, AnimationHelper.getFrame(b.translationX, frame));
                                    newBone.translation.y = AnimationHelper.getKey(b.translationY, AnimationHelper.getFrame(b.translationY, frame));
                                    newBone.translation.z = AnimationHelper.getKey(b.translationZ, AnimationHelper.getFrame(b.translationZ, frame));
                                    
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
                        line += " " + getString(-vertex.texture0.y);

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
    }
}
