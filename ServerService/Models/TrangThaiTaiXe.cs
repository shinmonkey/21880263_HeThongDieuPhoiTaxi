using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class TrangThaiTaiXe
{
    public int TtId { get; set; }

    public string TtTen { get; set; } = null!;

    public virtual ICollection<TaiXe> TaiXes { get; } = new List<TaiXe>();
}
