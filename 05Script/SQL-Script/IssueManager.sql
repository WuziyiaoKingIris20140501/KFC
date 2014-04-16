USE [CMS]
GO

/****** Object:  Table [dbo].[IssueManager]    Script Date: 08/30/2012 13:51:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IssueManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Priority] [nvarchar](1) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Assignto] [nvarchar](100) NULL,
	[Status] [nvarchar](1) NOT NULL,
	[IsIndemnify] [nvarchar](1) NOT NULL,
	[IndemnifyPrice] [nvarchar](100) NOT NULL,
	[TicketCode] [nvarchar](100) NULL,
	[TicketAmount] [nvarchar](100) NULL,
	[RelatedType] [nvarchar](2) NOT NULL,
	[RelatedID] [nvarchar](100) NULL,
	[Remark] [nvarchar](2000) NULL,
	[TimeDiffTal] [nvarchar](200) NULL,
	[TimeSpans] [decimal](30, 0) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL
 CONSTRAINT [pk_IssueManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


