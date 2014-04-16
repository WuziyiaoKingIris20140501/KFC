var fullPath = "http://localhost:57307";
//var fullPath = "http://h5.hotelvp.com";
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

    $("#dvProcessbg").show();
    $("#progressbar").show();
}

Global.UnLoadProcess = function () {
//    $("#dvprogressbar").hide();
//    var bgObj = document.getElementById("dvProcessbg");
//    bgObj.style.display = "none";

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