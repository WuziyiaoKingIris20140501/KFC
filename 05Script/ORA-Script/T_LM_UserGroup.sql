-- Create table
create table T_LM_UserGroup
(
  ID		      NUMBER not null,
  USERGROUP_NAME      VARCHAR2(100),
  REGIST_START         TIMESTAMP(6),
  REGIST_END           TIMESTAMP(6),
  LOGIN_START          TIMESTAMP(6),
  LOGIN_END            TIMESTAMP(6),
  SUBMIT_ORDER_FROM     NUMBER(4,0),
  SUBMIT_ORDER_TO       NUMBER(4,0),
  COMPLETE_ORDER_FROM   NUMBER(4,0),
  COMPLETE_ORDER_TO     NUMBER(4,0),
  LAST_ORDER_START      TIMESTAMP(6),
  LAST_ORDER_END        TIMESTAMP(6),
  MANUAL_ADD           NCLOB,
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
comment on table T_LM_UserGroup
  is '用户组信息表';
comment on column T_LM_UserGroup.ID
  is '用户组ID';
comment on column T_LM_UserGroup.USERGROUP_NAME
  is '用户组名称';
comment on column T_LM_UserGroup.REGIST_START
  is '注册时间开始日';
comment on column T_LM_UserGroup.REGIST_END
  is '注册时间结束日';
comment on column T_LM_UserGroup.LOGIN_START
  is '最后登录开始日';
comment on column T_LM_UserGroup.LOGIN_END
  is '最后登录结束日';
comment on column T_LM_UserGroup.SUBMIT_ORDER_FROM
  is '订单提交数量From';
comment on column T_LM_UserGroup.SUBMIT_ORDER_TO
  is '订单提交数量To';
comment on column T_LM_UserGroup.COMPLETE_ORDER_FROM
  is '成功订单数量From';
comment on column T_LM_UserGroup.COMPLETE_ORDER_TO
  is '成功订单数量To';
comment on column T_LM_UserGroup.LAST_ORDER_START
  is '最后下单时间From';
comment on column T_LM_UserGroup.LAST_ORDER_END
  is '最后下单时间To';
comment on column T_LM_UserGroup.MANUAL_ADD
  is '手动添加';
comment on column T_LM_UserGroup.CREATE_TIME
  is '创建时间';
comment on column T_LM_UserGroup.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_UserGroup
  add constraint T_LM_UserGroup_ID_PK primary key (ID)
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
