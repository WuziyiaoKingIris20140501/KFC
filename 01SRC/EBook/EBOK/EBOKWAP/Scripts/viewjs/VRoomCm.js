

function fn_initial_RoomIndexList() {
//    localStorage.setItem("mac", mac);
//    localStorage.setItem("hid", hid);
    fn_roomcm_index_Change('1');
}

function fn_roomcm_index_Change(arg) {

    //    localStorage.getItem(prefix + "deviceID")
    //    localStorage.setItem(prefix + "mac", "XXXXXX")
    //    localStorage.setItem(prefix + "hid", "1052")

    //    sessionStorage.getItem(prefix + "deviceID")
    //    sessionStorage.setItem(prefix + "deviceID", "")

    //Global.LoadProcess();
    //alert('1');
    if (arg == "1") {
        $("#liRcInToday").addClass("active");
        $("#liRcOffToday").removeClass("active");
        $("#liRcInHis").removeClass("active");
        fn_roomcm_index_main_search();
    }
    else if (arg == "2") {
        $("#liRcInToday").removeClass("active");
        $("#liRcOffToday").addClass("active");
        $("#liRcInHis").removeClass("active");
        fn_roomcm_review_main_search();
    }
    else {
        $("#liRcInToday").removeClass("active");
        $("#liRcOffToday").removeClass("active");
        $("#liRcInHis").addClass("active");
        fn_roomcm_list_main_search();
    }

    //    $("#spOCInToday").html("12");
    //    $("#spOcOffToday").html("11");
    //Global.UnLoadProcess();
}

function fn_roomcm_index_main_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/RoomCm/AjxGetIndexMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            $("#dvRoomCmIndexMainList").html(data);
            $("#spRCInToday").html(document.getElementById("hidRoomCount").value);
            $("#spRcOffToday").html(document.getElementById("hidRoomCount").value);
            $("#spToDayDate").html(document.getElementById("hidToDayDate").value);
            Global.UnLoadProcess();
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}

