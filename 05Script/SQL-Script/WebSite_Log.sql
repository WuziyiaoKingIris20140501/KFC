USE [CMS]
GO

/****** Object:  Table [dbo].[WebSite_log]    Script Date: 11/08/2011 10:36:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WebSite_log](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[DATETIME] [datetime] NULL,
	[LOG_LEVEL] [varchar](255) NULL,
	[LOGGER] [varchar](255) NULL,
	[COMPUTERNAME] [varchar](255) NULL,
	[IPADDRESS] [varchar](50) NULL,
	[EVENT_ID] [varchar](50) NULL,
	[CONTENT] [nvarchar](max) NULL,
	[USERID] [varchar](50) NULL,
	[USERNAME] [varchar](255) NULL,
 CONSTRAINT [pk_WebSite_log] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


