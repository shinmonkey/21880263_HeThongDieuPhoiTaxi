using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class DatXe
{
    public int DxId { get; set; }

    public DateTime DxNgayGio { get; set; }

    public string DxDiadiemdon { get; set; } = null!;

    public decimal DxGpsLon { get; set; }

    public decimal DxGpsLat { get; set; }

    public string KhTen { get; set; } = null!;

    public string KhPhone { get; set; } = null!;

    public int? KhId { get; set; }

    public int? TxId { get; set; }

    public int? XeId { get; set; }

    public int? LxId { get; set; }

    public int? TtdxId { get; set; }

}
