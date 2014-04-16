-- Create table
create table T_LM_PUSH_DETAIL_HISTORY
(
  ID               NUMBER not null,
  PUSH_ID          NUMBER,
  PUSH_LOGINMOBILE VARCHAR2(40),
  PUSH_SENDTIME    TIMESTAMP(6),
  PUSH_STATUS      VARCHAR2(2),
  PUSH_USECODE     VARCHAR2(40),
  PUSH_CLIENTCODE  VARCHAR2(40),
  CREATE_TIME      TIMESTAMP(6)
)
tablespace HOTELVPTEST
  pctfree 10
  initrans 1
  maxtrans 255;
-- Add comments to the table 
comment on table T_LM_PUSH_DETAIL_HISTORY
  is 'Push消息的历史明细表';
-- Add comments to the columns 
comment on column T_LM_PUSH_DETAIL_HISTORY.ID
  is '自增ID';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_ID
  is 'PUSH_ID号';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_LOGINMOBILE
  is '用户注册手机号';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_SENDTIME
  is '消息发送时间';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_STATUS
  is '发送状态：0-表示未发送；1-表示已经发送';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_USECODE
  is '用户使用平台：IOS，Android';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_CLIENTCODE
  is '渠道，如Hotelvp';
comment on column T_LM_PUSH_DETAIL_HISTORY.CREATE_TIME
  is '创建时间';
