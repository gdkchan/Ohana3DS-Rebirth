using Ohana3DS_Rebirth.Ohana.Containers;

using System;
using System.Diagnostics;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class PC
    {
        public static RenderBase.OModelGroup load(string file)
        {
            RenderBase.OModelGroup group = load(File.Open(file, FileMode.Open));

            return group;
        }

        /// <summary>
        ///     Loads a PC monster model from Pokémon.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The Model group with the monster meshes</returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            OContainer container = PkmnContainer.load(data);

            foreach (OContainer.fileEntry file in container.content)
            {
                FileIO.file loaded = new FileIO.file();

                try
                {
                    loaded = FileIO.load(new MemoryStream(file.data));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(string.Format("Error opening file:\n{0}\n{1}", e.Message, e.StackTrace));
                }

                if (loaded.data == null) continue;
                
                switch (loaded.type)
                {
                    case FileIO.formatType.model: models.merge((RenderBase.OModelGroup)loaded.data); break;
                    case FileIO.formatType.anims: models.skeletalAnimation.list.Add((RenderBase.OSkeletalAnimation)loaded.data); break;
                    case FileIO.formatType.image: models.texture.Add((RenderBase.OTexture)loaded.data); break;
                }
            }

            return models;
        }
    }
}
