USE [CMS]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPartnerLinkList]    Script Date: 03/13/2012 17:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPartnerLinkList]
	@PartnerID nvarchar(20)
AS
BEGIN
	SELECT [PartnerID]
      ,[PartnerLink]
  FROM [CMS].[dbo].[PartnerLinkManager] where [PartnerID]= @PartnerID ORDER BY CREATETIME DESC
END
