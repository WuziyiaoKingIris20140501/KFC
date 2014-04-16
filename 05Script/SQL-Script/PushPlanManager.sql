USE [CMS]
GO

/****** Object:  Table [dbo].[PushPlanManager]    Script Date: 07/02/2012 17:59:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PushPlanManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](1) NOT NULL,			-- 0:����  1:�û���  2:�ļ��ϴ�
	[Push_Title] [nvarchar](4000) NULL,
	[Push_Content] [nvarchar](4000) NULL,
	[Push_ProtoType] [nvarchar](4000) NULL,	-- �û�ʵ������  ѡ���û�������ļ��ϴ�
	[Push_Users] [nvarchar](4000) NULL,
	[Status] [nvarchar](1) NOT NULL,		-- 0:����  1:������  2:������  3:���
	[All_Count] [numeric](18, 0) NULL,
	[Suc_Count] [numeric](18, 0) NULL,
	[Action_Time] [datetime] NULL,			-- �������ʱ��
	[Result] [nvarchar](500) NULL,
	[Create_User] [nvarchar](100) NULL,
	[Create_Time] [datetime] NULL,
	[Update_User] [nvarchar](100) NULL,
	[Update_Time] [datetime] NULL,
 CONSTRAINT [pk_PushPlanManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


