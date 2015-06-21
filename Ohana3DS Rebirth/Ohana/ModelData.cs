using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ohana3DS_Rebirth.Ohana
{
    public sealed class ModelData
    {
        public static ModelData inst = new ModelData();

        private ModelData() { }
        public RenderBase.OModelGroup model {get;set;}
    }
}
