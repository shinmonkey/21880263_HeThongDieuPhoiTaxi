using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class TrangThaiDatXe
{
    public int TtdxId { get; set; }

    public string? TtdxTen { get; set; }

    public virtual ICollection<DatXe> DatXes { get; } = new List<DatXe>();
}
