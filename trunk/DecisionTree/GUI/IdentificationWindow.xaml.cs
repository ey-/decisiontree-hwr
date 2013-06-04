using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DecisionTree.Storage.TreeData;
using DecisionTree.Logic;
using DecisionTree.Storage;
using Microsoft.Windows.Controls.Ribbon;

namespace DecisionTree.GUI
{
    /*************************************************************************/
    /// <summary>
    /// Interaktionslogik für IdentificationWindow.xaml
    /// </summary>
    public partial class IdentificationWindow : RibbonWindow
    {
        IBusinessLogic mBusinessLogic = CBusinessLogic.getInstance();
        CTreeVertex mVertexToIdentify;
        CTableEntryList mTableEntryList;

        static IdentificationWindow mInstance = null;        

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="vertexToIdentify">Knoten der Identifiziert werden 
        /// soll</param>
        public IdentificationWindow(CTreeVertex vertexToIdentify, bool bInteractiveView)
        {
            // Sichergehen das nur ein Identifikationsfenster geöffnet ist
            checkSingleIndentificationWindow();

            InitializeComponent();

            mVertexToIdentify = vertexToIdentify;
            mTableEntryList = mBusinessLogic.getFilterdTableData(mVertexToIdentify);
            filteredDataGrid.DataContext = this;

            // die Spalten der Tabelle hinzufügen, abhängig von den Verwendeten Typen
            List<CAttributeType> attrTypeList = mBusinessLogic.getAttributeTypes();
            foreach (CAttributeType type in attrTypeList)
            { 
                if (type.Used == true)
                {
                    DataGridTextColumn column = new CTableColumn(type);
                    filteredDataGrid.Columns.Add(column);
                }
            }

            highlightSelectedAttributeColumn();

            // Nur für die Interaktive Ansicht soll der Button eingeblendet werden
            showButtonBar(bInteractiveView);
        }

        private void showButtonBar(bool bShowButtons)
        {
            if (bShowButtons == true)
            {

                groupAnsichtwechsel.Visibility = Visibility.Visible;
            }
            else 
            {
                groupAnsichtwechsel.Visibility = Visibility.Hidden;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Sichergehen das nur ein Identifikationsfenster geöffnet ist
        /// </summary>
        private void checkSingleIndentificationWindow()
        {
            // sollte bereits ein Identifikationsfenster offen sein, diese jetzt schließen
            if (mInstance != null)
            {
                mInstance.Close();
                mInstance = null;
            }
            // Fenster Instanz speichern, um sicher zu gehen dass nur ein 
            // Identifikationsfenster geöffnet ist
            mInstance = this;
        }

        /*********************************************************************/
        /// <summary>
        /// Holen der Liste mit Einträgen die von dem Knoten repräsentiert werden
        /// </summary>
        public CTableEntryList TableEntryList
        {
            get { return mTableEntryList; }
            
        }

        /*********************************************************************/
        /// <summary>
        /// Wird aufgerufen sobald der Benutzer auf 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseAttributeBtn_Click(object sender, RoutedEventArgs e)
        {
            CTableColumn selectedColumn = filteredDataGrid.CurrentColumn as CTableColumn;
            if (selectedColumn != null)
            {
                if (mBusinessLogic.setVertexAttribute(mVertexToIdentify, selectedColumn.ColumnDataType) == false)
                {
                    MessageBox.Show("Sie können dieses Attribut nicht auswählen, da es bereits als Attribut in einem anderen Knoten verwendet wird.", "Attribut wird bereits verwendet", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    highlightSelectedAttributeColumn();
                    this.Close();
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Hebt die Spalte hervor dessen Attributtyp, mit dem Typen des Knotens 
        /// übereinstimmt der gerade identifiziert wird.
        /// </summary>
        private void highlightSelectedAttributeColumn()
        {
            foreach (CTableColumn col in filteredDataGrid.Columns)
            {
                if (col.ColumnDataType == mVertexToIdentify.AttributeType)
                {
                    col.HeaderStyle = FindResource("TargetValueColumnHeaderStyle") as Style;
                }
                else
                {
                    col.HeaderStyle = null;
                }
            }
            
        }
    }
}
