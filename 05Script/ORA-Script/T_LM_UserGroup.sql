-- Create table
create table T_LM_UserGroup
(
  ID		      NUMBER not null,
  USERGROUP_NAME      VARCHAR2(100),
  REGIST_START         TIMESTAMP(6),
  REGIST_END           TIMESTAMP(6),
  LOGIN_START          TIMESTAMP(6),
  LOGIN_END            TIMESTAMP(6),
  SUBMIT_ORDER_FROM     NUMBER(4,0),
  SUBMIT_ORDER_TO       NUMBER(4,0),
  COMPLETE_ORDER_FROM   NUMBER(4,0),
  COMPLETE_ORDER_TO     NUMBER(4,0),
  LAST_ORDER_START      TIMESTAMP(6),
  LAST_ORDER_END        TIMESTAMP(6),
  MANUAL_ADD           NCLOB,
  CREATE_TIME         TIMESTAMP(6),
  UPDATE_TIME         TIMESTAMP(6)
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
comment on table T_LM_UserGroup
  is '�û�����Ϣ��';
comment on column T_LM_UserGroup.ID
  is '�û���ID';
comment on column T_LM_UserGroup.USERGROUP_NAME
  is '�û�������';
comment on column T_LM_UserGroup.REGIST_START
  is 'ע��ʱ�俪ʼ��';
comment on column T_LM_UserGroup.REGIST_END
  is 'ע��ʱ�������';
comment on column T_LM_UserGroup.LOGIN_START
  is '����¼��ʼ��';
comment on column T_LM_UserGroup.LOGIN_END
  is '����¼������';
comment on column T_LM_UserGroup.SUBMIT_ORDER_FROM
  is '�����ύ����From';
comment on column T_LM_UserGroup.SUBMIT_ORDER_TO
  is '�����ύ����To';
comment on column T_LM_UserGroup.COMPLETE_ORDER_FROM
  is '�ɹ���������From';
comment on column T_LM_UserGroup.COMPLETE_ORDER_TO
  is '�ɹ���������To';
comment on column T_LM_UserGroup.LAST_ORDER_START
  is '����µ�ʱ��From';
comment on column T_LM_UserGroup.LAST_ORDER_END
  is '����µ�ʱ��To';
comment on column T_LM_UserGroup.MANUAL_ADD
  is '�ֶ����';
comment on column T_LM_UserGroup.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_UserGroup.UPDATE_TIME
  is '����ʱ��';

-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_UserGroup
  add constraint T_LM_UserGroup_ID_PK primary key (ID)
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
