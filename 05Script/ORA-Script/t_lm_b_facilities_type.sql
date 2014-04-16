-- Create table
create table T_LM_B_FACILITIES_TYPE
(
  ID                   NUMBER not null,
  FACILITIES_TYPE_CODE VARCHAR2(40),
  FACILITIES_TYPE_NAME VARCHAR2(300),
  FACILITIES_TYPE_SEQ  NUMBER,
  STATUS               VARCHAR2(2),
  CREATE_TIME          TIMESTAMP(6),
  UPDATE_TIME          TIMESTAMP(6)
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
comment on table T_LM_B_FACILITIES_TYPE
  is '服务设施类别信息表';
-- Add comments to the columns 
comment on column T_LM_B_FACILITIES_TYPE.ID
  is '系统ID';
comment on column T_LM_B_FACILITIES_TYPE.FACILITIES_TYPE_CODE
  is '服务设施类别代码';
comment on column T_LM_B_FACILITIES_TYPE.FACILITIES_TYPE_NAME
  is '服务设施类别名称';
comment on column T_LM_B_FACILITIES_TYPE.FACILITIES_TYPE_SEQ
  is '服务设施类别排序';
comment on column T_LM_B_FACILITIES_TYPE.STATUS
  is '在线状态  1：上线  0：下线';
comment on column T_LM_B_FACILITIES_TYPE.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_FACILITIES_TYPE.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_FACILITIES_TYPE
  add constraint T_LM_B_FACILITIES_TYPE_ID_PK primary key (ID)
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