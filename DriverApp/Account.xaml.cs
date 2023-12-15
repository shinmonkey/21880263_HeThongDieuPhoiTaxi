namespace DriverApp;

public partial class Account : ContentPage
{
	public Account()
	{
		InitializeComponent();
	}
    private void SignOut_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new SignIn();
    }
}