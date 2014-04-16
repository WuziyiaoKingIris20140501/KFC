<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="HotelSalesPlanManager.aspx.cs"
    Title="酒店管理" Inherits="HotelSalesPlanManager" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
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
            top: 50%;
            left: 50%;
            margin-left: -74px;
            margin-top: -14px;
            padding: 10px 10px 10px 50px;
            text-align: left;
            line-height: 27px;
            font-weight: bold;
            position: absolute;
            z-index: 2001;
        }
        
        .dvHourCss
        {
            float: left;
            border: 1px #999999 solid;
            width: 35px;
            text-align: center;
            vertical-align: middle;
            height: 20px;
            margin-left: -1px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SetHotel(hotel) {
            document.getElementById("wctHotel").value = hotel;
        }

        function BtnSelectHotel() {
            document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
            alert(document.getElementById("wctHotel").value);
        }

        function ClearClickEvent() {
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("<%=hidHotelID.ClientID%>").value = "";
            document.getElementById("<%=ddpGuaid.ClientID%>").value = "PP";
            document.getElementById("<%=ddpCxlid.ClientID%>").value = "PT100";
        }

        function checkRoomStatus(val) {
            if ("true" == val) {
                $("#roomNumTd").show();
                $("#roomNumTr").show();
            } else if ("false" == val) {
                $("#roomNumTd").hide();
                $("#roomNumTr").hide();
            }
        }

        function SetPlanStyle(val) {
            if ("0" == val) {
                document.getElementById("<%=dvDTime.ClientID%>").style.display = "none";
                document.getElementById("<%=dvEachFor.ClientID%>").style.display = "none";
            } else if ("1" == val) {
                document.getElementById("<%=dvDTime.ClientID%>").style.display = "";
                document.getElementById("<%=dvEachFor.ClientID%>").style.display = "none";
            } else if ("2" == val) {
                document.getElementById("<%=dvDTime.ClientID%>").style.display = "none";
                document.getElementById("<%=dvEachFor.ClientID%>").style.display = "";
            }
        }

        function SetGuVal(val) {
            if ("LMBAR" == val) {
                document.getElementById("<%=ddpGuaid.ClientID%>").value = "PP";
                document.getElementById("<%=ddpCxlid.ClientID%>").value = "PT100";
            } else {
                document.getElementById("<%=ddpGuaid.ClientID%>").value = "RH04";
                document.getElementById("<%=ddpCxlid.ClientID%>").value = "NP24";
            }
        }

        function SetContronListVal() {
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
            document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
            var commidList = "";

            var weekList = "";
            if (document.getElementById("<%=chkWeekList.ClientID%>") != null) {
                var objWeekCheck = document.getElementById("<%=chkWeekList.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objWeekCheck.length; i++) {
                    if (document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).checked) {

                        weekList = weekList + document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).value + ',';
                    }
                }
                document.getElementById("<%=hidWeekList.ClientID%>").value = weekList;
            }

            var planWeekList = "";
            if (document.getElementById("<%=chkPlanWeek.ClientID%>") != null) {
                var objPlanWeekCheck = document.getElementById("<%=chkPlanWeek.ClientID%>").getElementsByTagName("input");
                for (var j = 0; j < objPlanWeekCheck.length; j++) {
                    if (document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + j).checked) {

                        planWeekList = planWeekList + document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + j).value + ',';
                    }
                }
                document.getElementById("<%=hidPlanWeekList.ClientID%>").value = planWeekList;
            }


            var effHourList = "";
            var dvID = "MainContent_dvHr";
            for (var k = 0; k <= 23; k++) {
                if (document.getElementById(dvID + k.toString()).style.backgroundColor.toUpperCase() == "GREEN") {
                    effHourList = effHourList + '1';
                }
                else {
                    effHourList = effHourList + '0';
                }
            }
            document.getElementById("<%=hidEffHour.ClientID%>").value = effHourList;
        }

        function SetWeekListVal(arg) {
            if (document.getElementById("<%=chkWeekList.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkWeekList.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (arg == "1") {
                        document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).checked = true;
                    }
                    else {
                        document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).checked = false;
                    }
                }
            }
        }

        function SetPlanWeekListVal(arg) {
            if (document.getElementById("<%=chkPlanWeek.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPlanWeek.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (arg == "1") {
                        document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + i).checked = true;
                    }
                    else {
                        document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + i).checked = false;
                    }
                }
            }
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

        function SetEffHourStyle(val) {
            var arr;
            if ("111100000000000000111111" == val) {
                //18点正常上线
                arr = [0, 1, 2, 3, 18, 19, 20, 21, 22, 23];
            } else if ("111100000000001111111111" == val) {
                //14点正常上线
                arr = [0, 1, 2, 3, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
            } else if ("111100000011111111111111" == val) {
                //14点正常上线
                arr = [0, 1, 2, 3, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
            } else if ("000000000000001111000000" == val) {
                //99元房计划
                arr = [14, 15, 16, 17];
            }
            else if ("99" == val) {
                arr = [];
            }
            else {
                arr = [];
            }

            var dvID = "MainContent_dvHr";

            for (var i = 0; i <= 23; i++) {

                document.getElementById(dvID + i.toString()).style.backgroundColor = "White";
                document.getElementById(dvID + i.toString()).style.color = "Black";
                if ("99" == val) {
                    document.getElementById(dvID + i.toString()).setAttribute("onclick", "SetOnClickEvent('" + dvID + i.toString() + "')");
                }
                else {
                    document.getElementById(dvID + i.toString()).setAttribute("onclick", "");
                }

                for (var j = 0; j < arr.length; j++) {
                    if (i == arr[j]) {
                        document.getElementById(dvID + i.toString()).style.backgroundColor = "Green";
                        document.getElementById(dvID + i.toString()).style.color = "White";
                    }
                }
            }
        }

        function SetOnClickEvent(arg) {
            if (document.getElementById(arg).style.backgroundColor.toUpperCase() == "GREEN") {
                document.getElementById(arg).style.backgroundColor = "White";
                document.getElementById(arg).style.color = "Black";
            }
            else {
                document.getElementById(arg).style.backgroundColor = "Green";
                document.getElementById(arg).style.color = "White";
            }
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">计划起止日期</li>
                        <li>
                            <table>
                                <tr>
                                    <td align="right" style="width: 92px">
                                        <font color="red">*</font>起始日期：
                                    </td>
                                    <td>
                                        <div style="display: none">
                                            <asp:TextBox ID="txtYestoday" runat="server" /></div>
                                        <input id="dpKeepStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_txtYestoday\')}',maxDate:'#F{$dp.$D(\'MainContent_dpKeepEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                    </td>
                                    <td style="width: 12px">
                                    </td>
                                    <td align="right">
                                        <font color="red">*</font>结束日期：
                                    </td>
                                    <td>
                                        <input id="dpKeepEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpKeepStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                            </table>
                            <table>
                                <tr>
                                    <td style="width: 22px">
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="chkWeekList" runat="server" RepeatDirection="Vertical" RepeatColumns="8"
                                            RepeatLayout="Table" CellSpacing="8" />
                                    </td>
                                    <td>
                                        <input type="button" id="btnAll" class="btn primary" value="全选" onclick="SetWeekListVal('1')" />
                                        <input type="button" id="btnUnAll" class="btn" value="反选" onclick="SetWeekListVal('0')" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <font color="red">*</font>开放时间：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpEffHour" runat="server" CssClass="noborder_inactive" Width="120px"
                                            onchange="SetEffHourStyle(this.value);">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div runat="server" id="dvHourList">
                                            <div class="dvHourCss" runat="server" id="dvHr0">
                                                0-1点</div>
                                            <div class="dvHourCss" runat="server" id="dvHr1">
                                                1-2点</div>
                                            <div class="dvHourCss" runat="server" id="dvHr2">
                                                2</div>
                                            <div class="dvHourCss" runat="server" id="dvHr3">
                                                3</div>
                                            <div class="dvHourCss" runat="server" id="dvHr4">
                                                4</div>
                                            <div class="dvHourCss" runat="server" id="dvHr5">
                                                5</div>
                                            <div class="dvHourCss" runat="server" id="dvHr6">
                                                6</div>
                                            <div class="dvHourCss" runat="server" id="dvHr7">
                                                7</div>
                                            <div class="dvHourCss" runat="server" id="dvHr8">
                                                8</div>
                                            <div class="dvHourCss" runat="server" id="dvHr9">
                                                9</div>
                                            <div class="dvHourCss" runat="server" id="dvHr10">
                                                10</div>
                                            <div class="dvHourCss" runat="server" id="dvHr11">
                                                11</div>
                                            <div class="dvHourCss" runat="server" id="dvHr12">
                                                12</div>
                                            <div class="dvHourCss" runat="server" id="dvHr13">
                                                13</div>
                                            <div class="dvHourCss" runat="server" id="dvHr14">
                                                14</div>
                                            <div class="dvHourCss" runat="server" id="dvHr15">
                                                15</div>
                                            <div class="dvHourCss" runat="server" id="dvHr16">
                                                16</div>
                                            <div class="dvHourCss" runat="server" id="dvHr17">
                                                17</div>
                                            <div class="dvHourCss" runat="server" id="dvHr18">
                                                18</div>
                                            <div class="dvHourCss" runat="server" id="dvHr19">
                                                19</div>
                                            <div class="dvHourCss" runat="server" id="dvHr20">
                                                20</div>
                                            <div class="dvHourCss" runat="server" id="dvHr21">
                                                21</div>
                                            <div class="dvHourCss" runat="server" id="dvHr22">
                                                22</div>
                                            <div class="dvHourCss" runat="server" style="width: 50px" id="dvHr23">
                                                23-24点</div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li></li>
                    </ul>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="frame01">
            <ul>
                <li class="title">选择酒店房型</li>
                <li>
                    <table style="line-height: 25px;">
                        <tr>
                            <td align="right" style="width: 80px">
                                <font color="red">*</font>选择酒店：
                            </td>
                            <td>
                                <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel"
                                    AutoParent="HotelSalesPlanManager.aspx?Type=hotel" />
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnSelectHotel" runat="server" CssClass="btn primary" Text="选择" OnClientClick="BtnSelectHotel()"
                                                        OnClick="btnSelectHotel_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <font color="red">*</font>价格代码类型：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddpPriceType" runat="server" CssClass="noborder_inactive" Width="120px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddpPriceType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <font color="red">*</font>货币：CNY-人民币
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td align="right" style="width: 80px">
                                                    保证金制度：
                                                </td>
                                                <td align="left" colspan="4">
                                                    <asp:DropDownList ID="ddpGuaid" runat="server" CssClass="noborder_inactive" Width="450px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    取消制度：
                                                </td>
                                                <td align="left" colspan="4">
                                                    <asp:DropDownList ID="ddpCxlid" runat="server" CssClass="noborder_inactive" Width="450px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td align="right" style="width: 77px">
                                                    <font color="red">*</font>选择房型：
                                                </td>
                                                <td colspan="2">
                                                    <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Always" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddpHotelRoomList" runat="server" CssClass="noborder_inactive"
                                                                Width="170px">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="width: 50px">
                                                </td>
                                                <td>
                                                    计划状态：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddpRoomStatus" Width="77px" runat="server" onchange="checkRoomStatus(this.value);">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 50px">
                                                </td>
                                                <td>
                                                    <span id="roomNumTr">&nbsp;房量设置：</span>
                                                </td>
                                                <td align="right">
                                                    <span id="roomNumTd">
                                                        <asp:TextBox ID="txtRoomCount" runat="server" Width="70px" MaxLength="3" />&nbsp;间&nbsp;
                                                    </span>
                                                </td>
                                                <td style="width: 50px">
                                                </td>
                                                <td>
                                                    保留房：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddpIsReserve" Width="77px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </li>
                <li></li>
            </ul>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">设置价格</li>
                        <li>
                            <table>
                                <tr>
                                    <td style="width: 22px">
                                    </td>
                                    <td style="display: none">
                                        单人价
                                    </td>
                                    <td>
                                        <font color="red">*计划供应商</font>
                                    </td>
                                    <td>
                                        <font color="red">*</font>二人价
                                    </td>
                                    <td>
                                        三人价
                                    </td>
                                    <td>
                                        四人价
                                    </td>
                                    <td>
                                        加床价
                                    </td>
                                    <td>
                                        网络价
                                    </td>
                                    <td>
                                        早餐数量
                                    </td>
                                    <td>
                                        每份早餐价格
                                    </td>
                                    <td>
                                        免费宽带
                                    </td>
                                    <td>
                                        价格浮动设置
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="display: none">
                                        <asp:TextBox ID="txtOnePrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpSup" runat="server" Width="77px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTwoPrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtThreePrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFourPrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBedPrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNetPrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpBreakfastNum" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBreakPrice" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpIsNetwork" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOffsetval" runat="server" Width="70px" MaxLength="6" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpOffsetunit" runat="server" Width="77px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li></li>
                    </ul>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="frame01">
            <ul>
                <li class="title">保存计划划</li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div id="dvDTime" style="display: none" runat="server">
                                <font color="red">*</font>保存时间：<input id="dpPlanDTime" class="Wdate" type="text"
                                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" runat="server" />
                            </div>
                            <div id="dvEachFor" style="display: none" runat="server">
                                <table>
                                    <tr>
                                        <td align="right">
                                            <font color="red">*</font>保存时间：
                                        </td>
                                        <td colspan="4">
                                            <input id="dpPlanTime" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'H:mm:ss'})"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="right" style="width: 92px">
                                            <font color="red">*</font>起始日期：
                                        </td>
                                        <td>
                                            <input id="dpPlanStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpPlanEnd\')||\'2020-10-01\'}'})"
                                                runat="server" />
                                        </td>
                                        <td style="width: 12px">
                                        </td>
                                        <td align="right">
                                            <font color="red">*</font>结束日期：
                                        </td>
                                        <td>
                                            <input id="dpPlanEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpPlanStart\')}',maxDate:'2020-10-01'})"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="display: none">
                                    <tr>
                                        <td style="width: 22px">
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="chkPlanWeek" runat="server" RepeatDirection="Vertical" RepeatColumns="8"
                                                RepeatLayout="Table" CellSpacing="8" />
                                        </td>
                                        <td>
                                            <input type="button" id="btnPlanAll" class="btn primary" value="全选" onclick="SetPlanWeekListVal('1')" />
                                            <input type="button" id="btnPlanUnAll" class="btn" value="反选" onclick="SetPlanWeekListVal('0')" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <div id="messageContent" runat="server" style="color: red;">
                            </div>
                            <div id="background" class="pcbackground" style="display: none;">
                            </div>
                            <div id="progressBar" class="pcprogressBar" style="display: none;">
                                数据加载中，请稍等...</div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        保存方式：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpSaveType" Width="77px" runat="server" onchange="SetPlanStyle(this.value);">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddPromotion" runat="server" CssClass="btn primary" Text="保存"
                                            OnClientClick="SetContronListVal();BtnLoadStyle();" OnClick="btnSave_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" OnClientClick="ClearClickEvent();"
                                            OnClick="btnRest_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
                <li></li>
            </ul>
        </div>
    </div>
    <asp:HiddenField ID="hidHotelID" runat="server" />
    <asp:HiddenField ID="hidCommonList" runat="server" />
    <asp:HiddenField ID="hidWeekList" runat="server" />
    <asp:HiddenField ID="hidPlanWeekList" runat="server" />
    <asp:HiddenField ID="hidEffHour" runat="server" />
</asp:Content>
