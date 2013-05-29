using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TreeData
{
    /// <summary>
    /// 
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

        public CTreeGraph Graph
        {
            get { return mGraph; }
        }
    }
}
