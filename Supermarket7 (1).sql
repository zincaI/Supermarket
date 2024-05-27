USE [master]
GO
/****** Object:  Database [Supermarket1]    Script Date: 5/3/2022 5:53:24 PM ******/
CREATE DATABASE [Supermarket7]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Supermarket7', FILENAME = N'E:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Supermarket7.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Supermarket7_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Supermarket7_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Supermarket7] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Supermarket7].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Supermarket7] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Supermarket7] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Supermarket7] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Supermarket7] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Supermarket7] SET ARITHABORT OFF 
GO
ALTER DATABASE [Supermarket7] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Supermarket7] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Supermarket7] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Supermarket7] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Supermarket7] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Supermarket7] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Supermarket7] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Supermarket7] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Supermarket7] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Supermarket7] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Supermarket7] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Supermarket7] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Supermarket7] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Supermarket7] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Supermarket7] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Supermarket7] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Supermarket7] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Supermarket7] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Supermarket7] SET  MULTI_USER 
GO
ALTER DATABASE [Supermarket7] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Supermarket7] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Supermarket7] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Supermarket7] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Supermarket7] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Supermarket7] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Supermarket7', N'ON'
GO
ALTER DATABASE [Supermarket7] SET QUERY_STORE = OFF
GO
USE [Supermarket7]
--creare tabele
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producer](
	[ProducerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Country] [nvarchar](100) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED([ProducerID] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone]    Script Date: 5/3/2022 5:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](15) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
 CONSTRAINT [CategoryID] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
    [ProductID] [int] IDENTITY(1,1) NOT NULL,
    [ProducerID] [int] NOT NULL,
    [CategoryID] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
    [BarCode] [int] NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [UQ_BarCode] UNIQUE ([BarCode]) -- Unique constraint for BarCode
        WITH (IGNORE_DUP_KEY = OFF)
) ON [PRIMARY]


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
    [StockID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
    [Quantity] [int] CHECK ([Quantity] >= 0) NOT NULL,
    [Date] [date],
    [SellPrice] [float],
    [BuyPrice] [float],
    [ExpirationDate] [date],
	[IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED ([StockID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
    [EmployeeID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Password] [varchar](100) NOT NULL,
    [Type] [varchar](100) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [EmployeeID] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
    [ReceiptID] [int] IDENTITY(1,1) NOT NULL,
    [EmployeeID] [int]  NOT NULL,
    [Date] [date] NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [ReceiptID] PRIMARY KEY CLUSTERED ([ReceiptID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt_Product](
    [ReceiptProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int]  NOT NULL,
    [ReceiptID] [int]  NOT NULL,
    [Quantity] [int] NOT NULL,
	[TotalPrice] [float] NOT NULL
	
    
    CONSTRAINT [ReceiptProductID] PRIMARY KEY CLUSTERED ([ReceiptProductID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[Receipt]
DROP CONSTRAINT [PK_Receipt]

ALTER TABLE [dbo].[Receipt]
ADD CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED ([ReceiptID] ASC)


SET IDENTITY_INSERT [dbo].[FK_Employee] OFF
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])

SET IDENTITY_INSERT [dbo].[FK_Receipt] OFF
GO
ALTER TABLE [dbo].[Receipt_Product]  WITH CHECK ADD  CONSTRAINT [FK_Receipt] FOREIGN KEY([ReceiptID])
REFERENCES [dbo].[Receipt] ([ReceiptID])

SET IDENTITY_INSERT [dbo].[FK_Product] OFF
GO
ALTER TABLE [dbo].[Receipt_Product]  WITH CHECK ADD  CONSTRAINT [FK_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])

SET IDENTITY_INSERT [dbo].[FK_ProductStock] OFF
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_ProductStock] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])

SET IDENTITY_INSERT [dbo].[FK_Category] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])

SET IDENTITY_INSERT [dbo].[FK_Producer] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Producer] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producer] ([ProducerID])


