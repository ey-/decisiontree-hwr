using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TreeData;
using DecisionTree.Storage;
using System.Threading;

namespace DecisionTree.Logic
{
    class CTDIDTAlgorithm
    {
        CTreeLogic mTreeLogic;
        CTableLogic mTableLogic;

        protected int mMinObjectsPerVertex = 1;

        public CTDIDTAlgorithm(CTreeLogic treeLogic, CTableLogic tableLogic)
        {
            mTreeLogic = treeLogic;
            mTableLogic = tableLogic;
        }

        internal void createGraph()
        {
            mTreeLogic.resetActiveTree();

            CTreeVertex rootNode = mTreeLogic.getRootNode();

            doTDIDT(rootNode);

            mTreeLogic.updateVertexValues();
        }

        /*********************************************************************/
        /// <summary>
        /// führt den TDIDT Algorithmus für einen Knoten durch und sucht nach
        /// dem Optimalen Attribut für diesen Knoten und seine Kindknoten
        /// </summary>
        /// <param name="vertex"></param>
        private void doTDIDT(CTreeVertex vertex)
        {
            mTreeLogic.updateVertexValues();
            if (stopCriteriaMet(vertex) == false)
            {
                // wir suchen das Optimale Attribut für diesen Knoten
                CAttributeType bestType = findBestAttribute(vertex);
                // nur wenn noch Attribute vorhanden sind machen wir weiter
//                if (bestType != null)
                {
                    mTreeLogic.setVertexAttribute(vertex, bestType);

                    foreach (CTreeVertex child in vertex.ChildList)
                    {
                        doTDIDT(child);
                    }
                }
            }
        }

        private bool stopCriteriaMet(CTreeVertex vertex)
        {
            if (vertex.CountObjects == 0) return true;
            if (vertex.CountObjectsPerClass[CTreeVertex.YES_INDEX] == 0) return true;
            if (vertex.CountObjectsPerClass[CTreeVertex.NO_INDEX] == 0) return true;
            if (vertex.CountObjects <= mMinObjectsPerVertex) return true;

            return false;
        }

        /*********************************************************************/
        /// <summary>
        /// Sucht nach einem Attribut, welches für den Knoten optimal ist
        /// </summary>
        /// <param name="vertex">Vertex für den der Typ gefunden werden soll</param>
        /// <returns>gefundener AttributTyp</returns>
        private CAttributeType findBestAttribute(CTreeVertex vertex)
        {
            double bestEntropy = double.MaxValue;
            CAttributeType bestAttrType = null;

            List<CAttributeType> attrTypeList = mTableLogic.getAttributeTypes();
            foreach (CAttributeType type in attrTypeList)
            {
                if ((type.Used == true) && (type.TargetAttribute == false) && 
                    (mTreeLogic.isAttributeUsedByParent(vertex, type) == false))
                {
                    mTreeLogic.setVertexAttribute(vertex, type);
                    mTreeLogic.updateVertexValues();

                    // Verhindern das ein Kindknoten alle Einträge enthält wie der Parent,
                    // dann können wir uns das auch sparen
                    if (childDoesOwnAllParentObjects(vertex) == false)
                    {
                        double entropy = calculateWeightedEntropy(vertex);
                        if (entropy < bestEntropy)
                        {
                            bestEntropy = entropy;
                            bestAttrType = type;
                        }
                    }
                }
            }

            return bestAttrType;
        }

        private double calculateWeightedEntropy(CTreeVertex vertex)
        {
            double sumEntropy = 0.0;

            // für jedes Kind die Teilentropie berechnen lassen
            foreach (CTreeVertex child in vertex.ChildList)
            {
                double childEntityFactor = (double)child.CountObjects / (double)vertex.CountObjects;
                double childYesFactor = (double)child.CountObjectsPerClass[CTreeVertex.YES_INDEX] / child.CountObjects;
                double childNoFactor = (double)child.CountObjectsPerClass[CTreeVertex.NO_INDEX] / child.CountObjects;

                // sichergehen das wir kein NaN oder sonstiges auf sumEntropy aufaddieren
                if ((childYesFactor != 0) && (double.IsNaN(childYesFactor) == false) &&
                    (childNoFactor != 0) && (double.IsNaN(childNoFactor) == false))
                {
                    sumEntropy += childEntityFactor * (-childYesFactor * Math.Log(childYesFactor, 2) - childNoFactor * Math.Log(childNoFactor, 2));
                }
            }

            return sumEntropy;
        }

        private bool childDoesOwnAllParentObjects(CTreeVertex parentVertex)
        {
            foreach (CTreeVertex child in parentVertex.ChildList)
            {
                if (child.CountObjects == parentVertex.CountObjects)
                {
                    return true;
                }
                if (child.CountObjects > 0)
                {
                    return false;
                }
            }
            return false;
        }

        internal void setMinObjectCountAutoTree(int number)
        {
            mMinObjectsPerVertex = number;
            System.Diagnostics.Debug.WriteLine("mMinObjectsPerVertex = " + mMinObjectsPerVertex);
        }
    }
}
