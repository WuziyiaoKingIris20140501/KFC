-- Create table
create table T_LM_PROC_EXAM
(
  TASK_ID            NUMBER not null,
  TASK_CAT           VARCHAR2(100),
  TASK_NAME          VARCHAR2(100),
  TASK_CODE          VARCHAR2(50),
  TASK_STATUS        NUMBER default 0,
  TASK_APPROVEREJECT NUMBER,
  TASK_CURPROCUSER   VARCHAR2(50),
  TASK_CREATETIME    TIMESTAMP(6) default SYSDATE,
  TASK_FINISHTIME    TIMESTAMP(6),
  TASK_CREATEBY      VARCHAR2(50),
  TASK_UPDATETIME    TIMESTAMP(6) default SYSDATE
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
comment on table T_LM_PROC_EXAM
  is '������˱�';
-- Add comments to the columns 
comment on column T_LM_PROC_EXAM.TASK_ID
  is '����';
comment on column T_LM_PROC_EXAM.TASK_CAT
  is '�������';
comment on column T_LM_PROC_EXAM.TASK_NAME
  is '������';
comment on column T_LM_PROC_EXAM.TASK_CODE
  is '�������Code';
comment on column T_LM_PROC_EXAM.TASK_STATUS
  is '״ֵ̬��0-δ����1-�Ѵ���';
comment on column T_LM_PROC_EXAM.TASK_APPROVEREJECT
  is 'ͬ�⻹�Ǿܾ���0-�ܾ���1-ͬ�⣻2-��ʧЧ��Ĭ��Ϊ�գ���ʾδ����';
comment on column T_LM_PROC_EXAM.TASK_CURPROCUSER
  is '��ǰ�����ˣ�system��ʾ���Դ���';
comment on column T_LM_PROC_EXAM.TASK_CREATETIME
  is '����ʱ��';
comment on column T_LM_PROC_EXAM.TASK_FINISHTIME
  is '���ʱ��';
comment on column T_LM_PROC_EXAM.TASK_CREATEBY
  is '������';
comment on column T_LM_PROC_EXAM.TASK_UPDATETIME
  is '�޸�ʱ��';
