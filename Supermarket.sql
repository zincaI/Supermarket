USE [master]
GO
/****** Object:  Database [Supermarket]    Script Date: 5/3/2022 5:53:24 PM ******/
CREATE DATABASE [Supermarket]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Supermarket', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Supermarket.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Supermarket_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Supermarket_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Supermarket] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Supermarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Supermarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Supermarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Supermarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Supermarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Supermarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Supermarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Supermarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Supermarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Supermarket] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Supermarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Supermarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Supermarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Supermarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Supermarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Supermarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Supermarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Supermarket] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Supermarket] SET  MULTI_USER 
GO
ALTER DATABASE [Supermarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Supermarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Supermarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Supermarket] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Supermarket] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Supermarket] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Supermarket', N'ON'
GO
ALTER DATABASE [Supermarket] SET QUERY_STORE = OFF
GO
USE [Supermarket]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/3/2022 5:53:24 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Producer](
--	[ProducerId] [int] IDENTITY(1,1) NOT NULL,
--	[Name] [nvarchar](100) NOT NULL,
--	[Country] [nvarchar](100) NULL

-- CONSTRAINT [PK_Person] PRIMARY KEY
--(
--	[ProducerId] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
--GO
--/****** Object:  Table [dbo].[Phone]    Script Date: 5/3/2022 5:53:24 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Category](
--	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
--	[Name] [varchar](15) NOT NULL
-- CONSTRAINT [CategoryID] PRIMARY KEY 
--(
--	[CategoryID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Product](
--    [ProductID] [int] IDENTITY(1,1) NOT NULL,
--    [ProducerID] [int] NOT NULL,
--    [CategoryID] [int] NOT NULL,
--    [BarCode] [int] NOT NULL,
--    [ExpirationDate] [date],
    
--    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
--    CONSTRAINT [UQ_BarCode] UNIQUE ([BarCode]) -- Unique constraint for BarCode
--        WITH (IGNORE_DUP_KEY = OFF)
--) ON [PRIMARY]
--GO


--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Stock](
--    [StockId] [int] IDENTITY(1,1) NOT NULL,
--	[ProductId] [int] NOT NULL,
--    [Quantity] [int] CHECK ([Quantity] >= 0) NOT NULL,
--    [Date] [date],
--    [SellPrice] [float],
--    [BuyPrice] [float],
    
--    CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED ([StockId] ASC)
--        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[User](
--    [UserId] [int] IDENTITY(1,1) NOT NULL,
--    [Name] [varchar](100) NOT NULL,
--    [Password] [varchar](100) NOT NULL,
--    [Type] [varchar](100) NOT NULL
    
--    CONSTRAINT [UserId] PRIMARY KEY CLUSTERED ([UserId] ASC)
--        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Receipt](
--    [ReceiptId] [int] IDENTITY(1,1) NOT NULL,
--    [UserId] [int]  NOT NULL,
--    [Date] [date] NOT NULL

--    CONSTRAINT [ReceiptId] PRIMARY KEY
--        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]


--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Receipt_Product](
--    [ReceiptProductId] [int] IDENTITY(1,1) NOT NULL,
--	[ProductId] [int]  NOT NULL,
--    [ReceiptId] [int]  NOT NULL,
--    [Quantity] [int] NOT NULL
	
    
--    CONSTRAINT [ReceiptProductId] PRIMARY KEY
--        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]

--GO
--SET IDENTITY_INSERT [dbo].[Person] ON 

--INSERT [dbo].[Person] ([PersonID], [Name], [Address]) VALUES (1, N'Ion', N'Brasov')
--INSERT [dbo].[Person] ([PersonID], [Name], [Address]) VALUES (2, N'Ana', N'Codlea')
--INSERT [dbo].[Person] ([PersonID], [Name], [Address]) VALUES (4, N'Maria', N'Timisoara')
--INSERT [dbo].[Person] ([PersonID], [Name], [Address]) VALUES (6, N'Simona', N'Braila')
--INSERT [dbo].[Person] ([PersonID], [Name], [Address]) VALUES (15, N'Andreea', N'Satu-Mare')
--SET IDENTITY_INSERT [dbo].[Person] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Phone] ON 

--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (2, 1, N'789012', N'Home')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (3, 2, N'1234', N'Office')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (4, 1, N'112233', N'De la birou')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (6, 2, N'234566', N'De acasa')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (8, 2, N'234566', N'Home')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (11, 1, N'222333', N'Home')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (12, 1, N'5678889', N'De acasa')
--INSERT [dbo].[Phone] ([PhoneID], [PersonID], [PhoneNumber], [Description]) VALUES (20, 1, N'1111', N'tretrt')
--SET IDENTITY_INSERT [dbo].[Phone] OFF
--GO
--ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Person] FOREIGN KEY([PersonID])
--REFERENCES [dbo].[Person] ([PersonID])

--ALTER TABLE [dbo].[Receipt]
--DROP CONSTRAINT [PK_Receipt]

--ALTER TABLE [dbo].[Receipt]
--ADD CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED ([ReceiptId] ASC)


--SET IDENTITY_INSERT [dbo].[FK_User] OFF
--GO
--ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_User] FOREIGN KEY([UserId])
--REFERENCES [dbo].[User] ([UserId])

--SET IDENTITY_INSERT [dbo].[FK_Receipt] OFF
--GO
--ALTER TABLE [dbo].[Receipt_Product]  WITH CHECK ADD  CONSTRAINT [FK_Receipt] FOREIGN KEY([ReceiptId])
--REFERENCES [dbo].[Receipt] ([ReceiptId])

--SET IDENTITY_INSERT [dbo].[FK_Product] OFF
--GO
--ALTER TABLE [dbo].[Receipt_Product]  WITH CHECK ADD  CONSTRAINT [FK_Product] FOREIGN KEY([ProductId])
--REFERENCES [dbo].[Product] ([ProductId])

SET IDENTITY_INSERT [dbo].[FK_ProductStock] OFF
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_ProductStock] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])

SET IDENTITY_INSERT [dbo].[FK_Category] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])

SET IDENTITY_INSERT [dbo].[FK_Producer] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Producer] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([ProducerId])

--GO

GO
USE [master]
GO
ALTER DATABASE [Supermarket] SET  READ_WRITE 
GO
