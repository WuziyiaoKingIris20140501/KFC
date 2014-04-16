USE [CMS]
GO

/****** Object:  Table [dbo].[HotelComparison]    Script Date: 06/26/2013 14:52:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HotelComparison](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Hotel_ID] [nvarchar](100) NULL,
	[Hotel_Name] [nvarchar](100) NULL,
	[Room_Code] [nvarchar](100) NULL,
    [Room_Name] [nvarchar](100) NULL,
    [Mapping_Hotel] [nvarchar](100) NULL,
    [Mapping_Room] [nvarchar](100) NULL,
    [City_ID] [nvarchar](100) NULL,
    [MPType] [nvarchar](20) NULL,
    [DType] [nvarchar](2) NULL,
    [DValue] [nvarchar](10) NULL,
    [Two_Price] [nvarchar](20) NULL,
    [Mapping_Price] [nvarchar](20) NULL,
    [Act_Price] [nvarchar](20) NULL,
	[Status] [nvarchar](1) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelComparison] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


