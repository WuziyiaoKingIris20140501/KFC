-- Create table
create table T_LM_COREHOTELGROUP_DETAIL
(
  ID                  NUMBER not null,
  COREGROUPID         NUMBER,
  HOTEL_ID            VARCHAR2(40),
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
comment on table T_LM_COREHOTELGROUP_DETAIL
  is '���ľƵ�����ϸ��Ϣ��';
-- Add comments to the columns 
comment on column T_LM_COREHOTELGROUP_DETAIL.ID
  is '���ľƵ�����ϸID';
comment on column T_LM_COREHOTELGROUP_DETAIL.COREGROUPID
  is '���ľƵ���ID';
comment on column T_LM_COREHOTELGROUP_DETAIL.HOTEL_ID
  is '���ľƵ�ID';
comment on column T_LM_COREHOTELGROUP_DETAIL.STATUS
  is '״̬';
comment on column T_LM_COREHOTELGROUP_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_COREHOTELGROUP_DETAIL.CREATE_USER
  is '������';
comment on column T_LM_COREHOTELGROUP_DETAIL.UPDATE_TIME
  is '����ʱ��';
comment on column T_LM_COREHOTELGROUP_DETAIL.UPDATE_USER
  is '������';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_COREHOTELGROUP_DETAIL
  add constraint T_LM_COREGROUP_DETAIL_ID_PK primary key (ID)
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
