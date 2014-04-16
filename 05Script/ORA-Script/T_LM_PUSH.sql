-- Create table
create table T_LM_PUSH
(
  PUSH_ID            NUMBER,
  PUSH_TITLE         VARCHAR2(80),
  PUSH_CONTENT       VARCHAR2(600),
  PUSH_SUCCESS_COUNT NUMBER,
  CREATE_TIME        TIMESTAMP(6) default sysdate,
  UPDATE_TIME        TIMESTAMP(6),
  PUSH_MEMO          VARCHAR2(200),
  PUSH_OBJECT        VARCHAR2(400)
)
tablespace HOTELVPTEST
  pctfree 10
  initrans 1
  maxtrans 255;
-- Add comments to the table 
comment on table T_LM_PUSH
  is 'Push消息主表';
-- Add comments to the columns 
comment on column T_LM_PUSH.PUSH_ID
  is '自增ID';
comment on column T_LM_PUSH.PUSH_TITLE
  is 'Push的标题';
comment on column T_LM_PUSH.PUSH_CONTENT
  is 'push的内容';
comment on column T_LM_PUSH.PUSH_SUCCESS_COUNT
  is '发送成功的条数';
comment on column T_LM_PUSH.CREATE_TIME
  is '创建时间';
comment on column T_LM_PUSH.UPDATE_TIME
  is '修改时间';
comment on column T_LM_PUSH.PUSH_MEMO
  is '备注';
comment on column T_LM_PUSH.PUSH_OBJECT
  is 'push消息的对象，如那些用户组';
