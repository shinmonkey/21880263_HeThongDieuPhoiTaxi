using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using ServerService.Models;

namespace CustomerApp
{
    public partial class MainPage : ContentPage
    {
        Location _location;
        public MainPage()
        {
            InitializeComponent();
            Dispatcher.Dispatch(async () =>
            {
                try
                {
                    // Tạo một yêu cầu với độ chính xác mong muốn và thời gian chờ tối đa
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                    // Gọi phương thức GetLocationAsync để lấy vị trí hiện tại
                    _location = await Geolocation.Default.GetLocationAsync(request);

                    if (_location != null)
                    {
                        var addresses = await Geocoding.Default.GetPlacemarksAsync(_location);
                        if (addresses.Any())
                        {
                            var _address = addresses.First();
                            txtaddress.Text = $"{_address.FeatureName}, {_address.Thoroughfare}, {_address.Locality}, {_address.AdminArea}, {_address.CountryName} ";
                        }
                        Pin pin = new Pin
                        {
                            Location = _location,
                            Label = txtaddress.Text,
                            Type = PinType.Place
                        };
                        MapSpan mapSpan = MapSpan.FromCenterAndRadius(_location, Distance.FromKilometers(0.444));
                        map.MoveToRegion(mapSpan);
                        map.Pins.Clear();
                        map.Pins.Add(pin);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý các ngoại lệ có thể xảy ra
                    Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("Error", $"Không thể lấy vị trí: {ex.Message}", "OK");
                    });
                }
            });
            SignIn._hubConnection.On<bool, string>("ReceiveOrder", (check, message) =>
            {
                if (check)
                {
                    Dispatcher.Dispatch(() =>
                    {
                        Navigation.PushAsync(new ChatHub(message));
                    });
                }
            });

        }
        private async void geocodinghBtn_Clicked(object sender, EventArgs e)
        {
            string address = txtaddress.Text;
            IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);
            _location = locations?.FirstOrDefault();
            if (_location != null)
            {
                var addresses = await Geocoding.Default.GetPlacemarksAsync(_location);
                if (addresses.Any())
                {
                    var _address = addresses.First();
                    txtaddress.Text = $"{_address.FeatureName} {_address.Thoroughfare} {_address.Locality} {_address.CountryName} ";
                }
                Pin pin = new Pin
                {
                    Location = _location,
                    Label = txtaddress.Text,
                    Type = PinType.Place
                };
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(_location, Distance.FromKilometers(0.444));
                map.MoveToRegion(mapSpan);
                map.Pins.Clear();
                map.Pins.Add(pin);
            }
        }

        private void OrderBtn_Clicked(object sender, EventArgs e)
        {
            Dispatcher.Dispatch(async () =>
            {
                var _order = new DatXe()
                {
                    KhTen = SignIn.UserData.FullName,
                    KhPhone = SignIn.UserData.Phone,
                    KhId = SignIn.UserData.Id,
                    DxDiadiemdon = txtaddress.Text,
                    LxId = kindCar.SelectedIndex+1,
                    DxNgayGio = DateTime.Now,
                    DxGpsLat =(decimal)_location.Latitude,
                    DxGpsLon =(decimal)_location.Longitude,
                };
                var message = JsonConvert.SerializeObject(_order);
                await SignIn._hubConnection.InvokeAsync("SendOrder", message);
            });
        }
    }
}