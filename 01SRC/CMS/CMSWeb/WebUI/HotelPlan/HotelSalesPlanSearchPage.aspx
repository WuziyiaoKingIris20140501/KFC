<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HotelSalesPlanSearchPage.aspx.cs" Inherits="WebUI_HotelPlan_HotelSalesPlanSearchPage" %>

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
    </style>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnSelect.ClientID%>").click(function () {
                //放处理代码
                BtnLoadStyle();
                var hotelID = document.getElementById("<%=hidSelectHotel.ClientID%>").value;
                var cityID = document.getElementById("<%=hidSelectCity.ClientID%>").value;
                var areaID = document.getElementById("<%=hidSelectBussiness.ClientID%>").value;
                var SalesID = document.getElementById("<%=hidSelectSalesID.ClientID%>").value;
                var flag = false;
                if (hotelID == "" && cityID == "" && areaID == "" && SalesID == "") {
                    alert("请输入查询条件！");
                    BtnCompleteStyle();
                    return false;
                }
                if (hotelID != "" || cityID != "" || areaID != "") {
                    SalesID = "";
                    if (hotelID != "" && (hotelID.indexOf("[") < 0 || hotelID.indexOf("]") < 0)) {
                        alert("酒店选择不合法，请重新选择！");
                        BtnCompleteStyle();
                        return false;
                    }
                    else if (cityID != "" && (cityID.indexOf("[") < 0 || cityID.indexOf("]") < 0)) {
                        alert("城市选择不合法，请重新选择！");
                        BtnCompleteStyle();
                        return false;
                    }
                    else if (areaID != "" && (areaID.indexOf("[") < 0 || areaID.indexOf("]") < 0)) {
                        alert("商圈选择不合法，请重新选择！");
                        BtnCompleteStyle();
                        return false;
                    }
                }
                else if (SalesID != "" && (SalesID.indexOf("[") < 0 || SalesID.indexOf("]") < 0)) {
                    hotelID = "";
                    cityID = "";
                    areaID = "";
                    alert("销售人员选择不合法，请重新选择！");
                    BtnCompleteStyle();
                    return false;
                } //else {
                $.ajax({
                    type: "Post",
                    url: "HotelSalesPlanSearchPage.aspx/GetHotelList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'hotelID':'" + hotelID + "','cityID':'" + cityID + "','areaID':'" + areaID + "','SalesID':'" + SalesID + "'}",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        var d = jQuery.parseJSON(data.d);
                        var TotalHotels = 0; //总酒店数
                        var linkman = " ";
                        var linktel = " ";
                        var remark = " ";
                        var outHtml = "<table id=\"HotelBBS\" width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\" style=\"cursor: pointer;border-collapse:collapse;border-color:#8D8D8D\">";
                        $.each(d, function (i) {
                            TotalHotels = TotalHotels + 1;
                            if (d[i].EXLINKMAN != "" && d[i].EXLINKMAN.indexOf('|') >= 0) {
                                linkman = d[i].EXLINKMAN.split('|')[0];
                            } else {
                                linkman = d[i].EXLINKMAN;
                            }
                            if (d[i].EXLINKTEL != "" && d[i].EXLINKTEL.indexOf('|') >= 0) {
                                linktel = d[i].EXLINKTEL.split('|')[0];
                            }
                            else {
                                linktel = d[i].EXLINKTEL;
                            }
                            remark = d[i].EXREMARK.replace(/\n|\r|(\r\n)|(\u0085)|(\u2028)|(\u2029)/g, "");
                            var propName = d[i].PROP_NAME_ZH;
                            if (jmz.GetLength(propName) > 34) {
                                propName = propName.substring(0, 16) + "...";
                            }

                            outHtml += "<tr style=\"line-height:30px;\" title=\"" + d[i].PROP_NAME_ZH + "\" id=\"" + d[i].PROP + "\" onclick=\"HotelBBSClick('" + d[i].PROP + "','" + d[i].PROP_NAME_ZH + "','" + d[i].CITYID + "','" + linkman + "','" + linktel + "','" + remark + "')\" ><td width=\"75\" style=\"border-color:#8D8D8D\">" + d[i].NAME_CN + "</td><td style=\"border-color:#8D8D8D\">" + propName + "</br>" + linkman + linktel + "</td></tr>";
                        });
                        outHtml += "</table>";
                        $("#DivMainHotels").html(outHtml);
                        $("#<%=countNum.ClientID%>").html(TotalHotels);

                        $("#<%=DivLastOrNext.ClientID%>").hide();
                        $("#<%=divMain.ClientID%>").hide();
                        $("#<%=spanHotelInfo.ClientID%>").html("");

                        BtnCompleteStyle();
                        return true;
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                        return false;
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        function SetControlValue() {
            //将选择的酒店、城市、商圈、销售人员
            document.getElementById("<%=hidSelectHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidSelectCity.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=hidSelectSalesID.ClientID%>").value = document.getElementById("wcthvpInventoryControl").value;
            document.getElementById("<%=hidSelectBussiness.ClientID%>").value = document.getElementById("wcthvpTagInfo").value;
        }

        function HotelBBSClick(hotelID, hotelName, cityID, linkMan, linkTel, Remark) {
            BtnLoadStyle();

            var oldHotelID = document.getElementById("<%=HidOldID.ClientID%>").value;
            if (oldHotelID != hotelID) {
                if (oldHotelID != "" && oldHotelID != hotelID) {
                    $("#" + oldHotelID + "").css("backgroundColor", "white");
                    document.getElementById("<%=HidOldID.ClientID%>").value = hotelID;
                } else {
                    document.getElementById("<%=HidOldID.ClientID%>").value = hotelID;
                }
                $("#" + hotelID + "").css("backgroundColor", "#FFCC66");
            }


            $("#<%=DivLastOrNext.ClientID%>").show();
            $("#<%=divMain.ClientID%>").show();

            document.getElementById("<%=HidID.ClientID%>").value = hotelID;
            document.getElementById("<%=HidName.ClientID%>").value = hotelName;
            document.getElementById("<%=HidCityID.ClientID%>").value = cityID;

            $("#<%=spanHotelInfo.ClientID%>").html("[" + hotelID + "]--" + hotelName);

            $("#<%=HotelLinkMan.ClientID%>").html(linkMan);
            $("#<%=HotelLinkTel.ClientID%>").html(linkTel);
            $("#<%=HotelEXLinkMan.ClientID%>").html(linkMan);
            $("#<%=HotelEXLinkTel.ClientID%>").html(linkTel);
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
                url: "HotelSalesPlanSearchPage.aspx/GetHotelPlansByLMBAR",
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
                url: "HotelSalesPlanSearchPage.aspx/GetHotelPlansByLMBAR2",
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
                url: "HotelSalesPlanSearchPage.aspx/GetHotelRoomsByLMBAR",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelID + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
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
                url: "HotelSalesPlanSearchPage.aspx/GetHotelRoomsByLMBAR2",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'hotelID':'" + hotelID + "'}",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    var d = jQuery.parseJSON(data.d);
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
                url: "HotelSalesPlanSearchPage.aspx/GetDTime",
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

            var sumWidth = 80; //总宽度
            var minTDWidth = 80; //最小宽度    
            if (DTime == "" || typeof (DTime) == "undefined") {
                sumWidth = accAdd(minTDWidth, 80);
            } else {
                var sl = accMul(DTime.length, 80);
                sumWidth = accAdd(sumWidth, sl);
            }

            sumWidth = sumWidth + "px"; //table宽度
            minTDWidth = minTDWidth + "px"; //列最小宽度

            document.getElementById("<%=divMain.ClientID%>").innerHTML = "";

            sb += "<table width=\"" + sumWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";

            var flag = false;
            for (var i = 0; i < LM2Rooms.length; i++) {
                sb += "<tr align=\"center\">";
                if (flag == false) {
                    flag = true;
                    i = 0;
                    sb += "<td style=\"width:" + minTDWidth + ";\">";

                    sb += "</td>";
                }
                sb += "</tr>";
            }
            sb += "</table>";


            //     <table>
            //        <tr>
            //            <td>LMBAR2</td>
            //            <td>10-13</td>
            //            <td>10-14</td>
            //        </tr>
            //        <tr>
            //            <td><input type="radio" name="radioRoomName" id="radioRoomNameByETD" title="ETD-单人间" /></td>
            //            <td>￥276<br />2 早餐数:2</td>
            //            <td>￥276<br />2 早餐数:2</td>
            //        </tr>
            //    </table>
        }
    </script>
    <script type="text/javascript">
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

        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div class="frame01" style="margin: 8px 14px 5px 14px;">
        <ul>
            <li class="title">酒店销售计划查询</li>
            <li>
                <table width="98%">
                    <tr>
                        <td>
                            选择酒店：
                        </td>
                        <td>
                            <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel"
                                AutoParent="HotelSalesPlanSearchPage.aspx?Type=hotel" />
                        </td>
                        <td>
                            城市：
                        </td>
                        <td>
                            <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city"
                                EnableViewState="false" AutoParent="HotelSalesPlanSearchPage.aspx?Type=city" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            销售:
                        </td>
                        <td>
                            <uc1:WebAutoComplete ID="wcthvpInventoryControl" CTLID="wcthvpInventoryControl" runat="server"
                                EnableViewState="false" AutoType="hvpInventoryControl" AutoParent="HotelSalesPlanSearchPage.aspx?Type=hvpInventoryControl" />
                        </td>
                        <td>
                            商圈：
                        </td>
                        <td>
                            <uc1:WebAutoComplete ID="wcthvpTagInfo" runat="server" CTLID="wcthvpTagInfo" AutoType="hvptaginfo"
                                EnableViewState="false" AutoParent="HotelSalesPlanSearchPage.aspx?Type=hvptaginfo" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            日期：
                        </td>
                        <td>
                            <input id="planStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_planEndDate\')||\'2020-10-01\'}'})"
                                runat="server" />
                            至：
                            <input id="planEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_planStartDate\')}',maxDate:'2020-10-01'})"
                                runat="server" />
                        </td>
                        <td colspan="2" align="center">
                            <input type="button" id="btnSelect" runat="server" class="btn primary" value="选择"
                                onclick="SetControlValue();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <div style="margin-left: 10px;">
                                        <div id="messageContent" runat="server" style="color: red; width: 400px;">
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
    <table width="100%" style="height: 600px;">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <div class="frame01" style="width: 100%; height: 600px; margin-left: 5px;">
                                <ul>
                                    <li class="title">酒店列表 <span style="float: right;"><span id="countNum" runat="server">
                                        0</span></span></li>
                                </ul>
                                <ul>
                                    <div id="DivMainHotels" style="height: 570px; width: 100%; overflow-x: auto; overflow-y: auto;">
                                    </div>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 1%;">
            </td>
            <td style="width: 75%;">
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
                                                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Always" runat="server">
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
                                                                                    <asp:Label ID="HotelLinkMan" runat="server" Text=""></asp:Label></div>
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
                                                                                    <asp:Label ID="HotelLinkTel" runat="server" Text=""></asp:Label></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="border-bottom: 1px solid #8D8D8D; width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 34px; margin: 0px 5px 5px 0px;">
                                                                <td style="border-bottom: 1px solid #8D8D8D; width: 45%;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;酒店联系人：
                                                                            </td>
                                                                            <td>
                                                                                <div id="Div1" runat="server">
                                                                                    <asp:Label ID="HotelEXLinkMan" runat="server" Text=""></asp:Label></div>
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
                                                                                <div id="Div2" runat="server">
                                                                                    <asp:Label ID="HotelEXLinkTel" runat="server" Text=""></asp:Label></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="border-bottom: 1px solid #8D8D8D; width: 10%;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div style="margin: 5px 5px 5px 5px;">
                                                    <input type="button" id="Button7" runat="server" class="btn primary" onclick="return MarkFullRoom('ExecuteRoom')"
                                                        value="新增计划" />&nbsp;&nbsp;&nbsp;
                                                    <input type="button" id="btnMarkCloseRoom" runat="server" class="btn primary" style="color: #FF6666"
                                                        onclick="return MarkFullRoom('CloseRoom')" value="修改计划" />
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
    <asp:HiddenField ID="hidSelectHotel" runat="server" />
    <asp:HiddenField ID="hidSelectCity" runat="server" />
    <asp:HiddenField ID="hidSelectBussiness" runat="server" />
    <asp:HiddenField ID="hidSelectSalesID" runat="server" />
    <asp:HiddenField ID="HidID" runat="server" />
    <asp:HiddenField ID="HidOldID" runat="server" />
    <asp:HiddenField ID="HidName" runat="server" />
    <asp:HiddenField ID="HidCityID" runat="server" />
</asp:Content>
