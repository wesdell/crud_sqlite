using crud_sqlite.database;
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
    public partial class CategoryWindow : Window
    {
        public CategoryWindow()
        {
            InitializeComponent();
        }

        private void FormatCategoryDataTable()
        {
            Format format = new Format();
            format.FormatDataGrid(dataGridCategory);
        }

        private void GetCategories()
        {
            Categories categories = new Categories();
            dataGridCategory.ItemsSource = categories.GetCategories().DefaultView;
            dataGridCategory.SelectedValuePath = "id";
            this.FormatCategoryDataTable();
        }

        private void ApplyCategory(object sender, RoutedEventArgs e)
        {
            if (dataGridCategory.SelectedValue != null)
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
            this.GetCategories();
        }
    }
}
