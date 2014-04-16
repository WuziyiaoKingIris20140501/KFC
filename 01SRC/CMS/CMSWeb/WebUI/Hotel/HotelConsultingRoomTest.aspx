<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HotelConsultingRoomTest.aspx.cs" Inherits="WebUI_Hotel_HotelConsultingRoomTest" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <style type="text/css">
        .pcbackground
        {
            display: block;
            width: 100%;
            height: 100%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: #666666;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 2000;
        }
        .pcprogressBar
        {
            border: solid 2px #3A599C;
            background: white url("/images/progressBar_m.gif") no-repeat 10px 10px;
            display: block;
            width: 148px;
            height: 28px;
            position: fixed;
            top: 40%;
            left: 40%;
            margin-left: -74px;
            margin-top: -14px;
            padding: 10px 10px 10px 50px;
            text-align: left;
            line-height: 27px;
            font-weight: bold;
            position: absolute;
            z-index: 2001;
        }
        .bgDiv2
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            right: 0px;
            background-color: #000000;
            filter: alpha(Opacity=80);
            -moz-opacity: 0.5;
            opacity: 0.5;
            z-index: 100;
            background-color: #000000;
            opacity: 0.6;
        }
        .popupDiv2
        {
            width: 800px;
            height: 620px;
            top: 15%;
            left: 18%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            border: solid 2px #ff8300;
            z-index: 1001;
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
        #txtRemark
        {
            height: 56px;
            width: 422px;
        }
        .style1
        {
            width: 34px;
            text-align: right;
        }
        .GView_ItemCSS
        {
            /*奇数行*/ /*background:url(../images/bg-frame0201.gif);*/
            background: white;
            line-height: 30px;
        }
        
        .GView_AlternatingItemCSS
        {
            /*偶数行*/
            background: #f6f6f6;
            border-top: 1px #e6e5e5 solid;
            border-bottom: 1px #e6e5e5 solid;
            line-height: 30px;
            border-bottom-color: #e6e5e5;
            border-top-color: #e6e5e5;
        }
        
        .lblLinkDetails
        {
            font-weight: bold;
            font-size: large;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var parms = new Object();
            parms = GetRequestUrl()
            var hotelID = parms["HotelID"];

            GetAA(hotelID); //临时方法  方法名

            $("#<%=btnDivRenewPlan.ClientID%>").click(function () {
                BtnLoadStyle();
                var hotelID = document.getElementById("<%=HidID.ClientID%>").value;
                var CityID = document.getElementById("<%=HidCityID.ClientID%>").value;
                var HotelNM = document.getElementById("<%=HidName.ClientID%>").value;
                var PriceCode = document.getElementById("<%=HidPriceCode.ClientID%>").value;
                var TwoPrice = $("#<%=lblDivPrice.ClientID%>").text(); // document.getElementById("<%=lblDivPrice.ClientID%>").innerHTML;
                var Status = "";
                var val = $('input:radio[id="MainContent_dropDivStatusOpen"]:checked').val();
                if (val == "开启") {
                    Status = "true";
                }
                else {
                    Status = "false";
                }
                var IsReserve = "";
                var RoomCount = $("#<%=txtDivRoomCount.ClientID%>").val();
                if ($("#MainContent_ckDivReserve")[0].checked == true) {
                    IsReserve = "0";
                } else {
                    IsReserve = "1";
                }
                var RoomName = document.getElementById("<%=HidRoomName.ClientID%>").value;
                var RoomCode = document.getElementById("<%=HidRoomCode.ClientID%>").value;
                var StartDTime = $("#<%=divPlanStartDate.ClientID%>").val();
                var EndDTime = $("#<%=divPlanEndDate.ClientID%>").val();
                var Remark = $("#<%=txtRemark.ClientID%>").val();
                $.ajax({
                    type: "Post",
                    url: "HotelConsultingRoomTest.aspx/RenewPlanBySingleHotel",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'hotelID':'" + hotelID + "','CityID':'" + CityID + "','HotelNM':'" + HotelNM + "','PriceCode':'" + PriceCode + "','TwoPrice':'" + TwoPrice + "','Status':'" + Status + "','RoomCount':'" + RoomCount + "','IsReserve':'" + IsReserve + "','RoomName':'" + RoomName + "','RoomCode':'" + RoomCode + "','StartDTime':'" + StartDTime + "','EndDTime':'" + EndDTime + "','Remark':'" + Remark + "'}",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        var d = jQuery.parseJSON(data.d);
                        if (d.d.message == "success") {
                            invokeCloseDiv();
                            HotelBBSClick('' + hotelID + '', '' + HotelNM + '', '' + document.getElementById("<%=HidLinkMan.ClientID%>").value + '', '' + document.getElementById("<%=HidLinkTel.ClientID%>").value + '', '' + document.getElementById("<%=HidRemark.ClientID%>").value + '');

                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    }
                });
            });
            $("#<%=btnMarkRoom.ClientID%>").click(function () {
                BtnLoadStyle();
                var roomStatus = document.getElementById("<%=HidCloseOrFullByRoom.ClientID%>").value;
                var hotelID = document.getElementById("<%=HidID.ClientID%>").value;
                var hotelNM = document.getElementById("<%=HidName.ClientID%>").value;
                var cityID = document.getElementById("<%=HidCityID.ClientID%>").value;
                var remark = $("#<%=divOperateRoomRemark.ClientID%>").val();
                var StartDate = document.getElementById("<%=planStartDate.ClientID%>").value;
                var EndDate = document.getElementById("<%=planEndDate.ClientID%>").value;
                var dateSE = document.getElementById("<%=HidMarkFullRoom.ClientID%>").value;
                var Lmbar2RoomCode = document.getElementById("<%=HidLmbar2RoomCode.ClientID%>").value;
                var LmbarRoomCode = document.getElementById("<%=HidLmbarRoomCode.ClientID%>").value;
                $.ajax({
                    type: "Post",
                    url: "HotelConsultingRoomTest.aspx/RenewPlanMarkRoom",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'RoomStatus':'" + roomStatus + "','hotelID':'" + hotelID + "','hotelNM':'" + hotelNM + "','cityID':'" + cityID + "','remark':'" + remark + "','StartDate':'" + StartDate + "','EndDate':'" + EndDate + "','dateSE':'" + dateSE + "','Lmbar2RoomCode':'" + Lmbar2RoomCode + "','LmbarRoomCode':'" + LmbarRoomCode + "'}",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        var d = jQuery.parseJSON(data.d);
                        if (d.message == "success") {
                            debugger;
                            invokeCloseRemark();
                            HotelBBSClick('' + hotelID + '', '' + hotelNM + '', '' + document.getElementById("<%=HidLinkMan.ClientID%>").value + '', '' + document.getElementById("<%=HidLinkTel.ClientID%>").value + '', '' + document.getElementById("<%=HidRemark.ClientID%>").value + '');

                            BtnCompleteStyle();
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    }
                });
            });
        });

        function HotelBBSClick(hotelID, hotelName, cityID, linkMan, linkTel, Remark) {
            BtnLoadStyle();
             

            var oldHotelID = document.getElementById("<%=HidOldID.ClientID%>").value;
            if (oldHotelID != hotelID) {
                if (oldHotelID != "" && oldHotelID != hotelID) {
                    var oldColor = document.getElementById("<%=HidOldColor.ClientID%>").value;
                    $("#" + oldHotelID + "").css("backgroundColor", oldColor);

                    document.getElementById("<%=HidOldID.ClientID%>").value = hotelID;
                    document.getElementById("<%=HidOldColor.ClientID%>").value = $("#" + hotelID + "").css("backgroundColor")

                } else {
                    document.getElementById("<%=HidOldID.ClientID%>").value = hotelID;
                    document.getElementById("<%=HidOldColor.ClientID%>").value = $("#" + hotelID + "").css("backgroundColor");
                }
                $("#" + hotelID + "").css("backgroundColor", "#FFCC66");
            }


            $("#<%=DivLastOrNext.ClientID%>").show();
            $("#<%=divMain.ClientID%>").show();

            document.getElementById("<%=HidID.ClientID%>").value = hotelID;
            document.getElementById("<%=HidName.ClientID%>").value = hotelName;
            document.getElementById("<%=HidCityID.ClientID%>").value = cityID;

            document.getElementById("<%=HidLinkMan.ClientID%>").value = linkMan;
            document.getElementById("<%=HidLinkTel.ClientID%>").value = linkTel;
            document.getElementById("<%=HidRemark.ClientID%>").value = linkTel;

            $("#<%=spanHotelInfo.ClientID%>").html("[" + hotelID + "]--" + hotelName);

            $("#<%=HotelEXLinkMan_span.ClientID%>").html(linkMan);
            $("#<%=HotelEXLinkMan_txt.ClientID%>").val(linkMan);

            $("#<%=HotelEXLinkTel_span.ClientID%>").html(linkTel);
            $("#<%=HotelEXLinkTel_txt.ClientID%>").val(linkTel);

            $("#<%=HotelEXLinkRemark_span.ClientID%>").html(Remark);
            $("#<%=HotelEXLinkRemark_txt.ClientID%>").val(Remark);

            document.getElementById("<%=HidLmbar2RoomCode.ClientID%>").value = "";
            document.getElementById("<%=HidLmbarRoomCode.ClientID%>").value = "";

            GetHotelPlansByLMBAR(hotelID);
            BtnCompleteStyle();
        }

    </script>
    <script type="text/javascript">
        //获取LMBAR计划
        function GetHotelPlansByLMBAR(hotelID) {
            var StartDate = document.getElementById("<%=planStartDate.ClientID%>").value;
            var EndDate = document.getElementById("<%=planEndDate.ClientID%>").value;
            $.ajax({
                type: "Post",
                url: "HotelConsultingRoomTest.aspx/GetHotelPlansByLMBAR",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelID + "','startDate':'" + StartDate + "','endDate':'" + EndDate + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
                    GetHotelPlansByLMBAR2(hotelID, d); //LMBAR2计划 -- 酒店 LMBAR计划信息
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    return "";
                }
            });
        }
        //获取LMBAR2计划
        function GetHotelPlansByLMBAR2(hotelID, LMPlans) {
            var StartDate = document.getElementById("<%=planStartDate.ClientID%>").value;
            var EndDate = document.getElementById("<%=planEndDate.ClientID%>").value;
            $.ajax({
                type: "Post",
                url: "HotelConsultingRoomTest.aspx/GetHotelPlansByLMBAR2",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelID + "','startDate':'" + StartDate + "','endDate':'" + EndDate + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
                    GetHotelRoomsByLMBAR(hotelID, LMPlans, d);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    return "";
                }
            });
        }

        //LMBAR房型
        function GetHotelRoomsByLMBAR(hotelID, LMPlans, LM2Plans) {
            $.ajax({
                type: "Post",
                url: "HotelConsultingRoomTest.aspx/GetHotelRoomsByLMBAR",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelID + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
                    //document.getElementById("<%=HidLmbarRoomCode.ClientID%>").value = d;
                    GetHotelRoomsByLMBAR2(hotelID, LMPlans, LM2Plans, d);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    return "";
                }
            });
        }

        //LMBAR2房型
        function GetHotelRoomsByLMBAR2(hotelID, LMPlans, LM2Plans, LMRooms) {
            $.ajax({
                type: "Post",
                url: "HotelConsultingRoomTest.aspx/GetHotelRoomsByLMBAR2",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelID + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
                    //document.getElementById("<%=HidLmbar2RoomCode.ClientID%>").value = d;
                    GetDTime(LMPlans, LM2Plans, LMRooms, d, hotelID);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    return "";
                }
            });
        }

        //获取时间段
        function GetDTime(LMPlans, LM2Plans, LMRooms, LM2Rooms, hotelID) {
            var StartDate = document.getElementById("<%=planStartDate.ClientID%>").value;
            var EndDate = document.getElementById("<%=planEndDate.ClientID%>").value;
            $.ajax({
                type: "Post",
                url: "HotelConsultingRoomTest.aspx/GetDTime",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'startDate':'" + StartDate + "','endDate':'" + EndDate + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
                    Joint(LMPlans, LM2Plans, LMRooms, LM2Rooms, d, hotelID);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    return "";
                }
            });
        }

        //拼装数据
        function Joint(LMPlans, LM2Plans, LMRooms, LM2Rooms, DTime, hotelID) {
            AssemblyDiv(LMRooms, LM2Rooms, LMPlans, LM2Plans, DTime, hotelID);
        }

        //拼装(Lmbar房型 \  Lmbar2房型   \  Lmbar计划   \  Lmbar2计划  \  相差日期数 )
        function AssemblyDiv(HotelRoomListLMBARCode, HotelRoomListLMBAR2Code, HotelPlanListLmbar, HotelPlanListLmbar2, DtTime, hotelID) {
            var sb = '';
            document.getElementById("<%=divMain.ClientID%>").innerHTML = "";
            var sumWidth = 160; //总宽度
            var lmbar2W = "80px";

            var lmbar2TitleWidth; //LMBAR2宽度
            if (HotelRoomListLMBAR2Code == "" || typeof (HotelRoomListLMBAR2Code) == "undefined") {
                sumWidth = accAdd(sumWidth, 80);
                lmbar2TitleWidth = "80px";
            } else {
                var sl = accMul(HotelRoomListLMBAR2Code.length, 80);
                sumWidth = accAdd(sumWidth, sl);
                lmbar2TitleWidth = sl + "px";
            }
            var lmbarTitleWidth; //LMBAR宽度
            if (HotelRoomListLMBARCode == "" || typeof (HotelRoomListLMBARCode) == "undefined") {
                sumWidth = accAdd(sumWidth, 80);
                lmbarTitleWidth = "80px";
            } else {
                var sl2 = accMul(HotelRoomListLMBARCode.length, 80);
                sumWidth = accAdd(sumWidth, sl2);
                lmbarTitleWidth = sl2 + "px";
            }

            sumWidth = sumWidth + "px";

            sb += "<table width=\"" + sumWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
            sb += "<tr align=\"center\">";
            sb += "<td rowspan=\"2\" style=\"width: 80px; border: solid #8D8D8D 1px;\">批量操作</td>";
            sb += "<td rowspan=\"2\" style=\"width: 80px; border: solid #8D8D8D 1px;\">日期" + "\\" + "房型</td>";
            sb += "<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\">LMBAR2</td><td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;\">LMBAR</td>";
            sb += "</tr>";
            sb += "<tr>";

            //循环LMBAR2 房型CODE    最后一个COde   td的style  去掉.
            var RoomListLMABAR2 = 0;
            if (HotelRoomListLMBAR2Code != "" && typeof (HotelRoomListLMBAR2Code) != "undefined") {
                RoomListLMABAR2 = HotelRoomListLMBAR2Code.length;
            }

            if (RoomListLMABAR2 > 0) {
                sb += "<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" style=\" border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb += "<tr align=\"center\">";
                for (i = 0; i < RoomListLMABAR2; i++) {
                    if (RoomListLMABAR2 - 1 == i) {
                        sb += "<td style=\"width:" + lmbar2W + ";\">";
                    }
                    else {
                        sb += "<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">";
                    }
                    sb += "<span>" + HotelRoomListLMBAR2Code[i]["ROOMNM"] + "</span></br><span>" + HotelRoomListLMBAR2Code[i]["ROOMCODE"] + "<span>";
                    sb += "</td>";
                    document.getElementById("<%=HidLmbar2RoomCode.ClientID%>").value += HotelRoomListLMBAR2Code[i]["ROOMCODE"] + "|";
                }
            }
            else {
                sb += "<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb += "<tr align=\"center\">";

                sb += "<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">";
                sb += "</td>";
            }
            sb += "</tr> </table></td>";

            // 循环LMBAR 房型CODE     最后一个Code   td的style  去掉
            var RoomListLMABAR = 0;
            if (HotelRoomListLMBARCode != "" && typeof (HotelRoomListLMBARCode) != "undefined") {
                RoomListLMABAR = HotelRoomListLMBARCode.length;
            }
            if (RoomListLMABAR > 0) {
                sb += "<td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" height=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb += "<tr align=\"center\">";
                for (i = 0; i < RoomListLMABAR; i++) {
                    if (RoomListLMABAR - 1 == i) {
                        sb += "<td style=\"width:" + lmbar2W + ";padding-top:5px;\">";
                    }
                    else {
                        sb += "<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";padding-top:5px;\">";
                    }
                    sb += "<span>" + HotelRoomListLMBARCode[i]["ROOMNM"] + "</span></br><span>" + HotelRoomListLMBARCode[i]["ROOMCODE"] + "<span>";
                    sb += "</td>";
                    document.getElementById("<%=HidLmbarRoomCode.ClientID%>").value += HotelRoomListLMBARCode[i]["ROOMCODE"] + "|";
                }
            }
            else {
                sb += "<td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;border-collapse:collapse;\"><table width=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb += "<tr align=\"center\">";
                sb += "<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">";
                sb += "";
                sb += "</td>";
            }

            sb += "</tr></table></td>";
            sb += "</tr>";

            var IsDayOfWeek = false;
            var day = 0;
            if (DtTime != "" && typeof (DtTime) != "undefined") {
                day = DtTime.length;
            }
            for (i = 0; i < day; i++) {
                sb += "<tr align=\"center\">"; //DtTime
                // 日期
                if (DtTime[i]["IsWeek"] == "true") {
                    IsDayOfWeek = true;
                    sb += "<td style=\"width: 80px; border: solid #8D8D8D 1px;background-color: #CDEBFF;height:40px;\"><input type=\"checkbox\" id=\"chkMarkFullRoom" + i + "\" name=\"chkMarkFullRoom\"  value=\"" + DtTime[i]["time"] + "\"/></td>";
                    sb += "<td style=\"width: 80px; border: solid #8D8D8D 1px;background-color: #CDEBFF;height:40px;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">" + DtTime[i]["time"] + "</td>";
                }
                else {
                    IsDayOfWeek = false;
                    sb += "<td style=\"width: 80px; border: solid #8D8D8D 1px;height:40px;\"><input type=\"checkbox\" id=\"chkMarkFullRoom" + i + "\"  name=\"chkMarkFullRoom\" value=\"" + DtTime[i]["time"] + "\"/></td>";
                    sb += "<td style=\"width: 80px; border: solid #8D8D8D 1px;height:40px;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">" + DtTime[i]["time"] + "</td>";
                }
                sb += "<td width=\"" + lmbar2TitleWidth + "\" > <table width=\"" + lmbar2TitleWidth + "\" style=\"border-collapse: collapse; border: none; \" cellpadding=\"0\" cellspacing=\"0\">";
                sb += "<tr align=\"center\" > ";


                //循环LMBAR2   酒店计划中的房型数量和价格
                var PlanLMBAR2 = 0;
                if (HotelPlanListLmbar2 != "" && typeof (HotelPlanListLmbar2) != "undefined") {
                    PlanLMBAR2 = HotelPlanListLmbar2.length;
                }
                if (PlanLMBAR2 > 0) {
                    var flag = false;
                    if (RoomListLMABAR2 > 0) {
                        for (j = 0; j < RoomListLMABAR2; j++) {
                            flag = false;
                            for (k = 0; k < PlanLMBAR2; k++) {
                                if (HotelRoomListLMBAR2Code[j]["ROOMCODE"] == HotelPlanListLmbar2[k]["ROOMTYPECODE"] && (Date.parse(HotelPlanListLmbar2[k]["EFFECTDATESTRING"].replace(/\-/g, "/")) - Date.parse(DtTime[i]["time"]) == 0)) {
                                    flag = true;
                                    if (HotelPlanListLmbar2[k]["ROOMNUM"] == "0") {
                                        if (HotelPlanListLmbar2[k]["STATUS"] == "false") {
                                            sb += "<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb += "<table width=\"100%\" style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar2[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar2[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar2[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                        else if (HotelPlanListLmbar2[k]["ISROOMFUL"] == "1") {
                                            sb += "<td style=\"background-color:#E96928;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E96928'\">";
                                            sb += "<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar2[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar2[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar2[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                        else {
                                            sb += "<td style=\"background-color:#E6B9B6;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E6B9B6'\">";
                                            sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar2[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar2[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar2[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                    }
                                    else {
                                        if (HotelPlanListLmbar2[k]["STATUS"] == "false") {
                                            sb += "<td style=\"background-color:#999999;border-right: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;border-bottom: solid #8D8D8D 1px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb += "<table width=\"100%\"  style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar2[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar2[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar2[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                        else {
                                            if (!IsDayOfWeek) {
                                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                                sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar2[k]["ROOMNUM"];
                                                if (HotelPlanListLmbar2[k]["ISRESERVE"] == "0") {
                                                    sb += "<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else {
                                                    sb += "</td></tr>";
                                                }
                                                sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar2[k]["TWOPRICE"] + "</td></tr></table>";
                                                sb += "</td>";
                                            }
                                            else {
                                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                                sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\"  height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar2[k]["ROOMNUM"];
                                                if (HotelPlanListLmbar2[k]["ISRESERVE"] == "0") {
                                                    sb += "<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else {
                                                    sb += "</td></tr>";
                                                }
                                                sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar2[k]["TWOPRICE"] + "</td></tr></table>";
                                                sb += "</td>";
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            if (!flag) {
                                if (!IsDayOfWeek) {
                                    sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                    sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb += "</td>";
                                }
                                else {
                                    sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                    sb += "<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb += "</td>";
                                }
                            }
                        }
                    }
                    else {
                        if (!IsDayOfWeek) {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                        else {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb += "<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                    }
                }
                else {
                    if (!IsDayOfWeek) {
                        if (RoomListLMABAR2 > 0) {
                            for (j = 0; j < RoomListLMABAR2; j++) {
                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb += "</td>";
                            }
                        }
                        else {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                    }
                    else {
                        if (RoomListLMABAR2 > 0) {
                            for (j = 0; j < RoomListLMABAR2; j++) {
                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb += "</td>";
                            }
                        }
                        else {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb += "<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                    }
                }

                sb += "</tr>";
                sb += "</table></td>";

                sb += "<td width=\"" + lmbarTitleWidth + "\" ><table width=\"" + lmbarTitleWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb += "<tr align=\"center\">";

                // 循环LMBAR   酒店计划中的房型数量和价格
                var PlanLMBAR = 0;
                if (HotelPlanListLmbar != "" && typeof (HotelPlanListLmbar) != "undefined") {
                    PlanLMBAR = HotelPlanListLmbar.length;
                }
                if (PlanLMBAR > 0) {
                    var flag = false;
                    if (RoomListLMABAR > 0) {
                        for (j = 0; j < RoomListLMABAR; j++) {
                            flag = false;
                            for (k = 0; k < PlanLMBAR; k++) {
                                if (HotelRoomListLMBARCode[j]["ROOMCODE"] == HotelPlanListLmbar[k]["ROOMTYPECODE"] && (Date.parse(HotelPlanListLmbar[k]["EFFECTDATESTRING"].replace(/\-/g, "/")) - Date.parse(DtTime[i]["time"]) == 0)) {
                                    flag = true;
                                    if (HotelPlanListLmbar[k]["ROOMNUM"] == "0") {
                                        if (HotelPlanListLmbar[k]["STATUS"] == "false") {
                                            sb += "<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"  onclick=\"showDiv('" + hotelID + "','LMBAR','" + HotelRoomListLMBARCode[j]["ROOMNM"] + "','" + HotelRoomListLMBARCode[j]["ROOMCODE"] + "','" + HotelPlanListLmbar[k]["TWOPRICE"] + "','" + HotelPlanListLmbar[k]["ROOMNUM"] + "','" + HotelPlanListLmbar[k]["STATUS"] + "','" + HotelPlanListLmbar[k]["ISRESERVE"] + "','" + HotelPlanListLmbar[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                        else if (HotelPlanListLmbar[k]["ISROOMFUL"] == "1") {
                                            sb += "<td style=\"background-color: #E96928;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E96928'\">";
                                            sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR','" + HotelRoomListLMBARCode[j]["ROOMNM"] + "','" + HotelRoomListLMBARCode[j]["ROOMCODE"] + "','" + HotelPlanListLmbar[k]["TWOPRICE"] + "','" + HotelPlanListLmbar[k]["ROOMNUM"] + "','" + HotelPlanListLmbar[k]["STATUS"] + "','" + HotelPlanListLmbar[k]["ISRESERVE"] + "','" + HotelPlanListLmbar[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                        else {
                                            sb += "<td style=\"background-color: #E6B9B6;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E6B9B6'\">";
                                            sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR','" + HotelRoomListLMBARCode[j]["ROOMNM"] + "','" + HotelRoomListLMBARCode[j]["ROOMCODE"] + "','" + HotelPlanListLmbar[k]["TWOPRICE"] + "','" + HotelPlanListLmbar[k]["ROOMNUM"] + "','" + HotelPlanListLmbar[k]["STATUS"] + "','" + HotelPlanListLmbar[k]["ISRESERVE"] + "','" + HotelPlanListLmbar[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                    }
                                    else {
                                        if (HotelPlanListLmbar[k]["STATUS"] == "false") {
                                            sb += "<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb += "<table width=\"100%\"  cellpadding=\"0\" cellspacing=\"0\" style=\"border: none;\"  height=\"40px;\"  onclick=\"showDiv('" + hotelID + "','LMBAR','" + HotelRoomListLMBARCode[j]["ROOMNM"] + "','" + HotelRoomListLMBARCode[j]["ROOMCODE"] + "','" + HotelPlanListLmbar[k]["TWOPRICE"] + "','" + HotelPlanListLmbar[k]["ROOMNUM"] + "','" + HotelPlanListLmbar[k]["STATUS"] + "','" + HotelPlanListLmbar[k]["ISRESERVE"] + "','" + HotelPlanListLmbar[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar[k]["ROOMNUM"];
                                            if (HotelPlanListLmbar[k]["ISRESERVE"] == "0") {
                                                sb += "<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else {
                                                sb += "</td></tr>";
                                            }
                                            sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb += "</td>";
                                        }
                                        else {
                                            if (!IsDayOfWeek) {
                                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                                sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR','" + HotelRoomListLMBARCode[j]["ROOMNM"] + "','" + HotelRoomListLMBARCode[j]["ROOMCODE"] + "','" + HotelPlanListLmbar[k]["TWOPRICE"] + "','" + HotelPlanListLmbar[k]["ROOMNUM"] + "','" + HotelPlanListLmbar[k]["STATUS"] + "','" + HotelPlanListLmbar[k]["ISRESERVE"] + "','" + HotelPlanListLmbar[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar[k]["ROOMNUM"];
                                                if (HotelPlanListLmbar[k]["ISRESERVE"] == "0") {
                                                    sb += "<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else {
                                                    sb += "</td></tr>";
                                                }
                                                sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar[k]["TWOPRICE"] + "</td></tr></table>";
                                                sb += "</td>";
                                            }
                                            else {
                                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                                sb += "<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + hotelID + "','LMBAR','" + HotelRoomListLMBARCode[j]["ROOMNM"] + "','" + HotelRoomListLMBARCode[j]["ROOMCODE"] + "','" + HotelPlanListLmbar[k]["TWOPRICE"] + "','" + HotelPlanListLmbar[k]["ROOMNUM"] + "','" + HotelPlanListLmbar[k]["STATUS"] + "','" + HotelPlanListLmbar[k]["ISRESERVE"] + "','" + HotelPlanListLmbar[k]["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + HotelPlanListLmbar[k]["ROOMNUM"];
                                                if (HotelPlanListLmbar[k]["ISRESERVE"] == "0") {
                                                    sb += "<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else {
                                                    sb += "</td></tr>";
                                                }
                                                sb += "<tr align=\"center\"><td>￥" + HotelPlanListLmbar[k]["TWOPRICE"] + "</td></tr></table>";
                                                sb += "</td>";
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            if (!flag) {
                                if (!IsDayOfWeek) {
                                    sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                    sb += "<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb += "</td>";
                                }
                                else {
                                    sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                    sb += "<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb += "</td>";
                                }
                            }
                        }
                    }
                    else {
                        if (!IsDayOfWeek) {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb += "<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                        else {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb += "<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                    }
                }
                else {
                    if (!IsDayOfWeek) {
                        if (RoomListLMABAR > 0) {
                            for (j = 0; j < RoomListLMABAR; j++) {
                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                sb += "<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb += "</td>";
                            }
                        }
                        else {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb += "<table  width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                    }
                    else {
                        if (RoomListLMABAR > 0) {
                            for (j = 0; j < RoomListLMABAR; j++) {
                                sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                sb += "<table  width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb += "</td>";
                            }
                        }
                        else {
                            sb += "<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb += "<table  width=\"100%\"  style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\"  height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb += "</td>";
                        }
                    }
                }

                sb += "</tr>";
                sb += "</table></td>";

                sb += "</tr>";
            }

            sb += "</table>";

            $("#<%=divMain.ClientID%>").html(sb);

        }
    </script>
    <script type="text/javascript">
        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        //获取相差日期数
        function getDays(strDateStart, strDateEnd) {
            var strSeparator = "-"; //日期分隔符
            var oDate1;
            var oDate2;
            var iDays;
            oDate1 = strDateStart.split(strSeparator);
            oDate2 = strDateEnd.split(strSeparator);
            var strDateS = new Date(oDate1[0], oDate1[1] - 1, oDate1[2]);
            var strDateE = new Date(oDate2[0], oDate2[1] - 1, oDate2[2]);
            iDays = parseInt(Math.abs(strDateS - strDateE) / 1000 / 60 / 60 / 24)//把相差的毫秒数转换为天数 
            return iDays;
        }

        //计算宽度值  乘法
        function accMul(arg1, arg2) {
            var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
            try { m += s1.split(".")[1].length } catch (e) { }
            try { m += s2.split(".")[1].length } catch (e) { }
            return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
        }
        //计算宽度值  加法
        function accAdd(arg1, arg2) {
            var r1, r2, m;
            try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
            try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
            m = Math.pow(10, Math.max(r1, r2))
            return (arg1 * m + arg2 * m) / m
        }
        //给Number类型增加一个add方法，调用起来更加方便。 
        Number.prototype.add = function (arg) {
            return accAdd(arg, this);
        }

        //日期相差天数
        function daysBetween() {
            var DateOne = document.getElementById("<%=planStartDate.ClientID%>").value;
            var DateTwo = document.getElementById("<%=planEndDate.ClientID%>").value;
            var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('-'));
            var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('-') + 1);
            var OneYear = DateOne.substring(0, DateOne.indexOf('-'));

            var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('-'));
            var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('-') + 1);
            var TwoYear = DateTwo.substring(0, DateTwo.indexOf('-'));

            var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 86400000);
            return Math.abs(cha);
        }

        var jmz = {};
        jmz.GetLength = function (str) {
            ///<summary>获得字符串实际长度，中文2，英文1</summary>
            ///<param name="str">要获得长度的字符串</param>
            var realLength = 0, len = str.length, charCode = -1;
            for (var i = 0; i < len; i++) {
                charCode = str.charCodeAt(i);
                if (charCode >= 0 && charCode <= 128) realLength += 1;
                else realLength += 2;
            }
            return realLength;
        };
    </script>
    <script type="text/javascript">
        //showDiv('" + hotelID + "','LMBAR2','" + HotelRoomListLMBAR2Code[j]["ROOMNM"] + "','" + HotelRoomListLMBAR2Code[j]["ROOMCODE"] + "','" + HotelPlanListLmbar2[k]["TWOPRICE"] + "','" + HotelPlanListLmbar2[k]["ROOMNUM"] + "','" + HotelPlanListLmbar2[k]["STATUS"] + "','" + HotelPlanListLmbar2[k]["ISRESERVE"] + "','" + HotelPlanListLmbar2[k]["EFFECTDATESTRING"] + "')
        function showDiv(hotelID, priceCode, roomame, roomCode, twoPrice, roomnum, status, isreserve, effectDate) {
            invokeOpenDiv();
            document.getElementById("<%=lblDivHotelName.ClientID%>").innerHTML = document.getElementById("<%=HidName.ClientID%>").value; //酒店名称
            document.getElementById("<%=lblDivRoomType.ClientID%>").innerHTML = roomame; //房型
            document.getElementById("<%=DivlblLinkDetails.ClientID%>").innerHTML = document.getElementById("<%=HidLinkMan.ClientID%>").value + document.getElementById("<%=HidLinkTel.ClientID%>").value;  //酒店联系人 电话 
            document.getElementById("<%=lblDivPrice.ClientID%>").innerHTML = twoPrice; //价格

            if (status == "true")//状态
            {
                showRoomDiv();
                document.getElementById("<%=dropDivStatusOpen.ClientID%>").checked = true;
                document.getElementById("<%=dropDivStatusClose.ClientID%>").checked = false;
            }
            else {
                closeRoomDiv();
                document.getElementById("<%=dropDivStatusOpen.ClientID%>").checked = false;
                document.getElementById("<%=dropDivStatusClose.ClientID%>").checked = true;
            }
            document.getElementById("<%=txtDivRoomCount.ClientID%>").value = roomnum; //房量 

            if (isreserve == "0")//是否是保留房
            {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = true;
            }
            else {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = false;
            }
            //批量开始 结束时间
            document.getElementById("<%=divPlanStartDate.ClientID%>").value = effectDate;
            document.getElementById("<%=divPlanEndDate.ClientID%>").value = effectDate;

            document.getElementById("<%=HiddenEffectDate.ClientID%>").value = effectDate;
            //备注
            document.getElementById("<%=LmbarRemarkHistory.ClientID%>").innerHTML = "";

            document.getElementById("<%=HidPriceCode.ClientID%>").value = priceCode;
            document.getElementById("<%=HidRoomName.ClientID%>").value = roomame;
            document.getElementById("<%=HidRoomCode.ClientID%>").value = roomCode;

            $.ajax({
                async: true,
                contentType: "application/json",
                url: "HotelConsultingRoomTest.aspx/GetHistoryRemarkByJson",
                type: "POST",
                dataType: "json",
                data: "{CityID:'" + document.getElementById("<%=HidCityID.ClientID%>").value + "',HotelID:'" + document.getElementById("<%=HidID.ClientID%>").value + "',PriceCode:'" + document.getElementById("<%=HidPriceCode.ClientID%>").value + "',RoomCode:'" + document.getElementById("<%=HidRoomCode.ClientID%>").value + "',PlanDTime:'" + document.getElementById("<%=HiddenEffectDate.ClientID%>").value + "'}",
                success: function (data) {
                    debugger;
                    var output = "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\"><tr style=\"line-height:30px;\"><td style=\"width:135px;text-align:center\">操作时间</td><td style=\"width:100px;text-align:center\">操作人</td><td style=\"width:70px;text-align:center\">价格</td><td style=\"width:50px;text-align:center\">状态</td><td style=\"width:50px;text-align:center\">房量</td><td style=\"width:50px;text-align:center\">保留房</td><td>备注</td></tr>";
                    var d = jQuery.parseJSON(data.d);
                    if (d.d != "[]") {
                        $.each(d, function (i) {
                            output += "<tr style=\"line-height:30px;\"><td style=\"width:135px;text-align:center\">" + d[i].Create_Time + "</td><td style=\"width:100px;text-align:center\">" + d[i].Create_User + "</td><td style=\"width:70px;text-align:center\">" + d[i].TwoPrice + "</td><td style=\"width:50px;text-align:center\">" + d[i].Status + "</td><td style=\"width:50px;text-align:center\">" + d[i].RoomNum + "</td><td style=\"width:50px;text-align:center\">" + d[i].IsReserve + "</td>";
                            if (d[i].Remark != "" && d[i].Remark.length > 20) {
                                output += "<td title=\"" + d[i].Remark + "\">" + d[i].Remark.substring(0, 20) + "..." + "</td></tr>";
                            } else {
                                output += "<td title=\"" + d[i].Remark + "\">" + d[i].Remark + "</td></tr>";
                            }
                        });
                    }
                    output += "</table>";
                    $("#<%=LmbarRemarkHistory.ClientID%>").html(output);
                },
                error: function (json) {

                }
            });
        }

        //显示弹出的层
        function invokeOpenDiv() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeCloseDiv() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function showRoomDiv() {
            $("#MainContent_txtDivRoomCount").removeAttr("disabled", "");
            $("#MainContent_ckDivReserve").removeAttr("disabled", "");
        }

        function closeRoomDiv() {
            document.getElementById("MainContent_txtDivRoomCount").disabled = "true";
            document.getElementById("MainContent_ckDivReserve").disabled = "true";
        } 
    </script>
    <script type="text/javascript">
        function MarkFullRoom(obj) {
            debugger;
            //批量操作  关房  开房 
            var s = '';
            $('input[name="chkMarkFullRoom"]:checked').each(function () {
                s += $(this).val() + ',';
            });
            if (s == '') {
                alert("你还没有选择任何内容！");
                return false;
            } else {
                s = s.substring(0, s.length - 1);
                var strs = new Array();
                strs = s.split(",");

                var myTime = new Date();
                myTime = myTime.getHours(); //当前时分秒 
                if (myTime < 4) {
                    //超过凌晨4点  正常逻辑
                    var sDate = new Date();

                    var eDate = new Date(document.getElementById("<%=divPlanStartDate.ClientID%>").value.replace(/\-/g, "\/"));
                    sDate.setDate(sDate.getDate() - 1);
                    //sDate = sDate.toLocaleDateString(); //当前年月日 
                    sDate = sDate.getFullYear() + "/" + accAdd(sDate.getMonth(), 1) + "/" + sDate.getDate(); //当前年月日
                    for (i = 0; i < strs.length; i++) {
                        var eDate = new Date(strs[i].replace(/\-/g, "\/"));
                        eDate = eDate.getFullYear() + "/" + accAdd(eDate.getMonth(), 1) + "/" + eDate.getDate();
                        if (eDate < sDate) {
                            alert("开始时间必须大于等于当前时间!");
                            return false;
                        }
                    }
                } else {
                    var sDate = new Date();
                    sDate = sDate.getFullYear() + "/" + accAdd(sDate.getMonth(), 1) + "/" + sDate.getDate(); //当前年月日

                    var eDate = new Date(document.getElementById("<%=divPlanStartDate.ClientID%>").value.replace(/\-/g, "\/"));
                    for (i = 0; i < strs.length; i++) {
                        var eDate = new Date(strs[i].replace(/\-/g, "\/"));
                        eDate = eDate.getFullYear() + "/" + accAdd(eDate.getMonth(), 1) + "/" + eDate.getDate();
                        if (eDate < sDate) {
                            alert("开始时间必须大于等于当前时间!");
                            return false;
                        }
                    }
                }

                document.getElementById("<%=HidMarkFullRoom.ClientID%>").value = s;
                document.getElementById("<%=HidCloseOrFullByRoom.ClientID%>").value = obj;
                invokeOpenRemark();
            }
            return true;
        }

        //显示弹出的层  AlertRemark  DivAlertRemark  DivAlertRemarkMain
        function invokeOpenRemark() {
            document.getElementById("DivAlertRemark").style.display = "block";
            //背景
            var bgObj = document.getElementById("DivAlertRemarkMain");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层  AlertRemark
        function invokeCloseRemark() {
            document.getElementById("DivAlertRemark").style.display = "none";
            document.getElementById("DivAlertRemarkMain").style.display = "none";
            BtnCompleteStyle();
        }

    </script>
    <script type="text/javascript">
        function SetSalesRoom(obj) {
            //将当前登录人默认为当前房控人员
            document.getElementById("wcthvpInventoryControl").value = obj;
        }
        function btnAlert() {
            alert("1.当天无计划的酒店，全部过滤掉；\n2.当选择房控人员时，过滤房控人员下面所有当天计划全部被关闭，并且关闭人全部是销售人员的酒店；\n3.当天下线的酒店，全部过滤掉；\n4.过滤所有非自签酒店；\n5.过滤HUBS1签约的锦江之星；\n6.过滤永不询房酒店");
        }
        function GetRequestUrl() {
            var url = location.search; //获取url中"?"符后的字串 
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }

        function GetAA(hotelid) {
            $.ajax({
                type: "Post",
                url: "HotelConsultingRoomTest.aspx/GetHotel",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelid + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var linkman = " ";
                    var linktel = " ";
                    var remark = " ";
                    var d = jQuery.parseJSON(data.d);
                    debugger;
                    $.each(d, function (i) {
                        if (d[i].LINKMAN1 != "" && d[i].LINKMAN1.indexOf('|') >= 0) {
                            linkman = d[i].LINKMAN1.split('|')[0];
                        } else {
                            linkman = d[i].LINKMAN1;
                        }
                        if (d[i].LINKTEL1 != "" && d[i].LINKTEL1.indexOf('|') >= 0) {
                            linktel = d[i].LINKTEL1.split('|')[0];
                        }
                        else {
                            linktel = d[i].LINKTEL1;
                        }
                        remark = d[i].REMARK1.replace(/\n|\r|(\r\n)|(\u0085)|(\u2028)|(\u2029)/g, "");

                        document.getElementById("<%=HidID.ClientID%>").value = hotelid;
                        document.getElementById("<%=HidCityID.ClientID%>").value = d[i].CITYID;
                        document.getElementById("<%=HidName.ClientID%>").value = d[i].PROP_NAME_ZH;


                        HotelBBSClick('' + hotelid + '', '' + d[i].PROP_NAME_ZH + '', '' + d[i].CITYID + '', '' + linkman + '', '' + linktel + '', '' + remark + '');

                        BtnCompleteStyle();
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                }
            });
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <table width="100%" style="height: 600px;">
        <tr>
            <td style="width: 100%;">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="frame01" style="width: 100%; height: 600px; margin-left: -5px;">
                                <ul>
                                    <li class="title"><span id="spanHotelInfo" runat="server"></span><span style="float: right;">
                                        <span style="text-align: right; color: White">█ </span>无计划&nbsp;&nbsp;&nbsp;<span
                                            style="text-align: right; color: #CDEBFF;">█ </span>周末&nbsp;&nbsp;&nbsp;<span style="text-align: right;
                                                color: #E6B9B6">█ </span>满房&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: #E96928">█
                                                </span>CC操作满房&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: #999999">█
                                        </span>计划关闭&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: Red">* </span>
                                        保留房&nbsp;&nbsp;&nbsp;</span></li>
                                </ul>
                                <ul>
                                    <li>
                                        <div style="height: 60px; width: 100%; margin-left: -10px;">
                                            <div style="float: right; vertical-align: super; width: 100%; display: none;" id="DivLastOrNext"
                                                runat="server">
                                                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 100%; border: 1px solid #8D8D8D; background-color: #E9E9E9"
                                                            cellpadding="0" cellspacing="0">
                                                            <tr style="height: 34px; margin: 0px 5px 5px 0px;">
                                                                <td style="border-bottom: 1px solid #8D8D8D; width: 45%;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;酒店联系人：
                                                                            </td>
                                                                            <td>
                                                                                <div id="SPANHotelEXLinkMan" runat="server">
                                                                                    <asp:Label ID="HotelEXLinkMan_span" runat="server" Text=""></asp:Label></div>
                                                                                <div id="TXTHotelEXLinkMan" runat="server" style="display: none; float: left">
                                                                                    <asp:TextBox ID="HotelEXLinkMan_txt" runat="server"></asp:TextBox></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="border-bottom: 1px solid #8D8D8D; width: 45%;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                电话：
                                                                            </td>
                                                                            <td>
                                                                                <div id="SPANHotelEXLinkTel" runat="server">
                                                                                    <asp:Label ID="HotelEXLinkTel_span" runat="server" Text=""></asp:Label></div>
                                                                                <div id="TXTHotelEXLinkTel" runat="server" style="display: none; float: left">
                                                                                    <asp:TextBox ID="HotelEXLinkTel_txt" runat="server"></asp:TextBox></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="border-bottom: 1px solid #8D8D8D; width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="border: 1px solid #8D8D8D">
                                                                <td colspan="2">
                                                                    <div id="SPANHotelEXLinkRemark" runat="server" style="text-align: left; vertical-align: inherit;">
                                                                        <div id="HotelEXLinkRemark_span" runat="server" style="text-align: left; vertical-align: inherit;
                                                                            min-height: 68px; max-width: 98%; word-break: break-all; word-wrap: break-word;
                                                                            margin-left: 15px;">
                                                                        </div>
                                                                    </div>
                                                                    <div id="TXTotelEXLinkRemark" runat="server" style="display: none">
                                                                        <textarea id="HotelEXLinkRemark_txt" runat="server" cols="115" style="min-height: 68px;
                                                                            margin-left: 15px; word-break: break-all; word-wrap: break-word;"></textarea>
                                                                    </div>
                                                                </td>
                                                                <%-- <td>
                                                                    <input type="button" id="btnClearLock" runat="server" class="btn primary" value="&nbsp;&nbsp;修改&nbsp;&nbsp;"
                                                                        style="margin-bottom: 3px;" onclick="ClearLock()" />
                                                                    <input type="button" id="btnEditRemark" runat="server" class="btn primary" value="&nbsp;&nbsp;保存&nbsp;&nbsp;"
                                                                        onclick="EditRemark()" style="margin-bottom: 3px; display: none;" />
                                                                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                                        <ContentTemplate>
                                                                            <input type="button" id="btnAlertLink" runat="server" class="btn" value="LM联系人" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>--%>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div style="margin: 5px 5px 5px 5px;">
                                                    <input type="button" id="Button7" runat="server" class="btn primary" onclick="return MarkFullRoom('ExecuteRoom')"
                                                        value="批量执行" />&nbsp;&nbsp;&nbsp;
                                                    <input type="button" id="btnMarkCloseRoom" runat="server" class="btn primary" style="color: #FF6666"
                                                        onclick="return MarkFullRoom('CloseRoom')" value="批量关房" />&nbsp;&nbsp;&nbsp;
                                                    <input type="button" id="btnMarkOpenRoom" runat="server" class="btn primary" onclick="return MarkFullRoom('OpenRoom')"
                                                        value="批量开房" />&nbsp;&nbsp;&nbsp;
                                                    <input type="button" id="btnMarkFullRoom" runat="server" class="btn primary" onclick="return MarkFullRoom('FullRoom')"
                                                        value="标记满房" visible="false" />&nbsp;&nbsp;&nbsp;
                                                    <%-- <input type="button" id="lastHotel" runat="server" class="btn primary" style="margin-left: 200px;"
                                                        onclick="LastOrNextByHotel('-1')" value="上一个" />&nbsp;&nbsp;&nbsp;
                                                    <input type="button" id="nextHotel" runat="server" class="btn primary" onclick="LastOrNextByHotel('1')"
                                                        value="下一个" />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divMain" style="height: 500px; width: 100%; margin-left: -10px; overflow-x: auto;
                                            overflow-y: auto;" runat="server">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2">
        <div class="frame01">
            <ul>
                <li class="title">酒店计划</li>
            </ul>
            <ul>
                <li style="padding-left: 0px;">
                    <table style="width: 100%;" class="GView_BodyCSS">
                        <tr style="height: 35px; vertical-align: middle;">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                酒店:
                            </td>
                            <td style="width: 190px; border: 1px solid #DCDCDC;">
                                <asp:Label ID="lblDivHotelName" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 390px; border: 1px solid #DCDCDC;" colspan="2">
                                <asp:Label ID="DivlblLinkDetails" CssClass="lblLinkDetails" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                房型:
                            </td>
                            <td style="border: 1px solid #DCDCDC;">
                                <asp:Label ID="lblDivRoomType" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="2">
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                价格:
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="3">
                                <asp:Label ID="lblDivPrice" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                状态:
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="3">
                                <input type="radio" runat="server" id="dropDivStatusOpen" name="dropDivStatus" value="开启"
                                    onclick="showRoomDiv()" />开启&nbsp;&nbsp;&nbsp;<input type="radio" runat="server"
                                        id="dropDivStatusClose" name="dropDivStatus" value="关闭" onclick="closeRoomDiv()" />关闭
                            </td>
                        </tr>
                        <tr style="width: 220px; height: 35px; vertical-align: middle;" align="left">
                            <div id="divRoomCount" runat="server">
                                <td class="style1" style="border: 1px solid #DCDCDC;">
                                    房量:
                                </td>
                                <td style="white-space: nowrap; border: 1px solid #DCDCDC;" colspan="3">
                                    <div id="divckReserve" runat="server">
                                        <asp:TextBox ID="txtDivRoomCount" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <%--<asp:CheckBox ID="ckDivReserve" runat="server" Text="保留房" />--%>
                                        <input type="checkbox" id="ckDivReserve" name="ckDivReserveName" runat="server" value="保留房" />保留房
                                    </div>
                                </td>
                            </div>
                        </tr>
                        <tr style="width: 200px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                批量
                            </td>
                            <td colspan="3" style="border: 1px solid #DCDCDC;">
                                <div id="IsBatchUpdateDiv" runat="server">
                                    <input id="divPlanStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_divPlanEndDate\')||\'2020-10-01\'}'})"
                                        runat="server" />&nbsp;至&nbsp;
                                    <input id="divPlanEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_divPlanStartDate\')}',maxDate:'2020-10-01'})"
                                        runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                备注:
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="3">
                                <textarea id="txtRemark" runat="server" cols="30" style="height: 48px;"></textarea>
                            </td>
                        </tr>
                        <tr style="width: 210px; border: 1px solid #DCDCDC;">
                            <td colspan="4" align="center" style="border: 1px solid #DCDCDC;">
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <input type="button" id="btnDivRenewPlan" runat="server" class="btn primary" value="更新计划" />
                                        <input type="button" value="取消" class="btn" onclick="invokeCloseDiv()" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div id="LmbarRemarkHistory" runat="server" style="width: 100%; height: 223px; overflow-y: auto;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
        </div>
    </div>
    <div id="DivAlertRemarkMain" style="display: none; position: absolute; top: 0px;
        left: 0px; right: 0px; background-color: #000000; filter: alpha(Opacity=80);
        -moz-opacity: 0.5; opacity: 0.5; z-index: 100; background-color: #000000; opacity: 0.6;">
    </div>
    <div id="DivAlertRemark" style="width: 360px; height: 120px; top: 55%; left: 45%;
        position: absolute; padding: 1px; vertical-align: middle; text-align: center;
        border: solid 2px #ff8300; z-index: 100; display: none; background-color: White;">
        <ul>
            <li class="title">操作原因</li>
        </ul>
        <ul>
            <li style="padding-left: 0px;">
                <table>
                    <tr>
                        <td class="style1" style="border-bottom: 1px solid #DCDCDC;">
                            备注:
                        </td>
                        <td style="border-bottom: 1px solid #DCDCDC;" colspan="3">
                            <textarea id="divOperateRoomRemark" runat="server" cols="30" style="height: 48px;"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 210px; border-bottom: 1px solid #DCDCDC;">
                        <td colspan="4" align="center">
                            <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <%-- <asp:Button ID="Button5" runat="server" Text="确定" CssClass="btn primary" OnClientClick="BtnLoadStyle();" />--%>
                                    <input type="button" id="btnMarkRoom" runat="server" value="确定" class="btn primary" />
                                    <input type="button" value="取消" class="btn" onclick="invokeCloseRemark()" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
    <asp:HiddenField ID="HidOldID" runat="server" />
    <asp:HiddenField ID="HidOldColor" runat="server" />
    <asp:HiddenField ID="HidID" runat="server" />
    <asp:HiddenField ID="HidName" runat="server" />
    <asp:HiddenField ID="HidCityID" runat="server" />
    <asp:HiddenField ID="HidLinkMan" runat="server" />
    <asp:HiddenField ID="HidLinkTel" runat="server" />
    <asp:HiddenField ID="HidPriceCode" runat="server" />
    <asp:HiddenField ID="HidRoomName" runat="server" />
    <asp:HiddenField ID="HidRoomCode" runat="server" />
    <asp:HiddenField ID="HidRemark" runat="server" />
    <asp:HiddenField ID="HidMarkFullRoom" runat="server" />
    <asp:HiddenField ID="HidCloseOrFullByRoom" runat="server" />
    <asp:HiddenField ID="HidLmbarRoomCode" runat="server" />
    <asp:HiddenField ID="HidLmbar2RoomCode" runat="server" />
    <asp:HiddenField ID="hidSelectHotel" runat="server" />
    <asp:HiddenField ID="hidSelectCity" runat="server" />
    <asp:HiddenField ID="hidSelectBussiness" runat="server" />
    <asp:HiddenField ID="hidSelectSalesID" runat="server" />
    <asp:HiddenField ID="HidDdlSelectValue" runat="server" />
    <asp:HiddenField ID="HidOnlineTime" runat="server" />
    <asp:HiddenField ID="HiddenEffectDate" runat="server" />
    <asp:HiddenField ID="HidStartDateTime" runat="server" />
    <asp:HiddenField ID="planStartDate" runat="server" />
    <asp:HiddenField ID="planEndDate" runat="server" />
</asp:Content>
