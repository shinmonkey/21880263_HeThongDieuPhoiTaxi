using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class TaiXe
{
    public int TxId { get; set; }

    public byte[]? TxGiayPhepLaiXe { get; set; }

    public byte[]? TxKhamSucKhoe { get; set; }

    public byte[]? TxCccd { get; set; }

    public int? TtId { get; set; }

    public decimal? TxGpsLon { get; set; }

    public decimal? TxGpsLat { get; set; }

    public virtual ICollection<DatXe> DatXes { get; } = new List<DatXe>();

    public virtual TrangThaiTaiXe? Tt { get; set; }

    public virtual User? Tx { get; set; } = null!;

    public virtual ICollection<Xe> Xes { get; } = new List<Xe>();
}
