CREATE DATABASE TRANGTRAINUOIHEO

GO
USE TRANGTRAINUOIHEO

--use master
--drop database TRANGTRAINUOIHEO 

GO
CREATE TABLE HEO
(
	MaHeo char(16),
	MaLoaiHeo char(16),
	MaGiongHeo char(16),
	GioiTinh nvarchar(6),
	NgaySinh date,
	TrongLuong int,
	MaChuong char(16),
	MaHeoCha char(16),
	MaHeoMe char(16),
	NguonGoc nvarchar(255),
	TinhTrang nvarchar(512),
	CONSTRAINT PK_HEO PRIMARY KEY (MaHeo)
)

GO
CREATE TABLE LOAIHEO
(
	MaLoaiHeo char(16),
	TenLoaiHeo nvarchar(255),
	MoTa ntext,
	CONSTRAINT PK_LH PRIMARY KEY (MaLoaiHeo)
)

GO
CREATE TABLE GIONGHEO
(
	MaGiongHeo char(16),
	TenGiongHeo nvarchar(255),
	MoTa ntext,
	CONSTRAINT PK_GH PRIMARY KEY (MaGiongHeo)
)

GO
CREATE TABLE CHUONGTRAI
(
	MaChuong char(16),
	MaLoaiChuong char(16),
	TinhTrang nvarchar(512),
	SuaChuaToiDa int,
	SoLuongHeo int,
	CONSTRAINT PK_CT PRIMARY KEY (MaChuong)
)

GO
CREATE TABLE LOAICHUONG
(
	MaLoaiChuong char(16),
	TenLoai nvarchar(255),
	MoTa ntext,
	CONSTRAINT PK_LCT PRIMARY KEY (MaLoaiChuong)
)


CREATE TABLE LICHCHUONG
(
	MaChuong char(16),
	NgayLam smalldatetime,
	TrangThai nvarchar(50),
	TenLich nvarchar(50),
	Mota nvarchar(50),
	MaNguoiTao char(16),
	MaNguoiLam char(16) NULL,

	CONSTRAINT PK_LC PRIMARY KEY (MaChuong, NgayLam, TenLich)
)


GO
CREATE TABLE LICHTIEMHEO
(
	MaLichTiem char(16),
	MaHeo char(16),
	MaThuoc char(16),
	NgayTiem smalldatetime ,
	LieuLuong int,
	TrangThai nvarchar(64),
	CONSTRAINT PK_LTH PRIMARY KEY (MaLichTiem)
)

GO
CREATE TABLE LICHPHOIGIONG
(
	MaLichPhoi char(16),
	MaHeoDuc char(16),
	MaHeoCai char(16),
	NgayPhoiGiong smalldatetime,
	Trangthai nvarchar(64),
		
	NgayDuKienDe smalldatetime ,
	NgayDeThucTe smalldatetime ,

	SoCon int,
	SoConChet int,
	NgayCaiSua smalldatetime,

	SoConChon int,
	NgayPhoiGiongLaiDuKien smalldatetime,
	CONSTRAINT PK_LPG PRIMARY KEY (MaLichPhoi)
)

Go 
create table HANGHOA 
(
	MaHangHoa char(16),
	TenHangHoa nvarchar(255),
	DonGia int,
	SoLuongTonKho int,
	TinhTrang nvarchar(512),
	LoaiHangHoa nvarchar(64),

	CONSTRAINT PK_HH PRIMARY KEY (MaHangHoa)
)

Go 
create table NHANVIEN 
(
	MaNhanVien char(16),
	HoTen nvarchar(255),
	BytesImage varbinary(max),
	MaChucVu char(16),
	GioiTinh nvarchar(16),
	NgaySinh Date,
	DiaChi nvarchar(1024),
	email nvarchar(1024),
	SDT char(16),
	NgayVaoLam Date,
	HeSoLuong float,
	TrangThai	nvarchar(50),
	_Username char(64),
	_PassWord char(64),

	CONSTRAINT PK_NV PRIMARY KEY (MaNhanVien)
)

