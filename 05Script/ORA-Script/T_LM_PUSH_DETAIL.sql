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
  is '��Push����Ϣ��ϸ��';
-- Add comments to the columns 
comment on column T_LM_PUSH_DETAIL.ID
  is '����ID';
comment on column T_LM_PUSH_DETAIL.PUSH_ID
  is 'PUSH_ID�ţ�����T_LM_PUSH��';
comment on column T_LM_PUSH_DETAIL.PUSH_LOGINMOBILE
  is '�û��ֻ���';
comment on column T_LM_PUSH_DETAIL.PUSH_SENDTIME
  is '��Ϣ����ʱ��';
comment on column T_LM_PUSH_DETAIL.PUSH_STATUS
  is '0-��ʾδ���ͣ�1-�ѷ���';
comment on column T_LM_PUSH_DETAIL.PUSH_USECODE
  is '�û�ʹ��ƽ̨��IOS��Android';
comment on column T_LM_PUSH_DETAIL.PUSH_CLIENTCODE
  is '������Ϣ����hotelvp';
comment on column T_LM_PUSH_DETAIL.CREATE_TIME
  is '����ʱ��';
