-- Create table
create table T_LM_UGROUP_ULT
(
  ID		NUMBER,
  USERGROUP_ID  VARCHAR2(40),
  LOGIN_MOBILE  VARCHAR2(40),
  ADD_TYPE     VARCHAR2(2),
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
comment on table T_LM_UGROUP_ULT
  is '�û�����ϸ�û���Ϣ��';
-- Add comments to the columns 
comment on column T_LM_UGROUP_ULT.ID
  is 'ϵͳID';
comment on column T_LM_UGROUP_ULT.USERGROUP_ID
  is '�û���ID';
comment on column T_LM_UGROUP_ULT.LOGIN_MOBILE
  is '�û���¼�ֻ�';
comment on column T_LM_UGROUP_ULT.ADD_TYPE
  is '��ӷ�ʽ 0���Զ� 1���ֶ�';
comment on column T_LM_UGROUP_ULT.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_UGROUP_ULT.UPDATE_TIME
  is '����ʱ��';