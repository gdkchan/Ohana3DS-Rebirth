using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class GR
    {
        /// <summary>
        ///     Loads a GR map model from Pokémon.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The Model group with the map meshes</returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup models;

            OContainer container = PkmnContainer.load(data);
            models = BCH.load(new MemoryStream(container.content[1].data));

            return models;
        }
    }
}
