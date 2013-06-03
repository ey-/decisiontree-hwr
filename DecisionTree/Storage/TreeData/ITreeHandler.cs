using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TreeData;

namespace DecisionTree.Storage
{
    public enum E_TREE_TYPE
    { 
        E_TREE_INTERACTIVE = 0,
        E_TREE_AUTOMATIC,
        E_TREE_COUNT
    }

    /// <summary>
    /// Schnittstelle für den Datenzugriff auf die Baumdaten.
    /// Alle Methoden die benötigt werden um auf die Daten zuzugreifen werden zuerst hier 
    /// eingefügt und dann durch die implementierende Klasse umgesetzt.
    /// </summary>
    public interface ITreeHandler
    {
        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        CTreeGraph getGraph();

        /*********************************************************************/
        /// <summary>
        /// Setzt den Baum der verwendet werden soll für die Zugriffe
        /// </summary>
        /// <param name="tree">Baum der verwendet werden soll</param>
        void setActiveTree(E_TREE_TYPE tree);

        /*********************************************************************/
        /// <summary>
        /// Setzt den Baum auf den Initalzustand mit einem leeren Root-Knoten 
        /// zurück
        /// </summary>
        void resetActiveTree();

        /*********************************************************************/
        /// <summary>
        /// Fügt einen Knoten zum Graphen hinzu
        /// </summary>
        /// <param name="parent">Parent-Knoten des Knotens</param>
        /// <param name="type">Typ des Attributes welches der Knoten 
        /// repräsentiert</param>
        /// <returns>erstellter Vertex</returns>
        CTreeVertex addVertex(CTreeVertex parent, CAttributeType type);

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Vertex aus dem Graphen. 
        /// !!Dabei werden KEINE Edges gelöscht!!
        /// </summary>
        /// <param name="vertex">zu Entfernender Vertex</param>
        /// <returns>Erfolg der Operation</returns>
        bool removeVertex(CTreeVertex vertex);

        /*********************************************************************/
        /// <summary>
        /// Löscht die Kindelement des übergebenen Vertex und die dazugehörigen 
        /// Verbindungen.
        /// </summary>
        /// <param name="vertex">Vertex dessen Kindknoten entfernt werden sollen</param>
        /// <returns>Erfolg der Operation</returns>
        bool removeChildVertices(CTreeVertex vertex);

        CTreeEdge addEdge(CTreeVertex parent, CTreeVertex child, CAttributeValue attributeValue);
        bool removeEdge(CTreeEdge edge);
    }
}
