using crud_sqlite.database;
using crud_sqlite.models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace crud_sqlite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int DEFAULT_STATE = 0;
        const int CREATE_STATE = 1;
        const int UPDATE_STATE = 2;

        int saveAction = DEFAULT_STATE;
        int itemId = 0;
        int categoryId = 0;
        int measureId = 0;

        private void FormatItemTable()
        {
            for (int i = 0; i < dataGridItem.Columns.Count; i += 1)
            {
                dataGridItem.Columns[i].Width = 90;
            }
        }

        private void ChangeItemSectionReadOnly(bool state)
        {
            itemDescription.IsReadOnly = state;
            brandDescription.IsReadOnly = state;
        }

        private void CleanItemSection()
        {
            itemDescription.Clear();
            brandDescription.Clear();
            measureDescription.Clear();
            categoryDescription.Clear();
        }

        private void ActiveItemSectionButtons(bool state)
        {
            if (state)
            {
                btnMeasure.Visibility = Visibility.Visible;
                btnCategory.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
            }
            else
            {
                btnMeasure.Visibility = Visibility.Hidden;
                btnCategory.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
            }
        }

        private void ChangeOperatiosButtonsEnabledState(bool state)
        {
            btnNewItem.IsEnabled = state;
            btnEditItem.IsEnabled = state;
            btnReportItem.IsEnabled = state;
            btnDeleteItem.IsEnabled = state;
            btnLogOut.IsEnabled = state;

            btnSearchItem.IsEnabled = state;
            searchItem.IsEnabled = state;
            dataGridItem.IsEnabled = state;
        }

        private void GetItems(String item)
        {
            Items items = new Items();
            dataGridItem.ItemsSource = items.GetItemsByName(item).DefaultView;
            this.FormatItemTable();
        }

        private void CreateNewItem(object sender, RoutedEventArgs e)
        {
            saveAction = CREATE_STATE;

            this.ChangeItemSectionReadOnly(false);
            this.CleanItemSection();
            this.ActiveItemSectionButtons(true);
            this.ChangeOperatiosButtonsEnabledState(false);

            itemDescription.Focus();
        }

        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            saveAction = UPDATE_STATE;
        }

        private void SaveItem(object sender, RoutedEventArgs e)
        {
            String response;
            Item item = new Item();
            item.Id = itemId;
            item.Description = itemDescription.Text.Trim();
            item.Brand = brandDescription.Text.Trim();
            item.Measure_Id = 1;
            item.Category_Id = 1;

            Items items = new Items();
            response = items.CreateUpdateItem(saveAction, item);

            if (response.Equals("Success"))
            {
                this.GetItems("%");
                MessageBox.Show("Successful register.", "System message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(response);
            }

            saveAction = DEFAULT_STATE;

            this.ChangeItemSectionReadOnly(true);
            this.CleanItemSection();
            this.ActiveItemSectionButtons(false);
            this.ChangeOperatiosButtonsEnabledState(true);
        }

        private void CancelItem(object sender, RoutedEventArgs e)
        {
            this.ChangeItemSectionReadOnly(true);
            this.CleanItemSection();
            this.ActiveItemSectionButtons(false);
            this.ChangeOperatiosButtonsEnabledState(true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetItems("%");
        }

        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
