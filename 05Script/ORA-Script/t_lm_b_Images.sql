-- Create table
create table T_LM_B_IMAGES
(
  ID            NUMBER not null,
  TITLE			VARCHAR2(200),
  IMG_TYPE		VARCHAR2(2),
  Resolution	VARCHAR2(1),
  HTP_PATH		VARCHAR2(1000),
  DNS_PATH		VARCHAR2(1000),
  STATUS		VARCHAR2(1), 
  CREATE_TIME    TIMESTAMP(6),
  UPDATE_TIME    TIMESTAMP(6),
  OPERATE_USER   VARCHAR2(50)	
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
-- Add comments to the table 
comment on table T_LM_B_IMAGES
  is 'ͼƬ��Ϣά����';
-- Add comments to the columns 
comment on column T_LM_B_IMAGES.ID
  is 'ϵͳID';
comment on column T_LM_B_IMAGES.TITLE
  is '��Ϣ����';
comment on column T_LM_B_IMAGES.IMG_TYPE
  is 'ͼƬ���� 01:����ͼƬ';
comment on column T_LM_B_IMAGES.Resolution
  is 'ͼƬ�ֱ��� 0:  1:  2:  3:  4: ';
comment on column T_LM_B_IMAGES.HTP_PATH
  is 'HTTPͼƬ·��';
comment on column T_LM_B_IMAGES.DNS_PATH
  is 'DNSͼƬ·��';
comment on column T_LM_B_IMAGES.STATUS
  is '״̬ 0:���� 1:����';
comment on column T_LM_B_IMAGES.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_B_IMAGES.UPDATE_TIME
  is '����ʱ��';
comment on column T_LM_B_IMAGES.OPERATE_USER
  is 'ά����';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_B_IMAGES
  add constraint T_LM_B_IMAGES_PK primary key (ID)
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