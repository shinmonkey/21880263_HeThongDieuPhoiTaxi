namespace CustomerApp;
using CommunityToolkit.Maui.Views;

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
        var result = await this.ShowPopupAsync(otpPopup);

        if (result is bool boolResult)
        {
            if (boolResult)
            {
                // Xử lý khi người dùng nhấn "Đồng ý"
            }
            else
            {
                // Xử lý khi người dùng nhấn "Hủy"
            }
        }
    }
}
public partial class OtpPopup : Popup
{
    public OtpPopup()
    {
        Content = new StackLayout
        {
            Children =
            {
                new Label { Text = "Nhập mã OTP của bạn:" , Margin= 10},
                new Entry { Placeholder = "Mã OTP", Margin= 10},
                new Button { Text = "Xác nhận", Margin = 10}
            },
            VerticalOptions= LayoutOptions.Center,
        };
    }
}