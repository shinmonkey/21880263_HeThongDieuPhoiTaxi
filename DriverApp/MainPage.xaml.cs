using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using ServerService.Models;
using System.Timers;

namespace DriverApp
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
                    trangthai.SelectedIndex = 1;
                    // Tạo một yêu cầu với độ chính xác mong muốn và thời gian chờ tối đa
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                    // Gọi phương thức GetLocationAsync để lấy vị trí hiện tại
                    _location = await Geolocation.Default.GetLocationAsync(request);

                    if (_location != null)
                    {
                        var addresses = await Microsoft.Maui.Devices.Sensors.Geocoding.Default.GetPlacemarksAsync(_location);
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
            SignIn._hubConnection.On<int,string>("ReceiveOrder", (id, messsage) =>
            {
                Dispatcher.Dispatch(async() =>
                {
                    if (trangthai.SelectedIndex == 1)
                    {
                        orderLayout.IsVisible = true;
                        DatXe order = JsonConvert.DeserializeObject<DatXe>(messsage);
                        order.DxId = id;
                        if (order != null)
                        {
                            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                            // Gọi phương thức GetLocationAsync để lấy vị trí hiện tại
                            _location = new Location((double)order.DxGpsLat,(double)order.DxGpsLon);

                            if (_location != null)
                            {
                                var addresses = await Microsoft.Maui.Devices.Sensors.Geocoding.Default.GetPlacemarksAsync(_location);
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
                    }
                });
            });
        }

        private void OKBtn_Clicked(object sender, EventArgs e)
        {
            Dispatcher.Dispatch(() =>
            {
                Navigation.PushAsync(new OrderDetail());
            });
        }

        private void cancelBtn_Clicked(object sender, EventArgs e)
        {
            cancelOrder();
        }

        private void cancelOrder()
        {
            orderLayout.IsVisible = false;
        }
    }
}