USE [CMS]
GO

/****** Object:  Table [dbo].[WebSite_SysConfiguration]    Script Date: 11/18/2011 13:19:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WebSite_SysConfiguration](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[DATETIME] [datetime] NULL,
	[Type] [varchar](50) NULL,
	[Key] [varchar](50) NULL,
	[Value] [nvarchar](1000) NULL,
	[USERID] [varchar](50) NULL,
 CONSTRAINT [pk_WebSite_SysConfiguration_log] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'online'
           ,'0'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'online'
           ,'1'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'bandtype'
           ,'B'
           ,'��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'bandtype'
           ,'H'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'bandtype'
           ,'J'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starrating'
           ,'0'
           ,'��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starrating'
           ,'1'
           ,'1��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starrating'
           ,'2'
           ,'2��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starrating'
           ,'3'
           ,'3��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starrating'
           ,'4'
           ,'4��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starrating'
           ,'5'
           ,'5��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'diamondrating'
           ,'0'
           ,'��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'diamondrating'
           ,'1'
           ,'׼1��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'diamondrating'
           ,'2'
           ,'׼2��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'diamondrating'
           ,'3'
           ,'׼3��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'diamondrating'
           ,'4'
           ,'׼4��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'diamondrating'
           ,'5'
           ,'׼5��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelstatus'
           ,'new'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelstatus'
           ,'active'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelstatus'
           ,'inactive'
           ,'��ʱ�ر�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelstatus'
           ,'close'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'priority'
           ,'1'
           ,'1'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'priority'
           ,'2'
           ,'2'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'priority'
           ,'3'
           ,'3'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'priority'
           ,'4'
           ,'4'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'priority'
           ,'5'
           ,'5'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'pricetype'
           ,''
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'pricetype'
           ,'LMBAR'
           ,'[LMBAR]Ԥ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'pricetype'
           ,'LMBAR2'
           ,'[LMBAR2]�ָ�'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'invoicestatus'
           ,'0'
           ,'δ����'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'invoicestatus'
           ,'1'
           ,'������'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'invoicestatus'
           ,'2'
           ,'�ѿ���'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'invoicestatus'
           ,'3'
           ,'���ʼ�'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'applychanel'
           ,'0'
           ,'�ͷ��绰'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'applychanel'
           ,'1'
           ,'�ֻ��ͻ���'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'applisttype'
           ,'0'
           ,'��ҹ�б�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'applisttype'
           ,'1'
           ,'��ҹ�б�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'HOTELID'
           ,'�Ƶ�ID'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'HOTELNM'
           ,'�Ƶ�����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'ADDRESS'
           ,'�Ƶ��ַ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'LGLTTUDE'
           ,'�Ƶ꾭γ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'HOTELDES'
           ,'�Ƶ�ſ�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'HOTELAPPR'
           ,'С��ʿ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'HOTELSERVICE'
           ,'�Ƶ������Ϣ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'BUSSES'
           ,'������ʩ��Ϣ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'TRADEAREA'
           ,'��Ȧ��Ϣ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'LINKTEL'
           ,'�Ƶ�Ԥ���绰'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'LINKFAX'
           ,'�Ƶ�Ԥ������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'HTIMAGE'
           ,'ͼƬ��Ϣ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'ROOMNM'
           ,'��������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'ROOMCODE'
           ,'���ʹ���'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'BEDNM'
           ,'��������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'NETPRICE'
           ,'�����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'VPPRICE'
           ,'LM�۸�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'appignore'
           ,'SALES'
           ,'�Ƶ�������Ա'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'HOTELNMZH'
           ,'�Ƶ�����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'HOTELNMEN'
           ,'�Ƶ�Ӣ������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'FOGSTATUS'
           ,'FOG�Ƶ�������״̬'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'CITY'
           ,'�Ƶ����ڳ���'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'DIAMOND'
           ,'�Ƶ���ʯ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'STAR'
           ,'�Ƶ��Ǽ�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'OPENDT'
           ,'��ҵ����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'RENOVATIONDT'
           ,'װ������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'TRADEAREA'
           ,'�Ƶ���Ȧ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'ADDRESS'
           ,'�Ƶ��ַ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'WEBSITE'
           ,'�Ƶ���ַ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'LINKTEL'
           ,'Ԥ���绰'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'LINKFAX'
           ,'Ԥ������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'LINKMAN'
           ,'��ϵ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'LINKMAIL'
           ,'��ϵ����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'LONGITUDE'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'LATITUDE'
           ,'γ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'hotelcheck'
           ,'DESCZH'
           ,'�Ƶ�����'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'1'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'2'
           ,'����һ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'3'
           ,'���ڶ�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'4'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'5'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'6'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'weektype'
           ,'7'
           ,'������'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'roomstatus'
           ,'true'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'roomstatus'
           ,'false'
           ,'�ر�'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,''
           ,'���ı�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'0'
           ,'0'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'1'
           ,'1'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'2'
           ,'2'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'3'
           ,'3'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'4'
           ,'4'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'5'
           ,'5'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'6'
           ,'6'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'7'
           ,'7'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'8'
           ,'8'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'breakfastnum'
           ,'9'
           ,'9'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isnetwork'
           ,''
           ,'���ı�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isnetwork'
           ,'true'
           ,'��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isnetwork'
           ,'false'
           ,'����'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'offsetunit'
           ,'0'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'offsetunit'
           ,'1'
           ,'�ٷ���'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isreserve'
           ,'0'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isreserve'
           ,'1'
           ,'�Ǳ�����'
           ,'System')
GO


INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'savetype'
           ,'0'
           ,'��������'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'savetype'
           ,'1'
           ,'��ʱ����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'savetype'
           ,'2'
           ,'ÿ���Զ�����'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'planstatus'
           ,'0'
           ,'ִֹͣ��'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'planstatus'
           ,'1'
           ,'�Զ�ִ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'planstatus'
           ,'2'
           ,'���ִ��'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH18','RH18-�������18:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH','RH-Ԥ��������18:00��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH17','RH17-�������17:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CP100','CP100-��Ҫ����ȫ�����ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH19','RH19-�������19:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH14','RH14-�������14:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH13','RH13-�������13:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH16','RH16-�������16:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CC100','CC100-��Ҫ��Ч���ÿ�����ȫ�����ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH15','RH15-�������15:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH12','RH12-�����������12:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH2359','RH2359-�������23:59����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','JJPFP','JJPFP-ͨ������+��Ա���ֶһ��˴�ס�ޡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','TP','TP-����IDS������Ԥ���������½�Э����㡣','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','EP1','EP1-����Ϊȫ��Ԥ�������ɻ�ͨ������Ƶ갴�½�Э����㣬�۸�������Ѽ�����ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH04','RH04-���ٱ�������Сʱ������ȡ��������ǰ֪ͨ��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH24','RH24-','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH23','RH23-�������23:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH22','RH22-�������22:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH21','RH21-����21:00����ס����Ҫ����������','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH20','RH20-�������20:00����ס����Ҫ���������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CGT','CGT-���������������ṩ���ÿ����������ÿ����ϱ��ܡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','EP','EP-����Ϊȫ��Ԥ�������ɻ�ͨ��������ס��ǰ֧�����Ƶ꣬�۸�������Ѽ�����ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','AA','AA-���Ա�֤��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','PP','PP-��ҪԤ��100%�ܷ��ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CC','CC-��Ҫ��Ч���ÿ����������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH22D100','RH22D100-�������22:00����ס����Ҫ���ÿ�����ȫ��ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CPH','CPH-��Ҫ��Ч���ÿ�Ԥ��100%�ܷ��ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CPC','CPC-��������VCardԤ����','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CP','CP-��Ҫ���������ѡ�','System')

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D1T6','RN1D1T6-������סǰ1���18:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D3T24','RN1D3T24-������סǰ3���24:00֮ǰȡ��������ȡ������֧��һ���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN2D1','RN2D1-������סǰ2���18:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1T6','RN1T6-������ס����18:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1T12','RN1T12-������ס��������12:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','PT100','PT100-����ȡ������֧��ȫ��ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D12T24','RN1D12T24-������סǰ12���24:00֮ǰȡ��������ȡ������֧��һ���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','PT2P100','PT2P100-������סǰ2���23:59֮ǰȡ��������ȡ������֧��ȫ��ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1','RN1-����ȡ������֧�������ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D7T24','RN1D7T24-������סǰ7���24:00֮ǰȡ��������ȡ������֧��һ���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP','NP-������ס����18:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','PT3P100','PT3P100-������סǰ3���18:00֮ǰȡ��������ȡ������֧��ȫ��ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','TEST','TEST-������ס���ڵ�ǰ1���0:00ȡ��,��ʱ��֧���ܷ��ѵ�100%','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','BB','BB-����ȡ����','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','JJPT1P100','JJPT1P100-������סǰ1���23:59֮ǰȡ��������ȡ����Ӧ��δ�������ֲ��˻���','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D1','RN1D1-������סǰ1���23:59֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP16','NP16-������ס����16:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D3T6','RN1D3T6-������סǰ3���18:00֮ǰȡ��������ȡ������֧��һ���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D1T12','RN1D1T12-������סǰ1�������12:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP24','NP24-ȡ���޷���','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP22','NP22-������ס����22:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP20','NP20-������ס����20:00֮ǰȡ��������ȡ������֧��1���ѡ�','System')

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isspriority'
           ,'0'
           ,'0'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isspriority'
           ,'1'
           ,'1'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isspriority'
           ,'2'
           ,'2'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isspriority'
           ,'3'
           ,'3'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'isspriority'
           ,'4'
           ,'4'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'0'
           ,'�Ƶ�����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'1'
           ,'����״̬����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'2'
           ,'����֧������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'3'
           ,'������������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'4'
           ,'�Ż�ȯ����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'5'
           ,'��Ʊ����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'6'
           ,'�û�����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'7'
           ,'��������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuetype'
           ,'8'
           ,'�����������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuestatus'
           ,'0'
           ,'�½�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuestatus'
           ,'1'
           ,'������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuestatus'
           ,'2'
           ,'�����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuestatus'
           ,'3'
           ,'��ɾ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuerelated'
           ,'0'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuerelated'
           ,'1'
           ,'�Ƶ�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuerelated'
           ,'2'
           ,'��Ʊ'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuerelated'
           ,'3'
           ,'�û�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuerelated'
           ,'4'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'issuerelated'
           ,'5'
           ,'����'
           ,'System')
GO



INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'0'
           ,'���ṩ�����п����к��˺Ų�ƥ��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'1'
           ,'���ṩ���������ڳ���'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'2'
           ,'���ṩ�����п�����֧�������֧��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'3'
           ,'���ṩ��������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'4'
           ,'���ṩ��֧�����˺�δע��'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'5'
           ,'��֧�����˻�δ����������Ϣ���������֡�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'cdropdown'
           ,'6'
           ,'�����ṩ��������Ϣ�����������ύ�������롣'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (getdate()
           ,'cdropdown'
           ,'7'
           ,'��֧�����˻��ѱ����ᣬ�������Ϣ���������֡�'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (getdate()
           ,'cdropdown'
           ,'8'
           ,'��֧�����˺���Ϣ���������������Ϣ���������֡�'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'2|0'
           ,'������'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'3|0'
           ,'����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'4|0'
           ,'����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'5|0'
           ,'����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'6|0'
           ,'����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'7|0'
           ,'����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'0|3'
           ,'׼����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'0|4'
           ,'׼����'
           ,'System')
GO
  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'starmeth'
           ,'0|5'
           ,'׼����'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,''
           ,'��ѡ��'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'1'
           ,'һ��'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'2'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'3'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'4'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'5'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'6'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'7'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'8'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'9'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'maxguest'
           ,'10'
           ,'ʮ��'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'wlan'
           ,'wifi'
           ,'WIFI'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'wlan'
           ,'����'
           ,'����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'wlan'
           ,'��'
           ,'�޿��'
           ,'System')
GO


  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'guesttype'
           ,'0'
           ,'������'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'guesttype'
           ,'1'
           ,'ֻ���ڱ�'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'guesttype'
           ,'2'
           ,'ֻ�����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'window'
           ,'0'
           ,'�޴�'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'window'
           ,'1'
           ,'�д�'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'window'
           ,'2'
           ,'�����д�'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'smoke'
           ,'0'
           ,'δ����'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'smoke'
           ,'1'
           ,'���̷�'
           ,'System')
GO

  INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'smoke'
           ,'2'
           ,'���̴���'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'BIZ','����󴲷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'STD','��׼��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SD','�󴲷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DXS','�����׷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DQ','�����󴲷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SUT','�����׷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'STD','��׼˫�˷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','�������','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','������׼��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','����˫�˷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SS','�󴲷����ڱ���','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SD','��׼�䣨�ڱ���','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','����˫����','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'FK','�߼��󴲷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'BT','������','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'BT','�����׼��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SD','��׼�󴲷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'ESUT','�׷�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SS','���˼�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'FT','���˼�','System')




INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'0','��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'1','˫��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'2','����������','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'3','Բ��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'4','ˮ��','System')



INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'HUBS1','HUBS1','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'ELONG','����','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'PODINNS','����','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'CTRIP','Я��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'HOMEINNS','���','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'MOTEL168','Ī̩168','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'HVP','�캣·','System')



INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'discount' ,'0','δָ��','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'discount' ,'1','�̶��ۿ�','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'discount' ,'2','�̶��۸�','System')





INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'effhour'
           ,'111100000000000000111111'
           ,'18����������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'effhour'
           ,'111100000000001111111111'
           ,'14����������'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'effhour'
           ,'000000000000001111000000'
           ,'99Ԫ���ƻ�'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'effhour'
           ,'111100000011111111111111'
           ,'10����ʱ����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'effhour'
           ,'99'
           ,'�Զ���ʱ��'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'faxlinktype'
           ,'0'
           ,'��������'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'faxlinktype'
           ,'4'
           ,'��˴���'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'rebindfaxlinktype'
           ,'0'
           ,'����'
           ,'System')
GO
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]
           ([DATETIME]
           ,[Type]
           ,[Key]
           ,[Value]
           ,[USERID])
     VALUES
           (GETDATE()
           ,'rebindfaxlinktype'
           ,'1'
           ,'�����'
           ,'System')
GO