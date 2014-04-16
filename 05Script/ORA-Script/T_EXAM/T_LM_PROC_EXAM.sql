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
  is '任务审核表';
-- Add comments to the columns 
comment on column T_LM_PROC_EXAM.TASK_ID
  is '自增';
comment on column T_LM_PROC_EXAM.TASK_CAT
  is '任务分类';
comment on column T_LM_PROC_EXAM.TASK_NAME
  is '任务名';
comment on column T_LM_PROC_EXAM.TASK_CODE
  is '任务代码Code';
comment on column T_LM_PROC_EXAM.TASK_STATUS
  is '状态值，0-未处理；1-已处理；';
comment on column T_LM_PROC_EXAM.TASK_APPROVEREJECT
  is '同意还是拒绝，0-拒绝，1-同意；2-已失效；默认为空，表示未处理';
comment on column T_LM_PROC_EXAM.TASK_CURPROCUSER
  is '当前处理人：system表示电脑处理';
comment on column T_LM_PROC_EXAM.TASK_CREATETIME
  is '创建时间';
comment on column T_LM_PROC_EXAM.TASK_FINISHTIME
  is '完成时间';
comment on column T_LM_PROC_EXAM.TASK_CREATEBY
  is '创建人';
comment on column T_LM_PROC_EXAM.TASK_UPDATETIME
  is '修改时间';
