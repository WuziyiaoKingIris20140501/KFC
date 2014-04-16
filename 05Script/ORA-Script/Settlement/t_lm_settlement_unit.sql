-- Create table
create table t_lm_settlement_unit
(
  ID			NUMBER not null,
  Unit_Name		VARCHAR2(150),
  Invoice_Name		VARCHAR2(150),
  Settlement_Term	VARCHAR2(5),
  Term_STDT		VARCHAR2(5),
  Hotel_Tax		NUMBER(5,2),
  Settlement_Address	VARCHAR2(300),
  Settlement_Per	VARCHAR2(100),
  Settlement_Tel	VARCHAR2(30),
  Settlement_Fax	VARCHAR2(30),
  Settlement_Sales	VARCHAR2(100),
  
  bill_item		VARCHAR2(300),
  Hotel_TaxNo		VARCHAR2(100),
  Hotel_PayNo		VARCHAR2(100),
  Remark		VARCHAR2(300),
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

comment on table t_lm_settlement_unit
  is '结算单位表';
-- Add comments to the columns 
comment on column t_lm_settlement_unit.ID
  is '结算单位ID（自动生成）';
comment on column t_lm_settlement_unit.Unit_Name
  is '结算单位名称';
comment on column t_lm_settlement_unit.Invoice_Name
  is '开票抬头';
comment on column t_lm_settlement_unit.Settlement_Term
  is '结算账期';
comment on column t_lm_settlement_unit.Hotel_Tax
  is '酒店税点';
comment on column t_lm_settlement_unit.Settlement_Address
  is '结算地址';
comment on column t_lm_settlement_unit.Settlement_Per
  is '结算联系人';
comment on column t_lm_settlement_unit.Settlement_Tel
  is '结算联系电话';
comment on column t_lm_settlement_unit.Settlement_Fax
  is '结算联系传真';
comment on column t_lm_settlement_unit.Settlement_Sales
  is '结算负责销售';
comment on column t_lm_settlement_unit.bill_item
  is '发票项目';
comment on column t_lm_settlement_unit.Hotel_TaxNo
  is '酒店税号';
comment on column t_lm_settlement_unit.Hotel_PayNo
  is '付款账号';
comment on column t_lm_settlement_unit.STATUS
  is '状态  1：上线  0：下线';
comment on column t_lm_settlement_unit.CREATE_User
  is '创建人';
comment on column t_lm_settlement_unit.CREATE_TIME
  is '创建时间';
comment on column t_lm_settlement_unit.UPDATE_User
  is '更新人';
comment on column t_lm_settlement_unit.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table t_lm_settlement_unit
  add constraint t_lm_settlement_unit_ID_PK primary key (ID)
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


  -- Create sequence 
create sequence t_lm_settlement_unit_SEQ
minvalue 0
maxvalue 9999999999999999999999999999
start with 1
increment by 1
cache 5;