USE [SELT]
GO

/****** Object:  Table [dbo].[SettlementItem]  Ω·À„œÓ±Ì  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SettlementItem](
   SLID                 int			         null,
   SETTLEID             int IDENTITY(1,1)    NOT NULL,
   SETTLETYPE           nvarchar(2)          null,
   ADJUSTNM             nvarchar(40)         null,
   ADJUSTAMOUNT         decimal(11,2)        null,
   REMARK               nvarchar(200)        null,
   WRITEOFFID            int		         null,
   HAIRCUTSCONFIRMUSER  nvarchar(20)         null,
   HAIRCUTSCONFIRMDATE  datetime             null,
   HAIRCUTSCONFIRMAMOUNT decimal(11,2)       null,
   STATUS               nvarchar(2)          null,
   CREATEUSER           nvarchar(20)         null,
   CREATETIME           datetime             null,
   UPDATEUSER           nvarchar(20)         null,
   UPDATETIME           datetime             null,
 CONSTRAINT [PK_SettlementItem] PRIMARY KEY CLUSTERED 
(
	[SETTLEID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE INDEX [idx_SettlementItem_SLID] ON dbo.SettlementItem (SLID);
