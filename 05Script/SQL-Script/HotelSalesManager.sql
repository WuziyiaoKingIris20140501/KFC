USE [CMS]
GO

/****** Object:  Table [dbo].[HotelSalesManager]    Script Date: 05/07/2012 16:21:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HotelSalesManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[User_Account] [nvarchar](100) NOT NULL,
	[Hotel_ID] [nvarchar](100) NOT NULL,
	[Start_Dtime] [date] NULL,
	[End_Dtime] [date] NULL,
	[Status] [nvarchar](1) NOT NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelSalesManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


