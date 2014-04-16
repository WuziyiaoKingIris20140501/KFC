<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="LmOrderLogPageBak.aspx.cs"
    Title="LM订单历史查询" Inherits="LmOrderLogPage" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
    </style>
    <%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script language="javascript" type="text/javascript">
        function ClearClickEvent() {
            document.getElementById("<%=txtOrderID.ClientID%>").value = "";
            document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
            document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
            document.getElementById("<%=txtMobile.ClientID%>").value = "";
            document.getElementById("<%=dpInStart.ClientID%>").value = "";
            document.getElementById("<%=dpInEnd.ClientID%>").value = "";
            document.getElementById("<%=dpOutStart.ClientID%>").value = "";
            document.getElementById("<%=dpOutEnd.ClientID%>").value = "";

            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
            document.getElementById("wctHotelGroupCodeNew").value = "";
            document.getElementById("wctHotelGroupCodeNew").text = "";


            document.getElementById("<%=ddpBookStatus.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpPayStatus.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpTicket.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpAprove.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpHotelComfirm.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpPlatForm.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpSalesManager.ClientID%>").selectedIndex = 0;

            document.getElementById("<%=chkAllCommon.ClientID%>").checked = true;
            if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {
                        document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked = false;
                    }
                    document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled = true;
                }
            }

            document.getElementById("<%=chkOutTest.ClientID%>").checked = false;
            document.getElementById("<%=chkOutCC.ClientID%>").checked = false;
            document.getElementById("<%=chkOutUC.ClientID%>").checked = false;

            var btnObject = document.getElementById("<%=dvCommonList.ClientID%>");
            var btnInput = btnObject.getElementsByTagName("input");
            var btnLength = btnInput.length;
            for (var i = btnLength - 1; i >= 0; i--) {
                if (btnInput[i].type = "button") {
                    btnObject.removeChild(btnInput[i]);
                }
            }
        }

        function SetRbtListValue(arg) {
            var vRbtid = document.getElementById(arg);
            //得到所有radio
            var vRbtidList = vRbtid.getElementsByTagName("input");
            if (vRbtidList.length > 0) {
                vRbtidList[0].checked = true;
            }
        }

        function SetControlValue() {
            document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=hidGroup.ClientID%>").value = document.getElementById("wctHotelGroupCodeNew").value;

            var commidList = "";
            var SalescommidList = "";
            if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {

                        commidList = commidList + document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).value + ',';
                    }
                }
                document.getElementById("<%=hidCommonList.ClientID%>").value = commidList;
            }

            var commboard = document.getElementById("<%=dvCommonList.ClientID%>");
            for (i = 0; i < commboard.childNodes.length; i++) {
                SalescommidList = SalescommidList + commboard.childNodes[i].id.substring(10) + ',';
            }
            document.getElementById("<%=hidSalesCommonList.ClientID%>").value = SalescommidList;
        }

        function PopupArea(arg) {
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
            var retunValue = window.open("LmSystemLogDetailPageByNew.aspx?ID=" + arg + "&time=" + time, null, fulls);
        }

        function SetChkAllCommonStyle() {
            if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true) {
                if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                    var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                    for (var i = 0; i < objCheck.length; i++) {
                        if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {
                            document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked = false;
                        }
                        document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled = true;
                    }
                }
            }
            else {
                if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                    var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                    for (var i = 0; i < objCheck.length; i++) {
                        if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled) {
                            document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled = false;
                        }
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

        function BtnCommonList() {
            var btnid;
            var dpValue;
            var idVal;

            idVal = document.getElementById("<%=ddpSalesManager.ClientID%>").value;
            dpValue = document.getElementById("<%=ddpSalesManager.ClientID%>").options[document.getElementById("<%=ddpSalesManager.ClientID%>").selectedIndex].text + "   ";

            if (idVal.trim() == "") {
                return;
            }

            btnid = "btnCommon_" + idVal;

            if (btnid == "btnCommon_") {
                return;
            }

            if (document.getElementById(btnid) != null) {
                return;
            }

            var board = document.getElementById("<%=dvCommonList.ClientID%>");
            var e = document.createElement("input");
            e.type = "button";
            e.setAttribute("id", btnid);
            e.value = dpValue;
            e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
            e.onclick = function () {
                e.parentNode.removeChild(this);
            }
            board.appendChild(e);
        }

        function ClearDateControl() {
            if (document.getElementById("<%=txtOrderID.ClientID%>").value.trim() != "") {
                document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
                document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">查看订单操作历史</li>
                        <li>
                            <table>
                                <tr>
                                    <td align="right">
                                        订单ID：
                                    </td>
                                    <td>
                                        <input name="textfield" type="text" id="txtOrderID" runat="server" style="width: 348px;"
                                            maxlength="200" onkeyup="ClearDateControl()" onkeypress="ClearDateControl()"
                                            onkeydown="ClearDateControl()" value="" />
                                    </td>
                                    <td align="right">
                                        选择城市：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city"
                                            AutoParent="LmOrderLogPage.aspx?Type=city" />
                                    </td>
                                    <td align="right">
                                        选择酒店：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel"
                                            AutoParent="LmOrderLogPage.aspx?Type=hotel" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        下单时间：
                                    </td>
                                    <td>
                                        <input id="dpCreateStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server" />
                                        <input id="dpCreateEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server" />
                                    </td>
                                    <td align="right">
                                        离店时间：
                                    </td>
                                    <td>
                                        <input id="dpOutStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpOutEnd\')||\'2020-10-01\'}'})" runat="server" />
                                        <input id="dpOutEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpOutStart\')}',maxDate:'2020-10-01'})" runat="server" />
                                    </td>
                                    <td align="right">
                                        入住时间：
                                    </td>
                                    <td>
                                        <input id="dpInStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpInEnd\')||\'2020-10-01\'}'})" runat="server" />
                                        <input id="dpInEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpInStart\')}',maxDate:'2020-10-01'})" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        手机号码：
                                    </td>
                                    <td>
                                        <input name="textfield" type="text" id="txtMobile" runat="server" style="width: 152px;"
                                            maxlength="15" value="" />
                                    </td>
                                    <td align="right">
                                        入住人姓名：
                                    </td>
                                    <td>
                                        <input name="textfield" type="text" id="txtGuestNM" runat="server" style="width: 152px;"
                                            value="" />
                                    </td>
                                    <td align="right">
                                        即时单：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpIsReserve" runat="server" Width="152px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        订单类型：
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="chkPayCode" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                                                        RepeatColumns="8" />
                                                </td>
                                                <td>
                                                    <div id="dvHotelChkCommon" runat="server">
                                                        <input type="checkbox" name="chkAllCommon" id="chkAllCommon" runat="server" onclick="SetChkAllCommonStyle()" />不限制</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        酒店确认状态：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpHotelComfirm" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        订单状态：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpBookStatus" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        审核状态：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpAprove" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        支付状态：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpPayStatus" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        应用平台：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpPlatForm" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        使用优惠券：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpTicket" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        订单渠道：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpOrderChannel" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        酒店销售人员：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpSalesManager" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                        <input id="btnAddCommon" type="button" value="添加" class="btn primary" onclick="BtnCommonList()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        酒店集团：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctHotelGroupCodeNew" runat="server" CTLID="wctHotelGroupCodeNew"
                                            AutoType="hotelgroup" AutoParent="LmOrderLogPage.aspx?Type=hotelgroup" />
                                    </td>
                                    <td colspan="4">
                                        <input type="checkbox" id="chkOutTest" runat="server" />不含测试单&nbsp;&nbsp;<input type="checkbox"
                                            id="chkOutCC" runat="server" />不含CC取消单&nbsp;&nbsp;<input type="checkbox" id="chkOutUC"
                                                runat="server" />不含用户取消单&nbsp;&nbsp;<input type="checkbox" id="chkFailOrder"
                                                runat="server" />不含失败单
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td colspan="3">
                                        <div id="dvCommonList" runat="server" style="width: 800px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlValue

();BtnLoadStyle();" OnClick="btnSearch_Click" />
                                        <input type="button" id="btnClear" class="btn" value="重置" onclick="ClearClickEvent();" />
                                        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel" OnClick="btnExport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <div id="messageContent" runat="server" style="color: red">
                            </div>
                        </li>
                    </ul>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="frame02">
                    <div style="margin-left: 10px;">
                        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1">
                        </webdiyer:AspNetPager>
                    </div>
                    <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False"
                        BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                        DataKeyNames="ID" OnRowDataBound="gridViewCSReviewLmSystemLogList_RowDataBound"
                        AllowSorting="true" OnSorting="gridViewCSReviewLmSystemLogList_Sorting" PageSize="50"
                        CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:HyperLinkField HeaderText="LM订单ID" DataNavigateUrlFields="LMID" DataNavigateUrlFormatString="LmSystemOrderDetailPage.aspx?ID={0}"
                                Target="_blank" DataTextField="LMID">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="FOGORDERID" HeaderText="FOG订ID">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <%--    <asp:HyperLinkField HeaderText="登录手机号" DataNavigateUrlFields="LOGINMOBILE" DataNavigateUrlFormatString="~/WebUI/UserGroup/UserDetailPage.aspx?ID={0}&TYPE=1" 
                         Target="_blank" DataTextField="LOGINMOBILE"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:HyperLinkField>--%>
                            <asp:TemplateField HeaderText="登录手机号" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpp" runat="server" Text='<%# Eval("LOGINMOBILE") %>' NavigateUrl="000."></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRICECODE" HeaderText="价格代码">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" HeaderText="创建时间" SortExpression="CREATETIME">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INDATE" HeaderText="入住日期" SortExpression="INDATE">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OUTDATE" HeaderText="离店日期" SortExpression="OUTDATE">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BTPRICE" HeaderText="金额" SortExpression="BTPRICE">
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TICKETAMOUNT" HeaderText="优惠券金额" SortExpression="TICKETAMOUNT">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDERSTATUS" HeaderText="预付订单状态" SortExpression="ORDERSTATUS">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDERSTATUSOTHER" HeaderText="现付订单状态" SortExpression="ORDERSTATUS">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FOGRESVSTATUS" HeaderText="确认状态" SortExpression="FOGRESVSTATUS">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FOGRESVTYPE" HeaderText="订单状态" SortExpression="FOGRESVTYPE">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态" Visible="false" SortExpression="PAYSTATUS">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FOGAUDITSTATUS" HeaderText="审核状态" SortExpression="FOGAUDITSTATUS">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GUESTNAMES" HeaderText="入住人">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SALESMG" HeaderText="销售人员">
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="UserID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("User_ID") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                    <div style="margin-left: 10px;">
                        <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb" ID="AspNetPager1"
                            runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                            CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页"
                            ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%"
                            ShowPageIndexBox="always" AlwaysShow="true" Width="100%" LayoutType="Table" OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
                <div id="background" class="pcbackground" style="display: none;">
                </div>
                <div id="progressBar" class="pcprogressBar" style="display: none;">
                    数据加载中，请稍等...</div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hidHotel" runat="server" />
    <asp:HiddenField ID="hidCity" runat="server" />
    <asp:HiddenField ID="hidGroup" runat="server" />
    <asp:HiddenField ID="hidCommonList" runat="server" />
    <asp:HiddenField ID="hidSalesCommonList" runat="server" />
</asp:Content>
