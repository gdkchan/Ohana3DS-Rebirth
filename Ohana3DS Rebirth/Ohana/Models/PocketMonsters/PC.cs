using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class PC
    {
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
                FileIO.file loaded = FileIO.load(new MemoryStream(file.data));

                if (loaded.data == null) continue;
                
                switch (loaded.type)
                {
                    case FileIO.formatType.model: models.merge((RenderBase.OModelGroup)loaded.data); break;
                    case FileIO.formatType.image: models.texture.Add((RenderBase.OTexture)loaded.data); break;
                }
            }

            return models;
        }
    }
}
