using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public virtual ICollection<HubConnection> HubConnections { get; } = new List<HubConnection>();

    public virtual KhachHang? KhachHang { get; set; }

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual TaiXe? TaiXe { get; set; }
}
