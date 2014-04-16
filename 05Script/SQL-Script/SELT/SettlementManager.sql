USE [SELT]
GO

/****** Object:  Table [dbo].[SettlementManager]  «ÂΩ·À„±Ì  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SettlementManager](
   SLID                 [int] IDENTITY(1,1) NOT NULL,
   BILLID               nvarchar(30)         null,
   SLMONTH              nvarchar(10)          null,
   INITIALSLAMOUNT      decimal(11,2)        null,
   ACTIVESLAMOUNT       decimal(11,2)        null,
   INVOICEID            int			         null,
   INVOICESTATUS        nvarchar(2)          null,
   WRITEOFFID            int		         null,
   BILLCONFIRMUSER      nvarchar(20)         null,
   BILLCONFIRMDATE      datetime             null,
   BILLCONFIRMREMARK    nvarchar(200)        null,
   HAIRCUTSCONFIRMUSER  nvarchar(20)         null,
   HAIRCUTSCONFIRMDATE  datetime             null,
   HAIRCUTSCONFIRMAMOUNT decimal(11,2)       null,
   HAIRCUTSCONFIRMREMARK nvarchar(200)       null,
   SLSTATUS             nvarchar(2)          null,
   STATUS               nvarchar(1)          null,
   SAVEUSER				nvarchar(20)         null,
   UNITCHARGE           nvarchar(20)         null,
   UNITID               nvarchar(10)         null,
   UNITNM               nvarchar(30)         null,
   INVOICE_NAME         nvarchar(150)        null,
   TERMS                nvarchar(10)         null,
   TERM_STDT            nvarchar(5)          null,
   HOTEL_TAX            decimal(5,2)         null,
   SETTLEMENT_ADDRESS   nvarchar(300)        null,
   SETTLEMENT_PER       nvarchar(20)         null,
   SETTLEMENT_TEL       nvarchar(30)         null,
   SETTLEMENT_FAX       nvarchar(30)         null,
   SETTLEMENT_SALES     nvarchar(100)        null,
   BILL_ITEM            nvarchar(200)        null,
   HOTEL_TAXNO          nvarchar(100)        null,
   HOTEL_PAYNO          nvarchar(100)        null,
   UNITREMARK			nvarchar(200)        null,
   CREATEUSER           nvarchar(20)         null,
   CREATETIME           datetime             null,
   UPDATEUSER           nvarchar(20)         null,
   UPDATETIME           datetime             null,
 CONSTRAINT [pk_SettlementManager] PRIMARY KEY CLUSTERED 
(
	[SLID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE INDEX [idx_SettlementManager_BILLID] ON dbo.SettlementManager (BILLID);
CREATE INDEX [idx_SettlementManager_UNITID] ON dbo.SettlementManager (UNITID);
CREATE INDEX [idx_SettlementManager_INVOICEID] ON dbo.SettlementManager (INVOICEID);
CREATE INDEX [idx_SettlementManager_WRITEOFFID] ON dbo.SettlementManager (WRITEOFFID);