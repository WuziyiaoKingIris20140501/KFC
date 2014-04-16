-- Create table
create table T_LM_UGROUP_RCL
(
  ID                 NUMBER not null,
  USERGROUP_ID       VARCHAR2(40),
  REGCHANEL_CODE     VARCHAR2(40),
  CREATE_TIME         TIMESTAMP(6),
  UPDATE_TIME         TIMESTAMP(6)
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
comment on table T_LM_UGROUP_RCL
  is '用户组注册渠道信息表';
comment on column T_LM_UGROUP_RCL.ID
  is '系统ID';
comment on column T_LM_UGROUP_RCL.USERGROUP_ID
  is '用户组ID';
comment on column T_LM_UGROUP_RCL.REGCHANEL_CODE
  is '注册渠道代码';
comment on column T_LM_UGROUP_RCL.CREATE_TIME
  is '创建时间';
comment on column T_LM_UGROUP_RCL.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_UGROUP_RCL
  add constraint T_LM_UGROUP_RCL_ID_PK primary key (ID)
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
