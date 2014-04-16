-- Create table
create table t_lm_b_facilities
(
  ID		NUMBER not null,
  FACILITIES_CODE VARCHAR2(60),
  NAME_ZH	VARCHAR2(300),
  NAME_EN	VARCHAR2(300),
  TYPE		VARCHAR2(40),
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
comment on table t_lm_b_facilities
  is '酒店服务设施管理表';
comment on column t_lm_b_facilities.ID
  is '系统ID';
comment on column t_lm_b_facilities.FACILITIES_CODE
  is '服务设施CODE  P开头5位补0 自动递增';
comment on column t_lm_b_facilities.NAME_ZH
  is '服务设施中文名称';
comment on column t_lm_b_facilities.NAME_EN
  is '服务设施英文名称';
comment on column t_lm_b_facilities.TYPE
  is '服务设施类型';
comment on column t_lm_b_facilities.STATUS
  is '在线状态  1：上线  0：下线';
comment on column t_lm_b_facilities.CREATE_TIME
  is '创建时间';
comment on column t_lm_b_facilities.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table t_lm_b_facilities
  add constraint t_lm_b_facilities_ID_PK primary key (ID)
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
