USE [CMS]
GO

/****** Object:  Table [dbo].[LMHotelPlan_History]    Script Date: 05/08/2012 14:20:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LMHotelPlan_History](
	[nid]					[int] IDENTITY(1,1) NOT NULL,
	[LMID]					int null,
	EFFECT_DATE				DATE NULL,
	SEASON               	NVARCHAR(50) NULL,
	MONEY_TYPE           	NVARCHAR(3) NULL,
	HOTEL_ID             	NVARCHAR(20) NULL,
	ROOM_TYPE_NAME       	NVARCHAR(100) NULL,
	ROOM_TYPE_CODE       	NVARCHAR(50) NULL,
	[STATUS]               	VARCHAR(1) NULL,
	ROOM_NUM             	int NULL,
	GMT_CREATED          	DATETIME NULL,
	CREATOR              	NVARCHAR(200) NULL,
	ONE_PRICE            	decimal(8,2) NULL,
	TWO_PRICE            	decimal(8,2) NULL,
	THREE_PRICE          	decimal(8,2) NULL,
	FOUR_PRICE           	decimal(8,2) NULL,
	ATTN_PRICE           	decimal(8,2) NULL,
	BREAKFAST_NUM        	decimal(2) NULL,
	EACH_BREAKFAST_PRICE 	decimal(8,2) NULL,
	IS_NETWORK           	VARCHAR(1) NULL,
	GMT_MODIFIED         	DATETIME NULL,
	MODIFIER             	NVARCHAR(50) NULL,
	IS_DELETED           	VARCHAR(1) NULL,
	HOLD_ROOM_NUM        	decimal(3) NULL,
	RATE_CODE            	NVARCHAR(20) NULL,
	GUAID                	NVARCHAR(40) NULL,
	CXLID                	NVARCHAR(40) NULL,
	OFFSETVAL            	decimal(8,2) NULL,
	OFFSETUNIT           	NVARCHAR(2) NULL,
	LMPRICE              	decimal(8,2) NULL,
	THIRDPRICE           	decimal(8,2) NULL,
	LMSTATUS             	NVARCHAR(64) NULL,
	IS_RESERVE           	NVARCHAR(2) NULL,
	HOTELVP_STATUS       	VARCHAR(1) NULL,
	APP_STATUS           	VARCHAR(1) NULL,
	IS_ROOMFUL           	decimal(1) NULL,
	[SALES_MANAGER]			NVARCHAR(100) NULL,
 CONSTRAINT [pk_LMHotelPlan_History] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO