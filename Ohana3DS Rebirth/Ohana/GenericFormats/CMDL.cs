using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ohana3DS_Rebirth.Ohana.GenericFormats
{
    class CMDL
    {
        /// <summary>
        ///     Exports a Model to the CMDL format.
        /// </summary>
        /// <param name="model">The Model that will be exported</param>
        /// <param name="fileName">The output File Name</param>
        /// <param name="modelIndex">Index of the model to be exported</param>
        /// <param name="skeletalAnimationIndex">(Optional) Index of the skeletal animation.</param>
        public static void export(RenderBase.OModelGroup model, string fileName, int modelIndex, int skeletalAnimationIndex = -1)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter xml = XmlWriter.Create(fileName, settings))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("NintendoWareIntermediateFile");
                xml.WriteStartElement("GraphicsContentCtr"); xml.WriteAttributeString("Version", "1.3.0"); xml.WriteAttributeString("Namespace", "");

                //Metadata
                xml.WriteStartElement("EditData");
                    xml.WriteStartElement("MetaData");
                        xml.WriteStartElement("Key");
                            xml.WriteString("MetaData");
                        xml.WriteEndElement();
                        xml.WriteStartElement("Create"); xml.WriteAttributeString("Author", Environment.UserName); xml.WriteAttributeString("Date", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")); xml.WriteAttributeString("Source", ""); xml.WriteAttributeString("FullPathOfSource", "");
                            xml.WriteStartElement("ToolDescriptions"); xml.WriteAttributeString("Name", "Ohana3DS"); xml.WriteAttributeString("Version", "1.0");
                            xml.WriteEndElement();
                        xml.WriteEndElement();
                    xml.WriteEndElement();
                    xml.WriteStartElement("ContentSummaryMetaData");
                        xml.WriteStartElement("Key");
                            xml.WriteString("ContentSummaries");
                        xml.WriteEndElement();
                        xml.WriteStartElement("Values");
                            xml.WriteStartElement("ContentSummary"); xml.WriteAttributeString("ContentTypeName", "GraphicsContent");
                                xml.WriteStartElement("ObjectSummaries");
                                    xml.WriteStartElement("ObjectSummary"); xml.WriteAttributeString("TypeName", "SkeletalModel"); xml.WriteAttributeString("Name", model.model[0].name);
                                        xml.WriteStartElement("Notes");
                                            //TODO
                                        xml.WriteEndElement();
                                    xml.WriteEndElement();
                                xml.WriteEndElement();
                            xml.WriteEndElement();
                        xml.WriteEndElement();
                    xml.WriteEndElement();
                xml.WriteEndElement();

                //Models
                xml.WriteStartElement("Models");
                    xml.WriteStartElement("SkeletalModel");
                        xml.WriteStartElement("EditData");

                            //Options
                            xml.WriteStartElement("ModelDccToolExportOption"); xml.WriteAttributeString("ExportStartFrame", "0"); xml.WriteAttributeString("Magnify", "1"); xml.WriteAttributeString("AdjustSkinning", "None"); xml.WriteAttributeString("MeshVisibilityMode", "BindByName");
                                xml.WriteStartElement("Key");
                                    xml.WriteString("ModelDccToolInfo");
                                xml.WriteEndElement();
                            xml.WriteEndElement();
                            xml.WriteStartElement("OptimizationLogArrayMetaData"); xml.WriteAttributeString("Size", "1");
                                xml.WriteStartElement("Key");
                                    xml.WriteString("OptimizationLogs");
                                xml.WriteEndElement();
                                xml.WriteStartElement("Values");
                                    xml.WriteStartElement("OptimizationLog"); xml.WriteAttributeString("Date", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")); xml.WriteAttributeString("EditorVersion", "1.0"); xml.WriteAttributeString("OptimizePrimitiveAverageCacheMissRatio", "2"); xml.WriteAttributeString("OptimizerIdentifier", "AlgorithmCombo");
                                        xml.WriteStartElement("Options");
                                            xml.WriteAttributeString("NodeCompressionMode", "None");
                                            xml.WriteAttributeString("IsMergeMeshOwnerBoneEnabled", "false");
                                            xml.WriteAttributeString("IsCombineMeshEnabled", "false");
                                            xml.WriteAttributeString("IsCompressMaterialEnabled", "false");
                                            xml.WriteAttributeString("IsOptimizePlygonPrimitiveEnabled", "true");
                                            xml.WriteAttributeString("IsConvertOneBoneSkeletalModelToModel", "false");
                                            xml.WriteAttributeString("IsDeleteUnusedVertexEnabled", "false");
                                            xml.WriteAttributeString("PositionQuantizeMode", "Float");
                                            xml.WriteAttributeString("NormalQuantizeMode", "Float");
                                            xml.WriteAttributeString("TextureQuantizeMode", "Float");
                                            xml.WriteAttributeString("GroupByIndexStream", "false");
                                            xml.WriteStartElement("OptimizePolygonPrimitiveLevel");
                                                xml.WriteString("0");
                                            xml.WriteEndElement();
                                        xml.WriteEndElement();
                                    xml.WriteEndElement();
                                xml.WriteEndElement();
                            xml.WriteEndElement();

                            //Anims Descriptions
                            xml.WriteStartElement("AnimationGroupDescriptions");
                                xml.WriteStartElement("GraphicsAnimationGroupDescription"); xml.WriteAttributeString("Name", "SkeletalAnimation"); xml.WriteAttributeString("EvaluationTiming", "AfterSceneCulling");
                                xml.WriteEndElement();
                            xml.WriteEndElement();

                            //

                        xml.WriteEndElement();
                    xml.WriteEndElement();
                xml.WriteEndElement();

                xml.WriteEndElement();
                xml.WriteEndElement();
                xml.WriteEndDocument();
            }
        }
    }
}
