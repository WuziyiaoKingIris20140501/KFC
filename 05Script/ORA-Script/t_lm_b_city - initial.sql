insert into t_lm_b_city
  (id, CITY_ID, area_id, name_cn, name_en, name_jp, seq, dareaid, gbcode, pinyin, pinyin_short, sname, lname, zipcode, phonecode, hotelnum, longitude, latitude,status, CREATE_TIME,UPDATE_TIME)
 select t_lm_b_city_seq.nextval,
cityid, areaid, name_zh, name_en, name_jp, seq, dareaid, gbcode, pinyin, pinyin_short, sname, lname, zipcode, phonecode, hotelnum, longitude, latitude, '0', sysdate,sysdate
from fog_t_city