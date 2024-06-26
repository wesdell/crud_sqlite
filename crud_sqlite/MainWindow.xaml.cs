﻿using crud_sqlite.database;
using crud_sqlite.models;
using crud_sqlite.services;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class MainWindow : Window
    {
        const int DEFAULT_STATE = 0;
        const int CREATE_STATE = 1;
        const int UPDATE_STATE = 2;

        int saveAction = DEFAULT_STATE;
        int itemId = 0;
        int categoryId = 0;
        int measureId = 0;

        private void FormatItemDataTable()
        {
            Format format = new Format();
            format.FormatDataGrid(dataGridItem);
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
            this.FormatItemDataTable();
        }

        private void CreateNewItem(object sender, RoutedEventArgs e)
        {
            this.saveAction = CREATE_STATE;

            this.ChangeItemSectionReadOnly(false);
            this.CleanItemSection();
            this.ActiveItemSectionButtons(true);
            this.ChangeOperatiosButtonsEnabledState(false);

            itemDescription.Focus();
        }

        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            if (dataGridItem.SelectedValue != null)
            {
                this.saveAction = UPDATE_STATE;

                this.ChangeItemSectionReadOnly(false);
                this.ActiveItemSectionButtons(true);
                this.ChangeOperatiosButtonsEnabledState(false);
            }
        }

        private void SelectMeasure(object sender, RoutedEventArgs e)
        {
            Measures measures = new Measures();
            MeasureWindow measureWindow = new MeasureWindow();
            measureWindow.ShowDialog();
            this.measureId = Convert.ToInt16(measureWindow.dataGridMeasure.SelectedValue);
            measureDescription.Text = measures.GetMeasureById(measureId);
        }

        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            Categories categories = new Categories();
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.ShowDialog();
            this.categoryId = Convert.ToInt16(categoryWindow.dataGridCategory.SelectedValue);
            categoryDescription.Text = categories.GetCategoryById(categoryId);
        }

        private void SaveItem(object sender, RoutedEventArgs e)
        {
            String response;
            Item item = new Item();
            item.Id = this.itemId;
            item.Description = itemDescription.Text.Trim();
            item.Brand = brandDescription.Text.Trim();
            item.Measure_Id = this.measureId;
            item.Category_Id = this.categoryId;

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
            this.itemId = 0;
            this.measureId = 0;
            this.categoryId = 0;

            this.ChangeItemSectionReadOnly(true);
            this.CleanItemSection();
            this.ActiveItemSectionButtons(false);
            this.ChangeOperatiosButtonsEnabledState(true);
        }

        private void CancelItem(object sender, RoutedEventArgs e)
        {
            saveAction = DEFAULT_STATE;
            this.itemId = 0;
            this.measureId = 0;
            this.categoryId = 0;

            this.ChangeItemSectionReadOnly(true);
            this.CleanItemSection();
            this.ActiveItemSectionButtons(false);
            this.ChangeOperatiosButtonsEnabledState(true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetItems("%");
        }

        private void ItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataRowView rowSelected = dataGrid.SelectedItem as DataRowView;

            if (rowSelected != null)
            {
                itemDescription.Text = rowSelected["description"].ToString();
                brandDescription.Text = rowSelected["brand"].ToString();
                measureDescription.Text = rowSelected["measure"].ToString();
                categoryDescription.Text = rowSelected["category"].ToString();
                this.itemId = Convert.ToInt16(rowSelected["id"].ToString());
                this.categoryId = Convert.ToInt16(rowSelected["category_id"].ToString());
                this.measureId = Convert.ToInt16(rowSelected["measure_id"].ToString());
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
