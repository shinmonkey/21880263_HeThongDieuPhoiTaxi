using Microsoft.AspNetCore.SignalR.Client;

namespace CustomerApp;

public partial class ChatHub : ContentPage
{
	public ChatHub(string message)
	{
		
		InitializeComponent();
        Dispatcher.Dispatch(() =>
		{
            lblChat.Text += $"{message}\n";
        });
        SignIn._hubConnection.On<bool, string>("ReceiveOrder", (check, message) => {
			Dispatcher.Dispatch(() =>
			{
				lblChat.Text += $"{message}\n";
			});
        });
    }

    private void btnSend_Clicked(object sender, EventArgs e)
    {
		Dispatcher.DispatchAsync(async () =>
		{
			await SignIn._hubConnection.InvokeCoreAsync("SendMessageToAll", args: new object[] {
			});
		});
    }

}