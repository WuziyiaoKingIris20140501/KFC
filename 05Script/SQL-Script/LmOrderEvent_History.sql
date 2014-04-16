USE [CMS]
GO

/****** Object:  Table [dbo].[LmOrderEvent_History]    Script Date: 02/03/2012 10:31:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LmOrderEvent_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[EVENT_TYPE] [nvarchar](255) NULL,
	[EVENT_LM_ID] [nvarchar](50) NULL,
	[EVENT_ID] [nvarchar](50) NULL,
	[BOOK_STATUS] [nvarchar](10) NULL,
	[PAY_STATUS] [nvarchar](10) NULL,
	[BOOK_SOURCE] [nvarchar](255) NULL,
	[LOGIN_MOBILE] [nvarchar](50) NULL,
	[EVENT_CONTENT] [nvarchar](max) NULL,
	[EVENT_RESULT] [nvarchar](max) NULL,
	[LMLOG_ID] [nvarchar](50) NULL,
	[EVENT_TIME] [datetime] NULL,
 CONSTRAINT [pk_LmOrderEvent_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO