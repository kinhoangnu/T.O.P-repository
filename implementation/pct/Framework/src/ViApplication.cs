/*
*  Copyright (c) 2017 Vanderlande Industries
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for the (singleton) application with some common default processing
    /// like: Startup/Exit/Exception handling (virtual methods),
    /// Idle processing (virtual method and as event),
    /// Initialize the default Mediator.
    /// </summary>
    public abstract class ViApplication
    {
        // Can ConcentViewModel pages be closed ?
        public bool CanClosePages = true;

        // Is the application designed to be used on tablets?
        public bool DesignedForTablet = false;

        // If running on a tablet, should it be full screen?
        public bool FullScreenOnTablet = true;

        // If not running on a tablet, what size should be used for "full screen".
        public int FullScreenOnTabletWidth = 1366;
        public int FullScreenOnTabletHeight = 700;
        public EventHandler OnIdleHandler;

        protected bool SingleInstance = false; // Can there be only one instance of this application?

        private readonly List<FunctionalModule> _functionalModules;

        private bool _isMessageBoxVisible;
        private Mutex _instanceMutex; // The created mutex exist as long as the application exists

        private InitAssemblies _initAssemblies;

        private string _language;

        /// <summary>
        /// Get the one and only VI MVVM Application instance.
        /// </summary>
        public static ViApplication Instance { get; private set; }

        /// <summary>
        /// Get the WPF Application instance.
        /// </summary>
        public Application WPFApplication
        {
            get { return Application.Current; }
        }

        /// <summary>
        /// Get the Project the application belongs to.
        /// </summary>
        public string Project { get; protected set; }

        /// <summary>
        /// Get the name of the application.
        /// </summary>
        public string Name { get; protected set; }

        public string ApplicationDataFolder
        {
            get
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                folder = Path.Combine(folder, "Vanderlande");
                if (Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }
                folder = Path.Combine(folder, Instance.Project);
                if (Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;
            }
        }

        public string Language
        {
            get { return _language; }
            set { SetLanguage(value); }
        }

        public MainWindowViewModel MainWindowViewModel { get; private set; }

        protected ViApplication(string name, string project)
        {
            Debug.Assert(Instance == null);
            Instance = this;
            _functionalModules = new List<FunctionalModule>();
            InitializeProperties(name, project);
            InitializeEvents();
        }

        public virtual void OnIdle()
        {
            InActivityManager.Step();
            Mediator.HandlePostedEvents();
            foreach (var fm in _functionalModules)
            {
                fm.DoOnIdle();
            }
            if (OnIdleHandler != null)
            {
                OnIdleHandler(this, null);
            }
        }

        public bool ActivateFunctionalModule(FunctionalModule mod)
        {
            if (mod.IsActive)
            {
                return true;
            }
            foreach (var fm in _functionalModules.Where(x => x.IsActive))
            {
                if (DeActivateFunctionalModule(fm) == false)
                {
                    return false;
                }
            }
            if (mod.DoCanActivate() == false)
            {
                return false;
            }
            return mod.DoActivate();
        }

        public bool DeActivateFunctionalModule(FunctionalModule mod)
        {
            if (mod.IsActive == false)
            {
                return true;
            }
            if (mod.DoCanDeActivate() == false)
            {
                return false;
            }
            return mod.DoDeActivate();
        }

        protected void AddFunctionalModule(FunctionalModule fm)
        {
            _functionalModules.Add(fm);
        }

        protected virtual void OnStartup(object sender, StartupEventArgs args)
        {
            InitializeMediator();
            SetLanguage("en-US");
            Dispatcher.CurrentDispatcher.Hooks.DispatcherInactive += DoIdle;
            _initAssemblies = new InitAssemblies();

            MainWindowViewModel = CreateMainWindowViewModel();
            if (MainWindowViewModel == null)
            {
                return;
            }
            ViewLocator.CreateView(MainWindowViewModel);
            WPFApplication.MainWindow = MainWindowViewModel.Window;
            ResizeForTablet();
            WPFApplication.MainWindow.Show();

            InActivityManager.Add(new ScreenSaver());

            foreach (var fm in _functionalModules)
            {
                fm.DoInitialize();
            }
        }

        protected virtual void OnExit(object sender, ExitEventArgs args)
        {
            foreach (var fm in _functionalModules)
            {
                fm.DoDispose();
            }
            Dispatcher.CurrentDispatcher.Hooks.DispatcherInactive -= DoIdle;
            WPFApplication.Exit -= OnExit;
            WPFApplication.Startup -= DoStartupEvent;
            WPFApplication.DispatcherUnhandledException -= DoUnhandledException;
            Mediator.Default.Clear();
        }

        protected virtual bool OnUnhandledException(Exception ex)
        {
            if (_isMessageBoxVisible == false) // Prevent a cascade of messageboxes.
            {
                _isMessageBoxVisible = true;
                var message = "Unhandled exception:";
                while (ex != null)
                {
                    message += "\n\t-" + ex.Message;
                    ex = ex.InnerException;
                }
                message += "\n\nThe application could become unstable.";
                MessageBox.Show(message, Name);
                _isMessageBoxVisible = false;
            }
            return true; // Consider the exception as handled.
        }

        protected virtual MainWindowViewModel CreateMainWindowViewModel()
        {
            return new MainWindowViewModel();
        }

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        private void InitializeProperties(string name, string project)
        {
            Thread.CurrentThread.Name = "Main thread " + name;
            Name = name;
            Project = project;
            MainWindowViewModel = null;
        }

        private void InitializeEvents()
        {
            WPFApplication.DispatcherUnhandledException += DoUnhandledException;
            WPFApplication.Startup += DoStartupEvent;

            /*
                    WPFApplication.Activated += OnActivated;
                    WPFApplication.Deactivated += OnDeactivated;
                    WPFApplication.SessionEnding += OnSessionEnding;
                    WPFApplication.FragmentNavigation += OnFragmentNavigation;
                    WPFApplication.LoadCompleted += OnLoadCompleted;
                    WPFApplication.Navigated += OnNavigated;
                    WPFApplication.Navigating += OnNavigating;
                    WPFApplication.NavigationFailed += OnNavigationFailed;
                    WPFApplication.NavigationProgress += OnNavigationProgress;
                    WPFApplication.NavigationStopped += OnNavigationStopped;
        */
        }

        // Create the (default) mediator, attach an exception handler and register commands.
        private void InitializeMediator()
        {
            var mediator = Mediator.Default;
            mediator.OnException += (a, b) => OnUnhandledException(b);

            mediator.Register<CommandBase>(this, OnCommandBase);
        }

        private void OnCommandBase(CommandBase cmd)
        {
            try
            {
                cmd.Execute();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("{0}.Execute: {1}", cmd.GetType().Name, ex.Message));
            }
        }

        private void DoStartupEvent(object sender, StartupEventArgs e)
        {
            if (CanRunThisInstance() == false)
            {
                return;
            }
            WPFApplication.Exit += OnExit;
            OnStartup(sender, e);
        }

        private void DoUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = OnUnhandledException(args.Exception);
        }

        private void DoIdle(object sender, EventArgs args)
        {
            try
            {
                OnIdle();
            }
            catch (Exception ex)
            {
                OnUnhandledException(ex);
            }
        }

        private void SetLanguage(string value)
        {
            var path = new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            path += "\\resources\\Languages\\" + value + ".xaml";
            var uriObj = new Uri(path, UriKind.Absolute);
            var resourceDictionary = new ResourceDictionary { Source = uriObj };

            var dict = WPFApplication.Resources.MergedDictionaries;
            if (_language == null)
            {
                dict.Add(resourceDictionary);
            }
            else
            {
                dict[dict.Count - 1] = resourceDictionary;
                ViewModel.RefreshAllProperties();
            }
            _language = value;
        }

        /// Check if given application is allowed to start.
        /// returns false if not.
        private bool CanRunThisInstance()
        {
            if (SingleInstance == false)
            {
                return true;
            }
            bool created;
            _instanceMutex = new Mutex(true, "Global\\Vanderlande " + Project + " " + Name, out created);
            if (created)
            {
                _instanceMutex.ReleaseMutex();
                return true;
            }
            ShowOtherProcess();
            WPFApplication.Shutdown();
            return false;
        }

        // Search for running instance and make that window topmost
        private void ShowOtherProcess()
        {
            var thisProcess = Process.GetCurrentProcess();
            var otherProcess =
                Process.GetProcessesByName(thisProcess.ProcessName).FirstOrDefault(x => x.Id != thisProcess.Id);
            if (otherProcess != null)
            {
                if (IsIconic(otherProcess.MainWindowHandle))
                {
                    const int SW_RESTORE = 9;
                    ShowWindow(otherProcess.MainWindowHandle, SW_RESTORE);
                }
                SetForegroundWindow(otherProcess.MainWindowHandle);
            }
        }

        private void ResizeForTablet()
        {
            if (DesignedForTablet == false || FullScreenOnTablet == false)
            {
                return; // The application is not designed for tablets or it should not run full screen by default.
            }

            const int SM_TABLETPC = 86;
            if (GetSystemMetrics(SM_TABLETPC) != 0)
            {
                // The application is running on a tablet.
                MainWindowViewModel.Window.WindowState = WindowState.Maximized;
            }
            else // The tablet application is not running on a tablet; use predefined size.
            {
                MainWindowViewModel.Window.Width = FullScreenOnTabletWidth;
                MainWindowViewModel.Window.Height = FullScreenOnTabletHeight;
            }
        }
    }
}