go 
Create table ThongBao
(
	MaThongBao char(16),
	_MaNguoiGui char(16),
	_MaNguoiNhan char(16),
	TinhTrang nvarchar(16),
	TieuDe ntext,
	NoiDung ntext,
	ThoiGian smalldatetime,
	CONSTRAINT PK_TB PRIMARY KEY (MaThongBao)
)

go 
create table CHUCVU
(
	MaChucVu char(16),
	TenChucVu nvarchar(255),
	LuongCoBan int,
	ID_Permision char(16),
	MoTa ntext,

	CONSTRAINT PK_CV PRIMARY KEY (MaChucVu)
)


go
create table PERMISION
(
	ID_Permision char(16),
	Name_Permision nvarchar(255),
	Permision_Descript nvarchar(2048),

	constraint PK_Permision PRIMARY KEY (ID_Permision)
)


go
create table PERMISION_DETAIL
(
	ID_PermisionDetail char(16),
	ActionDetail nvarchar(255),
	PermisionDetail_Descript nvarchar(2048),
	ID_Permision char(16),
	constraint PK_PermisionDetail PRIMARY KEY (ID_PermisionDetail)
)


Go 
Create table DOITAC
(
	MaDoiTac char(16),
	LoaiDoiTac nvarchar(64),
	TenDoiTac nVarchar(255),
	DiaChi nvarchar(1024),
	SDT char(15),
	Email nvarchar(2048),

	constraint PK_DT PRIMARY KEY (MaDoiTac)
)

Go 
Create table PHIEUSUACHUA
(
	SoPhieu char(16),
	NgaySuaChua smalldatetime,
	MaNhanVien char(16),
	GhiChu ntext,
	TrangThai nvarchar(64),
	MaDoiTac char(16),
	TongTien int,	
	constraint PK_PSC PRIMARY KEY (SoPhieu)
)

go
create table CT_PHIEUSUACHUA
(
	SoPhieu char(16),
	MaChuong char(16),
	MoTa ntext,
	ThanhTien int,
	constraint PK_CT_PSC PRIMARY KEY (SoPhieu, MaChuong)
)

go
Create table PHIEUHEO
(
	SoPhieu char(16),
	NgayLap smalldatetime,
	MaNhanVien char(16),
	MaDoiTac char(16),
	TrangThai nvarchar(64),
	LoaiPhieu nvarchar(64),
	TongTien int,

	constraint PK_PH primary key (SoPhieu)
)

Go 
Create table CT_PHIEUHEO
(
	SoPhieu char(16),
	MaHeo char(16),
	TrongLuong int,
	DonGia int,
	constraint PK_CT_PH primary key (SoPhieu, MaHeo)
)


go
Create table PHIEUKIEMKHO
(
	SoPhieu char(16),
	NgayLap smalldatetime,
	MaNhanVien char(16),
	GhiChu ntext,
	KetQua nvarchar(255),

	constraint PK_PKK primary key (SoPhieu)
)

Go 
Create table CT_PHIEUKIEMKHO
(
	SoPhieu char(16),
	MaHangHoa char(16),
	SoLuongHienCo int,
	SoLuongKiemTra int,
	constraint PK_CT_PKK primary key (SoPhieu, MaHangHoa)
)

go
Create table	PHIEUHANGHOA
(
	SoPhieu char(16),
	NgayLap smalldatetime,
	MaNhanVien char(16),
	MaNhanVienNhan char(16),
	MaDoiTac char(16),
	TrangThai nvarchar(64),
	LoaiPhieu nvarchar(64),
	TongTien int,

	constraint PK_PHH primary key (SoPhieu)
)

Go 
Create table CT_PHIEUHANGHOA
(
	SoPhieu char(16),
	MaHangHoa char(16),
	SoLuong int,
	DonGia int,
	constraint PK_CT_PHH primary key (SoPhieu, MaHangHoa)
)

GO 
CREATE TABLE THAMSO
(
	id INT IDENTITY primary key,
	XuatChuongMin	int,
	XuatChuongMax	int,
	MonthXuatChuongMin	int,
	MonthXuatChuongMax int,
	TuoiNhapDan		int,
	CanHuyet	int,
	TuoiPhoiGiongDuc int,
	TuoiPhoiGiongCai int,
	RePhoiGiongDuc int,
	RePhoiGiongCai int,
	ThucAnMotNgay int,
	ThucAnMax int,
	SoNgayMangThai int,
)

