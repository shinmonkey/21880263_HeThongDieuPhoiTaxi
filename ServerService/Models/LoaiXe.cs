using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class LoaiXe
{
    public int LxId { get; set; }

    public string? LxTen { get; set; }

    public virtual ICollection<DatXe> DatXes { get; } = new List<DatXe>();

    public virtual ICollection<Xe> Xes { get; } = new List<Xe>();
}
