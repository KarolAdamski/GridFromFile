using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MessageBox = WPFMessageBox.MessageBox;



namespace GridFromFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // enable dataset and save
        DataSet dataSet = new DataSet();
        bool ableToSave = true;

        public MainWindow()
        {
            InitializeComponent();

            // it is path to xml file
            string pathFileXml= System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\XMLFile1.xml");
            // it is start of loading data to grid
            dataSet.ReadXml(@pathFileXml);
            DataView dataView = new DataView(dataSet.Tables[0]);
            dataGrid.ItemsSource = dataView;

            // button of disable changes is not active on the start
            cancel_button.IsEnabled = false;
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            string pathFileXml = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\XMLFile1.xml");
            try
            {

                // here grid is cleared
                dataGrid.ItemsSource = null;
                dataGrid.Items.Clear();
                dataSet.Clear();

                // loading to grid data from .xml file
                dataSet.ReadXml(@pathFileXml);
                DataView dataView = new DataView(dataSet.Tables[0]);
                dataGrid.ItemsSource = dataView;

                // button is disabled after erase it added row
                cancel_button.IsEnabled = false;

                MessageBox.ShowInformation("Canceled Changes!");
            }
            catch (Exception ex)
            {
                MessageBox.ShowError(ex, "Cancel was't sucessful!");
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // it is path to xml file 
            string pathFileXml = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\XMLFile1.xml");

            // it is to check empty cell, after error with save
            ableToSave =true;
            
            try
            {
                foreach (DataRowView row in dataGrid.Items)
                {
                    if (row.Row.ItemArray[0].ToString() == "" || row.Row.ItemArray[0] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                    else if (row.Row.ItemArray[1].ToString() == "" || row.Row.ItemArray[1] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                    else if (row.Row.ItemArray[2].ToString() == "" || row.Row.ItemArray[2] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                    else if (row.Row.ItemArray[3].ToString() == "" || row.Row.ItemArray[3] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                    else if (row.Row.ItemArray[5].ToString() == "" || row.Row.ItemArray[5] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                    else if (row.Row.ItemArray[6].ToString() == "" || row.Row.ItemArray[6] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                    else if (row.Row.ItemArray[7].ToString() == "" || row.Row.ItemArray[7] == null)
                    {
                        ableToSave = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }

         
            try
            {
                if (ableToSave == true)
                {
                    // after press Save button, grid will be saved
                    dataSet.AcceptChanges();
                    dataSet.WriteXml(@pathFileXml);

                    MessageBox.ShowInformation("Grid is saved!","Success of save!");

                } else
                {
                    MessageBox.ShowError("Grid has empty cell!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowError(ex, "It was error with exception!");
            }

        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // edit place checking
            if (e.EditAction == DataGridEditAction.Commit || e.EditAction == DataGridEditAction.Cancel)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    // here is the unlock button when editing lines, Age is readonly

                    var bindingPath = (column.Binding as Binding).Path.Path;
                    if (bindingPath == "FirstName" || bindingPath == "LastName" || bindingPath == "StreetName" || bindingPath == "HouseNumber" || bindingPath == "ApartmentNumber"
                        || bindingPath == "PostalCode" || bindingPath == "PhoneNumber" || bindingPath == "DayofBirth")
                    {
                        // it get current row index
                        int rowIndex = e.Row.GetIndex();
                        var el = e.EditingElement as TextBox;
                        if (el.Text == "" || el.Text != null)
                        {
                            cancel_button.IsEnabled = true;
                        }
                        // el.Text has the new, user-entered value
                    }
                }
            }
        }
    }
}
