-- Create table
create table T_LM_PROMOTIONMSG_DETAIL
(
  ID             NUMBER not null,
  PROMOTIONMSG_ID   VARCHAR2(100),
  PRO_TYPE       VARCHAR2(1),
  CITY_ID	 VARCHAR2(100),
  HOTELGROUP_ID  VARCHAR2(100),
  HOTEL_ID	 VARCHAR2(100),
  ROOM_ID	 VARCHAR2(100),
  USERGROUP_ID   VARCHAR2(100),
  STATUS	 VARCHAR2(1), 
  CREATE_TIME    TIMESTAMP(6),
  UPDATE_TIME    TIMESTAMP(6)
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
comment on table T_LM_PROMOTIONMSG_DETAIL
  is '������Ϣ��ϸ��';
-- Add comments to the columns 
comment on column T_LM_PROMOTIONMSG_DETAIL.ID
  is '������Ϣ��ϸID';
comment on column T_LM_PROMOTIONMSG_DETAIL.PROMOTIONMSG_ID
  is '������ϢID';
comment on column T_LM_PROMOTIONMSG_DETAIL.PRO_TYPE
  is '�������� 0:ȫ�ִ��� 1:���д��� 2:�Ƶ꼯�Ŵ��� 3:�Ƶ���� 4:���ʹ��� 5:�û���';
comment on column T_LM_PROMOTIONMSG_DETAIL.CITY_ID
  is '����ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.HOTELGROUP_ID
  is '�Ƶ꼯��ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.HOTEL_ID
  is '�Ƶ�ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.ROOM_ID
  is '����ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.USERGROUP_ID
  is '�û���ID';
comment on column T_LM_PROMOTIONMSG_DETAIL.STATUS
  is '״̬ 0:��Ч1:��Ч';
comment on column T_LM_PROMOTIONMSG_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column T_LM_PROMOTIONMSG_DETAIL.UPDATE_TIME
  is '����ʱ��';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_LM_PROMOTIONMSG_DETAIL
  add constraint T_LM_PROMOTIONMSG_DETAIL_PK primary key (ID)
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