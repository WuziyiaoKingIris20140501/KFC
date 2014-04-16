-- Create table
create table t_lm_b_hotelgroup
(
  ID			NUMBER not null,
  GROUP_CODE		VARCHAR2(60),
  NAME_ZH		VARCHAR2(300),
  NAME_EN		VARCHAR2(300),
  DESCRIPTION_ZH	VARCHAR2(800),
  DESCRIPTION_EN	VARCHAR2(800),
  BANDTYPE		VARCHAR2(2),
  STATUS		VARCHAR2(2),
  CREATE_TIME		TIMESTAMP(6),
  UPDATE_TIME		TIMESTAMP(6)
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
comment on table t_lm_b_hotelgroup
  is '酒店集团管理表';
comment on column t_lm_b_hotelgroup.ID
  is '系统ID';
comment on column t_lm_b_hotelgroup.GROUP_CODE
  is '酒店集团CODE';
comment on column t_lm_b_hotelgroup.NAME_ZH
  is '酒店集团中文名称';
comment on column t_lm_b_hotelgroup.NAME_EN
  is '酒店集团英文名称';
comment on column t_lm_b_hotelgroup.DESCRIPTION_ZH
  is '酒店集团描述中文名称';
comment on column t_lm_b_hotelgroup.DESCRIPTION_EN
  is '酒店集团描述英文名称';
comment on column t_lm_b_hotelgroup.BANDTYPE
  is '酒店集团类型 H:豪华型 J:经济型';
comment on column t_lm_b_hotelgroup.STATUS
  is '在线状态  1：上线  0：下线';
comment on column t_lm_b_hotelgroup.CREATE_TIME
  is '创建时间';
comment on column t_lm_b_hotelgroup.UPDATE_TIME
  is '更新时间';

-- Create/Recreate primary, unique and foreign key constraints 
alter table t_lm_b_hotelgroup
  add constraint t_lm_b_hotelgroup_ID_PK primary key (ID)
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
