using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ohana3DS_Rebirth.Ohana.GenericFormats
{
    class DAE
    {
        /// <summary>
        ///     Exports a Model to the Collada format.
        ///     See: https://developer.valvesoftware.com/wiki/Studiomdl_Data for more information.
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
                xml.WriteStartElement("COLLADA", "http://www.collada.org/2005/11/COLLADASchema"); xml.WriteAttributeString("version", "1.4.1");
                    xml.WriteStartElement("asset");
                        xml.WriteStartElement("contributor");
                            xml.WriteElementString("authoring_tool", "Ohana3DS");
                        xml.WriteEndElement();
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_materials");
                    for (int i = 0; i < model.texture.Count;i++ )
                    {
                        xml.WriteStartElement("material"); xml.WriteAttributeString("id", model.texture[i].name); xml.WriteAttributeString("name", model.texture[i].name);
                            xml.WriteStartElement("instance_effect"); xml.WriteAttributeString("url", "#" + model.texture[i].name + "-fx");
                            xml.WriteEndElement();
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_effects");
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_geometries");
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_controllers");
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_animations");
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_lights");
                    xml.WriteEndElement();
                    xml.WriteStartElement("library_visual_scenes");
                    xml.WriteEndElement();
                    xml.WriteStartElement("scene");
                    xml.WriteEndElement();
                xml.WriteEndElement();
            xml.WriteEndDocument();
            }
        }
    }
}
