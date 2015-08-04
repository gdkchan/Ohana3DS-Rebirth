using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats.GenericFormats
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
        /// <param name="skeletalAnimationIndex">(Optional) Index of the skeletal animation</param>
        public static void export(RenderBase.OModelGroup model, string fileName, int modelIndex, int skeletalAnimationIndex = -1)
        {
            RenderBase.OModel mdl = model.model[modelIndex];

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

						if (modelIndex != -1)
						{
							//Images
							xml.WriteStartElement("library_images");
								for (int i = 0; i < model.texture.Count; i++)
								{
									xml.WriteStartElement("image"); xml.WriteAttributeString("id", model.texture[i].name);
										xml.WriteStartElement("init_from"); xml.WriteString(@"../" + model.texture[i].name + ".png"); xml.WriteEndElement();
									xml.WriteEndElement();
								}
							xml.WriteEndElement();

							//Materials
							xml.WriteStartElement("library_materials");
							for (int i = 0; i < mdl.material.Count; i++)
							{
								xml.WriteStartElement("material"); xml.WriteAttributeString("id", String.Format("material_{0}", i)); xml.WriteAttributeString("name", mdl.material[i].name);
									xml.WriteStartElement("instance_effect"); xml.WriteAttributeString("url", "#" + String.Format("effect_{0}", i)); xml.WriteEndElement();
								xml.WriteEndElement();
							}
							xml.WriteEndElement();

							//Effects
							xml.WriteStartElement("library_effects");
							for (int i = 0; i < mdl.material.Count; i++)
							{
								xml.WriteStartElement("effect"); xml.WriteAttributeString("id", String.Format("effect_{0}", i)); xml.WriteAttributeString("name", mdl.material[i].name);
									xml.WriteStartElement("profile_COMMON");
										xml.WriteStartElement("newparam"); xml.WriteAttributeString("sid", "img_surface");
											xml.WriteStartElement("surface"); xml.WriteAttributeString("type", "2D");
												xml.WriteStartElement("init_from"); xml.WriteString(mdl.material[i].name0); xml.WriteEndElement();
												xml.WriteStartElement("format"); xml.WriteString("A8R8G8B8"); xml.WriteEndElement();
											xml.WriteEndElement();
										xml.WriteEndElement();
										xml.WriteStartElement("newparam"); xml.WriteAttributeString("sid", "img_sampler");
											xml.WriteStartElement("sampler2D");
												xml.WriteStartElement("source"); xml.WriteString("img_surface"); xml.WriteEndElement();
											xml.WriteEndElement();
										xml.WriteEndElement();
										xml.WriteStartElement("technique"); xml.WriteAttributeString("sid", "common");
											xml.WriteStartElement("phong");
												xml.WriteStartElement("emission");
													xml.WriteStartElement("color"); xml.WriteString("0 0 0 1"); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("ambient");
													xml.WriteStartElement("color"); xml.WriteString("0 0 0 1"); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("diffuse");
													xml.WriteStartElement("texture"); xml.WriteAttributeString("texture", "img_sampler"); xml.WriteAttributeString("texcoord", null); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("specular");
													xml.WriteStartElement("color"); xml.WriteString("1 1 1 1"); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("shininess");
													xml.WriteStartElement("float"); xml.WriteString("0"); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("transparent"); xml.WriteAttributeString("opaque", "A_ONE");
													xml.WriteStartElement("color"); xml.WriteString("0 0 0 1"); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("transparency");
													xml.WriteStartElement("float"); xml.WriteString("1"); xml.WriteEndElement();
												xml.WriteEndElement();
												xml.WriteStartElement("index_of_refraction");
													xml.WriteStartElement("float"); xml.WriteString("0"); xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();
										xml.WriteEndElement();
									xml.WriteEndElement();
								xml.WriteEndElement();
							}
							xml.WriteEndElement();

							//Geometry
							xml.WriteStartElement("library_geometries");
								int gSource = 0;
								for (int j = 0; j < mdl.modelObject.Count; j++)
								{
									xml.WriteStartElement("geometry");  xml.WriteAttributeString("id", String.Format("geometry_{0}", j)); xml.WriteAttributeString("name", String.Format("mesh_{0}", j));
										xml.WriteStartElement("mesh");
											string positionSrcRef = String.Format("geometry_source_{0}", gSource++);
											string normalSrcRef = String.Format("geometry_source_{0}", gSource++);
											string uvSrcRef = String.Format("geometry_source_{0}", gSource++);
											string colorSrcRef = String.Format("geometry_source_{0}", gSource++);

											//Position
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", positionSrcRef); xml.WriteAttributeString("name", String.Format("mesh_{0}_position", j));
												xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", positionSrcRef + "-array"); xml.WriteAttributeString("count", (mdl.modelObject[j].obj.Count * 3).ToString());
													for (int k = 0; k < mdl.modelObject[j].obj.Count; k++)
													{
														RenderBase.OVertex vtx = mdl.modelObject[j].obj[k];
														xml.WriteString((k > 0 ? " " : null) + getString(vtx.position.x) + " " + getString(vtx.position.y) + " " + getString(vtx.position.z));
													}
												xml.WriteEndElement();

												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", "#" + positionSrcRef + "-array"); xml.WriteAttributeString("count", mdl.modelObject[j].obj.Count.ToString()); xml.WriteAttributeString("stride", "3");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "X"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "Y"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "Z"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//Normal
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", normalSrcRef); xml.WriteAttributeString("name", String.Format("mesh_{0}_normal", j));
												xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", normalSrcRef + "-array"); xml.WriteAttributeString("count", (mdl.modelObject[j].obj.Count * 3).ToString());
													for (int k = 0; k < mdl.modelObject[j].obj.Count; k++)
													{
														RenderBase.OVertex vtx = mdl.modelObject[j].obj[k];
														xml.WriteString((k > 0 ? " " : null) + getString(vtx.normal.x) + " " + getString(vtx.normal.y) + " " + getString(vtx.normal.z));
													}
												xml.WriteEndElement();

												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", "#" + normalSrcRef + "-array"); xml.WriteAttributeString("count", mdl.modelObject[j].obj.Count.ToString()); xml.WriteAttributeString("stride", "3");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "X"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "Y"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "Z"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//UVs
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", uvSrcRef); xml.WriteAttributeString("name", String.Format("mesh_{0}_texuv", j));
												xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", uvSrcRef + "-array"); xml.WriteAttributeString("count", (mdl.modelObject[j].obj.Count * 2).ToString());
													for (int k = 0; k < mdl.modelObject[j].obj.Count; k++)
													{
														RenderBase.OVertex vtx = mdl.modelObject[j].obj[k];
														xml.WriteString((k > 0 ? " " : null) + getString(vtx.texture0.x) + " " + getString(vtx.texture0.y));
													}
												xml.WriteEndElement();

												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", "#" + uvSrcRef + "-array"); xml.WriteAttributeString("count", mdl.modelObject[j].obj.Count.ToString()); xml.WriteAttributeString("stride", "2");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "S"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "T"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//Color
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", colorSrcRef); xml.WriteAttributeString("name", String.Format("mesh_{0}_color", j));
												xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", colorSrcRef + "-array"); xml.WriteAttributeString("count", (mdl.modelObject[j].obj.Count * 4).ToString());
													for (int k = 0; k < mdl.modelObject[j].obj.Count; k++)
													{
														RenderBase.OVertex vtx = mdl.modelObject[j].obj[k];
														float a = 1f;
														float r = ((vtx.diffuseColor >> 16) & 0xff) / 255f;
														float g = ((vtx.diffuseColor >> 8) & 0xff) / 255f;
														float b = (vtx.diffuseColor & 0xff) / 255f;
														xml.WriteString((k > 0 ? " " : null) + getString(r) + " " + getString(g) + " " + getString(b) + " " + getString(a));
													}
												xml.WriteEndElement();

												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", "#" + colorSrcRef + "-array"); xml.WriteAttributeString("count", mdl.modelObject[j].obj.Count.ToString()); xml.WriteAttributeString("stride", "4");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "R"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "G"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "B"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "A"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//Vertex
											xml.WriteStartElement("vertices"); xml.WriteAttributeString("id", String.Format("geometry_vertices_{0}", j));
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "POSITION"); xml.WriteAttributeString("source", "#" + positionSrcRef); xml.WriteEndElement();
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "NORMAL"); xml.WriteAttributeString("source", "#" + normalSrcRef); xml.WriteEndElement();
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "TEXCOORD"); xml.WriteAttributeString("source", "#" + uvSrcRef); xml.WriteEndElement();
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "COLOR"); xml.WriteAttributeString("source", "#" + colorSrcRef); xml.WriteEndElement();
											xml.WriteEndElement();

											xml.WriteStartElement("triangles"); xml.WriteAttributeString("material", String.Format("_material_{0}", mdl.modelObject[j].materialId)); xml.WriteAttributeString("count", mdl.modelObject[j].obj.Count.ToString());
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "VERTEX"); xml.WriteAttributeString("source", "#" + String.Format("geometry_vertices_{0}", j)); xml.WriteAttributeString("offset", "0"); xml.WriteEndElement();
												xml.WriteStartElement("p");
													for (int i = 0; i < mdl.modelObject[j].obj.Count; i++) xml.WriteString((i > 0 ? " " : null) + i.ToString());
												xml.WriteEndElement();
											xml.WriteEndElement();
										xml.WriteEndElement();
									xml.WriteEndElement();
								}
							xml.WriteEndElement();

							xml.WriteStartElement("library_controllers");
								string jointNameTable = null;
								StringBuilder invBindMtx = new StringBuilder();

								List<RenderBase.OMatrix> skeletonTransform = new List<RenderBase.OMatrix>();
								for (int index = 0; index < mdl.skeleton.Count; index++)
								{
									RenderBase.OMatrix transform = new RenderBase.OMatrix();
									transformSkeleton(mdl.skeleton, index, ref transform);
									skeletonTransform.Add(transform);
								}

								for (int i = 0; i < mdl.skeleton.Count; i++)
								{
									jointNameTable += mdl.skeleton[i].name + " ";

									RenderBase.OMatrix mtx = skeletonTransform[i].invert();

									invBindMtx.Append(
										getString(mtx.M11) + " " + getString(mtx.M21) + " " + getString(mtx.M31) + " " + getString(mtx.M41) + " " +
										getString(mtx.M12) + " " + getString(mtx.M22) + " " + getString(mtx.M32) + " " + getString(mtx.M42) + " " +
										getString(mtx.M13) + " " + getString(mtx.M23) + " " + getString(mtx.M33) + " " + getString(mtx.M43) + " " +
										getString(mtx.M14) + " " + getString(mtx.M24) + " " + getString(mtx.M34) + " " + getString(mtx.M44) + " ");
								}
								jointNameTable = jointNameTable.TrimEnd();
								string jointInvBindMatrix = invBindMtx.ToString().TrimEnd();

								for (int j = 0; j < mdl.modelObject.Count; j++)
								{
									xml.WriteStartElement("controller"); xml.WriteAttributeString("id", String.Format("controller_{0}", j));
										xml.WriteStartElement("skin"); xml.WriteAttributeString("source", "#" + String.Format("geometry_{0}", j));
											xml.WriteStartElement("bind_shape_matrix"); xml.WriteString("1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1"); xml.WriteEndElement();
											
											//Joint
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", String.Format("controller_{0}_joints", j));
												xml.WriteStartElement("Name_array"); xml.WriteAttributeString("id", String.Format("controller_{0}_joints-array", j)); xml.WriteAttributeString("count", mdl.skeleton.Count.ToString()); xml.WriteString(jointNameTable); xml.WriteEndElement();
												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", String.Format("#controller_{0}_joints-array", j)); xml.WriteAttributeString("count", mdl.skeleton.Count.ToString()); xml.WriteAttributeString("stride", "1");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "JOINT"); xml.WriteAttributeString("type", "name"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//Inverse Bind Matrix
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", String.Format("controller_{0}_bind_poses", j));
												xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", String.Format("controller_{0}_bind_poses-array", j)); xml.WriteAttributeString("count", (mdl.skeleton.Count * 16).ToString()); xml.WriteString(jointInvBindMatrix); xml.WriteEndElement();
												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", String.Format("#controller_{0}_bind_poses-array", j)); xml.WriteAttributeString("count", mdl.skeleton.Count.ToString()); xml.WriteAttributeString("stride", "16");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "TRANSFORM"); xml.WriteAttributeString("type", "float4x4"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//Weight
											xml.WriteStartElement("source"); xml.WriteAttributeString("id", String.Format("controller_{0}_weights", j));
												StringBuilder jointWeightTable = new StringBuilder();
												for (int k = 0; k < mdl.modelObject[j].obj.Count; k++) 
												{
													RenderBase.OVertex vtx = mdl.modelObject[j].obj[k];
													float[] weights = new float[4];

													int i;
													float weightSum = 0;
													for (i = 0; i < vtx.weight.Count; i++) 
													{
														weights[i] = vtx.weight[i];
														weightSum += weights[i];
													}
													if (i < 4) weights[i] = 1f - weightSum;

													for (i = 0; i < 4; i++) jointWeightTable.Append(getString(weights[i]) + " ");
												}

												xml.WriteStartElement("float_array"); xml.WriteAttributeString("id", String.Format("controller_{0}_weights-array", j)); xml.WriteAttributeString("count", (mdl.modelObject[j].obj.Count * 4).ToString()); xml.WriteString(jointWeightTable.ToString().TrimEnd()); xml.WriteEndElement();
												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("accessor"); xml.WriteAttributeString("source", String.Format("#controller_{0}_weights-array", j)); xml.WriteAttributeString("count", (mdl.modelObject[j].obj.Count * 4).ToString()); xml.WriteAttributeString("stride", "1");
														xml.WriteStartElement("param"); xml.WriteAttributeString("name", "WEIGHT"); xml.WriteAttributeString("type", "float"); xml.WriteEndElement();
													xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();

											//Joints
											xml.WriteStartElement("joints");
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "JOINT"); xml.WriteAttributeString("source", String.Format("#controller_{0}_joints", j)); xml.WriteEndElement();
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "INV_BIND_MATRIX"); xml.WriteAttributeString("source", String.Format("#controller_{0}_bind_poses", j)); xml.WriteEndElement();
											xml.WriteEndElement();

											xml.WriteStartElement("vertex_weights"); xml.WriteAttributeString("count", mdl.modelObject[j].obj.Count.ToString());
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "JOINT"); xml.WriteAttributeString("source", String.Format("#controller_{0}_joints", j)); xml.WriteAttributeString("offset", "0"); xml.WriteEndElement();
												xml.WriteStartElement("input"); xml.WriteAttributeString("semantic", "WEIGHT"); xml.WriteAttributeString("source", String.Format("#controller_{0}_weights", j)); xml.WriteAttributeString("offset", "1"); xml.WriteEndElement();
												xml.WriteStartElement("vcount"); for (int i = 0; i < mdl.modelObject[j].obj.Count; i++) xml.WriteString(i > 0 ? " 4" : "4"); xml.WriteEndElement();
												xml.WriteStartElement("v");
													for (int k = 0; k < mdl.modelObject[j].obj.Count; k++)
													{
														RenderBase.OVertex vtx = mdl.modelObject[j].obj[k];

														for (int i = 0; i < 4; i++)
														{
															int l = k * 4 + i;

															if (i < vtx.node.Count)
																xml.WriteString((l > 0 ? " " : null) + vtx.node[i].ToString() + " " + l.ToString());
															else
																xml.WriteString((l > 0 ? " " : null) + "0 " + l.ToString());
														}
													}
												xml.WriteEndElement();
											xml.WriteEndElement();
										xml.WriteEndElement();
									xml.WriteEndElement();
								}
							xml.WriteEndElement();
						}

						//Visual Scenes
						xml.WriteStartElement("library_visual_scenes");
							xml.WriteStartElement("visual_scene"); xml.WriteAttributeString("id", "VisualSceneNode"); xml.WriteAttributeString("name", "scene");
								int nodeIndex = 0;
								writeSkeleton(xml, mdl.skeleton, 0, ref nodeIndex);
								for (int i = 0; i < mdl.modelObject.Count; i++)
								{
									xml.WriteStartElement("node"); xml.WriteAttributeString("id", String.Format("vsn_{0}", i)); xml.WriteAttributeString("name", mdl.modelObject[i].name);
										xml.WriteStartElement("matrix"); xml.WriteString("1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1"); xml.WriteEndElement();
										xml.WriteStartElement("instance_controller"); xml.WriteAttributeString("url", "#" + String.Format("controller_{0}", i));
											xml.WriteStartElement("skeleton"); xml.WriteString("#skeleton_node_0"); xml.WriteEndElement();
											xml.WriteStartElement("bind_material");
												xml.WriteStartElement("technique_common");
													xml.WriteStartElement("instance_material"); xml.WriteAttributeString("symbol", String.Format("_material_{0}", mdl.modelObject[i].materialId)); xml.WriteAttributeString("target", String.Format("#material_{0}", mdl.modelObject[i].materialId)); xml.WriteEndElement();
												xml.WriteEndElement();
											xml.WriteEndElement();
										xml.WriteEndElement();
									xml.WriteEndElement();
								}
							xml.WriteEndElement();
						xml.WriteEndElement();

						//Scene
						xml.WriteStartElement("scene");
							xml.WriteStartElement("instance_visual_scene"); xml.WriteAttributeString("url", "#VisualSceneNode");
							xml.WriteEndElement();
						xml.WriteEndElement();

					xml.WriteEndElement();
				xml.WriteEndDocument();
            }
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

        /// <summary>
        ///     Transforms a Skeleton from relative to absolute positions.
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the bone to convert</param>
        /// <param name="target">Target matrix to save bone transformation</param>
        private static void transformSkeleton(List<RenderBase.OBone> skeleton, int index, ref RenderBase.OMatrix target)
        {
            target *= RenderBase.OMatrix.scale(skeleton[index].scale);
            target *= RenderBase.OMatrix.rotateX(skeleton[index].rotation.x);
            target *= RenderBase.OMatrix.rotateY(skeleton[index].rotation.y);
            target *= RenderBase.OMatrix.rotateZ(skeleton[index].rotation.z);
            target *= RenderBase.OMatrix.translate(skeleton[index].translation);
            if (skeleton[index].parentId > -1) transformSkeleton(skeleton, skeleton[index].parentId, ref target);
        }

        /// <summary>
        ///     Writes the skeleton hierarchy to the DAE.
        /// </summary>
        /// <param name="xml">XML Writer</param>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the current bone (root bone when it's not a recursive call)</param>
        /// <param name="acc">Bone index accumulator</param>
        private static void writeSkeleton(XmlWriter xml, List<RenderBase.OBone> skeleton, int index, ref int acc)
        {
            xml.WriteStartElement("node"); xml.WriteAttributeString("id", String.Format("skeleton_node_{0}", acc++)); xml.WriteAttributeString("name", skeleton[index].name); xml.WriteAttributeString("sid", skeleton[index].name); xml.WriteAttributeString("type", "JOINT");
                RenderBase.OMatrix mtx = new RenderBase.OMatrix();
                mtx *= RenderBase.OMatrix.scale(skeleton[index].scale);
                mtx *= RenderBase.OMatrix.rotateX(skeleton[index].rotation.x);
                mtx *= RenderBase.OMatrix.rotateY(skeleton[index].rotation.y);
                mtx *= RenderBase.OMatrix.rotateZ(skeleton[index].rotation.z);
                mtx *= RenderBase.OMatrix.translate(skeleton[index].translation);

                xml.WriteStartElement("matrix"); xml.WriteString(
                    getString(mtx.M11) + " " + getString(mtx.M21) + " " + getString(mtx.M31) + " " + getString(mtx.M41) + " " +
                    getString(mtx.M12) + " " + getString(mtx.M22) + " " + getString(mtx.M32) + " " + getString(mtx.M42) + " " +
                    getString(mtx.M13) + " " + getString(mtx.M23) + " " + getString(mtx.M33) + " " + getString(mtx.M43) + " " +
                    getString(mtx.M14) + " " + getString(mtx.M24) + " " + getString(mtx.M34) + " " + getString(mtx.M44));
                xml.WriteEndElement();

                for (int i = 0; i < skeleton.Count; i++) if (skeleton[i].parentId == index) writeSkeleton(xml, skeleton, i, ref acc);
            xml.WriteEndElement();
        }
    }
}
