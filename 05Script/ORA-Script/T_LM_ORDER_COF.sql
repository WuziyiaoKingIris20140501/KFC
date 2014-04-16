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
  is 'LM����ȷ�ϱ�';
-- Add comments to the columns 
comment on column T_LM_ORDER_COF.ID
  is '����ID';
comment on column T_LM_ORDER_COF.ORDER_ID
  is '����ID';
comment on column T_LM_ORDER_COF.FOG_ORDER_NUM
  is 'fog ������';
comment on column T_LM_ORDER_COF.STATUS
  is 'ȷ��״̬ 0 ������ 1�ɴ��� 2������';
comment on column T_LM_ORDER_COF.OPE_USER
  is '������';
comment on column T_LM_ORDER_COF.FAX_SYSTEM
  is '���淢��ϵͳ';
comment on column T_LM_ORDER_COF.FAX_NO
  is '�������';
comment on column T_LM_ORDER_COF.FAX_IMAGE_URL
  is '����ͼƬurl';
comment on column T_LM_ORDER_COF.FAX_STATUS
  is '���淢��״̬ 0�������� 1���ѷ��� 2������ʧ��';
comment on column T_LM_ORDER_COF.FAX_DTIME
  is '�ɹ�����ʱ��';
comment on column T_LM_ORDER_COF.FAX_NUMBER
  is '���淢�����Դ���';
comment on column T_LM_ORDER_COF.FAX_FDTIME
  is '�״η���ʱ��';
comment on column T_LM_ORDER_COF.FOLLOW_UP
  is '����� 1���� 0����';
comment on column T_LM_ORDER_COF.CREATE_TIME
  is '����ȷ�ϴ���ʱ��';
comment on column T_LM_ORDER_COF.CREATE_USER
  is '����ȷ�ϴ�����';
comment on column T_LM_ORDER_COF.UPDATE_TIME
  is '����ȷ�ϸ���ʱ��';
comment on column T_LM_ORDER_COF.UPDATE_USER
  is '����ȷ�ϸ�����';
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
