using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using System.ComponentModel;

namespace DecisionTree.Storage.TreeData
{
    /// <summary>
    /// Klasse zum Speichern einer Verbindung zwischen zwei 
    /// Vertex im Baum
    /// </summary>
    public class CTreeEdge : TaggedEdge<CTreeVertex, CAttributeValue>, INotifyPropertyChanged
    {
        protected CAttributeValue mEdgeValue;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="source">Startpunkt der Verbindung</param>
        /// <param name="target">Endpunkt der Verbindung</param>
        /// <param name="mEdgeValue">Wert der Kante</param>
        public CTreeEdge(CTreeVertex source, CTreeVertex target, CAttributeValue edgeValue) 
            : base(source, target, edgeValue)
        {
            mEdgeValue = edgeValue;
        }
        
        public string EdgeValue
        {
            get 
            {
                if (mEdgeValue != null)
                {
                    return mEdgeValue.TableValue;
                }
                return "";
            }
        }

        public override string ToString()
        {
            return EdgeValue;
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
    }
}
