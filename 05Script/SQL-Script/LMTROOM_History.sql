USE [CMS]
GO

/****** Object:  Table [dbo].[LMTROOM_History]    Script Date: 01/30/2013 12:01:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LMTROOM_History](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[HOTEL_ID] [nvarchar](40) NULL,
	[ROOM_CODE] [nvarchar](40) NULL,
	[ROOM_NAME] [nvarchar](100) NULL,
	[ROOM_DESC] [nvarchar](200) NULL,
	[BED_TYPE] [nvarchar](100) NULL,
	[MAX_GUEST] [nvarchar](2) NULL,
	[IS_WINDOW] [nvarchar](1) NULL,
	[GUEST_TYPE] [nvarchar](1) NULL,
	[FLOOR] [nvarchar](100) NULL,
	[NETWORK] [nvarchar](100) NULL,
	[ROOM_AREA] [nvarchar](100) NULL,
	[IS_NOSMOKE] [nvarchar](1) NULL,
	[STATUS] [nvarchar](2) NULL,
	[CREATE_TIME] [datetime] NULL,
	[CREATE_USER] [nvarchar](100) NULL,
	
 CONSTRAINT [pk_LMTROOM_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

