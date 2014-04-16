USE [CMS]
GO

/****** Object:  Table [dbo].[PushInfoPlanManager]    Script Date: 02/25/2013 16:31:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PushInfoPlanManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](1) NOT NULL,
	[PushType] [nvarchar](1) NOT NULL,
	[Push_Title] [nvarchar](4000) NULL,
	[Push_Content] [nvarchar](4000) NULL,
	[Push_ProtoType] [nvarchar](4000) NULL,
	[Wap_Url] [nvarchar](500) NULL,
	[Push_Users] [nvarchar](4000) NULL,
	[Status] [nvarchar](1) NOT NULL,
	[All_Count] [numeric](18, 0) NULL,
	[Suc_Count] [numeric](18, 0) NULL,
	[Action_Time] [datetime] NULL,
	[Result] [nvarchar](500) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
	[PackageCode] [nvarchar](50) NULL,
	[PackageAmount] [nvarchar](50) NULL,
 CONSTRAINT [pk_PushInfoPlanManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


