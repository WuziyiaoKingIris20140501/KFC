USE [CMS]
GO

/****** Object:  Table [dbo].[WebSiteEvent_History]    Script Date: 11/30/2011 19:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebSiteEvent_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[USERID] [nvarchar](50) NULL,
	[USERNAME] [nvarchar](255) NULL,
	[IPADDRESS] [nvarchar](50) NULL,
	[CREATEDATE] [datetime] NULL,
	[EVENT_TYPE] [nvarchar](255) NULL,
	[EVENT_ID] [nvarchar](50) NULL,
	[EVENT_CONTENT] [nvarchar](max) NULL,
	[EVENT_RESULT] [nvarchar](max) NULL,
 CONSTRAINT [pk_WebSiteEvent_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


