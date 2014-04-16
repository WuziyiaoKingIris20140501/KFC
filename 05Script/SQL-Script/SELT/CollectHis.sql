USE [SELT]
GO

/****** Object:  Table [dbo].[CollectHis]  »Ø¿î±í  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CollectHis](
   COLLECTID            int IDENTITY(1,1)    NOT NULL,
   PAYMENTTYPE          nvarchar(2)          null,
   ACTUALAMOUNT         decimal(11,2)        null,
   ORIGINAMOUNT         decimal(11,2)        null,
   UPLOADUSER           nvarchar(20)         null,
   UPLOADDATE           datetime             null,
   UPLOADREMARK         nvarchar(200)        null,
   UPLOADFILENAME       nvarchar(200)        null,
   IMPORTTYPE           nvarchar(1)          null,
   SERIALDATE           datetime             null,
   TOTALCOUNT           int                  null,
   TOTALAMOUNT          decimal(11,2)        null,
   REMARK               nvarchar(200)        null,
   STATUS               nvarchar(1)          null,
   CREATEUSER           nvarchar(20)         null,
   CREATETIME           datetime             null,
   UPDATEUSER           nvarchar(20)         null,
   UPDATETIME           datetime             null,
CONSTRAINT [PK_Collect] PRIMARY KEY CLUSTERED 
(
	[COLLECTID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
