using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana.Models;

namespace Ohana3DS_Rebirth.Ohana.Animations
{
    class PB
    {
        public static RenderBase.OModelGroup load(string fileName)
        {
            return load(new MemoryStream(File.ReadAllBytes(fileName)));
        }

        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup group = new RenderBase.OModelGroup();
            RenderBase.OModelGroup tempGroup;
            byte[] buffer;

            BinaryReader input = new BinaryReader(data);

            data.Seek(8, SeekOrigin.Begin);

            uint begin;
            uint end;
            uint length;

            bool eof = false;

            begin = 0;
            end = 0;

            for (int i = 0; eof == false; i++)
            {
                try
                {
                    data.Seek(12 + i * 8, SeekOrigin.Begin);
                }
                catch
                {
                    eof = true;
                }

                try
                {
                    begin = input.ReadUInt32();
                    end = input.ReadUInt32();

                    if (begin < data.Length && end < data.Length)
                    {
                        //PB files seem to vary in their order of variables.
                        if (end > begin)
                        {
                            length = end - begin;
                        }
                        else
                        {
                            length = begin - end;
                        }

                        if (length > 0)
                        {
                            if (end > begin)
                            {
                                data.Seek(begin, SeekOrigin.Begin);
                            }
                            else
                            {
                                data.Seek(end, SeekOrigin.Begin);
                            }

                            buffer = new byte[length];
                            input.Read(buffer, 0, (int)length);

                            if (buffer[0] == 0x42 && buffer[1] == 0x43 && buffer[2] == 0x48)
                            {
                                tempGroup = BCH.load(new MemoryStream(buffer));

                                for (int j = 0; j < tempGroup.skeletalAnimation.list.Count; j++)
                                {
                                    group.skeletalAnimation.list.Add(tempGroup.skeletalAnimation.list[j]);
                                }

                                for (int j = 0; j < tempGroup.materialAnimation.list.Count; j++)
                                {
                                    group.materialAnimation.list.Add(tempGroup.materialAnimation.list[j]);
                                }

                                for (int j = 0; j < tempGroup.visibilityAnimation.list.Count; j++)
                                {
                                    group.visibilityAnimation.list.Add(tempGroup.visibilityAnimation.list[j]);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    eof = true;
                }
            }

            data.Close();

            if (group.skeletalAnimation.list.Count > 0)
            {
                MessageBox.Show("This animation file contains skeletal animations.");
            }

            return group;
        }
    }
}
