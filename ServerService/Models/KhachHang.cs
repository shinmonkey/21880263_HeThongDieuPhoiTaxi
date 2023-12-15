using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class KhachHang
{
    public int KhId { get; set; }

    public decimal? KhGpsLon { get; set; }

    public decimal? KhGpsLat { get; set; }

    public virtual ICollection<DatXe> DatXes { get; } = new List<DatXe>();

    public virtual User? Kh { get; set; } = null!;
}
