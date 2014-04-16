 insert into t_lm_b_prop
(prop, orgid, prop_name_jp, prop_name_en, prop_name_zh, address1_zh, address1_en, address1_jp, address2_zh, address2_en, address2_jp, areaid, 
countryid, cityid, prop_typeid, zip, status, phone, fax, timezone, checkin, late_checkin, checkout, holdtime, opening_date, renovated_date, 
description_zh, description_en, description_jp, latitude, longitude, need_pay, star_rating, diamond_rating, price_high, price_low, total_rooms, 
total_floors, contact_name_zh, contact_name_en, contact_name_jp, contact_title_zh, contact_title_en, contact_title_jp, contact_phone, contact_email, 
activated_date, modify_date, modify_user, total_elevators, double_rooms, handi_rooms, king_rooms, meet_room_cap, max_prop_bednum, meeting_rooms, 
meet_room_area, nosmoking_rooms, twin_rooms, queen_rooms, add_extra_bed, big_rooms, website, is_model, fee_low, gdscode1, gdscode2, gdscode3, 
gdscode4, gdscode5, gdscode6, gdscode7, gdscode8, gdscode9, gdscode10, gdscode11, gdscode12, gdscode13, gdscode14, gdscode15, low_limit, 
high_limit, propcat, districtid, is_welcome_foreign, is_faith, censor, is_pms, keywords, simple_desc_zh, simple_desc_en, simple_desc_jp, is_expo, 
currencycode, air, est_online_date, use_logo, rq_book, overbooklimit, tradearea_zh, tradearea_en, tradearea_id, is_lm, contractdate, tp_fax, 
tp_contact_man, tp_contact_phone, linkemail, linkman, linktel, linkfax, hotel_description_zh, 
GROUP_CODE,auto_trust,create_time, update_time)
select 
ftg.prop,ftg.orgid,ftg.prop_name_jp,ftg.prop_name_en,ftg.prop_name_zh,ftg.address1_zh,ftg.address1_en,ftg.address1_jp,ftg.
address2_zh,ftg.address2_en,ftg.address2_jp,ftg.areaid,ftg.countryid,ftg.cityid,ftg.prop_typeid,ftg.zip,ftg.status,ftg.phone,ftg.fax,ftg.timezone,ftg.checkin,ftg.late_checkin,ftg.
checkout,ftg.holdtime,ftg.opening_date,renovated_date,ftg.description_zh,ftg.description_en,ftg.description_jp,ftg.latitude,ftg.longitude,ftg.need_pay,ftg.star_rating,ftg.
diamond_rating,ftg.price_high,ftg.price_low,ftg.total_rooms,ftg.total_floors,ftg.contact_name_zh,ftg.contact_name_en,ftg.contact_name_jp,ftg.contact_title_zh,ftg.contact_title_en,ftg.
contact_title_jp,ftg.contact_phone,ftg.contact_email,ftg.activated_date,ftg.modify_date,ftg.modify_user,ftg.total_elevators,ftg.double_rooms,ftg.handi_rooms,ftg.king_rooms,ftg.meet_room_cap,ftg.
max_prop_bednum,ftg.meeting_rooms,ftg.meet_room_area,ftg.nosmoking_rooms,ftg.twin_rooms,ftg.queen_rooms,ftg.add_extra_bed,ftg.big_rooms,ftg.WEBSITE,ftg.is_model,ftg.fee_low,ftg.gdscode1,ftg.gdscode2,ftg.
gdscode3,ftg.gdscode4,ftg.gdscode5,ftg.gdscode6,ftg.gdscode7,ftg.gdscode8,ftg.gdscode9,ftg.gdscode10,ftg.gdscode11,ftg.gdscode12,ftg.gdscode13,ftg.gdscode14,ftg.gdscode15,ftg.low_limit,ftg.high_limit,ftg.propcat,ftg.
districtid,ftg.is_welcome_foreign,ftg.is_faith,ftg.censor,ftg.is_pms,ftg.keywords,ftg.SIMPLE_DESC_ZH,ftg.simple_desc_en,ftg.simple_desc_jp,ftg.is_expo,ftg.currencycode,ftg.air,ftg.est_online_date,ftg.use_logo,ftg.
rq_book,ftg.overbooklimit,ftg.tradearea_zh,ftg.tradearea_en,ftg.tradearea_id,ftg.is_lm,ftg.contractdate,ftg.tp_fax,ftg.tp_contact_man,ftg.tp_contact_phone,ftg.
tlt.mail,tlt.linkman,tlt.tel,tlt.FAX,ftpd.property_zh,ftpg.orgid,'1',sysdate,sysdate
from FOG_T_PROP ftg left join T_LM_HOTEL tlt on ftg.prop = tlt.HOTEL_ID
left join FOG_T_PROP_DESC ftpd on ftpd.PROP=ftg.PROP
left join fog_t_proporg ftpg on ftpg.orgid=ftg.orgid
where not exists (select tbp.PROP from t_lm_b_prop tbp where ftg.prop=tbp.PROP)