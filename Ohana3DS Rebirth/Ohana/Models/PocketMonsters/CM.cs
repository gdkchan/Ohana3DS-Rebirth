using Ohana3DS_Rebirth.Ohana.Containers;

using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class CM
    {
        /// <summary>
        ///     Loads a MM overworld chibi character model from Pokémon.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The Model group with the character meshes</returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            OContainer container = PkmnContainer.load(data);
            models = Gen7.load(new MemoryStream(container.content[0].data));

            return models;
        }
    }
}
