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
  is '�Ƶ������ʩ���ñ�';
comment on column t_lm_b_facilities_hotel.ID
  is 'ϵͳID';
comment on column t_lm_b_facilities_hotel.FACILITIES_CODE
  is '������ʩCODE';
comment on column t_lm_b_facilities_hotel.HOTEL_ID
  is '�Ƶ�ID';
comment on column t_lm_b_facilities.STATUS
  is '����״̬  1����Ч  0����Ч';
comment on column t_lm_b_facilities_hotel.CREATE_TIME
  is '����ʱ��';
comment on column t_lm_b_facilities_hotel.UPDATE_TIME
  is '����ʱ��';

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
