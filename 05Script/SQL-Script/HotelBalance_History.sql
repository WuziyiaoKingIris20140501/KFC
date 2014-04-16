USE [CMS]
GO

/****** Object:  Table [dbo].[HotelBalance_History]    Script Date: 07/16/2012 11:38:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HotelBalance_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Hotel_ID] [nvarchar](100) NULL,
	[PriceCode] [nvarchar](100) NULL,
	[RoomCode] [nvarchar](100) NULL,
	[InDate] [date] NULL,
	[BalType] [nvarchar](100) NULL,
	[BalValue] [nvarchar](100) NULL,
	[Status] [nvarchar](1) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelBalance_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


