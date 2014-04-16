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
  is 'Push��Ϣ����ʷ��ϸ��';
-- Add comments to the columns 
comment on column T_LM_PUSH_DETAIL_HISTORY.ID
  is '����ID';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_ID
  is 'PUSH_ID��';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_LOGINMOBILE
  is '�û�ע���ֻ���';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_SENDTIME
  is '��Ϣ����ʱ��';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_STATUS
  is '����״̬��0-��ʾδ���ͣ�1-��ʾ�Ѿ�����';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_USECODE
  is '�û�ʹ��ƽ̨��IOS��Android';
comment on column T_LM_PUSH_DETAIL_HISTORY.PUSH_CLIENTCODE
  is '��������Hotelvp';
comment on column T_LM_PUSH_DETAIL_HISTORY.CREATE_TIME
  is '����ʱ��';
