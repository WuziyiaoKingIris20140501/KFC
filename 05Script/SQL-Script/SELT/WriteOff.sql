USE [SELT]
GO

/****** Object:  Table [dbo].[CollectHis]  回款销账表  Script Date: 11/15/2013 14:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WriteOffHis](
   COLLECTID            int                  null,
   WRITEOFFID           int IDENTITY(1,1)    NOT NULL,
   DEALTIME             datetime             null,
   PAYNAME              nvarchar(100)        null,
   PAYACCOUNT           nvarchar(50)         null,
   INTOAMOUNT           decimal(11,2)        null,
   DETAILSERIALNUM      nvarchar(100)        null,
   SUMMARY              nvarchar(100)        null,
   REMARK               nvarchar(200)        null,
   INTOSTATUS           nvarchar(2)          null,
   INTODATE             datetime             null,
   FILTERMATCHID	    nvarchar(200)        null,
   FILTERMATCHSTATUS    nvarchar(50)         null,
   STATUS               nvarchar(1)          null,
   CREATEUSER           nvarchar(20)         null,
   CREATETIME           datetime             null,
   UPDATEUSER           nvarchar(20)         null,
   UPDATETIME           datetime             null,
   
CONSTRAINT [PK_WriteOff] PRIMARY KEY CLUSTERED 
(
	[WRITEOFFID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO