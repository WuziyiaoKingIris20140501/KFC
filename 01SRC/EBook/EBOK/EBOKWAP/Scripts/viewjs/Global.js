var fullPath = "";
//var fullPath = "http://h5.hotelvp.com";

var prefix = "fre";

function GetPath(url) {
    return fullPath + url;
}

//Global接口
function Global() {
}

Global.LoadProcess = function () {
//    var bgObj = document.getElementById("dvProcessbg");
//    bgObj.style.display = "block";

//    $("#dvprogressbar").progressbar({
//        value: false
//    });

//    var progressbar = $("#dvprogressbar");
//    var progressbarValue = progressbar.find(".ui-progressbar-value");
//    progressbarValue.css({
//        "background": '#F2F2F2'
    //    });



    // cookie清楚后从新调用安卓初始化cookie
//    if (localStorage.getItem("mac") == "" || localStorage.getItem("hid") == "") {
//        
//    }


    $("#dvProcessbg").show();
    $("#progressbar").show();
}

Global.UnLoadProcess = function () {
//    $("#dvprogressbar").hide();
//    var bgObj = document.getElementById("dvProcessbg");
//    bgObj.style.display = "none";
    setTimeout("UnLoadProcess()", 1000);
//    $("#dvProcessbg").hide();
//    $("#progressbar").hide();
}

function UnLoadProcess() {
    $("#dvProcessbg").hide();
    $("#progressbar").hide();
} 

//function fn_Log_out() {
//    $.ajax({
//        url: GetPath("/Account/LogOff"),
//        type: "POST",
//        contentType: "application/json; charset=utf-8",
//        dataType: "html",
//        data: "",
//        success: function (data) {
//            var url = GetPath("/Account/Login")
//            window.location.href = url;
//        },
//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//        }
//  });
//}


function fn_ordercm_main_span_count() {
    $.ajax({
        url: GetPath("/OrderCm/AjxGetSpanCount"),
        type: "POST",
        dataType: "html",
        data: {
            mac: localStorage.getItem("mac"),
            hid: localStorage.getItem("hid")
        },
        success: function (data) {
            var sapns = data.split(',');
            $("#spOcOffToday").html(sapns[0]);
            $("#spAcOffToday").html(sapns[1]);
            Global.UnLoadProcess();
        },
        error: function (data) {
        }
    });
}

String.prototype.Trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}