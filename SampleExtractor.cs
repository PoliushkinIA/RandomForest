using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomForest
{
    class SampleExtractor
    {
        List<Sample> ExtractedSamplesList;
       public delegate void SetStatusDelegate(string message);
        SetStatusDelegate sSDInstance;
        bool needNotification;
        public SampleExtractor(SetStatusDelegate sSDInputInstatnce,bool allowNotification)
        {
            sSDInstance = sSDInputInstatnce;
            needNotification = allowNotification;
        }
        public SamplesContainer ExtractSamples(string filePath)
        {
            int samplesCounter = 0;
            ExtractedSamplesList = new List<Sample>();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                string curStr;
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    do { curStr = sr.ReadLine(); } while (curStr != "#>");
                    Sample NewSample = new Sample();
                    do
                    {
                        curStr = sr.ReadLine();
                    if (curStr == "") continue;
                        if (curStr.IndexOf(':') > 0)
                        {
                            NewSample.SetAttribute(curStr.Substring(0, curStr.IndexOf(':')), curStr.Substring(curStr.IndexOf(':') + 1, curStr.Length - curStr.IndexOf(':') - 1));
                            continue;
                        }
                        if (curStr[0]=='*')
                        {
                            string curSubStr = curStr.Substring(1, curStr.Length - 1);
                            NewSample.SetClassLabel(curSubStr);
                            continue;
                        }
                        if (curStr.IndexOf('/') >= 0) 
                            NewSample.SetName(curStr.Substring(curStr.LastIndexOf('/')+1, curStr.Length - curStr.LastIndexOf('/') - 1));
                    } while (curStr != "<#");
                    samplesCounter++;
                    ExtractedSamplesList.Add(NewSample);

                }
                SamplesContainer newSCInstance = new SamplesContainer(ExtractedSamplesList);
                
                if(needNotification)
                
                sSDInstance("Samples succesfully extracted. Number of sumples is " + newSCInstance.samplesList.Count()+".");
                return newSCInstance;
            }
            catch(Exception ex)
            {
                sSDInstance(ex.TargetSite+"\n"+ex.Message);
                return null;
            }
        }
        public SamplesContainer ReturnSamples()
        {
            SamplesContainer newSCInstance = new SamplesContainer(ExtractedSamplesList);
            return newSCInstance;
        }
    }
}
