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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Sample> TrainigSamples;
        public MainWindow()
        {
            InitializeComponent();
            FileNameTextBox.Text = Directory.GetCurrentDirectory() + "\\TrainingSamples.txt";
        }

        private void ExtractSampleButton_Click(object sender, RoutedEventArgs e)
        {
            SampleExtractor SampleExtrInstance = new SampleExtractor();
            StatusTextBlock.Text = SampleExtrInstance.ExtractSamples(FileNameTextBox.Text);
            if (StatusTextBlock.Text[0] == 'T')
            {
                TrainigSamples = SampleExtrInstance.ReturnSamples();
                SamplesMenuItem.IsEnabled = true;
            }
        }


        private void SamplesMunuItemClick(object sender, RoutedEventArgs e)
        {
            SamplesBrowseWindow SBWnd = new SamplesBrowseWindow(TrainigSamples);
            SBWnd.Show();
            SBWnd.Owner = this;
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
