<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="OrderConfirmInfoManager.aspx.cs"
    Title="订单确认管理" Inherits="OrderConfirmInfoManager" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/AutoCptControl.ascx" TagName="WebAutoComplete"
    TagPrefix="ac1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <style type="text/css">
        .pcbackground
        {
            display: block;
            width: 100%;
            height: 100%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: while;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 20000;
        }
        .pcprogressBar
        {
            border: solid 2px #3A599C;
            background: white url("/images/progressBar_m.gif") no-repeat 10px 10px;
            display: block;
            width: 148px;
            height: 28px;
            position: fixed;
            top: 50%;
            left: 50%;
            margin-left: -74px;
            margin-top: -14px;
            padding: 10px 10px 10px 50px;
            text-align: left;
            line-height: 27px;
            font-weight: bold;
            position: absolute;
            z-index: 20001;
        }
        .bgDiv2
        {
            opacity: 0.5;
            filter: alpha(opacity=50);
            background: #666666;
            z-index: 100;
            display: none;
            bottom: 0;
            left: 0;
            position: fixed;
            right: 0;
            top: 0;
        }
        .popupDiv2
        {
            width: 1100px;
            height: 600px;
            position: absolute;
            padding: 1px;
            z-index: 10000;
            display: none;
            background-color: White;
            top: 15%;
            left: 25%;
            margin-left: -150px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: -50px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
        
        .Tb_BodyCSS tr
        {
            height: 30px;
        }
        .Tb_BodyCSS td
        {
            height: 30px;
            border-right: 1px #d5d5d5 solid;
            border-top: 1px #d5d5d5 solid;
            padding-left: 10px;
        }
        
        .Tb_BodyCSS
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1pxsolid#ccc;
        }
        
        .Tb_BodyCSS2 tr
        {
            height: 30px;
        }
        .Tb_BodyCSS2 td
        {
            height: 30px;
            border-right: 1px #d5d5d5 solid;
            border-top: 1px #d5d5d5 solid;
        }
        
        .Tb_BodyCSS2
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1pxsolid#ccc;
        }
        
        
        .popupPreView
        {
            width: 1100px;
            height: 600px;
            position: absolute;
            padding: 1px;
            z-index: 11000;
            display: none;
            background-color: White;
            top: 15%;
            left: 25%;
            margin-left: -150px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: -50px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
        
        .flipy
        {
            -moz-transform: scaleY(-1);
            -webkit-transform: scaleY(-1);
            -o-transform: scaleY(-1);
            transform: scaleY(-1); /*IE*/
            filter: FlipH FlipV;
            -moz-transform: rotate(180deg);
            -webkit-transform: rotate(180deg);
            -o-transform: rotate(180deg);
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script language="javascript" type="text/javascript">
        function SetWebAutoControl() {
            document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("WebAutoComplete").value;
            document.getElementById("<%=hidCityID.ClientID%>").value = document.getElementById("wctCity").value;
        }

        document.onkeydown = function (e) {
            e = e ? e : window.event;
            var keyCode = e.which ? e.which : e.keyCode;
            if (keyCode == 27) {
                if (document.getElementById("dvPreView").style.display == "block") {
                    document.getElementById("dvPreView").style.display = "none";
                    document.getElementById("<%=hidimgToBackPre.ClientID%>").value = "";
                    document.getElementById("<%=dvToBackPre.ClientID%>").innerHTML = "";
                }
                else {
                    if (document.getElementById("popupDiv3").style.display == "block") {
                        invokeCloseViewList();
                    }
                    else {
                        invokeCloseList();
                    }
                }
            }
        }

        function isIE() { //ie? 
            if (window.navigator.userAgent.toLowerCase().indexOf("msie") >= 1)
                return true;
            else
                return false;
        }

        //显示弹出的层
        function invokeOpenList() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            //            bgObj.style.width = document.body.offsetWidth + "px";
            //            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
            if (!isIE()) {
                if (document.getElementById("<%=dvErrorInfo.ClientID%>").innerHTML != "") {
                    SetTDMsgShow();
                }
            }
            else {
                if (document.getElementById("<%=dvErrorInfo.ClientID%>").innerText != "") {
                    SetTDMsgShow();
                }
            }
            AJMemoHis("1");
        }

        function invokeOpenListViewAj() {
            AJCHorderViewList();
            document.getElementById("popupDiv3").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            BtnCompleteStyle();
            AJMemoViewHis("1");
        }

        function AJCHorderViewList() {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/GetHCorderViewList",
                type: "POST",
                dataType: "json",
                data: "{orderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "',hotelID:'" + document.getElementById("<%=hidHotelID.ClientID%>").value + "',contTel:'" + document.getElementById("<%=hidContactTel.ClientID%>").value + "'}",
                success: function (data) {
                    var outHput = "";
                    var outCput = "";
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        if (d != null && d[i] != "") {
                            if (d[i].CHTYPE == "1") {
                                outHput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\">";
                                outHput += "<tr align=\"left\"><td align=\"right\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\">房型：</td><td align=\"left\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\">" + d[i].BOOKNUMS + "</td></tr><tr align=\"left\"><td align=\"right\" style=\"width:20%\">订单号：</td><td align=\"left\" valign=\"middle\">" + d[i].ORDERID + "</td></tr><tr align=\"left\"><td align=\"right\" style=\"width:20%\">入住人：</td><td align=\"left\" valign=\"middle\">" + d[i].PERSON + "</td></tr><tr align=\"left\"><td align=\"right\">总价：</td><td align=\"left\" valign=\"middle\">" + d[i].PRICES + "</td></tr><tr align=\"left\"><td align=\"right\">日期：</td><td align=\"left\" valign=\"middle\">" + d[i].INODATE + "</td></tr>";
                                outHput += "</td></tr></table>";
                            } else {
                                // 同用户的订单 点击重刷页面 按钮需要另外定义 需要将原来锁定的所有订单都解锁。并把当前订单加锁。
                                outCput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\">";
                                outCput += "<tr align=\"left\"><td align=\"right\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\">酒店：</td><td align=\"left\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\"><div style=\"float:left\">" + d[i].HOTELNM + "</div><div style=\"float:right;cursor:pointer;margin-right:5px\" onclick=\"BtnLoadStyle();ActionOrderView('" + d[i].ORDERID + "','0')\"<span>[查看]</span></div></td></tr><tr align=\"left\"><td align=\"right\" style=\"width:20%\">入住人：</td><td align=\"left\" valign=\"middle\">" + d[i].PERSON + "</td></tr><tr align=\"left\"><td align=\"right\">总价：</td><td align=\"left\" valign=\"middle\">" + d[i].PRICES + "</td></tr><tr align=\"left\"><td align=\"right\">日期：</td><td align=\"left\" valign=\"middle\">" + d[i].INODATE + "</td></tr>";
                                outCput += "</table>";
                            }
                        }
                    });

                    if (outHput == "" && outCput == "") {
                        document.getElementById("tdODDetail2").style.width = "60%";
                        document.getElementById("tdFXDetail2").style.width = "40%";
                        document.getElementById("tdOTDetail2").style.display = "none";

                        document.getElementById("popupDiv3").style.width = "1100px";
                        document.getElementById("popupDiv3").style.top = "15%";
                        document.getElementById("popupDiv3").style.left = "25%";

                        document.getElementById("dvCLoseImg2").style.marginRight = "-463px";
                    }
                    else {
                        document.getElementById("tdODDetail2").style.width = "40%";
                        document.getElementById("tdFXDetail2").style.width = "32%";
                        document.getElementById("tdOTDetail2").style.width = "28%";
                        document.getElementById("tdOTDetail2").style.display = "";

                        document.getElementById("popupDiv3").style.width = "1280px";
                        document.getElementById("popupDiv3").style.top = "13%";
                        document.getElementById("popupDiv3").style.left = "17%";
                        document.getElementById("dvCLoseImg2").style.marginRight = "-788px";
                    }

                    if (outHput == "") {
                        outHput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\"><tr align=\"left\"><td align=\"center\">本酒店没有其他待确认订单</td></tr>";
                    }

                    if (outCput == "") {
                        outCput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\"><tr align=\"left\"><td align=\"center\">本客人没有其他的未确认订单</td></tr>";
                    }

                    document.getElementById("dvOtherHOrder2").innerHTML = outHput;
                    document.getElementById("dvOtherCOrder2").innerHTML = outCput;
                },
                error: function (json) {
                }
            });
        }



        function AJMemoViewHis(arg) {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/SetMemoVal",
                type: "POST",
                dataType: "json",
                data: "{strKey:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "',ReType:'" + arg + "'}",
                success: function (data) {
                    var output = "<table  class=\"Tb_BodyCSS2\" style=\"border:1px #d5d5d5 solid; padding:1px; margin:-3px 0px 0px 7px;width:98.2%\"><tr><td style=\"width:20%;\" align=\"center\">状态</td><td style=\"width:37%;\" align=\"center\">操作记录</td><td align=\"center\">备注</td></tr>";
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        output += "<tr><td style=\"width:20%;\" align=\"center\">" + d[i].OD_STATUS + "</td><td style=\"width:37%;\" align=\"center\">" + d[i].EVENT_USER + "<br/>" + d[i].EVENT_TIME + "</td>";
                        if (d[i].REMARK.length > 20) {
                            output += "<td title=\"" + d[i].REMARK + "\">" + d[i].REMARK.substring(0, 20) + "..." + "</td></tr>";
                        } else {
                            output += "<td title=\"" + d[i].REMARK + "\">" + d[i].REMARK + "</td></tr>";
                        }
                    });
                    output += "</table>";
                    document.getElementById("<%=lbMemoHis2.ClientID%>").innerHTML = output;
                },
                error: function (json) {
                }
            });
        }













        function invokeOpenListAj() {
            AJCHorderList();
            document.getElementById("popupDiv2").style.display = "block";

            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            //            bgObj.style.width = document.body.offsetWidth + "px";
            //            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
            if (!isIE()) {
                if (document.getElementById("<%=dvErrorInfo.ClientID%>").innerHTML != "") {
                    SetTDMsgShow();
                }
            }
            else {
                if (document.getElementById("<%=dvErrorInfo.ClientID%>").innerText != "") {
                    SetTDMsgShow();
                }
            }
            AJMemoHis("1");
        }

        function AJCHorderList() {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/GetHCorderList",
                type: "POST",
                dataType: "json",
                data: "{orderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "',hotelID:'" + document.getElementById("<%=hidHotelID.ClientID%>").value + "',contTel:'" + document.getElementById("<%=hidContactTel.ClientID%>").value + "'}",
                success: function (data) {
                    var outHput = "";
                    var outCput = "";
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        if (d != null && d[i] != "") {
                            if (d[i].CHTYPE == "1") {
                                outHput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\">";
                                outHput += "<tr align=\"left\"><td align=\"right\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\">房型：</td><td align=\"left\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\">" + d[i].BOOKNUMS + "</td></tr><tr align=\"left\"><td align=\"right\" style=\"width:20%\">订单号：</td><td align=\"left\" valign=\"middle\">" + d[i].ORDERID + "</td></tr><tr align=\"left\"><td align=\"right\" style=\"width:20%\">入住人：</td><td align=\"left\" valign=\"middle\">" + d[i].PERSON + "</td></tr><tr align=\"left\"><td align=\"right\">总价：</td><td align=\"left\" valign=\"middle\">" + d[i].PRICES + "</td></tr><tr align=\"left\"><td align=\"right\">日期：</td><td align=\"left\" valign=\"middle\">" + d[i].INODATE + "</td></tr>";

                                // 自动生成控件  配合  取值的ID来定义  还需要切换 确认和取消2个模式
                                outHput += "<tr align=\"left\"><td align=\"right\" style=\"width:28%\">操作：</td><td align=\"left\" valign=\"middle\"><span style=\"float:left\"><input type=\"radio\" name=rdi_" + d[i].ORDERID + " value=\"1\" onclick=\"SetAutoRdClick('" + d[i].ORDERID + "','1')\" /> 确认 <input type=\"radio\" name=rdi_" + d[i].ORDERID + " value=\"0\" onclick=\"SetAutoRdClick('" + d[i].ORDERID + "','0')\" /> 取消 <input type=\"radio\" name=rdi_" + d[i].ORDERID + " value=\"2\" onclick=\"SetAutoRdClick('" + d[i].ORDERID + "','2')\"/> 备注 </span><span style=\"float:right;margin-right:5px\"><input type=\"checkbox\" id=chk_" + d[i].ORDERID + ">需跟进</span></td></tr>";


                                outHput += "<tr align=\"left\" id=tr_txt_" + d[i].ORDERID + " style=\"display:none;\" ><td align=\"right\" style=\"width:20%\">确认订单号：</td><td align=\"left\" valign=\"middle\"><input type=\"text\" id=txt_od_" + d[i].ORDERID + " value='' style='width:125px;' /></td></tr>";
                                outHput += "<tr align=\"left\" id=tr_sel_" + d[i].ORDERID + " style=\"display:none;\" ><td align=\"right\" style=\"width:20%\">取消原因：</td><td align=\"left\" valign=\"middle\"><select id=sel_" + d[i].ORDERID + " style='width:135px;'><option value =\"CRC01\">满房</option><option value =\"CRH09\">酒店无人接听/无法接通</option><option value=\"CRC06\">酒店变价</option><option value=\"CRH10\">终止合作</option><option value=\"CRH07\">无协议</option><option value=\"CRH11\">不接外宾</option><option value=\"CRC02\">重复订单</option><option value=\"CRG18\">用户取消</option><option value=\"OCDISB\">骚扰订单</option><option value=\"CRG99\">其他</option></select></td></tr>";


                                outHput += "<tr align=\"left\"><td align=\"right\" style=\"width:20%\">操作备注：</td><td align=\"left\" valign=\"middle\">";
                                outHput += "<div style=\"float:left;\" id=dv_txt_" + d[i].ORDERID + ">" + "<input type=\"text\" id=txt_rk_" + d[i].ORDERID + " value='' style='width:125px;' /></div>";
                                outHput += "<div style=\"float:right;margin-right:3px;\"><input type=\"button\" class=\"btn\" id=btnIssue_" + d[i].ORDERID + " value='问题单' style=\"width:55px;padding: 4px 8px 4px;\" onclick=\"OpenODIssuePage('" + d[i].ORDERID + "','" + escape(d[i].ISSNM) + "')\" /></div>";


                                outHput += "</td></tr></table>";
                            } else {
                                // 同用户的订单 点击重刷页面 按钮需要另外定义 需要将原来锁定的所有订单都解锁。并把当前订单加锁。
                                outCput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\">";
                                outCput += "<tr align=\"left\"><td align=\"right\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\">酒店：</td><td align=\"left\" valign=\"middle\" style=\"background-color:#6379B2; height:12px; font-weight:bold;color:White;\"><div style=\"float:left\">" + d[i].HOTELNM + "</div><div style=\"float:right;cursor:pointer;margin-right:5px\" onclick=\"BtnLoadStyle();UnLockPopupArea('" + document.getElementById("<%=hidHotelID.ClientID%>").value + "','" + d[i].ORDERID + "')\"<span>[处理]</span></div></td></tr><tr align=\"left\"><td align=\"right\" style=\"width:20%\">入住人：</td><td align=\"left\" valign=\"middle\">" + d[i].PERSON + "</td></tr><tr align=\"left\"><td align=\"right\">总价：</td><td align=\"left\" valign=\"middle\">" + d[i].PRICES + "</td></tr><tr align=\"left\"><td align=\"right\">日期：</td><td align=\"left\" valign=\"middle\">" + d[i].INODATE + "</td></tr>";
                                outCput += "</table>";
                            }
                        }
                    });

                    if (outHput == "" && outCput == "") {
                        document.getElementById("tdODDetail").style.width = "60%";
                        document.getElementById("tdFXDetail").style.width = "40%";
                        document.getElementById("tdOTDetail").style.display = "none";

                        document.getElementById("popupDiv2").style.width = "1100px";
                        document.getElementById("popupDiv2").style.top = "15%";
                        document.getElementById("popupDiv2").style.left = "25%";

                        document.getElementById("dvCLoseImg").style.marginRight = "-463px";
                    }
                    else {
                        document.getElementById("tdODDetail").style.width = "40%";
                        document.getElementById("tdFXDetail").style.width = "32%";
                        document.getElementById("tdOTDetail").style.width = "28%";
                        document.getElementById("tdOTDetail").style.display = "";

                        document.getElementById("popupDiv2").style.width = "1280px";
                        document.getElementById("popupDiv2").style.top = "13%";
                        document.getElementById("popupDiv2").style.left = "17%";
                        document.getElementById("dvCLoseImg").style.marginRight = "-788px";
                    }

                    if (outHput == "") {
                        outHput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\"><tr align=\"left\"><td align=\"center\">本酒店没有其他待确认订单</td></tr>";
                    }

                    if (outCput == "") {
                        outCput += "<table style=\"border:1px #d5d5d5 solid; padding:1px; margin:3px 0px 0px -8px;width:97%;background-color:White;\"><tr align=\"left\"><td align=\"center\">本客人没有其他的未确认订单</td></tr>";
                    }

                    document.getElementById("dvOtherHOrder").innerHTML = outHput;
                    document.getElementById("dvOtherCOrder").innerHTML = outCput;
                    var cs = document.getElementById("dvOtherHOrder").getElementsByTagName("input");
                    for (i = 0; i < cs.length; i++) {
                        if (cs[i].type == "radio") {
                            if (cs[i].value == "2") {
                                cs[i].checked = true;
                            }
                        }
                    }
                },
                error: function (json) {
                }
            });
        }

        function SetAutoRdClick(orderID, rdType) {
            document.getElementById("dv_txt_" + orderID).style.width = "150px";
            document.getElementById("dv_txt_" + orderID).style.marginleft = "3px";

            if (rdType == "1") {
                document.getElementById("tr_txt_" + orderID).style.display = "";
                document.getElementById("tr_sel_" + orderID).style.display = "none";
            }
            else if (rdType == "0") {
                document.getElementById("tr_txt_" + orderID).style.display = "none";
                document.getElementById("tr_sel_" + orderID).style.display = "";
            }
            else if (rdType == "2") {
                document.getElementById("tr_txt_" + orderID).style.display = "none";
                document.getElementById("tr_sel_" + orderID).style.display = "none";
            }
        }

        //隐藏弹出的层
        function invokeCloseList() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";

            document.getElementById("<%=hidUnlockHotelID.ClientID%>").value = document.getElementById("<%=hidHotelID.ClientID%>").value;
            document.getElementById("<%=btnSetUnlock.ClientID%>").click();
        }

        function invokeCloseViewList() {
            document.getElementById("popupDiv3").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function SetSpRekButtonStyle() {
            document.getElementById("<%=spRekButton.ClientID%>").style.display = "";
            document.getElementById("spEx").style.width = "84%";
        }

        function PopupArea(arg) {
            document.getElementById("<%=hidOrderID.ClientID%>").value = arg;
            document.getElementById("<%=btnlock.ClientID%>").click();
        }

        function PopupViewArea(arg) {
            document.getElementById("<%=hidOrderID.ClientID%>").value = arg;
            document.getElementById("<%=btnView.ClientID%>").click();
        }

        function UnLockPopupArea(hotelid, arg) {
            document.getElementById("<%=hidUnlockHotelID.ClientID%>").value = hotelid;
            document.getElementById("<%=hidOrderID.ClientID%>").value = arg;
            document.getElementById("<%=btnUnLockPopupArea.ClientID%>").click();
        }

        function AJMemoHis(arg) {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/SetMemoVal",
                type: "POST",
                dataType: "json",
                data: "{strKey:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "',ReType:'" + arg + "'}",
                success: function (data) {
                    var output = "<table  class=\"Tb_BodyCSS2\" style=\"border:1px #d5d5d5 solid; padding:1px; margin:-3px 0px 0px 7px;width:98.2%\"><tr><td style=\"width:20%;\" align=\"center\">状态</td><td style=\"width:37%;\" align=\"center\">操作记录</td><td align=\"center\">备注</td></tr>";
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        output += "<tr><td style=\"width:20%;\" align=\"center\">" + d[i].OD_STATUS + "</td><td style=\"width:37%;\" align=\"center\">" + d[i].EVENT_USER + "<br/>" + d[i].EVENT_TIME + "</td>";
                        if (d[i].REMARK.length > 20) {
                            output += "<td title=\"" + d[i].REMARK + "\">" + d[i].REMARK.substring(0, 20) + "..." + "</td></tr>";
                        } else {
                            output += "<td title=\"" + d[i].REMARK + "\">" + d[i].REMARK + "</td></tr>";
                        }
                    });
                    output += "</table>";
                    document.getElementById("<%=lbMemoHis.ClientID%>").innerHTML = output;
                },
                error: function (json) {
                }
            });
        }

        function AJUnlockOrder() {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/UnlockOrder",
                type: "POST",
                dataType: "json",
                data: "{OrderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "'}",
                success: function (data) {
                    var output = data.d;
                    var arrmp = output.split("|");
                    if (arrmp.length > 0) {
                        document.getElementById("<%=imgAlert.ClientID%>").src = arrmp[0];
                        document.getElementById("<%=dvImg.ClientID%>").style.marginLeft = arrmp[1];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerHTML = arrmp[2];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerText = arrmp[2];
                        if (arrmp[3] != "") {
                            document.getElementById("<%=lbActionUser.ClientID%>").innerHTML = arrmp[3];
                            document.getElementById("<%=lbActionUser.ClientID%>").innerText = arrmp[3];
                        }
                        var ajaxtdMsg = $("#dvMsg");
                        if ($("#dvMsg").is(":hidden")) {
                            ajaxtdMsg.slideDown("fast");
                        }
                    }
                    BtnCompleteStyle();
                },
                error: function (json) {
                    BtnCompleteStyle();
                }
            });
        }

        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        function ClearDateControl() {
            if (document.getElementById("<%=txtOrderID.ClientID%>").value.trim() != "") {
                document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
                document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
                document.getElementById("<%=ddpHotelConfirm.ClientID%>").selectedIndex = 0;

                var vpChkid = document.getElementById("<%=chklBStatusOther.ClientID%>");
                //得到所有radio
                var vpChkidList = vpChkid.getElementsByTagName("INPUT");
                for (var i = 0; i < vpChkidList.length; i++) {
                    vpChkidList[i].checked = false;
                }
            }
        }

        function OpenIssuePage() {
            var arg = document.getElementById("<%=hidOrderID.ClientID%>").value;
            var IsuNm = document.getElementById("<%=hidIssueNm.ClientID%>").value;
            var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                fulls += ",height=" + ah;
                fulls += ",innerHeight=" + ah;
                fulls += ",width=" + aw;
                fulls += ",innerWidth=" + aw;
                fulls += ",resizable"
            } else {
                fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
            }
            var time = new Date();
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=0&RID=' + arg + "&IsuNm=" + escape(IsuNm) + "&time=" + time, null, fulls);
        }


        function OpenODIssuePage(arg, IsuNm) {
            var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                fulls += ",height=" + ah;
                fulls += ",innerHeight=" + ah;
                fulls += ",width=" + aw;
                fulls += ",innerWidth=" + aw;
                fulls += ",resizable"
            } else {
                fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
            }
            var time = new Date();
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=0&RID=' + arg + "&IsuNm=" + IsuNm + "&time=" + time, null, fulls);
        }

        function SetTDMsgShow() {
            var ajaxtdMsg = $("#dvMsg");
            if ($("#dvMsg").is(":hidden")) {
                ajaxtdMsg.slideDown("fast");
            }
            else {
                ajaxtdMsg.slideUp("fast");
            }
        }

        function SetTBSearchHide() {
            var ajaxtdMsg = $("#dvSearch");
            ajaxtdMsg.slideUp(100);
            document.getElementById("btnTb").value = "▼";
            document.getElementById("btnTb").text = "▼";
        }

        function SetTBSearchShow() {
            var ajaxtdMsg = $("#dvSearch");
            if ($("#dvSearch").is(":hidden")) {
                ajaxtdMsg.slideDown(100);

                document.getElementById("btnTb").value = "▲";
                document.getElementById("btnTb").text = "▲";
            }
            else {
                ajaxtdMsg.slideUp(100);
                document.getElementById("btnTb").value = "▼";
                document.getElementById("btnTb").text = "▼";

            }
        }

        function SetRdClick(arg) {
            if (arg == "0") {
                document.getElementById("<%=trComfIn.ClientID%>").style.display = "none";
                document.getElementById("<%=trCanlReson.ClientID%>").style.display = "";
            }
            else if (arg == "1") {
                document.getElementById("<%=trComfIn.ClientID%>").style.display = "";
                document.getElementById("<%=trCanlReson.ClientID%>").style.display = "none";
            }
            else if (arg == "2") {
                document.getElementById("<%=trComfIn.ClientID%>").style.display = "none";
                document.getElementById("<%=trCanlReson.ClientID%>").style.display = "none";
            }
        }

        function PopupPrintArea() {
            var time = new Date(); //OrderOperationPrint OrderConfirmInfoPrint
            window.showModalDialog("OrderConfirmInfoPrint.aspx?ID=" + document.getElementById("<%=hidOrderID.ClientID%>").value + "&time=" + time, window, "dialogWidth:800px;dialogHeight:750px;center:yes;status:no;scroll:yes;help:no;");
        }

        function ConfirmSendFax() {
            var result = window.confirm("确定发送传真？");
            if (result) {
                BtnLoadStyle();
                return true;
            } else {
                return false;
            }
        }

        function SetImagePreview(arg) {
            AJImagePreviewList(arg);
            document.getElementById("dvPreView").style.display = "block";
        }

        function bbimg(o) {
            var zoom = parseInt(o.style.zoom, 10) || 100;
            zoom += event.wheelDelta / 12;
            if (zoom > 0) o.style.zoom = zoom + '%';
            return false;
        }

        function wheel(event) {
            var delta = 0;
            var div = document.getElementById("<%=dvToBackPre.ClientID%>");
            if (!event) /**//* For IE. */
            {
                event = window.event;
            }
            if (event.wheelDelta) {
                //让一个是IE
                //if(event.srcElement.tagName=="IMG" && div.contains(event.srcElement))
                //{
                //event.srcElement.width+=event.wheelDelta/15;
                // return false;
                //}
            }
            else if (event.detail) {
                //如果是firefox
                var allImg = div.childNodes
                var isThis = false; //现判断鼠标中仑的元素是不是包含在那个div里面
                for (i = 0; i < allImg.length; i++) {
                    if (allImg[i] == event.target) {
                        isThis = true;
                    }
                }
                if (isThis && event.target.tagName == "INPUT") {
                    $(event.target).width(event.target.width + event.detail * 12);
                    $(event.target).height(event.target.height + event.detail * 12);
                    event.returnValue = false;
                }
            }
            return true;
        }

        function RotateRightImage() {
            var div = document.getElementById("<%=dvToBackPre.ClientID%>");
            var lis = div.childNodes;
            for (var i = 0; i < lis.length; i++) {
                if (lis.item(i).className == "") {
                    lis.item(i).setAttribute("class", "flipy");
                    lis.item(i).setAttribute("className", "flipy");
                } else {
                    lis.item(i).setAttribute("class", "");
                    lis.item(i).setAttribute("className", "");
                }
                lis.item(i).onmousewheel = function () { return bbimg(this) };
            }
        }

        function changeImg(btn, arg) //鼠标移入，更换图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock_press.png";
            }
            else {
                btn.src = "../../Images/clock_press.png";
            }
        }
        function changeback(btn, arg)  //鼠标移出，换回原来的图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock.png";
            }
            else {
                btn.src = "../../Images/clock.png";
            }
        }

        function PreViewFaxImg(imgToBack, aLink, atype) {
            document.getElementById("<%=hidimgToBackPre.ClientID%>").value = imgToBack;
            var output = "";
            if (imgToBack.indexOf(",") != -1) {
                var arrmp = imgToBack.split(",");
                for (i = 0; i < arrmp.length; i++) {
                    if (arrmp[i] != "") {
                        output += "<input type=\"image\" id=\"imagediv\" src='" + arrmp[i] + "' onclick=\"return false;\" onload=\"javascript\:if(this.width>screen.width-500)this.style.width=screen.width-500;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1100px;\"/><br/>";
                    }
                }
            }
            else {
                output += "<input type=\"image\" id=\"imagediv\" src='" + imgToBack + "' onclick=\"return false;\" onload=\"javascript\:if(this.width>screen.width-500)this.style.width=screen.width-500;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1100px;\"/>";
            }
            document.getElementById("<%=dvToBackPre.ClientID%>").innerHTML = output;

            if (window.addEventListener) {
                window.addEventListener('DOMMouseScroll', wheel, false); //给firefox添加鼠标滚动事件
            }

            var tbList = document.getElementById("tbPreList");
            if (tbList != null) {
                var rows = tbList.rows;
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i].cells(0).innerText == aLink) {
                        rows[i].style.backgroundColor = "#f6f6f6";
                        rows[i].onmouseover = "";
                        rows[i].onmouseout = "";
                    }
                    else {
                        if (rows[i].cells(1).innerText == "0") {
                            rows[i].style.backgroundColor = "White";
                        }
                        else {
                            rows[i].style.backgroundColor = "#E9EBF2";
                        }
                        rows[i].onmouseover = function () { d = this.style.backgroundColor; this.style.backgroundColor = '#f6f6f6' };
                        rows[i].onmouseout = function () { this.style.backgroundColor = d };
                    }
                }
            }
        }



        function AJImagePreviewList(arg) {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/SetImagePreviewList",
                type: "POST",
                dataType: "json",
                data: "{strKey:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "',ReType:'" + arg + "'}",
                success: function (data) {
                    var output = "<table id=\"tbPreList\" class=\"Tb_BodyCSS2\" style=\"border:1px #d5d5d5 solid;margin-left:1px;padding:1px;width:99.2%;\">";
                    var d = jQuery.parseJSON(data.d);
                    var sendFx = "-1";
                    var inum = 0;
                    $.each(d, function (i) {
                        if (sendFx != d[i].FAXNID) {
                            output += "<tr style=\"background-color:#E9EBF2\" onmouseover=\"d=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'\" onmouseout=\"this.style.backgroundColor=d\">"
                            inum = inum + 1
                            output += "<td style=\"display:none;\" align=\"center\">" + inum + "</td>";
                            output += "<td style=\"display:none;\" align=\"center\">1</td>";
                            output += "<td style=\"width:100%;\" align=\"left\" onclick=\"PreViewFaxImg('" + d[i].FAXTURL + "', '" + inum + "')\"><span id=\"aPreimage\" style=\"margin-left:5px;margin-right:10px;width:100%\">[" + d[i].FAXTST + "]&nbsp;" + d[i].FAXDT + "</span></td>";
                            output += "</tr>";

                            if (d[i].FAXBST != "") {
                                inum = inum + 1
                                output += "<tr onmouseover=\"d=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'\" onmouseout=\"this.style.backgroundColor=d\">"
                                output += "<td style=\"display:none;\" align=\"center\">" + inum + "</td>";
                                output += "<td style=\"display:none;\" align=\"center\">0</td>";
                                output += "<td style=\"width:100%;\" align=\"right\" onclick=\"PreViewFaxImg('" + d[i].FAXBURL + "', '" + inum + "')\"><span id=\"aPreimage\" style=\"margin-right:10px;width:100%\">[" + d[i].FAXBST + "]&nbsp;" + d[i].FAXBDT + "</span></td>";
                                output += "</tr>";
                            }
                            sendFx = d[i].FAXNID;
                        }
                        else {
                            inum = inum + 1
                            output += "<tr onmouseover=\"d=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'\" onmouseout=\"this.style.backgroundColor=d\">"
                            output += "<td style=\"display:none;\" align=\"center\">" + inum + "</td>";
                            output += "<td style=\"display:none;\" align=\"center\">0</td>";
                            output += "<td style=\"width:100%;\" align=\"right\" onclick=\"PreViewFaxImg('" + d[i].FAXBURL + "', '" + inum + "')\"><span id=\"aPreimage\" style=\"margin-right:10px;width:100%\">[" + d[i].FAXBST + "]&nbsp;" + d[i].FAXBDT + "</span></td>";
                            output += "</tr>";
                        }
                    });
                    output += "</table>";
                    document.getElementById("<%=dvImagePreList.ClientID%>").innerHTML = output;
                },
                error: function (json) {
                }
            });
        }

        function doPrint() {
            var time = new Date();
            window.showModalDialog("OrderConfirmInfoImagePrint.aspx?ID=" + document.getElementById("<%=hidimgToBackPre.ClientID%>").value + "&time=" + time, window, "dialogWidth:800px;dialogHeight:750px;center:yes;status:no;scroll:yes;help:no;");
        }

        function ClosePrint() {
            document.getElementById("dvPreView").style.display = "none";
            document.getElementById("<%=hidimgToBackPre.ClientID%>").value = "";
            document.getElementById("<%=dvToBackPre.ClientID%>").innerHTML = "";
        }


        function AJReSendFaxOrder() {
            if (!ConfirmSendFax()) {
                return;
            }
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/ReSendFaxOrder",
                type: "POST",
                dataType: "json",
                data: "{OrderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "', HotelID:'" + document.getElementById("<%=hidHotelID.ClientID%>").value + "'}",
                success: function (data) {
                    var output = data.d;
                    var arrmp = output.split("|");
                    if (arrmp.length > 0) {
                        document.getElementById("<%=imgAlert.ClientID%>").src = arrmp[0];
                        document.getElementById("<%=dvImg.ClientID%>").style.marginLeft = arrmp[1];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerHTML = arrmp[2];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerText = arrmp[2];
                        var ajaxtdMsg = $("#dvMsg");
                        if ($("#dvMsg").is(":hidden")) {
                            ajaxtdMsg.slideDown("fast");
                        }
                    }
                    BtnCompleteStyle();
                },
                error: function (json) {
                    BtnCompleteStyle();
                }
            });
        }


        function AJModifyRemark() {
            BtnLoadStyle();
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/ModifyRemark",
                type: "POST",
                dataType: "json",
                data: "{HotelID:'" + document.getElementById("<%=hidHotelID.ClientID%>").value + "', Remark:'" + document.getElementById("<%=txtExRemark.ClientID%>").value + "',OrderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "'}",
                success: function (data) {
                    var output = data.d;
                    var arrmp = output.split("|");
                    if (arrmp.length > 0) {
                        document.getElementById("<%=imgAlert.ClientID%>").src = arrmp[0];
                        document.getElementById("<%=dvImg.ClientID%>").style.marginLeft = arrmp[1];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerHTML = arrmp[2];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerText = arrmp[2];
                        var ajaxtdMsg = $("#dvMsg");
                        if ($("#dvMsg").is(":hidden")) {
                            ajaxtdMsg.slideDown("fast");
                        }
                    }
                    BtnCompleteStyle();
                },
                error: function (json) {
                    BtnCompleteStyle();
                }
            });
        }

        function AJRestOrderVal() {
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/RestOrderVal",
                type: "POST",
                dataType: "json",
                data: "{OrderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "'}",
                success: function (data) {
                    var output = data.d;
                    var arrmp = output.split("|");
                    if (arrmp.length > 0) {
                        document.getElementById("<%=lbBOOK_STATUS.ClientID%>").innerHTML = arrmp[0];
                        document.getElementById("<%=lbBOOK_STATUS.ClientID%>").innerText = arrmp[0];
                        document.getElementById("<%=lbBOOK_REMARK.ClientID%>").innerHTML = arrmp[1];
                        document.getElementById("<%=lbBOOK_REMARK.ClientID%>").innerText = arrmp[1];
                        document.getElementById("<%=lbFaxStatus.ClientID%>").innerHTML = arrmp[2];
                        document.getElementById("<%=lbFaxStatus.ClientID%>").innerText = arrmp[2];
                    }
                },
                error: function (json) {
                }
            });
        }

        function AJSaveOrderList() {
            ChkOrderList();
            $.ajax({
                contentType: "application/json",
                url: "OrderConfirmInfoManager.aspx/SaveOrderList",
                type: "POST",
                dataType: "json",
                data: "{OrderList:'" + document.getElementById("<%=hidCommonList.ClientID%>").value + "', OrderID:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "'}",
                success: function (data) {
                    var output = data.d;
                    var arrmp = output.split("|");
                    if (arrmp.length > 0) {
                        document.getElementById("<%=imgAlert.ClientID%>").src = arrmp[0];
                        document.getElementById("<%=dvImg.ClientID%>").style.marginLeft = arrmp[1];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerHTML = arrmp[2];
                        document.getElementById("<%=dvErrorInfo.ClientID%>").innerText = arrmp[2];
                        var ajaxtdMsg = $("#dvMsg");
                        if ($("#dvMsg").is(":hidden")) {
                            ajaxtdMsg.slideDown("fast");
                        }
                        AJRestOrderVal();
                        AJMemoHis("1");
                    }
                    BtnCompleteStyle();
                },
                error: function (json) {
                    BtnCompleteStyle();
                }
            });
        }

        function ChkOrderList() {
            var commidList = "";

            if (document.getElementById("<%=rdbCofIn.ClientID%>").checked) {
                commidList = commidList + document.getElementById("<%=hidOrderID.ClientID%>").value + "_1_" + document.getElementById("<%=txtCofNum.ClientID%>").value + "_" + document.getElementById("<%=txtBOOK_REMARK.ClientID%>").value + "_" + document.getElementById("<%=chkFollowUp.ClientID%>").checked + ",";
            }
            else if (document.getElementById("<%=rdbCofCal.ClientID%>").checked) {
                commidList = commidList + document.getElementById("<%=hidOrderID.ClientID%>").value + "_0_" + document.getElementById("<%=ddpCanelReson.ClientID%>").options[document.getElementById("<%=ddpCanelReson.ClientID%>").selectedIndex].value + "_" + document.getElementById("<%=txtBOOK_REMARK.ClientID%>").value + "_" + document.getElementById("<%=chkFollowUp.ClientID%>").checked + ",";
            }
            else if (document.getElementById("<%=rdbCofRe.ClientID%>").checked) {
                commidList = commidList + document.getElementById("<%=hidOrderID.ClientID%>").value + "_2_" + document.getElementById("<%=txtBOOK_REMARK.ClientID%>").value + "_" + document.getElementById("<%=chkFollowUp.ClientID%>").checked + ",";
            }

            var csid = "";
            var cs = document.getElementById("dvOtherHOrder").getElementsByTagName("input");
            for (i = 0; i < cs.length; i++) {
                if (cs[i].type == "radio") {
                    if (cs[i].checked) {
                        csid = cs[i].name.split("_")[1];
                        if (cs[i].value == "0") {
                            commidList = commidList + csid + "_" + cs[i].value.toString() + "_" + document.getElementById("sel_" + csid).options[document.getElementById("sel_" + csid).selectedIndex].value + "_" + document.getElementById("txt_rk_" + csid).value + "_" + document.getElementById("chk_" + csid).checked + ",";
                        }
                        else if (cs[i].value == "2") {
                            commidList = commidList + csid + "_" + cs[i].value.toString() + "_" + document.getElementById("txt_rk_" + csid).value + "_" + document.getElementById("chk_" + csid).checked + ",";
                        }
                        else {
                            commidList = commidList + csid + "_" + cs[i].value.toString() + "_" + document.getElementById("txt_od_" + csid).value + "_" + document.getElementById("txt_rk_" + csid).value + "_" + document.getElementById("chk_" + csid).checked + ",";
                        }
                    }
                }
            }
            document.getElementById("<%=hidCommonList.ClientID%>").value = commidList;
            //alert(commidList);
        }

        function ActionOrderView(orderID, Dtype) {
            document.getElementById("<%=hidDType.ClientID%>").value = Dtype;
            if (Dtype == "0") {
                BtnLoadStyle();
                PopupViewArea(orderID);
            }
            else {
                BtnLoadStyle();
                PopupArea(orderID);
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <div class="frame01">
            <ul>
                <li class="title">订单确认</li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div id="dvSearch">
                                <table width="98%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="right">
                                            城市：
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server" Width="100px" />--%>
                                            <ac1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" CTLLEN="130px" AutoType="city"
                                                AutoParent="OrderConfirmInfoManager.aspx?Type=city" />
                                        </td>
                                        <td align="right">
                                            排序：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddpSort" CssClass="noborder_inactive" runat="server" Width="130px" />
                                        </td>
                                        <td align="right">
                                            下单时间：
                                        </td>
                                        <td>
                                            <input id="dpCreateStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})"
                                                runat="server" />
                                            <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})"
                                                runat="server" />
                                        </td>
                                        <td>
                                            价格代码:
                                        </td>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="chkPriceCode" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" ></asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            操作状态：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddpControlStatus" CssClass="noborder_inactive" runat="server"
                                                Width="130px" />
                                        </td>
                                        <td align="right">
                                            传真发送：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddpFaxStatus" CssClass="noborder_inactive" runat="server" Width="130px" />
                                        </td>
                                        <td align="right">
                                            确认状态：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddpHotelConfirm" CssClass="noborder_inactive" runat="server"
                                                Width="130px" />
                                        </td>
                                        <td align="right">
                                            订单状态：
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="ddpBStatusOther" CssClass="noborder_inactive" runat="server" Width="100px" />--%>
                                            <asp:CheckBoxList ID="chklBStatusOther" runat="server" RepeatDirection="Vertical"
                                                RepeatColumns="8" RepeatLayout="Table" CellSpacing="8" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table width="98%">
                                <tr>
                                    <td style="width: 6px">
                                    </td>
                                    <td align="right">
                                        酒店：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete"
                                            AutoType="hotel" AutoParent="OrderConfirmInfoManager.aspx?Type=hotel" />
                                    </td>
                                    <td align="right">
                                        用户：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserID" runat="server" Width="150px" MaxLength="100" />
                                    </td>
                                    <td align="right">
                                        订单：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrderID" runat="server" Width="150px" MaxLength="100" onkeyup="ClearDateControl()"
                                            onkeypress="ClearDateControl()" onkeydown="ClearDateControl()" />
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索订单"
                                                    OnClientClick="SetWebAutoControl();SetTBSearchHide();BtnLoadStyle();" OnClick="btnSearch_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnRefresh" runat="server" CssClass="btn primary" Text="刷新列表" OnClientClick="BtnLoadStyle();"
                                                    OnClick="btnRefresh_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left">
                                        <input type="button" class="btn primary" id="btnTb" value='▲' onclick="SetTBSearchShow()" />
                                    </td>
                                    <td>
                                        <div style="display: none">
                                            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnlock" runat="server" CssClass="btn primary" Text="加锁" OnClick="btnlock_Click" />
                                                    <asp:Button ID="btnUnLockPopupArea" runat="server" CssClass="btn primary" Text="加锁"
                                                        OnClick="btnUnLockPopupArea_Click" />
                                                    <asp:Button ID="btnView" runat="server" CssClass="btn primary" Text="查看" OnClick="btnView_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
        <div class="frame02">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" CssClass="GView_BodyCSS"
                        OnRowDataBound="gridViewCSList_RowDataBound">
                        <Columns>
                            <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="订单号">
                      <ItemTemplate >
                        <a href="###" onclick="BtnLoadStyle();PopupArea('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>')"><%# DataBinder.Eval(Container.DataItem, "ORDERID") %></a>
                      </ItemTemplate>
                    </asp:TemplateField>
                   <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="ORDERID" DataNavigateUrlFormatString="OrderOperation.aspx?ID={0}" 
                     Target="_blank" DataTextField="ORDERID"><ItemStyle HorizontalAlign="Center"  Width="5%" /></asp:HyperLinkField>--%>
                            <asp:BoundField DataField="ORDERID" HeaderText="订单号">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CITYNM" HeaderText="城市">
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GUESTNAMES" HeaderText="入住人">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LOGINMOBILE" HeaderText="预订人电话">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" HeaderText="创建时间">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MODIFYST" HeaderText="状态">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MODIFYPE" HeaderText="最后操作人">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FAXSEND" HeaderText="传真">
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REFAXSTATUS" HeaderText="回传">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HOTELDIS" HeaderText="确认状态">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDECTIME" HeaderText="创建时间" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BOOKSTATUS" HeaderText="确认状态" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORSTATUS" HeaderText="订单状态">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REDDIS" HeaderText="系统时间" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FOLLOWUP" HeaderText="跟进">
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审核操作" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="button" class="btn primary" id="btnOpe" style="width: 65px; height: 23px;
                                        padding: 2px 2px 2px 2px" value='处理' onclick="ActionOrderView('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>','1')" />
                                    <input type="button" class="btn" id="btnView" style="width: 35px; height: 23px; padding: 2px 2px 2px 2px"
                                        value='查看' onclick="ActionOrderView('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>','0')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidCityID" runat="server" />
                <asp:HiddenField ID="hidHotelID" runat="server" />
                <asp:HiddenField ID="hidUnlockHotelID" runat="server" />
                <asp:HiddenField ID="hidOrderID" runat="server" />
                <asp:HiddenField ID="hidContactTel" runat="server" />
                <asp:HiddenField ID="hidPriceCode" runat="server" />
                <asp:HiddenField ID="hidIssueNm" runat="server" />
                <asp:HiddenField ID="hidimgToBackPre" runat="server" />
                <asp:HiddenField ID="hidCommonList" runat="server" />
                <asp:HiddenField ID="hidDType" runat="server" />
                <div id="dvPreView" class="popupPreView">
                    <div style="width: 99%; height: 99%; margin: 0,0,0,0">
                        <table width="100%;height:100%">
                            <tr>
                                <td style="width: 20%; height: 100%" valign="top" align="right">
                                    <table style="border: 0px; padding: 0px; margin: 0px 0px 0px 2px; width: 101%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 9px; font-weight: bold; color: White;">
                                                传真详情
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="dvImagePreList" runat="server" style="width: 99.4%; height: 550px; overflow-y: auto;
                                        margin-left: 4px; margin-top: -3px">
                                    </div>
                                </td>
                                <td style="width: 80%; height: 100%" valign="top" align="left">
                                    <table style="border: 0px; padding: 0px; margin: 0px 0px 0px 5px; width: 100%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left; margin-top: 5px">
                                                    传真预览
                                                </div>
                                                <div style="float: left; margin-left: 15px; margin-top: -3px">
                                                    <img alt="" style="width: 30px; height: 30px" src="../../Images/clock.png" title="传真向右旋转90°"
                                                        onmousedown="changeImg(this, '1')" onmouseout="changeback(this, '1')" onclick="RotateRightImage()" />
                                                </div>
                                                <div style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn" id="btnPrintImage" value='打印' onclick="doPrint()" />
                                                </div>
                                                <div style="float: right; margin-right: -87px; margin-top: -26px">
                                                    <img src="../../Styles/images/close.png" style="cursor: pointer" alt="关闭" title="关闭"
                                                        onclick="ClosePrint()" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbPreList" class="Tb_BodyCSS2" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 7px; width: 99.6%">
                                        <tr style="height: 530px; width: 100%">
                                            <td style="height: 530px; width: 100%">
                                                <div runat="server" id="dvToBackPre" style="float: left; width: 100%; height: 550px;
                                                    overflow-y: auto;">
                                                    <%--<asp:Image runat="server" ID="imgToBackPre" Width="100%" Height="900px" />--%>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="background" class="pcbackground" style="display: none;">
                </div>
                <div id="progressBar" class="pcprogressBar" style="display: none;">
                    数据加载中，请稍等...</div>
                <div id="bgDiv2" class="bgDiv2">
                </div>
                <div id="popupDiv2" class="popupDiv2">
                    <div style="width: 99%; height: 99%; margin: 0,0,0,0">
                        <table width="100%">
                            <tr>
                                <td style="width: 60%" id="tdODDetail" valign="top" align="right">
                                    <div style="float: right; margin-right: -463px; margin-top: -18px" id="dvCLoseImg">
                                        <img src="../../Styles/images/close.png" style="cursor: pointer" alt="关闭" title="关闭"
                                            onclick="invokeCloseList()" />
                                    </div>
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 3px; width: 100.2%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                订单详情
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbDetail" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 5px; width: 99.4%; height: 100%">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right" width="20%">
                                                酒店名称：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbHOTEL_NAME" runat="server" />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right">
                                                酒店联系人：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbHotel_ex" runat="server" />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right">
                                                入住人：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbGUEST_NAMES" runat="server" />
                                                &nbsp;&nbsp;电话：
                                                <asp:Label ID="lbCONTACT_TEL" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                入住天数：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbOrderDays" runat="server" />晚&nbsp;&nbsp;（<asp:Label ID="lbIN_DATE"
                                                    runat="server" />
                                                至
                                                <asp:Label ID="lbOUT_DATE" runat="server" />） 到店时间：<asp:Label ID="lbARRIVE_TIME"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订房型：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbROOM_TYPE_NAME" runat="server" />&nbsp;&nbsp;<asp:Label ID="lbBOOK_ROOM_NUM"
                                                    runat="server" />间
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                总价/支付：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbTotalPrice" runat="server" />&nbsp;&nbsp;<asp:Label ID="lbPRICE_CODE"
                                                    runat="server" />&nbsp;&nbsp;<asp:Label ID="lbPAY_STATUS" runat="server" /><span id="PayMethod" runat="server" style="color:Red" ></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                早餐宽带：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbBreakNet" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                订单说明：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbBOOK_REMARK" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订人电话：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbLOGIN_MOBILE" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                优惠券：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbTICKET_PAGENM" runat="server" />
                                                &nbsp;&nbsp;<asp:Label ID="lbTICKET_AMOUNT" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                担保信息：
                                            </td>
                                            <td align="left">
                                                <%--<asp:Label ID="lbRESV_GUA_NM" runat="server" />&nbsp;&nbsp;/&nbsp;&nbsp;--%>
                                                <asp:Label ID="lbIS_GUA" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订时间：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbCREATE_TIME" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订渠道：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbORDER_CHANNEL" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                酒店供应商：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbVendorNM" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                酒店销售：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblSalesMG" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                酒店备注：
                                            </td>
                                            <td align="left">
                                                <span style="float: left; width: 99%" id="spEx">
                                                    <asp:TextBox ID="txtExRemark" runat="server" TextMode="MultiLine" Width="97%" Height="40px"
                                                        onfocus="SetSpRekButtonStyle()" />
                                                </span><span style="float: right; width: 14%; margin-right: 5px" runat="server" id="spRekButton">
                                                    <%--<asp:Button ID="btnModifyRemark" runat="server" CssClass="btn primary" Width="75px" Text="修改" OnClientClick="BtnLoadStyle();" OnClick="btnModifyRemark_Click" />--%>
                                                    <input type="button" class="btn primary" id="btnModifyRemark" value='修改' onclick="AJModifyRemark()" />
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 40%;" id="tdFXDetail" valign="top" align="left">
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 5px; width: 100%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left">
                                                    订单操作（<asp:Label ID="lbActionUser" runat="server" />正在操作）
                                                </div>
                                                <div style="float: right; margin-right: 5px">
                                                    订单号：<asp:Label ID="lbORDER_NUM" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbControl" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 7px; width: 99.1%">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right" style="width: 23%">
                                                订单状态：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbBOOK_STATUS" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订传真：
                                            </td>
                                            <td align="left" valign="middle">
                                                <span style="float: left; margin-top: 3px">
                                                    <asp:Label ID="lbFaxStatus" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lbFaxStatusBK"
                                                        runat="server" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn primary" id="btnFindSend" value='查看' onclick="SetImagePreview('1')" />
                                                    <%--<asp:Button ID="btnSendFax" runat="server" CssClass="btn" OnClientClick="return ConfirmSendFax();" Text="重新发送" OnClick="btnSendFax_Click"/>--%>
                                                    <input type="button" class="btn" id="btnReSendFaxOrder" value='重新发送' onclick="AJReSendFaxOrder()" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="right">
                                                酒店回传：
                                            </td>
                                            <td align="left" valign="middle">
                                                <span style="float: left; margin-top: 3px">
                                                    <asp:Label ID="lbHotelReturnFax" runat="server" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn primary" id="btnFindReturn" value='查看' onclick="SetImagePreview('1')" />
                                                    <input type="button" class="btn" id="btnReturnFax" value='查找回传' />
                                                    <%--<asp:Button ID="btnFindReturn" runat="server" CssClass="btn primary" Text="查看" OnClick="btnFindReturn_Click"/>
                                    <asp:Button ID="btnReturnFax" runat="server" CssClass="btn" Text="查找回传" OnClick="btnReturnFax_Click"/>--%>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                取消传真：
                                            </td>
                                            <td align="left" valign="middle">
                                                <span style="float: left; margin-top: 3px">
                                                    <asp:Label ID="lbCancelFax" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lbCancelFaxBK"
                                                        runat="server" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn primary" id="btnFindCancelFax" value='查看' onclick="SetImagePreview('2')" />
                                                    <input type="button" class="btn" id="btnReturnCalFax" value='查找回传' />
                                                    <%--<asp:Button ID="btnFindCancelFax" runat="server" CssClass="btn primary" Text="查看" OnClick="btnFindCancelFax_Click"/>
                                    <asp:Button ID="btnReturnCalFax" runat="server" CssClass="btn" Text="查找回传" OnClick="btnReturnCalFax_Click"/>--%>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                操作订单：
                                            </td>
                                            <td align="left">
                                                <span style="float: left">
                                                    <asp:RadioButton ID="rdbCofIn" GroupName="rdbOdCom" runat="server" Text="确认可入住" onclick="SetRdClick('1')" />
                                                    <asp:RadioButton ID="rdbCofCal" GroupName="rdbOdCom" runat="server" Text="取消订单" onclick="SetRdClick('0')" />
                                                    <asp:RadioButton ID="rdbCofRe" GroupName="rdbOdCom" runat="server" Checked="true"
                                                        Text="备注" onclick="SetRdClick('2')" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <asp:CheckBox ID="chkFollowUp" runat="server" Text="需跟进" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trComfIn">
                                            <td align="right">
                                                确认订单号：
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCofNum" runat="server" Width="170px" Height="20px" />
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trCanlReson">
                                            <td align="right">
                                                取消原因：
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddpCanelReson" CssClass="noborder_inactive" runat="server"
                                                    Width="180px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                操作备注：
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtBOOK_REMARK" runat="server" TextMode="MultiLine" Width="97%"
                                                    Height="90px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="height: 32px">
                                                <%--<asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>--%>
                                                <input type="button" class="btn primary" id="btnSet" value='确认修改' style="width: 75px"
                                                    onclick="BtnLoadStyle();AJSaveOrderList()" />
                                                <%--<asp:Button ID="btnSet" runat="server" CssClass="btn primary" Width="75px" Text="确认修改" OnClientClick="BtnLoadStyle();ChkOrderList();" OnClick="btnSet_Click" />--%>&nbsp;&nbsp;
                                                <input type="button" class="btn primary" id="btnPrint" value='打印订单' style="width: 75px"
                                                    onclick="PopupPrintArea()" />&nbsp;&nbsp;
                                                <%--<asp:Button ID="btnPrint" runat="server" CssClass="btn primary" Width="75px" Text="打印订单"/>&nbsp;--%>
                                                <input type="button" class="btn" id="btnUnlockOrder" value='强制解锁' style="width: 75px"
                                                    onclick="BtnLoadStyle();AJUnlockOrder()" />&nbsp;&nbsp;
                                                <%--<asp:Button ID="btnUnlock" runat="server" CssClass="btn" Width="75px" Text="强制解锁" OnClientClick="BtnLoadStyle();" OnClick="btnUnlock_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                <input type="button" class="btn" id="btnIssue" value='问题单' style="width: 75px" onclick="OpenIssuePage()" />
                                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="border: 1px #d5d5d5 solid; padding: 1px; margin: -1px 0px 0px 7px; width: 98.3%;
                                        background-color: #E9EBF2; height: 60px; text-align: center; vertical-align: middle;
                                        overflow-y: auto; display: none;" id="dvMsg">
                                        <div style="float: left; margin-left: 100px; margin-top: 5px" runat="server" id="dvImg">
                                            <img src="../../Styles/images/suc.png" alt="" runat="server" id="imgAlert" />
                                        </div>
                                        <div id="dvErrorInfo" style="float: left; margin-top: 8px" runat="server">
                                        </div>
                                        <div style="float: left; width: 100%; text-align: center; margin-top: 3px">
                                            <asp:Button ID="btnNextOd" runat="server" CssClass="btn primary" Width="90px" Text="下一张订单"
                                                OnClientClick="BtnLoadStyle();" OnClick="btnNextOd_Click" />
                                        </div>
                                    </div>
                                    <table style="border: 0px; padding: 0px; margin: 2px 0px 0px 5px; width: 100%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left">
                                                    订单操作历史
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="lbMemoHis" runat="server" style="width: 101%; height: 144px; overflow-y: auto;
                                        margin-top: -2px">
                                    </div>
                                    <br />
                                </td>
                                <td style="width: 28%; display: none;" id="tdOTDetail" valign="top" align="right">
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 7px; width: 100.5%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left">
                                                    本酒店其他订单
                                                </div>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbOtherHOrder" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 9px; width: 99.2%; height: 300px">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="center" valign="top" width="100%">
                                                <div style="width: 100%; height: 290px; overflow-y: auto;" id="dvOtherHOrder">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 7px; width: 100.5%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                本客人其他订单
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbOtherCOrder" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 9px; width: 99.2%; height: 208px">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="center" valign="top" width="100%">
                                                <div style="width: 100%; height: 198px; overflow-y: auto;" id="dvOtherCOrder">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="display: none">
                    <asp:Button ID="btnSetUnlock" runat="server" CssClass="btn primary" Width="75px"
                        Text="退出解锁" OnClick="btnSetUnlock_Click" />
                </div>
                <div id="popupDiv3" class="popupDiv2">
                    <div style="width: 99%; height: 99%; margin: 0,0,0,0">
                        <table width="100%">
                            <tr>
                                <td style="width: 60%" id="tdODDetail2" valign="top" align="right">
                                    <div style="float: right; margin-right: -463px; margin-top: -18px" id="dvCLoseImg2">
                                        <img src="../../Styles/images/close.png" style="cursor: pointer" alt="关闭" title="关闭"
                                            onclick="invokeCloseViewList()" />
                                    </div>
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 3px; width: 100.2%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                订单详情
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbDetail2" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 5px; width: 99.4%; height: 100%">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right" width="20%">
                                                酒店名称：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbHOTEL_NAME2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right">
                                                酒店联系人：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbHotel_ex2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right">
                                                入住人：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbGUEST_NAMES2" runat="server" />
                                                &nbsp;&nbsp;电话：
                                                <asp:Label ID="lbCONTACT_TEL2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                入住天数：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbOrderDays2" runat="server" />晚&nbsp;&nbsp;（<asp:Label ID="lbIN_DATE2"
                                                    runat="server" />
                                                至
                                                <asp:Label ID="lbOUT_DATE2" runat="server" />） 到店时间：<asp:Label ID="lbARRIVE_TIME2"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订房型：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbROOM_TYPE_NAME2" runat="server" />&nbsp;&nbsp;<asp:Label ID="lbBOOK_ROOM_NUM2"
                                                    runat="server" />间
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                总价/支付：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbTotalPrice2" runat="server" />&nbsp;&nbsp;<asp:Label ID="lbPRICE_CODE2"
                                                    runat="server" />&nbsp;&nbsp;<asp:Label ID="lbPAY_STATUS2" runat="server" /><span id="PayMethod2" runat="server" style="color:Red" ></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                早餐宽带：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbBreakNet2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                订单说明：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbBOOK_REMARK2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订人电话：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbLOGIN_MOBILE2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                优惠券：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbTICKET_PAGENM2" runat="server" />
                                                &nbsp;&nbsp;<asp:Label ID="lbTICKET_AMOUNT2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                担保信息：
                                            </td>
                                            <td align="left">
                                                <%--<asp:Label ID="lbRESV_GUA_NM2" runat="server" />&nbsp;&nbsp;/&nbsp;&nbsp;--%>
                                                <asp:Label ID="lbIS_GUA2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订时间：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbCREATE_TIME2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订渠道：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbORDER_CHANNEL2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                酒店供应商：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbVendorNM2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                酒店销售：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblSalesMG2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                酒店备注：
                                            </td>
                                            <td align="left">
                                                <span style="float: left; width: 99%" id="Span1">
                                                    <asp:TextBox ID="txtExRemark2" runat="server" TextMode="MultiLine" Width="97%" Height="40px" />
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 40%;" id="tdFXDetail2" valign="top" align="left">
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 5px; width: 100%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left">
                                                    订单操作（<asp:Label ID="lbActionUser2" runat="server" />正在操作）
                                                </div>
                                                <div style="float: right; margin-right: 5px">
                                                    订单号：<asp:Label ID="lbORDER_NUM2" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbControl2" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 7px; width: 99.1%">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="right" style="width: 23%">
                                                订单状态：
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbBOOK_STATUS2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                预订传真：
                                            </td>
                                            <td align="left" valign="middle">
                                                <span style="float: left; margin-top: 3px">
                                                    <asp:Label ID="lbFaxStatus2" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lbFaxStatusBK2"
                                                        runat="server" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn primary" id="btnFindSend2" value='查看' onclick="SetImagePreview('1')" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="right">
                                                酒店回传：
                                            </td>
                                            <td align="left" valign="middle">
                                                <span style="float: left; margin-top: 3px">
                                                    <asp:Label ID="lbHotelReturnFax2" runat="server" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn primary" id="btnFindReturn2" value='查看' onclick="SetImagePreview('1')" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                取消传真：
                                            </td>
                                            <td align="left" valign="middle">
                                                <span style="float: left; margin-top: 3px">
                                                    <asp:Label ID="lbCancelFax2" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lbCancelFaxBK2"
                                                        runat="server" />
                                                </span><span style="float: right; margin-right: 5px">
                                                    <input type="button" class="btn primary" id="Button1" value='查看' onclick="SetImagePreview('2')" />
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="border: 0px; padding: 0px; margin: 2px 0px 0px 5px; width: 100%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left">
                                                    订单操作历史
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="lbMemoHis2" runat="server" style="width: 101%; height: 288px; overflow-y: auto;
                                        margin-top: -2px">
                                    </div>
                                    <br />
                                </td>
                                <td style="width: 28%; display: none;" id="tdOTDetail2" valign="top" align="right">
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 7px; width: 100.5%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                <div style="float: left">
                                                    本酒店其他订单
                                                </div>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbOtherHOrder2" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 9px; width: 99.2%; height: 300px">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="center" valign="top" width="100%">
                                                <div style="width: 100%; height: 290px; overflow-y: auto;" id="dvOtherHOrder2">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 7px; width: 100.5%">
                                        <tr align="left">
                                            <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                                padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                                本客人其他订单
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tbOtherCOrder2" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                        padding: 1px; margin: -3px 0px 0px 9px; width: 99.2%; height: 208px">
                                        <tr style="background-color: #E9EBF2;">
                                            <td align="center" valign="top" width="100%">
                                                <div style="width: 100%; height: 198px; overflow-y: auto;" id="dvOtherCOrder2">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
