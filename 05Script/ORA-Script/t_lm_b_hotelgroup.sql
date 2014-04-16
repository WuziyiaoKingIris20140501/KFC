-- Create table
create table t_lm_b_hotelgroup
(
  ID			NUMBER not null,
  GROUP_CODE		VARCHAR2(60),
  NAME_ZH		VARCHAR2(300),
  NAME_EN		VARCHAR2(300),
  DESCRIPTION_ZH	VARCHAR2(800),
  DESCRIPTION_EN	VARCHAR2(800),
  BANDTYPE		VARCHAR2(2),
  STATUS		VARCHAR2(2),
  CREATE_TIME		TIMESTAMP(6),
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
comment on table t_lm_b_hotelgroup
  is '�Ƶ꼯�Ź����';
comment on column t_lm_b_hotelgroup.ID
  is 'ϵͳID';
comment on column t_lm_b_hotelgroup.GROUP_CODE
  is '�Ƶ꼯��CODE';
comment on column t_lm_b_hotelgroup.NAME_ZH
  is '�Ƶ꼯����������';
comment on column t_lm_b_hotelgroup.NAME_EN
  is '�Ƶ꼯��Ӣ������';
comment on column t_lm_b_hotelgroup.DESCRIPTION_ZH
  is '�Ƶ꼯��������������';
comment on column t_lm_b_hotelgroup.DESCRIPTION_EN
  is '�Ƶ꼯������Ӣ������';
comment on column t_lm_b_hotelgroup.BANDTYPE
  is '�Ƶ꼯������ H:������ J:������';
comment on column t_lm_b_hotelgroup.STATUS
  is '����״̬  1������  0������';
comment on column t_lm_b_hotelgroup.CREATE_TIME
  is '����ʱ��';
comment on column t_lm_b_hotelgroup.UPDATE_TIME
  is '����ʱ��';

-- Create/Recreate primary, unique and foreign key constraints 
alter table t_lm_b_hotelgroup
  add constraint t_lm_b_hotelgroup_ID_PK primary key (ID)
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
