//New Love Plus public Model loading methods.
using System.IO;

using Ohana3DS_Rebirth.Ohana.Models.NewLovePlus;

namespace Ohana3DS_Rebirth.Ohana.Models
{
    class NLP
    {
        /// <summary>
        ///     Loads a New Love Plus Model file.
        /// </summary>
        /// <param name="fileName">File Name of the Model file</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(string fileName)
        {
            return Model.load(fileName);
        }

        /// <summary>
        ///     Loads a New Love Plus Mesh file.
        ///     Only the raw Mesh data will be loaded, without materials, skeleton or textures.
        /// </summary>
        /// <param name="data">Stream of the Mesh</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup loadMesh(Stream data)
        {
            RenderBase.OModelGroup models = new RenderBase.OModelGroup();
            RenderBase.OModel model = new RenderBase.OModel();
            Mesh.load(data, model, true);

            model.material.Add(new RenderBase.OMaterial());
            models.model.Add(model);
            return models;
        }
    }
}
