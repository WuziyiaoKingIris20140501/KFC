-- Create table
create table T_LM_PUSH
(
  PUSH_ID            NUMBER,
  PUSH_TITLE         VARCHAR2(80),
  PUSH_CONTENT       VARCHAR2(600),
  PUSH_SUCCESS_COUNT NUMBER,
  CREATE_TIME        TIMESTAMP(6) default sysdate,
  UPDATE_TIME        TIMESTAMP(6),
  PUSH_MEMO          VARCHAR2(200),
  PUSH_OBJECT        VARCHAR2(400)
)
tablespace HOTELVPTEST
  pctfree 10
  initrans 1
  maxtrans 255;
-- Add comments to the table 
comment on table T_LM_PUSH
  is 'Push��Ϣ����';
-- Add comments to the columns 
comment on column T_LM_PUSH.PUSH_ID
  is '����ID';
comment on column T_LM_PUSH.PUSH_TITLE
  is 'Push�ı���';
comment on column T_LM_PUSH.PUSH_CONTENT
  is 'push������';
comment on column T_LM_PUSH.PUSH_SUCCESS_COUNT
  is '���ͳɹ�������';
comment on column T_LM_PUSH.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_PUSH.UPDATE_TIME
  is '�޸�ʱ��';
comment on column T_LM_PUSH.PUSH_MEMO
  is '��ע';
comment on column T_LM_PUSH.PUSH_OBJECT
  is 'push��Ϣ�Ķ�������Щ�û���';
