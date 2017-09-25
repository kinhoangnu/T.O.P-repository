How to create your first VI_WPF application from scratch.....

Step 1: Create an empty application

1) Generate a WPF Application using Visual Studio (e.g. Your).
2) Create YourApplication.cs, derived from ViApplication
3) Adjust app.xaml.cs to reference YourApplication
4) Add a Resources folder
5) Add a Resources/Languages folder
6) Copy/link a en-US.xaml resource dictionary.
	Set properties
		'Build Action' to 'Content'
		'Copy to Output Directory' to 'Copy if newer'
7) Remove StartupUri from app.xaml. Also remove the MainWindow.xaml.
8) Add a reference to the VI_WPF assembly
9) Add a reference to the System Windows Interactivity assembly (Extension)


You now have a running empty application for a Guest user, in English.


Step 2: Add the first page (viewmodels).

1) Create ABCViewModel folder (best practice, create a folder for each viewmodel)
2) Create ABCView.xaml, using Add New Item/UserControl WPF
3) Create ABCViewModel, using Add New Item/Class
4) Remove .src.ABCViewModel from the namespace paths in all three files (.xaml, .xaml.cs, .cs)
5) Derive ABCViewModel from ContentViewModel (com.vanderlande.wpf)
6) Add the ABCViewModel to the list of available pages:
       protected override MainWindowViewModel CreateMainWindowViewModel()
        {
            MainWindowViewModel mainWnd = base.CreateMainWindowViewModel();
            mainWnd.RegisterContent(typeof (ABCViewModel));
            return mainWnd;
        }
7) Add English translation for the ABCViewModel, key 'ABC' and 'ABCDescription' to en-US.xaml.
	(just strip off 'ViewModel')

You now have a running application with an empty page.



Step 3: Fill the page using standard MVVM binding.

That's up to you.




