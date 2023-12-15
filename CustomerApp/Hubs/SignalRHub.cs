using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace CustomerApp.Hubs
{
    public class SignalRHub 
    {
        private readonly HubConnection _hubConnection;
        public SignalRHub()
        {
            var baseUrl = "http://localhost";

            // Android can't connect to localhost
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                baseUrl = "http://10.0.2.2";
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}:5127/chatHub")
                .Build();
        }
    }
}
