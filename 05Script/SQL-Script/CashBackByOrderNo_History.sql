USE [CMS]
GO

/****** Object:  Table [dbo].[CashBackByOrderNo_History]    Script Date: 07/19/2012 17:21:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CashBackByOrderNo_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Hotel_ID] [nvarchar](100) NULL,
	[Order_No] [nvarchar](100) NULL,
	[User_ID] [nvarchar](100) NULL,
	[Cash_SN] [nvarchar](100) NULL,
	[Status] [nvarchar](1) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_CashBackByOrderNo_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


