using Ohana3DS_Rebirth.Ohana.Containers;

using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class CP
    {
        /// <summary>
        ///     Loads a CP overworld character model from Pokémon.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The Model group with the character meshes</returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            OContainer container = PkmnContainer.load(data);
            models = CM.load(new MemoryStream(container.content[1].data));

            return models;
        }
    }
}
