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
  is '核心酒店组明细信息表';
-- Add comments to the columns 
comment on column T_LM_COREHOTELGROUP_DETAIL.ID
  is '核心酒店组明细ID';
comment on column T_LM_COREHOTELGROUP_DETAIL.COREGROUPID
  is '核心酒店组ID';
comment on column T_LM_COREHOTELGROUP_DETAIL.HOTEL_ID
  is '核心酒店ID';
comment on column T_LM_COREHOTELGROUP_DETAIL.STATUS
  is '状态';
comment on column T_LM_COREHOTELGROUP_DETAIL.CREATE_TIME
  is '创建时间';
comment on column T_LM_COREHOTELGROUP_DETAIL.CREATE_USER
  is '创建人';
comment on column T_LM_COREHOTELGROUP_DETAIL.UPDATE_TIME
  is '更新时间';
comment on column T_LM_COREHOTELGROUP_DETAIL.UPDATE_USER
  is '更新人';
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
