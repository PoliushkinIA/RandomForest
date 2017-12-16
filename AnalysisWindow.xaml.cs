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
    /// Логика взаимодействия для AnalysisWindow.xaml
    /// </summary>
    public partial class AnalysisWindow : Window
    {
        RForest rForest;
        public AnalysisWindow(RForest rForestInstance)
        {
            rForest = rForestInstance;
            InitializeComponent();
        }

        public void DrawTree(int index)
        {
            Node tree = (Node)rForest.trees[index];
        }
    }
}
