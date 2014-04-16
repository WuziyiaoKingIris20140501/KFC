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
  is '结算单位包含酒店表';
-- Add comments to the columns 
comment on column t_lm_settlement_unit_hotel.Unit_ID
  is '结算单位ID';
comment on column t_lm_settlement_unit_hotel.Hotel_ID
  is '结算酒店ID';
comment on column t_lm_settlement_unit_hotel.Price_Code
  is '订单类型 （预付 现付 可多选 11111）';
comment on column t_lm_settlement_unit_hotel.Vendor
  is '酒店供应商  （所有 艺龙 携程 布丁 莫泰 1111111111）';
comment on column t_lm_settlement_unit_hotel.STATUS
  is '状态  1：有效  0：无效';
comment on column t_lm_settlement_unit_hotel.CREATE_User
  is '创建人';
comment on column t_lm_settlement_unit_hotel.CREATE_TIME
  is '创建时间';
comment on column t_lm_settlement_unit_hotel.UPDATE_User
  is '更新人';
comment on column t_lm_settlement_unit_hotel.UPDATE_TIME
  is '更新时间';