function fn_roomcm_index_main_refresh() {
    $.ajax({
        url: GetPath("/RoomCm/AjxGetIndexMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            $("#dvRoomCmIndexMainList").html(data);
            $("#spRCInToday").html(document.getElementById("hidRoomCount").value);
            $("#spRcOffToday").html(document.getElementById("hidRoomCount").value);
            $("#spToDayDate").html(document.getElementById("hidToDayDate").value);
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}

function fn_roomcm_review_main_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/RoomCm/AjxGetReviewRoomMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            $("#dvRoomCmIndexMainList").html(data);
            Global.UnLoadProcess();
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}


function fn_roomcm_clear_data() {
    $("#dvControlDate button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    $("#tdInter button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    $("#tdBrefa button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    $("#tdRoomnum button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    $("#tdRoomCdList button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    $("#dvstartDate").css('display', 'none');
    $("#dvendDate").css('display', 'none');
    $("#dvDateBT").css('display', 'none');
    $("#dvDateBT2").css('display', 'none');
    $("#txt_roomcm_review_morbrefa").css('display', 'none');
    $("#txt_roomcm_review_mornum").css('display', 'none');

    $("#txt_roomcm_review_startdate").val("");
    $("#txt_roomcm_review_enddate").val("");
    $("#txt_roomcm_review_morbrefa").val("");
    $("#txt_roomcm_review_mornum").val("");

    $("#roomcmerrmsg").css('display', 'none');
    $("#dvroomcmbody").css('margin-top', '15px');

    $('#main_date_tb').find('td').each(function () {//搜寻表格里的每一个区间
        if ($(this).attr("id").lastIndexOf("P_") > 0) {
            $(this).removeClass("date-table-select");
        }
    });
}

function fn_roomcm_review_date_change(arg) {
    $("#dvControlDate button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });
    $(arg).addClass("btn-warning");
//    alert($("#btn_roomcm_review_today").hasClass("btn-warning"));
    //    alert($("#btn_roomcm_review_otherday").hasClass("btn-warning"));

    if (arg == "#btn_roomcm_review_otherday") {
        $("#dvstartDate").css('display', 'block');
        $("#dvendDate").css('display', 'block');
        $("#dvDateBT").css('display', 'block');
        $("#dvDateBT2").css('display', 'block');
    }
    else {
        $("#dvstartDate").css('display', 'none');
        $("#dvendDate").css('display', 'none');
        $("#dvDateBT").css('display', 'none');
        $("#dvDateBT2").css('display', 'none');
    }
}

function fn_roomcm_review_inter_change(arg) {
    $("#tdInter button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });
    $(arg).addClass("btn-warning");
}

function fn_roomcm_review_brefa_change(arg) {
    $("#tdBrefa button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });
    $(arg).addClass("btn-warning");

    if (arg == "#btn_roomcm_review_morbrefa") {
        $("#txt_roomcm_review_morbrefa").css('display', 'block');
    }
    else {
        $("#txt_roomcm_review_morbrefa").css('display', 'none');
    }
}

function fn_roomcm_review_roomnum_change(arg) {
    $("#tdRoomnum button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });
    $(arg).addClass("btn-warning");

    if (arg == "#btn_roomcm_review_mornum") {
        $("#txt_roomcm_review_mornum").css('display', 'block');
    }
    else {
        $("#txt_roomcm_review_mornum").css('display', 'none');
    }
}

function fn_roomcm_review_roomcd_change(arg) {
//    $("#tdRoomCdList button").each(function () {
//        $(this).removeClass("btn-warning");
//        $(this).addClass("btn-default");
//    });
    //    $(arg).addClass("btn-warning");
    if ($(arg).hasClass("btn-warning")) {
        $(arg).removeClass("btn-warning");
    }
    else {
        $(arg).addClass("btn-warning");
    }
}

function fn_roomcm_review_roomcd_all_change() {
    $("#tdRoomCdList button").each(function () {
        $(this).addClass("btn-warning");
    });
    $("#btnAll").removeClass("btn-warning");
}

function fn_roomcm_index_msg_show() {
    $("#dvProcessbg").show();
    $("#roomcminfoMsg").show();
}

function fn_roomcm_index_msg_hide() {
    $("#dvProcessbg").hide();
    $("#roomcminfoMsg").hide();
}

function fn_roomcm_index_msg_dv_hide() {
    $("#roomcminfoMsg").hide();
    $("#warningMsg").css("z-index", 100001);
}

function fn_roomcm_index_msg_suc_show() {
    $("#dvProcessbg").show();
    $("#roomcmsucMsg").show();
}

function fn_roomcm_index_msg_suc_hide() {
    $("#dvProcessbg").hide();
    $("#roomcmsucMsg").hide();
}


function fn_roomcm_review_datetimepicker_show() {
    if ($("#txt_roomcm_review_startdate").val() == "") {
        $("#txt_roomcm_review_startdate").val(document.getElementById("hidFromDayDate").value);
        $("#txt_roomcm_review_enddate").val(document.getElementById("hidFromDayDate").value);
        $("#tdDTP_" + document.getElementById("hidFromDayDate").value).addClass('date-table-select');
    }

    $("#dvProcessbg").show();
    $("#dvDateTimePicker").show();
}

function fn_roomcm_review_dateptClick(datetime) {
    var date = datetime.split("_")[1];
    var start = $("#txt_roomcm_review_startdate").val();
    var end = DateAdd($("#txt_roomcm_review_enddate").val(),1);
    var t = DateDiff(end, start);

    //  单选
    if (t <= 1) {
        var days = DateDiff(date, start);
        if (days == 0) {
            start = start;
            end = DateAdd(start, 1);
        }
        else if (days > 0) {
            start = start;
            end = DateAdd(date, 1);
        }
        else {
            start = date;
        }
        $('#main_date_tb').find('td').each(function () {//搜寻表格里的每一个区间
            if ($(this).attr("id").lastIndexOf("P_") > 0) {
                var v = $(this).attr("id").split("_")[1];
                if (v >= start && v < end) {
                    $(this).addClass("date-table-select");
                }
                else {
                    $(this).removeClass("date-table-select");
                }
            }
        });

    }  // 多选
    else {
        start = date;
        end = DateAdd(start, 1);
        $('#main_date_tb td').each(function () {
            if ($(this).attr("id").lastIndexOf("P_") > 0) {
                var v = $(this).attr("id").split("_")[1];
                if (v != date) {
                    $(this).removeClass("date-table-select");
                }
                else {
                    $(this).addClass("date-table-select");
                }
            }
        });
    }

    $("#txt_roomcm_review_startdate").val(start);
    $("#txt_roomcm_review_enddate").val(DateAdd(end, -1));
}

function DateDiff(sDate1, sDate2) { //sDate1和sDate2是2002-12-18格式    
    var aDate, oDate1, oDate2, iDays;
    aDate = sDate1.split("-");
    oDate1 = new Date(aDate[0], aDate[1] - 1, aDate[2]);
    aDate = sDate2.split("-");
    oDate2 = new Date(aDate[0], aDate[1] - 1, aDate[2]);

    iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);
    if ((oDate1 - oDate2) < 0) {
        return -iDays;
    }
    return iDays;
}
function DateAdd(date, days) {
    var myDate = new Date(date.replace(/-/g, "/"));
    myDate.setDate(myDate.getDate() + days);
    return myDate.formate("yyyy-MM-dd");
}

Date.prototype.formate = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份           
        "d+": this.getDate(), //日           
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时           
        "H+": this.getHours(), //小时           
        "m+": this.getMinutes(), //分           
        "s+": this.getSeconds(), //秒           
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度           
        "S": this.getMilliseconds() //毫秒           
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

function fn_roomcm_review_datetimepicker_hide() {
    $("#dvProcessbg").hide();
    $("#dvDateTimePicker").hide();
}

function fn_roomcm_save_rooms_check() {
    $("#roomcmerrmsg").css('display', 'none');
    $("#dvroomcmbody").css('margin-top', '15px');

    var bErr = false;
    var saveDate = "";
    var saveTime = "111100000000000000111111";
    var roomcd = ""
    var roomnum = "";
    var roombref = "";
    var roomWifi = "";
    var errmsg = "";
    var errtype = "";

    if ($("#btn_roomcm_review_today").hasClass("btn-warning")) {
        saveDate = "Sysdate";
    }
    else if ($("#btn_roomcm_review_otherday").hasClass("btn-warning")) {
        saveDate = $("#txt_roomcm_review_startdate").val() + "_" + $("#txt_roomcm_review_enddate").val();
    }

    if (saveDate == "" || saveDate == "_") {
        bErr = true;
        errmsg += "日期 ";
    }

    $("#tdRoomCdList button").each(function () {
        if ($(this).hasClass("btn-warning")) {
            roomcd = roomcd + $(this).attr("id") + ",";
        }
    });

    if (roomcd == "") {
        bErr = true;
        errmsg += "房型 ";
    }
    var bflag = true;
    $("#tdRoomnum button").each(function () {
        if ($(this).hasClass("btn-warning")) {
            if ($(this).attr("id") == "btn_roomcm_review_mornum") {
                roomnum = $("#txt_roomcm_review_mornum").val().trim();
                if (parseInt(roomnum) == roomnum && parseInt(roomnum) >= 0 && parseInt(roomnum) <= 999) { }
                else {
                    bErr = true;
                    errtype += " 房量请输入0-999整数 ";
                }
            }
            else {
                roomnum = $(this).val();
                if (roomnum == "") {
                    bErr = true;
                    errmsg += "房量 ";
                }
            }
            bflag = false;
        }
    });

    if (roomnum == "" && bflag) {
        bErr = true;
        errmsg += "房量 ";
    }

    bflag = true;
    $("#tdBrefa button").each(function () {
        if ($(this).hasClass("btn-warning")) {
            if ($(this).attr("id") == "btn_roomcm_review_morbrefa") {
                roombref = $("#txt_roomcm_review_morbrefa").val().trim();

                if (parseInt(roombref) == roombref && parseInt(roombref) >= 0 && parseInt(roombref) <= 999) { }
                else {
                    bErr = true;
                    errtype += " 早餐请输入0-999整数 ";
                }
            }
            else {
                roombref = $(this).val();
                 if (roombref == "") {
                    bErr = true;
                    errmsg += "早餐 ";
                }
            }
            bflag = false;
        }
    });

    if (roombref == "" && bflag) {
        bErr = true;
        errmsg += "早餐 ";
    }

    $("#tdInter button").each(function () {
        if ($(this).hasClass("btn-warning")) {
            roomWifi = $(this).val();
        }
    });

    if (roomWifi == "") {
        bErr = true;
        errmsg += "宽带 ";
    }

    //$("#dvmsgcontent").html("1232312321");
    //$("#dvroomcmbody").css('margin-top', '0px');
    if (bErr) {
        $("#roomcmerrmsg").css('display', 'block');
        if (errmsg == "")
        {
            $("#dvmsgcontent").html(errtype);
        }
        else
        {
            $("#dvmsgcontent").html("请选择：" + errmsg + "!  " + errtype);
        }
        
        $("#dvroomcmbody").css('margin-top', '0px');
        
    }
    else {
        sessionStorage.setItem("roomcm_saveDate", saveDate);
        sessionStorage.setItem("roomcm_saveTime", saveTime);
        sessionStorage.setItem("roomcm_roomcd", roomcd);
        sessionStorage.setItem("roomcm_roomnum", roomnum);
        sessionStorage.setItem("roomcm_roombref", roombref);
        sessionStorage.setItem("roomcm_roomWifi", roomWifi);
        fn_roomcm_index_msg_show();
    }
}

function fn_roomcm_review_roomcd_save(opeuserid) {
    fn_roomcm_index_msg_hide();
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/RoomCm/AjxSaveRoomInfo"),
        type: "POST",
        async: true,
        dataType: "Json",
        data: {
            saveDate: sessionStorage.getItem("roomcm_saveDate"),
            saveTime: sessionStorage.getItem("roomcm_saveTime"),
            roomcd: sessionStorage.getItem("roomcm_roomcd"),
            roomnum: sessionStorage.getItem("roomcm_roomnum"),
            roombref: sessionStorage.getItem("roomcm_roombref"),
            roomWifi: sessionStorage.getItem("roomcm_roomWifi"),
            opeuserid: opeuserid,
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            //            $("#dvRoomCmIndexMainList").html(data);
            $("#progressbar").hide();
            fn_roomcm_index_msg_suc_show();
            setTimeout("fn_roomcm_index_msg_suc_hide()", 1000);
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}

function fn_roomcm_list_main_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/RoomCm/AjxGetListRoomMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            $("#dvRoomCmIndexMainList").html(data);
            Global.UnLoadProcess();
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}


function fn_roomcm_list_search() {
    Global.LoadProcess();
    var searchDate = "";
    if ($("#btn_roomcm_review_today").hasClass("btn-warning")) {
        searchDate = "Sysdate";
    }
    else if ($("#btn_roomcm_review_otherday").hasClass("btn-warning")) {
        searchDate = $("#txt_roomcm_review_startdate").val() + "_" + $("#txt_roomcm_review_enddate").val();
    }
    $.ajax({
        url: GetPath("/RoomCm/AjxGetRoomPlanListMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid"),
            sdate: searchDate
        },
        success: function (data) {
            $("#dvRoomPlanList").html(data);
            Global.UnLoadProcess();
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}

function fn_roomcm_room_modify(rateCode, roomTypeCode, roomTypeName, roomNum, status, twoprice) {
    $("#txt_roomcs_roomnum").val(roomNum);
    $("#txt_roomcs_price").val(twoprice);
    $("#tdRoomStatus button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    var ratename = (rateCode == "LMBAR") ? "(预付)" : "(现付)";

    $("#sp_room_name").html("[" + roomTypeCode + "]" + roomTypeName + ratename);

    if (status == "false") {
        $("#btn_roomcs_status_offline").addClass("btn-warning");
    }
    else if (status == "true" && parseInt(roomNum) == 0) {
        $("#btn_roomcs_status_full").addClass("btn-warning");
    }
    else {
        $("#btn_roomcs_status_online").addClass("btn-warning");
    }

    sessionStorage.setItem("roomcs_ratecode", rateCode);
    sessionStorage.setItem("roomcs_roomtypecode", roomTypeCode);

    $("#dvUserDetail").html("修改房态");
    $("#warningMsg").css("z-index", 100001);
    fn_roomcs_room_detail_show();
}

function fn_roomcs_room_detail_show(arg) {
    $("#dvRoomcsSaveMsg").html("");
    $("#dvProcessbg").show();
    $("#warningMsg").show();
}

function fn_roomcs_room_detail_hide() {
    $("#dvProcessbg").hide();
    $("#warningMsg").hide();
}

function fn_roomcs_index_msg_suc_show() {
    $("#dvProcessbg").show();
    $("#roomcssucMsg").show();
}

function fn_roomcs_index_msg_suc_hide() {
    $("#dvProcessbg").hide();
    $("#roomcssucMsg").hide();
}


function fn_roomcs_status_save_check() {
    $("#dvRoomcsSaveMsg").html("");
    var roomNum = $("#txt_roomcs_roomnum").val().trim();
    var twoPrice = $("#txt_roomcs_price").val().trim();
    var bErr = false;
    var errmsg = "";

    if ($("#btn_roomcs_status_online").hasClass("btn-warning") && roomNum == "") {
        bErr = true;
        errmsg += "请输入：房量!";
    }
    else {
        if ($("#btn_roomcs_status_online").hasClass("btn-warning")) {
            if (parseInt(roomNum) == roomNum && parseInt(roomNum) > 0 && parseInt(roomNum) <= 999) { }
            else {
                bErr = true;
                errmsg += " 房量请输入1-999整数!";
            }

            if (parseFloat(twoPrice) == twoPrice && parseFloat(twoPrice) >= 50 && parseFloat(roomNum) <= 99999) {
                var reg = new RegExp("^[0-9]+(.[0-9]{2})?$", "g");
                if (!reg.test(twoPrice)) {
                    bErr = true;
                    errmsg += " 价格请输入50.00-99999.99数!";
                }  
            }
            else {
                bErr = true;
                errmsg += " 价格请输入50.00-99999.99数!";
            }
        }
        else {
//            if (parseInt(roomNum) == roomNum && parseInt(roomNum) >= 0 && parseInt(roomNum) <= 999) { }
//            else {
//                bErr = true;
//                errmsg += " 房量请输入0-999整数!";
//            }
        }
    }
    var roomStatus = "";
    $("#tdRoomStatus button").each(function () {
        if ($(this).hasClass("btn-warning")) {
            roomStatus = $(this).val();
        }
    });

    if (roomStatus == "" || roomStatus == "-1") {
        bErr = true;
        errmsg += "请选择当前状态！";
    } 

    if (bErr) {
        $("#dvRoomcsSaveMsg").html(errmsg);
    }
    else {
        sessionStorage.setItem("roomcs_roomnum", roomNum);
        sessionStorage.setItem("roomcs_twoprice", twoPrice);
        var userstatus = "";
        if ($("#btn_roomcs_status_online").hasClass("btn-warning")) {
            userstatus = "1";
        }
        else if ($("#btn_roomcs_status_full").hasClass("btn-warning")) {
            userstatus = "0";
        }
        else {
            userstatus = "-1";
        }
        sessionStorage.setItem("roomcs_status", userstatus);

        $("#warningMsg").css("z-index", 100000);
        $("#roomcminfoMsg").show();
    }
}

function fn_roomcs_status_change(arg) {
    $("#tdRoomStatus button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });
    $(arg).addClass("btn-warning");
}

function fn_roomcm_roomcd_status_save(opeuserid) {
    $("#roomcminfoMsg").hide();
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/RoomCm/AjxSaveRoomStatusInfo"),
        type: "POST",
        async: true,
        dataType: "Json",
        data: {
            ratecode: sessionStorage.getItem("roomcs_ratecode"),
            roomcd: sessionStorage.getItem("roomcs_roomtypecode"),
            roomnum: sessionStorage.getItem("roomcs_roomnum"),
            status: sessionStorage.getItem("roomcs_status"),
            twoprice: sessionStorage.getItem("roomcs_twoprice"),
            opeuserid: opeuserid,
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            //            $("#dvRoomCmIndexMainList").html(data);
            if (data.message == "success") {
                $("#progressbar").hide();
                fn_roomcm_index_msg_suc_show();
                setTimeout("fn_roomcm_index_msg_suc_hide()", 1000);
                fn_roomcm_index_main_refresh();
            }
            else {
                $("#progressbar").hide();
                $("#dvRoomcsSaveMsg").html(data.message);
            }

        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}