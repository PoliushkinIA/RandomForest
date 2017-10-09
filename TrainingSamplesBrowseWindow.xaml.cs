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
        SamplesContainer SCInstance;

        public SamplesBrowseWindow(SamplesContainer inpSContainer)
        {
            InitializeComponent();

            SCInstance = inpSContainer;
           
            SampleComboBox.ItemsSource = SCInstance.samplesList.Select(p => p.SampleName).ToList(); ;
            SampleComboBox.SelectedIndex = 0;
            OutputSelectedSample();
        }

        private void SampleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OutputSelectedSample();
        }

        void OutputSelectedSample()
        {
            Sample sample = SCInstance.samplesList[SampleComboBox.SelectedIndex];
            SampleTextBlock.Text = "";
            foreach (string key in SCInstance.samplesDomain.Keys)
            {
                SampleTextBlock.Text += "    "+key+": "+ (sample.Atributes.ContainsKey(key) ? sample.GetAttribute(key) : "-") +"\n";
            }
            if(sample.ClassLabel!=null)
            SampleTextBlock.Text += "An example belongs to the class: " + (sample.ClassLabel == "Yes" ? "Edible" : "Inedible");
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.IsEnabled = true;
        }
    }
}
