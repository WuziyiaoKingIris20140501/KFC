-- Create table
create table T_LM_B_DESTINATION_HOTEL
(
  ID			NUMBER not null,
  CITY_ID		VARCHAR2(40),
  HOTEL_ID		VARCHAR2(40),
  DESTINATION_ID	VARCHAR2(40),
  TYPE_ID		VARCHAR2(40),
  DISTANCE		VARCHAR2(300),
  CREATE_TIME TIMESTAMP(6),
  UPDATE_TIME TIMESTAMP(6)
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
comment on table T_LM_B_DESTINATION_HOTEL
  is '酒店目的地信息表';
-- Add comments to the columns 
comment on column T_LM_B_DESTINATION_HOTEL.ID
  is 'ID';
comment on column T_LM_B_DESTINATION_HOTEL.CITY_ID
  is '城市ID';
comment on column T_LM_B_DESTINATION_HOTEL.HOTEL_ID
  is '酒店ID';
comment on column T_LM_B_DESTINATION_HOTEL.DESTINATION_ID
  is '目的地ID';
comment on column T_LM_B_DESTINATION_HOTEL.TYPE_ID
  is '目的地类型ID';
comment on column T_LM_B_DESTINATION_HOTEL.DISTANCE
  is '酒店与目的地之间距离(单位：KM)';
comment on column T_LM_B_DESTINATION_HOTEL.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_DESTINATION_HOTEL.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_DESTINATION_HOTEL
  add constraint T_LM_B_DESTINATION_HOTEL_ID_PK primary key (ID)
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
