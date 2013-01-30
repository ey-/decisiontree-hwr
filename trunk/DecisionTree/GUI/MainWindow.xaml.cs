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
using DecisionTree.Logic;

namespace DecisionTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        IBusinessLogic mBusinessLogic;

        /*********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            mBusinessLogic = CBusinessLogic.getInstance();
            mBusinessLogic.registerWindow(this);
        }
        
        /*********************************************************************/
        /// <summary>
        /// Wird aufgerufen wenn eine Button im Ribbonelement geklickt wird.
        /// </summary>
        /// <param name="sender">Welcher Button geklickt wurde</param>
        /// <param name="e">irgendwelche Parameter</param>
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
