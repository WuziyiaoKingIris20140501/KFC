-- Create table
create table T_LM_UGROUP_ULT
(
  ID		NUMBER,
  USERGROUP_ID  VARCHAR2(40),
  LOGIN_MOBILE  VARCHAR2(40),
  ADD_TYPE     VARCHAR2(2),
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
comment on table T_LM_UGROUP_ULT
  is '用户组明细用户信息表';
-- Add comments to the columns 
comment on column T_LM_UGROUP_ULT.ID
  is '系统ID';
comment on column T_LM_UGROUP_ULT.USERGROUP_ID
  is '用户组ID';
comment on column T_LM_UGROUP_ULT.LOGIN_MOBILE
  is '用户登录手机';
comment on column T_LM_UGROUP_ULT.ADD_TYPE
  is '添加方式 0：自动 1：手动';
comment on column T_LM_UGROUP_ULT.CREATE_TIME
  is '创建时间';
comment on column T_LM_UGROUP_ULT.UPDATE_TIME
  is '更新时间';