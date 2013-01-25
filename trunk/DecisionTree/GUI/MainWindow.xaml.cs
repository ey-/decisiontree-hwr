using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;
using DecisionTree.GUI;

namespace DecisionTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Insert code required on object creation below this point.


        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnTest)
            {
                if (this.grpTable.Visibility != System.Windows.Visibility.Visible)
                {
                    this.grpTable.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.grpTable.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }
    }
}
