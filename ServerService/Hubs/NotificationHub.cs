using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ServerService.Models;
using JWT.Exceptions;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ServerService.Hubs
{
    public class NotificationHub : Hub
    {
        readonly CarHubContext _dbContext;
        readonly IConfiguration _configuration;
        public NotificationHub(CarHubContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task SendMessage(string user, string message)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == 1);
            if (_user != null)
            {
                user = _user.Username;
            }
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendOrder(string messsage )
        {
            var order = JsonConvert.DeserializeObject<DatXe>(messsage);
            if (order != null)
            {
                try
                {
                    order.TtdxId = 1;
                    await _dbContext.DatXes.AddAsync(order);
                    await _dbContext.SaveChangesAsync();
                    var _drivers = _dbContext.TaiXes.ToList();
                    foreach (var driver in _drivers)
                    {
                        var _connectionids = _dbContext.HubConnections.Where(s => s.Id == driver.TxId).ToList();
                        foreach (var connectionid in _connectionids)
                        {
                            await Clients.Clients(connectionid.Connectionid).SendAsync("ReceiveOrder",order.DxId, messsage);
                        }
                    }
                    await Clients.Caller.SendAsync("ReceiveOrder", true, messsage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await Clients.Caller.SendAsync("ReceiveOrder", false, messsage);

        }

        public async Task Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { return; }
            var _user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (_user != null && BCrypt.Net.BCrypt.EnhancedVerify(password, _user.Password))
            {
                var secret = _configuration.GetSection("Jwt")["Key"];

                var payload = new Dictionary<string, object>
            {
                { "id", _user.Id },
                { "username", _user.Username }
            };
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                var token = encoder.Encode(payload, secret);
                SaveUserConnected(_user.Id, _user.Username, Context.ConnectionId);
                await Clients.Caller.SendAsync("ReceiveJWT", username, token);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveJWT", username, string.Empty);
            }
        }

        public void SaveUserConnected(int userid, string username, string connectionid)
        {
            var _Hubuser = _dbContext.HubConnections.Where(u => u.Id == userid).ToList();
            var _connecteduser = new HubConnection
            {
                Id = userid,
                Connectionid = connectionid,
                Username = username,
            };
            _dbContext.HubConnections.Add(_connecteduser);
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }

        }
        public async Task LoginTaiXe(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { return; }
            var _user = await _dbContext.Users.Include(u => u.TaiXe).FirstOrDefaultAsync(u => u.Username == username && u.TaiXe != null);
            if (_user != null && BCrypt.Net.BCrypt.EnhancedVerify(password, _user.Password))
            {
                var secret = _configuration.GetSection("Jwt")["Key"];

                var payload = new Dictionary<string, object>
            {
                { "id", _user.Id },
                { "username", _user.Username }
            };
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                var token = encoder.Encode(payload, secret);
                SaveUserConnected(_user.Id, _user.Username, Context.ConnectionId);
                await Clients.Caller.SendAsync("ReceiveJWT", username, token);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveJWT", username, string.Empty);
            }
        }
        public async Task LoginKhachHang(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { return; }
            var _user = await _dbContext.Users.Include(u => u.KhachHang).FirstOrDefaultAsync(u => u.Username == username && u.KhachHang != null);
            if (_user != null && BCrypt.Net.BCrypt.EnhancedVerify(password, _user.Password))
            {
                var secret = _configuration.GetSection("Jwt")["Key"];

                var payload = new Dictionary<string, object>
            {
                { "Id", _user.Id },
            };
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                var token = encoder.Encode(payload, secret);
                var __user = new Dictionary<string, object>()
                {
                    { "Id", _user.Id},
                    { "Username", _user.Username },
                    { "FullName", _user.FullName },
                    { "Phone", _user.Phone },
                    { "JWT", token }
                };
                var message = JsonConvert.SerializeObject(__user);
                SaveUserConnected(_user.Id, _user.Username, Context.ConnectionId);
                await Clients.Caller.SendAsync("ReceiveJWT", username, message);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveJWT", username, string.Empty);
            }
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var secret = _configuration.GetSection("Jwt")["Key"];
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtValidator validator = new JwtValidator(serializer, new UtcDateTimeProvider());
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            try
            {
                var json = decoder.Decode(token, secret, verify: true); //token sẽ được kiểm tra ở đây
                var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                if (payload == null) return null;
                var claims = payload.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())).ToArray();
                var identity = new ClaimsIdentity(claims);
                return new ClaimsPrincipal(identity);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token đã hết hạn");
                return null;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token có chữ ký không hợp lệ");
                return null;
            }
        }
    }
}
