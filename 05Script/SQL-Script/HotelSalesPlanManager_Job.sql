USE [CMS]
GO

/****** Object:  Table [dbo].[HotelSalesPlanManager_Job]    Script Date: 06/01/2012 10:54:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HotelSalesPlanManager_Job](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[HPID] [int] NOT NULL,
	[Type] [nvarchar](1) NOT NULL,
	[Plan_DTime] [datetime] NULL,
	[Status] [nvarchar](1) NOT NULL,
	[Action] [nvarchar](1) NOT NULL,
	[Action_Time] [datetime] NULL,
	[Result] [nvarchar](500) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelSalesPlanManager_Job] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO