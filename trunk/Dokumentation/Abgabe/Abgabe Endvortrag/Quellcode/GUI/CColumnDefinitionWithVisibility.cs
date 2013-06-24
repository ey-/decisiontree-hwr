using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DecisionTree.GUI
{
    /// <summary>
    /// Von http://www.codeproject.com/Articles/437237/WPF-Grid-Column-and-Row-Hiding
    /// </summary>
    public class CColumnDefinitionWithVisibility : ColumnDefinition
    {
        // Variables
        public static DependencyProperty VisibleProperty;

        // Properties
        public Boolean Visible
        {
            get { return (Boolean)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Constructors
        static CColumnDefinitionWithVisibility()
        {
            VisibleProperty = DependencyProperty.Register("Visible",
                typeof(Boolean),
                typeof(CColumnDefinitionWithVisibility),
                new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));
            
            ColumnDefinition.WidthProperty.OverrideMetadata(typeof(CColumnDefinitionWithVisibility),
                new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
                    new CoerceValueCallback(CoerceWidth)));
        }

        // Get/Set
        public static void SetVisible(DependencyObject obj, Boolean nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }
        public static Boolean GetVisible(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(VisibleProperty);
        }

        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(ColumnDefinition.WidthProperty);
        }
        static Object CoerceWidth(DependencyObject obj, Object nValue)
        {
            return (((CColumnDefinitionWithVisibility)obj).Visible) ? nValue : new GridLength(0);
        }
    }
}
