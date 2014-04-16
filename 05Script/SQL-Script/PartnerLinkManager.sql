USE [CMS]
GO

/****** Object:  Table [dbo].[PartnerManager_History]    Script Date: 06/19/2012 10:58:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PartnerLinkManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[PartnerID] [nvarchar](255) NULL,
	[PartnerTitle] [nvarchar](255) NULL,
	[PartnerLink] [nvarchar](2000) NULL,
	[ReMark] [nvarchar](500) NULL,
	[Cost] [nvarchar](255) NULL,
	[Hits] [numeric](18,0) NULL,
	[UserID] [nvarchar](255) NULL,
	[CREATETIME] [datetime] NULL,
	[UPDATETIME] [datetime] NULL,
 CONSTRAINT [pk_PartnerLinkManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


