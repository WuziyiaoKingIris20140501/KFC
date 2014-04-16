USE [CMS]
GO

/****** Object:  Table [dbo].[PushPlanAction_History]    Script Date: 07/02/2012 11:44:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PushPlanAction_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[PPID] [int] NOT NULL,
	[TelPhone] [nvarchar](100) NULL,
	[DeviceToken] [nvarchar](200) NULL,
	[Action_Time] [datetime] NULL,
	[Result] [nvarchar](500) NULL
 CONSTRAINT [pk_PushPlanAction_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


