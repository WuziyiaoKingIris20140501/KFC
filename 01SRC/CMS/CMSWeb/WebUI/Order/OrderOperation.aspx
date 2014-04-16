<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="OrderOperation.aspx.cs"
    Title="LM订单操作" Inherits="OrderOperation" %>

<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
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
    <script language="javascript" type="text/javascript">
        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        function BtnSendFaxLoadStyle() {
            var ajaxbg = $("#backgroundSendFax,#progressBarSendFax");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnSendFaxCompleteStyle() {
            var ajaxbg = $("#backgroundSendFax,#progressBarSendFax");
            ajaxbg.hide();
        }

        function PopupHotelArea() {
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
            window.open('<%=ResolveClientUrl("~/WebUI/Hotel/HotelInfoManager.aspx")%>?hid=' + document.getElementById("<%=hidHotelID.ClientID%>").value + "&time=" + time, null, fulls);
        }

        function PopupArea() {
            var time = new Date();
            window.open("OrderOperationPrint.aspx?ID=" + document.getElementById("<%=hidOrderID.ClientID%>").value + "&time=" + time, "NewWindow", "width=800,height=750,scrollbars=yes,resizable=no");
        }

        function ConfirmSendFax() {
            var result = window.confirm("确定发送传真？");
            if (result) {
                return true;
                BtnSendFaxLoadStyle();
            } else {
                return false;
            }
        }
    </script>
    <div id="right">
        <table width="100%">
            <tr>
                <td style="width: 60%">
                    <div class="frame01">
                        <ul>
                            <li class="title">订单查询</li>
                            <li>
                                <table width="95%">
                                    <tr>
                                        <td align="right" width="15%">
                                            订单ID：
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <asp:TextBox ID="txtOrderID" runat="server" Width="150px" MaxLength="100" />
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="查询" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div id="detailMessageContent" runat="server" style="color: red">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </li>
                            <li class="title">订单信息</li>
                            <li>
                                <table width="95%" runat="server" id="tbDetail">
                                    <tr>
                                        <td align="right" width="15%">
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
                                            价格代码：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbPRICE_CODE" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            订单状态：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbBOOK_STATUS" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            担保：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbIS_GUA" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            到店时间：
                                        </td>
                                        <td align="left">
                                            酒店规定：
                                            <asp:Label ID="lbRESV_GUA_HOLD_TIME" runat="server" />&nbsp;&nbsp; 用户选择：
                                            <asp:Label ID="lbUSER_HOLD_TIME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            担保制度：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbRESV_GUA_NM" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            取消制度：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbRESV_CXL_NM" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            支付状态：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbPAY_STATUS" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            酒店名称：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbHOTEL_NAME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            联系电话：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbLINKTEL" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            入住人姓名：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbGUEST_NAMES" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            联系人姓名：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbCONTACT_NAME" runat="server" />
                                            &nbsp;&nbsp; 电话：
                                            <asp:Label ID="lbCONTACT_TEL" runat="server" />
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
                                            天数：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbOrderDays" runat="server" />天&nbsp;&nbsp;（<asp:Label ID="lbIN_DATE"
                                                runat="server" />
                                            至
                                            <asp:Label ID="lbOUT_DATE" runat="server" />）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            房型：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbROOM_TYPE_NAME" runat="server" />&nbsp;&nbsp;<asp:Label ID="lbBOOK_ROOM_NUM"
                                                runat="server" />间
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            到店时间：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbARRIVE_TIME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            优惠券：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbTICKET_USERCODE" runat="server" />-<asp:Label ID="lbTICKET_PAGENM"
                                                runat="server" />-<asp:Label ID="lbTICKET_AMOUNT" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            补充说明：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbBOOK_REMARK" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            HVP订单号：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbORDER_NUM" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </li>
                        </ul>
                    </div>
                    <div class="frame01">
                        <ul>
                            <li class="title">入住详情</li>
                        </ul>
                    </div>
                    <div class="frame02">
                        <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" AutoGenerateColumns="False"
                            BackColor="White" AllowPaging="True" CellPadding="4" CellSpacing="1"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" OnRowDataBound="gridViewCSSystemLogDetail_RowDataBound"
                            OnPageIndexChanging="gridViewCSSystemLogDetail_PageIndexChanging" PageSize="10"
                            CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:BoundField DataField="INDATE" HeaderText="日期" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TWOPRICE" HeaderText="价格" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BREAKFAST" HeaderText="早餐" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                        </asp:GridView>
                    </div>
                </td>
                <td style="width: 40%" valign="top">
                    <div class="frame01">
                        <ul>
                            <li class="title">订单操作</li>
                            <li>
                                <table width="90%" runat="server" id="tbControl">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            订单状态：
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <asp:DropDownList ID="ddpOrderStatus" CssClass="noborder_inactive" runat="server"
                                                Width="153px" OnSelectedIndexChanged="ddpOrderStatus_SelectedIndexChanged" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            备注：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBOOK_REMARK" runat="server" TextMode="MultiLine" Width="300px"
                                                Height="100px" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trCanlReson">
                                        <td align="right" style="width: 200px">
                                            取消原因：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddpCanelReson" CssClass="noborder_inactive" runat="server"
                                                Width="153px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnSet" runat="server" CssClass="btn primary" Text="确认修改" OnClientClick="BtnLoadStyle();"
                                                OnClick="btnSet_Click" />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnPrint" runat="server" CssClass="btn primary" Text="打印订单"
                                                OnClientClick="PopupArea()" />
                                            <asp:Button ID="btnSendFax" runat="server" CssClass="btn primary" OnClientClick="return ConfirmSendFax();"
                                                Text="发送传真" OnClick="btnSendFax_Click" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <div id="backgroundSendFax" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBarSendFax" class="pcprogressBar" style="display: none;">
        传真发送中，请稍等...</div>
    <asp:HiddenField ID="hidOrderID" runat="server" />
    <asp:HiddenField ID="hidHotelID" runat="server" />
</asp:Content>
