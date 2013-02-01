﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.GUI;
using System.Collections.ObjectModel;
using DecisionTree.Storage;
using DecisionTree.Storage.TableData;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Schnittstelle für Zugriff auf die Logikebene. Alle Methoden die benötigt werden,
    /// werden zuerst hier eingefügt und dann in der implementierenden Klasse umgesetzt.
    /// </summary>
    public interface IBusinessLogic
    {
        /*********************************************************************/
        /// <summary>
        /// Registiert das MainWindow Interface damit der Zugriff auf die Fenster erfolgen kann.
        /// </summary>
        /// <param name="mainWindow">Interface zum MainWindow</param>
        void registerWindow(IMainWindow mainWindow);
        CTableEntryList getAllTableData();


    }
}
