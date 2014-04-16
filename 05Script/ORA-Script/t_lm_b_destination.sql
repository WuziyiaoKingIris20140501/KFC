-- Create table
create table T_LM_B_DESTINATION
(
  ID		NUMBER not null,
  CITY_ID	VARCHAR2(40),
  TYPE_ID	VARCHAR2(40),
  NAME_CN	VARCHAR2(300),
  NAME_EN	VARCHAR2(300),
  ADDRESS_CN	VARCHAR2(300),
  ADDRESS_EN	VARCHAR2(300),
  TEL_ST	VARCHAR2(40),
  TEL_LG	VARCHAR2(40),
  LATITUDE	VARCHAR2(40),
  LONGITUDE	VARCHAR2(40),
  STATUS	VARCHAR2(2),
  CREATE_TIME	TIMESTAMP(6),
  UPDATE_TIME	TIMESTAMP(6)
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
comment on table T_LM_B_DESTINATION
  is 'Ŀ�ĵ���Ϣ��';
-- Add comments to the columns 
comment on column T_LM_B_DESTINATION.ID
  is 'ID';
comment on column T_LM_B_DESTINATION.CITY_ID
  is '����CODE';
comment on column T_LM_B_DESTINATION.TYPE_ID
  is 'Ŀ�ĵ�����ID';
comment on column T_LM_B_DESTINATION.NAME_CN
  is '��������';
comment on column T_LM_B_DESTINATION.NAME_EN
  is 'Ӣ������';
comment on column T_LM_B_DESTINATION.ADDRESS_CN
  is '���ĵ�ַ����';
comment on column T_LM_B_DESTINATION.ADDRESS_EN
  is 'Ӣ�ĵ�ַ����';
comment on column T_LM_B_DESTINATION.TEL_ST
  is '�绰����';
comment on column T_LM_B_DESTINATION.TEL_LG
  is '�绰����';
comment on column T_LM_B_DESTINATION.LATITUDE
  is 'γ��';
comment on column T_LM_B_DESTINATION.LONGITUDE
  is '����';
comment on column T_LM_B_DESTINATION.STATUS
  is '����״̬  1������  0������';
comment on column T_LM_B_DESTINATION.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_DESTINATION.UPDATE_TIME
  is '����ʱ��';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_DESTINATION
  add constraint T_LM_B_DESTINATION_ID_PK primary key (ID)
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
