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
using DecisionTree.Storage.TreeData;
using GraphSharp.Controls;

namespace DecisionTree
{
    public enum E_VIEW
    { 
        E_TABLE_VIEW,
        E_TREE_INTERACTIVE_VIEW,
        E_TREE_AUTOMATIC_VIEW
    }

    /*************************************************************************/
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        protected IBusinessLogic mBusinessLogic;

        protected CTableEntryList mTableEntryList;
        protected CTreeGraph mGraph;
        protected string layoutAlgorithmType;
        
        /*********************************************************************/
        /// <summary>
        /// Konstruktor, Initialisierung der Komponenten
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mBusinessLogic = CBusinessLogic.getInstance();
            mBusinessLogic.registerWindow(this);

            mTableEntryList = mBusinessLogic.getAllTableData();
            
            graph.DataContext = this;
            mGraph = mBusinessLogic.getGraph();
            LayoutAlgorithmType = "LinLog";

            DataContext = this;

            setViewVisibility(E_VIEW.E_TABLE_VIEW);
        }

        /*********************************************************************/
        /// <summary>
        /// provisorische Funktion evtll. to delete
        /// </summary>
        public CTableEntryList TableEntryList
        {
            get { return mTableEntryList; }
        }

        /*********************************************************************/
        /// <summary>
        /// Zugriff auf die Daten im Graphen
        /// </summary>
        public CTreeGraph VisualGraph
        {
            get { return mGraph = mBusinessLogic.getGraph(); }
        }

        /*********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        public string LayoutAlgorithmType
        {
            get { return layoutAlgorithmType; }
            set { layoutAlgorithmType = value; }
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
                    this.Cursor = Cursors.Wait;

                    datagrid1.Columns.Clear();
                    List<CAttributeType> addedColumns = mBusinessLogic.openCSVFile(openDlg.FileName);
                    foreach (CAttributeType columnData in addedColumns)
                    {
                        addDatagridColumn(columnData);
                    }

                    mTableEntryList = mBusinessLogic.getAllTableData();
                    this.datagrid1.ItemsSource = mTableEntryList;

                    this.Cursor = Cursors.Arrow;
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
                CTableColumn selectedColumn = datagrid1.CurrentColumn as CTableColumn;
                if (selectedColumn != null)
                {
                    if (mBusinessLogic.setTargetAttribute(selectedColumn.ColumnDataType) == true)
                    {
                        foreach (CTableColumn column in datagrid1.Columns)
                        {
                            //column.HeaderStyle = FindResource("DefaultColumnHeaderStyle") as Style;
                            column.HeaderStyle = null;
                        }

                        selectedColumn.HeaderStyle = FindResource("TargetValueColumnHeaderStyle") as Style;
                        CTableEntry entry = (CTableEntry)datagrid1.CurrentItem;
                    }

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
        /// Wird bei Klick auf einen Button für den Ansichtswechsel aufgerufen
        /// und führt Ansichtswechsel durch
        /// </summary>
        private void viewToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            E_VIEW selectedView = E_VIEW.E_TABLE_VIEW;

            // Ansicht auf Tabellenansicht wechseln
            if (sender == viewTableBtn)
            {
                selectedView = E_VIEW.E_TABLE_VIEW;
            }
            // Ansicht auf Baum Interaktiv Ansicht wechseln
            else if (sender == viewTreeInteractivBtn)
            {
                selectedView = E_VIEW.E_TREE_INTERACTIVE_VIEW;
            }
            // Ansicht auf Baum Automatisch Ansicht wechseln
            else if (sender == viewTreeAutomaticBtn)
            {
                selectedView = E_VIEW.E_TREE_AUTOMATIC_VIEW;
            }

            setViewVisibility(selectedView);
            mBusinessLogic.changeView(selectedView);

            mGraph = mBusinessLogic.getGraph();
            graph.DataContext = this;
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt die Sichtbarkeit einer Ansicht
        /// </summary>
        /// <param name="view">Ansicht die Angezeigt werden soll</param>
        private void setViewVisibility(E_VIEW view)
        {
            bool tableVisibility = false;
            bool treeInteractiveVisibility = false;
            bool treeAutomaticVisibility = false;

            switch (view)
            {
                case E_VIEW.E_TABLE_VIEW: tableVisibility = true; break;
                case E_VIEW.E_TREE_INTERACTIVE_VIEW: treeInteractiveVisibility = true; break;
                case E_VIEW.E_TREE_AUTOMATIC_VIEW: treeAutomaticVisibility = true; break;
            }

            viewTable.Visible = tableVisibility;
            viewTreeInteractiv.Visible = treeInteractiveVisibility;
            viewTreeAutomatic.Visible = treeAutomaticVisibility;

            grpTable.Visibility = bool2Visibility(tableVisibility);
            grpTreeInteractive.Visibility = bool2Visibility(treeInteractiveVisibility);
            grpTreeAutomatic.Visibility = bool2Visibility(treeAutomaticVisibility);

            viewTableBtn.IsChecked = tableVisibility;
            viewTreeInteractivBtn.IsChecked = treeInteractiveVisibility;
            viewTreeAutomaticBtn.IsChecked = treeAutomaticVisibility;
        }

        /*********************************************************************/
        /// <summary>
        /// Konvertiert ein boolean in ein Visibility Typen
        /// </summary>
        /// <param name="visible">Sichtbar oder nicht</param>
        /// <returns>Sichtbarkeitstyp</returns>
        private Visibility bool2Visibility(bool visible)
        {
            if (visible == true)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
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
                DataGridTextColumn column = new CTableColumn(columnData);
                //column.Header = columnData.Name;
                //column.Binding = new Binding("[ " + columnData.Index + "].TableValue");
                datagrid1.Columns.Add(column);
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void nodeDoubleClicked(object sender, RoutedEventArgs e)
        {
            if (sender is VertexControl)
            {
                VertexControl control = sender as VertexControl;
                CTreeVertex vertex = control.Vertex as CTreeVertex;

                IdentificationWindow identWindow = new IdentificationWindow(vertex);
                identWindow.Show();
            }
        }
       
    } // class
} // namespace
