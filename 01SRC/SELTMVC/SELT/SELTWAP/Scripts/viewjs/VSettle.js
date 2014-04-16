function fn_settle_Clear() {
    $(":text").val("");
    $("#settleMonth").get(0).selectedIndex = 0;
    $("#chkSLSTN").get(0).checked = true;
    $("#chkSLSTY").get(0).checked = false;
    $("#chkSLSTD").get(0).checked = false;
}

function fn_settle_main_search() {
    Global.LoadProcess();
    $.ajax({
        url: GetPath("/Settlement/AjxGetSettleMainList"),
        type: "POST",
        dataType: "html",
        data: {
            month: "",
            unitID: "",
            city: "",
            unitcharge: "",
            orderid: "",
            billid: "",
            saveuser: "",
            slstatus: ""
        },
        success: function (data) {
            $("#dvSettleMainList").html(data);
            Global.UnLoadProcess();
        },
        error: function (data) {
        }
    });
}

function fn_edit_settle_detial(arg) {
    alert(arg);
}