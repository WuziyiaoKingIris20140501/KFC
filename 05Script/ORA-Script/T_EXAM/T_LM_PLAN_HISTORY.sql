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
  is '计划变更历史表';
-- Add comments to the columns 
comment on column T_LM_PLAN_HISTORY.ID
  is '自增字段';
comment on column T_LM_PLAN_HISTORY.REFID
  is '酒店计划ID，值参考T_LM_Plan中的ID值';
comment on column T_LM_PLAN_HISTORY.EFFECT_DATE
  is '生效日期';
comment on column T_LM_PLAN_HISTORY.SEASON
  is '季节维护';
comment on column T_LM_PLAN_HISTORY.MONEY_TYPE
  is '币种 CNY 人民币 USD 美元 HKD 港币';
comment on column T_LM_PLAN_HISTORY.HOTEL_ID
  is '引用酒店ID';
comment on column T_LM_PLAN_HISTORY.ROOM_TYPE_NAME
  is '房型名称';
comment on column T_LM_PLAN_HISTORY.ROOM_TYPE_CODE
  is '房型代码';
comment on column T_LM_PLAN_HISTORY.STATUS
  is '房态 0 关闭 1 可用 ';
comment on column T_LM_PLAN_HISTORY.ROOM_NUM
  is '房量设置';
comment on column T_LM_PLAN_HISTORY.GMT_CREATED
  is '更新时间';
comment on column T_LM_PLAN_HISTORY.CREATOR
  is '操作人';
comment on column T_LM_PLAN_HISTORY.ONE_PRICE
  is '单人价';
comment on column T_LM_PLAN_HISTORY.TWO_PRICE
  is '双人价';
comment on column T_LM_PLAN_HISTORY.THREE_PRICE
  is '三人价';
comment on column T_LM_PLAN_HISTORY.FOUR_PRICE
  is '四人价';
comment on column T_LM_PLAN_HISTORY.ATTN_PRICE
  is '加床价';
comment on column T_LM_PLAN_HISTORY.BREAKFAST_NUM
  is '早餐数量';
comment on column T_LM_PLAN_HISTORY.EACH_BREAKFAST_PRICE
  is '每份早餐价格';
comment on column T_LM_PLAN_HISTORY.IS_NETWORK
  is '是否有免费网络 0 无 1 有';
comment on column T_LM_PLAN_HISTORY.HOLD_ROOM_NUM
  is '已锁房量';
comment on column T_LM_PLAN_HISTORY.RATE_CODE
  is '价格代码，LMBAR：预付，LMBAR2：现付';
comment on column T_LM_PLAN_HISTORY.GUAID
  is '保证金ID';
comment on column T_LM_PLAN_HISTORY.CXLID
  is '取消金ID';
comment on column T_LM_PLAN_HISTORY.OFFSETVAL
  is '浮动值';
comment on column T_LM_PLAN_HISTORY.OFFSETUNIT
  is '浮动标志，0:固定值，1：百分比';
comment on column T_LM_PLAN_HISTORY.LMPRICE
  is '根据衍生公式和浮动规则计算出的LM酒店价格';
comment on column T_LM_PLAN_HISTORY.THIRDPRICE
  is 'LM酒店所对应的第三方价格';
comment on column T_LM_PLAN_HISTORY.LMSTATUS
  is 'LM对应版本酒店状态，0:LM关闭，1：LM开放；第1位表示IOS，第2位表示WAP，第3位ANDROID，第4位116114';
comment on column T_LM_PLAN_HISTORY.IS_RESERVE
  is '0：保留房；1：非保留房';
comment on column T_LM_PLAN_HISTORY.HOTELVP_STATUS
  is 'cms操作状态 0下线   1上线 ';
comment on column T_LM_PLAN_HISTORY.APP_STATUS
  is 'App上线状态，0-下线 1上线';
comment on column T_LM_PLAN_HISTORY.CREATE_TIME
  is '创建时间';
comment on column T_LM_PLAN_HISTORY.OPERATOR
  is '操作人，自动的操作则为system';
