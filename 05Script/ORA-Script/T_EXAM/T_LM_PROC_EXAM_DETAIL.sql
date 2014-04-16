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
  is '自增ID';
comment on column T_LM_PROC_EXAM_DETAIL.TASK_ID
  is '任务ID';
comment on column T_LM_PROC_EXAM_DETAIL.OLD_CONTENT
  is '原来内容，xml数据格式';
comment on column T_LM_PROC_EXAM_DETAIL.NEW_CONTENT
  is '本次修改内容，xml数据格式';
comment on column T_LM_PROC_EXAM_DETAIL.REFID
  is '参考ID，如酒店计划、则参考计划ID；其它类型，则使用相同规则';
comment on column T_LM_PROC_EXAM_DETAIL.CREATE_TIME
  is '创建时间';
