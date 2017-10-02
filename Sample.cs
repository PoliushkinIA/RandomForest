using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForest
{
    public class Sample
    {
        public string SampleName;
        public Dictionary<String, String> Atributes;
        public string ClassLabel;
        public Sample()
        {
            Atributes = new Dictionary<string, string>(15);
        }
        public bool SetAttribute(string Key, string Value)
        {
            if (!this.Atributes.ContainsKey(Key))
            {
                this.Atributes.Add(Key, Value);
                return true;
            }
            return false;
        }
        public bool SetName(string name)
        {
            if (name != "")
            {
                SampleName = name;
                return true;
            }
            else return false;
        }
        public bool SetClassLabel(string Class)
        {
            if (Class == "Yes" || Class == "No")
            {
                ClassLabel = Class;
                return true;
            }
            else return false;
        }
        public string GetAttribute(string Key)
        {
            return Atributes[Key];
        }
    }
}
