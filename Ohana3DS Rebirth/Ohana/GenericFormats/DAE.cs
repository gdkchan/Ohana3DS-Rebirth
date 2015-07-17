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
        ///     See: https://www.khronos.org/files/collada_spec_1_4.pdf for more information.
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

                    //Metadata
                    xml.WriteStartElement("asset");
                        xml.WriteStartElement("contributor");
                            xml.WriteElementString("authoring_tool", "Ohana3DS");
                        xml.WriteEndElement();
                    xml.WriteEndElement();

                    //Materials
                    if (model.texture.Count > 0)
                    {
                        xml.WriteStartElement("library_materials");
                        for (int i = 0; i < model.texture.Count;i++ )
                        {
                            xml.WriteStartElement("material"); xml.WriteAttributeString("id", model.texture[i].name); xml.WriteAttributeString("name", model.texture[i].name);
                                xml.WriteStartElement("instance_effect"); xml.WriteAttributeString("url", "#" + model.texture[i].name + "-fx");
                                xml.WriteEndElement();
                            xml.WriteEndElement();
                        }
                        xml.WriteEndElement();
                    }

                    //Effects
                    /*if(){
                        xml.WriteStartElement("library_effects");
                        xml.WriteEndElement();
                    }*/

                    //Geometry
                    if (model.model.Count > 0)
                    {
                        xml.WriteStartElement("library_geometries");
                        for (int i = 0; i < model.model.Count; i++)
                        {
                            xml.WriteStartElement("geometry"); xml.WriteAttributeString("name", model.model[i].name); xml.WriteAttributeString("id", model.model[i].name);
                                xml.WriteStartElement("mesh");
                                for (int j = 0; j < model.model[i].modelObject.Count; j++)
                                {
                                    xml.WriteStartElement("source"); xml.WriteAttributeString("id", model.model[i].modelObject[j].name);
                                        xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", model.model[i].modelObject[j].name + "-array"); xml.WriteAttributeString("count", model.model[i].modelObject[j].obj.Count.ToString()); xml.WriteString("\r\n");
                                            for (int k = 0; k < model.model[i].modelObject[j].obj.Count; k++) xml.WriteString(model.model[i].modelObject[j].obj[k].position.x.ToString() + " " + model.model[i].modelObject[j].obj[k].position.y.ToString() + " " + model.model[i].modelObject[j].obj[k].position.z.ToString() + "\r\n");
                                        xml.WriteEndElement();
                                    xml.WriteEndElement();
                                }
                                xml.WriteEndElement();
                            xml.WriteEndElement();

                        }
                        xml.WriteEndElement();
                    }
                    //Controllers
                    /*xml.WriteStartElement("library_controllers");
                        xml.WriteStartElement("controller"); xml.WriteAttributeString("id", "");
                        xml.WriteEndElement();
                    xml.WriteEndElement();*/

                    //Animations
                    if(model.skeletalAnimation.list.Count > 0){
                        xml.WriteStartElement("library_animations");
                        for (int i = 0; i < model.model.Count; i++){
                        for (int j = 0; j < model.model[i].skeleton.Count; j++)
                        {
                            RenderBase.OBone newBone = new RenderBase.OBone();
                            newBone.parentId = model.model[i].skeleton[j].parentId;
                            newBone.rotation = new RenderBase.OVector3(model.model[i].skeleton[j].rotation);
                            newBone.translation = new RenderBase.OVector3(model.model[i].skeleton[j].translation);
                            xml.WriteStartElement("animation"); xml.WriteAttributeString("id", model.skeletalAnimation.list[i].name + "-anim"); xml.WriteAttributeString("name", model.skeletalAnimation.list[i].name);
                            foreach (RenderBase.OSkeletalAnimationBone b in ((RenderBase.OSkeletalAnimation)model.skeletalAnimation.list[skeletalAnimationIndex]).bone)
                            {
                                xml.WriteStartElement("animation"); xml.WriteAttributeString("id", b.name);
                                    xml.WriteStartElement("source");
                                    xml.WriteEndElement();
                                    xml.WriteStartElement("source");
                                    xml.WriteEndElement();
                                    xml.WriteStartElement("sample");
                                    xml.WriteEndElement();
                                    xml.WriteStartElement("channel");
                                    xml.WriteEndElement();
                                xml.WriteEndElement();
                            }
                                xml.WriteEndElement();
                            xml.WriteEndElement();
                        }
                        }
                        xml.WriteEndElement();
                    }

                    //Lights
                    if(model.light.Count > 0){
                        xml.WriteStartElement("library_lights");
                        for (int i = 0; i < model.model.Count; i++)
                        { 
                            xml.WriteStartElement("light"); xml.WriteAttributeString("id", model.light[i].name + "-light"); xml.WriteAttributeString("name", model.light[i].name);
                                xml.WriteStartElement("technique_common");
                                    xml.WriteStartElement("ambient");
                                    xml.WriteString(((float)model.light[i].ambient.R / 255f).ToString() + " " + ((float)model.light[i].ambient.G / 255f).ToString() + " " + ((float)model.light[i].ambient.B / 255f).ToString());
                                    xml.WriteEndElement();
                                xml.WriteEndElement();
                            xml.WriteEndElement(); 
                        }
                        xml.WriteEndElement();
                    }

                    //Visual Scenes
                    xml.WriteStartElement("library_visual_scenes");
                        xml.WriteStartElement("visual_scene"); xml.WriteAttributeString("id", ""); xml.WriteAttributeString("name", "");
                        xml.WriteEndElement();
                    xml.WriteEndElement();

                    //Scene
                    xml.WriteStartElement("scene");
                        xml.WriteStartElement("instance_visual_scene"); xml.WriteAttributeString("url", "#");
                        xml.WriteEndElement();
                    xml.WriteEndElement();

                xml.WriteEndElement();
            xml.WriteEndDocument();
            }
        }
    }
}
