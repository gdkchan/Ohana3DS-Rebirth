using System;
using System.Collections.Generic;
using System.IO;
using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;

namespace Ohana3DS_Rebirth
{
    class BatchMode
    {
        // private List<FileIO.file> files = new List<FileIO.file>();
        private string[] filenames;
        
        public void openFolder(string path)
        {
            filenames = Directory.GetFiles(path);
            
            //foreach(string filename in filenames)
            //{
            //    files.Add(FileIO.load(filename));
            //}
        }

        public void exportModels(string destination, int format)
        {
            if (!Directory.Exists(destination))
            {
                System.Console.Error.WriteLine("Invalid output directory!");
                Environment.Exit(-1);
            }

            foreach (string filename in filenames)
            {
                var file = FileIO.load(filename);
                var data = (RenderBase.OModelGroup)file.data;
                for (int i = 0; i < data.model.Count; i++)
                {
                    string fileName = Path.Combine(destination, Path.GetFileNameWithoutExtension(filename) + data.model[i].name);
                    switch (format)
                    {
                        case 0: DAE.export(data, fileName + ".dae", i); break;
                        case 1: SMD.export(data, fileName + ".smd", i); break;
                        case 2: OBJ.export(data, fileName + ".obj", i); break;
                        case 3: CMDL.export(data, fileName + ".cmdl", i); break;
                    }
                }
            }
            Console.WriteLine("All " + filenames.Length + " models exported.");
        }

        public void exportTextures(string destination)
        {
            if (!Directory.Exists(destination))
            {
                System.Console.Error.WriteLine("Invalid output directory!");
                Environment.Exit(-1);
            }

            foreach (string filename in filenames)
            {
                var file = FileIO.load(filename);
                var data = (RenderBase.OModelGroup)file.data;
                foreach (RenderBase.OTexture tex in data.texture)
                {
                    string fileName = Path.Combine(destination, Path.GetFileNameWithoutExtension(filename) + tex.name) + ".png";
                    tex.texture.Save(fileName);
                }
            }
            Console.WriteLine("Textures for all " + filenames.Length + " models exported.");
        }
    }
}
