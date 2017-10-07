using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForest
{
    public class SamplesContainer
    {
        public Dictionary<string, List<string>> SamplesDomain;
        public List<Sample> SamplesList;

        public SamplesContainer(List<Sample> inputSamplesList)
        {
            SamplesList = new List<Sample>();
            SamplesDomain = new Dictionary<string, List<string>>();
            SamplesList = inputSamplesList;
            CalculateDomain();
        }

        public bool CalculateDomain()
        {
            if (SamplesList.Count == 0) return false;
            
            foreach(Sample s in SamplesList)
            {
                foreach (string Key in s.Atributes.Keys)
                {
                    if (!SamplesDomain.ContainsKey(Key))
                    {
                        List<string> atrList = new List<string>();
                        SamplesDomain.Add(Key,atrList);
                        SamplesDomain[Key].Add(s.Atributes[Key]);
                    }else if(!SamplesDomain[Key].Contains(s.Atributes[Key])) SamplesDomain[Key].Add(s.Atributes[Key]);
                }
            }

            return true;
        }
    }
}
