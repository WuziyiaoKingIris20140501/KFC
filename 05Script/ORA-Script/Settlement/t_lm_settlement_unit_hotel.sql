-- Create table
create table t_lm_settlement_unit_hotel
(
  Unit_ID		NUMBER not null,
  Hotel_ID		VARCHAR2(150),
  Price_Code		VARCHAR2(20),
  Vendor		VARCHAR2(20),
  STATUS		VARCHAR2(2),
  CREATE_User		VARCHAR2(100),
  CREATE_TIME		TIMESTAMP(6),
  UPDATE_User		VARCHAR2(100),
  UPDATE_TIME		TIMESTAMP(6)
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

comment on table t_lm_settlement_unit_hotel
  is '���㵥λ�����Ƶ��';
-- Add comments to the columns 
comment on column t_lm_settlement_unit_hotel.Unit_ID
  is '���㵥λID';
comment on column t_lm_settlement_unit_hotel.Hotel_ID
  is '����Ƶ�ID';
comment on column t_lm_settlement_unit_hotel.Price_Code
  is '�������� ��Ԥ�� �ָ� �ɶ�ѡ 11111��';
comment on column t_lm_settlement_unit_hotel.Vendor
  is '�Ƶ깩Ӧ��  ������ ���� Я�� ���� Ī̩ 1111111111��';
comment on column t_lm_settlement_unit_hotel.STATUS
  is '״̬  1����Ч  0����Ч';
comment on column t_lm_settlement_unit_hotel.CREATE_User
  is '������';
comment on column t_lm_settlement_unit_hotel.CREATE_TIME
  is '����ʱ��';
comment on column t_lm_settlement_unit_hotel.UPDATE_User
  is '������';
comment on column t_lm_settlement_unit_hotel.UPDATE_TIME
  is '����ʱ��';