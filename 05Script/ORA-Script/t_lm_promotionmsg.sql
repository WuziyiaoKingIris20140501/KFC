-- Create table
create table T_LM_PROMOTIONMSG
(
  ID             NUMBER not null,
  START_DATE     DATE,
  END_DATE       DATE,
  PRO_TITLE       VARCHAR2(200),
  PRIORITY	 VARCHAR2(1),
  PRO_CONTENT    VARCHAR2(1000),
  RATE_CODE      VARCHAR2(20),
  PRO_IMAGE_ID	 VARCHAR2(100),
  STATUS	 VARCHAR2(1), 
  CREATE_TIME    TIMESTAMP(6),
  UPDATE_TIME    TIMESTAMP(6),
  OPERATE_USER   VARCHAR2(50)
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
comment on table T_LM_PROMOTIONMSG
  is '������Ϣά����';
-- Add comments to the columns 
comment on column T_LM_PROMOTIONMSG.ID
  is 'ϵͳID';
comment on column T_LM_PROMOTIONMSG.START_DATE
  is '������ʼʱ��';
comment on column T_LM_PROMOTIONMSG.END_DATE
  is '��������ʱ��';
comment on column T_LM_PROMOTIONMSG.PRO_TITLE
  is '��Ϣ����';
comment on column T_LM_PROMOTIONMSG.PRIORITY
  is '���ȼ� 1,2,3,4,5';
comment on column T_LM_PROMOTIONMSG.PRO_CONTENT
  is '������Ϣ����';
comment on column T_LM_PROMOTIONMSG.RATE_CODE
  is '�۸���� LMBAR��Ԥ�� LMBAR2���ָ�';  
comment on column T_LM_PROMOTIONMSG.PRO_IMAGE_ID
  is '������ϢͼƬID';  
comment on column T_LM_PROMOTIONMSG.STATUS
  is '״̬ 0:���� 1:����';
comment on column T_LM_PROMOTIONMSG.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_PROMOTIONMSG.UPDATE_TIME
  is '����ʱ��';
comment on column T_LM_PROMOTIONMSG.OPERATE_USER
  is 'ά����';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_PROMOTIONMSG
  add constraint T_LM_PROMOTIONMSG_PK primary key (ID)
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