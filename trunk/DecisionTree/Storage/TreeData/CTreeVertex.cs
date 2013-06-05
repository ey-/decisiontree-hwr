using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DecisionTree.Storage.TreeData
{
    /// <summary>
    /// Klasse zum Speichern eines Knoten im Baum
    /// </summary>
    public class CTreeVertex : INotifyPropertyChanged
    {
        protected const string NO_RULE_SET_TEXT = "keine Regel festgelegt";
        public const int YES_INDEX = 0;
        public const int NO_INDEX = 1;

        protected CTreeVertex mParentVertex = null;
        protected List<CTreeVertex> mChildNodes = new List<CTreeVertex>();
        protected CTreeEdge mParentEdge = null;
        protected CTreeGraph mGraph;

        protected CAttributeType mAttributeType;

        protected string mName = "keine Regel festgelegt";

        #region Testdaten für Präsi -> Danach wieder löschen
        /*protected int mNumObjects;
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
        }*/
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
            AttributeType = attributeType;
        }

        /*********************************************************************/
        /// <summary>
        /// Attributtyp welcher durch diesen Knoten repräsentiert wird.
        /// </summary>
        public CAttributeType AttributeType
        {
            get { return mAttributeType; }
            set 
            { 
                mAttributeType = value;

                if (mAttributeType != null)
                {
                    mName = mAttributeType.Name;
                }
                else
                {
                    mName = NO_RULE_SET_TEXT;
                }
                NotifyPropertyChanged("VertexName");
                NotifyPropertyChanged("CountObjects");
                NotifyPropertyChanged("CountObjectsPerClass");
                NotifyPropertyChanged("Entropy");
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Übergeordneter Knoten dieses Knotens
        /// </summary>
        public CTreeVertex ParentVertex
        {
            get { return mParentVertex; }
        }

        public List<CTreeVertex> ChildList
        {
            get { return mChildNodes; }
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
            get { return (mAttributeType != null) ? mAttributeType.Name : NO_RULE_SET_TEXT; }
        }

        protected int mTotalObjectCount = 0;
        public int CountObjects
        {
            // TODO CTreeVertex::CountObjects
            get { return mTotalObjectCount; }
            set 
            { 
                mTotalObjectCount = value;
                NotifyPropertyChanged("CountObjects");
            }
        }

        protected int[] mCountObjectsPerClass = new int[2];
        /// <summary>
        /// Nochmal überdenken wie man das schöner machen kann
        /// </summary>
        public int[] CountObjectsPerClass
        {
            // // TODO CTreeVertex::CountObjectsPerClass
            get 
            {
                return mCountObjectsPerClass;
            }
            set
            {
                mCountObjectsPerClass = value;
                NotifyPropertyChanged("CountObjectsPerClass");
            }
        }

        double mEntropy = 0.0;
        public double Entropy
        {
            get { return mEntropy; }
            set 
            { 
                mEntropy = value;
                NotifyPropertyChanged("Entropy");
            }
        }

        double mWeightedEntropy = 0.0;
        public double WeightedEntropy
        {
            get { return mWeightedEntropy; }
            set
            {
                mWeightedEntropy = value;
                NotifyPropertyChanged("WeightedEntropy");
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

    }// class
} // namespace
