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
  is '�Ƶ�Ŀ�ĵ���Ϣ��';
-- Add comments to the columns 
comment on column T_LM_B_DESTINATION_HOTEL.ID
  is 'ID';
comment on column T_LM_B_DESTINATION_HOTEL.CITY_ID
  is '����ID';
comment on column T_LM_B_DESTINATION_HOTEL.HOTEL_ID
  is '�Ƶ�ID';
comment on column T_LM_B_DESTINATION_HOTEL.DESTINATION_ID
  is 'Ŀ�ĵ�ID';
comment on column T_LM_B_DESTINATION_HOTEL.TYPE_ID
  is 'Ŀ�ĵ�����ID';
comment on column T_LM_B_DESTINATION_HOTEL.DISTANCE
  is '�Ƶ���Ŀ�ĵ�֮�����(��λ��KM)';
comment on column T_LM_B_DESTINATION_HOTEL.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_DESTINATION_HOTEL.UPDATE_TIME
  is '����ʱ��';
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
