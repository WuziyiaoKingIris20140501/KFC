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
           ,'下线'
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
           ,'上线'
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
           ,'无'
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
           ,'豪华型'
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
           ,'经济型'
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
           ,'无'
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
           ,'1星'
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
           ,'2星'
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
           ,'3星'
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
           ,'4星'
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
           ,'5星'
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
           ,'无'
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
           ,'准1星'
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
           ,'准2星'
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
           ,'准3星'
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
           ,'准4星'
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
           ,'准5星'
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
           ,'待上线'
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
           ,'上线'
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
           ,'临时关闭'
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
           ,'下线'
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
           ,'所有'
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
           ,'[LMBAR]预付'
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
           ,'[LMBAR2]现付'
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
           ,'未申请'
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
           ,'已申请'
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
           ,'已开具'
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
           ,'已邮寄'
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
           ,'客服电话'
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
           ,'手机客户端'
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
           ,'昨夜列表'
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
           ,'今夜列表'
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
           ,'酒店ID'
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
           ,'酒店名称'
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
           ,'酒店地址'
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
           ,'酒店经纬度'
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
           ,'酒店概况'
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
           ,'小贴士'
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
           ,'酒店服务信息'
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
           ,'商务设施信息'
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
           ,'商圈信息'
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
           ,'酒店预订电话'
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
           ,'酒店预订传真'
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
           ,'图片信息'
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
           ,'房型名称'
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
           ,'房型代码'
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
           ,'床型名称'
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
           ,'网络价'
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
           ,'LM价格'
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
           ,'酒店销售人员'
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
           ,'酒店名称'
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
           ,'酒店英文名称'
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
           ,'FOG酒店上下线状态'
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
           ,'酒店所在城市'
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
           ,'酒店钻石级'
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
           ,'酒店星级'
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
           ,'开业日期'
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
           ,'装修日期'
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
           ,'酒店商圈'
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
           ,'酒店地址'
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
           ,'酒店网址'
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
           ,'预订电话'
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
           ,'预订传真'
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
           ,'联系人'
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
           ,'联系邮箱'
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
           ,'经度'
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
           ,'纬度'
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
           ,'酒店详情'
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
           ,'星期天'
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
           ,'星期一'
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
           ,'星期二'
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
           ,'星期三'
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
           ,'星期四'
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
           ,'星期五'
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
           ,'星期六'
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
           ,'可用'
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
           ,'关闭'
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
           ,'不改变'
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
           ,'不改变'
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
           ,'含'
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
           ,'不含'
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
           ,'整数'
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
           ,'百分数'
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
           ,'保留房'
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
           ,'非保留房'
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
           ,'立即保存'
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
           ,'定时保存'
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
           ,'每日自动更新'
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
           ,'停止执行'
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
           ,'自动执行'
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
           ,'完成执行'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH18','RH18-如果晚于18:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH','RH-预订保留至18:00。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH17','RH17-如果晚于17:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CP100','CP100-需要担保全部房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH19','RH19-如果晚于19:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH14','RH14-如果晚于14:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH13','RH13-如果晚于13:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH16','RH16-如果晚于16:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CC100','CC100-需要有效信用卡担保全部房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH15','RH15-如果晚于15:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH12','RH12-如果晚于中午12:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH2359','RH2359-如果晚于23:59后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','JJPFP','JJPFP-通过礼享+会员积分兑换此次住宿。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','TP','TP-本单IDS渠道已预付，将按月结协议结算。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','EP1','EP1-本单为全额预付单，由汇通天下与酒店按月结协议结算，价格包括房费及服务费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH04','RH04-至少保留两个小时，如欲取消，需提前通知。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH24','RH24-','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH23','RH23-如果晚于23:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH22','RH22-如果晚于22:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH21','RH21-超过21:00后入住，需要担保首晚房费','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH20','RH20-如果晚于20:00后入住，需要担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CGT','CGT-客人已向海外渠道提供信用卡担保，信用卡资料保密。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','EP','EP-本单为全额预付单，由汇通天下在入住日前支付给酒店，价格包括房费及服务费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','AA','AA-测试保证金','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','PP','PP-需要预付100%总房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CC','CC-需要有效信用卡担保首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','RH22D100','RH22D100-如果晚于22:00后入住，需要信用卡担保全额房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CPH','CPH-需要有效信用卡预付100%总房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CPC','CPC-海外渠道VCard预付。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'guaid','CP','CP-需要担保首晚房费。','System')

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D1T6','RN1D1T6-请在入住前1天的18:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D3T24','RN1D3T24-请在入住前3天的24:00之前取消，逾期取消，需支付一晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN2D1','RN2D1-请在入住前2天的18:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1T6','RN1T6-请在入住当天18:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1T12','RN1T12-请在入住当天中午12:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','PT100','PT100-如欲取消，需支付全额房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D12T24','RN1D12T24-请在入住前12天的24:00之前取消，逾期取消，需支付一晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','PT2P100','PT2P100-请在入住前2天的23:59之前取消，逾期取消，需支付全额房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1','RN1-如欲取消，需支付首晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D7T24','RN1D7T24-请在入住前7天的24:00之前取消，逾期取消，需支付一晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP','NP-请在入住当天18:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','PT3P100','PT3P100-请在入住前3天的18:00之前取消，逾期取消，需支付全额房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','TEST','TEST-请在入住日期的前1天的0:00取消,超时需支付总房费的100%','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','BB','BB-测试取消金','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','JJPT1P100','JJPT1P100-请在入住前1天的23:59之前取消，逾期取消或应到未到，积分不退还。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D1','RN1D1-请在入住前1天的23:59之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP16','NP16-请在入住当天16:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D3T6','RN1D3T6-请在入住前3天的18:00之前取消，逾期取消，需支付一晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','RN1D1T12','RN1D1T12-请在入住前1天的中午12:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP24','NP24-取消无罚金。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP22','NP22-请在入住当天22:00之前取消，逾期取消，需支付1晚房费。','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration]([DATETIME],[Type],[Key],[Value],[USERID]) VALUES (GETDATE(),'cxlid','NP20','NP20-请在入住当天20:00之前取消，逾期取消，需支付1晚房费。','System')

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
           ,'酒店问题'
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
           ,'订单状态问题'
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
           ,'订单支付问题'
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
           ,'订单返现问题'
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
           ,'优惠券问题'
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
           ,'发票问题'
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
           ,'用户问题'
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
           ,'提现问题'
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
           ,'订单审核问题'
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
           ,'新建'
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
           ,'处理中'
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
           ,'已完成'
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
           ,'已删除'
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
           ,'订单'
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
           ,'酒店'
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
           ,'发票'
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
           ,'用户'
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
           ,'提现'
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
           ,'反馈'
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
           ,'您提供的银行开户行和账号不匹配'
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
           ,'请提供开户行所在城市'
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
           ,'您提供的银行开户行支付宝付款不支持'
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
           ,'请提供银行名称'
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
           ,'您提供的支付宝账号未注册'
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
           ,'您支付宝账户未激活，请更新信息后重新提现。'
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
           ,'您所提供的提现信息有误，请重新提交提现申请。'
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
           ,'您支付宝账户已被冻结，请更新信息后重新提现。'
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
           ,'您支付宝账号信息不完整，请更新信息后重新提现。'
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
           ,'经济型'
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
           ,'三星'
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
           ,'四星'
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
           ,'五星'
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
           ,'六星'
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
           ,'七星'
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
           ,'准三星'
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
           ,'准四星'
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
           ,'准五星'
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
           ,'请选择'
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
           ,'一人'
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
           ,'二人'
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
           ,'三人'
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
           ,'四人'
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
           ,'五人'
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
           ,'六人'
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
           ,'七人'
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
           ,'八人'
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
           ,'九人'
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
           ,'十人'
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
           ,'有线'
           ,'有线'
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
           ,'无'
           ,'无宽带'
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
           ,'无限制'
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
           ,'只可内宾'
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
           ,'只可外宾'
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
           ,'无窗'
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
           ,'有窗'
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
           ,'部分有窗'
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
           ,'未区分'
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
           ,'无烟房'
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
           ,'无烟处理'
           ,'System')
