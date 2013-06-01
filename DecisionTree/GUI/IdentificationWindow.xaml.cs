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

namespace DecisionTree.GUI
{
    /*************************************************************************/
    /// <summary>
    /// Interaktionslogik für IdentificationWindow.xaml
    /// </summary>
    public partial class IdentificationWindow : Window
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
        public IdentificationWindow(CTreeVertex vertexToIdentify)
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

            mVertexToIdentify = vertexToIdentify;
            
            InitializeComponent();
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
        }

        /*********************************************************************/
        /// <summary>
        /// Holen der Liste mit Einträgen die von dem Knoten repräsentiert werden
        /// </summary>
        public CTableEntryList TableEntryList
        {
            get { return mTableEntryList; }
            
        }
    }
}
