USE [SELT]
GO

/****** Object:  Table [dbo].[LiquidateItem]  «ÂÀ„œÓ±Ì  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LiquidateItem](
   SLID                 int			         null,
   LIQUIDATIONID        int IDENTITY(1,1)    NOT NULL,
   LIQUIDATIONTYPE      nvarchar(2)          null,
   ADJUSTNM             nvarchar(40)         null,
   SLAMOUNT             decimal(11,2)        null,
   REMARK               nvarchar(200)        null,
   ORDERID              nvarchar(30)         null,
   CITYID               nvarchar(30)         null,
   CITYNM               nvarchar(30)         null,
   HOTELID              nvarchar(30)         null,
   HOTELNM              nvarchar(100)        null,
   GROUPID              nvarchar(30)         null,
   GROUPNM              nvarchar(100)        null,
   ROOMID               nvarchar(30)         null,
   ROOMNM               nvarchar(100)        null,
   ROOMNUM              nvarchar(5)          null,
   GUESTNM              nvarchar(30)         null,
   INDATE               date	             null,
   OUTDATE              date	             null,
   TOTALAMOUNT          decimal(11,2)        null,
   ISNEXTMONTH          nvarchar(1)          null,
   INTOMONTH            nvarchar(20)          null,
   NEXTTIMES            int                  null,
   STATUS               nvarchar(1)          null,
   CREATEUSER           nvarchar(20)         null,
   CREATETIME           datetime             null,
   UPDATEUSER           nvarchar(20)         null,
   UPDATETIME           datetime             null,

 CONSTRAINT [PK_LIQUIDATEITEM] PRIMARY KEY CLUSTERED 
(
	[LIQUIDATIONID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE INDEX [idx_LiquidateItem_SLID] ON dbo.LiquidateItem (SLID);
CREATE INDEX [idx_LiquidateItem_ORDERID] ON dbo.LiquidateItem (ORDERID);
CREATE INDEX [idx_LiquidateItem_CITYID] ON dbo.LiquidateItem (CITYID);
CREATE INDEX [idx_LiquidateItem_HOTELID] ON dbo.LiquidateItem (HOTELID);
CREATE INDEX [idx_LiquidateItem_GROUPID] ON dbo.LiquidateItem (GROUPID);