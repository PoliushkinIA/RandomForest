using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForest
{
    public class Sample
    {
        public Dictionary<String, String> Atributes;
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
        public string GetAttribute(string Key)
        {
            return Atributes[Key];
        }
    }
}
