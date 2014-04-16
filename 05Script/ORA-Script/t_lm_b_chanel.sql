-- Create table
create table T_LM_B_CHANEL
(
  ID		NUMBER not null,
  CHANEL_CODE	VARCHAR2(40),
  CHANEL_NAME   VARCHAR2(300),
  STATUS	VARCHAR2(2),
  CREATE_TIME   TIMESTAMP(6),
  UPDATE_TIME   TIMESTAMP(6)
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

comment on table T_LM_B_CHANEL
  is '������Ϣ��';
-- Add comments to the table 
comment on table T_LM_B_CHANEL
  is '������������';
-- Add comments to the columns 
comment on column T_LM_B_CHANEL.CHANEL_CODE
  is '�������';
comment on column T_LM_B_CHANEL.CHANEL_NAME
  is '��������';
comment on column T_LM_B_CHANEL.STATUS
  is '����״̬  1������  0������';
comment on column T_LM_B_CHANEL.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_CHANEL.UPDATE_TIME
  is '����ʱ��';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_CHANEL
  add constraint T_LM_B_CHANEL_ID_PK primary key (ID)
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
