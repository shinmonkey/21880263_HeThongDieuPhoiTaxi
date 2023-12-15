using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class HubConnection
{
    public string Connectionid { get; set; } = null!;

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public virtual User IdNavigation { get; set; } = null!;
}
