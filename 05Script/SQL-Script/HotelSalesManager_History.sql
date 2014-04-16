USE [CMS]
GO

/****** Object:  Table [dbo].[HotelSalesManager_History]    Script Date: 07/12/2012 16:23:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HotelSalesManager_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[User_Account] [nvarchar](100) NULL,
	[Hotel_ID] [nvarchar](100) NULL,
	[Start_Dtime] [date] NULL,
	[End_Dtime] [date] NULL,
	[Fax] [nvarchar](100) NULL,
	[Per] [nvarchar](100) NULL,
	[Tel] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
 CONSTRAINT [pk_HotelSalesManager_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


