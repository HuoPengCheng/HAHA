create  database Smaileeye
go
use  Smaileeye
--��ɫ��
create table  Roletb 
(
 RId int primary key identity,
 RName varchar(50),
)
insert into Roletb values('��������Ա'),
('����Ա'),
('ά��Ա'),
('�ͻ�')
select * from Roletb
--drop table Roletb
--�û���Ϣ�����
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
insert into UserInfotb values('������','111111111','123','15148869632',1,1)
insert into UserInfotb values('Ԭ�ٷ�','222222222','123','13295298257',2,1)
insert into UserInfotb values('֣����','333333333','123','10086112233',2,1)
insert into UserInfotb values('������','444444444','123','10086112234',2,1)
insert into UserInfotb values('�ż���','555555555','123','10086112235',2,1)
insert into UserInfotb values('��￭','666666666','123','10086112236',2,1)
insert into UserInfotb values('������','777777777','123','10086112237',3,1)
insert into UserInfotb values('��˧��','888888888','123','10086112238',3,1)
insert into UserInfotb values('��ռ��','999999999','123','10086112239',3,1)
insert into UserInfotb values('��·˳','121212121','123','10086112231',4,1)
insert into UserInfotb values('����','123456789','123','10086112232',4,1)
insert into UserInfotb values('����','131313131','123','10086112243',4,1)
select * from Roletb as a join UserInfotb as b on a.RId=b.RId where UState=1
--drop table UserInfotb
--Ȩ�޹����
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
--ά�޶��������
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
--ά����Ʒ����
create table CommodityDetailstb
(
CId int primary key identity,
CName varchar(50),
CPId int
)
--drop table CommodityDetailstb
--ά��Աά�������
create table MaintenanceDetailstb
(
MId int primary key identity,
URDId int,
UId int,
)
--drop table MaintenanceDetailstb
--Ͷ����Ϣ��
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
--������Ϣ��
create table Materialstb
(
MAId  int primary key identity,    
MaterialName varchar(50),
MSpecification varchar(50),
MCategory varchar(50),
MAmount int,
MImg    varchar(max)
)
--������Ϣ��
create table Tooltb
(
TId  int primary key identity,    
ToolName varchar(50),
TSpecification varchar(50),
TCategory varchar(50),
Img    varchar(max)
)
--���������
create table ApplyFortb
(
AId int primary key identity,    
MaterialName varchar(50),
UId          int,   
MaterialAmount int,
)
--�ɹ���Ϣ��
create table Purchasetb
(
PId	int primary key identity,  
MAterialName VARCHAR(50),
Category varchar(50),
PAmount  int
)
--ά��Աʹ�ò�����ϸ��
create table UserSubsidiarytb
(
USId  int primary key identity,  
UId          int,   
TId          int,
DrawTime     datetime
)
--ʵ����֤��
create table RealNametb
(
RNId       int primary key identity, 
UId			int,
Prcture1    varchar(max),
Prcture2	varchar(max),
)
--�ʾ����ֱ�
create  table Questiontb
(
QId			int primary key identity, 
Question1	varchar(50),
Question2	varchar(50),
Question3	varchar(50),
Question4	varchar(50),
QNumber		int
)

