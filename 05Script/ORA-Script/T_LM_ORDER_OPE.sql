-- Create table
create table T_LM_ORDER_OPE
(
  ID                  NUMBER not null,
  FOG_ORDER_NUM       VARCHAR2(40),
  BOOK_STATUS         VARCHAR2(2),
  PAY_STATUS          VARCHAR2(2) default 0,
  BOOK_REMARK         VARCHAR2(255),
  ORDER_CANCLE_REASON VARCHAR2(255),
  BOOK_STATUS_OTHER   VARCHAR2(2),
  FOG_RESVTYPE        VARCHAR2(2),
  FOG_RESVSTATUS      VARCHAR2(2),
  FOG_AUDITSTATUS     VARCHAR2(2),
  STATUS              VARCHAR2(2),
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
comment on table T_LM_ORDER_OPE
  is 'LM����������';
-- Add comments to the columns 
comment on column T_LM_ORDER_OPE.ID
  is '����ID';
comment on column T_LM_ORDER_OPE.CREATE_TIME
  is '��������ʱ��';
comment on column T_LM_ORDER_OPE.BOOK_STATUS
  is 'Ԥ��״̬ 0 �½� 1�½���FOG�ɹ�  2�½���fogʧ�� 3 ��ʱ4ȡ���� 5 �ɹ�';
comment on column T_LM_ORDER_OPE.PAY_STATUS
  is '֧��״̬ 0δ֧�� 1:֧���ɹ� 2:�ȴ�֧�� 3:֧���� 4:֧��ʧ�� 5:֧��ȷ���� 6:�쳣ȡ��';
comment on column T_LM_ORDER_OPE.FOG_ORDER_NUM
  is 'fog ������';
comment on column T_LM_ORDER_OPE.BOOK_REMARK
  is '������ע';
comment on column T_LM_ORDER_OPE.ORDER_CANCLE_REASON
  is 'ȡ������ԭ��';
comment on column T_LM_ORDER_OPE.BOOK_STATUS_OTHER
  is '�ָ�����״̬
    0 �½�  
    1  Ԥ���ɹ��ȴ�ȷ�� 
  2
 �½���fogʧ��    3 �û�ȡ��
    4 ����ס��ȷ��    5 NO-SHOW
 
    6 ����ɣ�������סʱ�䣩
    7 �����
    8 ���
   9 CCȡ��';
comment on column T_LM_ORDER_OPE.FOG_RESVTYPE
  is 'FOG�������� n���µ� e���޸ĵ� c��ȡ����';
comment on column T_LM_ORDER_OPE.FOG_RESVSTATUS
  is 'FOG����״̬  0�����Ƶ�ȷ�� 1���Ƶ���ȷ��';
comment on column T_LM_ORDER_OPE.FOG_AUDITSTATUS
  is 'FOG�������״̬ 5��NoShow 7����ס�� 8�� ���';
comment on column T_LM_ORDER_OPE.STATUS
  is '��������״̬ 1����Ч 0����Ч ';
  
comment on column T_LM_ORDER_OPE.CREATE_TIME
  is '������������ʱ��'; 
comment on column T_LM_ORDER_OPE.CREATE_USER
  is '��������������'; 
comment on column T_LM_ORDER_OPE.UPDATE_TIME
  is '������������ʱ��';  
comment on column T_LM_ORDER_OPE.UPDATE_USER
  is '��������������';  
  
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_ORDER_OPE
  add constraint T_LM_ORDER_OPE_PK primary key (ID)
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
