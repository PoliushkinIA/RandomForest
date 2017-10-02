using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RandomForest
{
    /// <summary>
    /// Логика взаимодействия для SamplesBrowseWindow.xaml
    /// </summary>
    public partial class SamplesBrowseWindow : Window
    {
        List<Sample> SamplesList;
        public SamplesBrowseWindow(List<Sample> Samples)
        {
            InitializeComponent();
            SamplesList = Samples;
            List<string> CBList = new List<string>(Samples.Count);
            for (int i = 0; i < Samples.Count; i++) CBList.Add(i.ToString());
            SampleComboBox.ItemsSource = CBList;
            SampleComboBox.SelectedIndex = 0;
            OutputSelectedSample();
        }

        private void SampleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OutputSelectedSample();
        }

        void OutputSelectedSample()
        {
            Sample sample = SamplesList[SampleComboBox.SelectedIndex];
            SampleTextBlock.Text = "";
            SampleTextBlock.Text += "Sample name: " + sample.SampleName + "\n";
            SampleTextBlock.Text += "  Continent: " + (sample.Atributes.ContainsKey("Continent")? sample.GetAttribute("Continent"):"-") + "\n";
            SampleTextBlock.Text += "  Landscape: " + (sample.Atributes.ContainsKey("Landscape") ? sample.GetAttribute("Landscape") : "-") + "\n";
            SampleTextBlock.Text += "  Substratum: "+ (sample.Atributes.ContainsKey("Substratum") ? sample.GetAttribute("Substratum") : "-") + "\n";
            SampleTextBlock.Text += "  Grouping: " + (sample.Atributes.ContainsKey("Grouping") ? sample.GetAttribute("Grouping") : "-") + "\n";
            SampleTextBlock.Text += "  Worminess: "+ (sample.Atributes.ContainsKey("Worminess") ? sample.GetAttribute("Worminess") : "-") + "\n";
            SampleTextBlock.Text += "  EarthLevel: "+ (sample.Atributes.ContainsKey("EarthLevel") ? sample.GetAttribute("EarthLevel") : "-") + "\n";
            SampleTextBlock.Text += "  Taste: "+ (sample.Atributes.ContainsKey("Taste") ? sample.GetAttribute("Taste") : "-") + "\n";
            SampleTextBlock.Text += "  Oxidation: "+ (sample.Atributes.ContainsKey("Oxidation") ? sample.GetAttribute("Oxidation") : "-") + "\n";
            SampleTextBlock.Text += "  CapStruct: " + (sample.Atributes.ContainsKey("CapStruct") ? sample.GetAttribute("CapStruct") : "-") + "\n";
            SampleTextBlock.Text += "  CapShape: "+ (sample.Atributes.ContainsKey("CapShape") ? sample.GetAttribute("CapShape") : "-") + "\n";
            SampleTextBlock.Text += "  Skirt: " + (sample.Atributes.ContainsKey("Skirt") ? sample.GetAttribute("Skirt") : "-") + "\n";
            SampleTextBlock.Text += "  Slime: "+ (sample.Atributes.ContainsKey("Slime") ? sample.GetAttribute("Slime") : "-") + "\n";
            SampleTextBlock.Text += "  StipeDensity: "+ (sample.Atributes.ContainsKey("StipeDensity") ? sample.GetAttribute("StipeDensity") : "-") + "\n";
            SampleTextBlock.Text += "An example belongs to the class: " + (sample.ClassLabel == "Yes" ? "Edible" : "Inedible") ;
        }
    }
}
