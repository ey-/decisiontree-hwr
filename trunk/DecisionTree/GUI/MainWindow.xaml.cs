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
using GraphSharp.Algorithms.Layout;
using System.ComponentModel;

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
    public partial class MainWindow : RibbonWindow, IMainWindow, INotifyPropertyChanged
    {
        protected IBusinessLogic mBusinessLogic;

        protected CTableEntryList mTableEntryList;
        protected CTreeGraph mGraph;
        protected string layoutAlgorithmType;
        protected E_VIEW mCurrentView;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor, Initialisierung der Komponenten
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mBusinessLogic = CBusinessLogic.getInstance();
            mBusinessLogic.init();
            mBusinessLogic.registerWindow(this);

            mTableEntryList = mBusinessLogic.getAllTableData();

            graph.DataContext = this;
            mGraph = mBusinessLogic.getGraph();
            
                
            GraphSharp.Algorithms.Layout.Simple.Tree.SimpleTreeLayoutParameters layoutParameter = new GraphSharp.Algorithms.Layout.Simple.Tree.SimpleTreeLayoutParameters();
            layoutParameter.LayerGap = 50;
            layoutParameter.VertexGap = 50;
            layoutParameter.Direction = LayoutDirection.TopToBottom;
                   
            LayoutAlgorithmType = "LinLog";
            graph.LayoutParameters = layoutParameter;
               
            DataContext = this;

            setTreeViewsEnabled(false);
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
        /// Tooltip abhängig von der View
        /// </summary>
        public string vertexTooltip
        {
            get 
            {
                if (mCurrentView == E_VIEW.E_TREE_INTERACTIVE_VIEW)
                {
                    return "Klicken sie hier um das Attribut auszuwählen oder zu ändern.";
                }
                else if (mCurrentView == E_VIEW.E_TREE_AUTOMATIC_VIEW)
                {
                    return "Klicken sie hier um die Tabelle mit den Werten dieses Knoten zu sehen.";
                }
                return "";

            }
            set
            {
            }
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
                openDlg.Filter = "CSV-Dateiein (*.csv)|*.csv";
                openDlg.Title = "Bitte wählen Sie eine CSV-Datei aus.";
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
                    // http://social.msdn.microsoft.com/Forums/en/wpf/thread/1b694f75-7621-4c88-8055-6c31c601c87f

                    // wir haben eine neue Tabelle, also ist noch kein Zielattribut gesetzt 
                    // und man soll nicht auf die Baumansichten wechseln
                    setTreeViewsEnabled(false);
                    viewToggleButton_Checked(viewTableBtn, null);

                    this.Cursor = Cursors.Arrow;
                }
            }
            // Datei Speichern Button
            else if (sender == saveFile)
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Filter = "CSV-Dateiein (*.csv)|*.csv";
                saveDlg.Title = "Bitte wählen Sie eine CSV-Datei unter der sie die Tabelle speichern möchten.";
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
                string name = Microsoft.VisualBasic.Interaction.InputBox("Geben Sie den Namen des Attributs ein", "Attributname", "");
                if (!name.Equals(""))
                {
                    CAttributeType columnData = mBusinessLogic.addAttribute(name);
                    addDatagridColumn(columnData);
                }
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

                        setTreeViewsEnabled(true);
                    }
                    else
                    {
                        MessageBox.Show("Das Attribut \"" + selectedColumn.ColumnDataType.Name + "\" kann nicht als Zielattribut gesetzt werden, da nur Datensätze mit den Einträgen \"j\" und \"n\" zugelassen sind.", "Ungültiges Zielattribut", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void RibbonButtonAutomaticView_Click(object sender, RoutedEventArgs e)
        {
            if (sender == rerenderGraphBtn)
            {
                viewToggleButton_Checked(viewTreeAutomaticBtn, null);
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

            if ((selectedView == E_VIEW.E_TREE_INTERACTIVE_VIEW) || (selectedView == E_VIEW.E_TREE_AUTOMATIC_VIEW))
            {
                mGraph = mBusinessLogic.getGraph();
                NotifyPropertyChanged("VisualGraph");
                NotifyPropertyChanged("vertexTooltip");
            }

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

            // Ansichtskomponente (Tabelle/Graph) ein und ausblenden
            viewTable.Visible = tableVisibility;
            viewTreeInteractiv.Visible = false;
            if (view == E_VIEW.E_TREE_INTERACTIVE_VIEW || view == E_VIEW.E_TREE_AUTOMATIC_VIEW)
            {
                viewTreeInteractiv.Visible = true;
            }
            
            // Im Ribbon die Ansichtsspezifischen Gruppen ein und Ausblenden
            grpTable.Visibility = bool2Visibility(tableVisibility);
            grpTreeInteractive.Visibility = bool2Visibility(treeInteractiveVisibility);
            grpTreeAutomatic.Visibility = bool2Visibility(treeAutomaticVisibility);

            // Ansichtsbuttons auswählen
            viewTableBtn.IsChecked = tableVisibility;
            viewTreeInteractivBtn.IsChecked = treeInteractiveVisibility;
            viewTreeAutomaticBtn.IsChecked = treeAutomaticVisibility;

            // aktive View merken
            mCurrentView = view;
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

                bool bInteractiveView = true;
                if (mCurrentView != E_VIEW.E_TREE_INTERACTIVE_VIEW)
                {
                    bInteractiveView = false;
                }
                IdentificationWindow identWindow = new IdentificationWindow(vertex, bInteractiveView);
                identWindow.Show();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*********************************************************************/
        /// <summary>
        /// Gibt dem Graphen bescheid das ein Attribut geändert wurde
        /// </summary>
        /// <param name="info">Name des Feldes welches sich geändert hat</param>
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void RibbonTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == minObjectCountTextBox)
            {
                TextBox textbox = sender as TextBox;
                textbox.Text = textbox.Text.Replace(" ", "");
                
                int number = 0;
                if (int.TryParse(textbox.Text, out number) == true)
                {
                    if (mBusinessLogic != null)
                    {
                        mBusinessLogic.setMinObjectCountAutoTree(number);
                    }
                }
            }
        }

        private void minObjectCountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender == minObjectCountTextBox)
            {
                if ((e.Key == System.Windows.Input.Key.D1) ||
                    (e.Key == System.Windows.Input.Key.D2) ||
                    (e.Key == System.Windows.Input.Key.D3) ||
                    (e.Key == System.Windows.Input.Key.D4) ||
                    (e.Key == System.Windows.Input.Key.D5) ||
                    (e.Key == System.Windows.Input.Key.D6) ||
                    (e.Key == System.Windows.Input.Key.D7) ||
                    (e.Key == System.Windows.Input.Key.D8) ||
                    (e.Key == System.Windows.Input.Key.D9) ||
                    (e.Key == System.Windows.Input.Key.D0) ||
                    (e.Key == System.Windows.Input.Key.NumPad1) ||
                    (e.Key == System.Windows.Input.Key.NumPad2) ||
                    (e.Key == System.Windows.Input.Key.NumPad3) ||
                    (e.Key == System.Windows.Input.Key.NumPad4) ||
                    (e.Key == System.Windows.Input.Key.NumPad5) ||
                    (e.Key == System.Windows.Input.Key.NumPad6) ||
                    (e.Key == System.Windows.Input.Key.NumPad7) ||
                    (e.Key == System.Windows.Input.Key.NumPad8) ||
                    (e.Key == System.Windows.Input.Key.NumPad9) ||
                    (e.Key == System.Windows.Input.Key.NumPad0))
                {
                    // ok
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void setTreeViewsEnabled(bool enabled)
        {
            viewTreeInteractivBtn.IsEnabled = enabled;
            viewTreeAutomaticBtn.IsEnabled = enabled;
        }

    } // class
} // namespace