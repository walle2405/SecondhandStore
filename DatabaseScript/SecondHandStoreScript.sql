if exists (select * from sysdatabases where name='SecondhandStore')
begin
  raiserror('Dropping existing SecondhandStore database ....',0,1)
  DROP database SecondhandStore
end
GO

raiserror('Creating SecondhandStore database....',0,1)
SET NOCOUNT ON
CREATE DATABASE [SecondhandStore]
GO
USE [SecondhandStore]
GO

CREATE TABLE [dbo].Role (
	[roleId] [nvarchar](2) PRIMARY KEY NOT NULL,
	[roleName] [nvarchar](50) NOT NULL,
)
GO
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES 
					('AD', 'Admin'),
					('US', 'User'),
					('DE', 'Deactivated User');
CREATE TABLE [dbo].Account(
	[accountId] [int] IDENTITY (1,1) PRIMARY KEY NOT NULL,
	[password] [varchar](64) NOT NULL,
	[fullname] [nvarchar](50) NOT NULL,
	[dob] [date] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[roleId] [nvarchar](2) REFERENCES Role(roleId) NOT NULL ,
	[address] [nvarchar](255) NOT NULL,
	[phoneNo] [varchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
	[credibilityPoint] [int] NOT NULL,
	[pointBalance] [int] NOT NULL,
	[createdDate] [date] NOT NULL
)
GO

CREATE TABLE [dbo].Category (
	[categoryId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[categoryName] [nvarchar](50) NOT NULL,
	[categoryValue] [int] NOT NULL
)
GO

CREATE TABLE [dbo].Status(
	[statusId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[statusName] [nvarchar](255) NOT NULL
)
GO

CREATE TABLE [dbo].Post(
	[postId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[accountId][int] REFERENCES Account(accountId) NOT NULL,
	[productName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[categoryId] [int] REFERENCES Category(CategoryId) NOT NULL,
	[price][float] NOT NULL,
	[postStatusId] [int] REFERENCES Status(StatusId) NOT NULL,
	[isDonated] [bit] NOT NULL,
	[createdDate] [date] NOT NULL,
)
GO

CREATE TABLE [dbo].Image(
	[imageId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[postId] [int] REFERENCES Post(postId) NOT NULL,
	[imageUrl][nvarchar](4000) NOT NULL
)

CREATE TABLE [dbo].TopUp (
	[orderId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[topUpPoint] [int] NOT NULL,
	[accountId] [int] REFERENCES Account(accountId) NOT NULL,
	[topUpDate] [date] NOT NULL,
	[price] [float] NOT NULL,
	[topupStatusId][int] REFERENCES Status(StatusId) NOT NULL
)
GO

CREATE TABLE [dbo].ExchangeOrder (
	[orderId][int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[postId][int] REFERENCES Post(postId) NOT NULL,
	[sellerId][int] REFERENCES Account(accountId) NOT NULL,
	[buyerId][int] REFERENCES Account(accountId) NOT NULL,
	[orderDate][date] NOT NULL,
	[orderStatusId] [int] REFERENCES Status(StatusId) NOT NULL,
)
GO

CREATE TABLE [dbo].Report(
	[reportId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[reason] [nvarchar](4000) NOT NULL,
	[reporterId] [int] REFERENCES Account(accountId) NOT NULL,
	[reportedAccountId] [int] REFERENCES Account(accountId) NOT NULL,
	[reportDate] [date] NOT NULL,
	[reportStatusId] [int] REFERENCES Status(statusId) NOT NULL
)
GO

CREATE TABLE [dbo].Review(
	[reviewId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[reviewerId] [int] REFERENCES Account(accountId) NOT NULL,
	[reviewedId] [int] REFERENCES Account(accountId) NOT NULL,
	[ratingStar] [int],
	[description] [nvarchar](4000),
	[createdDate] [date] NOT NULL,
	CHECK(ratingStar >= 1 and ratingStar <= 5)
);

CREATE TABLE [dbo].ReportImage(
	[imageId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[reportId] [int] REFERENCES Report(reportId) NOT NULL,
	[imageUrl][nvarchar](4000) NOT NULL
)

insert into Account ( roleId, password, fullname, dob, email, address, phoneNo, isActive, credibilityPoint, pointBalance, createdDate) 
values 
( 'AD', '12345', 'Adminstrator', '2003-01-01', 'fptoseservice@fpt.edu.vn', 'FPT University', '0123456789', 1, 50, 0, '2023-01-01'),
( 'US', '12345', 'NGUYEN TRUNG TIN', '2003-01-01', 'tinntse171390@fpt.edu.vn', 'VINHOME S303','0123456789', 1, 50, 1300, '2023-01-01'),
( 'US', '67890', 'NGUYEN MINH KHANG', '2003-01-01', 'khangnmse171557@fpt.edu.vn', 'VINHOME S303','0987654321', 1, 50, 1300, '2023-01-01'),
( 'US', '12345', 'BUI HOANG HAI', '2003-01-01', 'haibhse171489@fpt.edu.vn', 'VINHOME S205','0987654321', 1, 50, 1300, '2023-01-01'),
( 'US', '12345', 'PHAM QUANG THAI', '2003-01-01', 'thaipq@fpt.edu.vn', 'THU DUC CITY','0912356821', 1, 50, 1300, '2023-01-01'),
( 'US', '12345', 'DAO NGO CHI BAO', '2003-01-01', 'baodnc@fpt.edu.vn', 'MAN THIEN','0987623121', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc888de', 'JANE SMITH', '2003-01-01', 'janesmith@fpt.edu.vn', '456 Elm St, Town', '0905555678', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc999cf', 'DAVID BROWN', '2003-01-01', 'davidbrown@fpt.edu.vn', '789 Oak St, Village', '0905559190', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc101', 'SARAH LEE', '2003-01-01', 'sarahlee@fpt.edu.vn', '321 Pine St, County', '0905552345', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc11ee', 'MIA PINK', '2003-01-01', 'miapink@fpt.edu.vn', '873 Sumer, Town', '0905557878', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc12dd', 'ALEX KIM', '2003-01-01', 'alexkim@fpt.edu.vn', '252 Maple St, Town', '0905553233', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc13dd', 'PHAM THANH TOAN', '2003-01-01', 'toanpt@fpt.edu.vn', '7 District', '0901593570', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc14dd', 'TONY TOM', '2003-01-01', 'tonytom@fpt.edu.vn', '445 Pine St, Country', '0909555333', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc15dd', 'DANG KIM THANH', '2003-01-01', 'thanhdk@fpt.edu.vn', '1 District', '0909999666', 1, 50, 1300, '2023-01-01'),
( 'US', 'abc16ff', 'EVE GRACE', '2003-01-01', 'evegrace@fpt.edu.vn', '55 Summer, Town', '090666999', 1, 50, 1300, '2023-01-01');

INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('Clothes', 20);
INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('Accessories', 10);
INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('Electronics', 70);
INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('Books', 10);
INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('Musical Instruments', 30);
INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('School Supplies', 5);
INSERT [dbo].[Category] ([categoryName], [categoryValue]) VALUES ('Others', 10);

INSERT [dbo].[Status] ([statusName]) VALUES 
('Active'), 
('Inactive'),
('Pending'), 
('Accepted'),
('Rejected'),
('Processing'), 
('Cancelled'), 
('Completed');

insert into Post (accountId, productName, price, description, categoryId, isDonated, createdDate, postStatusId)
Values
(2, N'Manchester United Jersey', 100000, N'DASDSDSADADAD', 1, 0, '2023-02-19', 4),
(4, N'Do Vo Vovinam', 100000, N'Pass lai gia re o FPT XAVALO', 1, 0, '2023-02-21', 4),
(5, N'CASIO FX-580', 150000, N'DOI MAY 580 SANG VINACAL', 3, 0, '2023-03-12', 4),
(3, N'Dan tranh', 300000, N'Like new', 5, 0, '2023-03-27', 4),
(2, N'Sach tieng Nhat Dekiru', 200000, N'pass lai sau khi hoc xong Nhat 2', 4, 0, '2023-03-30', 4),
(7, 'Mat kinh', 150000, N'can pass lai', 2, 0, '2023-04-28', 4),
(7, 'Dac Nhan Tam', 200000, 'dacnhantam', 4, 0, '2023-06-21', 4),
(9, 'LOGITECH GPRO', 800000,  'mouse gpro', 3, 0, '2023-07-10', 4),
(9, 'DareUEk87', 459000, 'ban phim dareu', 3, 0, '2023-07-11', 4),
(11, 'Pencilcase', 170000, 'hop but', 3, 0, '2023-07-12', 4),
(11, 'Notebook', 170000 , 'so tay', 4, 0, '2023-07-12', 4),
(13, 'Sao truc', 200000, 'sao truc', 5, 0, '2023-07-12', 4),
(10, 'Dan ty ba', 1500000, 'dan ty ba', 5, 0, '2023-07-13', 4),
(15, 'Kaki pants', 350000,  'quan kaki',  1, 0, '2023-07-13', 4),
(5, 'Jordan 1', 300000,  'quan kaki',  1, 0, '2023-07-14', 4);

insert into Image(postId, imageUrl)
Values
(1, 'https://imagesswp391.blob.core.windows.net/post/ee6f28e3-b871-4f68-ac9f-891c5e098177kinh-mat-hinh-da-giac-gong-nho-spe-0008-mau-bac-main__62611__1643894339.jpg'),
(2, 'https://imagesswp391.blob.core.windows.net/post/ee6f28e3-b871-4f68-ac9f-891c5e098177kinh-mat-hinh-da-giac-gong-nho-spe-0008-mau-bac-main__62611__1643894339.jpg'),
(3, 'https://imagesswp391.blob.core.windows.net/post/ee6f28e3-b871-4f68-ac9f-891c5e098177kinh-mat-hinh-da-giac-gong-nho-spe-0008-mau-bac-main__62611__1643894339.jpg'),
(4, 'https://imagesswp391.blob.core.windows.net/post/ee6f28e3-b871-4f68-ac9f-891c5e098177kinh-mat-hinh-da-giac-gong-nho-spe-0008-mau-bac-main__62611__1643894339.jpg'),
(5, 'https://imagesswp391.blob.core.windows.net/post/ee6f28e3-b871-4f68-ac9f-891c5e098177kinh-mat-hinh-da-giac-gong-nho-spe-0008-mau-bac-main__62611__1643894339.jpg'),
(6, 'https://imagesswp391.blob.core.windows.net/post/ee6f28e3-b871-4f68-ac9f-891c5e098177kinh-mat-hinh-da-giac-gong-nho-spe-0008-mau-bac-main__62611__1643894339.jpg'),
(7, 'https://imagesswp391.blob.core.windows.net/post/ff1d181c-e85c-42fa-9a5e-1757d08746334b8ca7d8687072ed6dc73e6c4dd8813b.jpg'),
(8, 'https://imagesswp391.blob.core.windows.net/post/1c097365-02ce-47da-9fd7-b086fca8913215900362337536.jpg'),
(9, 'https://imagesswp391.blob.core.windows.net/post/fdd7f1ab-de82-4597-b66d-06deff041178keyboard-co-dareu-ek87-blue-sw-usb-chinh-hang-9776.jpg'),
(10, 'https://imagesswp391.blob.core.windows.net/post/2251cf86-5da7-4ad2-8844-e244eddc9388pencil-case-smiggle-fresh-combo.jpg'),
(11, 'https://imagesswp391.blob.core.windows.net/post/11e71335-5dba-480f-977b-96488aaee68fa4-spiral-notebook-300-pages-large-grid-wire-bound-good-quality-299.jpg'),
(12, 'https://imagesswp391.blob.core.windows.net/post/7bdbbc65-b394-45b7-8155-1422fe6ef066download%20%283%29.jpg'),
(13, 'https://imagesswp391.blob.core.windows.net/post/b76231d5-9ce7-4dbd-9c3b-3ae17cb2dde4Dan%20ty%20ba.jpg'),
(14, 'https://imagesswp391.blob.core.windows.net/post/a8a82663-96fc-4393-a91b-d5e67ce729d1Kaki%20pants.jpg'),
(15, 'https://imagesswp391.blob.core.windows.net/post/2ab43cb0-9f95-4c72-bcc9-dd67b8d9c47bJordan%201.jpg');

insert into TopUp ( accountId, topUpPoint, price, topUpDate, topUpStatusId)
values
(5, 200, 200000, '2023-02-02', 8),
(6, 300, 300000, '2023-02-22', 8),
(8, 400, 400000, '2023-04-25', 8),
(11, 650, 650000, '2023-05-20', 8),
(15, 2000, 2000000, '2023-06-30', 8),
(4, 100, 100000, '2023-07-02', 8),
(4, 100, 100000, '2023-07-02', 8),
(4, 100, 100000, '2023-07-02', 8),
(4, 100, 100000, '2023-07-02', 8);

SET NOCOUNT OFF
raiserror('The Secondhand Store database in now ready for use.',0,1)
