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
        SamplesContainer TrainingSamples;
        SamplesContainer TestSamples;
        public string trainingSFilePath;
        public string testSFilePath;

        public MainWindow()
        {
            InitializeComponent();
            FileNameTextBox.Text = Directory.GetCurrentDirectory() + "\\TrainingSamples.txt";
        }

        private void ExtractSampleButton_Click(object sender, RoutedEventArgs e)
        {
            SampleExtractor SampleExtrInstance = new SampleExtractor(this);
            TrainingSamples = SampleExtrInstance.ExtractSamples(FileNameTextBox.Text);
            if (TrainingSamples!=null)
            {
                trainingSFilePath = FileNameTextBox.Text;
                SamplesBrowseMenuItem.IsEnabled = true;
                AddSampleMenuItem.IsEnabled = true;
            }
        }


        private void SamplesBrowseMunuItemClick(object sender, RoutedEventArgs e)
        {
            SamplesBrowseWindow SBWnd = new SamplesBrowseWindow(TrainingSamples);
            SBWnd.Show();
            SBWnd.Owner = this;
            this.IsEnabled = false;
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSampleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddSamplesWindow ASWnd = new AddSamplesWindow(TrainingSamples,trainingSFilePath);
            ASWnd.Show();
            ASWnd.Owner = this;
            this.IsEnabled = false;
        }
        
        public void SetStatusText(string text)
        {
            StatusTextBlock.Text = text;
        }
    }
}
