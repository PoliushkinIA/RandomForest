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
using System.Dynamic;

namespace RandomForest
{
    /// <summary>
    /// Логика взаимодействия для BrowseGridWnd.xaml
    /// </summary>
    public partial class BrowseGridWnd : Window
    {
        SamplesContainer samples;

        public BrowseGridWnd(SamplesContainer samples)
        {
            InitializeComponent();
            this.samples = samples;
            DataGridTextColumn column;
            column = new DataGridTextColumn();
            column.Header = "Name";
            column.Binding = new Binding("Name");
            dataGrid.Columns.Add(column);
            foreach (var colname in samples.attributes)
            {
                column = new DataGridTextColumn();
                column.Header = colname;
                column.Binding = new Binding(colname.Replace(' ', '_'));
                dataGrid.Columns.Add(column);
            }
            column = new DataGridTextColumn();
            column.Header = "Class";
            column.Binding = new Binding("Class");
            dataGrid.Columns.Add(column);

            dynamic row;
            foreach (var sample in samples.samplesList)
            {
                row = new ExpandoObject();
                foreach (var attribute in sample.Atributes)
                    ((IDictionary<String, Object>)row)[attribute.Key.Replace(' ', '_')] = attribute.Value;
                ((IDictionary<String, Object>)row)["Name"] = sample.SampleName;
                ((IDictionary<String, Object>)row)["Class"] = sample.ClassLabel;
                dataGrid.Items.Add(row);
            }
        }
    }
}
