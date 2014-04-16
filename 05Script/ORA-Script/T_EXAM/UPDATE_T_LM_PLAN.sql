Alter Table T_LM_PLAN drop COLUMN IS_OPEN;--//删除字段
Alter TABLE T_LM_PLAN add HOTELVP_STATUS CHAR(1);--//新增字段
Alter Table T_LM_PLAN add  APP_STATUS CHAR(1) as (CASE  WHEN "STATUS" || "HOTELVP_STATUS"='11' THEN '1' ELSE '0' END);--//新增字段;--//新增字段