Go 
Create Table QuyDinhTiemHeo
(
	MaTiemHeo char(16),
	MaVaxin char(16),
	TuoiTiem int,
	MoTa ntext,
	constraint PK_QDTH_MTH primary key (MaTiemHeo)

)
Go 	
Create table MuaDichBenh
(
	MaDichBenh char(16),
	TenDichBenh nchar(200),
	NgayBatDau smalldatetime,
	NgayKetThuc smalldatetime,
	NguyenNhan ntext,
	BienPhap ntext,
		constraint PK_MDB_MDB primary key (MaDichBenh)


)


go 
create table ListActionDetail
(
	id INT IDENTITY primary key,
	ActionDetail nvarchar(64)
)

Create table	BAOCAOTONKHO
(
	MaBCTK char(16),
	Thang int,
	Nam int,
	MaHH char(16),
	TonDau int,
	TonCuoi int,
	SoLuongNhapThem int,
	SoLuongXuatRa int,

	constraint PK_BCTK primary key (MaBCTK)
)

GO
--table Heo--
ALTER TABLE HEO ADD CONSTRAINT FK_H_MLH
FOREIGN KEY (MaLoaiHeo) REFERENCES LOAIHEO(MaLoaiHeo)

ALTER TABLE HEO ADD CONSTRAINT FK_H_MGH
FOREIGN KEY (MaGiongHeo) REFERENCES GIONGHEO(MaGiongHeo)

ALTER TABLE HEO ADD CONSTRAINT FK_H_CT
FOREIGN KEY (MaChuong) REFERENCES CHUONGTRAI(MaChuong)

--table ChuongTrai--
ALTER TABLE CHUONGTRAI ADD CONSTRAINT FK_CT_MC
FOREIGN KEY (MaLoaiChuong) REFERENCES LOAICHUONG(MaLoaiChuong)

-- table LichChuong--
ALTER TABLE LICHCHUONG ADD CONSTRAINT FK_LC_MC 
FOREIGN KEY (MaChuong) REFERENCES CHUONGTRAI(MaChuong)

ALTER TABLE LICHCHUONG ADD CONSTRAINT FK_LC_MNT 
FOREIGN KEY (MaNguoiTao) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE LICHCHUONG ADD CONSTRAINT FK_LC_MNL 
FOREIGN KEY (MaNguoiLam) REFERENCES NHANVIEN(MaNhanVien)

--table LICHTIEMHEO--
ALTER TABLE LICHTIEMHEO ADD CONSTRAINT FK_LTH_MH
FOREIGN KEY (MaHeo) REFERENCES HEO(MaHeo)

--table LICHPHOIGIONG--
ALTER TABLE LICHPHOIGIONG ADD CONSTRAINT FK_LPG_MHD
FOREIGN KEY (MaHeoDuc) REFERENCES HEO(MaHeo)

ALTER TABLE LICHPHOIGIONG ADD CONSTRAINT FK_LPG_MHC
FOREIGN KEY (MaHeoCai) REFERENCES HEO(MaHeo)


--table NHANVIEN--
ALTER TABLE NHANVIEN ADD CONSTRAINT FK_NV_MCV
FOREIGN KEY (MaChucVu) REFERENCES CHUCVU(MaChucVu)

--table THONGBAO--
ALTER TABLE THONGBAO ADD CONSTRAINT FK_TB_NN
FOREIGN KEY (_MaNguoiNhan) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE THONGBAO ADD CONSTRAINT FK_TB_NG
FOREIGN KEY (_MaNguoiGui) REFERENCES NHANVIEN(MaNhanVien)

--table CHUCVU--
ALTER TABLE CHUCVU ADD CONSTRAINT FK_CV_P
FOREIGN KEY (ID_Permision) REFERENCES PERMISION(ID_Permision)

--table PERMISION_DETAIL--
ALTER TABLE PERMISION_DETAIL ADD CONSTRAINT FK_PD_P
FOREIGN KEY (ID_Permision) REFERENCES PERMISION(ID_Permision)

