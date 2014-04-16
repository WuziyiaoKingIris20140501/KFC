-- Create table
create table T_LM_PLAN_HISTORY
(
  ID                   NUMBER not null,
  REFID                NUMBER,
  EFFECT_DATE          DATE,
  SEASON               VARCHAR2(50),
  MONEY_TYPE           VARCHAR2(3),
  HOTEL_ID             VARCHAR2(20),
  ROOM_TYPE_NAME       VARCHAR2(100),
  ROOM_TYPE_CODE       VARCHAR2(50),
  STATUS               CHAR(1),
  ROOM_NUM             NUMBER(3),
  GMT_CREATED          TIMESTAMP(6),
  CREATOR              VARCHAR2(200),
  ONE_PRICE            NUMBER(8,2),
  TWO_PRICE            NUMBER(8,2),
  THREE_PRICE          NUMBER(8,2),
  FOUR_PRICE           NUMBER(8,2),
  ATTN_PRICE           NUMBER(8,2),
  BREAKFAST_NUM        NUMBER(2),
  EACH_BREAKFAST_PRICE NUMBER(8,2),
  IS_NETWORK           CHAR(1),
  GMT_MODIFIED         TIMESTAMP(6),
  MODIFIER             VARCHAR2(50),
  IS_DELETED           CHAR(1),
  HOLD_ROOM_NUM        NUMBER(3),
  RATE_CODE            VARCHAR2(20),
  GUAID                VARCHAR2(40),
  CXLID                VARCHAR2(40),
  OFFSETVAL            NUMBER(8,2),
  OFFSETUNIT           VARCHAR2(2),
  LMPRICE              NUMBER(8,2),
  THIRDPRICE           NUMBER(8,2),
  LMSTATUS             VARCHAR2(64),
  IS_RESERVE           VARCHAR2(2),
  HOTELVP_STATUS       CHAR(1),
  APP_STATUS           CHAR(1),
  CREATE_TIME          TIMESTAMP(6) default SYSDATE,
  OPERATOR             VARCHAR2(50)
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
comment on table T_LM_PLAN_HISTORY
  is '�ƻ������ʷ��';
-- Add comments to the columns 
comment on column T_LM_PLAN_HISTORY.ID
  is '�����ֶ�';
comment on column T_LM_PLAN_HISTORY.REFID
  is '�Ƶ�ƻ�ID��ֵ�ο�T_LM_Plan�е�IDֵ';
comment on column T_LM_PLAN_HISTORY.EFFECT_DATE
  is '��Ч����';
comment on column T_LM_PLAN_HISTORY.SEASON
  is '����ά��';
comment on column T_LM_PLAN_HISTORY.MONEY_TYPE
  is '���� CNY ����� USD ��Ԫ HKD �۱�';
comment on column T_LM_PLAN_HISTORY.HOTEL_ID
  is '���þƵ�ID';
comment on column T_LM_PLAN_HISTORY.ROOM_TYPE_NAME
  is '��������';
comment on column T_LM_PLAN_HISTORY.ROOM_TYPE_CODE
  is '���ʹ���';
comment on column T_LM_PLAN_HISTORY.STATUS
  is '��̬ 0 �ر� 1 ���� ';
comment on column T_LM_PLAN_HISTORY.ROOM_NUM
  is '��������';
comment on column T_LM_PLAN_HISTORY.GMT_CREATED
  is '����ʱ��';
comment on column T_LM_PLAN_HISTORY.CREATOR
  is '������';
comment on column T_LM_PLAN_HISTORY.ONE_PRICE
  is '���˼�';
comment on column T_LM_PLAN_HISTORY.TWO_PRICE
  is '˫�˼�';
comment on column T_LM_PLAN_HISTORY.THREE_PRICE
  is '���˼�';
comment on column T_LM_PLAN_HISTORY.FOUR_PRICE
  is '���˼�';
comment on column T_LM_PLAN_HISTORY.ATTN_PRICE
  is '�Ӵ���';
comment on column T_LM_PLAN_HISTORY.BREAKFAST_NUM
  is '�������';
comment on column T_LM_PLAN_HISTORY.EACH_BREAKFAST_PRICE
  is 'ÿ����ͼ۸�';
comment on column T_LM_PLAN_HISTORY.IS_NETWORK
  is '�Ƿ���������� 0 �� 1 ��';
comment on column T_LM_PLAN_HISTORY.HOLD_ROOM_NUM
  is '��������';
comment on column T_LM_PLAN_HISTORY.RATE_CODE
  is '�۸���룬LMBAR��Ԥ����LMBAR2���ָ�';
comment on column T_LM_PLAN_HISTORY.GUAID
  is '��֤��ID';
comment on column T_LM_PLAN_HISTORY.CXLID
  is 'ȡ����ID';
comment on column T_LM_PLAN_HISTORY.OFFSETVAL
  is '����ֵ';
comment on column T_LM_PLAN_HISTORY.OFFSETUNIT
  is '������־��0:�̶�ֵ��1���ٷֱ�';
comment on column T_LM_PLAN_HISTORY.LMPRICE
  is '����������ʽ�͸�������������LM�Ƶ�۸�';
comment on column T_LM_PLAN_HISTORY.THIRDPRICE
  is 'LM�Ƶ�����Ӧ�ĵ������۸�';
comment on column T_LM_PLAN_HISTORY.LMSTATUS
  is 'LM��Ӧ�汾�Ƶ�״̬��0:LM�رգ�1��LM���ţ���1λ��ʾIOS����2λ��ʾWAP����3λANDROID����4λ116114';
comment on column T_LM_PLAN_HISTORY.IS_RESERVE
  is '0����������1���Ǳ�����';
comment on column T_LM_PLAN_HISTORY.HOTELVP_STATUS
  is 'cms����״̬ 0����   1���� ';
comment on column T_LM_PLAN_HISTORY.APP_STATUS
  is 'App����״̬��0-���� 1����';
comment on column T_LM_PLAN_HISTORY.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_PLAN_HISTORY.OPERATOR
  is '�����ˣ��Զ��Ĳ�����Ϊsystem';
