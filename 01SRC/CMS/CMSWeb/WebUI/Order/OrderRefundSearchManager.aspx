<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OrderRefundSearchManager.aspx.cs" Inherits="WebUI_Order_OrderRefundSearchManager" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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

        function SetUserHid() {
            document.getElementById("<%=hidSelecHotel.ClientID%>").value = document.getElementById("wctHotel").value;
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">搜索</li>
                        <li>
                            <table>
                                <tr>
                                    <td align="right">
                                        酒店:
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel"
                                            AutoParent="OrderRefundSearchManager.aspx?Type=hotel" />
                                    </td>
                                    <td align="right">
                                        销售:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpSalesManager" runat="server" Width="360px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="margin-left:5px;">
                                        订单编号:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFogaOrderNum" Width="230"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        创建时间：
                                    </td>
                                    <td>
                                        <input id="dpCreateStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />至：
                                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
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
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();SetUserHid();"
                                            OnClick="btnSearch_Click" />
                                        <input type="button" id="btnClear" class="btn" style="display: none" value="重置" onclick="ClearClickEvent();" />
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
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="frame02">
                    <asp:Panel ID="Panel1" runat="server" Height="700px" ScrollBars="Auto" Width="1450px">
                        <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False"
                            BackColor="White" CellPadding="4" CellSpacing="1" Width="2400px" EmptyDataText="没有数据"
                            PageSize="50" AllowPaging="True" OnPageIndexChanging="gridViewCSReviewList_PageIndexChanging"
                            CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:BoundField HeaderText="订单号" DataField="FOG_ORDER_NUM">
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="订单来源" DataField="VENDOR">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="酒店名称" DataField="HOTELNAME">
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="城市" DataField="CITYNAME">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="预定日期" DataField="CREATETIME">
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="入住日期" DataField="INDATE">
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="订单状态" DataField="BOOKSTATUS">
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="支付方式" DataField="PAYMETHOD">
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="支付状态" DataField="FOGRESVSTATUS">
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="background" class="pcbackground" style="display: none;">
        </div>
        <div id="progressBar" class="pcprogressBar" style="display: none;">
            数据加载中，请稍等...</div>
    </div>
    <asp:HiddenField ID="hidSelecHotel" runat="server" />
</asp:Content>
