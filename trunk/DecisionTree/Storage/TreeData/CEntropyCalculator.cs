using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TreeData
{
    class CEntropyCalculator
    {

        public double getEntropy(CTreeVertex vertex)
        {
            if (vertex.CountObjectsPerClass[CTreeVertex.YES_INDEX] == 0 || vertex.CountObjectsPerClass[CTreeVertex.NO_INDEX] == 0)
            {
                return 0;
            }
            else
            {
                double yesFactor = (double)vertex.CountObjectsPerClass[CTreeVertex.YES_INDEX] / (double)vertex.CountObjects;
                double noFactor = (double)vertex.CountObjectsPerClass[CTreeVertex.NO_INDEX] / (double)vertex.CountObjects;

                return -(yesFactor * Math.Log(yesFactor, 2) + noFactor * Math.Log(noFactor, 2));
            }
        }

        public double getWeightedEntropy(CTreeVertex vertex)
        {
            double sumEntropy = 0.0;

            // für jedes Kind die Teilentropie berechnen lassen
            foreach (CTreeVertex child in vertex.ChildList)
            {
                double childEntityFactor = (double)child.CountObjects / (double)vertex.CountObjects;
                double childYesFactor = (double)child.CountObjectsPerClass[CTreeVertex.YES_INDEX] / child.CountObjects;
                double childNoFactor = (double)child.CountObjectsPerClass[CTreeVertex.NO_INDEX] / child.CountObjects;

                // sichergehen das wir kein NaN oder sonstiges auf sumEntropy aufaddieren
                if ((childEntityFactor != 0) && (double.IsNaN(childEntityFactor) == false) &&
                    (childYesFactor != 0) && (double.IsNaN(childYesFactor) == false) &&
                    (childNoFactor != 0) && (double.IsNaN(childNoFactor) == false))
                {
                    sumEntropy += childEntityFactor * (-childYesFactor * Math.Log(childYesFactor, 2) - childNoFactor * Math.Log(childNoFactor, 2));
                }
            }

            return sumEntropy;
        }
    }
}
