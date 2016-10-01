using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;
using Ohana3DS_Rebirth.Ohana.Models;
using System.Collections.Generic;

namespace Ohana3DS_Rebirth.Ohana.Animations.PocketMonsters
{
    class BS
    {
        /// <summary>
        ///     Loads a BS animation file from Pokémon.
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The Model group with the animations</returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            List<RenderBase.OModelGroup> models = new List<RenderBase.OModelGroup>();
            OContainer naCont = PkmnContainer.load(data); //Get NA containers from BS
            for (int i = 1; i < naCont.content.Count; i++) { //Skip first entry because its not a NA (TODO: figure out this data)
                OContainer bchCont = PkmnContainer.load(new MemoryStream(naCont.content[1].data)); //Get BCH from NA containers
                models.Add(BCH.load(new MemoryStream(bchCont.content[0].data)));
            }

            return models[0]; //TODO: Figure out how to load all anim BCHs
        }
    }
}
