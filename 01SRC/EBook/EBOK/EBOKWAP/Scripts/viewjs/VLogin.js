function fn_Log_in() {
    $.ajax({
        url: GetPath("/Account/Login"),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: "{'userid':'" + document.getElementById("login_id").value + "','pwd':'" + document.getElementById("login_pwd").value + "','remember':'" + document.getElementById("login_remember").checked + "'}",
        success: function (data) {
            if (data != "") {
                $("#dv_login_msg").html(data);
            }
            else {
                var url = GetPath("/Home/Index")
                window.location.href = url;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = "";
        }
    });
}