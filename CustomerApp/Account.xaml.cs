using CustomerApp.Models;

namespace CustomerApp;

public partial class Account : ContentPage
{
	public Account()
	{
		InitializeComponent();
		if (SignIn.UserData != null)
		{
			Dispatcher.Dispatch(() =>
			{
                txtTenTaiKhoan.Text = SignIn.UserData.FullName;
                txtPhone.Text += SignIn.UserData.Phone;
            });
		}
	}

    private void SignOut_Clicked(object sender, EventArgs e)
    {
		SignIn.UserData = null;
        App.Current.MainPage = new SignIn();
    }
}