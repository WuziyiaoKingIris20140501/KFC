USE [CMS]
GO

/****** Object:  Table [dbo].[OrderConfirmManager]    Script Date: 08/31/2012 10:56:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderConfirmManager](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[fog_order_num] [nvarchar](100) NULL,
	[login_mobile] [nvarchar](100) NULL,
	[contact_name] [nvarchar](100) NULL,
	[hotel_id] [nvarchar](100) NULL,
	[hotel_name] [nvarchar](100) NULL,
	[linktel] [nvarchar](100) NULL,
	[price_code] [nvarchar](10) NULL,
	[create_time] [nvarchar](100) NULL,
	[in_date] [nvarchar](100) NULL,
	[book_status_other] [nvarchar](10) NULL,
	[update_time] [nvarchar](100) NULL,
	[timeD_diff] [nvarchar](100) NULL,
	[cancel_reason] [nvarchar](100) NULL
 CONSTRAINT [pk_OrderConfirmManager] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


