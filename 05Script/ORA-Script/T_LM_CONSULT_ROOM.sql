-- Create table
create table T_LM_CONSULT_ROOM
(
  ID            NUMBER not null,
  SALES_ACCOUNT VARCHAR2(100),
  RTYPE         VARCHAR2(2),
  CITY_ID      VARCHAR2(40),
  TAG_ID        VARCHAR2(40),
  HOTEL_ID    VARCHAR2(40),
  STATUS        VARCHAR2(1),
  CREATEDATE    DATE,
  CREATEUSER    VARCHAR2(100),
  OTYPE         VARCHAR2(2)
)
tablespace HOTELVPTEST
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
comment on table T_LM_CONSULT_ROOM
  is 'LM巡房表';
-- Add comments to the columns 
comment on column T_LM_CONSULT_ROOM.ID
  is '自增ID';
comment on column T_LM_CONSULT_ROOM.SALES_ACCOUNT
  is '销售人员ID';
comment on column T_LM_CONSULT_ROOM.CITY_ID
  is '城市ID';
comment on column T_LM_CONSULT_ROOM.TAG_ID
  is '商圈ID';
comment on column T_LM_CONSULT_ROOM.HOTEL_ID
  is '酒店ID';
comment on column T_LM_CONSULT_ROOM.STATUS
  is '状态';
comment on column T_LM_CONSULT_ROOM.CREATEDATE
  is '创建日期';
comment on column T_LM_CONSULT_ROOM.CREATEUSER
  is '创建人';
  comment on column T_LM_CONSULT_ROOM.OTYPE
  is '0:询房 1:订单审核 2:订单确认';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_CONSULT_ROOM
  add constraint T_LM_CONSULT_ROOM_PK primary key (ID)
  using index 
  tablespace HOTELVPTEST
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
-- Create/Recreate indexes 
create index T_LM_CONSULT_CITY_ID_IDX on T_LM_CONSULT_ROOM (CITY_ID)
  tablespace HOTELVPTEST
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
create index T_LM_CONSULT_TAG_ID_IDX on T_LM_CONSULT_ROOM (TAG_ID)
  tablespace HOTELVPTEST
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
create index T_LM_CONSULT_HOTEL_ID_IDX on T_LM_CONSULT_ROOM (HOTEL_ID)
  tablespace HOTELVPTEST
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
