using System.Collections.Generic;
using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth.Ohana.Textures.PocketMonsters
{
    class PT
    {
        public static List<RenderBase.OTexture> load(string file)
        {
            return load(File.Open(file, FileMode.Open));
        }

        /// <summary>
        ///     Loads all monster textures on a PT Pokémon container.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The monster textures</returns>
        public static List<RenderBase.OTexture> load(Stream data)
        {
            List<RenderBase.OTexture> textures = new List<RenderBase.OTexture>();
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            OContainer container = PkmnContainer.load(data);
            for (int i = 0; i < container.content.Count; i++)
            {
                FileIO.file file = FileIO.load(new MemoryStream(container.content[i].data));
                if (file.type == FileIO.formatType.model) textures.AddRange(((RenderBase.OModelGroup)file.data).texture);
            }

            return textures;
        }
    }
}