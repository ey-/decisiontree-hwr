using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TreeData
{
    /// <summary>
    /// Klasse zum Speichern eines Knoten im Baum
    /// </summary>
    public class CTreeVertex
    {
        protected CTreeVertex mParentVertex;
        protected CTreeEdge mParentEdge;
        protected CTreeGraph mGraph;

        protected CAttributeType mAttributeType;

        #region Testdaten für Präsi -> Danach wieder löschen
        protected int mNumObjects;
        protected int mNumObjectsYes;
        protected int mNumObjectsNo;
        protected double mEntropy;

        public void setDemoData(string vertexName, int countObjects, int countObjectsYes, int countObjectsNo, double entropy)
        {
            if (vertexName != "")
            {
                mAttributeType = new CAttributeType(0);
                mAttributeType.Name = vertexName;
            }

            mNumObjects = countObjects;
            mNumObjectsYes = countObjectsYes;
            mNumObjectsNo = countObjectsNo;
            mEntropy = entropy;
        }
        #endregion

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="parent">Übergeordneter Knoten im Baum</param>
        /// <param name="attributeType">Attributtyp nach dem aufgeteilt werden soll</param>
        public CTreeVertex(CTreeVertex parent, CTreeGraph graph, CAttributeType attributeType = null)
        { 
            mParentVertex = parent;
            mGraph = graph;
            mAttributeType = attributeType;
        }

        /*********************************************************************/
        /// <summary>
        /// Übergeordneter Knoten dieses Knotens
        /// </summary>
        public CTreeVertex ParentVertex
        {
            get { return mParentVertex; }
        }

        /*********************************************************************/
        /// <summary>
        /// Verbindung zum übergeordnetten Knoten
        /// </summary>
        public CTreeEdge ParentEdge
        {
            get { return mParentEdge; }
            set { mParentEdge = value; }
        }



        public string VertexName
        {
            get
            {
                if (mAttributeType != null)
                {
                    return mAttributeType.Name;
                }
                return "keine Regel festgelegt";
            }
        }

        public int CountObjects
        {
            get { return mNumObjects; }
        }

        /// <summary>
        /// Nochmal überdenken wie man das schöner machen kann
        /// </summary>
        public int[] CountObjectsPerClass
        { 
            get 
            {
                int [] ret = new int[2];
                ret[0] = mNumObjectsYes;
                ret[1] = mNumObjectsNo;
                return ret;
            }
        }

        public double Entropy
        {
            get { return mEntropy; }
        }

        

    }// class
} // namespace
