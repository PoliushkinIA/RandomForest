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
        Label statusLabel;
        SampleExtractor(Label inpStatusLabel) {
           statusLabel = inpStatusLabel;
        }

        public bool GetSamples(string fileName)
        {
            string path;
            int samplesCounter = 0;
            path = Directory.GetCurrentDirectory() + fileName;
            
                try
                {
                   StreamReader sr = new StreamReader(path) ;
                    while (!sr.EndOfStream)
                    {
                        string curStr = sr.ReadLine();

                    }
                statusLabel.Content = "Trainig samples has succesfully extracted.\nTotal number of trainig samples is " + samplesCounter;
                }
                catch
                {
                statusLabel.Content = "Error. Check the correctness of the path and file name.";
                }
            return true;
        }    
    }
}
