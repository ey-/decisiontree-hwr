using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;
using DecisionTree.GUI;
using DecisionTree.Logic;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage;
using System.Data;
using Microsoft.Win32;
using System.IO;
using System.Globalization;

namespace DecisionTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        IBusinessLogic mBusinessLogic;
        CTableEntryList mTableEntryList;

        Setter TARGET_ATTRIBUTE_SETTER = new Setter();

        /*********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mBusinessLogic = CBusinessLogic.getInstance();
            mBusinessLogic.registerWindow(this);

            mTableEntryList = mBusinessLogic.getAllTableData();
            this.datagrid1.ItemsSource = mTableEntryList;

            DataContext = this;

            viewTableBtn.IsChecked = true;
        }

        /*********************************************************************/
        /// <summary>
        /// provisorische Funktion evtll. to delete
        /// </summary>
        public CTableEntryList TableEntryList
        {
            get { return mTableEntryList; }
            set {  }
        }

        /*********************************************************************/
        /// <summary>
        /// Wird aufgerufen wenn eine MenuItem geklickt wird.
        /// </summary>
        /// <param name="sender">Welcher Button geklickt wurde</param>
        /// <param name="e">"irgendwelche Parameter" (Arne)</param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Programm Beenden Button
            if (sender == exitApplication)
            {
                Application.Current.Shutdown();
            }
            // Datei Öffnen Button
            else if (sender == openFile)
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                if (openDlg.ShowDialog() == true)
                {
                    datagrid1.Columns.Clear();
                    List<CAttributeType> addedColumns = mBusinessLogic.openCSVFile(openDlg.FileName);
                    foreach (CAttributeType columnData in addedColumns)
                    {
                        addDatagridColumn(columnData);
                    }

                    mTableEntryList = mBusinessLogic.getAllTableData();
                    this.datagrid1.ItemsSource = mTableEntryList;

                    // http://social.msdn.microsoft.com/Forums/en/wpf/thread/1b694f75-7621-4c88-8055-6c31c601c87f
                }
            }
            // Datei Speichern Button
            else if (sender == saveFile) 
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                if (saveDlg.ShowDialog() == true)
                {
                    mBusinessLogic.saveCSVFile(saveDlg.FileName);
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Wird aufgrufen wenn ein Button von der Tabellenansicht geklickt wurde
        /// </summary>
        private void RibbonButtonTableView_Click(object sender, RoutedEventArgs e)
        {
            // Spalte hinzufügen Button
            if (sender.Equals(btnAddColumn) == true)
            {
                CAttributeType columnData = mBusinessLogic.addAttribute();
                addDatagridColumn(columnData);
            }
            // selektierte Spalte löschen Button
            else if (sender.Equals(btnRemoveColumn) == true)
            {
                if (datagrid1.CurrentColumn != null)
                {
                    DataGridColumn column = datagrid1.CurrentColumn;
                    if (mBusinessLogic.removeAttribute((string)column.Header) == true)
                    {
                        datagrid1.Columns.Remove(column);
                    }
                }
            }
            // Neue Zeile hinzufügen
            else if (sender.Equals(btnAddRow) == true)
            {
                 mTableEntryList.addEntry(mBusinessLogic.addDataset());
            }
            // Selektierte Zeile Löschen
            else if (sender.Equals(btnRemoveRow) == true)
            {
                if (datagrid1.SelectedItem != null)
                {
                    CTableEntry selectedEntry = (CTableEntry)datagrid1.SelectedItem;
                    if (mBusinessLogic.removeDataset(selectedEntry) == true)
                    {
                        mTableEntryList.Remove(selectedEntry);
                    }
                }
            }
            // Selektierte Zeile als Zielattribut setzen
            else if (sender.Equals(btnSetTargetAttribute) == true)
            {
                if (datagrid1.CurrentColumn != null)
                {
                    foreach (DataGridColumn column in datagrid1.Columns) 
                    {
                        column.HeaderStyle = FindResource("DefaultColumnHeaderStyle") as Style;
                    }
                    
                    datagrid1.CurrentColumn.HeaderStyle = FindResource("TargetValueColumnHeaderStyle") as Style;
                    CTableEntry entry = (CTableEntry)datagrid1.CurrentItem;

                    
                    /*
                    GridViewColumnHeader.

                    Style test = new Style("{x:Type DataGridColumnHeader}");
                    Setter asd = new Setter();
                    //asd.
                    //column.

                    GridViewColumnHeader qwe = new GridViewColumnHeader();
                    qwe.
                    */
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Wird bei Klick auf den Ansichtswechsel durchgeführt
        /// </summary>
        private void viewToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == viewTableBtn)
            {
                viewTable.Visible = true;
                viewTreeInteractiv.Visible = false;
                viewTreeAutomatic.Visible = false;

                grpTable.Visibility = System.Windows.Visibility.Visible;
                grpTreeInteractive.Visibility = System.Windows.Visibility.Hidden;
                grpTreeAutomatic.Visibility = System.Windows.Visibility.Hidden;

                viewTreeInteractivBtn.IsChecked = false;
                viewTreeAutomaticBtn.IsChecked = false;
            }
            else if (sender == viewTreeInteractivBtn)
            {
                viewTable.Visible = false;
                viewTreeInteractiv.Visible = true;
                viewTreeAutomatic.Visible = false;

                grpTable.Visibility = System.Windows.Visibility.Hidden;
                grpTreeInteractive.Visibility = System.Windows.Visibility.Visible;
                grpTreeAutomatic.Visibility = System.Windows.Visibility.Hidden;
                
                viewTableBtn.IsChecked = false;
                viewTreeAutomaticBtn.IsChecked = false;
            }
            else if (sender == viewTreeAutomaticBtn)
            {
                viewTable.Visible = false;
                viewTreeInteractiv.Visible = false;
                viewTreeAutomatic.Visible = true;

                grpTable.Visibility = System.Windows.Visibility.Hidden;
                grpTreeInteractive.Visibility = System.Windows.Visibility.Hidden;
                grpTreeAutomatic.Visibility = System.Windows.Visibility.Visible;
                
                viewTableBtn.IsChecked = false;
                viewTreeInteractivBtn.IsChecked = false;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Fügt eine Spalte für das Datagrid ein, aus einem AttributTypen
        /// </summary>
        /// <param name="columnData">Attributtype für den eine Spalte eingefügt 
        /// werden soll</param>
        void addDatagridColumn(CAttributeType columnData)
        {
            if (columnData != null)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = columnData.Name;
                column.Binding = new Binding("[ " + columnData.Index + "].TableValue");
                datagrid1.Columns.Add(column);
            }
        }
        /*
        void test(string bindPath)
        {
            Style style = new Style(typeof(GridViewColumnHeader));
            Trigger trigger = new Trigger();
            Setter setterColored = new Setter(GridViewColumnHeader.BackgroundProperty, "Moccasin");
            trigger.Property = 
            style.Triggers.Add(
        }*/

        private void DataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            //e.Row.Background = FindResource("RedBackgroundBrush") as Brush;
            
        }
    } // class
} // namespace
