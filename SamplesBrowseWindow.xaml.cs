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
            SampleTextBlock.Text = "";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Continent") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Landscape") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Substratum") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Grouping") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Worminess") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("EarthLevel") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Taste") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Oxidation") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("CapStruct") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("CapShape") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Skirt") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Slime") + "\n";
            SampleTextBlock.Text += SamplesList[SampleComboBox.SelectedIndex].GetAttribute("StipeDensity") + "\n";
            SampleTextBlock.Text += "Class label: " + SamplesList[SampleComboBox.SelectedIndex].GetAttribute("Edibility") + "\n";
        }
    }
}
