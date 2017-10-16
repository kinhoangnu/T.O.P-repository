/*
*  Copyright (c) 2015 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/
using System;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using com.vanderlande.wpf;

namespace Your
{
    public class YourApplication : ViApplication 
    {
        public YourApplication() :
            base("Configuration tool ", "T.O.P Project")
        { }

        protected override MainWindowViewModel CreateMainWindowViewModel()
        {
            MainWindowViewModel mainWnd = base.CreateMainWindowViewModel();
            mainWnd.RegisterContent(typeof(BuffersViewModel));
            mainWnd.RegisterContent(typeof(ProductionAreasViewModel));
            mainWnd.RegisterContent(typeof(ProcessesViewModel));
            mainWnd.RegisterContent(typeof(SecondaryActivitiesViewModel));
            mainWnd.RegisterContent(typeof(WorkstationClassesViewModel));
            mainWnd.RegisterContent(typeof(WorkstationGroupViewModel));
            mainWnd.RegisterContent(typeof(WorkstationsViewModel));
            mainWnd.RegisterContent(typeof(OperatorsViewModel));

            mainWnd.RegisterContent(typeof(LoadXMLViewModel));
            
            return mainWnd;
        }



        

    }

}

