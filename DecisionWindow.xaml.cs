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
    /// Логика взаимодействия для DecisionWindow.xaml
    /// </summary>
    public partial class DecisionWindow : Window
    {
        SamplesContainer TrainingSamples;
        SamplesContainer TestSamples;
        RForest RForestInstance;
        public DecisionWindow(SamplesContainer TrainingSamples, SamplesContainer TestSamples)
        {
            InitializeComponent();
            sampleComboBox.ItemsSource = TestSamples.samplesList.Select(p => p.SampleName).ToList();
            sampleComboBox.SelectedIndex = 0;
            this.TrainingSamples = TrainingSamples;
            this.TestSamples = TestSamples;
        }

        private void processDecisionButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> treesDecisions = new List<string>();
            try
            {
                
                processingStatusTextBlock.Text = "";
                foreach (DecisionTree tree in RForestInstance.trees)
                {
                    treesDecisions.Add(tree.Decide(TestSamples.samplesList[sampleComboBox.SelectedIndex]));
                }
                foreach (string classLabel in TrainingSamples.classLabels)
                {
                    processingStatusTextBlock.Text += classLabel + " - " + ((float)(treesDecisions.Where(p => p == classLabel).ToList().Count) / (float)(treesDecisions.Count)).ToString() + "\n";
                }

            }
            catch (Exception ex)
            {
                processingStatusTextBlock.Text = (ex.TargetSite + "\n" + ex.Message);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void buildAFrorestButton_Click(object sender, RoutedEventArgs e)
        {
            RForestInstance = new RForest(TrainingSamples, Convert.ToInt32(treesNumTextBox.Text));
            processingStatusTextBlock.Text = "The forest awaits instructions.";
            processDecisionButton.IsEnabled = true;
            extendedAnalysisButton.IsEnabled = true;
        }

        private void extendedAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            AnalysisWindow analysisWndInstance = new AnalysisWindow(RForestInstance);
            analysisWndInstance.Owner = this;
            analysisWndInstance.Show();
            this.IsEnabled = false;
        }
    }
}
