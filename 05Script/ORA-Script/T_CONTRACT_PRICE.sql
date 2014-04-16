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
  is '比价酒店房型关联表';
-- Add comments to the columns 
comment on column T_CONTRACT_PRICE.HOTEL_ID
  is '酒店ID';
comment on column T_CONTRACT_PRICE.ROOM_CODE
  is '房型code';
comment on column T_CONTRACT_PRICE.DTYPE
  is '折扣方式 0:未指定 1:固定折扣 2:固定价格 ';
comment on column T_CONTRACT_PRICE.DVALE
  is '折扣值';
comment on column T_CONTRACT_PRICE.STATUS
  is '该房型的状态1 上线 0下线';
comment on column T_CONTRACT_PRICE.CREATE_TIME
  is '创建时间';
comment on column T_CONTRACT_PRICE.UPDATE_TIME
  is '修改时间';
comment on column T_CONTRACT_PRICE.CREATE_USER
  is '创建人';
comment on column T_CONTRACT_PRICE.UPDATE_USER
  is '修改人';
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
