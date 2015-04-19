using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ohana3DS_Rebirth.Ohana
{
    class BCH
    {
        private enum Light_Type
        {
            Point,
            Directional
        }
        private struct BCH_Header
        {
            public string Magic;
            public byte Backward_Compatibility;
            public byte Foward_Compatibility;
            public UInt16 Version;

            public UInt32 Main_Header_Offset;
            public UInt32 Commands_Offset;
            public UInt32 Description_Offset;
            public UInt32 Data_Offset;
            public UInt32 Data_Extended_Offset;
            public UInt32 Relocatable_Table_Offset;

            public UInt32 Main_Header_Length;
            public UInt32 Commands_Length;
            public UInt32 Description_Length;
            public UInt32 Data_Length;
            public UInt32 Data_Extended_Length;
            public UInt32 Relocatable_Table_Length;
            public UInt32 Unused_Data_Section_Length;
            public UInt32 Unused_Description_Section_Length;

            public UInt16 Flags;
            public UInt16 Address_Count;
        }

        private struct BCH_Content_Header
        {
            public UInt32 Objects_Header_Pointer_Offset;
            public UInt32 Objects_Header_Pointer_Entries;
            public UInt32 Objects_Shadow_Name_Offset;
            public UInt32 Objects_Shadow_Offset;
            public UInt32 Objects_Shadow_Entries;
            public UInt32 Materials_Offset;
            public UInt32 Materials_Entries;
            public UInt32 Unknow_1_Offset;
            public UInt32 Unknow_1_Entries;
            public UInt32 Unknow_2_Name_Offset;
            public UInt32 Unknow_2_Offset;
            public UInt32 Unknow_2_Entries;
        }
        private struct BCH_Command
        {
            public string Command;
            public UInt32 Unknow; //Sempre maior que 0x0 e menor que 0xFF
            public UInt32 Flags;
        }
        private struct BCH_Object_Shadow
        {
            public UInt32 Unknow_3;
            public UInt32 Unknow_4;
            public Vector3 Unknow_1_1;
            public Vector3 Unknow_1_2;
            public Vector3 Unknow_2_1;
            public Vector3 Unknow_2_2;
        }

        private struct BCH_Objects_Header
        {
            public byte Flags;
            public byte Skeleton_Scaling_Type;
            public UInt16 Shadow_Material_Entries;
            public Matrix World_Transform;

            public UInt32 Textures_Table_Offset;
            public UInt32 Textures_Table_Entries;
            public UInt32 Objects_Name_Offset;
            public UInt32 Vertices_Table_Offset;
            public UInt32 Vertices_Table_Entries;
            public UInt32 Skeleton_Offset;
            public UInt32 Skeleton_Entries;
        }

        private struct BCH_Object_Entry
        {
            public UInt32 Texture_ID;
            public UInt32 Visibility_Group;
            public UInt32 Vertices_Offset;
            public UInt32 Unknow_Count;
            public UInt32 Faces_Offset;
            public UInt32 Faces_Entries;
            public UInt32 Nodes_Offset;
            public UInt32 Nodes_Entries;
            public Vector3 Unknow;

            //Dados das sub-tabelas
        }

        public static bool Load(string File_Name)
        {
            FileStream Data = new FileStream(File_Name, FileMode.Open);
            BinaryReader Input = new BinaryReader(Data);

            /*+=================+
              | Header primário |
              +=================+*/
            BCH_Header Header = new BCH_Header();
            Header.Magic = IOUtils.Read_String(Data, 0);
            Header.Backward_Compatibility = Input.ReadByte();
            Header.Foward_Compatibility = Input.ReadByte();
            Header.Version = Input.ReadUInt16();

            Header.Main_Header_Offset = Input.ReadUInt32();
            Header.Commands_Offset = Input.ReadUInt32();
            Header.Description_Offset = Input.ReadUInt32();
            Header.Data_Offset = Input.ReadUInt32();
            Header.Data_Extended_Offset = Input.ReadUInt32();
            Header.Relocatable_Table_Offset = Input.ReadUInt32();

            Header.Main_Header_Length = Input.ReadUInt32();
            Header.Commands_Length = Input.ReadUInt32();
            Header.Description_Length = Input.ReadUInt32();
            Header.Data_Length = Input.ReadUInt32();
            Header.Data_Extended_Length = Input.ReadUInt32();
            Header.Relocatable_Table_Length = Input.ReadUInt32();
            Header.Unused_Data_Section_Length = Input.ReadUInt32();
            Header.Unused_Description_Section_Length = Input.ReadUInt32();

            Header.Flags = Input.ReadUInt16();
            Header.Address_Count = Input.ReadUInt16();

            /*+==================+
              | Header principal |
              +==================+*/
            Data.Seek(Header.Main_Header_Offset, SeekOrigin.Begin);
            BCH_Content_Header Content_Header = new BCH_Content_Header();
            Content_Header.Objects_Header_Pointer_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Objects_Header_Pointer_Entries = Input.ReadUInt32();
            Content_Header.Objects_Shadow_Name_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Objects_Shadow_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Objects_Shadow_Entries = Input.ReadUInt32();
            Content_Header.Materials_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Materials_Entries = Input.ReadUInt32();
            Input.ReadUInt32(); //??? (0x0) ^ Provavelmente relacionado ao material
            Input.ReadUInt32(); //Unused?
            Content_Header.Unknow_1_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Unknow_1_Entries = Input.ReadUInt32();
            Content_Header.Unknow_2_Name_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Unknow_2_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Content_Header.Unknow_2_Entries = Input.ReadUInt32();
            Input.ReadUInt32(); //Nome vazio
            //(O resto não é usado, apenas aponta para um campo vazio de 0x0)

            //Objetos
            Data.Seek(Content_Header.Objects_Header_Pointer_Offset, SeekOrigin.Begin);
            UInt32 Objects_Header_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;

            //Nomes das sombras
            List<BCH_Command> Shadows_Name = Get_Commands(Header, Content_Header, Input, Content_Header.Objects_Shadow_Name_Offset);
            
            //Sombra dos objetos?
            List<BCH_Object_Shadow> Objects_Shadow = new List<BCH_Object_Shadow>();
            for (int Index = 0; Index < Content_Header.Objects_Shadow_Entries; Index++)
            {
                BCH_Object_Shadow Object_Shadow;

                Data.Seek(Content_Header.Objects_Shadow_Offset + (Index * 4), SeekOrigin.Begin);
                UInt32 Data_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
                Data.Seek(Data_Offset, SeekOrigin.Begin);

                Object_Shadow.Unknow_3 = Input.ReadUInt32();
                Object_Shadow.Unknow_4 = Input.ReadUInt32(); //Alterar isso deixa os personagens pretos!!!!

                Object_Shadow.Unknow_1_1 = new Vector3(Input.ReadSingle(), Input.ReadSingle(), Input.ReadSingle());
                Object_Shadow.Unknow_1_2 = new Vector3(Input.ReadSingle(), Input.ReadSingle(), Input.ReadSingle());
                Object_Shadow.Unknow_2_1 = new Vector3(Input.ReadSingle(), Input.ReadSingle(), Input.ReadSingle()); //Flag 0x10?
                Object_Shadow.Unknow_2_2 = new Vector3(Input.ReadSingle(), Input.ReadSingle(), Input.ReadSingle());
            }

            //Materials (Textura@Material)
            List<BCH_Command> Materials_Name = Get_Commands(Header, Content_Header, Input, Content_Header.Materials_Offset);

            //Unknow 1
            for (int Index = 0; Index < Content_Header.Unknow_1_Entries; Index++)
            {
                Data.Seek(Content_Header.Unknow_1_Offset + (Index * 4), SeekOrigin.Begin);
                UInt32 Data_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
                Data.Seek(Data_Offset, SeekOrigin.Begin);

                //8 bytes por entrada
                UInt32 Offset = Input.ReadUInt32(); //Offset para ? (tamanho 0x30)
                UInt32 Unknow = Input.ReadUInt32(); //???
            }

            //Unknow 2
            for (int Index = 0; Index < Content_Header.Unknow_2_Entries; Index++)
            {
                Data.Seek(Content_Header.Unknow_2_Offset + (Index * 4), SeekOrigin.Begin);
                UInt32 Data_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
                Data.Seek(Data_Offset, SeekOrigin.Begin);

                //8 bytes por entrada
                UInt32 Offset = Input.ReadUInt32(); //Offset para ? (tamanho 0x30)
                UInt32 Unknow = Input.ReadUInt32(); //???
            }

            /*+====================+
              | Header dos objetos |
              +====================+*/
            Data.Seek(Objects_Header_Offset, SeekOrigin.Begin);
            BCH_Objects_Header Objects_Header;
            Objects_Header.Flags = Input.ReadByte();
            Objects_Header.Skeleton_Scaling_Type = Input.ReadByte();
            Objects_Header.Shadow_Material_Entries = Input.ReadUInt16();

            Objects_Header.World_Transform.M11 = Input.ReadSingle();
            Objects_Header.World_Transform.M12 = Input.ReadSingle();
            Objects_Header.World_Transform.M13 = Input.ReadSingle();
            Objects_Header.World_Transform.M14 = Input.ReadSingle();

            Objects_Header.World_Transform.M21 = Input.ReadSingle();
            Objects_Header.World_Transform.M22 = Input.ReadSingle();
            Objects_Header.World_Transform.M23 = Input.ReadSingle();
            Objects_Header.World_Transform.M24 = Input.ReadSingle();

            Objects_Header.World_Transform.M31 = Input.ReadSingle();
            Objects_Header.World_Transform.M32 = Input.ReadSingle();
            Objects_Header.World_Transform.M33 = Input.ReadSingle();
            Objects_Header.World_Transform.M34 = Input.ReadSingle();

            MessageBox.Show(Input.BaseStream.Position.ToString("X8"));
            Objects_Header.Textures_Table_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Objects_Header.Textures_Table_Entries = Input.ReadUInt32();
            Objects_Header.Objects_Name_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Objects_Header.Vertices_Table_Offset = Input.ReadUInt32() + Header.Main_Header_Offset;
            Objects_Header.Vertices_Table_Entries = Input.ReadUInt32();
            Input.ReadUInt32(); //Vertice Table Offset again?
            Input.ReadUInt32(); //Skeleton offset COMMAND TABLE ALSO
            Input.ReadUInt32(); //Skeleton offset (repeat) COMMAND TABLE ALSO REPEAT
            Input.ReadUInt32(); //Skeleton offset (repeat) ! no SK muda aqui
            Input.ReadUInt32(); //Skeleton offset (repeat)
            Input.ReadUInt32(); //Skeleton offset (repeat)
            Input.ReadUInt32(); //Skeleton offset (repeat)
            Input.ReadUInt32(); //Skeleton offset (repeat)
            Input.ReadUInt32(); //0x0
            Input.ReadUInt32(); //0x0
            Input.ReadUInt32(); //Aponta para estrutura de Vector3
            Objects_Header.Skeleton_Entries = Input.ReadUInt32(); //ex: 0x4F
            Objects_Header.Skeleton_Offset = Input.ReadUInt32() + Header.Main_Header_Offset; //Skeleton offset (repeat)
            Input.ReadUInt32(); //Aponta para 0xFFFF e mais nada
            Input.ReadUInt32(); //Count? (ex: 0x10)
            Input.ReadUInt32(); //Aponta para nome da tabela de nomes (ex: rsbt0001_00)
            Input.ReadUInt32(); //Count? (ex: 0x10)
            Input.ReadUInt32(); //Aponta para uma estrutura de comandos localizada logo após o 0xFFFF acima
            Input.ReadUInt32(); //0x0
            Input.ReadUInt32(); //Último offset

            MessageBox.Show(Objects_Header.Skeleton_Entries.ToString("X8"));

            /*+=====================+
              | Header dos vertices |
              +=====================+*/
            Data.Seek(Objects_Header.Vertices_Table_Offset, SeekOrigin.Begin);
            List<BCH_Object_Entry> Objects = new List<BCH_Object_Entry>();

            for (int Index = 0; Index < Objects_Header.Vertices_Table_Entries; Index++)
            {
                BCH_Object_Entry Object_Entry;
                Object_Entry.Texture_ID = Input.ReadUInt32();
                Object_Entry.Visibility_Group = Input.ReadUInt32();
                Object_Entry.Vertices_Offset = Input.ReadUInt32() + Header.Description_Offset;
                Object_Entry.Unknow_Count = Input.ReadUInt32(); //ex: 0x24
                Object_Entry.Faces_Offset = Input.ReadUInt32();
                Object_Entry.Faces_Entries = Input.ReadUInt32();
                Object_Entry.Nodes_Offset = Input.ReadUInt32(); //???
                Object_Entry.Nodes_Entries = Input.ReadUInt32(); //???
                Object_Entry.Unknow = new Vector3(Input.ReadSingle(), Input.ReadSingle(), Input.ReadSingle());
                Input.ReadUInt32(); //ex: 0x278
                Input.ReadUInt32(); //0x0
                Input.ReadUInt32(); //Ponteiro para dados estranhos, mas parece alguns offsets e depois floats
                
                //Sub-tabelas

                
                Objects.Add(Object_Entry);
            }

            return true;
        }

        private static List<BCH_Command> Get_Commands(BCH_Header Header, BCH_Content_Header Content_Header, BinaryReader Input, UInt32 Table_Offset)
        {
            Input.BaseStream.Seek(Table_Offset, SeekOrigin.Begin);
            Input.ReadUInt32(); //0xFFFFFFFF
            UInt32 Entries = Input.ReadUInt32();
            List<BCH_Command> Out = new List<BCH_Command>();
            for (int Index = 0; Index < Entries; Index++)
            {
                BCH_Command Command;

                Input.BaseStream.Seek(Table_Offset + 8 + (Index * 12), SeekOrigin.Begin);
                UInt32 Name_Offset = Input.ReadUInt32();
                if (Name_Offset != 0)
                {
                    Command.Unknow = Input.ReadUInt32();
                    Command.Flags = Input.ReadUInt32();
                    Command.Command = IOUtils.Read_String(Input, Header.Commands_Offset + Name_Offset);
                    Out.Add(Command);
                }
            }

            return Out;
        }
    }
}
