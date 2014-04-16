-- Create table
create table T_LM_B_PAYMENT_PLAT
(
  ID		NUMBER not null,
  PAYMENT_CODE	VARCHAR2(40),
  PLATFORM_CODE VARCHAR2(40),
  STATUS	VARCHAR2(2),
  CREATE_TIME   TIMESTAMP(6),
  UPDATE_TIME   TIMESTAMP(6)
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
comment on table T_LM_B_PAYMENT_PLAT
  is '������Ӧ��ƽ̨��ϵ��Ϣ��';
comment on column T_LM_B_PAYMENT_PLAT.ID
  is 'ϵͳID';
comment on column T_LM_B_PAYMENT_PLAT.PAYMENT_CODE
  is '֧����ʽ����';
comment on column T_LM_B_PAYMENT_PLAT.PLATFORM_CODE
  is 'Ӧ��ƽ̨����';
comment on column T_LM_B_PAYMENT_PLAT.STATUS
  is '����״̬  1������  0������';
comment on column T_LM_B_PAYMENT_PLAT.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_PAYMENT_PLAT.UPDATE_TIME
  is '����ʱ��';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_PAYMENT_PLAT
  add constraint T_LM_B_PAYMENT_PLAT_ID_PK primary key (ID)
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
