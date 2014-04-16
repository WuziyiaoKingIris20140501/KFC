

function fn_initial_LoacalVal(mac, hid, hnm) {
    if (mac != "")
    {
        localStorage.setItem("mac", mac);
    }
    if (hid != "") {
        localStorage.setItem("hid", hid);
    }
    if (hnm != "") {
        localStorage.setItem("hnm", hnm);
    }
    fn_ordercm_index_Change('1');
}

function fn_ordercm_index_Change(arg) {

//    localStorage.getItem(prefix + "deviceID")
//    localStorage.setItem(prefix + "mac", "XXXXXX")
//    localStorage.setItem(prefix + "hid", "1052")

//    sessionStorage.getItem(prefix + "deviceID")
//    sessionStorage.setItem(prefix + "deviceID", "")

    //Global.LoadProcess();
    if (arg == "1") {
        $("#liOcInToday").addClass("active");
        $("#liOcOffToday").removeClass("active");
        $("#liOcInHis").removeClass("active");
        fn_ordercm_index_main_search();
    }
    else if (arg == "2") {
        $("#liOcInToday").removeClass("active");
        $("#liOcOffToday").addClass("active");
        $("#liOcInHis").removeClass("active");
        fn_ordercm_review_main_search();
    }
    else {
        $("#liOcInToday").removeClass("active");
        $("#liOcOffToday").removeClass("active");
        $("#liOcInHis").addClass("active");
        fn_ordercm_list_main_search();
    }

//    $("#spOCInToday").html("12");
//    $("#spOcOffToday").html("11");
    //Global.UnLoadProcess();
}

//function fn_ordercm_index_msg_show() {
//    $("#dvProcessbg").show();
//    $("#warningMsg").show();
//}

