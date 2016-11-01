using Ohana3DS_Rebirth.Ohana.Animations;
using Ohana3DS_Rebirth.Ohana.Containers;
using System.Collections.Generic;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class CM
    {
        /// <summary>
        ///     Loads a CM overworld character model from Pokémon.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The Model group with the character meshes</returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            OContainer container = PkmnContainer.load(data);
            models = GfModel.load(new MemoryStream(container.content[0].data));

            List<RenderBase.OSkeletalAnimation> anms = GfMotion.load(new MemoryStream(container.content[1].data));

            foreach (RenderBase.OSkeletalAnimation anm in anms)
            {
                models.skeletalAnimation.list.Add(anm);
            }

            return models;
        }
    }
}
