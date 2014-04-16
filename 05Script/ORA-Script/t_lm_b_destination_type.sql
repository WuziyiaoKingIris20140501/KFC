-- Create table
create table T_LM_B_DESTINATION_TYPE
(
  ID		NUMBER not null,
  PARENTS_ID	VARCHAR2(40),
  NAME_CN	VARCHAR2(300),
  NAME_EN	VARCHAR2(300),
  SHOW_FlAG	VARCHAR2(2),
  STATUS	VARCHAR2(2),
  CREATE_TIME	TIMESTAMP(6),
  UPDATE_TIME	TIMESTAMP(6)
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
comment on table T_LM_B_DESTINATION_TYPE
  is 'Ŀ�ĵ�������Ϣ��';
-- Add comments to the columns 
comment on column T_LM_B_DESTINATION_TYPE.ID
  is 'ID';
comment on column T_LM_B_DESTINATION_TYPE.PARENTS_ID
  is '�ϼ�ID';
comment on column T_LM_B_DESTINATION_TYPE.NAME_CN
  is '��������';
comment on column T_LM_B_DESTINATION_TYPE.NAME_EN
  is 'Ӣ������';
comment on column T_LM_B_DESTINATION_TYPE.SHOW_FlAG
  is '��ʾFLAG  1����ʾ  0������ʾ';
comment on column T_LM_B_DESTINATION_TYPE.STATUS
  is '����״̬  1������  0������';
comment on column T_LM_B_DESTINATION_TYPE.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_DESTINATION_TYPE.UPDATE_TIME
  is '����ʱ��';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_DESTINATION_TYPE
  add constraint T_LM_B_DESTINATION_TYPE_ID_PK primary key (ID)
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
