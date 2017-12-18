using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.IO;
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
        SamplesContainer TrainingSamples;
        SamplesContainer TestSamples;
        BitmapImage curTreeBitmapImage;
        BitmapImage curGraphBitmapImage;
        public AnalysisWindow(RForest rForestInstance, SamplesContainer TrainingSamples, SamplesContainer TestSamples)
        {
            rForest = rForestInstance;
            this.TrainingSamples = TrainingSamples;
            this.TestSamples = TestSamples;
            InitializeComponent();
            DrawTree(0);
            graphScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            List<int> numbersList = new List<int>();
            for (int i = 0; i < rForestInstance.trees.Count; i++)
                numbersList.Add(i);
            treeComboBox.ItemsSource = numbersList;
            treeComboBox.SelectedIndex = 0;

            if (TestSamples != null)
            {
                samplesComboBox.ItemsSource = TestSamples.samplesList.Select(p => p.SampleName).ToList();
                samplesComboBox.SelectedIndex = 0;

                classComboBox.ItemsSource = TrainingSamples.classLabels;
                classComboBox.SelectedIndex = 0;

                DrawComparsion(TestSamples.samplesList[samplesComboBox.SelectedIndex]);
            }
            else
            {
                graphInfoTextBlock.Text = "To view the voting schedule, you need to import a test sample before.";
                samplesComboBox.IsEnabled = false;
                classComboBox.IsEnabled = false;
                graphScrollViewer.IsEnabled = false;
                natSizeButton.IsEnabled = false;
                scSizeButton.IsEnabled = false;
            }

        }

        public void DrawTree(int index)
        {
            Node tree = (Node)rForest.trees[index];
            //collection of information about a tree
            List<int> offsLst = new List<int>();
            List<int> posLst = new List<int>();
            treeInfo(tree, 0, offsLst);
            int treeHeight = offsLst.Count;
            int treeWidth = offsLst.Max();
            string treeStr="";
            treeStr += "Tree width: " + treeWidth +"\n" + "Tree height: " + treeHeight +"\n" +"Power of the tree: "+offsLst.Sum();
            aboutTreTextBox.Text = treeStr;

            Bitmap treeBitMap = new Bitmap(treeWidth * 180, treeHeight * 100);
            Graphics g = Graphics.FromImage(treeBitMap);
            g.FillRegion(new SolidBrush(System.Drawing.Color.AntiqueWhite), new Region(new System.Drawing.Rectangle(0, 0, treeWidth * 180, treeHeight * 100)));
            //tree drawing
            treeOutput(g, tree, posLst, 0, 0, 0, offsLst, treeWidth, "");
            g.Dispose();

            curTreeBitmapImage = BitmapToImageSource(treeBitMap);
            treeImage.Source = curTreeBitmapImage;
        }

        void DrawComparsion(Sample sample)
        {
            List<string> treesDecisions = new List<string>();
            foreach (DecisionTree tree in rForest.trees)
            {
                treesDecisions.Add(tree.Decide(sample));
            }
            
            Bitmap graphBitMap = new Bitmap(treesDecisions.Count*5,100);
            Graphics g = Graphics.FromImage(graphBitMap);
            g.FillRegion(new SolidBrush(System.Drawing.Color.WhiteSmoke), new Region(new System.Drawing.Rectangle(0, 0, treesDecisions.Count * 5, 100)));

            SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);

            int width = graphBitMap.Width / treesDecisions.Count;
            for(int i = 1;i<=treesDecisions.Count;i++)
                {
                    float percent = (float)(treesDecisions.Take(i).Where(p => p == (string)classComboBox.SelectedItem).ToList().Count) / (float)(treesDecisions.Count);

                    g.FillRectangle(myBrush, (i-1)*width, graphBitMap.Height - (float)(graphBitMap.Height)*(float)(percent), i * width, (float)graphBitMap.Height * (float)percent);
                }
            for(int i = 1; i<= (int)(treesDecisions.Count/5) - 1; i++)
            {
                int x = (int)(graphBitMap.Width * (float)(i / ((float)(treesDecisions.Count / 5))));
                g.DrawLine(myPen, x, graphBitMap.Height *0.9F, x, graphBitMap.Height - 1);
            }
            g.DrawLine(myPen, 0, 0, graphBitMap.Width, 0);
            g.DrawLine(myPen, 0, 0, 0, graphBitMap.Height);
            g.DrawLine(myPen, graphBitMap.Width-1, 0, graphBitMap.Width-1, graphBitMap.Height);
            g.DrawLine(myPen, 0, graphBitMap.Height-1, graphBitMap.Width, graphBitMap.Height-1);
            myBrush.Dispose();
            myPen.Dispose();
            g.Dispose();
            curGraphBitmapImage = BitmapToImageSource(graphBitMap);
            graphImage.Source = curGraphBitmapImage;
            graphInfoTextBlock.Text = "The number of votes depending on the size of the forest(1-"+treesDecisions.Count+"):";
        }

        void treeInfo(DecisionTree node, int level, List<int> curLst)
        {
            if (curLst.Count == level) curLst.Add(0);
            if (node.GetType() == typeof(Node))
            {
                curLst[level]++;
                foreach (DecisionTree branch in ((Node)node).branches.Values)
                {
                    treeInfo(branch, level + 1, curLst);
                }
            }
            else
            {
                curLst[level]++;
            }
        }

        void treeOutput(Graphics g, DecisionTree node, List<int> posLst, int level, int preX, int preY, List<int> offsLst, int maxWidth, string atrVal)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.Font nodeFont = new System.Drawing.Font("Courier", 12, System.Drawing.FontStyle.Regular);
            System.Drawing.Font branchFont = new System.Drawing.Font("Courier", 12, System.Drawing.FontStyle.Regular);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            if (posLst.Count == level) posLst.Add(0);
            int curX = posLst[level];

            int paddingY = 75;
            int dx = 120;
            int dy = 25;

            int x = 5 + (curX + (maxWidth - offsLst[level]) / 2) * dx + 60 * curX;
            int y = 5 + (int)(level * dy + level * paddingY);
            int preXV = 0;
            if (level > 0) preXV = 5 + (preX + (maxWidth - offsLst[level - 1]) / 2) * dx + 60 * preX;
            int preYV = 5 + (int)(preY * dy + preY * paddingY) + dy;

            if (node.GetType() == typeof(Node))
            {
                posLst[level]++;
                g.DrawRectangle(myPen, new System.Drawing.Rectangle(x, y, dx, dy));

                g.DrawString(((Node)node).attribute, nodeFont, drawBrush, x, y);
                g.DrawString(atrVal, branchFont, drawBrush, x, y - dy);
                if (preX != curX || preY != level)
                {
                    g.DrawLine(myPen, x + 60, y, preXV + 60, preYV);

                }

                drawBrush.Dispose();
                branchFont.Dispose();
                nodeFont.Dispose();
                myPen.Dispose();

                foreach (KeyValuePair<string, DecisionTree> branch in ((Node)node).branches)
                {
                    treeOutput(g, branch.Value, posLst, level + 1, curX, level, offsLst, maxWidth, branch.Key);
                }

            }
            else
            {

                g.DrawLine(myPen, x + 60, y, preXV + 60, preYV);
                myPen.Color = System.Drawing.Color.Green;
                g.DrawRectangle(myPen, new System.Drawing.Rectangle(x, y, dx, dy));
                g.DrawString(atrVal, branchFont, drawBrush, x, y - dy);
                drawBrush.Color = System.Drawing.Color.Red;
                g.DrawString(((Leaf)node).classLabel, nodeFont, drawBrush, x, y);
                posLst[level]++;

                drawBrush.Dispose();
                branchFont.Dispose();
                nodeFont.Dispose();
                myPen.Dispose();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.IsEnabled = true;
            this.Owner.Show();
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void treeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawTree(treeComboBox.SelectedIndex);
        }

        private void samplesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawComparsion(TestSamples.samplesList[samplesComboBox.SelectedIndex]);
        }

        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawComparsion(TestSamples.samplesList[samplesComboBox.SelectedIndex]);
        }

        private void changeTheScaleButton_Click(object sender, RoutedEventArgs e)
        {
            treeImage.Width = curTreeBitmapImage.Width;
            treeImage.Height = curTreeBitmapImage.Height;
        }

        private void sczledSizeButton_Click(object sender, RoutedEventArgs e)
        {
            treeImage.Width = scrollViewer.ActualWidth;
            treeImage.Height = scrollViewer.ActualHeight;
        }

        private void natSizeButton_Click(object sender, RoutedEventArgs e)
        {
            graphImage.Width = curGraphBitmapImage.Width;
            graphImage.Height = 100;
        }

        private void scSizeButton_Click(object sender, RoutedEventArgs e)
        {
            graphImage.Width = graphScrollViewer.ActualWidth;
            graphImage.Height = 100;
        }
    }
}
