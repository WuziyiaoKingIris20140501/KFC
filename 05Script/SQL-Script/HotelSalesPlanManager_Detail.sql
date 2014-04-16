USE [CMS]
GO

/****** Object:  Table [dbo].[HotelSalesPlanManager_Detail]    Script Date: 05/14/2012 16:06:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HotelSalesPlanManager_Detail](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[HPID] [int] NOT NULL,
	[Start_Dtime] [datetime] NULL,
	[End_Dtime] [datetime] NULL,
	[EFFECT_HOUR_TYPE] [nvarchar](40) ,
	[EFFECT_HOUR] [nvarchar](40) default 111100000000000000111111,
	[Week_List] [nvarchar](100) NULL,
	[HOTEL_ID] [nvarchar](20) NULL,
	[RATE_CODE] [nvarchar](20) NULL,
	[MONEY_TYPE] [nvarchar](3) NULL,
	[GUAID] [nvarchar](40) NULL,
	[CXLID] [nvarchar](40) NULL,
	[ROOM_TYPE_NAME] [nvarchar](100) NULL,
	[ROOM_TYPE_CODE] [nvarchar](50) NULL,
	[STATUS] [nvarchar](10) NULL,
	[ROOM_NUM] [int] NULL,
	[IS_RESERVE] [nvarchar](2) NULL,
	[ONE_PRICE] [decimal](8, 2) NULL,
	[TWO_PRICE] [decimal](8, 2) NULL,
	[THREE_PRICE] [decimal](8, 2) NULL,
	[FOUR_PRICE] [decimal](8, 2) NULL,
	[ATTN_PRICE] [decimal](8, 2) NULL,
	[BREAKFAST_NUM] [decimal](2, 0) NULL,
	[EACH_BREAKFAST_PRICE] [decimal](8, 2) NULL,
	[IS_NETWORK] [nvarchar](10) NULL,
	[OFFSETVAL] [decimal](8, 2) NULL,
	[OFFSETUNIT] [nvarchar](2) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelSalesPlanManager_Detail] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


