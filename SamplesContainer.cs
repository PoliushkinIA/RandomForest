using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForest
{
    public class SamplesContainer
    {
        public Dictionary<string, List<string>> samplesDomain;
        public List<Sample> samplesList;
        public List<string> classLabels;
        public List<string> attributes;

        public SamplesContainer(List<Sample> inputSamplesList)
        {
            samplesList = new List<Sample>();
            samplesDomain = new Dictionary<string, List<string>>();
            classLabels = new List<string>();
            samplesList = inputSamplesList;
            attributes = new List<string>();
            CalculateDomain();
        }

        public bool CalculateDomain()
        {
            if (samplesList.Count == 0) return false;

            foreach (string a in samplesList[0].Atributes.Keys)
                if (!attributes.Contains(a))
                    attributes.Add(a);
            
            foreach(Sample s in samplesList)
            {
                foreach (string Key in s.Atributes.Keys)
                {
                    if (!samplesDomain.ContainsKey(Key))
                    {
                        List<string> atrList = new List<string>();
                        samplesDomain.Add(Key,atrList);
                        samplesDomain[Key].Add(s.Atributes[Key]);
                    }else if(!samplesDomain[Key].Contains(s.Atributes[Key])) samplesDomain[Key].Add(s.Atributes[Key]);
                }

                if (!classLabels.Contains(s.ClassLabel))
                {
                    classLabels.Add(s.ClassLabel);
                }
            }

            return true;
        }
    }
}
