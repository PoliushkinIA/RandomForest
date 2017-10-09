using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
    /// Логика взаимодействия для AddSamplesWindow.xaml
    /// </summary>
    public partial class AddSamplesWindow : Window
    {
        List<TextBlock> textBlocksList;
        List<ComboBox> comboBoxesList;
        ComboBox classLabelCB;
        TextBlock classLabelInfoTB;
        string filePath;
        SamplesContainer sCInstance;
        bool isTrainingSAdding;

        public AddSamplesWindow(SamplesContainer inpSCInstance,string inpFilePath,bool isTraining)
        {
            InitializeComponent();
            filePath = inpFilePath;
            sCInstance = inpSCInstance;
            filePathTextBox.Text = inpFilePath;
            isTrainingSAdding = isTraining;

            comboBoxesList = new List<ComboBox>();
            textBlocksList = new List<TextBlock>();

            int left=10, top=10, right=140;
            foreach (string key in inpSCInstance.samplesDomain.Keys)
            {
                ComboBox newCB = new ComboBox();
                grid.Children.Add(newCB);
                newCB.Width = right - left;
                newCB.Height = 22;
                Grid.SetColumn(newCB,1);
                Grid.SetRow(newCB, 1);
                newCB.Margin = new Thickness(10,top,0,0);
                newCB.VerticalAlignment = VerticalAlignment.Top;
                newCB.HorizontalAlignment = HorizontalAlignment.Left;
                newCB.ItemsSource = inpSCInstance.samplesDomain[key];
                newCB.SelectedIndex = 0;

                TextBlock newTB = new TextBlock();
                grid.Children.Add(newTB);
                newTB.Width = right - left;
                newTB.Height = 22;
                Grid.SetRow(newTB, 1);
                newTB.Margin = new Thickness(10, top, 0, 0);
                newTB.VerticalAlignment = VerticalAlignment.Top;
                newTB.HorizontalAlignment = HorizontalAlignment.Left;
                newTB.Text = key+" :";
                top += 28;

                comboBoxesList.Add(newCB);
                textBlocksList.Add(newTB);
            }
            if (isTraining)
            {
                classLabelCB = new ComboBox();
                grid.Children.Add(classLabelCB);
                classLabelCB.Width = right - left;
                classLabelCB.Height = 22;
                Grid.SetColumn(classLabelCB, 1);
                Grid.SetRow(classLabelCB, 1);
                classLabelCB.Margin = new Thickness(10, top, 0, 0);
                classLabelCB.VerticalAlignment = VerticalAlignment.Top;
                classLabelCB.HorizontalAlignment = HorizontalAlignment.Left;
                classLabelCB.ItemsSource = inpSCInstance.classLabels;
                classLabelCB.SelectedIndex = 0;

                classLabelInfoTB = new TextBlock();
                grid.Children.Add(classLabelInfoTB);
                classLabelInfoTB.Width = right - left;
                classLabelInfoTB.Height = 22;
                Grid.SetRow(classLabelInfoTB, 1);
                classLabelInfoTB.Margin = new Thickness(10, top, 0, 0);
                classLabelInfoTB.VerticalAlignment = VerticalAlignment.Top;
                classLabelInfoTB.HorizontalAlignment = HorizontalAlignment.Left;
                classLabelInfoTB.Text = "Select the class label: ";
                top += 28;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void writeToFileButton_Click(object sender, RoutedEventArgs e)
        {
            Sample newSampleInstance = new Sample();
            List<string> sampleText = new List<string>();
            sampleText.Add("\n");
            sampleText.Add("#>" + "\n");
            sampleText.Add("//" + sampleNameTextBox.Text + "\n");
            for (int i = 0; i < comboBoxesList.Count; i++)
            {
                newSampleInstance.SetAttribute(textBlocksList[i].Text, comboBoxesList[i].Text);
                sampleText.Add(textBlocksList[i].Text.Substring(0, textBlocksList[i].Text.IndexOf(' ')) +":"+ comboBoxesList[i].Text + "\n");
            }
            if (isTrainingSAdding)
            {
                sampleText.Add("*" + classLabelCB.SelectedItem + "\n");
                newSampleInstance.SetClassLabel((string)classLabelCB.SelectedItem);
            }
            sampleText.Add("<#" + "\n");
            File.AppendAllLines(filePath,sampleText);

            newSampleInstance.SetName(sampleNameTextBox.Text);
            newSampleInstance.SetClassLabel(comboBoxesList[comboBoxesList.Count-1].Text);
            
            sCInstance.samplesList.Add(newSampleInstance);
        }
    }
}
