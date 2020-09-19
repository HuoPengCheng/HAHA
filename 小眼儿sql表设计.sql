create  database Smaileeye
go
use  Smaileeye
--角色表
create table  Roletb 
(
 RId int primary key identity,
 RName varchar(50),
)
insert into Roletb values('超级管理员'),
('管理员'),
('维修员'),
('客户')
select * from Roletb
--drop table Roletb
--用户信息详情表
create table UserInfotb
(
 UId  int primary key identity,
 UName varchar(50),
 UAccount varchar(50),
 UPwd varchar(50),
 UPhone varchar(50),
 RId int foreign key references Roletb (RId),
 UState int
)
insert into UserInfotb values('霍鹏程','111111111','123','15148869632',1,1)
insert into UserInfotb values('袁少峰','222222222','123','13295298257',2,1)
insert into UserInfotb values('郑明辰','333333333','123','10086112233',2,1)
insert into UserInfotb values('张鑫鑫','444444444','123','10086112234',2,1)
insert into UserInfotb values('张家旺','555555555','123','10086112235',2,1)
insert into UserInfotb values('李达凯','666666666','123','10086112236',2,1)
insert into UserInfotb values('王国玺','777777777','123','10086112237',3,1)
insert into UserInfotb values('陈帅兵','888888888','123','10086112238',3,1)
insert into UserInfotb values('郭占标','999999999','123','10086112239',3,1)
insert into UserInfotb values('冯路顺','121212121','123','10086112231',4,1)
insert into UserInfotb values('张三','123456789','123','10086112232',4,1)
insert into UserInfotb values('李四','131313131','123','10086112243',4,1)
select * from Roletb as a join UserInfotb as b on a.RId=b.RId where UState=1
--drop table UserInfotb
--权限管理表
create table Juristb
(
JId int primary key identity,
RId int foreign key references Roletb (RId),
JAdd int,
JDel int,
JShow int,
JUpt int
)
--drop table Juristb
--维修订单详情表
create table UserRepairsDetailstb 
(
 UrdId int primary key identity,
Ordernumber VARCHAR(90),
Type int,
Marque varchar(50),
Cause varchar(50),
Reason varchar(50),
Addre1ss varchar(50),
DetailedAddress varchar(50),
Date varchar(50),
UId int,
State int
)
--drop table UserRepairsDetailstb
--维修商品类别表
create table CommodityDetailstb
(
CId int primary key identity,
CName varchar(50),
CPId int
)
--drop table CommodityDetailstb
--维修员维修详情表
create table MaintenanceDetailstb
(
MId int primary key identity,
URDId int,
UId int,
)
--drop table MaintenanceDetailstb
--投诉信息表
create table Complaintb
(
CoId int primary key identity,
Ordernumber varchar(50),
UId1 int, 
UId2 int,
Comment VARCHAR(50),
Img VARCHAR(MAX),
Static int
)
--材料信息表
create table Materialstb
(
MAId  int primary key identity,    
MaterialName varchar(50),
MSpecification varchar(50),
MCategory varchar(50),
MAmount int,
MImg    varchar(max)
)
--工具信息表
create table Tooltb
(
TId  int primary key identity,    
ToolName varchar(50),
TSpecification varchar(50),
TCategory varchar(50),
Img    varchar(max)
)
--材料申请表
create table ApplyFortb
(
AId int primary key identity,    
MaterialName varchar(50),
UId          int,   
MaterialAmount int,
)
--采购信息表
create table Purchasetb
(
PId	int primary key identity,  
MAterialName VARCHAR(50),
Category varchar(50),
PAmount  int
)
--维修员使用材料明细表
create table UserSubsidiarytb
(
USId  int primary key identity,  
UId          int,   
TId          int,
DrawTime     datetime
)
--实名认证表
create table RealNametb
(
RNId       int primary key identity, 
UId			int,
Prcture1    varchar(max),
Prcture2	varchar(max),
)
--问卷评分表
create  table Questiontb
(
QId			int primary key identity, 
Question1	varchar(50),
Question2	varchar(50),
Question3	varchar(50),
Question4	varchar(50),
QNumber		int
)

