using CustomerApp.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace CustomerApp;

public partial class SignIn : ContentPage
{
    public static HubConnection _hubConnection;
    public static string JWT = string.Empty;
    public static User UserData;

    public SignIn()
	{

		InitializeComponent();
        var baseUrl = "http://localhost";

        // Android can't connect to localhost
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            baseUrl = "http://10.0.2.2";
        }

        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}:5077/notificationHub")
            .WithAutomaticReconnect()
            .Build();
        _hubConnection.On<string, string>("ReceiveJWT", (user, jwt) =>
        {
            Dispatcher.Dispatch(() =>
            {
                if (!string.IsNullOrEmpty(jwt))
                    UserData = JsonConvert.DeserializeObject<User>(jwt);
                if (UserData != null) { App.Current.MainPage = new AppShell(); }
            });
        });

        Task.Run(() =>
        {
            Dispatcher.Dispatch(async () =>
            {
                await _hubConnection.StartAsync();
            });
        });

    }
    private void SignInClicked(object sender, EventArgs e)
    {
        Dispatcher.Dispatch(async () =>
        {
            await _hubConnection.InvokeCoreAsync("LoginKhachHang", args: new[]
            {
                txtUsername.Text,
                txtPassword.Text
            });            
        });
    }

    private void SignUp_Clicked(object sender, EventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            App.Current.MainPage = new SignUp();
        });

    }

}