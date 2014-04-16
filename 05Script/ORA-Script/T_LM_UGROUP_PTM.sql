-- Create table
create table T_LM_UGROUP_PTM
(
  ID            NUMBER not null,
  USERGROUP_ID	VARCHAR2(40),
  PLATFORM_CODE	VARCHAR2(40),
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
comment on table T_LM_UGROUP_PTM
  is '用户平台组应用信息表';
comment on column T_LM_UGROUP_PTM.ID
  is '系统ID';
comment on column T_LM_UGROUP_PTM.USERGROUP_ID
  is '用户组ID';
comment on column T_LM_UGROUP_PTM.PLATFORM_CODE
  is '应用平台代码';
comment on column T_LM_UGROUP_PTM.CREATE_TIME
  is '创建时间';
comment on column T_LM_UGROUP_PTM.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_UGROUP_PTM
  add constraint T_LM_UGROUP_PTM_ID_PK primary key (ID)
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