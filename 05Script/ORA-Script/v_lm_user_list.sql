create or replace view v_lm_user_list as
select "ID","LOGINMOBILE","CREATETIME","SIGN_KEY","SIGNDATE","VERSION","ALLCOUNT","COMPLECOUNT","LASTORDERTIME","REGCHANEL_CODE","REGCHANELNM","PLATFORM_CODE","PLATFORMNM","TODAYLOGIN" from (select ur.ID,
    ur.login_mobile AS LOGINMOBILE,ur.CREATETIME
    ,ur.SIGN_KEY,ur.sign_date AS SIGNDATE,ur.VERSION,
    nvl(act.allCount, 0) AS ALLCOUNT,nvl(cpt.compleCount,0) AS COMPLECOUNT,
    LastT.lastordertime,
    ur.regchanel_code,lbr.REGCHANEL_NAME AS REGCHANELNM ,ur.platform_code,lbp.PLATFORM_NAME AS PLATFORMNM,tlua.TODAYLOGIN
    from t_lm_user ur
    left join
    (select count(id) AS allCount ,login_mobile from t_lm_order group by login_mobile) act
    on act.login_mobile=ur.login_mobile
    left join
    (select count(id) AS compleCount ,login_mobile from t_lm_order where ((price_code= 'LMBAR' and BOOK_STATUS=5) OR ((price_code= 'LMBAR2' OR price_code= 'BAR' OR price_code= 'BARB') AND (FOG_RESVTYPE='n' or FOG_RESVTYPE='e') AND book_status_other <> '3')) group by login_mobile) cpt
    on cpt.login_mobile=ur.login_mobile
    left join
    (select lo.login_mobile,lo.CREATE_TIME AS lastordertime from t_lm_order lo inner join
    (select login_mobile,max(CREATE_TIME) as mCreateTime from t_lm_order group by login_mobile) ld
    on lo.login_mobile=ld.login_mobile and lo.CREATE_TIME=ld.mCreateTime) LastT
    on LastT.login_mobile=ur.login_mobile
    left join T_LM_B_REGCHANEL lbr on ur.regchanel_code=lbr.regchanel_code
    left join T_LM_B_PLATFORM lbp on ur.platform_code=lbp.platform_code
    left join (select t.mobile,max(t.today_login) AS TODAYLOGIN from t_lm_user_action t group by t.mobile) tlua
    on ur.login_mobile=tlua.mobile) tt

