//Ohana3DS Source Model Importer/Exporter by gdkchan

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// </summary>
        /// <param name="model">The Model that will be exported</param>
        /// <param name="fileName">The output File Name</param>
        public static void export(RenderBase.OModelGroup model, string fileName)
        {
            RenderBase.OModel mdl = model.model[0]; //SMD only support one model
            StringBuilder output = new StringBuilder();

            output.AppendLine("version 1");
            output.AppendLine("nodes");
            for (int i = 0; i < mdl.skeleton.Count; i++)
            {
                output.AppendLine(i.ToString() + " \"" + mdl.skeleton[i].name + "\" " + mdl.skeleton[i].parentId.ToString());
            }
            output.AppendLine("end");
            output.AppendLine("skeleton");
            output.AppendLine("time 0");
            int index = 0;
            RenderBase.OVector3[] skl = new RenderBase.OVector3[mdl.skeleton.Count];
            foreach (RenderBase.OBone bone in mdl.skeleton)
            {
                string line = index.ToString();
                line += " " + getString(bone.translation.x * bone.scale.x);
                line += " " + getString(bone.translation.y * bone.scale.y);
                line += " " + getString(bone.translation.z * bone.scale.z);
                line += " " +  getString(bone.rotation.x);
                line += " " +  getString(bone.rotation.y);
                line += " " +  getString(bone.rotation.z);
                output.AppendLine(line);
                index++;

                skl[index - 1] = new RenderBase.OVector3();
                getSkl(mdl.skeleton, index - 1, ref skl[index - 1]);
            }
            output.AppendLine("end");
            output.AppendLine("triangles");
            uint triangleCount = 0;
            foreach (RenderBase.OModelObject obj in mdl.modelObject)
            {
                string textureName = mdl.material[obj.materialId].name0;
                if (textureName == null) textureName = "dummy";

                foreach (RenderBase.OVertex vertex in obj.obj)
                {
                    if (triangleCount == 0) output.AppendLine(textureName);

                    string line = mdl.skeleton.Count.ToString();
                    float x = vertex.position.x;
                    float y = vertex.position.y;
                    float z = vertex.position.z;
                    for (int b = 0; b < vertex.node.Count; b++)
                    {
                        /*x += skl[vertex.node[b]].x;
                        y += skl[vertex.node[b]].y;
                        z += skl[vertex.node[b]].z;*/
                    }
                    line += " " + getString(x);
                    line += " " + getString(y);
                    line += " " + getString(z);
                    line += " " + getString(vertex.normal.x);
                    line += " " + getString(vertex.normal.y);
                    line += " " + getString(vertex.normal.z);
                    line += " " + getString(vertex.texture0.x);
                    line += " " + getString(vertex.texture0.y);

                    line += " " + vertex.node.Count.ToString();
                    for (int i = 0; i < vertex.node.Count; i++)
                    {
                        line += " " + vertex.node[i].ToString();
                        if (i < vertex.weight.Count)
                            line += " " + getString(vertex.weight[i]);
                        else
                            line += " 1";
                    }

                    output.AppendLine(line);
                    triangleCount = (triangleCount + 1) % 3;
                }
            }
            output.AppendLine("end");

            File.WriteAllText(fileName, output.ToString());
        }

        private static string getString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static void getSkl(List<RenderBase.OBone> skeleton, int index, ref RenderBase.OVector3 target)
        {
            target.x += skeleton[index].translation.x;
            target.y += skeleton[index].translation.y;
            target.z += skeleton[index].translation.z;

            if (skeleton[index].parentId > -1) getSkl(skeleton, skeleton[index].parentId, ref target);
        }
    }
}