--add instruction
GO
/****** Object:  StoredProcedure [dbo].[AddPerson]    Script Date: 5/3/2022 5:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddProduct]
	@ProducerId int,
	@CategoryId int,
	@Name nvarchar(30),
	@Barcode int,
	@ProductId int output
AS
BEGIN
	insert into Product([ProducerID],[CategoryID],[Name],[BarCode]) values( @ProducerId,@CategoryId,@Name,@Barcode)
	select @ProductId = scope_identity()
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddProducer]
	@Name nvarchar(100),
	@Country nvarchar (100),
	@ProducerId int output
AS
BEGIN
	insert into Producer([Name],[Country]) values(@Name,@Country)
	select @ProducerId = scope_identity()
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCategory]
	@Name nvarchar(100),
	@CategoryId int output
AS
BEGIN
	insert into Producer([Name]) values(@Name)
	select @CategoryId = scope_identity()
END


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddStock]
	@ProductId int,
	@Quantity int,
	@Date date,
	@BuyPrice float,
	@SellPrice float,
	@ExpirationDate date,
	@StockId int output
AS
BEGIN
	insert into Stock([ProductID],[Quantity],[Date],[BuyPrice],[SellPrice],[ExpirationDate]) values(@ProductId,@Quantity,@Date,@BuyPrice,@SellPrice,@ExpirationDate)
	select @StockId = scope_identity()
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddEmployee]
	@Name varchar(100),
	@Password varchar (100),
	@Type varchar(100),
	@EmployeeId int output
AS
BEGIN
	insert into Employee([Name],[Password],[Type]) values(@Name,@Password,@Type)
	select @EmployeeId = scope_identity()
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE[dbo].[AddReceipt]
	@EmployeeId int,
	@Date date,
	@ReceiptId int output
AS
BEGIN
	insert into Receipt([EmployeeID],[Date]) values (@EmployeeId,@Date)
	select @ReceiptId=scope_identity()
END


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[AddReceipt_Product]
	@ProductId int,
	@ReceiptId int,
	@Quantity int,
	@TotalPrice float,
	@Receipt_ProductId int output
AS
BEGIN
	insert into Receipt_Product([ProductID],[ReceiptID],[Quantity],[TotalPrice]) values (@ProductId,@ReceiptId,@Quantity,@TotalPrice)
	select @Receipt_ProductId=scope_identity()
END


--modify instruction

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyProduct] 
	@ProductId int,
	@ProducerId int,
	@CategoryId int,
	@Name nvarchar(30),
	@BarCode int
AS
BEGIN
	update	Product
	set		[ProducerID] = @ProducerId, 
			[CategoryID] = @CategoryId,
			[Name]=@Name,
			[BarCode]=@BarCode
	where	ProductID = @ProductId
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyProducer] 
	@ProducerId int,
	@Name nvarchar(100),
	@Country nvarchar(100)
AS
BEGIN
	update	Producer
	set		[Name] = @Name, 
			[Country] = @Country
	where	ProducerID = @ProducerId
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyCategory] 
	@CategoryId int,
	@Name nvarchar(15)
AS
BEGIN
	update	Category
	set		[Name] = @Name
	where	CategoryID = @CategoryId
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyStock] 
	@StockId int,
	@ProductId int,
	@Quantity int,
	@Date date,
	@SellPrice float,
	@ExpirationDate date
AS
BEGIN
	update	Stock
	set		[ProductID] = @ProductId,
			[Quantity] = @Quantity,
			[Date]=@Date,
			[SellPrice]=@SellPrice,
			[ExpirationDate]=@ExpirationDate
	where	StockID = @StockId
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyEmployee] 
	@EmployeeId int,
	@Name varchar(100),
	@Password varchar(100),
	@Type varchar(100)
AS
BEGIN
	update	Employee
	set		[Name] = @Name,
			[Password] = @Password,
			[Type]=@Type
	where	EmployeeID = @EmployeeId
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyReceipt_Product] 
	@Receipt_ProductId int,
	@ProductId int,
	@ReceiptId int,
	@Quantity int,
	@TotalPrice float
AS
BEGIN
	update Receipt_Product
	set		[ProductID] = @ProductId,
			[ReceiptID] = @ReceiptId,
			[Quantity]=@Quantity,
			[TotalPrice]=@TotalPrice
	where	ReceiptProductID = @Receipt_ProductId
END

--delete instruction

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProduct]
	@id int
AS
BEGIN
	update Product
	set IsActive=0
	where ProductID = @id
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProducer]
	@id int
AS
BEGIN
	update Producer
	set IsActive=0
	where ProducerID = @id
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCategory]
	@id int
AS
BEGIN
	update Category
	set IsActive=0
	where CategoryID = @id
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStock]
	@id int
AS
BEGIN
	update Stock
	set IsActive=0
	where StockID = @id
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEmployee]
	@id int
AS
BEGIN
	update Employee
	set IsActive=0
	where EmployeeID = @id
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteReceipt]
	@id int
AS
BEGIN
	update Receipt
	set IsActive=0
	where ReceiptID = @id
END



--get instruction

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProduct]
AS
BEGIN
	select ProductID,[ProducerID],[CategoryID],[Name], [BarCode],[IsActive] from Product
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducer]
AS
BEGIN
	select ProducerID,[Name],[Country],[IsActive] 
	from Producer
	where IsActive=1
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCategory]
AS
BEGIN
	select CategoryID,[Name],[IsActive] from Category
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllStock]
AS
BEGIN
	select StockID,[ProductID],[Quantity],[Date],[SellPrice],[BuyPrice],[ExpirationDate],[IsActive] from Stock
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllEmployee]
AS
BEGIN
	select EmployeeID,[Name],[Password],[Type],[IsActive] from Employee
END


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllReceipt]
AS
BEGIN
	select ReceiptID,[EmployeeID],[Date],[IsActive] from Receipt
END


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllReceipt_Product]
AS
BEGIN
	select ReceiptProductID,[ProductID],[ReceiptID],[Quantity],[TotalPrice] from Receipt_Product
END




--restabilire tabela
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RestoreProducer]
    @ProducerID INT
AS
BEGIN
    UPDATE [dbo].[Producer]
    SET [IsActive] = 1
    WHERE [ProducerID] = @ProducerID;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestoreCategory]
    @CategoryID INT
AS
BEGIN

    UPDATE [dbo].[Category]
    SET [IsActive] = 1
    WHERE [CategoryID] = @CategoryID;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestoreProduct]
    @ProductID INT
AS
BEGIN

    UPDATE [dbo].[Product]
    SET [IsActive] = 1
    WHERE [ProductID] = @ProductID;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestoreStock]
    @StockID INT
AS
BEGIN

    UPDATE [dbo].[Stock]
    SET [IsActive] = 1
    WHERE [StockID] = @StockID;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestoreEmployee]
    @EmployeeId INT
AS
BEGIN

    UPDATE [dbo].[Employee]
    SET [IsActive] = 1
    WHERE [EmployeeID] =@EmployeeId;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestoreReceipt]
    @ReceiptID INT
AS
BEGIN

    UPDATE [dbo].[Receipt]
    SET [IsActive] = 1
    WHERE [ReceiptID] = @ReceiptID;
END

GO
USE [master]
GO
ALTER DATABASE [Supermarket2] SET  READ_WRITE 
GO