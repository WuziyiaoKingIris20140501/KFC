-- Create table
create table t_lm_b_facilities
(
  ID		NUMBER not null,
  FACILITIES_CODE VARCHAR2(60),
  NAME_ZH	VARCHAR2(300),
  NAME_EN	VARCHAR2(300),
  TYPE		VARCHAR2(40),
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
comment on table t_lm_b_facilities
  is '�Ƶ������ʩ�����';
comment on column t_lm_b_facilities.ID
  is 'ϵͳID';
comment on column t_lm_b_facilities.FACILITIES_CODE
  is '������ʩCODE  P��ͷ5λ��0 �Զ�����';
comment on column t_lm_b_facilities.NAME_ZH
  is '������ʩ��������';
comment on column t_lm_b_facilities.NAME_EN
  is '������ʩӢ������';
comment on column t_lm_b_facilities.TYPE
  is '������ʩ����';
comment on column t_lm_b_facilities.STATUS
  is '����״̬  1������  0������';
comment on column t_lm_b_facilities.CREATE_TIME
  is '����ʱ��';
comment on column t_lm_b_facilities.UPDATE_TIME
  is '����ʱ��';

-- Create/Recreate primary, unique and foreign key constraints 
alter table t_lm_b_facilities
  add constraint t_lm_b_facilities_ID_PK primary key (ID)
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
