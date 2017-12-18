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
    /// Логика взаимодействия для ConsultWindow.xaml
    /// </summary>
    public partial class ConsultWindow : Window
    {
        int currentTree = 0;
        RForest forest;
        List<string> treesDecisions = new List<string>();
        Node currentNode;
        Dictionary<string, string> answers = new Dictionary<string, string>();

        public ConsultWindow()
        {
            InitializeComponent();
        }

        void EndConsultation()
        {
            ((DecisionWindow)Owner).processingStatusTextBlock.Text = "";
            foreach (string classLabel in treesDecisions.Distinct())
            {
                ((DecisionWindow)Owner).processingStatusTextBlock.Text += classLabel + " - " + ((float)(treesDecisions.Where(p => p == classLabel).ToList().Count) / (float)(treesDecisions.Count)).ToString() + "\n";
            }
            this.Close();
            return;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            forest = ((DecisionWindow)Owner).RForestInstance;
            while (currentTree < forest.trees.Count && forest.trees[currentTree].GetType() == typeof(Leaf))
                treesDecisions.Add(((Leaf)forest.trees[currentTree++]).Decide());
            if (currentTree == forest.trees.Count)
                EndConsultation();
            currentNode = (Node)forest.trees[currentTree];
            attributeName.Content = currentNode.attribute;
            attributeValue.ItemsSource = currentNode.branches.Keys;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DecisionTree candidate = currentNode.branches[(string)attributeValue.SelectedItem];
            answers.Add((string)attributeName.Content, (string)attributeValue.SelectedItem);
            if (candidate.GetType() == typeof(Leaf))
            {
                treesDecisions.Add(((Leaf)candidate).Decide());
                if (++currentTree == forest.trees.Count)
                    EndConsultation();
                while (currentTree < forest.trees.Count && forest.trees[currentTree].GetType() == typeof(Leaf))
                    treesDecisions.Add(((Leaf)forest.trees[currentTree++]).Decide());
                if (currentTree == forest.trees.Count)
                {
                    EndConsultation();
                    return;
                }
                currentNode = (Node)forest.trees[currentTree];
            }
            else
                currentNode = (Node)candidate;
            attributeName.Content = currentNode.attribute;
            attributeValue.ItemsSource = currentNode.branches.Keys;
            while (answers.ContainsKey(currentNode.attribute))
            {
                candidate = currentNode.branches[answers[currentNode.attribute]];
                if (candidate.GetType() == typeof(Leaf))
                {
                    treesDecisions.Add(((Leaf)candidate).Decide());
                    if (++currentTree == forest.trees.Count)
                        EndConsultation();
                    while (currentTree < forest.trees.Count && forest.trees[currentTree].GetType() == typeof(Leaf))
                        treesDecisions.Add(((Leaf)forest.trees[currentTree++]).Decide());
                    if (currentTree == forest.trees.Count)
                    {
                        EndConsultation();
                        return;
                    }
                    currentNode = (Node)forest.trees[currentTree];
                }
                else
                    currentNode = (Node)candidate;
                attributeName.Content = currentNode.attribute;
                attributeValue.ItemsSource = currentNode.branches.Keys;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
