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
  is '������Ϣ��';
-- Add comments to the columns 
comment on column T_LM_B_CITY.ID
  is 'ID';
comment on column T_LM_B_CITY.CITY_ID
  is '����ID';
comment on column T_LM_B_CITY.AREA_ID
  is '����ID';
comment on column T_LM_B_CITY.NAME_CN
  is '��������';
comment on column T_LM_B_CITY.NAME_EN
  is 'Ӣ������';
comment on column T_LM_B_CITY.SEQ
  is '����';
comment on column T_LM_B_CITY.DAREAID
  is '����ID';
comment on column FOG_T_CITY.GBCODE
  is '���ұ�׼����';
comment on column FOG_T_CITY.PINYIN
  is 'ƴ��';
comment on column FOG_T_CITY.PINYIN_SHORT
  is 'ƴ����д';
comment on column FOG_T_CITY.SNAME
  is '���ж�ȫ��';
comment on column FOG_T_CITY.LNAME
  is '���г�ȫ��';
comment on column FOG_T_CITY.ZIPCODE
  is '��������';
comment on column FOG_T_CITY.PHONECODE
  is '�绰����';
comment on column FOG_T_CITY.HOTELNUM
  is '��ʽ�Ƶ�����';
comment on column FOG_T_CITY.LONGITUDE
  is '����';
comment on column FOG_T_CITY.LATITUDE
  is 'γ��';
comment on column T_LM_B_CITY.STATUS
  is '����״̬  1������  0������';
comment on column T_LM_B_CITY.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_CITY.UPDATE_TIME
  is '����ʱ��';
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