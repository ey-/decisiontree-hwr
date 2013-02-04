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
        /// Wird aufgerufen wenn eine Button im Ribbonelement geklickt wird.
        /// </summary>
        /// <param name="sender">Welcher Button geklickt wurde</param>
        /// <param name="e">irgendwelche Parameter</param>
        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnTest)
            {
                if (this.grpTable.Visibility != System.Windows.Visibility.Visible)
                {
                    this.grpTable.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.grpTable.Visibility = System.Windows.Visibility.Hidden;
                }
            }

            
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
                MessageBox.Show("Diese Funktion wurde noch nicht implementiert");
            }

            if (sender == saveFile)
            {
                MessageBox.Show("Diese Funktion wurde noch nicht implementiert");
            }
        }

        private void RibbonButtonTableView_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btnAddColumn) == true)
            {
                CAttributeType type = mBusinessLogic.addAttribute();
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = type.Name;
                column.Binding = new Binding("[ " + type.Index + "].TableValue");
                datagrid1.Columns.Add(column);
            }
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

        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
