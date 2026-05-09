-- ================================== database table structure ================================== 

CREATE TABLE Cityshop.dbo.M_Customer (
	CustomerId uniqueidentifier NOT NULL,
	CustomerName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerPhone nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerEmail nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerAddress nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Pincode nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Password nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	DLM datetime NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_Customer PRIMARY KEY (CustomerId)
);

CREATE TABLE Cityshop.dbo.M_Item (
	ItemId uniqueidentifier NOT NULL,
	ItemName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ItemDescription nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ItemGroupId uniqueidentifier NULL,
	ItemCategoryId uniqueidentifier NULL,
	ItemSizeId uniqueidentifier NULL,
	ItemCode nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ItemImage nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_Item PRIMARY KEY (ItemId)
);

CREATE TABLE Cityshop.dbo.M_ItemCategory (
	CategoryId uniqueidentifier NOT NULL,
	CategoryName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_ItemCategory PRIMARY KEY (CategoryId)
);

CREATE TABLE Cityshop.dbo.M_ItemGroup (
	GroupId uniqueidentifier NOT NULL,
	GroupName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_ItemGroup PRIMARY KEY (GroupId)
);

CREATE TABLE Cityshop.dbo.M_ItemSize (
	SizeId uniqueidentifier NOT NULL,
	SizeName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_ItemSize PRIMARY KEY (SizeId)
);

CREATE TABLE Cityshop.dbo.M_Shop (
	ShopId uniqueidentifier NOT NULL,
	ShopName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ShopAddress nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ShopCode nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Pincode nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ShopPhone nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ShopLogo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ShopImage nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	GstNo nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Latitude nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Longitude nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	StatusId uniqueidentifier NULL,
	ShopTypeId uniqueidentifier NULL,
	OpeningTime time NULL,
	ClosingTime time NULL,
	NearByLocation nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_Shop PRIMARY KEY (ShopId)
);

CREATE TABLE Cityshop.dbo.M_ShopStatus (
	StatusId uniqueidentifier NOT NULL,
	StatusName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Prefix nvarchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Colour nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_ShopStatus PRIMARY KEY (StatusId)
);

CREATE TABLE Cityshop.dbo.M_ShopType (
	ShopTypeId uniqueidentifier NOT NULL,
	TypeName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_ShopType PRIMARY KEY (ShopTypeId)
);

CREATE TABLE Cityshop.dbo.M_User (
	UserId uniqueidentifier NOT NULL,
	UserName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserPhone nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserEmail nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Password nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Role] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	ModifiedDate datetime NULL,
	IsActive bit NULL,
	CONSTRAINT PK_M_User PRIMARY KEY (UserId)
);

CREATE TABLE Cityshop.dbo.Map_Bill_Payment (
	MapId uniqueidentifier NOT NULL,
	ShopId uniqueidentifier NULL,
	InvoiceNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	OrderId uniqueidentifier NULL,
	PaidCustomerId uniqueidentifier NULL,
	Payment decimal(18,2) NULL,
	Paymode nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_Map_Bill_Payment PRIMARY KEY (MapId)
);

CREATE TABLE Cityshop.dbo.Map_Order_Bill (
	MapId uniqueidentifier NOT NULL,
	ShopId uniqueidentifier NULL,
	InvoiceNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	OrderId uniqueidentifier NULL,
	DeliveryCharge decimal(18,2) NULL,
	OrderTotal decimal(18,2) NULL,
	NetTotal decimal(18,2) NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_Map_Order_Bill PRIMARY KEY (MapId)
);

CREATE TABLE Cityshop.dbo.Map_Order_Items (
	MapId uniqueidentifier NOT NULL,
	OrderId uniqueidentifier NULL,
	ItemId uniqueidentifier NULL,
	SizeId uniqueidentifier NULL,
	ItemQty int NULL,
	Total decimal(18,2) NULL,
	CreatedDate datetime NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	OrderNumber varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Map_Order_Items PRIMARY KEY (MapId)
);

CREATE TABLE Cityshop.dbo.Map_Shop_Item (
	MapId uniqueidentifier NOT NULL,
	ShopId uniqueidentifier NULL,
	ItemId uniqueidentifier NULL,
	SizeId uniqueidentifier NULL,
	Qty int NULL,
	Price decimal(18,2) NULL,
	Discount decimal(18,2) NULL,
	StockQty int NULL,
	CreatedDate datetime NULL,
	CreatedBy nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_Map_Shop_Item PRIMARY KEY (MapId)
);

CREATE TABLE Cityshop.dbo.Tbl_ActiveUser (
	MapId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NULL,
	AccessToken nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	RefreshToken nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	LoginDateTime datetime NULL,
	CONSTRAINT PK_Tbl_ActiveUser PRIMARY KEY (MapId)
);

CREATE TABLE Cityshop.dbo.Tbl_OtpStore (
	trackId uniqueidentifier NOT NULL,
	email nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	otp nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	createTime datetime NULL,
	expiryTime datetime NULL,
	isUsed bit NULL,
	CONSTRAINT PK_OtpStore PRIMARY KEY (trackId)
);

CREATE TABLE Cityshop.dbo.Tbl_Reviews (
	MapId uniqueidentifier NOT NULL,
	ShopId uniqueidentifier NULL,
	CustomerId uniqueidentifier NULL,
	Rating int NULL,
	Comment nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Tbl_Reviews PRIMARY KEY (MapId)
);

CREATE TABLE Cityshop.dbo.Tbl_Shop_Order (
	OrderId uniqueidentifier NOT NULL,
	ShopId uniqueidentifier NULL,
	OrderNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerId uniqueidentifier NULL,
	DeliveryAddress nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Pincode nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Phone nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	DLM datetime NULL,
	ULM nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	OrderTotal decimal(18,2) NULL,
	CONSTRAINT PK_Tbl_Shop_Order PRIMARY KEY (OrderId)
);