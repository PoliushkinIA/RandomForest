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
using System.Windows.Shapes;

namespace RandomForest
{
    /// <summary>
    /// Логика взаимодействия для DecisionExtractWindow.xaml
    /// </summary>
    public partial class TestSampleExtractWindow : Window
    {
        public TestSampleExtractWindow()
        {
            InitializeComponent();
            samplesFilePathTextBox.Text = Directory.GetCurrentDirectory() + "\\TestSamples.txt";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void extractSamplesButton_Click(object sender, RoutedEventArgs e)
        {
            SampleExtractor SampleExtrInstance = new SampleExtractor(SetStatus,true);
            ((MainWindow)this.Owner).TestSamples = SampleExtrInstance.ExtractSamples(samplesFilePathTextBox.Text);
            if (((MainWindow)this.Owner).TestSamples != null)
            {
                ((MainWindow)this.Owner).BrowseTestSamplesMenuItem.IsEnabled = true;
                ((MainWindow)this.Owner).VerdictMenuItem.IsEnabled = true;
            }
        }

        void SetStatus(string status)
        {
            statusTextBlock.Text = status;
        }
    }
}
