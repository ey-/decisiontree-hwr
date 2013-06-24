using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;

namespace DecisionTree.Storage.TreeData
{
    /// <summary>
    /// 
    /// </summary>
    public class CTreeGraph : BidirectionalGraph<CTreeVertex, CTreeEdge>
    {
        /*********************************************************************/
        /// <summary>
        /// Konstrukotr
        /// </summary>
        public CTreeGraph()
            : base() { }

        public CTreeGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public CTreeGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }

    }
}
