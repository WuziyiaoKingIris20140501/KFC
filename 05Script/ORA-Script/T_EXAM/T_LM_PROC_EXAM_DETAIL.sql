-- Create table
create table T_LM_PROC_EXAM_DETAIL
(
  ID          NUMBER,
  TASK_ID     NUMBER,
  OLD_CONTENT PUBLIC.XMLTYPE,
  NEW_CONTENT PUBLIC.XMLTYPE,
  REFID       VARCHAR2(50),
  CREATE_TIME TIMESTAMP(6) default SYSDATE
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
-- Add comments to the columns 
comment on column T_LM_PROC_EXAM_DETAIL.ID
  is '����ID';
comment on column T_LM_PROC_EXAM_DETAIL.TASK_ID
  is '����ID';
comment on column T_LM_PROC_EXAM_DETAIL.OLD_CONTENT
  is 'ԭ�����ݣ�xml���ݸ�ʽ';
comment on column T_LM_PROC_EXAM_DETAIL.NEW_CONTENT
  is '�����޸����ݣ�xml���ݸ�ʽ';
comment on column T_LM_PROC_EXAM_DETAIL.REFID
  is '�ο�ID����Ƶ�ƻ�����ο��ƻ�ID���������ͣ���ʹ����ͬ����';
comment on column T_LM_PROC_EXAM_DETAIL.CREATE_TIME
  is '����ʱ��';
