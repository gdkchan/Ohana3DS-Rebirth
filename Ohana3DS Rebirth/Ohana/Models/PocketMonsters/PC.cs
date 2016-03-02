using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class PC
    {
        /// <summary>
        ///  Seems to add a context menu option in file/open. Need to test a build.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(string file)
        {
            return load(File.Open(file, FileMode.Open));
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
            models = BCH.load(new MemoryStream(container.content[0].data));

            return models;
        }
    }
}
