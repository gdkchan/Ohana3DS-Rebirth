/*
 * Parser for the XML files created by the NLPUnpacker tool from SERI files.
 */

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.NewLovePlus
{
    public class Serialization
    {
        [XmlInclude(typeof(String))]
        [XmlInclude(typeof(Integer))]
        [XmlInclude(typeof(Boolean))]
        [XmlInclude(typeof(Float))]
        [XmlInclude(typeof(StringArray))]
        [XmlInclude(typeof(IntegerArray))]
        [XmlInclude(typeof(BooleanArray))]
        [XmlInclude(typeof(FloatArray))]
        [XmlInclude(typeof(NestedArray))]
        public class SERIParameter
        {
            [XmlAttribute]
            public string Name;
        }

        public class String : SERIParameter
        {
            public string Value;
        }

        public class Integer : SERIParameter
        {
            public int Value;
        }

        public class Boolean : SERIParameter
        {
            public bool Value;
        }

        public class Float : SERIParameter
        {
            public float Value;
        }

        public class StringArray : SERIParameter
        {
            [XmlArrayItem("Value")]
            public string[] Values;
        }

        public class IntegerArray : SERIParameter
        {
            [XmlArrayItem("Value")]
            public int[] Values;
        }

        public class BooleanArray : SERIParameter
        {
            [XmlArrayItem("Value")]
            public bool[] Values;
        }

        public class FloatArray : SERIParameter
        {
            [XmlArrayItem("Value")]
            public float[] Values;
        }

        public class NestedArray : SERIParameter
        {
            [XmlArrayItem("Parameter")]
            public SERIParameter[] Values;
        }

        [XmlRoot]
        public class SERI
        {
            [XmlArrayItem("Parameter")]
            public List<SERIParameter> Parameters = new List<SERIParameter>();

            /// <summary>
            ///     Grabs a Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public SERIParameter getParameter(string name)
            {
                foreach (SERIParameter param in Parameters) if (param.Name == name) return param;
                return null;
            }

            /// <summary>
            ///     Grabs the value of a String Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public string getStringParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return null;
                return ((String)param).Value;
            }

            /// <summary>
            ///     Grabs the value of a Integer Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public int getIntegerParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return 0;
                return ((Integer)param).Value;
            }

            /// <summary>
            ///     Grabs the value of a Boolean Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public bool getBooleanParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return false;
                return ((Boolean)param).Value;
            }

            /// <summary>
            ///     Grabs the value of a Float Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public float getFloatParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return 0;
                return ((Float)param).Value;
            }

            /// <summary>
            ///     Grabs the values of a String Array Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public string[] getStringArrayParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return null;
                return ((StringArray)param).Values;
            }

            /// <summary>
            ///     Grabs the values of a Integer Array Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public int[] getIntegerArrayParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return null;
                return ((IntegerArray)param).Values;
            }

            /// <summary>
            ///     Grabs the values of a Boolean Array Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public bool[] getBooleanArrayParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return null;
                return ((BooleanArray)param).Values;
            }

            /// <summary>
            ///     Grabs the values of a Float Array Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public float[] getFloatArrayParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return null;
                return ((FloatArray)param).Values;
            }

            /// <summary>
            ///     Grabs the values of a Nested Array Parameter with the given name.
            /// </summary>
            /// <param name="data">The SERI data</param>
            /// <param name="name">The name of the parameter</param>
            /// <returns></returns>
            public SERIParameter[] getNestArrayParameter(string name)
            {
                SERIParameter param = getParameter(name);
                if (param == null) return null;
                return ((NestedArray)param).Values;
            }
        }

        /// <summary>
        ///     Reads a SERI from a XML file.
        /// </summary>
        /// <param name="xmlFileName">The Full Path of the XML file</param>
        /// <returns></returns>
        public static SERI getSERI(string xmlFileName)
        {
            using (FileStream input = new FileStream(xmlFileName, FileMode.Open))
            {
                return getSERI(input);
            }
        }

        /// <summary>
        ///     Reads a SERI from a XML file.
        /// </summary>
        /// <param name="xmlData">The Stream of the XML file</param>
        /// <returns></returns>
        public static SERI getSERI(Stream xmlData)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(SERI));
            SERI output = (SERI)deserializer.Deserialize(xmlData);
            xmlData.Dispose();
            return output;
        }
    }
}
