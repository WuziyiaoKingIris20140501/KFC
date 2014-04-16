function fn_city_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/City/GetCityList"),
        type: "POST",
        dataType: "json",
        data: {
            citynm: "shanghai",
            startdt: "",
            enddt: "",
            online: ""
        },
        success: function (data) {
            if (data != null) {
                var outPut = "";
                outPut += "<table class=\"Tb_BodyCSS2\" style=\"border:1px #d5d5d5 solid; padding:1px;\">";
                outPut += "<tr><th>ID</th><th>城市名称</th><th>城市拼音</th><th>优先级</th><th>销售开始时间</th><th>在线状态</th><th>城市类型</th><th>创建时间</th><th>编辑时间</th><th>操作</th></tr>";
                $.each(data, function (i) {
                    outPut += "<tr><td>" + data[i].ID + "</td>";
                    outPut += "<td>" + data[i].CITYNM + "</td>";
                    outPut += "<td>" + data[i].PINYIN + "</td>";
                    outPut += "<td>" + data[i].SEQ + "</td>";
                    outPut += "<td>" + data[i].SALE_HOUR + "</td>";
                    outPut += "<td>" + data[i].ONLINESTATUS + "</td>";
                    outPut += "<td>" + data[i].CITYTYPES + "</td>";
                    outPut += "<td>" + data[i].CDTIME + "</td>";
                    outPut += "<td>" + data[i].UDTIME + "</td>";
                    outPut += "<td><a>编辑</a></td></tr>";
                });
                $("#city_citylist_dv").html(outPut);
                Global.UnLoadProcess();
            }
        },
        error: function (json) {
        }
    });
}



function fn_city_ajx_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/City/AjxGetCityList"),
        type: "POST",
        dataType: "html",
        data: {
            citynm: "shanghai",
            startdt: "",
            enddt: "",
            online: ""
        },
        success: function (data) {
            $("#city_citylist_dv").html(data);
            Global.UnLoadProcess();
        },
        error: function (data) {
        }
    });
}



function SetChkAllStyle() {
    var chkObject = document.getElementById("city_citylist_dv");
    if (chkObject != null) {
        var chkInput = chkObject.getElementsByTagName("input");
        for (var i = 0; i < chkInput.length; i++) {
            if (chkInput[i].type == "checkbox") {
                if (document.getElementById("chk_lis_all").checked == true) {
                    chkInput[i].checked = true;
                }
                else {
                    chkInput[i].checked = false;
                }
            }
        }
    }
}


function fn_show_dialog() {
    $("#dialog").dialog("open");
//    $("#dialog-message").dialog({
//        modal: true,
//        buttons: {
//            Ok: function () {
//                $(this).dialog("close");
//            }
//        }
//    });
}