-- Create table
create table T_LM_B_PAYMENT_PLAT
(
  ID		NUMBER not null,
  PAYMENT_CODE	VARCHAR2(40),
  PLATFORM_CODE VARCHAR2(40),
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
comment on table T_LM_B_PAYMENT_PLAT
  is '渠道与应用平台关系信息表';
comment on column T_LM_B_PAYMENT_PLAT.ID
  is '系统ID';
comment on column T_LM_B_PAYMENT_PLAT.PAYMENT_CODE
  is '支付方式代码';
comment on column T_LM_B_PAYMENT_PLAT.PLATFORM_CODE
  is '应用平台代码';
comment on column T_LM_B_PAYMENT_PLAT.STATUS
  is '在线状态  1：上线  0：下线';
comment on column T_LM_B_PAYMENT_PLAT.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_PAYMENT_PLAT.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_PAYMENT_PLAT
  add constraint T_LM_B_PAYMENT_PLAT_ID_PK primary key (ID)
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
