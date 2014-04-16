<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LmOrderLogPage.aspx.cs" Inherits="WebUI_DBQuery_LmOrderLogPage" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
    </style>
    <script type="text/javascript" language="javascript">
        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        function SetControlValue() {
            document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=hidTagInfo.ClientID%>").value = document.getElementById("wcthvpTagInfo").value;
            document.getElementById("<%=hidGroup.ClientID%>").value = document.getElementById("wctHotelGroupCodeNew").value;

            var commidList = "";
            if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {

                        commidList = commidList + document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).value + ',';
                    }
                }
                document.getElementById("<%=hidCommonList.ClientID%>").value = commidList;
            }
        }

        function ClearDateControl() {
            if (document.getElementById("<%=txtOrderID.ClientID%>").value.trim() != "" || document.getElementById("<%=txtMobile.ClientID%>").value.trim() != "") {
                document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
                document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";


                document.getElementById("<%=chkSucceed.ClientID%>").checked = false;
                document.getElementById("<%=chkUserCancelN.ClientID%>").checked = false;
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
                        <li class="title">订单查找</li>
                        <li>
                            <table>
                                <tr>
                                    <td align="right">
                                        下单时间：
                                    </td>
                                    <td colspan="3">
                                        <input id="dpCreateStart" class="Wdate" type="text" style="width: 300px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        <input id="dpCreateEnd" class="Wdate" type="text" style="width: 300px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td align="right">
                                        酒店集团：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctHotelGroupCodeNew" runat="server" CTLID="wctHotelGroupCodeNew"
                                            AutoType="hotelgroup" AutoParent="LmOrderLogPage.aspx?Type=hotelgroup" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        入住时间：
                                    </td>
                                    <td>
                                        <input id="dpInStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpInEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        <input id="dpInEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpInStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td align="right">
                                        城市：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city"
                                            AutoParent="LmOrderLogPage.aspx?Type=city" />
                                    </td>
                                    <td align="right">
                                        商圈：
                                    </td>
                                    <td>
                                        <%--<uc1:WebAutoComplete ID="wcthvpTagInfo" runat="server" CTLID="wcthvpTagInfo" AutoType="hvptaginfo"
                                            EnableViewState="false" AutoParent="LmOrderLogPage.aspx?Type=hvptaginfo" />--%>
                                        <uc1:WebAutoComplete ID="wcthvpTagInfo" runat="server" CTLID="wcthvpTagInfo" AutoType="tag"
                                            EnableViewState="false" AutoParent="LmOrderLogPage.aspx?Type=tag" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        离店时间：
                                    </td>
                                    <td>
                                        <input id="dpOutStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpOutEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        <input id="dpOutEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpOutStart\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                    </td>
                                    <td align="right">
                                        酒店销售人员：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpSalesManager" runat="server" Width="360px" Height="25px">
                                        </asp:DropDownList>
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
                                        手机号码：
                                    </td>
                                    <td>
                                        <input name="textfield" type="text" id="txtMobile" runat="server" style="width: 152px;"
                                            maxlength="15" value="" onkeydown="ClearDateControl()" onkeypress="ClearDateControl()"
                                            onkeyup="ClearDateControl()" />
                                    </td>
                                    <td align="right">
                                        订单ID：
                                    </td>
                                    <td>
                                        <input name="textfield" type="text" id="txtOrderID" runat="server" style="width: 348px;"
                                            maxlength="200" onkeyup="ClearDateControl()" onkeypress="ClearDateControl()"
                                            onkeydown="ClearDateControl()" value="" />
                                    </td>
                                    <td align="right">
                                        入住人姓名：
                                    </td>
                                    <td>
                                        <input name="textfield" type="text" id="txtGuestNM" runat="server" style="width: 152px;"
                                            value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        创建状态：
                                    </td>
                                    <td>
                                        <input type="checkbox" id="chkSucceed" runat="server" />成功&nbsp;&nbsp;<input type="checkbox"
                                            id="chkFail" runat="server" />失败
                                    </td>
                                    <td align="right">
                                        用户取消：
                                    </td>
                                    <td>
                                        <input type="checkbox" id="chkUserCancelN" runat="server" />否&nbsp;&nbsp;<input type="checkbox"
                                            id="chkUserCancelY" runat="server" />是
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
                <div class="frame01">
                    <ul>
                        <li class="title">订单筛选</li>
                        <li>
                            <table>
                                <tr>
                                    <td align="right">
                                        订单类型：
                                    </td>
                                    <td style="width: 330px;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="chkPayCode" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                                                        RepeatColumns="8" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        酒店确认状态：
                                    </td>
                                    <td style="width: 330px;">
                                        <asp:DropDownList ID="ddlAffirmStatus" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        确认超时：
                                    </td>
                                    <td>
                                        <input type="checkbox" id="chkTimeOutN" runat="server" />否&nbsp;&nbsp;<input type="checkbox"
                                            id="chkTimeOutY" runat="server" />是
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        含返现券：
                                    </td>
                                    <td>
                                        <input type="checkbox" id="chkTicketY" runat="server" />有&nbsp;&nbsp;<input type="checkbox"
                                            id="chkTicketN" runat="server" />无
                                    </td>
                                    <td align="right">
                                        应用平台：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpPlatForm" runat="server" Width="150px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        订单渠道：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpOrderChannel" runat="server" Width="150px" Height="25px">
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
                                        支付方式：
                                    </td>
                                    <td>
                                        <input type="checkbox" id="chkPayMethodAlipay" runat="server" />支付宝&nbsp;&nbsp;<input
                                            type="checkbox" id="chkPayMethodUnionPay" runat="server" />银联
                                    </td>
                                    <td align="right">
                                        支付状态：
                                    </td>
                                    <td>
                                        <input type="checkbox" id="chkPayStatusSus" runat="server" />支付成功&nbsp;&nbsp;<input
                                            type="checkbox" id="chkPayStatusBackPay" runat="server" />待支付&nbsp;&nbsp;<input type="checkbox"
                                                id="chkPayStatusRebate" runat="server" />已退款
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlValue

();BtnLoadStyle();" OnClick="btnSearch_Click" />
                                        <input type="button" id="btnClear" class="btn" value="重置" onclick="ClearClickEvent();" />
                                        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel" OnClick="btnExport_Click" />
                                    </td>
                                </tr>
                            </table>
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
                    <div style="width: 100%; overflow-x: auto">
                        <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False"
                            BackColor="White" CellPadding="4" CellSpacing="1" Width="120%" EmptyDataText="没有数据"
                            DataKeyNames="ID" OnRowDataBound="gridViewCSReviewLmSystemLogList_RowDataBound"
                            AllowSorting="true" PageSize="50" CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:HyperLinkField HeaderText="订单ID" DataNavigateUrlFields="LMID" DataNavigateUrlFormatString="LmSystemLogDetailPageByNew.aspx?ID={0}"
                                    Target="_blank" DataTextField="LMID">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="FOG_ORDER_NUM" HeaderText="FOG订单ID">
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="登录手机号" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpp" runat="server" Text='<%# Eval("login_mobile") %>' NavigateUrl="000."></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="create_time" HeaderText="创建时间">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hotel_name" HeaderText="酒店">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="name_cn" HeaderText="城市">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="room_type_name" HeaderText="房型">
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="book_room_num" HeaderText="预定间数">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="price_code" HeaderText="价格代码">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="book_price" HeaderText="预定价格">
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="in_date" HeaderText="入住日期">
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="out_date" HeaderText="离店日期">
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="guest_names" HeaderText="入住人">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FOGRESVSTATUS" HeaderText="确认状态">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FOGAUDITSTATUS" HeaderText="审核状态">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sales_account" HeaderText="销售">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="confirmTime" HeaderText="确认时长">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ticket_amount" HeaderText="含券金额">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="order_channel" HeaderText="订单渠道">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="app_platform" HeaderText="订单平台">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
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
                    </div>
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
    <asp:HiddenField ID="hidTagInfo" runat="server" />
    <asp:HiddenField ID="hidGroup" runat="server" />
    <asp:HiddenField ID="hidCommonList" runat="server" />
    <asp:HiddenField ID="hidSalesCommonList" runat="server" />
</asp:Content>
