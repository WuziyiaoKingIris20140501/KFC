insert into t_lm_b_hotelgroup (id, group_code, name_zh, name_en, description_zh, description_en, bandtype, status, create_time, update_time )
select t_lm_b_hotelgroup_seq.nextval,orgid,name_zh,'',org_desc_zh,'',
(CASE bandtype WHEN '经济型' THEN 'J' WHEN '豪华型' THEN 'H' ELSE 'B' END ),
'1',sysdate,sysdate from fog_t_proporg