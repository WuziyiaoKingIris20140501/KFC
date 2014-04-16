USE [CMS]
GO

/****** Object:  Table [dbo].[HotelSalesPlanManager]    Script Date: 05/14/2012 15:50:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HotelSalesPlanManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](1) NOT NULL,
	[Plan_Time] [time](7) NULL,
	[Plan_DTime] [datetime] NULL,
	[Start_Dtime] [date] NULL,
	[End_Dtime] [date] NULL,
	[Week_List] [nvarchar](100) NULL,
	[Status] [nvarchar](1) NOT NULL,
	[Action] [nvarchar](3) NOT NULL,
	[Result] [nvarchar](3000) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
	[Action_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelSalesPlanManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


