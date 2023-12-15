using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Cryptography;
using System.Text;

namespace DriverApp;

public partial class SignIn : ContentPage
{
    public static HubConnection _hubConnection;
    private string _jwt = string.Empty;

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
                _jwt = jwt;
                if (!string.IsNullOrEmpty(_jwt)) { App.Current.MainPage = new AppShell(); }
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
                await _hubConnection.InvokeCoreAsync("LoginTaiXe", args: new[]
                {
                    txtUsername.Text,
                    txtPassword.Text,
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
