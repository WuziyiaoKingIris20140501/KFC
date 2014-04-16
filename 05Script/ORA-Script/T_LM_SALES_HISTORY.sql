-- Create table
create table T_LM_SALES_HISTORY
(
  ID                  NUMBER not null,
  HOTEL_ID            VARCHAR2(40),
  SALES_ACCOUNT       VARCHAR2(100),
  STATUS              VARCHAR2(1),
  START_DATE          DATE,
  END_DATE            DATE
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
comment on table T_LM_SALES_HISTORY
  is 'LM������';
-- Add comments to the columns 
comment on column T_LM_SALES_HISTORY.ID
  is '����ID';
comment on column T_LM_SALES_HISTORY.HOTEL_ID
  is '�Ƶ�ID';
comment on column T_LM_SALES_HISTORY.SALES_ACCOUNT
  is '������ԱID';
comment on column T_LM_SALES_HISTORY.STATUS
  is '״̬';
comment on column T_LM_SALES_HISTORY.START_DATE
  is '����ʼ����';
comment on column T_LM_SALES_HISTORY.END_DATE
  is '�����������';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_SALES_HISTORY
  add constraint T_LM_SALES_HISTORY_PK primary key (ID)
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
create index T_LM_SALES_HOTEL_ID_IDX on T_LM_SALES_HISTORY (HOTEL_ID)
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
