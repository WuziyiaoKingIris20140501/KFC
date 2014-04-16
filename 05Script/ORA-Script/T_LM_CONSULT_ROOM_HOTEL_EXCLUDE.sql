-- Create table
create table T_LM_CONSULT_ROOM_HOTEL_EXCLUDE
(
  ID            NUMBER not null,
  TLCRID        VARCHAR2(10),
  HOTEL_ID      VARCHAR2(40),
  STATUS        VARCHAR2(1),
  CREATEDATE    DATE,
  CREATEUSER    VARCHAR2(100)
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
comment on table T_LM_CONSULT_ROOM_HOTEL_EXCLUDE
  is 'LMѲ���ų��Ƶ��';
-- Add comments to the columns 
comment on column T_LM_CONSULT_ROOM_HOTEL_EXCLUDE.ID
  is '����ID';
comment on column T_LM_CONSULT_ROOM_HOTEL_EXCLUDE.TLCRID
  is 'Ѳ����ID';
comment on column T_LM_CONSULT_ROOM_HOTEL_EXCLUDE.HOTEL_ID
  is '�Ƶ�ID';
comment on column T_LM_CONSULT_ROOM_HOTEL_EXCLUDE.STATUS
  is '״̬';
comment on column T_LM_CONSULT_ROOM_HOTEL_EXCLUDE.CREATEDATE
  is '��������';
comment on column T_LM_CONSULT_ROOM_HOTEL_EXCLUDE.CREATEUSER
  is '������';
-- Create/Recreate indexes 
create index T_LM_CONSULT_EXCLUDE_TLCRID_IDX on T_LM_CONSULT_ROOM_HOTEL_EXCLUDE (TLCRID)
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

create index T_LM_CONSULT_EXCLUDE_HOTEL_ID_IDX on T_LM_CONSULT_ROOM_HOTEL_EXCLUDE (HOTEL_ID)
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
