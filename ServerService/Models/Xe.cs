using System;
using System.Collections.Generic;

namespace ServerService.Models;

public partial class Xe
{
    public int XeId { get; set; }

    public int? TxId { get; set; }

    public int? LxId { get; set; }

    public string XeBienso { get; set; } = null!;

    public byte[] XeGiayDangky { get; set; } = null!;

    public virtual ICollection<DatXe> DatXes { get; } = new List<DatXe>();

    public virtual LoaiXe? Lx { get; set; }

    public virtual TaiXe? Tx { get; set; }
}
