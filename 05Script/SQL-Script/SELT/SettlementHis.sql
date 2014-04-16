USE [SELT]
GO

/****** Object:  Table [dbo].[LiquidateItem]  清结算历史表  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SettlementHis](
   SLHISID              int IDENTITY(1,1)    NOT NULL,
   SLID                 int			         null,
   OPETYPE              nvarchar(20)         null,
   OPEUSER              nvarchar(20)         null,
   OPECONTENT           nvarchar(Max)        null,
   OPEKEY               nvarchar(100)        null,
   OPEDATE              datetime             null,
 CONSTRAINT [PK_SettlementHis] PRIMARY KEY CLUSTERED 
(
	[SLHISID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE INDEX [idx_SettlementHis_OPETYPE] ON dbo.SettlementHis (OPETYPE);
CREATE INDEX [idx_SettlementHis_OPEKEY] ON dbo.SettlementHis (OPEKEY);