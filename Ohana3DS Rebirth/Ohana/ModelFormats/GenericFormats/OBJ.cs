using System;
using System.Text;
using System.Globalization;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats.GenericFormats
{
    class OBJ
    {
        public static void export(RenderBase.OModelGroup model, string fileName, int modelIndex)
        {
            StringBuilder output = new StringBuilder();

            RenderBase.OModel mdl = model.model[modelIndex];

            int faceIndexBase = 1;
            for (int objIndex = 0; objIndex < mdl.modelObject.Count; objIndex++)
            {
                output.AppendLine("g " + mdl.modelObject[objIndex].name);
                output.AppendLine(null);

                output.AppendLine("usemtl " + mdl.material[mdl.modelObject[objIndex].materialId].name0 + ".png");
                output.AppendLine(null);

                MeshUtils.optimizedMesh obj = MeshUtils.optimizeMesh(mdl.modelObject[objIndex]);
                foreach (RenderBase.OVertex vertex in obj.vertices)
                {
                    output.AppendLine("v " + getString(vertex.position.x) + " " + getString(vertex.position.y) + " " + getString(vertex.position.z));
                    output.AppendLine("vn " + getString(vertex.normal.x) + " " + getString(vertex.normal.y) + " " + getString(vertex.normal.z));
                    output.AppendLine("vt " + getString(vertex.texture0.x) + " " + getString(vertex.texture0.y));
                }
                output.AppendLine(null);

                for (int i = 0; i < obj.indices.Count; i += 3)
                {
                    output.AppendLine(String.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}",
                        faceIndexBase + obj.indices[i],
                        faceIndexBase + obj.indices[i + 1],
                        faceIndexBase + obj.indices[i + 2]));
                }
                faceIndexBase += obj.vertices.Count;
                output.AppendLine(null);
            }

            File.WriteAllText(fileName, output.ToString());
        }

        /// <summary>
        ///     Transforms a Float into a String that will always have "." into decimal places,
        ///     even if the region uses ",".
        /// </summary>
        /// <param name="value">The Float value</param>
        /// <returns></returns>
        private static string getString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
