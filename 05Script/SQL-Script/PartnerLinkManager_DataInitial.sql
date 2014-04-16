INSERT INTO [CMS].[dbo].[PartnerLinkManager]
           ([PartnerID]
           ,[PartnerTitle]
           ,[PartnerLink]
           ,[ReMark]
           ,[Cost]
           ,[Hits]
           ,[UserID]
           ,[CREATETIME]
           ,[UPDATETIME])
    SELECT 
PH.[PartnerID]
,PH.[PartnerTitle]
,PH.[PartnerLink]
,PH.[ReMark]
,PH.[Cost]
,ISNULL(TT.CHCT,0)
,PH.[UserID]
,PH.[CREATETIME]
,PH.[UPDATETIME]
FROM [CMS].[dbo].[PartnerManager_History] PH
left join (select count([nid]) AS CHCT, CHANNEL from [CMS].[dbo].[APKDownLoad_log] group by CHANNEL) TT
ON PH.[PartnerID]=tt.CHANNEL

