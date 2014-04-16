-- Create table
create table T_LM_ORDER_SETTLE
(
  HOTELID      VARCHAR2(50),
  MONTHS       VARCHAR2(200),
  OPE_USER     VARCHAR2(20),
  CREATE_TIME TIMESTAMP(6),
  UPDATE_TIME TIMESTAMP(6)
);

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_ORDER_SETTLE
  add constraint T_LM_ORDER_SETTLE_PK primary key (HOTELID)