--table PHIEUSUACHUA--
ALTER TABLE PHIEUSUACHUA ADD CONSTRAINT FK_PSC_MNV
FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE PHIEUSUACHUA ADD CONSTRAINT FK_PSC_MDT
FOREIGN KEY (MaDoiTac) REFERENCES DOITAC(MaDoiTac)

ALTER TABLE CT_PHIEUSUACHUA ADD CONSTRAINT FK_CT_PSC_SP
FOREIGN KEY (SoPhieu) REFERENCES PHIEUSUACHUA(SoPhieu)

ALTER TABLE CT_PHIEUSUACHUA ADD CONSTRAINT FK_CT_PSC_MC
FOREIGN KEY (MaChuong) REFERENCES CHUONGTRAI(MaChuong)

--table PHIEUHEO--
ALTER TABLE PHIEUHEO ADD CONSTRAINT FK_PH_MNV
FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE PHIEUHEO ADD CONSTRAINT FK_PH_MDT
FOREIGN KEY (MaDoiTac) REFERENCES DOITAC(MaDoiTac)

ALTER TABLE CT_PHIEUHEO ADD CONSTRAINT FK_CT_PH_SP
FOREIGN KEY (SoPhieu) REFERENCES PHIEUHEO(SoPhieu)

ALTER TABLE CT_PHIEUHEO ADD CONSTRAINT FK_CT_PH_MH
FOREIGN KEY (MaHeo) REFERENCES HEO(MaHeo)

--table PHIEUHANGHOA--
ALTER TABLE PHIEUHANGHOA ADD CONSTRAINT FK_PHH_MNV
FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE PHIEUHANGHOA ADD CONSTRAINT FK_PHH_MNVN
FOREIGN KEY (MaNhanVienNhan) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE PHIEUHANGHOA ADD CONSTRAINT FK_PHH_MDT
FOREIGN KEY (MaDoiTac) REFERENCES DOITAC(MaDoiTac)

ALTER TABLE CT_PHIEUHANGHOA ADD CONSTRAINT FK_CT_PHH_SP
FOREIGN KEY (SoPhieu) REFERENCES PHIEUHANGHOA(SoPhieu)

ALTER TABLE CT_PHIEUHANGHOA ADD CONSTRAINT FK_CT_PHH_MHH
FOREIGN KEY (MaHangHoa) REFERENCES HANGHOA(MaHangHoa)

--table PHIEUKIEMKHO--
ALTER TABLE PHIEUKIEMKHO ADD CONSTRAINT FK_PKK_MNV
FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)

ALTER TABLE CT_PHIEUKIEMKHO ADD CONSTRAINT FK_CT_PKK_SP
FOREIGN KEY (SoPhieu) REFERENCES PHIEUKIEMKHO(SoPhieu)

ALTER TABLE CT_PHIEUKIEMKHO ADD CONSTRAINT FK_CT_PKK_MHH
FOREIGN KEY (MaHangHoa) REFERENCES HANGHOA(MaHangHoa)

--table BAOCAOTONKHO
ALTER TABLE BAOCAOTONKHO ADD CONSTRAINT FK_BCTK_HH
FOREIGN KEY (MaHH) REFERENCES HANGHOA(MaHangHoa)

--table  QuyDinhTiemHeo
alter table QuyDinhTiemHeo add constraint FK_QDTH_HH

FOREIGN KEY (MaVaxin) REFERENCES HANGHOA(MaHangHoa)
go


INSERT INTO ListActionDetail VALUES (N'Quản lý nhân viên');
INSERT INTO ListActionDetail VALUES (N'Quản lý đàn heo');
INSERT INTO ListActionDetail VALUES (N'Quản lý chuồng nuôi');
INSERT INTO ListActionDetail VALUES (N'Quản lý kho');
INSERT INTO ListActionDetail VALUES (N'Quản lý tài chính');
INSERT INTO ListActionDetail VALUES (N'Quản lý cây mục tiêu');
INSERT INTO ListActionDetail VALUES (N'Quản lý nhật ký');

INSERT INTO THAMSO  VALUES (0,120,0,120,0,3,120,120,2,2,20,25,98);


