namespace QuranApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		MainPage = new NavigationPage(new NewMainPage());

	}
}
