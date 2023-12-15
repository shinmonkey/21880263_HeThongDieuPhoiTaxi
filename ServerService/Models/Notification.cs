using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string MessageType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
