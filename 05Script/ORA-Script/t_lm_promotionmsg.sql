-- Create table
create table T_LM_PROMOTIONMSG
(
  ID             NUMBER not null,
  START_DATE     DATE,
  END_DATE       DATE,
  PRO_TITLE       VARCHAR2(200),
  PRIORITY	 VARCHAR2(1),
  PRO_CONTENT    VARCHAR2(1000),
  RATE_CODE      VARCHAR2(20),
  PRO_IMAGE_ID	 VARCHAR2(100),
  STATUS	 VARCHAR2(1), 
  CREATE_TIME    TIMESTAMP(6),
  UPDATE_TIME    TIMESTAMP(6),
  OPERATE_USER   VARCHAR2(50)
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
-- Add comments to the table 
comment on table T_LM_PROMOTIONMSG
  is '促销信息维护表';
-- Add comments to the columns 
comment on column T_LM_PROMOTIONMSG.ID
  is '系统ID';
comment on column T_LM_PROMOTIONMSG.START_DATE
  is '持续开始时间';
comment on column T_LM_PROMOTIONMSG.END_DATE
  is '持续结束时间';
comment on column T_LM_PROMOTIONMSG.PRO_TITLE
  is '信息标题';
comment on column T_LM_PROMOTIONMSG.PRIORITY
  is '优先级 1,2,3,4,5';
comment on column T_LM_PROMOTIONMSG.PRO_CONTENT
  is '促销信息正文';
comment on column T_LM_PROMOTIONMSG.RATE_CODE
  is '价格代码 LMBAR：预付 LMBAR2：现付';  
comment on column T_LM_PROMOTIONMSG.PRO_IMAGE_ID
  is '促销信息图片ID';  
comment on column T_LM_PROMOTIONMSG.STATUS
  is '状态 0:下线 1:上线';
comment on column T_LM_PROMOTIONMSG.CREATE_TIME
  is '创建时间';
comment on column T_LM_PROMOTIONMSG.UPDATE_TIME
  is '更新时间';
comment on column T_LM_PROMOTIONMSG.OPERATE_USER
  is '维护人';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_PROMOTIONMSG
  add constraint T_LM_PROMOTIONMSG_PK primary key (ID)
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