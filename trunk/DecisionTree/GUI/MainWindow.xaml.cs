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

namespace DecisionTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        IBusinessLogic mBusinessLogic;

        /*********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mBusinessLogic = CBusinessLogic.getInstance();
            mBusinessLogic.registerWindow(this);
            DataContext = this;

            viewTableBtn.IsChecked = true;
        }

        /*********************************************************************/
        /// <summary>
        /// provisorische Funktion evtll. to delete
        /// </summary>
        public CTableEntryList TableEntryList
        {
            get { return mBusinessLogic.getAllTableData(); }
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
            if (sender == exitApplication)
            {
                Application.Current.Shutdown();
            }

            if (sender == openFile)
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                if (openDlg.ShowDialog() == true)
                {
                    mBusinessLogic.openCSVFile(openDlg.FileName);
                }
            }

            if (sender == saveFile)
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
                CAttributeType type = mBusinessLogic.addAttribute();
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = type.Name;
                column.Binding = new Binding("[ " + type.Index + "].TableValue");
                datagrid1.Columns.Add(column);
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

        
    }
}
