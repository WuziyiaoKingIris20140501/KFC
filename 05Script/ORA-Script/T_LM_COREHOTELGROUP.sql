-- Create table
create table T_LM_COREHOTELGROUP
(
  ID                  NUMBER not null,
  COREGROUP_NAME      NVARCHAR2(100),
  STATUS              NVARCHAR2(1),
  CREATE_TIME         TIMESTAMP(6),
  CREATE_USER         NVARCHAR2(100),
  UPDATE_TIME         TIMESTAMP(6),
  UPDATE_USER         NVARCHAR2(100)
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
comment on table T_LM_COREHOTELGROUP
  is '���ľƵ�����Ϣ��';
-- Add comments to the columns 
comment on column T_LM_COREHOTELGROUP.ID
  is '���ľƵ���ID';
comment on column T_LM_COREHOTELGROUP.COREGROUP_NAME
  is '���ľƵ�������';
comment on column T_LM_COREHOTELGROUP.STATUS
  is '״̬';
comment on column T_LM_COREHOTELGROUP.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_COREHOTELGROUP.CREATE_USER
  is '������';
comment on column T_LM_COREHOTELGROUP.UPDATE_TIME
  is '����ʱ��';
comment on column T_LM_COREHOTELGROUP.UPDATE_USER
  is '������';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_COREHOTELGROUP
  add constraint T_LM_COREHOTELGROUP_ID_PK primary key (ID)
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
