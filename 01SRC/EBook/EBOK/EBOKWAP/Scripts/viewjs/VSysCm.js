
function fn_syscm_index_main_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/SysCnf/AjxGetIndexMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            $("#dvSysCmIndexMainList").html(data);
//            $("#spRCInToday").html(document.getElementById("hidRoomCount").value);
//            $("#spRcOffToday").html(document.getElementById("hidRoomCount").value);
            Global.UnLoadProcess();
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}

function fn_syscnf_user_status_change(arg) {
    $("#tduserStatus button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });
    $(arg).addClass("btn-warning");
}

function fn_syscnf_user_detail_show(arg) {
    if (arg == "1") {
        $("#txt_syscnf_usernm").val("");
        $("#txt_syscnf_tel").val("");
        $("#txt_syscnf_remark").val("");
        $("#tduserStatus button").each(function () {
            $(this).removeClass("btn-warning");
            $(this).addClass("btn-default");
        });
        $("#btn_syscnf_user_online").addClass("btn-warning");
        $("#dvUserDetail").html("添加新用户");
    }
    $("#dvSysCnfSaveMsg").html("");
    sessionStorage.setItem("syscnf_action", arg);
    $("#dvProcessbg").show();
    $("#warningMsg").show();
}

function fn_syscnf_user_modify(userId, userName, tel, remark, status) {
    $("#txt_syscnf_usernm").val(userName);
    $("#txt_syscnf_tel").val(tel);
    $("#txt_syscnf_remark").val(remark);
    $("#tduserStatus button").each(function () {
        $(this).removeClass("btn-warning");
        $(this).addClass("btn-default");
    });

    if (status == "1") {
        $("#btn_syscnf_user_online").addClass("btn-warning");
    }
    else {
        $("#btn_syscnf_user_offline").addClass("btn-warning");
    }
    sessionStorage.setItem("syscnf_userid", userId);
    $("#dvUserDetail").html("编辑用户");
    fn_syscnf_user_detail_show('0');
} 

function fn_syscnf_user_detail_hide() {
     $("#dvProcessbg").hide();
     $("#warningMsg").hide();
 }

 function fn_syscnf_user_delewarning_show() {
     $("#dvProcessbg").show();
     $("#delewarningMsg").show();
 }

 function fn_syscnf_user_delewarning_hide() {
     $("#dvProcessbg").hide();
     $("#delewarningMsg").hide();
 }


 function fn_syscnf_user_save_check() {
      var userName = $("#txt_syscnf_usernm").val().trim();
     var tel = $("#txt_syscnf_tel").val().trim();
     var bErr = false;
     var errmsg = "";
     if (userName == "") {
         bErr = true;
         errmsg += "用户名 ";
     }
     if (tel == "") {
         bErr = true;
         errmsg += "联系电话 ";
     }

     if (bErr) {
         $("#dvSysCnfSaveMsg").html("请输入：" + errmsg + "!");
     }
     else {
         sessionStorage.setItem("syscnf_usernm", userName);
         sessionStorage.setItem("syscnf_tel", tel);
         sessionStorage.setItem("syscnf_remark", $("#txt_syscnf_remark").val().trim());
         var userstatus = "";
         if ($("#btn_syscnf_user_online").hasClass("btn-warning")) {
             userstatus = "1";
         }
         else {
             userstatus = "0";
         }
         sessionStorage.setItem("syscnf_status", userstatus);
         fn_syscnf_user_save("");
     }
 }

 function fn_syscnf_user_modify_del(userId, userName) {
     sessionStorage.setItem("syscnf_deluserid", userId);
     sessionStorage.setItem("syscnf_delusernm", userName);
     $("#syscnferrmsg").css('display', 'none');
     $("#dvsyscnfbody").css('margin-top', '0px');
     $("#stdeleUser").html("警告! 是否确认删除用户<br/>[ID:" + userId + "]" + userName + "？");
     fn_syscnf_user_delewarning_show();
 }

 function fn_syscnf_index_msg_suc_show() {
     $("#dvProcessbg").show();
     $("#syscnfsucMsg").show();
 }

 function fn_syscnf_index_msg_suc_hide() {
     $("#dvProcessbg").hide();
     $("#syscnfsucMsg").hide();
 }

 function fn_syscnf_index_msg_suc_time_hide() {
     $("#dvProcessbg").hide();
     $("#syscnfsucMsg").hide();
     fn_syscnf_index_refesh_search();
 }

 function fn_syscnf_user_save(opeuserid) {
     $("#dvProcessbg").css("z-index", 100001);
     Global.LoadProcess();
     $.ajax({
         url: GetPath("/SysCnf/AjxSaveUserInfo"),
         type: "POST",
         async: true,
         dataType: "Json",
         data: {
             userId: sessionStorage.getItem("syscnf_userid"),
             userName: sessionStorage.getItem("syscnf_usernm"),
             tel: sessionStorage.getItem("syscnf_tel"),
             status: sessionStorage.getItem("syscnf_status"),
             remark: sessionStorage.getItem("syscnf_remark"),
             opeuserid: opeuserid,
             action: sessionStorage.getItem("syscnf_action"),
             mac: localStorage.getItem("mac"),
             hid: localStorage.getItem("hid")
         },
         success: function (data) {
             if (data.message == "success") {
                 $("#dvProcessbg").css("z-index", 100000);
                 $("#progressbar").hide();
                 fn_syscnf_user_detail_hide();
                 fn_syscnf_index_msg_suc_show();
                 setTimeout("fn_syscnf_index_msg_suc_time_hide()", 1000);
             }
             else {
                 $("#progressbar").hide();
                 $("#dvSysCnfSaveMsg").html(data.message);
             }
         },
         error: function (data) {
             //Global.UnLoadProcess();
         }
     });
 }


 function fn_syscnf_user_del_save(opeuserid) {
    $("#delewarningMsg").hide();
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/SysCnf/AjxDeleteUserInfo"),
        type: "POST",
        async: true,
        dataType: "Json",
        data: {
            userId: sessionStorage.getItem("syscnf_deluserid"),
            userName: sessionStorage.getItem("syscnf_delusernm"),
            opeuserid: opeuserid,
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            if (data.message == "success") {
                $("#progressbar").hide();
                fn_syscnf_index_msg_suc_show();
                setTimeout("fn_syscnf_index_msg_suc_time_hide()", 1000);
            }
            else {
                Global.UnLoadProcess();
                $("#syscnferrmsg").css('display', 'block');
                $("#dvmsgcontent").html(data.message);
                $("#dvsyscnfbody").css('margin-top', '-15px');
            }
        },
        error: function (data) {
            //Global.UnLoadProcess();
        }
    });
}

function fn_syscnf_index_refesh_search() {
    $.ajax({
        url: GetPath("/SysCnf/AjxGetIndexMainList"),
        type: "POST",
        async: true,
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            $("#dvSysCmIndexMainList").html(data);
            Global.UnLoadProcess();
        },
        error: function (data) {
        }
    });
}