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
  is 'LM订单操作表';
-- Add comments to the columns 
comment on column T_LM_ORDER_OPE.ID
  is '自增ID';
comment on column T_LM_ORDER_OPE.CREATE_TIME
  is '定单创建时间';
comment on column T_LM_ORDER_OPE.BOOK_STATUS
  is '预定状态 0 新建 1新建入FOG成功  2新建入fog失败 3 超时4取消单 5 成功';
comment on column T_LM_ORDER_OPE.PAY_STATUS
  is '支付状态 0未支付 1:支付成功 2:等待支付 3:支付中 4:支付失败 5:支付确认中 6:异常取消';
comment on column T_LM_ORDER_OPE.FOG_ORDER_NUM
  is 'fog 定单号';
comment on column T_LM_ORDER_OPE.BOOK_REMARK
  is '订单备注';
comment on column T_LM_ORDER_OPE.ORDER_CANCLE_REASON
  is '取消订单原因';
comment on column T_LM_ORDER_OPE.BOOK_STATUS_OTHER
  is '现付订单状态
    0 新建  
    1  预订成功等待确认 
  2
 新建入fog失败    3 用户取消
    4 可入住已确认    5 NO-SHOW
 
    6 已完成（超过入住时间）
    7 审核中
    8 离店
   9 CC取消';
comment on column T_LM_ORDER_OPE.FOG_RESVTYPE
  is 'FOG订单类型 n：新单 e：修改单 c：取消单';
comment on column T_LM_ORDER_OPE.FOG_RESVSTATUS
  is 'FOG订单状态  0：待酒店确认 1：酒店已确认';
comment on column T_LM_ORDER_OPE.FOG_AUDITSTATUS
  is 'FOG订单审核状态 5：NoShow 7：入住中 8： 离店';
comment on column T_LM_ORDER_OPE.STATUS
  is '订单操作状态 1：有效 0：无效 ';
  
comment on column T_LM_ORDER_OPE.CREATE_TIME
  is '订单操作创建时间'; 
comment on column T_LM_ORDER_OPE.CREATE_USER
  is '订单操作创建认'; 
comment on column T_LM_ORDER_OPE.UPDATE_TIME
  is '订单操作更新时间';  
comment on column T_LM_ORDER_OPE.UPDATE_USER
  is '订单操作更新人';  
  
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
