-- Create table
create table T_LM_B_CITY
(
  ID          NUMBER not null,
  CITY_ID       VARCHAR2(40) not null,
  AREA_ID       VARCHAR2(40),
  NAME_CN      VARCHAR2(30),
  NAME_EN      VARCHAR2(30),
  NAME_JP      VARCHAR2(30),
  SEQ          INTEGER default 0,
  DAREAID      VARCHAR2(40),
  GBCODE       VARCHAR2(40),
  PINYIN       VARCHAR2(40),
  PINYIN_SHORT VARCHAR2(40),
  SNAME        VARCHAR2(40),
  LNAME        VARCHAR2(100),
  ZIPCODE      VARCHAR2(10),
  PHONECODE    NUMBER(8),
  HOTELNUM     NUMBER(8) default 0,
  LONGITUDE    VARCHAR2(20),
  LATITUDE     VARCHAR2(20),
  STATUS      VARCHAR2(2),
  CREATE_TIME TIMESTAMP(6),
  UPDATE_TIME TIMESTAMP(6)
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
comment on table T_LM_B_CITY
  is '城市信息表';
-- Add comments to the columns 
comment on column T_LM_B_CITY.ID
  is 'ID';
comment on column T_LM_B_CITY.CITY_ID
  is '城市ID';
comment on column T_LM_B_CITY.AREA_ID
  is '区域ID';
comment on column T_LM_B_CITY.NAME_CN
  is '中文名称';
comment on column T_LM_B_CITY.NAME_EN
  is '英文名称';
comment on column T_LM_B_CITY.SEQ
  is '排序';
comment on column T_LM_B_CITY.DAREAID
  is '国家ID';
comment on column FOG_T_CITY.GBCODE
  is '国家标准代码';
comment on column FOG_T_CITY.PINYIN
  is '拼音';
comment on column FOG_T_CITY.PINYIN_SHORT
  is '拼音缩写';
comment on column FOG_T_CITY.SNAME
  is '城市短全名';
comment on column FOG_T_CITY.LNAME
  is '城市长全名';
comment on column FOG_T_CITY.ZIPCODE
  is '邮政编码';
comment on column FOG_T_CITY.PHONECODE
  is '电话区号';
comment on column FOG_T_CITY.HOTELNUM
  is '正式酒店数量';
comment on column FOG_T_CITY.LONGITUDE
  is '经度';
comment on column FOG_T_CITY.LATITUDE
  is '纬度';
comment on column T_LM_B_CITY.STATUS
  is '在线状态  1：上线  0：下线';
comment on column T_LM_B_CITY.CREATE_TIME
  is '创建时间';
comment on column T_LM_B_CITY.UPDATE_TIME
  is '更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_CITY
  add constraint T_LM_B_CITY_ID_PK primary key (ID)
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