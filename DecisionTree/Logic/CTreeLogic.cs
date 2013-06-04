using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TreeData;
using DecisionTree.Storage;

namespace DecisionTree.Logic
{
    /*************************************************************************/
    /// <summary>
    /// Enthält die gesamte Logik um die Daten der Bäume zu steuern
    /// </summary>
    public class CTreeLogic
    {
        ITreeHandler mTreeHandler;
        CTableLogic mTableLogic;
        CTDIDTAlgorithm mTDIDTAlgorithm;

        bool mbInteractiveTreeNeedReset = false;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTreeLogic(CTableLogic tableLogic)
        {
            mTreeHandler = new CTreeHandler();
            mTableLogic = tableLogic;
            mTDIDTAlgorithm = new CTDIDTAlgorithm(this, tableLogic);
        }

        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        public CTreeGraph getGraph()
        {
            return mTreeHandler.getGraph();
        }

        /*********************************************************************/
        /// <summary>
        /// Wird aufgerufen wenn sich am Aufbau der Tabelle etwas geändert hat,
        /// damit z.B. die Interaktive Ansicht neu zurückgesetzt werden kann.
        /// </summary>
        public void setTableSetupChanged()
        {
            mbInteractiveTreeNeedReset = true;
        }

        /*********************************************************************/
        /// <summary>
        /// Der Nutzer hat auf die angegebene Ansicht gewechselt
        /// </summary>
        /// <param name="selectedView">View auf die gewechselt wurde</param>
        internal void setTreeForView(E_VIEW selectedView)
        {
            // Je nach View den Entsprechenden Baum aktive setzen
            if (selectedView == E_VIEW.E_TREE_INTERACTIVE_VIEW)
            {
                mTreeHandler.setActiveTree(E_TREE_TYPE.E_TREE_INTERACTIVE);

                // wenn in der Tabelle Attribute hinzugefügt oder gelöscht 
                // wurden, muss der Baum interaktiven Ansicht reseted werden
                if (mbInteractiveTreeNeedReset == true)
                {
                    resetActiveTree();
                    mbInteractiveTreeNeedReset = false;
                }
            } else
            {
                mTreeHandler.setActiveTree(E_TREE_TYPE.E_TREE_AUTOMATIC);

                mTDIDTAlgorithm.createGraph();
            }
        }

        public void resetActiveTree()
        {
            mTreeHandler.resetActiveTree();
        }

        public void updateVertexValues()
        {
            mTreeHandler.updateVertexValues(mTableLogic);
        }

        protected CTreeVertex addVertex(CTreeVertex parent, CAttributeType type = null)
        {
            return mTreeHandler.addVertex(parent, type);
        }

        protected bool removeVertex(CTreeVertex vertex)
        {
            return mTreeHandler.removeVertex(vertex);
        }

        protected CTreeEdge addEdge(CTreeVertex parent, CTreeVertex child, CAttributeValue attributeValue)
        {
            return mTreeHandler.addEdge(parent, child, attributeValue);
        }

        public CTreeVertex getRootNode()
        {
            return mTreeHandler.getRoot();
        }
        
        /*********************************************************************/
        /// <summary>
        /// Setzt für einen Vertex das Attribut welches dieser Repräsentiert
        /// </summary>
        /// <param name="vertex">Vertex dessen Attribut geändert werden soll</param>
        /// <param name="attributeType">neues Attribut des Vertex</param>
        internal bool setVertexAttribute(CTreeVertex vertex, CAttributeType attributeType)
        {
            // sollte das Attribut bereits von einem Parent-Knoten verwendet werden
            // darf der Benutzer dieser Attribut nicht verwenden
            if (isAttributeUsedByParent(vertex, attributeType) == true)
            {
                return false;
            }

            // wenn das Attribut bereits das ist welches der Vertex repräsentiert,
            // müssen wir nichts machen.
            if (vertex.AttributeType != attributeType)
            {
                mTreeHandler.removeChildVertices(vertex);

                // Attribut des Vertex setzen
                vertex.AttributeType = attributeType;

                if (vertex.AttributeType != null)
                {
                    // TODO Kindknoten erzeugen und Verbindungen anlegen:

                    //diskreter Wert (Splitwerte zurzeit nicht verfügbar -> ||true)
                    if (attributeType.DataType.Equals(E_DATATYPE.E_STRING) || true)
                    {
                        //alte Children löschen
                        mTreeHandler.removeChildVertices(vertex);

                        //Werte für die Kindknoten erhalten
                        CValueList childVertices = mTableLogic.getChildVertices(vertex);

                        //Knoten und Verbindungen hinzufügen
                        foreach (CAttributeValue value in childVertices)
                        {
                            CTreeVertex childVertex = addVertex(vertex, null);
                            childVertex.ParentEdge = addEdge(vertex, childVertex, value);
                        }
                    }
                    else //stetiger Wert
                    {
                        //Splitwert abfragen
                        //Datenbankabfrage, GROUP BY?
                        //
                    }
                }
                return true;
            }
            return false;
        }

        /*********************************************************************/
        /// <summary>
        /// Prüft ob ein AttributType bereits von ein ParentKnoten des 
        /// übergebenen Kontens verwendet wird
        /// </summary>
        /// <param name="vertex">Vertex dessen Parentes geprüft werden</param>
        /// <param name="type">zu Prüfender Type</param>
        /// <returns>True - wird von Parent benutzt, False - sonst</returns>
        public bool isAttributeUsedByParent(CTreeVertex vertex, CAttributeType type)
        {
            // Durch alle Parentelemente des Knotenes gehen und prüfen ob dieses Attribut 
            // nicht schon verwendet wurde
            CTreeVertex parent = vertex.ParentVertex;
            while (parent != null)
            {
                if (parent.AttributeType == type)
                {
                    return true;
                }
                parent = parent.ParentVertex;
            }
            return false;
        }

        internal void setMinObjectCountAutoTree(int number)
        {
            mTDIDTAlgorithm.setMinObjectCountAutoTree(number);
        }
    }
}
