using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ServerService.Models;

public partial class CarHubContext : DbContext
{
    public CarHubContext()
    {
    }

    public CarHubContext(DbContextOptions<CarHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DatXe> DatXes { get; set; }

    public virtual DbSet<HubConnection> HubConnections { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiXe> LoaiXes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<TaiXe> TaiXes { get; set; }

    public virtual DbSet<TrangThaiDatXe> TrangThaiDatXes { get; set; }

    public virtual DbSet<TrangThaiTaiXe> TrangThaiTaiXes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatXe>(entity =>
        {
            entity.HasKey(e => e.DxId).HasName("PK__DatXe__D89627D5FEC6DC60");

            entity.ToTable("DatXe");

            entity.Property(e => e.DxId).HasColumnName("dx_id");
            entity.Property(e => e.DxDiadiemdon)
                .HasMaxLength(128)
                .HasColumnName("dx_diadiemdon");
            entity.Property(e => e.DxGpsLat)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("dx_GPS_lat");
            entity.Property(e => e.DxGpsLon)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("dx_GPS_lon");
            entity.Property(e => e.DxNgayGio)
                .HasColumnType("datetime")
                .HasColumnName("dx_ngayGio");
            entity.Property(e => e.KhId).HasColumnName("kh_id");
            entity.Property(e => e.KhPhone)
                .HasMaxLength(12)
                .HasColumnName("kh_phone");
            entity.Property(e => e.KhTen)
                .HasMaxLength(128)
                .HasColumnName("kh_ten");
            entity.Property(e => e.LxId).HasColumnName("lx_id");
            entity.Property(e => e.TtdxId).HasColumnName("ttdx_id");
            entity.Property(e => e.TxId).HasColumnName("tx_id");
            entity.Property(e => e.XeId).HasColumnName("xe_id");

            entity.HasOne(d => d.Kh).WithMany(p => p.DatXes)
                .HasForeignKey(d => d.KhId)
                .HasConstraintName("FK_DatXe_khachHang");

            entity.HasOne(d => d.Lx).WithMany(p => p.DatXes)
                .HasForeignKey(d => d.LxId)
                .HasConstraintName("FK_DATXE_LOAIXE");

            entity.HasOne(d => d.Ttdx).WithMany(p => p.DatXes)
                .HasForeignKey(d => d.TtdxId)
                .HasConstraintName("FK_DATXE_TrangThaiDatXe");

            entity.HasOne(d => d.Tx).WithMany(p => p.DatXes)
                .HasForeignKey(d => d.TxId)
                .HasConstraintName("Fk_DatXe_TaiXe");

            entity.HasOne(d => d.Xe).WithMany(p => p.DatXes)
                .HasForeignKey(d => d.XeId)
                .HasConstraintName("FK_DATXE_XE");
        });

        modelBuilder.Entity<HubConnection>(entity =>
        {
            entity.HasKey(e => e.Connectionid).HasName("PK__HubConne__D4A5C81224A5DAA0");

            entity.ToTable("HubConnection");

            entity.Property(e => e.Connectionid)
                .HasMaxLength(64)
                .HasColumnName("CONNECTIONID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Username).HasMaxLength(64);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.HubConnections)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HubConnection_User");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.KhId).HasName("PK__KhachHan__667B75A3035DC87D");

            entity.ToTable("KhachHang");

            entity.Property(e => e.KhId)
                .ValueGeneratedNever()
                .HasColumnName("kh_id");
            entity.Property(e => e.KhGpsLat)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("kh_gps_lat");
            entity.Property(e => e.KhGpsLon)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("kh_gps_lon");

            entity.HasOne(d => d.Kh).WithOne(p => p.KhachHang)
                .HasForeignKey<KhachHang>(d => d.KhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KhachHang_NguoiDung");
        });

        modelBuilder.Entity<LoaiXe>(entity =>
        {
            entity.HasKey(e => e.LxId).HasName("PK__LoaiXe__712D56B623245D32");

            entity.ToTable("LoaiXe");

            entity.Property(e => e.LxId).HasColumnName("lx_id");
            entity.Property(e => e.LxTen)
                .HasMaxLength(32)
                .HasColumnName("lx_ten");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__NOTIFICA__83A4A4464AC02D1A");

            entity.ToTable("NOTIFICATION");

            entity.Property(e => e.NotificationId).HasColumnName("NOTIFICATION_ID");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATETIME");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Message).HasColumnName("MESSAGE");
            entity.Property(e => e.MessageType)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICATION_User");
        });

        modelBuilder.Entity<TaiXe>(entity =>
        {
            entity.HasKey(e => e.TxId).HasName("PK__TaiXe__65E44DD102B31B37");

            entity.ToTable("TaiXe");

            entity.Property(e => e.TxId)
                .ValueGeneratedNever()
                .HasColumnName("tx_id");
            entity.Property(e => e.TtId).HasColumnName("tt_id");
            entity.Property(e => e.TxCccd).HasColumnName("tx_cccd");
            entity.Property(e => e.TxGiayPhepLaiXe).HasColumnName("tx_giayPhepLaiXe");
            entity.Property(e => e.TxGpsLat)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("tx_GPS_lat");
            entity.Property(e => e.TxGpsLon)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("tx_GPS_lon");
            entity.Property(e => e.TxKhamSucKhoe).HasColumnName("tx_KhamSucKhoe");

            entity.HasOne(d => d.Tt).WithMany(p => p.TaiXes)
                .HasForeignKey(d => d.TtId)
                .HasConstraintName("FK_TaiXe_TrangThaiTaiXe");

            entity.HasOne(d => d.Tx).WithOne(p => p.TaiXe)
                .HasForeignKey<TaiXe>(d => d.TxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Taixe_NguoiDung");
        });

        modelBuilder.Entity<TrangThaiDatXe>(entity =>
        {
            entity.HasKey(e => e.TtdxId).HasName("PK__TrangTha__C3606772340AFC2C");

            entity.ToTable("TrangThaiDatXe");

            entity.Property(e => e.TtdxId).HasColumnName("ttdx_id");
            entity.Property(e => e.TtdxTen)
                .HasMaxLength(32)
                .HasColumnName("ttdx_ten");
        });

        modelBuilder.Entity<TrangThaiTaiXe>(entity =>
        {
            entity.HasKey(e => e.TtId).HasName("PK__TrangTha__1B50D8865F670C66");

            entity.ToTable("TrangThaiTaiXe");

            entity.Property(e => e.TtId).HasColumnName("tt_id");
            entity.Property(e => e.TtTen)
                .HasMaxLength(255)
                .HasColumnName("tt_ten");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0728C3021A");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E4F37E358D").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__User__5C7E359EFE456197").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(128);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.XeId).HasName("PK__Xe__92A55F50D2247B67");

            entity.ToTable("Xe");

            entity.Property(e => e.XeId).HasColumnName("xe_id");
            entity.Property(e => e.LxId).HasColumnName("lx_id");
            entity.Property(e => e.TxId).HasColumnName("tx_id");
            entity.Property(e => e.XeBienso)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("xe_bienso");
            entity.Property(e => e.XeGiayDangky).HasColumnName("xe_giayDangky");

            entity.HasOne(d => d.Lx).WithMany(p => p.Xes)
                .HasForeignKey(d => d.LxId)
                .HasConstraintName("FK_Xe_LoaiXe");

            entity.HasOne(d => d.Tx).WithMany(p => p.Xes)
                .HasForeignKey(d => d.TxId)
                .HasConstraintName("FK_Xe_TaiXe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
