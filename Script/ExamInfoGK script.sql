USE [master]
GO
/****** Object:  Database [GKJ_2022]    Script Date: 23-02-2023 11:37:53 ******/
CREATE DATABASE [GKJ_2022]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GKJ2022', FILENAME = N'D:\MSSQL13.MSSQLSERVER\MSSQL\DATA\GKJ2022.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GKJ2022_log', FILENAME = N'D:\MSSQL13.MSSQLSERVER\MSSQL\DATA\GKJ2022_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GKJ_2022] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GKJ_2022].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GKJ_2022] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GKJ_2022] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GKJ_2022] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GKJ_2022] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GKJ_2022] SET ARITHABORT OFF 
GO
ALTER DATABASE [GKJ_2022] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GKJ_2022] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GKJ_2022] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GKJ_2022] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GKJ_2022] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GKJ_2022] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GKJ_2022] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GKJ_2022] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GKJ_2022] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GKJ_2022] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GKJ_2022] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GKJ_2022] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GKJ_2022] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GKJ_2022] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GKJ_2022] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GKJ_2022] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GKJ_2022] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GKJ_2022] SET RECOVERY FULL 
GO
ALTER DATABASE [GKJ_2022] SET  MULTI_USER 
GO
ALTER DATABASE [GKJ_2022] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GKJ_2022] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GKJ_2022] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GKJ_2022] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GKJ_2022] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GKJ_2022] SET QUERY_STORE = OFF
GO
USE [GKJ_2022]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [GKJ_2022]
GO
/****** Object:  Table [dbo].[Admin_tbl]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin_tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Heading] [nvarchar](max) NULL,
	[Extension] [nvarchar](max) NULL,
	[Active] [varchar](50) NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_Admin_tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[All_tbl_Final_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[All_tbl_Final_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q1_Ans] [nvarchar](max) NULL,
	[Q_2Ans] [nvarchar](max) NULL,
	[Q_3Ans] [nvarchar](max) NULL,
	[Q_4Ans] [nvarchar](max) NULL,
	[Q_5Ans] [nvarchar](max) NULL,
	[Q_6Ans] [nvarchar](max) NULL,
	[Q_7Ans] [nvarchar](max) NULL,
	[Q_8Ans] [nvarchar](max) NULL,
	[Q_9Ans] [nvarchar](max) NULL,
	[Q_10Ans] [nvarchar](max) NULL,
	[Q_11Ans] [nvarchar](max) NULL,
	[Q_12Ans] [nvarchar](max) NULL,
	[Q_13Ans] [nvarchar](max) NULL,
	[Q_14Ans] [nvarchar](max) NULL,
	[Q_15Ans] [nvarchar](max) NULL,
	[Q_16Ans] [nvarchar](max) NULL,
	[Q_17Ans] [nvarchar](max) NULL,
	[Q_18Ans] [nvarchar](max) NULL,
	[Q_19Ans] [nvarchar](max) NULL,
	[Q_20Ans] [nvarchar](max) NULL,
	[Q_21Ans] [nvarchar](max) NULL,
	[Q_22Ans] [nvarchar](max) NULL,
	[Q_23Ans] [nvarchar](max) NULL,
	[Q_24Ans] [nvarchar](max) NULL,
	[Q_25Ans] [nvarchar](max) NULL,
	[Q_26Ans] [nvarchar](max) NULL,
	[Q_27Ans] [nvarchar](max) NULL,
	[Q_28Ans] [nvarchar](max) NULL,
	[Q_29Ans] [nvarchar](max) NULL,
	[Q_30Ans] [nvarchar](max) NULL,
	[Q_31Ans] [nvarchar](max) NULL,
	[Q_32Ans] [nvarchar](max) NULL,
	[Q_33Ans] [nvarchar](max) NULL,
	[Q_34Ans] [nvarchar](max) NULL,
	[Q_35Ans] [nvarchar](max) NULL,
	[Q_36Ans] [nvarchar](max) NULL,
	[Q_37Ans] [nvarchar](max) NULL,
	[Q_38Ans] [nvarchar](max) NULL,
	[Q_39Ans] [nvarchar](max) NULL,
	[Q_40Ans] [nvarchar](max) NULL,
	[Q_41Ans] [nvarchar](max) NULL,
	[Q_42Ans] [nvarchar](max) NULL,
	[Q_43Ans] [nvarchar](max) NULL,
	[Q_44Ans] [nvarchar](max) NULL,
	[Q_45Ans] [nvarchar](max) NULL,
	[Q_46Ans] [nvarchar](max) NULL,
	[Q_47Ans] [nvarchar](max) NULL,
	[Q_48Ans] [nvarchar](max) NULL,
	[Q_49Ans] [nvarchar](max) NULL,
	[Q_50Ans] [nvarchar](max) NULL,
	[Q_51Ans] [nvarchar](max) NULL,
	[Q_52Ans] [nvarchar](max) NULL,
	[Q_53Ans] [nvarchar](max) NULL,
	[Q_54Ans] [nvarchar](max) NULL,
	[Q_55Ans] [nvarchar](max) NULL,
	[Q_56Ans] [nvarchar](max) NULL,
	[Q_57Ans] [nvarchar](max) NULL,
	[Q_58Ans] [nvarchar](max) NULL,
	[Q_59Ans] [nvarchar](max) NULL,
	[Q_60Ans] [nvarchar](max) NULL,
	[Q_61Ans] [nvarchar](max) NULL,
	[Q_62Ans] [nvarchar](max) NULL,
	[Q_63Ans] [nvarchar](max) NULL,
	[Q_64Ans] [nvarchar](max) NULL,
	[Q_65Ans] [nvarchar](max) NULL,
	[Q_66Ans] [nvarchar](max) NULL,
	[Q_67Ans] [nvarchar](max) NULL,
	[Q_68Ans] [nvarchar](max) NULL,
	[Q_69Ans] [nvarchar](max) NULL,
	[Q_70Ans] [nvarchar](max) NULL,
	[Q_71Ans] [nvarchar](max) NULL,
	[Q_72Ans] [nvarchar](max) NULL,
	[Q_73Ans] [nvarchar](max) NULL,
	[Q_74Ans] [nvarchar](max) NULL,
	[Q_75Ans] [nvarchar](max) NULL,
	[Q_76Ans] [nvarchar](max) NULL,
	[Q_77Ans] [nvarchar](max) NULL,
	[Q_78Ans] [nvarchar](max) NULL,
	[Q_79Ans] [nvarchar](max) NULL,
	[Q_80Ans] [nvarchar](max) NULL,
	[Q_81Ans] [nvarchar](max) NULL,
	[Q_82Ans] [nvarchar](max) NULL,
	[Q_83Ans] [nvarchar](max) NULL,
	[Q_84Ans] [nvarchar](max) NULL,
	[Q_85Ans] [nvarchar](max) NULL,
	[Q_86Ans] [nvarchar](max) NULL,
	[Q_87Ans] [nvarchar](max) NULL,
	[Q_88Ans] [nvarchar](max) NULL,
	[Q_89Ans] [nvarchar](max) NULL,
	[Q_90Ans] [nvarchar](max) NULL,
	[Q_91Ans] [nvarchar](max) NULL,
	[Q_92Ans] [nvarchar](max) NULL,
	[Q_93Ans] [nvarchar](max) NULL,
	[Q_94Ans] [nvarchar](max) NULL,
	[Q_95Ans] [nvarchar](max) NULL,
	[Q_96Ans] [nvarchar](max) NULL,
	[Q_97Ans] [nvarchar](max) NULL,
	[Q_98Ans] [nvarchar](max) NULL,
	[Q_99Ans] [nvarchar](max) NULL,
	[Q_100Ans] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[File_Upload_Reason]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File_Upload_Reason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Seat_No] [nvarchar](7) NULL,
	[File_Status] [nvarchar](50) NULL,
	[Reason] [nvarchar](max) NULL,
	[Index_No] [nvarchar](50) NULL,
 CONSTRAINT [PK_File_Upload_Reason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_100_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_100_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q100Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_11to15_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_11to15_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q11Ans] [nvarchar](max) NULL,
	[Q12Ans] [nvarchar](max) NULL,
	[Q13Ans] [nvarchar](max) NULL,
	[Q14Ans] [nvarchar](max) NULL,
	[Q15Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_1to5_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_1to5_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q1Ans] [nvarchar](max) NULL,
	[Q2Ans] [nvarchar](max) NULL,
	[Q3Ans] [nvarchar](max) NULL,
	[Q4Ans] [nvarchar](max) NULL,
	[Q5Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_21to25_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_21to25_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q21Ans] [nvarchar](max) NULL,
	[Q22Ans] [nvarchar](max) NULL,
	[Q23Ans] [nvarchar](max) NULL,
	[Q24Ans] [nvarchar](max) NULL,
	[Q25Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_26to30_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_26to30_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q26Ans] [nvarchar](max) NULL,
	[Q27Ans] [nvarchar](max) NULL,
	[Q28Ans] [nvarchar](max) NULL,
	[Q29Ans] [nvarchar](max) NULL,
	[Q30Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_31to35_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_31to35_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q31Ans] [nvarchar](max) NULL,
	[Q32Ans] [nvarchar](max) NULL,
	[Q33Ans] [nvarchar](max) NULL,
	[Q34Ans] [nvarchar](max) NULL,
	[Q35Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_36to40_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_36to40_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q36Ans] [nvarchar](max) NULL,
	[Q37Ans] [nvarchar](max) NULL,
	[Q38Ans] [nvarchar](max) NULL,
	[Q39Ans] [nvarchar](max) NULL,
	[Q40Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_41to45_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_41to45_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q41Ans] [nvarchar](max) NULL,
	[Q42Ans] [nvarchar](max) NULL,
	[Q43Ans] [nvarchar](max) NULL,
	[Q44Ans] [nvarchar](max) NULL,
	[Q45Ans] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_41to45_Ans] PRIMARY KEY CLUSTERED 
(
	[seat_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_46to50_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_46to50_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q46Ans] [nvarchar](max) NULL,
	[Q47Ans] [nvarchar](max) NULL,
	[Q48Ans] [nvarchar](max) NULL,
	[Q49Ans] [nvarchar](max) NULL,
	[Q50Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_51to55_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_51to55_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q51Ans] [nvarchar](max) NULL,
	[Q52Ans] [nvarchar](max) NULL,
	[Q53Ans] [nvarchar](max) NULL,
	[Q54Ans] [nvarchar](max) NULL,
	[Q55Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_56to60_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_56to60_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q56Ans] [nvarchar](max) NULL,
	[Q57Ans] [nvarchar](max) NULL,
	[Q58Ans] [nvarchar](max) NULL,
	[Q59Ans] [nvarchar](max) NULL,
	[Q60Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_61to65_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_61to65_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q61Ans] [nvarchar](max) NULL,
	[Q62Ans] [nvarchar](max) NULL,
	[Q63Ans] [nvarchar](max) NULL,
	[Q64Ans] [nvarchar](max) NULL,
	[Q65Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_66to70_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_66to70_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q66Ans] [nvarchar](max) NULL,
	[Q67Ans] [nvarchar](max) NULL,
	[Q68Ans] [nvarchar](max) NULL,
	[Q69Ans] [nvarchar](max) NULL,
	[Q70Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_6to10_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_6to10_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q6Ans] [nvarchar](max) NULL,
	[Q7Ans] [nvarchar](max) NULL,
	[Q8Ans] [nvarchar](max) NULL,
	[Q9Ans] [nvarchar](max) NULL,
	[Q10Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_71to75_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_71to75_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q71Ans] [nvarchar](max) NULL,
	[Q72Ans] [nvarchar](max) NULL,
	[Q73Ans] [nvarchar](max) NULL,
	[Q74Ans] [nvarchar](max) NULL,
	[Q75Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_76to80_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_76to80_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q76Ans] [nvarchar](max) NULL,
	[Q77Ans] [nvarchar](max) NULL,
	[Q78Ans] [nvarchar](max) NULL,
	[Q79Ans] [nvarchar](max) NULL,
	[Q80Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_81to85_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_81to85_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q81Ans] [nvarchar](max) NULL,
	[Q82Ans] [nvarchar](max) NULL,
	[Q83Ans] [nvarchar](max) NULL,
	[Q84Ans] [nvarchar](max) NULL,
	[Q85Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_86to90_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_86to90_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q86Ans] [nvarchar](max) NULL,
	[Q87Ans] [nvarchar](max) NULL,
	[Q88Ans] [nvarchar](max) NULL,
	[Q89Ans] [nvarchar](max) NULL,
	[Q90Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_91to95_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_91to95_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [datetime] NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q91Ans] [nvarchar](max) NULL,
	[Q92Ans] [nvarchar](max) NULL,
	[Q93Ans] [nvarchar](max) NULL,
	[Q94Ans] [nvarchar](max) NULL,
	[Q95Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_96to97_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_96to97_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q96Ans] [nvarchar](max) NULL,
	[Q97Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_98_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_98_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q98Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_99_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_99_Ans](
	[index_no] [nvarchar](50) NULL,
	[seat_no] [nvarchar](50) NOT NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q99Ans] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Admin_Login]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Admin_Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Admin_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Attendance]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Attendance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](max) NULL,
	[Seat_no] [nvarchar](max) NULL,
	[time_time] [datetime] NULL,
	[Batch] [nvarchar](max) NULL,
	[MAC] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Attendance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Attendance_Web]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Attendance_Web](
	[ID] [int] NOT NULL,
	[Roll_No] [nvarchar](7) NOT NULL,
	[Batch] [nvarchar](max) NULL,
	[Exam_Login_Status] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](4) NULL,
	[Attendance_Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Attendance_Web] PRIMARY KEY CLUSTERED 
(
	[Roll_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Batch]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Batch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ID_Student] [int] NULL,
	[Student_Name] [nvarchar](max) NULL,
	[Total_Student] [int] NULL,
	[B1] [int] NULL,
	[B2] [int] NULL,
	[B3] [int] NULL,
	[B4] [int] NULL,
	[B5] [int] NULL,
	[B6] [int] NULL,
 CONSTRAINT [PK_Tbl_Batch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Batch_Activation]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Batch_Activation](
	[ID] [int] NOT NULL,
	[Batch] [nvarchar](2) NOT NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Tbl_Batch_Activation] PRIMARY KEY CLUSTERED 
(
	[Batch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Code_Master]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Code_Master](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[division_code] [nvarchar](max) NULL,
	[division_name] [nvarchar](max) NULL,
	[district_code] [nvarchar](max) NULL,
	[district_name] [nvarchar](max) NULL,
	[taluka_code] [nvarchar](max) NULL,
	[taluka_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Code_Master] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_College_Registration]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_College_Registration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[College_Name] [nvarchar](max) NULL,
	[College_Address] [nvarchar](max) NULL,
	[Principal_Name] [nvarchar](max) NULL,
	[Principal_Mobile] [nvarchar](max) NULL,
	[Total_Students] [nvarchar](max) NULL,
	[Total_Machines] [nvarchar](max) NULL,
	[Total_Teachers] [nvarchar](max) NULL,
	[IT_Teacher_Name] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Confirm_Password] [nvarchar](max) NULL,
	[IT_Teachers_MobileNumber1] [nvarchar](max) NULL,
	[IT_Teachers_Mobilenumber2] [nvarchar](max) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Tbl_College_Registration_1] PRIMARY KEY CLUSTERED 
(
	[Index_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Decode_Answer]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Decode_Answer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[index_no] [nvarchar](7) NULL,
	[seat_no] [nvarchar](7) NULL,
	[datetime] [nvarchar](max) NULL,
	[ip] [nvarchar](max) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q1_Ans] [nvarchar](max) NULL,
	[Q_2Ans] [nvarchar](max) NULL,
	[Q_3Ans] [nvarchar](max) NULL,
	[Q_4Ans] [nvarchar](max) NULL,
	[Q_5Ans] [nvarchar](max) NULL,
	[Q_6Ans] [nvarchar](max) NULL,
	[Q_7Ans] [nvarchar](max) NULL,
	[Q_8Ans] [nvarchar](max) NULL,
	[Q_9Ans] [nvarchar](max) NULL,
	[Q_10Ans] [nvarchar](max) NULL,
	[Q_11Ans] [nvarchar](max) NULL,
	[Q_12Ans] [nvarchar](max) NULL,
	[Q_13Ans] [nvarchar](max) NULL,
	[Q_14Ans] [nvarchar](max) NULL,
	[Q_15Ans] [nvarchar](max) NULL,
	[Q_16Ans] [nvarchar](max) NULL,
	[Q_17Ans] [nvarchar](max) NULL,
	[Q_18Ans] [nvarchar](max) NULL,
	[Q_19Ans] [nvarchar](max) NULL,
	[Q_20Ans] [nvarchar](max) NULL,
	[Q_21Ans] [nvarchar](max) NULL,
	[Q_22Ans] [nvarchar](max) NULL,
	[Q_23Ans] [nvarchar](max) NULL,
	[Q_24Ans] [nvarchar](max) NULL,
	[Q_25Ans] [nvarchar](max) NULL,
	[Q_26Ans] [nvarchar](max) NULL,
	[Q_27Ans] [nvarchar](max) NULL,
	[Q_28Ans] [nvarchar](max) NULL,
	[Q_29Ans] [nvarchar](max) NULL,
	[Q_30Ans] [nvarchar](max) NULL,
	[Q_31Ans] [nvarchar](max) NULL,
	[Q_32Ans] [nvarchar](max) NULL,
	[Q_33Ans] [nvarchar](max) NULL,
	[Q_34Ans] [nvarchar](max) NULL,
	[Q_35Ans] [nvarchar](max) NULL,
	[Q_36Ans] [nvarchar](max) NULL,
	[Q_37Ans] [nvarchar](max) NULL,
	[Q_38Ans] [nvarchar](max) NULL,
	[Q_39Ans] [nvarchar](max) NULL,
	[Q_40Ans] [nvarchar](max) NULL,
	[Q_41Ans] [nvarchar](max) NULL,
	[Q_42Ans] [nvarchar](max) NULL,
	[Q_43Ans] [nvarchar](max) NULL,
	[Q_44Ans] [nvarchar](max) NULL,
	[Q_45Ans] [nvarchar](max) NULL,
	[Q_46Ans] [nvarchar](max) NULL,
	[Q_47Ans] [nvarchar](max) NULL,
	[Q_48Ans] [nvarchar](max) NULL,
	[Q_49Ans] [nvarchar](max) NULL,
	[Q_50Ans] [nvarchar](max) NULL,
	[Q_51Ans] [nvarchar](max) NULL,
	[Q_52Ans] [nvarchar](max) NULL,
	[Q_53Ans] [nvarchar](max) NULL,
	[Q_54Ans] [nvarchar](max) NULL,
	[Q_55Ans] [nvarchar](max) NULL,
	[Q_56Ans] [nvarchar](max) NULL,
	[Q_57Ans] [nvarchar](max) NULL,
	[Q_58Ans] [nvarchar](max) NULL,
	[Q_59Ans] [nvarchar](max) NULL,
	[Q_60Ans] [nvarchar](max) NULL,
	[Q_61Ans] [nvarchar](max) NULL,
	[Q_62Ans] [nvarchar](max) NULL,
	[Q_63Ans] [nvarchar](max) NULL,
	[Q_64Ans] [nvarchar](max) NULL,
	[Q_65Ans] [nvarchar](max) NULL,
	[Q_66Ans] [nvarchar](max) NULL,
	[Q_67Ans] [nvarchar](max) NULL,
	[Q_68Ans] [nvarchar](max) NULL,
	[Q_69Ans] [nvarchar](max) NULL,
	[Q_70Ans] [nvarchar](max) NULL,
	[Q_71Ans] [nvarchar](max) NULL,
	[Q_72Ans] [nvarchar](max) NULL,
	[Q_73Ans] [nvarchar](max) NULL,
	[Q_74Ans] [nvarchar](max) NULL,
	[Q_75Ans] [nvarchar](max) NULL,
	[Q_76Ans] [nvarchar](max) NULL,
	[Q_77Ans] [nvarchar](max) NULL,
	[Q_78Ans] [nvarchar](max) NULL,
	[Q_79Ans] [nvarchar](max) NULL,
	[Q_80Ans] [nvarchar](max) NULL,
	[Q_81Ans] [nvarchar](max) NULL,
	[Q_82Ans] [nvarchar](max) NULL,
	[Q_83Ans] [nvarchar](max) NULL,
	[Q_84Ans] [nvarchar](max) NULL,
	[Q_85Ans] [nvarchar](max) NULL,
	[Q_86Ans] [nvarchar](max) NULL,
	[Q_87Ans] [nvarchar](max) NULL,
	[Q_88Ans] [nvarchar](max) NULL,
	[Q_89Ans] [nvarchar](max) NULL,
	[Q_90Ans] [nvarchar](max) NULL,
	[Q_91Ans] [nvarchar](max) NULL,
	[Q_92Ans] [nvarchar](max) NULL,
	[Q_93Ans] [nvarchar](max) NULL,
	[Q_94Ans] [nvarchar](max) NULL,
	[Q_95Ans] [nvarchar](max) NULL,
	[Q_96Ans] [nvarchar](max) NULL,
	[Q_97Ans] [nvarchar](max) NULL,
	[Q_98Ans] [nvarchar](max) NULL,
	[Q_99Ans] [nvarchar](max) NULL,
	[Q_100Ans] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Decode_Answer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Decoded_Answer_Sheets_Sada]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Decoded_Answer_Sheets_Sada](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Seat_No] [nvarchar](50) NULL,
	[Login_Status] [nvarchar](50) NULL,
	[Paper_ID] [nvarchar](50) NULL,
	[Exam_Time] [nvarchar](max) NULL,
	[Q1ANS1] [nvarchar](max) NULL,
	[Q1ANS2] [nvarchar](max) NULL,
	[Q1ANS3] [nvarchar](max) NULL,
	[Q1ANS4] [nvarchar](max) NULL,
	[Q1ANS5] [nvarchar](max) NULL,
	[Q1ANS6] [nvarchar](max) NULL,
	[Q1ANS7] [nvarchar](max) NULL,
	[Q1ANS8] [nvarchar](max) NULL,
	[Q1ANS9] [nvarchar](max) NULL,
	[Q1ANS10] [nvarchar](max) NULL,
	[Q1ANSWERTIME] [nvarchar](max) NULL,
	[Q2ANS1] [nvarchar](max) NULL,
	[Q2ANS2] [nvarchar](max) NULL,
	[Q2ANS3] [nvarchar](max) NULL,
	[Q2ANS4] [nvarchar](max) NULL,
	[Q2ANS5] [nvarchar](max) NULL,
	[Q2ANS6] [nvarchar](max) NULL,
	[Q2ANS7] [nvarchar](max) NULL,
	[Q2ANS8] [nvarchar](max) NULL,
	[Q2ANS9] [nvarchar](max) NULL,
	[Q2ANS10] [nvarchar](max) NULL,
	[Q2ANSWERTIME] [nvarchar](max) NULL,
	[Q3ANS1] [nvarchar](max) NULL,
	[Q3ANS2] [nvarchar](max) NULL,
	[Q3ANS3] [nvarchar](max) NULL,
	[Q3ANS4] [nvarchar](max) NULL,
	[Q3ANS5] [nvarchar](max) NULL,
	[Q3ANS6] [nvarchar](max) NULL,
	[Q3ANS7] [nvarchar](max) NULL,
	[Q3ANS8] [nvarchar](max) NULL,
	[Q3ANS9] [nvarchar](max) NULL,
	[Q3ANS10] [nvarchar](max) NULL,
	[Q3ANSWERTIME] [nvarchar](max) NULL,
	[Q4ANS1] [nvarchar](max) NULL,
	[Q4ANS2] [nvarchar](max) NULL,
	[Q4ANS3] [nvarchar](max) NULL,
	[Q4ANS4] [nvarchar](max) NULL,
	[Q4ANS5] [nvarchar](max) NULL,
	[Q4ANS6] [nvarchar](max) NULL,
	[Q4ANS7] [nvarchar](max) NULL,
	[Q4ANS8] [nvarchar](max) NULL,
	[Q4ANS9] [nvarchar](max) NULL,
	[Q4ANS10] [nvarchar](max) NULL,
	[Q4ANSWERTIME] [nvarchar](max) NULL,
	[Q5ANS1] [nvarchar](max) NULL,
	[Q5ANS2] [nvarchar](max) NULL,
	[Q5ANSWERTIME] [nvarchar](max) NULL,
	[Q6ANS1] [nvarchar](max) NULL,
	[Q6ANSWERTIME] [nvarchar](max) NULL,
	[Q7ANS1] [nvarchar](max) NULL,
	[Q7ANS2] [nvarchar](max) NULL,
	[Q7ANS3] [nvarchar](max) NULL,
	[Q7ANS4] [nvarchar](max) NULL,
	[Q7ANS5] [nvarchar](max) NULL,
	[Q7ANS6] [nvarchar](max) NULL,
	[Q7ANS7] [nvarchar](max) NULL,
	[Q7ANS8] [nvarchar](max) NULL,
	[Q7ANSWERTIME] [nvarchar](max) NULL,
	[Q8ANS1] [nvarchar](max) NULL,
	[Q8ANS2] [nvarchar](max) NULL,
	[Q8ANS3] [nvarchar](max) NULL,
	[Q8ANS4] [nvarchar](max) NULL,
	[Q8ANSWERTIME] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Decoded_Answer_Sheets_Sada] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_District_Co_Ordinator_Details]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_District_Co_Ordinator_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[District_Co_Ordinator_ID] [int] NULL,
	[Co_Ordinator_Name] [nvarchar](max) NULL,
	[District_Code] [nvarchar](max) NULL,
	[Taluka_Code] [nvarchar](max) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Tbl_District_Co_Ordinator_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_District_Co_Ordinator_Registration]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_District_Co_Ordinator_Registration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[College_Name] [nvarchar](max) NULL,
	[College_Address] [nvarchar](max) NULL,
	[Coordinator_Name] [nvarchar](max) NULL,
	[Coordinator_Mobile] [nvarchar](max) NULL,
	[Coordinator_Email] [nvarchar](max) NULL,
	[Coordinator_Eduction] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Confirm_Password] [nvarchar](max) NULL,
	[Division_Code] [nvarchar](max) NULL,
	[District_Code] [nvarchar](max) NULL,
	[Taluka_Code] [nvarchar](max) NULL,
	[College_Code] [nvarchar](max) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Tbl_District_Co_Ordinator_Registration_1] PRIMARY KEY CLUSTERED 
(
	[Index_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Division_Co_Ordinator_Registration]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Division_Co_Ordinator_Registration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[College_Name] [nvarchar](max) NULL,
	[College_Address] [nvarchar](max) NULL,
	[Coordinator_Name] [nvarchar](max) NULL,
	[Coordinator_Mobile] [nvarchar](max) NULL,
	[Coordinator_Email] [nvarchar](max) NULL,
	[Coordinator_Eduction] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Confirm_Password] [nvarchar](max) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Tbl_Division_Co_Ordinator_Registration_1] PRIMARY KEY CLUSTERED 
(
	[Index_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_DTT1]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_DTT1](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Datetime] [nvarchar](50) NULL,
	[Index_No] [nvarchar](50) NULL,
	[Index_NoN] [nvarchar](50) NULL,
	[SYS_No] [nvarchar](50) NULL,
	[MAC] [nvarchar](max) NULL,
	[Screen_Res] [nvarchar](max) NULL,
	[Screen_Res_Change] [nvarchar](50) NULL,
	[Read_Wrtite_Access] [nvarchar](50) NULL,
	[Processor] [nvarchar](50) NULL,
	[Active] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_DTT1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_DTT2]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_DTT2](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Datetime] [nvarchar](max) NULL,
	[Index_No] [nvarchar](50) NULL,
	[Index_No_OLD] [nvarchar](50) NULL,
	[Read_Wrtite_Access] [nvarchar](max) NULL,
	[datetime_set] [nvarchar](max) NULL,
	[MAC] [nvarchar](max) NULL,
	[Active] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_DTT2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_DTT3]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_DTT3](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_NoN] [nvarchar](50) NULL,
	[Index_No] [nvarchar](50) NULL,
	[Datetime] [nvarchar](max) NULL,
	[MAC] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NULL,
	[QP] [nvarchar](50) NOT NULL,
	[Hit] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tbl_DTT3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_File_Upload_Details]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_File_Upload_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](max) NULL,
	[IP_ADDRESS] [nvarchar](max) NULL,
	[date_Time] [datetime] NULL,
	[Teacher_Name] [nvarchar](max) NULL,
	[Mobile_Number] [nvarchar](max) NULL,
	[Email_Id] [nvarchar](max) NULL,
	[Total_Students] [int] NULL,
	[Total_Present_Student] [int] NULL,
	[Total_Absent_Student] [int] NULL,
	[Text_Ans_File] [int] NULL,
 CONSTRAINT [PK_Tbl_File_Upload_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Final_Ans]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Final_Ans](
	[Index_No] [nvarchar](50) NULL,
	[Seat_No] [nvarchar](50) NULL,
	[datetime] [datetime] NULL,
	[Ip] [nvarchar](50) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q1_Ans] [nvarchar](max) NULL,
	[Q_2Ans] [nvarchar](max) NULL,
	[Q_3Ans] [nvarchar](max) NULL,
	[Q_4Ans] [nvarchar](max) NULL,
	[Q_5Ans] [nvarchar](max) NULL,
	[Q_6Ans] [nvarchar](max) NULL,
	[Q_7Ans] [nvarchar](max) NULL,
	[Q_8Ans] [nvarchar](max) NULL,
	[Q_9Ans] [nvarchar](max) NULL,
	[Q_10Ans] [nvarchar](max) NULL,
	[Q_11Ans] [nvarchar](max) NULL,
	[Q_12Ans] [nvarchar](max) NULL,
	[Q_13Ans] [nvarchar](max) NULL,
	[Q_14Ans] [nvarchar](max) NULL,
	[Q_15Ans] [nvarchar](max) NULL,
	[Q_16Ans] [nvarchar](max) NULL,
	[Q_17Ans] [nvarchar](max) NULL,
	[Q_18Ans] [nvarchar](max) NULL,
	[Q_19Ans] [nvarchar](max) NULL,
	[Q_20Ans] [nvarchar](max) NULL,
	[Q_21Ans] [nvarchar](max) NULL,
	[Q_22Ans] [nvarchar](max) NULL,
	[Q_23Ans] [nvarchar](max) NULL,
	[Q_24Ans] [nvarchar](max) NULL,
	[Q_25Ans] [nvarchar](max) NULL,
	[Q_26Ans] [nvarchar](max) NULL,
	[Q_27Ans] [nvarchar](max) NULL,
	[Q_28Ans] [nvarchar](max) NULL,
	[Q_29Ans] [nvarchar](max) NULL,
	[Q_30Ans] [nvarchar](max) NULL,
	[Q_31Ans] [nvarchar](max) NULL,
	[Q_32Ans] [nvarchar](max) NULL,
	[Q_33Ans] [nvarchar](max) NULL,
	[Q_34Ans] [nvarchar](max) NULL,
	[Q_35Ans] [nvarchar](max) NULL,
	[Q_36Ans] [nvarchar](max) NULL,
	[Q_37Ans] [nvarchar](max) NULL,
	[Q_38Ans] [nvarchar](max) NULL,
	[Q_39Ans] [nvarchar](max) NULL,
	[Q_40Ans] [nvarchar](max) NULL,
	[Q_41Ans] [nvarchar](max) NULL,
	[Q_42Ans] [nvarchar](max) NULL,
	[Q_43Ans] [nvarchar](max) NULL,
	[Q_44Ans] [nvarchar](max) NULL,
	[Q_45Ans] [nvarchar](max) NULL,
	[Q_46Ans] [nvarchar](max) NULL,
	[Q_47Ans] [nvarchar](max) NULL,
	[Q_48Ans] [nvarchar](max) NULL,
	[Q_49Ans] [nvarchar](max) NULL,
	[Q_50Ans] [nvarchar](max) NULL,
	[Q_51Ans] [nvarchar](max) NULL,
	[Q_52Ans] [nvarchar](max) NULL,
	[Q_53Ans] [nvarchar](max) NULL,
	[Q_54Ans] [nvarchar](max) NULL,
	[Q_55Ans] [nvarchar](max) NULL,
	[Q_56Ans] [nvarchar](max) NULL,
	[Q_57Ans] [nvarchar](max) NULL,
	[Q_58Ans] [nvarchar](max) NULL,
	[Q_59Ans] [nvarchar](max) NULL,
	[Q_60Ans] [nvarchar](max) NULL,
	[Q_61Ans] [nvarchar](max) NULL,
	[Q_62Ans] [nvarchar](max) NULL,
	[Q_63Ans] [nvarchar](max) NULL,
	[Q_64Ans] [nvarchar](max) NULL,
	[Q_65Ans] [nvarchar](max) NULL,
	[Q_66Ans] [nvarchar](max) NULL,
	[Q_67Ans] [nvarchar](max) NULL,
	[Q_68Ans] [nvarchar](max) NULL,
	[Q_69Ans] [nvarchar](max) NULL,
	[Q_70Ans] [nvarchar](max) NULL,
	[Q_71Ans] [nvarchar](max) NULL,
	[Q_72Ans] [nvarchar](max) NULL,
	[Q_73Ans] [nvarchar](max) NULL,
	[Q_74Ans] [nvarchar](max) NULL,
	[Q_75Ans] [nvarchar](max) NULL,
	[Q_76Ans] [nvarchar](max) NULL,
	[Q_77Ans] [nvarchar](max) NULL,
	[Q_78Ans] [nvarchar](max) NULL,
	[Q_79Ans] [nvarchar](max) NULL,
	[Q_80Ans] [nvarchar](max) NULL,
	[Q_81Ans] [nvarchar](max) NULL,
	[Q_82Ans] [nvarchar](max) NULL,
	[Q_83Ans] [nvarchar](max) NULL,
	[Q_84Ans] [nvarchar](max) NULL,
	[Q_85Ans] [nvarchar](max) NULL,
	[Q_86Ans] [nvarchar](max) NULL,
	[Q_87Ans] [nvarchar](max) NULL,
	[Q_88Ans] [nvarchar](max) NULL,
	[Q_89Ans] [nvarchar](max) NULL,
	[Q_90Ans] [nvarchar](max) NULL,
	[Q_91Ans] [nvarchar](max) NULL,
	[Q_92Ans] [nvarchar](max) NULL,
	[Q_93Ans] [nvarchar](max) NULL,
	[Q_94Ans] [nvarchar](max) NULL,
	[Q_95Ans] [nvarchar](max) NULL,
	[Q_96Ans] [nvarchar](max) NULL,
	[Q_97Ans] [nvarchar](max) NULL,
	[Q_98Ans] [nvarchar](max) NULL,
	[Q_99Ans] [nvarchar](max) NULL,
	[Q_100Ans] [nvarchar](max) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tbl_Final_Ans_Bk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Final_Ans_BK]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Final_Ans_BK](
	[Index_No] [nvarchar](50) NULL,
	[Seat_No] [nvarchar](50) NULL,
	[datetime] [datetime] NULL,
	[Ip] [nvarchar](50) NULL,
	[Paper_ID] [nvarchar](max) NULL,
	[Q1_Ans] [nvarchar](max) NULL,
	[Q_2Ans] [nvarchar](max) NULL,
	[Q_3Ans] [nvarchar](max) NULL,
	[Q_4Ans] [nvarchar](max) NULL,
	[Q_5Ans] [nvarchar](max) NULL,
	[Q_6Ans] [nvarchar](max) NULL,
	[Q_7Ans] [nvarchar](max) NULL,
	[Q_8Ans] [nvarchar](max) NULL,
	[Q_9Ans] [nvarchar](max) NULL,
	[Q_10Ans] [nvarchar](max) NULL,
	[Q_11Ans] [nvarchar](max) NULL,
	[Q_12Ans] [nvarchar](max) NULL,
	[Q_13Ans] [nvarchar](max) NULL,
	[Q_14Ans] [nvarchar](max) NULL,
	[Q_15Ans] [nvarchar](max) NULL,
	[Q_16Ans] [nvarchar](max) NULL,
	[Q_17Ans] [nvarchar](max) NULL,
	[Q_18Ans] [nvarchar](max) NULL,
	[Q_19Ans] [nvarchar](max) NULL,
	[Q_20Ans] [nvarchar](max) NULL,
	[Q_21Ans] [nvarchar](max) NULL,
	[Q_22Ans] [nvarchar](max) NULL,
	[Q_23Ans] [nvarchar](max) NULL,
	[Q_24Ans] [nvarchar](max) NULL,
	[Q_25Ans] [nvarchar](max) NULL,
	[Q_26Ans] [nvarchar](max) NULL,
	[Q_27Ans] [nvarchar](max) NULL,
	[Q_28Ans] [nvarchar](max) NULL,
	[Q_29Ans] [nvarchar](max) NULL,
	[Q_30Ans] [nvarchar](max) NULL,
	[Q_31Ans] [nvarchar](max) NULL,
	[Q_32Ans] [nvarchar](max) NULL,
	[Q_33Ans] [nvarchar](max) NULL,
	[Q_34Ans] [nvarchar](max) NULL,
	[Q_35Ans] [nvarchar](max) NULL,
	[Q_36Ans] [nvarchar](max) NULL,
	[Q_37Ans] [nvarchar](max) NULL,
	[Q_38Ans] [nvarchar](max) NULL,
	[Q_39Ans] [nvarchar](max) NULL,
	[Q_40Ans] [nvarchar](max) NULL,
	[Q_41Ans] [nvarchar](max) NULL,
	[Q_42Ans] [nvarchar](max) NULL,
	[Q_43Ans] [nvarchar](max) NULL,
	[Q_44Ans] [nvarchar](max) NULL,
	[Q_45Ans] [nvarchar](max) NULL,
	[Q_46Ans] [nvarchar](max) NULL,
	[Q_47Ans] [nvarchar](max) NULL,
	[Q_48Ans] [nvarchar](max) NULL,
	[Q_49Ans] [nvarchar](max) NULL,
	[Q_50Ans] [nvarchar](max) NULL,
	[Q_51Ans] [nvarchar](max) NULL,
	[Q_52Ans] [nvarchar](max) NULL,
	[Q_53Ans] [nvarchar](max) NULL,
	[Q_54Ans] [nvarchar](max) NULL,
	[Q_55Ans] [nvarchar](max) NULL,
	[Q_56Ans] [nvarchar](max) NULL,
	[Q_57Ans] [nvarchar](max) NULL,
	[Q_58Ans] [nvarchar](max) NULL,
	[Q_59Ans] [nvarchar](max) NULL,
	[Q_60Ans] [nvarchar](max) NULL,
	[Q_61Ans] [nvarchar](max) NULL,
	[Q_62Ans] [nvarchar](max) NULL,
	[Q_63Ans] [nvarchar](max) NULL,
	[Q_64Ans] [nvarchar](max) NULL,
	[Q_65Ans] [nvarchar](max) NULL,
	[Q_66Ans] [nvarchar](max) NULL,
	[Q_67Ans] [nvarchar](max) NULL,
	[Q_68Ans] [nvarchar](max) NULL,
	[Q_69Ans] [nvarchar](max) NULL,
	[Q_70Ans] [nvarchar](max) NULL,
	[Q_71Ans] [nvarchar](max) NULL,
	[Q_72Ans] [nvarchar](max) NULL,
	[Q_73Ans] [nvarchar](max) NULL,
	[Q_74Ans] [nvarchar](max) NULL,
	[Q_75Ans] [nvarchar](max) NULL,
	[Q_76Ans] [nvarchar](max) NULL,
	[Q_77Ans] [nvarchar](max) NULL,
	[Q_78Ans] [nvarchar](max) NULL,
	[Q_79Ans] [nvarchar](max) NULL,
	[Q_80Ans] [nvarchar](max) NULL,
	[Q_81Ans] [nvarchar](max) NULL,
	[Q_82Ans] [nvarchar](max) NULL,
	[Q_83Ans] [nvarchar](max) NULL,
	[Q_84Ans] [nvarchar](max) NULL,
	[Q_85Ans] [nvarchar](max) NULL,
	[Q_86Ans] [nvarchar](max) NULL,
	[Q_87Ans] [nvarchar](max) NULL,
	[Q_88Ans] [nvarchar](max) NULL,
	[Q_89Ans] [nvarchar](max) NULL,
	[Q_90Ans] [nvarchar](max) NULL,
	[Q_91Ans] [nvarchar](max) NULL,
	[Q_92Ans] [nvarchar](max) NULL,
	[Q_93Ans] [nvarchar](max) NULL,
	[Q_94Ans] [nvarchar](max) NULL,
	[Q_95Ans] [nvarchar](max) NULL,
	[Q_96Ans] [nvarchar](max) NULL,
	[Q_97Ans] [nvarchar](max) NULL,
	[Q_98Ans] [nvarchar](max) NULL,
	[Q_99Ans] [nvarchar](max) NULL,
	[Q_100Ans] [nvarchar](max) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tbl_Final_Ans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Inspection]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Inspection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Time] [smalldatetime] NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[SYS_No] [nvarchar](max) NOT NULL,
	[OS_Name] [nvarchar](max) NOT NULL,
	[Ram] [nvarchar](max) NOT NULL,
	[HDD] [nvarchar](max) NOT NULL,
	[MAC] [nvarchar](max) NOT NULL,
	[Browser_Name] [nvarchar](max) NOT NULL,
	[Extn_IP] [nvarchar](max) NOT NULL,
	[Screen_Res] [nvarchar](max) NOT NULL,
	[IE_Version] [nvarchar](max) NOT NULL,
	[Active] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_Inspection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Inspection_Basic_Info]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Inspection_Basic_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IP_Address] [nvarchar](50) NOT NULL,
	[Date_Time] [datetime] NOT NULL,
	[Index_No] [nvarchar](50) NOT NULL,
	[College_Name] [nvarchar](max) NOT NULL,
	[Teacher_Name] [nvarchar](max) NOT NULL,
	[Contact_No] [nvarchar](max) NOT NULL,
	[No_Of_Student] [int] NOT NULL,
	[No_Of_System] [int] NOT NULL,
	[Active] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_Inspection_Basic_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_ITCOLLEGELIST]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_ITCOLLEGELIST](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](255) NULL,
	[statusadd] [nvarchar](255) NULL,
	[pass_sts] [nvarchar](255) NULL,
 CONSTRAINT [PK_Tbl_ITCOLLEGELIST_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Login]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NULL,
	[Seat_No] [nvarchar](7) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Mother_Name] [nvarchar](50) NULL,
	[Batch] [nvarchar](2) NULL,
	[Stream] [nvarchar](2) NULL,
	[Div] [int] NULL,
	[Paper_ID] [nvarchar](4) NULL,
	[Hand] [nvarchar](2) NULL,
	[Reschedule_Status] [int] NULL,
	[Reschedule_Batch] [nvarchar](10) NULL,
	[Reschedule_Paper_ID] [nvarchar](10) NULL,
	[Reschedule_Approve_BY] [nvarchar](max) NULL,
	[Course] [nvarchar](max) NULL,
	[Coll_Add_Student] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Login_1] PRIMARY KEY CLUSTERED 
(
	[Seat_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Login1]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Login1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Index_No] [nchar](7) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Login1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Mac]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Mac](
	[ID] [int] NOT NULL,
	[Index_No] [nvarchar](7) NULL,
	[Mac_ID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_mac] PRIMARY KEY CLUSTERED 
(
	[Mac_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Password]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Password](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](50) NOT NULL,
	[B1] [nvarchar](50) NULL,
	[B2] [nvarchar](50) NULL,
	[B3] [nvarchar](50) NULL,
	[B4] [nvarchar](50) NULL,
	[B5] [nvarchar](50) NULL,
	[B6] [nvarchar](50) NULL,
	[B7] [nvarchar](50) NULL,
	[Div_Code] [int] NULL,
	[Coordinator1_Name] [nvarchar](500) NULL,
	[Coordinator1_Mobile] [nvarchar](50) NULL,
	[Coordinator2_Name] [nvarchar](500) NULL,
	[Coordinator2_Mobile] [nvarchar](50) NULL,
	[Index_Print_Status] [int] NULL,
	[Batch_Print_Status] [int] NULL,
 CONSTRAINT [PK_Tbl_Password_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Reschedule]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Reschedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](max) NULL,
	[Seat_No] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Batch] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Reschedule_Batch] [nvarchar](max) NULL,
	[Approved_By_Division] [nvarchar](max) NULL,
	[Approved_By_OLE] [nvarchar](max) NULL,
	[Letter] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Reschedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Reschedule_ApproveBy_Division]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Reschedule_ApproveBy_Division](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[Division_Code] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Reschedule_ApproveBy_Division] PRIMARY KEY CLUSTERED 
(
	[Index_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Reschedule_College]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Reschedule_College](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[Division_Code] [nvarchar](1) NULL,
	[Date_Time] [datetime] NULL,
	[IP_Adress] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Reschedule_College] PRIMARY KEY CLUSTERED 
(
	[Index_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Reschedule_Student]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Reschedule_Student](
	[ID] [int] NOT NULL,
	[Record_ID] [int] NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[Seat_No] [nvarchar](7) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Initial_Batch] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Reschedule_Batch] [nvarchar](max) NULL,
	[Approved_By_Division] [nvarchar](max) NULL,
	[Approved_By_OLE] [nvarchar](max) NULL,
	[Division_Code] [nvarchar](1) NULL,
 CONSTRAINT [PK_Tbl_Reschedule_Student] PRIMARY KEY CLUSTERED 
(
	[Seat_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_State_Co_Ordinator_Registration]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_State_Co_Ordinator_Registration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Index_No] [nvarchar](7) NOT NULL,
	[College_Name] [nvarchar](max) NULL,
	[College_Address] [nvarchar](max) NULL,
	[Coordinator_Name] [nvarchar](max) NULL,
	[Coordinator_Mobile] [nvarchar](max) NULL,
	[Coordinator_Email] [nvarchar](max) NULL,
	[Coordinator_Eduction] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Confirm_Password] [nvarchar](max) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Tbl_State_Co_Ordinator_Registration_1] PRIMARY KEY CLUSTERED 
(
	[Index_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Student]    Script Date: 23-02-2023 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[seat] [nvarchar](255) NULL,
	[div] [nvarchar](255) NULL,
	[sch] [nvarchar](255) NULL,
	[srno] [nvarchar](255) NULL,
	[stream] [nvarchar](255) NULL,
	[cent] [nvarchar](255) NULL,
	[hand] [nvarchar](255) NULL,
	[fresh_rpt] [nvarchar](255) NULL,
	[reg_rpt] [nvarchar](255) NULL,
	[medium] [nvarchar](255) NULL,
	[cname] [nvarchar](255) NULL,
	[mother] [nvarchar](255) NULL,
	[sub2] [nvarchar](255) NULL,
 CONSTRAINT [PK_Tbl_Student] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [GKJ_2022] SET  READ_WRITE 
GO
