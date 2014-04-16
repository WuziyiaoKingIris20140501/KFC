-- Create table
create table T_LM_B_DESTINATION_TYPE
(
  ID		NUMBER not null,
  PARENTS_ID	VARCHAR2(40),
  NAME_CN	VARCHAR2(300),
  NAME_EN	VARCHAR2(300),
  SHOW_FlAG	VARCHAR2(2),
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
-- Add comments to the table 
comment on table T_LM_B_DESTINATION_TYPE
  is '目的地类型信息表';
-- Add comments to the columns 
comment on column T_LM_B_DESTINATION_TYPE.ID
  is 'ID';
comment on column T_LM_B_DESTINATION_TYPE.PARENTS_ID
  is '上级ID';
comment on column T_LM_B_DESTINATION_TYPE.NAME_CN
  is '中文名称';
comment on column T_LM_B_DESTINATION_TYPE.NAME_EN
  is '英文名称';
comment on column T_LM_B_DESTINATION_TYPE.SHOW_FlAG
  is '显示FLAG  1：显示  0：不显示';
comment on column T_LM_B_DESTINATION_TYPE.STATUS
  is '在线状态  1：上线  0：下线';
comment on column T_LM_B_DESTINATION_TYPE.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_DESTINATION_TYPE.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_DESTINATION_TYPE
  add constraint T_LM_B_DESTINATION_TYPE_ID_PK primary key (ID)
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
