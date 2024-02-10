using crud_sqlite.database;
using crud_sqlite.models;
using crud_sqlite.services;
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

namespace crud_sqlite
{
    /// <summary>
    /// Interaction logic for MeasureWindow.xaml
    /// </summary>
    public partial class MeasureWindow : Window
    {
        public MeasureWindow()
        {
            InitializeComponent();
        }

        private void FormatMeasureDataTable()
        {
            Format format = new Format();
            format.FormatDataGrid(dataGridMeasure);
        }

        private void GetMeasures()
        {
            Measures measures = new Measures();
            dataGridMeasure.ItemsSource = measures.GetMeasures().DefaultView;
            dataGridMeasure.SelectedValuePath = "id";
            this.FormatMeasureDataTable();
        }

        private void ApplyMeasure(object sender, RoutedEventArgs e)
        {
            if (dataGridMeasure.SelectedValue != null)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a register.", "System message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetMeasures();
        }
    }
}
