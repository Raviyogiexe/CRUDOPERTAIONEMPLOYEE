USE [master]
GO
/****** Object:  Database [dbEmployee]    Script Date: 8/15/2021 5:45:56 PM ******/
CREATE DATABASE [dbEmployee]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbEmployee', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbEmployee.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbEmployee_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbEmployee_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbEmployee] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbEmployee].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbEmployee] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbEmployee] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbEmployee] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbEmployee] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbEmployee] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbEmployee] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbEmployee] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbEmployee] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbEmployee] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbEmployee] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbEmployee] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbEmployee] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbEmployee] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbEmployee] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbEmployee] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbEmployee] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbEmployee] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbEmployee] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbEmployee] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbEmployee] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbEmployee] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbEmployee] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbEmployee] SET RECOVERY FULL 
GO
ALTER DATABASE [dbEmployee] SET  MULTI_USER 
GO
ALTER DATABASE [dbEmployee] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbEmployee] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbEmployee] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbEmployee] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbEmployee] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbEmployee] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbEmployee', N'ON'
GO
ALTER DATABASE [dbEmployee] SET QUERY_STORE = OFF
GO
USE [dbEmployee]
GO
/****** Object:  User [ravi]    Script Date: 8/15/2021 5:45:56 PM ******/
CREATE USER [ravi] FOR LOGIN [ravi] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Employeedata]    Script Date: 8/15/2021 5:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employeedata](
	[Employee_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_Name] [varchar](50) NULL,
	[dob] [datetime] NULL,
	[gender] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[state] [int] NULL,
 CONSTRAINT [PK_Employeedata] PRIMARY KEY CLUSTERED 
(
	[Employee_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHobies]    Script Date: 8/15/2021 5:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHobies](
	[Hobyies] [int] IDENTITY(1,1) NOT NULL,
	[hobyname] [varchar](50) NULL,
	[employeeid] [int] NULL,
 CONSTRAINT [PK_tblHobies] PRIMARY KEY CLUSTERED 
(
	[Hobyies] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblstate]    Script Date: 8/15/2021 5:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblstate](
	[stateid] [int] IDENTITY(1,1) NOT NULL,
	[statename] [varchar](50) NULL,
 CONSTRAINT [PK_tblstate] PRIMARY KEY CLUSTERED 
(
	[stateid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblstate] ON 

INSERT [dbo].[tblstate] ([stateid], [statename]) VALUES (1, N'rajasthan')
INSERT [dbo].[tblstate] ([stateid], [statename]) VALUES (2, N'up')
INSERT [dbo].[tblstate] ([stateid], [statename]) VALUES (3, N'mp')
INSERT [dbo].[tblstate] ([stateid], [statename]) VALUES (4, N'hp')
SET IDENTITY_INSERT [dbo].[tblstate] OFF
GO
/****** Object:  StoredProcedure [dbo].[insertaempdataproc]    Script Date: 8/15/2021 5:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertaempdataproc]
@empname varchar(max),
@dob datetime,
@gender varchar(10),
@address varchar(10),
@state int,
@idno int output
as 
begin


insert into Employeedata (Employee_Name,dob,gender,address,state)
values(@empname,@dob,@gender,@address,@state)

set @idno= (SELECT @@IDENTITY) 
                                                                        
select @idno as idno  

end
GO
/****** Object:  StoredProcedure [dbo].[showalldata]    Script Date: 8/15/2021 5:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[showalldata]
as
begin

select * from Employeedata join tblstate on Employeedata.state=tblstate.stateid

end
GO
USE [master]
GO
ALTER DATABASE [dbEmployee] SET  READ_WRITE 
GO
