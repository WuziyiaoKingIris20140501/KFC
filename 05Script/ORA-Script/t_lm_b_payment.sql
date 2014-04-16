-- Create table
create table T_LM_B_PAYMENT
(
  ID		NUMBER not null,
  PAYMENT_CODE	VARCHAR2(40),
  PAYMENT_NAME  VARCHAR2(300),
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
comment on table T_LM_B_PAYMENT
  is '支付方式信息表';
comment on column T_LM_B_PLATFORM.ID
  is '系统ID';
comment on column T_LM_B_PLATFORM.PLATFORM_CODE
  is '应用平台代码';
comment on column T_LM_B_PLATFORM.PLATFORM_NAME
  is '应用平台名称';
comment on column T_LM_B_PLATFORM.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_PLATFORM.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_PAYMENT
  add constraint T_LM_B_PAYMENT_ID_PK primary key (ID)
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
