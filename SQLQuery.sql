drop database CarHub
GO
create database CarHub
go
use CarHub
go
go
create table [dbo].[User](
Id int not null identity(1,1) primary key,
Username varchar(64) not null unique,
Phone varchar(12) unique not null,
Password nvarchar(128) not null,
FullName nvarchar(128) not null,
)
go
create table [dbo].[TrangThaiTaiXe](
tt_id int not null identity(1,1) primary key,
tt_ten nvarchar(255) not null,
)
go
create table [dbo].[TaiXe](
tx_id int not null primary key,
tx_giayPhepLaiXe varbinary(MAX),
tx_KhamSucKhoe varbinary(MAX),
tx_cccd varbinary(max),
tt_id int,
tx_GPS_lon decimal(12,9),
tx_GPS_lat decimal(12,9),
constraint FK_TaiXe_TrangThaiTaiXe foreign key (tt_id) references TrangThaiTaiXe(tt_id),
constraint FK_Taixe_NguoiDung foreign key (tx_id) references [dbo].[User](id)
)
go
create table [LoaiXe](
lx_id int not null identity(1,1) primary key,
lx_ten nvarchar(32),
)
go
create table [Xe](
xe_id int not null identity(1,1) primary key,
tx_id int,
lx_id int,
xe_bienso varchar(32) not null,
xe_giayDangky varbinary(max) not null,
constraint FK_Xe_TaiXe foreign key (tx_id) references TaiXe(tx_id),
constraint FK_Xe_LoaiXe foreign key (lx_id) references loaixe(lx_id),
)
go
create table [KhachHang](
kh_id int not null primary key,
kh_gps_lon decimal(12,9),
kh_gps_lat decimal(12,9),
Constraint FK_KhachHang_NguoiDung foreign key (kh_id) references [dbo].[User](id)
)
go
create table [TrangThaiDatXe](
ttdx_id int not null identity(1,1) primary key,
ttdx_ten nvarchar(32)
)
go
create table [DatXe](
dx_id int not null identity(1,1) primary key ,
dx_ngayGio datetime not null,
dx_diadiemdon nvarchar(128) not null,
dx_GPS_lon decimal(12,9) not null,
dx_GPS_lat decimal(12,9) not null,
kh_ten nvarchar(128) not null,
kh_phone nvarchar(12) not null,
kh_id int,
tx_id int,
xe_id int,
lx_id int,
ttdx_id int,
constraint FK_DatXe_khachHang foreign key (kh_id) references KhachHang(kh_id),
constraint Fk_DatXe_TaiXe foreign key (tx_id) references TaiXe(tx_id),
constraint FK_DATXE_XE FOREIGN KEY (XE_ID) REFERENCES XE(XE_ID),
CONSTRAINT FK_DATXE_LOAIXE FOREIGN KEY (LX_ID) REFERENCES LOAIXE(LX_ID),
CONSTRAINT FK_DATXE_TrangThaiDatXe FOREIGN KEY (ttdx_id) REFERENCES TrangThaiDatXe(ttdx_id),
)
GO
CREATE TABLE [HubConnection](
CONNECTIONID NVARCHAR(64) PRIMARY KEY  not null,
ID INT not null,
Username nvarchar(64) not null,
CONSTRAINT FK_HubConnection_User FOREIGN KEY (ID) REFERENCES [user](ID),
)
--alter table HubConnection
--drop constraint FK_HUBCONNECTION_Nguoidung
GO
CREATE TABLE [NOTIFICATION](
NOTIFICATION_ID INT IDENTITY(1,1) PRIMARY KEY,
ID INT not null,
Username VARCHAR(64) not null,
MessageType VARCHAR(64) not null,
MESSAGE NVARCHAR(MAX) not null,
DATETIME DATETIME default getdate() not null,
CONSTRAINT FK_NOTIFICATION_User FOREIGN KEY (ID) REFERENCES [user](ID),

)
--alter table NOTIFICATION
--drop constraint FK_NOTIFICATION_NguoiDung
GO
ALTER DATABASE Carhub SET ENABLE_BROKER with rollback immediate;
ALTER AUTHORIZATION ON DATABASE::Carhub TO sa;
