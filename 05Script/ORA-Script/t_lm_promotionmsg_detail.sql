-- Create table
create table T_LM_PROMOTIONMSG_DETAIL
(
  ID             NUMBER not null,
  PROMOTIONMSG_ID   VARCHAR2(100),
  PRO_TYPE       VARCHAR2(1),
  CITY_ID	 VARCHAR2(100),
  HOTELGROUP_ID  VARCHAR2(100),
  HOTEL_ID	 VARCHAR2(100),
  ROOM_ID	 VARCHAR2(100),
  USERGROUP_ID   VARCHAR2(100),
  STATUS	 VARCHAR2(1), 
  CREATE_TIME    TIMESTAMP(6),
  UPDATE_TIME    TIMESTAMP(6)
)
tablespace TS_FREEK
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_LM_PROMOTIONMSG_DETAIL
  is '促销信息明细表';
-- Add comments to the columns 
comment on column T_LM_PROMOTIONMSG_DETAIL.ID
  is '促销信息明细ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.PROMOTIONMSG_ID
  is '促销信息ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.PRO_TYPE
  is '促销类型 0:全局促销 1:城市促销 2:酒店集团促销 3:酒店促销 4:房型促销 5:用户组';
comment on column T_LM_PROMOTIONMSG_DETAIL.CITY_ID
  is '城市ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.HOTELGROUP_ID
  is '酒店集团ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.HOTEL_ID
  is '酒店ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.ROOM_ID
  is '房型ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.USERGROUP_ID
  is '用户组ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.STATUS
  is '状态 0:无效1:有效';
comment on column T_LM_PROMOTIONMSG_DETAIL.CREATE_TIME
  is '创建时间';
comment on column T_LM_PROMOTIONMSG_DETAIL.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_PROMOTIONMSG_DETAIL
  add constraint T_LM_PROMOTIONMSG_DETAIL_PK primary key (ID)
  using index 
  tablespace TS_FREEK
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );