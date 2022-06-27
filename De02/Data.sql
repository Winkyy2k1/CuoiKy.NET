use master
go
if DB_ID('NhanvienDB') is not null
	drop database NhanvienDB
go
create database NhanvienDB
go
use NhanvienDB
go
create table PhongBan(
	MaPhong int primary key identity,
	TenPhong nvarchar(25)
)
go
insert into PhongBan values('Hanh chinh')
insert into PhongBan values('Ke toan')
insert into PhongBan values('Tong hop')
go
create table Nhanvien(
	MaNV int primary key,
	Hoten nvarchar(30), 
	Songaycong int,
	Luong int,
	MaPhong int foreign key references PhongBan(MaPhong)
)
go

insert into Nhanvien values(1,'Mai Anh',20,5000,1)
insert into Nhanvien values(2,'Thu Huong',27,4000,2)
insert into Nhanvien values(3,'Van Hai',30,6000,1)
insert into Nhanvien values(4,'Hong Linh',25,3000,3)

select * from Nhanvien 
 