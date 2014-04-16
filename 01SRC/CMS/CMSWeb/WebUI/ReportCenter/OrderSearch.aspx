<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OrderSearch.aspx.cs" Inherits="WebUI_ReportCenter_OrderSearch" %>

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
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
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

        function SetControlValue() {
            document.getElementById("<%=HidCityID.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=HidSales.ClientID%>").value = document.getElementById("wctSales").value;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">查看数据</li>
                        <li>
                            <table>
                                <tr>
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
                                        城市：
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city"
                                            EnableViewState="false" AutoParent="OrderSearch.aspx?Type=city" />
                                    </td>
                                    <td>
                                        销售:
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server" AutoType="sales"
                                            EnableViewState="false" AutoParent="OrderSearch.aspx?Type=sales" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();SetControlValue();"
                                                    OnClick="btnSearch_Click" />
                                                <input type="button" id="btnClear" class="btn" style="display: none" value="重置" onclick="ClearClickEvent();" />
                                                <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel" OnClick="btnExport_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False"
                        BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                        DataKeyNames="Week" CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="HOTEL_ID" HeaderText="酒店ID">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prop_name_zh" HeaderText="酒店名称">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cityid" HeaderText="城市">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sales_account" HeaderText="销售人员">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="毛订单" HeaderText="毛订单">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="确认可住单" HeaderText="确认可住单">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="离店单" HeaderText="离店单">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="用户取消单" HeaderText="用户取消单">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CC取消单" HeaderText="CC取消单">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                </div>
                <div id="background" class="pcbackground" style="display: none;">
                </div>
                <div id="progressBar" class="pcprogressBar" style="display: none;">
                    数据加载中，请稍等...</div>
                <asp:HiddenField ID="HidCityID" runat="server" />
                <asp:HiddenField ID="HidSales" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
