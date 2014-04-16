-- Create table
create table t_lm_b_facilities_hotel
(
  ID			NUMBER not null,
  FACILITIES_CODE	VARCHAR2(40),
  HOTEL_ID		VARCHAR2(40),
  STATUS		VARCHAR2(2),
  CREATE_TIME		TIMESTAMP(6),
  UPDATE_TIME		TIMESTAMP(6)
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
comment on table t_lm_b_facilities_hotel
  is '酒店服务设施设置表';
comment on column t_lm_b_facilities_hotel.ID
  is '系统ID';
comment on column t_lm_b_facilities_hotel.FACILITIES_CODE
  is '服务设施CODE';
comment on column t_lm_b_facilities_hotel.HOTEL_ID
  is '酒店ID';
comment on column t_lm_b_facilities.STATUS
  is '在线状态  1：有效  0：无效';
comment on column t_lm_b_facilities_hotel.CREATE_TIME
  is '创建时间';
comment on column t_lm_b_facilities_hotel.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table t_lm_b_facilities_hotel
  add constraint t_lm_b_facilities_hotel_ID_PK primary key (ID)
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
