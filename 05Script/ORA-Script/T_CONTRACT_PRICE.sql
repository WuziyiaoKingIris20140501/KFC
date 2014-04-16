-- Create table
create table T_CONTRACT_PRICE
(
  HOTEL_ID    VARCHAR2(40) not null,
  ROOM_CODE   VARCHAR2(40) not null,
  DTYPE       CHAR(1),
  DVALE       VARCHAR2(10),
  STATUS      VARCHAR2(2) default 1,
  CREATE_TIME TIMESTAMP(6),
  UPDATE_TIME TIMESTAMP(6),
  CREATE_USER VARCHAR2(100),
  UPDATE_USER VARCHAR2(100)
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
comment on table T_CONTRACT_PRICE
  is '�ȼ۾Ƶ귿�͹�����';
-- Add comments to the columns 
comment on column T_CONTRACT_PRICE.HOTEL_ID
  is '�Ƶ�ID';
comment on column T_CONTRACT_PRICE.ROOM_CODE
  is '����code';
comment on column T_CONTRACT_PRICE.DTYPE
  is '�ۿ۷�ʽ 0:δָ�� 1:�̶��ۿ� 2:�̶��۸� ';
comment on column T_CONTRACT_PRICE.DVALE
  is '�ۿ�ֵ';
comment on column T_CONTRACT_PRICE.STATUS
  is '�÷��͵�״̬1 ���� 0����';
comment on column T_CONTRACT_PRICE.CREATE_TIME
  is '����ʱ��';
comment on column T_CONTRACT_PRICE.UPDATE_TIME
  is '�޸�ʱ��';
comment on column T_CONTRACT_PRICE.CREATE_USER
  is '������';
comment on column T_CONTRACT_PRICE.UPDATE_USER
  is '�޸���';
-- Create/Recreate indexes 
create index T_CONTRACT_HOTELID_IDX on T_CONTRACT_PRICE (HOTEL_ID)
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
create index T_CONTRACT_ROOMCODE_IDX on T_CONTRACT_PRICE (ROOM_CODE)
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
