namespace DriverApp;
using CommunityToolkit.Maui.Views;
using Geocoding;
using System.Windows.Input;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
	}

    private async void signUpBtn_Clicked(object sender, EventArgs e)
    {
        await DisplayOtpPopup();
    }

    public async Task DisplayOtpPopup()
    {
        var otpPopup = new OtpPopup();
        await this.ShowPopupAsync(otpPopup);
        Dispatcher.Dispatch(() =>
        {
            if (otpPopup.OtpCode == "OTP")
            {
                App.Current.MainPage = new SignUpNext();
            }
        });
    }
}
public partial class OtpPopup : Popup
{
    public string OtpCode { get; set; }

    public ICommand ConfirmCommand { get; set; }
    Entry entry = new Entry { Placeholder = "Mã OTP", Margin = 10 };

    public OtpPopup()
    {
        entry.SetBinding(Entry.TextProperty, nameof(OtpCode));
        var confirmBtn = new Button { Text = "Xác nhận", Margin = 10 };
        confirmBtn.Clicked += ConfirmBtn_Clicked;
        Content = new StackLayout
        {
            Children =
            {
                new Label { Text = "Nhập mã OTP của bạn:" , Margin= 10},
                entry,
                confirmBtn
            },
            VerticalOptions= LayoutOptions.Center,
        };

    }

    private void ConfirmBtn_Clicked(object sender, EventArgs e)
    {
        OtpCode = entry.Text;
        this.Close();
    }
}