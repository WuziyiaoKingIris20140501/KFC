USE [CMS]
GO

/****** Object:  Table [dbo].[LmOrderAction_History]    Script Date: 08/06/2013 16:33:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LmOrderAction_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[EVENT_TYPE] [nvarchar](255) NULL,
	[EVENT_FG_ID] [nvarchar](50) NULL,
	[ACTION_ID] [nvarchar](50) NULL,
	[OD_STATUS] [nvarchar](10) NULL,
	[REMARK] [nvarchar](2000) NULL,
	[CANNEL] [nvarchar](2000) NULL,
	[EVENT_USER] [nvarchar](50) NULL,
	[EVENT_TIME] [datetime] NULL,
	[APPROVE_ID] [nvarchar](50) NULL,
	[ROOM_ID] [nvarchar](50) NULL,
	[OperateResult] [nvarchar](50) NULL,
	[ISDBAPPROVE] [nvarchar](2) NULL,
 CONSTRAINT [pk_LmOrderAction_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE INDEX [idx_LmOrderAction_History_EVENT_FG_ID] ON dbo.LmOrderAction_History (EVENT_FG_ID);
CREATE INDEX [idx_LmOrderAction_History_EVENT_USER] ON dbo.LmOrderAction_History (EVENT_USER);
CREATE INDEX [idx_LmOrderAction_History_CREATEDATE] ON dbo.LmOrderAction_History (EVENT_TIME);

go