GO

INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'BIZ','商务大床房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'STD','标准间','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SD','大床房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DXS','豪华套房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DQ','豪华大床房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SUT','商务套房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'STD','标准双人房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','豪华标间','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','豪华标准间','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','豪华双人房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SS','大床房（内宾）','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SD','标准间（内宾）','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'DT','豪华双床房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'FK','高级大床房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'BT','商务标间','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'BT','商务标准间','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SD','标准大床房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'ESUT','套房','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'SS','单人间','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'roomnm' ,'FT','三人间','System')




INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'0','大床','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'1','双床','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'2','三床及以上','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'3','圆床','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'bedtype' ,'4','水床','System')



INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'HUBS1','HUBS1','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'ELONG','艺龙','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'PODINNS','布丁','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'CTRIP','携程','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'HOMEINNS','如家','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'MOTEL168','莫泰168','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'suphoteltype' ,'HVP','天海路','System')



INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'discount' ,'0','未指定','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'discount' ,'1','固定折扣','System')
INSERT INTO [CMS].[dbo].[WebSite_SysConfiguration] ([DATETIME] ,[Type] ,[Key] ,[Value] ,[USERID]) VALUES (GETDATE(),'discount' ,'2','固定价格','System')





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
           ,'18点正常上线'
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
           ,'14点正常上线'
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
           ,'99元房计划'
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
           ,'10点限时抢购'
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
           ,'自定义时间'
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
           ,'订单传真'
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
           ,'审核传真'
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
           ,'订单'
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
           ,'解除绑定'
           ,'System')
GO