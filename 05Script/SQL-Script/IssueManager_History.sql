USE [CMS]
GO

/****** Object:  Table [dbo].[IssueManager_History]    Script Date: 08/30/2012 13:51:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IssueManager_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[IssueID] [int] NOT NULL,
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
	[ChkAssginMsg] [nvarchar](1) NULL,
	[AssginMsg] [nvarchar](200) NULL,
	[AssginMsgRS] [nvarchar](100) NULL,
	[ChkUserMsg] [nvarchar](1) NULL,
	[UserMsg] [nvarchar](200) NULL,
	[UserMsgRS] [nvarchar](100) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL
 CONSTRAINT [pk_IssueManager_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