//function fn_ordercm_index_msg_hide() {
//     $("#dvProcessbg").hide();
//     $("#warningMsg").hide();
// }


 function fn_ordercm_index_main_search() {
     Global.LoadProcess();
     $.ajax({
         url: GetPath("/OrderCm/AjxAPPGetIndexMainList"),
         type: "POST",
         dataType: "html",
         data: {
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid")
         },
         success: function (data) {
             $("#dvOderCmIndexMainList").html(data);
             $("#spACInToday").html(document.getElementById("hidOrderCount").value);
             $("#spAcOffToday").html(document.getElementById("hidOrderCount").value);
             $("#spRAOffToday").html(document.getElementById("hidOdReviewCt").value);
             Global.UnLoadProcess();
         },
         error: function (data) {
             Global.UnLoadProcess();
         }
     });
 }

 function fn_ordercm_review_main_search() {
     Global.LoadProcess();
     $.ajax({
         url: GetPath("/OrderCm/AjxAPPGetOrderReviewList"),
         type: "POST",
         dataType: "html",
         data: {
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid")
         },
         success: function (data) {
             $("#dvOderCmIndexMainList").html(data);
             $("#spACInToday").html(document.getElementById("hidOrderCount").value);
             $("#spAcOffToday").html(document.getElementById("hidOrderCount").value);
             $("#spRAOffToday").html(document.getElementById("hidOdReviewCt").value);
             Global.UnLoadProcess();
         },
         error: function (data) {
             Global.UnLoadProcess();
         }
     });
 }

 function fn_ordercm_index_msg_show() {
     $("#dvProcessbg").show();
     $("#ordercminfoMsg").show();
 }

 function fn_ordercm_index_msg_hide() {
     $("#dvProcessbg").hide();
     $("#ordercminfoMsg").hide();
 }

 function fn_ordercm_index_msg_suc_show() {
     $("#dvProcessbg").show();
     $("#ordercmsucMsg").show();
 }

 function fn_ordercm_index_msg_suc_hide() {
     $("#dvProcessbg").hide();
     $("#ordercmsucMsg").hide();
 }


 function fn_ordercm_save_status_check(orderid, comfirmid, remarkid, actionType) {
     $("#ordercmerrmsg").css('display', 'none');
     $("#dvordercmbody").css('margin-top', '0px');

     var comid = $(comfirmid).val().Trim();
     var remark = $(remarkid).val().Trim();

     if (actionType == "4" && comid == "") {
         $("#ordercmerrmsg").css('display', 'block');
         $("#dvmsgcontent").html("请选择输入客人房号!");
         $("#dvordercmbody").css('margin-top', '-15px');
     }
     else {
         sessionStorage.setItem("ordercm_orderid", orderid);
         sessionStorage.setItem("ordercm_comfirmid", comid);
         sessionStorage.setItem("ordercm_actionType", actionType);
         sessionStorage.setItem("ordercm_remark", remark);
         fn_ordercm_index_msg_show();
     }
 }

 function fn_ordercm_confirm_orderid_save(opeuserid) {
     fn_ordercm_index_msg_hide();
     Global.LoadProcess();
     
     $.ajax({
         url: GetPath("/OrderCm/AjxAPPConfirmOrderInfo"),
         type: "POST",
         async: true,
         dataType: "Json",
         data: {
             orderid: sessionStorage.getItem("ordercm_orderid"),
             comfirmid: sessionStorage.getItem("ordercm_comfirmid"),
             actionType: sessionStorage.getItem("ordercm_actionType"),
             remark: sessionStorage.getItem("ordercm_remark"),
             opeuserid: opeuserid,
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid")
         },
         success: function (data) {
             $("#progressbar").hide();
             if (data.message == "success") {
                 fn_ordercm_index_msg_suc_show();
                 setTimeout("fn_ordercm_index_msg_suc_hide()", 1000);
                 refeshList();
             }
             else {
                 $("#dvProcessbg").hide();
                 $("#ordercmerrmsg").css('display', 'block');
                 $("#dvmsgcontent").html(data.message);
                 $("#dvordercmbody").css('margin-top', '-15px');
             }
         },
         error: function (data) {
             //Global.UnLoadProcess();
         }
     });
 }

 function refeshList() {
     $.ajax({
         url: GetPath("/OrderCm/AjxAPPGetIndexMainList"),
         type: "POST",
         dataType: "html",
         data: {
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid")
         },
         success: function (data) {
             $("#dvOderCmIndexMainList").html(data);
             $("#spACInToday").html(document.getElementById("hidOrderCount").value);
             $("#spAcOffToday").html(document.getElementById("hidOrderCount").value);
             $("#spRAOffToday").html(document.getElementById("hidOdReviewCt").value);
             fn_ordercm_main_span_count();
         },
         error: function (data) {
         }
     });
 }

 function fn_ordercm_list_main_search() {
     Global.LoadProcess();
     $.ajax({
         url: GetPath("/OrderCm/AjxAPPGetOrderQueryInfo"),
         type: "POST",
         async: true,
         dataType: "html",
         data: {
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid")
         },
         success: function (data) {
             $("#dvOderCmIndexMainList").html(data);
             Global.UnLoadProcess();
         },
         error: function (data) {
             //Global.UnLoadProcess();
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
     }
     else {
         $("#dvstartDate").css('display', 'none');
         $("#dvendDate").css('display', 'none');
         $("#dvDateBT").css('display', 'none');
     }
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
     var end = DateAdd($("#txt_roomcm_review_enddate").val(), 1);
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

 function fn_ordercm_list_search() {
     Global.LoadProcess();
     var searchDate = "";
     if ($("#btn_roomcm_review_today").hasClass("btn-warning")) {
         searchDate = "Sysdate";
     }
     else if ($("#btn_roomcm_review_otherday").hasClass("btn-warning")) {
         searchDate = $("#txt_roomcm_review_startdate").val() + "_" + $("#txt_roomcm_review_enddate").val();
     }
     $.ajax({
         url: GetPath("/OrderCm/AjxAPPGetOrderQueryList"),
         type: "POST",
         async: true,
         dataType: "html",
         data: {
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid"),
             sdate: searchDate
         },
         success: function (data) {
             $("#dvOrderQueryList").html(data);
             Global.UnLoadProcess();
         },
         error: function (data) {
             //Global.UnLoadProcess();
         }
     });
 }
