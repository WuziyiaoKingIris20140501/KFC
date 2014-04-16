CREATE INDEX [idx_etype] ON [dbo].[WebSiteEvent_History] ([EVENT_TYPE]);
CREATE INDEX [idx_etid] ON [dbo].[WebSiteEvent_History] ([EVENT_ID]);
CREATE INDEX [idx_APKDownLoad_log_CHANNEL] ON dbo.APKDownLoad_log (CHANNEL);
CREATE INDEX [idx_LmOrderEvent_History_EVENT_LM_ID] ON dbo.LmOrderEvent_History (EVENT_LM_ID);
CREATE INDEX [idx_LmOrderEvent_History_EVENT_ID] ON dbo.LmOrderEvent_History (EVENT_ID);
CREATE INDEX [idx_LMHotelPlan_History_LMID] ON dbo.LMHotelPlan_History (LMID);
CREATE INDEX [idx_LMHotelPlan_History_HOTEL_ID] ON dbo.LMHotelPlan_History (HOTEL_ID);
CREATE INDEX [idx_HotelSalesPlanManager_Detail_HPID] ON dbo.HotelSalesPlanManager_Detail (HPID);
CREATE INDEX [idx_HotelSalesPlanManager_Job_HPID] ON dbo.HotelSalesPlanManager_Job (HPID);
CREATE INDEX [idx_PushInfoPlanAction_History_PPID] ON dbo.PushInfoPlanAction_History (PPID);
CREATE INDEX [idx_PushInfoPlanAction_History_UserID] ON dbo.PushInfoPlanAction_History (UserID);
CREATE INDEX [idx_PushInfoPlanAction_History_DeviceToken] ON dbo.PushInfoPlanAction_History (DeviceToken);
CREATE INDEX [idx_HotelSalesManager_User_Account] ON dbo.HotelSalesManager (User_Account);
CREATE INDEX [idx_HotelSalesManager_Hotel_ID] ON dbo.HotelSalesManager (Hotel_ID);
CREATE INDEX [idx_HotelSalesManager_History_User_Account] ON dbo.HotelSalesManager_History (User_Account);
CREATE INDEX [idx_HotelSalesManager_History_Hotel_ID] ON dbo.HotelSalesManager_History (Hotel_ID);
CREATE INDEX [idx_PushPlanAction_History_PPID] ON dbo.PushPlanAction_History (PPID);
CREATE INDEX [idx_PushPlanAction_History_TelPhone] ON dbo.PushPlanAction_History (TelPhone);
CREATE INDEX [idx_PushPlanAction_History_DeviceToken] ON dbo.PushPlanAction_History (DeviceToken);
CREATE INDEX [idx_LMTROOM_History_HOTEL_ID] ON dbo.LMTROOM_History (HOTEL_ID);
CREATE INDEX [idx_LMTROOM_History_ROOM_CODE] ON dbo.LMTROOM_History (ROOM_CODE);
CREATE INDEX [idx_LMTROOM_History_BED_TYPE] ON dbo.LMTROOM_History (BED_TYPE);
CREATE INDEX [idx_OrderConfirmManager_fog_order_num] ON dbo.OrderConfirmManager (fog_order_num);
CREATE INDEX [idx_OrderConfirmManager_fog_login_mobile] ON dbo.OrderConfirmManager (login_mobile);
CREATE INDEX [idx_OrderConfirmManager_fog_hotel_id] ON dbo.OrderConfirmManager (hotel_id);
CREATE INDEX [idx_WebSiteEvent_History_CREATEDATE] ON dbo.WebSiteEvent_History (CREATEDATE);