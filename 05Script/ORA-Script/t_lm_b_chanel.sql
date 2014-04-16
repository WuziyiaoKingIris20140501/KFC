-- Create table
create table T_LM_B_CHANEL
(
  ID		NUMBER not null,
  CHANEL_CODE	VARCHAR2(40),
  CHANEL_NAME   VARCHAR2(300),
  STATUS	VARCHAR2(2),
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

comment on table T_LM_B_CHANEL
  is '渠道信息表';
-- Add comments to the table 
comment on table T_LM_B_CHANEL
  is '发放渠道主表';
-- Add comments to the columns 
comment on column T_LM_B_CHANEL.CHANEL_CODE
  is '渠道编号';
comment on column T_LM_B_CHANEL.CHANEL_NAME
  is '渠道名称';
comment on column T_LM_B_CHANEL.STATUS
  is '在线状态  1：上线  0：下线';
comment on column T_LM_B_CHANEL.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_CHANEL.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_CHANEL
  add constraint T_LM_B_CHANEL_ID_PK primary key (ID)
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
