-- Create table
create table T_LM_ORDER_COF
(
  ID                  NUMBER not null,
  ORDER_ID            NUMBER,
  FOG_ORDER_NUM       VARCHAR2(40),
  STATUS              VARCHAR2(2),
  OPE_USER            VARCHAR2(255),
  FAX_SYSTEM          VARCHAR2(255),
  FAX_NO              VARCHAR2(40),
  FAX_IMAGE_URL       VARCHAR2(1000),
  FAX_STATUS          VARCHAR2(2),
  FAX_DTIME           TIMESTAMP(6),
  FAX_NUMBER          VARCHAR2(3),
  FAX_FDTIME          TIMESTAMP(6),
  FOLLOW_UP           VARCHAR2(2),
  CREATE_TIME         TIMESTAMP(6),
  CREATE_USER         VARCHAR2(255),
  UPDATE_TIME         TIMESTAMP(6),
  UPDATE_USER         VARCHAR2(255)
)
tablespace HOTELVPTEST
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
comment on table T_LM_ORDER_COF
  is 'LM订单确认表';
-- Add comments to the columns 
comment on column T_LM_ORDER_COF.ID
  is '自增ID';
comment on column T_LM_ORDER_COF.ORDER_ID
  is '订单ID';
comment on column T_LM_ORDER_COF.FOG_ORDER_NUM
  is 'fog 定单号';
comment on column T_LM_ORDER_COF.STATUS
  is '确认状态 0 待处理 1可处理 2处理中';
comment on column T_LM_ORDER_COF.OPE_USER
  is '操作人';
comment on column T_LM_ORDER_COF.FAX_SYSTEM
  is '传真发送系统';
comment on column T_LM_ORDER_COF.FAX_NO
  is '传真号码';
comment on column T_LM_ORDER_COF.FAX_IMAGE_URL
  is '传真图片url';
comment on column T_LM_ORDER_COF.FAX_STATUS
  is '传真发送状态 0：待发送 1：已发送 2：发送失败';
comment on column T_LM_ORDER_COF.FAX_DTIME
  is '成功发送时间';
comment on column T_LM_ORDER_COF.FAX_NUMBER
  is '传真发送重试次数';
comment on column T_LM_ORDER_COF.FAX_FDTIME
  is '首次发送时间';
comment on column T_LM_ORDER_COF.FOLLOW_UP
  is '需跟进 1：是 0：否';
comment on column T_LM_ORDER_COF.CREATE_TIME
  is '订单确认创建时间';
comment on column T_LM_ORDER_COF.CREATE_USER
  is '订单确认创建认';
comment on column T_LM_ORDER_COF.UPDATE_TIME
  is '订单确认更新时间';
comment on column T_LM_ORDER_COF.UPDATE_USER
  is '订单确认更新人';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_ORDER_COF
  add constraint T_LM_ORDER_COF_PK primary key (ID)
  using index 
  tablespace HOTELVPTEST
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
