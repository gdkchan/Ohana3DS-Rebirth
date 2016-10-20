using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ohana3DS_Rebirth.Ohana.Models.GenericFormats
{
    public class CMDL
    {
        [XmlRootAttribute("NintendoWareIntermediateFile")]
        public class CtrModel {
            public ctrGraphicsContent GraphicsContentCtr = new ctrGraphicsContent();
        }

        public class ctrGraphicsContent {
            [XmlAttribute]
            public string Version = "1.3.0";

            [XmlAttribute]
            public string Namespace = "";

            public ctrEditData EditData = new ctrEditData();
            public ctrModels Models = new ctrModels();
        }

        public class ctrEditData { //This class is used in multiple locations with different elements, so init them manually
            public ctrMetaData MetaData = null;
            public ctrModelDccToolExportOpt ModelDccToolExportOption = null;
            public ctrOptLogArrayMeta OptimizationLogArrayMetaData = null;
            public ctrContentSummaryMeta ContentSummaryMetaData = null;
        }

        public class ctrModels {
            public ctrSkeletalModel SkeletalModel = new ctrSkeletalModel();
        }

        #region EDITDATA
        public class ctrMetaData {
            public string Key;
            public ctrCreate Create = new ctrCreate();
            public ctrModify Modify = new ctrModify();
        }

        public class ctrCreate {
            [XmlAttribute]
            public string Author;

            [XmlAttribute]
            public string Date;

            [XmlAttribute]
            public string Source;

            [XmlAttribute]
            public string FullPathOfSource;

            public ctrToolDesc ToolDescription = new ctrToolDesc();
        }

        public class ctrToolDesc {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public string Version;
        }


        public class ctrModify {
            [XmlAttribute]
            public string Date;

            public ctrToolDesc ToolDescription = new ctrToolDesc();
        }

        public class ctrContentSummaryMeta {
            public string Key;
            public ctrValues Values = new ctrValues();

        }

        public class ctrValues {
            public ctrContentSummary ContentSummary = new ctrContentSummary();
        }

        public class ctrContentSummary {
            [XmlAttribute]
            public string ContentTypeName;

            [XmlArrayItem("ObjectSummary")]
            public List<ctrObjectSummary> ObjectSummaries = new List<ctrObjectSummary>();
        }

        public class ctrObjectSummary {
            [XmlAttribute]
            public string TypeName;

            [XmlAttribute]
            public string Name;

            [XmlArrayItem("Note")]
            public List<ctrNote> Notes = new List<ctrNote>();
        }

        public class ctrNote {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public uint Value;
        }

        public class ctrOptLogArrayMeta {
            [XmlAttribute]
            public uint Size;

            public string Key;

            [XmlArrayItem("OptimizationLog")]
            public List<ctrOptLog> Values = new List<ctrOptLog>();
        }

        public class ctrOptLog {
            [XmlAttribute]
            public string Date;

            [XmlAttribute]
            public string EditorVersion;

            [XmlAttribute]
            public double OptimizePrimitiveAverageCacheMissRatio;

            [XmlAttribute]
            public string OptimizerIdentifier;
        }
        #endregion

        public class ctrSkeletalModel {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public bool IsBranchVisible;

            [XmlAttribute]
            public bool IsVisible;

            [XmlAttribute]
            public string CullingMode;

            [XmlAttribute]
            public bool IsNonuniformScalable;

            [XmlAttribute]
            public uint LayerId;

            [XmlAttribute]
            public uint NeededBoneCapacity;

            public ctrEditData EditData = new ctrEditData();

            [XmlArrayItem("GraphicsAnimationGroupDescription")]
            public List<ctrGraphicsAnimGroupDesc> AnimationGroupDescription = new List<ctrGraphicsAnimGroupDesc>();

            public ctrTransform Transform = new ctrTransform();

            [XmlArrayItem("SeperateDataShapeCtr")]
            public List<ctrSeperateDataShape> Shapes = new List<ctrSeperateDataShape>();

            [XmlArrayItem("MaterialCtr")]
            public List<ctrMaterial> Materials = new List<ctrMaterial>();

            [XmlArrayItem("Mesh")]
            public List<ctrMesh> Meshes = new List<ctrMesh>();

            [XmlArrayItem("MeshNodeVisibility")]
            public List<ctrMeshVis> MeshNodeVisibilities = new List<ctrMeshVis>();

            public ctrSkeleton Skeleton = new ctrSkeleton();
        }

        #region ANIMATIONS
        public class ctrGraphicsAnimGroupDesc {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public string EvaluationTiming;

            [XmlArrayItem("AnimationMemberDescription")]
            public List<ctrAnimMemberDesc> MemberInformationSet = new List<ctrAnimMemberDesc>();
        }

        public class ctrAnimMemberDesc {
            [XmlAttribute]
            public string BlendOperationName;

            [XmlAttribute]
            public bool IsBinarized;

            public string Path;
        }
        #endregion

        #region TRANSFORM
        public class ctrTransform {
            public ctrScale Scale = new ctrScale();
            public ctrRotation Rotation = new ctrRotation();
            public ctrTranslate Translate = new ctrTranslate();
        }

        public class ctrScale {
            [XmlAttribute]
            public float X;

            [XmlAttribute]
            public float Y;

            [XmlAttribute]
            public float Z;
        }

        public class ctrRotation {
            [XmlAttribute]
            public float X;

            [XmlAttribute]
            public float Y;

            [XmlAttribute]
            public float Z;
        }

        public class ctrTranslate {
            [XmlAttribute]
            public float X;

            [XmlAttribute]
            public float Y;

            [XmlAttribute]
            public float Z;
        }
        #endregion

        #region SHAPES
        public class ctrSeperateDataShape {
            public ctrOBB OrientedBoundingBox = new ctrOBB();

            public ctrPositionOff PositionOffset = new ctrPositionOff();

            [XmlArrayItem("PrimitiveSetCtr")]
            public List<ctrPrimSet> PrimitiveSets = new List<ctrPrimSet>();

            [XmlArrayItem("Vector3VertexStreamCtr")]
            public List<ctrVertAttrib> VertexAttributes = new List<ctrVertAttrib>();
        }

        public class ctrOBB {
            public ctrCenterPos CenterPosition = new ctrCenterPos();
            public ctrMatrix OrientationMatrix = new ctrMatrix();
            public ctrSize Size = new ctrSize();
        }

        public class ctrCenterPos {
            [XmlAttribute]
            public float X;

            [XmlAttribute]
            public float Y;

            [XmlAttribute]
            public float Z;
        }

        public class ctrMatrix {
            [XmlAttribute]
            public float M00;

            [XmlAttribute]
            public float M01;
            
            [XmlAttribute]
            public float M02;

            [XmlAttribute]
            public float M10;

            [XmlAttribute]
            public float M11;

            [XmlAttribute]
            public float M12;

            [XmlAttribute]
            public float M20;

            [XmlAttribute]
            public float M21;

            [XmlAttribute]
            public float M22;
        }

        public class ctrSize {
            [XmlAttribute]
            public float X;

            [XmlAttribute]
            public float Y;

            [XmlAttribute]
            public float Z;
        }

        public class ctrPositionOff {
            [XmlAttribute]
            public float X;

            [XmlAttribute]
            public float Y;

            [XmlAttribute]
            public float Z;
        }

        public class ctrPrimSet {
            [XmlAttribute]
            public string SkinningMode;

            public string BoneIndexTable;

            [XmlArrayItem("PrimitiveCtr")]
            public List<ctrPrim> Primitives = new List<ctrPrim>();
        }

        public class ctrPrim {
            [XmlArrayItem("UshortIndexStreamCtr")]
            public List<ctrIndexStream> IndexStreams = new List<ctrIndexStream>();
        }

        public class ctrIndexStream {
            [XmlAttribute]
            public string PrimitiveMode;

            [XmlAttribute]
            public string Size;
        }

        public class ctrVertAttrib {
            [XmlAttribute]
            public string Usage;

            [XmlAttribute]
            public uint VertexSize;

            [XmlAttribute]
            public double Scale;

            [XmlAttribute]
            public string QuantizedMode;

            [XmlText]
            public string Vec3Array;
        }
        #endregion

        public class ctrMaterial {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public bool IsCompressible;

            [XmlAttribute]
            public uint LightSetIndex;

            [XmlAttribute]
            public uint FogIndex;

            [XmlAttribute]
            public bool IsFragmentLightEnabled;

            [XmlAttribute]
            public bool IsVertexLightEnabled;

            [XmlAttribute]
            public bool IsHemiSphereLightEnabled;

            [XmlAttribute]
            public bool IsHemiSphereOcclusionEnabled;

            [XmlAttribute]
            public bool IsFogEnabled;

            [XmlAttribute]
            public string TextureCoordinateConfig;

            [XmlAttribute]
            public string TranslucencyKind;

            [XmlAttribute]
            public int ShaderProgramDescriptionIndex;

            [XmlAttribute]
            public string ShaderBinaryKind;
        }

        public class ctrMesh {
            [XmlAttribute]
            public bool IsVisible;

            [XmlAttribute]
            public int RenderPriority;

            [XmlAttribute]
            public string MeshNodeName;

            public string SeparateShapeReference;
            public string MaterialReference;
        }

        public class ctrMeshVis {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public bool IsVisible;
        }

        public class ctrSkeleton {
            [XmlAttribute]
            public string RootBoneName;

            [XmlAttribute]
            public string ScalingRule;

            [XmlAttribute]
            public bool IsTranslateAnimationEnabled;

            [XmlArrayItem("Bone")]
            public List<ctrBone> Bones = new List<ctrBone>();
        }

        public class ctrBone {
            [XmlAttribute]
            public string Name;

            [XmlAttribute]
            public string ParentBoneName;

            [XmlAttribute]
            public bool IsSegmentScaleCompensate;

            [XmlAttribute]
            public bool IsCompressible;

            [XmlAttribute]
            public bool IsNeededRendering;

            [XmlAttribute]
            public bool HasSkinningMatrix;

            [XmlAttribute]
            public RenderBase.OBillboardMode BillboardMode;

            public ctrTransform Transform = new ctrTransform();
        }

        public class ctrModelDccToolExportOpt {
            [XmlAttribute]
            public uint ExportStartFrame;

            [XmlAttribute]
            public uint Magnify;

            [XmlAttribute]
            public string AdjustSkinning;

            [XmlAttribute]
            public string MeshVisibilityMode;

            public string Key;
        }

        /// <summary>
        ///     Exports a Model to the CMDL format.
        /// </summary>
        /// <param name="model">The Model that will be exported</param>
        /// <param name="fileName">The output File Name</param>
        /// <param name="modelIndex">Index of the model to be exported</param>
        /// <param name="skeletalAnimationIndex">(Optional) Index of the skeletal animation.</param>
        public static void export(RenderBase.OModelGroup model, string fileName, int modelIndex, int skeletalAnimationIndex = -1)
        {
            RenderBase.OModel mdl = model.model[modelIndex];
            CtrModel ctrMdl = new CtrModel();

            //EditData
            ctrEditData edit = ctrMdl.GraphicsContentCtr.EditData;
            edit.MetaData = new ctrMetaData();
            edit.MetaData.Key = "MetaData";
            edit.MetaData.Create.Author = Environment.UserName;
            edit.MetaData.Create.Source = "";
            edit.MetaData.Create.FullPathOfSource = "";
            edit.MetaData.Create.Date = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            edit.MetaData.Create.ToolDescription.Name = "Ohana3DS";
            edit.MetaData.Create.ToolDescription.Version = "1.0";
            edit.MetaData.Modify.Date = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            edit.MetaData.Modify.ToolDescription.Name = "Ohana3DS";
            edit.MetaData.Modify.ToolDescription.Version = "1.0";
            edit.ContentSummaryMetaData = new ctrContentSummaryMeta();
            edit.ContentSummaryMetaData.Key = "ContentSummaries";
            edit.ContentSummaryMetaData.Values.ContentSummary.ContentTypeName = "GraphicsContent";

            List<ctrObjectSummary> summaries = edit.ContentSummaryMetaData.Values.ContentSummary.ObjectSummaries;
            ctrObjectSummary sum = new ctrObjectSummary();
            sum.TypeName = "SkeletalModel";
            sum.Name = mdl.name;
            summaries.Add(sum);

            List<ctrNote> notes = sum.Notes;
            ctrNote note;
            note = new ctrNote();
            note.Name = "MaterialCount";
            note.Value = (uint)mdl.material.Count;
            notes.Add(note);
            note = new ctrNote();
            note.Name = "ShapeCount";
            note.Value = (uint)mdl.mesh.Count;
            notes.Add(note);
            note = new ctrNote();
            note.Name = "MeshCount";
            note.Value = (uint)mdl.mesh.Count;
            notes.Add(note);
            note = new ctrNote();
            note.Name = "BoneCount";
            note.Value = (uint)mdl.skeleton.Count;
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalPrimitiveSetCount";
            note.Value = (uint)0;   //TODO
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalNoneSkinningPrimitiveSetCount";
            note.Value = (uint)0;   //TODO
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalRigidSkinningPrimitiveSetCount";
            note.Value = (uint)0;   //TODO
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalSmoothSkinningPrimitiveSetCount";
            note.Value = (uint)0;   //TODO
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalIndexStreamCount";
            note.Value = (uint)MeshUtils.optimizeMesh(mdl.mesh[0]).indices.Count;   //TODO
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalPolygonCount";
            note.Value = (uint)mdl.verticesCount / 3;
            notes.Add(note);
            note = new ctrNote();
            note.Name = "TotalVertexCount";
            note.Value = (uint)MeshUtils.getOptimizedVertCount(mdl.mesh);
            notes.Add(note);

            //Skeleton data
            ctrSkeletalModel skelData = ctrMdl.GraphicsContentCtr.Models.SkeletalModel;
            skelData.EditData.ModelDccToolExportOption = new ctrModelDccToolExportOpt();
            skelData.EditData.OptimizationLogArrayMetaData = new ctrOptLogArrayMeta();
            skelData.Name = mdl.name;
            skelData.IsBranchVisible = true;
            skelData.IsVisible = true;
            skelData.CullingMode = "Dynamic";
            skelData.IsNonuniformScalable = false;
            skelData.LayerId = mdl.layerId;
            skelData.NeededBoneCapacity = 20;

            //Skeleton EditData
            ctrModelDccToolExportOpt dccExpOpt = skelData.EditData.ModelDccToolExportOption;
            dccExpOpt.ExportStartFrame = 0;
            dccExpOpt.Magnify = 1;
            dccExpOpt.AdjustSkinning = "SmoothSkinning";
            dccExpOpt.MeshVisibilityMode = "BindByName";
            dccExpOpt.Key = "ModelDccToolInfo";
            ctrOptLogArrayMeta optLogArr = skelData.EditData.OptimizationLogArrayMetaData;
            optLogArr.Size = 1;
            optLogArr.Key = "OptimizationLogs";
            ctrOptLog optLog;
            optLog = new ctrOptLog();
            optLog.Date = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            optLog.EditorVersion = "1.4.5.44775";
            optLog.OptimizePrimitiveAverageCacheMissRatio = 0.7663249;
            optLog.OptimizerIdentifier = "AlgorithmCombo";
            optLogArr.Values.Add(optLog);

            //Skeleton Animation
            List<ctrGraphicsAnimGroupDesc> animGroups = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.AnimationGroupDescription;
            ctrGraphicsAnimGroupDesc animGroup;
            ctrAnimMemberDesc animMembDesc;
            animGroup = new ctrGraphicsAnimGroupDesc();
            animGroup.Name = "SkeletalAnimation";
            animGroup.EvaluationTiming = "AfterSceneCulling";
            animGroups.Add(animGroup);
            List<ctrAnimMemberDesc> skelMembSet = animGroup.MemberInformationSet;
            animMembDesc= new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "CalculatedTransform";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Skeleton.Bones[\"*\"].AnimatedTransform";
            skelMembSet.Add(animMembDesc);

            //Visibility Animation
            animGroup = new ctrGraphicsAnimGroupDesc();
            animGroup.Name = "VisibilityAnimation";
            animGroup.EvaluationTiming = "BeforeWorldUpdate";
            animGroups.Add(animGroup);
            List<ctrAnimMemberDesc> visMembSet = animGroup.MemberInformationSet;
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Bool";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "IsVisible";
            visMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Bool";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Meshes[\"*\"].IsVisible";
            visMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Bool";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "MeshNodeVisibilities[\"*\"].IsVisible";
            visMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Bool";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "IsBranchVisible";
            visMembSet.Add(animMembDesc);

            //Material Animation
            animGroup = new ctrGraphicsAnimGroupDesc();
            animGroup.Name = "MaterialAnimation";
            animGroup.EvaluationTiming = "AfterSceneCulling";
            animGroups.Add(animGroup);
            List<ctrAnimMemberDesc> matMembSet = animGroup.MemberInformationSet;
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Emission";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Ambient";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Diffuse";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Specular0";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Specular1";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Constant0";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Constant1";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Constant2";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Constant3";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Constant4";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].MaterialColor.Constant5";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].TextureMappers[\"*\"].Sampler.BorderColor";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Int";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].TextureMappers[\"*\"].Texture";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "RgbaColor";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].FragmentOperation.BlendOperation.BlendColor";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Vector2";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].TextureCoordinators[\"*\"].Scale";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Float";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].TextureCoordinators[\"*\"].Rotate";
            matMembSet.Add(animMembDesc);
            animMembDesc = new ctrAnimMemberDesc();
            animMembDesc.BlendOperationName = "Vector2";
            animMembDesc.IsBinarized = true;
            animMembDesc.Path = "Materials[\"*\"].TextureCoordinators[\"*\"].Translate";
            matMembSet.Add(animMembDesc);

            //Transform
            ctrTransform trans = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.Transform;
            trans.Scale.X = 1;
            trans.Scale.Y = 1;
            trans.Scale.Z = 1;
            trans.Rotation.X = 0;
            trans.Rotation.Y = 0;
            trans.Rotation.Z = 0;
            trans.Translate.X = 0;
            trans.Translate.Y = 0;
            trans.Translate.Z = 0;

            //Shapes
            List<ctrSeperateDataShape> shapes = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.Shapes;
            ctrSeperateDataShape shape;
            ctrOBB obb;
            ctrPrimSet primSet;
            ctrVertAttrib vertAtt;
            foreach (var sh in mdl.mesh) {
                shape = new ctrSeperateDataShape();
                obb = new ctrOBB();
                obb.CenterPosition.X = sh.boundingBox.centerPosition.x;
                obb.CenterPosition.Y = sh.boundingBox.centerPosition.y;
                obb.CenterPosition.Z = sh.boundingBox.centerPosition.z;
                obb.OrientationMatrix.M00 = sh.boundingBox.orientationMatrix.M11;
                obb.OrientationMatrix.M01 = sh.boundingBox.orientationMatrix.M12;
                obb.OrientationMatrix.M02 = sh.boundingBox.orientationMatrix.M13;
                obb.OrientationMatrix.M10 = sh.boundingBox.orientationMatrix.M21;
                obb.OrientationMatrix.M11 = sh.boundingBox.orientationMatrix.M22;
                obb.OrientationMatrix.M12 = sh.boundingBox.orientationMatrix.M23;
                obb.OrientationMatrix.M20 = sh.boundingBox.orientationMatrix.M31;
                obb.OrientationMatrix.M21 = sh.boundingBox.orientationMatrix.M32;
                obb.OrientationMatrix.M22 = sh.boundingBox.orientationMatrix.M33;
                obb.Size.X = sh.boundingBox.size.x;
                obb.Size.Y = sh.boundingBox.size.y;
                obb.Size.Z = sh.boundingBox.size.z;
                shape.OrientedBoundingBox = obb;
                shape.PositionOffset.X = 0;
                shape.PositionOffset.Y = 0;
                shape.PositionOffset.Z = 0;
                primSet = new ctrPrimSet();
                primSet.SkinningMode = "SmoothSkinning";
                primSet.BoneIndexTable = "";

                StringBuilder sb;
                vertAtt = new ctrVertAttrib();
                vertAtt.Usage = "Position";
                vertAtt.VertexSize = (uint)sh.vertices.Count;
                vertAtt.Scale = 1;
                vertAtt.QuantizedMode = "Float";
                sb = new StringBuilder("\n");
                foreach(var vec in sh.vertices) {
                    sb.Append(
                        vec.position.x + " " +
                        vec.position.y + " " +
                        vec.position.z + " " +
                        "\n"
                        );
                }
                vertAtt.Vec3Array = sb.ToString();
                shape.VertexAttributes.Add(vertAtt);

                vertAtt = new ctrVertAttrib();
                vertAtt.Usage = "Normal";
                vertAtt.VertexSize = (uint)sh.vertices.Count;
                vertAtt.Scale = 1;
                vertAtt.QuantizedMode = "Float";
                sb = new StringBuilder("\n");
                foreach (var vec in sh.vertices) {
                    sb.Append(
                        vec.normal.x + " " +
                        vec.normal.y + " " +
                        vec.normal.z + " " +
                        "\n"
                        );
                }
                vertAtt.Vec3Array = sb.ToString();
                shape.VertexAttributes.Add(vertAtt);

                vertAtt = new ctrVertAttrib();
                vertAtt.Usage = "TextureCoordinate0";
                vertAtt.VertexSize = (uint)sh.vertices.Count;
                vertAtt.Scale = 1;
                vertAtt.QuantizedMode = "Float";
                sb = new StringBuilder("\n");
                foreach (var vec in sh.vertices) {
                    sb.Append(
                        vec.texture0.x + " " +
                        vec.texture0.y + " " +
                        "\n"
                        );
                }
                vertAtt.Vec3Array = sb.ToString();
                shape.VertexAttributes.Add(vertAtt);

                vertAtt = new ctrVertAttrib();
                vertAtt.Usage = "BoneIndex";
                vertAtt.VertexSize = (uint)sh.vertices.Count;
                vertAtt.Scale = 1;
                vertAtt.QuantizedMode = "Ubyte";
                //TODO obtain bone indecies
                shape.VertexAttributes.Add(vertAtt);

                vertAtt = new ctrVertAttrib();
                vertAtt.Usage = "BoneWeight";
                vertAtt.VertexSize = (uint)sh.vertices.Count;
                vertAtt.Scale = 0.01;
                vertAtt.QuantizedMode = "Ubyte";
                //TODO obtain bone weights
                shape.VertexAttributes.Add(vertAtt);

                shape.PrimitiveSets.Add(primSet);
                shapes.Add(shape);
            }

            //Materials
            List<ctrMaterial> mats = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.Materials;
            ctrMaterial mat;
            foreach (var mt in mdl.material) {
                mat = new ctrMaterial();
                mat.Name = mt.name;
                mat.IsCompressible = true;
                mat.LightSetIndex = mt.lightSetIndex;
                mat.FogIndex = mt.fogIndex;
                mat.IsFragmentLightEnabled = mt.isFragmentLightEnabled;
                mat.IsVertexLightEnabled = mt.isVertexLightEnabled;
                mat.IsHemiSphereLightEnabled = mt.isHemiSphereLightEnabled;
                mat.IsHemiSphereOcclusionEnabled = mt.isHemiSphereOcclusionEnabled;
                mat.IsFogEnabled = mt.isFogEnabled;
                mat.TextureCoordinateConfig = "Config0120";
                mat.TranslucencyKind = "Layer0";
                mat.ShaderProgramDescriptionIndex = -1;
                mat.ShaderBinaryKind = "Default";
                mats.Add(mat);
            }

            //Meshes
            List<ctrMesh> meshes = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.Meshes;
            ctrMesh mesh;
            int i = 0;
            foreach(var m in mdl.mesh) {
                mesh = new ctrMesh();
                mesh.IsVisible = m.isVisible;
                mesh.RenderPriority = m.renderPriority;
                mesh.MeshNodeName = m.name;
                mesh.SeparateShapeReference = "Shapes[" + i++ + "]";
                mesh.MaterialReference = "Materials[\"" + m.name + "\"]";
                meshes.Add(mesh);
            }

            //Mesh Node Visibility
            List<ctrMeshVis> meshVisabilites = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.MeshNodeVisibilities;
            ctrMeshVis meshVisability;
            foreach (var vis in mdl.mesh) {
                meshVisability = new ctrMeshVis();
                meshVisability.Name = vis.name;
                meshVisability.IsVisible = true;
                meshVisabilites.Add(meshVisability);
            }

            //Skeleton
            ctrSkeleton skeleton = ctrMdl.GraphicsContentCtr.Models.SkeletalModel.Skeleton;
            List<ctrBone> bones = new List<ctrBone>();
            ctrBone bone;
            skeleton.RootBoneName = mdl.skeleton[0].name; //Not sure if this always holds true
            skeleton.ScalingRule = "Standard";
            skeleton.IsTranslateAnimationEnabled = true;
            skeleton.Bones = bones;
            foreach (var b in mdl.skeleton) {
                bone = new ctrBone();
                bone.Name = b.name;
                bone.ParentBoneName = b.parentId == -1 ? "" : mdl.skeleton[b.parentId].name;
                bone.IsSegmentScaleCompensate = b.isSegmentScaleCompensate;
                bone.IsCompressible = true;
                bone.IsNeededRendering = false;
                bone.HasSkinningMatrix = true;
                bone.BillboardMode = b.billboardMode;
                bone.Transform.Rotation.X = b.rotation.x;
                bone.Transform.Rotation.Y = b.rotation.y;
                bone.Transform.Rotation.Z = b.rotation.z;
                bone.Transform.Scale.X = b.scale.x;
                bone.Transform.Scale.Y = b.rotation.y;
                bone.Transform.Scale.Z = b.scale.z;
                bone.Transform.Translate.X = b.translation.x;
                bone.Transform.Translate.Y = b.translation.y;
                bone.Transform.Translate.Z = b.translation.z;
                bones.Add(bone);
            }

            //XML Serializer
            XmlWriterSettings settings = new XmlWriterSettings {
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "\t"
            };
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(CtrModel));
            XmlWriter output = XmlWriter.Create(new FileStream(fileName, FileMode.Create), settings);
            serializer.Serialize(output, ctrMdl, ns);
            output.Close();
        }
    }
}