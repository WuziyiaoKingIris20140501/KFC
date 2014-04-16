-- Create table
create table T_LM_B_REGCHANEL
(
  ID			NUMBER not null,
  REGCHANEL_CODE	VARCHAR2(40),
  REGCHANEL_NAME	VARCHAR2(300),
  STATUS		VARCHAR2(2),
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
comment on table T_LM_B_REGCHANEL
  is '注册渠道信息表';
comment on column T_LM_B_REGCHANEL.ID
  is '系统ID';
comment on column T_LM_B_REGCHANEL.REGCHANEL_CODE
  is '注册渠道代码';
comment on column T_LM_B_REGCHANEL.REGCHANEL_NAME
  is '注册渠道名称';
comment on column T_LM_B_REGCHANEL.STATUS
  is '在线状态  1：上线  0：下线';
comment on column T_LM_B_REGCHANEL.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_REGCHANEL.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_REGCHANEL
  add constraint T_LM_B_REGCHANEL_ID_PK primary key (ID)
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
