-- Create table
create table T_LM_PUSH_DETAIL
(
  ID               NUMBER not null,
  PUSH_ID          NUMBER,
  PUSH_LOGINMOBILE VARCHAR2(40),
  PUSH_SENDTIME    TIMESTAMP(6),
  PUSH_STATUS      VARCHAR2(2),
  PUSH_USECODE     VARCHAR2(40),
  PUSH_CLIENTCODE  VARCHAR2(40),
  CREATE_TIME      TIMESTAMP(6) default SYSDATE
)
tablespace HOTELVPTEST
  pctfree 10
  initrans 1
  maxtrans 255;
-- Add comments to the table 
comment on table T_LM_PUSH_DETAIL
  is '待Push的消息明细表';
-- Add comments to the columns 
comment on column T_LM_PUSH_DETAIL.ID
  is '自增ID';
comment on column T_LM_PUSH_DETAIL.PUSH_ID
  is 'PUSH_ID号，管理T_LM_PUSH表';
comment on column T_LM_PUSH_DETAIL.PUSH_LOGINMOBILE
  is '用户手机号';
comment on column T_LM_PUSH_DETAIL.PUSH_SENDTIME
  is '消息发送时间';
comment on column T_LM_PUSH_DETAIL.PUSH_STATUS
  is '0-表示未发送；1-已发送';
comment on column T_LM_PUSH_DETAIL.PUSH_USECODE
  is '用户使用平台，IOS，Android';
comment on column T_LM_PUSH_DETAIL.PUSH_CLIENTCODE
  is '渠道信息，如hotelvp';
comment on column T_LM_PUSH_DETAIL.CREATE_TIME
  is '创建时间';
