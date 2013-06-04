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

            mRoot = new CTreeVertex(null, mGraph);
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
            foreach (CTreeEdge edge in mEdgeList)
            {
                mGraph.RemoveEdge(edge);
            }
            mEdgeList.Clear();
            foreach (CTreeVertex vertex in mVertexList)
            {
                mGraph.RemoveVertex(vertex);
            }
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
    }
}
