-- Create table
create table T_LM_B_IMAGES
(
  ID            NUMBER not null,
  TITLE			VARCHAR2(200),
  IMG_TYPE		VARCHAR2(2),
  Resolution	VARCHAR2(1),
  HTP_PATH		VARCHAR2(1000),
  DNS_PATH		VARCHAR2(1000),
  STATUS		VARCHAR2(1), 
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
comment on table T_LM_B_IMAGES
  is '图片信息维护表';
-- Add comments to the columns 
comment on column T_LM_B_IMAGES.ID
  is '系统ID';
comment on column T_LM_B_IMAGES.TITLE
  is '信息标题';
comment on column T_LM_B_IMAGES.IMG_TYPE
  is '图片类型 01:促销图片';
comment on column T_LM_B_IMAGES.Resolution
  is '图片分辨率 0:  1:  2:  3:  4: ';
comment on column T_LM_B_IMAGES.HTP_PATH
  is 'HTTP图片路径';
comment on column T_LM_B_IMAGES.DNS_PATH
  is 'DNS图片路径';
comment on column T_LM_B_IMAGES.STATUS
  is '状态 0:下线 1:上线';
comment on column T_LM_B_IMAGES.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_IMAGES.UPDATE_TIME
  is '更新时间';
comment on column T_LM_B_IMAGES.OPERATE_USER
  is '维护人';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_IMAGES
  add constraint T_LM_B_IMAGES_PK primary key (ID)
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