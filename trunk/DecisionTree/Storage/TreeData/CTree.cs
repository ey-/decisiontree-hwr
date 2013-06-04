using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DecisionTree.Storage.TreeData
{
    /*************************************************************************/
    /// <summary>
    /// Klasse zum verwalten eines Baums
    /// </summary>
    public class CTree
    {
        

        CTreeGraph mGraph;
        List<CTreeEdge> mEdgeList;
        List<CTreeVertex> mVertexList;

        CTreeVertex mRoot;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTree()
        {
            mGraph = new CTreeGraph();
            mEdgeList = new List<CTreeEdge>();
            mVertexList = new List<CTreeVertex>();

            mRoot = addVertex(null);
            mVertexList.Add(mRoot);
        }

        /*********************************************************************/
        /// <summary>
        /// Getter zum holen des Graphen zur Anzeige durch Graph#
        /// </summary>
        public CTreeGraph Graph
        {
            get { return mGraph; }
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt den Baum auf den Initalzustand mit einem leeren Root-Knoten 
        /// zurück
        /// </summary>
        public void resetTree()
        {
            // alle bestehenden Knoten und Verbindungen des Baumes löschen
            /*foreach (CTreeEdge edge in mEdgeList)
            {
                mGraph.RemoveEdge(edge);
            }*/
            mEdgeList.Clear();
            /*foreach (CTreeVertex vertex in mVertexList)
            {
                mGraph.RemoveVertex(vertex);
            }*/
            mGraph = new CTreeGraph();
            mVertexList.Clear();
            
            mRoot = null;

            mRoot = addVertex(null);
        }

        /*********************************************************************/
        /// <summary>
        /// Erstellt einen Vertex und fügt ihn in den Baum ein.
        /// </summary>
        /// <param name="parent">Übergeordneter Knoten im Baum</param>
        /// <param name="attributeType">Attributtyp nach dem aufgeteilt werden soll</param>
        /// <returns>Hinzugefügter Vertex</returns>
        public CTreeVertex addVertex(CTreeVertex parent, CAttributeType attributeType = null)
        {
            CTreeVertex vertex = new CTreeVertex(parent, mGraph, attributeType);
            mVertexList.Add(vertex);
            mGraph.AddVertex(vertex);

            if (parent != null)
            {
                parent.ChildList.Add(vertex);
            }

            return vertex;
        }


        internal bool removeVertex(CTreeVertex vertex)
        {
            mVertexList.Remove(vertex);
            return mGraph.RemoveVertex(vertex);
        }

        internal CTreeEdge addEdge(CTreeVertex parent, CTreeVertex child, CAttributeValue attributeValue)
        {
            CTreeEdge edge = new CTreeEdge(parent, child, attributeValue);
            mGraph.AddEdge(edge);
            mEdgeList.Add(edge);

            return edge;
        }

        internal bool removeEdge(CTreeEdge edge)
        {
            mEdgeList.Remove(edge);
            return mGraph.RemoveEdge(edge);
        }

        internal void updateVertexValues(Logic.CTableLogic tableLogic)
        {
            updateVertexValue(mRoot, tableLogic);   
        }

        private void updateVertexValue(CTreeVertex vertex, Logic.CTableLogic tableLogic)
        {
            vertex.CountObjects = tableLogic.getFilteredTableData(vertex).Count;
            updateVertexClassCount(vertex, tableLogic);
            updateVertexEntropy(vertex, tableLogic);

            foreach (CTreeVertex child in vertex.ChildList)
            {
                updateVertexValue(child, tableLogic);
            }
        }

        private void updateVertexEntropy(CTreeVertex vertex, Logic.CTableLogic tableLogic)
        {
            if (vertex.CountObjectsPerClass[CTreeVertex.YES_INDEX] == 0 || vertex.CountObjectsPerClass[CTreeVertex.NO_INDEX] == 0)
            {
                vertex.Entropy = 0;
            }
            else
            {
                double yesFactor = (double)vertex.CountObjectsPerClass[CTreeVertex.YES_INDEX] / (double)vertex.CountObjects;
                double noFactor = (double)vertex.CountObjectsPerClass[CTreeVertex.NO_INDEX] / (double)vertex.CountObjects;

                vertex.Entropy = -(yesFactor * Math.Log(yesFactor, 2) + noFactor * Math.Log(noFactor, 2));
            }
        }

        private void updateVertexClassCount(CTreeVertex vertex, Logic.CTableLogic tableLogic)
        {
            CTableEntryList entryList = tableLogic.getFilteredTableData(vertex);
            CAttributeType targetAttribute = getTargetAttribute(tableLogic);

            int[] counts = new int[2];
            counts[CTreeVertex.YES_INDEX] = 0;
            counts[CTreeVertex.NO_INDEX] = 0;

            if (targetAttribute != null)
            {
                bool bFirst = true;
                int targetAttributeIndex = 0;
                foreach (CTableEntry entry in entryList)
                {
                    if (bFirst == true)
                    {
                        for (int i = 0; i < entry.Size; i++)
                        {
                            if (entry[i].AttributeType.InternalName == targetAttribute.InternalName)
                            {
                                targetAttributeIndex = i;
                            }
                        }
                        bFirst = false;
                    }

                    // TODO Bei Zielattr nicht nur Binär Verzweigen
                    if (entry[targetAttributeIndex].TableValue == "j")
                    {
                        counts[CTreeVertex.YES_INDEX]++;
                    }
                    else if (entry[targetAttributeIndex].TableValue == "n")
                    {
                        counts[CTreeVertex.NO_INDEX]++;
                    }
                }
            }

            vertex.CountObjectsPerClass = counts;
        }

        private static CAttributeType getTargetAttribute(Logic.CTableLogic tableLogic)
        {
            List<CAttributeType> attributeTypeList = tableLogic.getAttributeTypes();
            CAttributeType targetAttribute = null;
            foreach (CAttributeType type in attributeTypeList)
            {
                if (type.TargetAttribute == true)
                {
                    targetAttribute = type;
                    break;
                }
            }
            return targetAttribute;
        }

        internal CTreeVertex getRoot()
        {
            return mRoot;
        }
    }
}
