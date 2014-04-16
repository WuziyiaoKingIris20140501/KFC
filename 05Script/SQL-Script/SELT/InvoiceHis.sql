USE [SELT]
GO

/****** Object:  Table [dbo].[InvoiceHis]  发票历史表  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceHis](
   SLID                 int			         null,
   INVOICEID            int IDENTITY(1,1)    NOT NULL,
   INVOICENM            nvarchar(200)        null,
   INVOICEPROJECT       nvarchar(100)        null,
   INVOICEAMOUNT        decimal(11,2)        null,
   ZIPADDRESS           nvarchar(200)        null,
   INVOICENUM           nvarchar(100)        null,
   ISREOPEN             nvarchar(1)          null,
   REOPENREASON         nvarchar(100)        null,
   OLDINVOICENUM        nvarchar(100)        null,
   ZIPNUM               nvarchar(50)         null,
   ISREZIP              nvarchar(1)          null,
   OLDZIPNUM            nvarchar(50)         null,
   STATUS               nvarchar(1)          null,
   CREATEUSER           nvarchar(20)         null,
   CREATETIME           datetime             null,
   UPDATEUSER           nvarchar(20)         null,
   UPDATETIME           datetime             null,
 CONSTRAINT [PK_InvoiceHis] PRIMARY KEY CLUSTERED 
(
	[INVOICEID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE INDEX [idx_InvoiceHis_SLID] ON dbo.InvoiceHis (SLID);
