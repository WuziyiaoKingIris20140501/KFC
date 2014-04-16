insert into t_lm_corehotelgroup
  (id, coregroup_name, status, create_time, create_user, update_time, update_user)
values
  (t_lm_coregroup_seq.nextval, 'CMS每日自动检查核心酒店', '1', sysdate, 'CMS', sysdate, 'CMS');