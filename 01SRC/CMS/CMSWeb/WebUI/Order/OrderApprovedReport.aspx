<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OrderApprovedReport.aspx.cs" Inherits="WebUI_Order_OrderApprovedReport" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        function SetControlValue() {
            document.getElementById("<%=hidSelectCity.ClientID%>").value = document.getElementById("wctCity").value;
        }
        function SetAllControlValue() {
            document.getElementById("<%=hidAllSelectCity.ClientID%>").value = document.getElementById("wctAllCity").value;
        }
        function BtnBalLoadStyle() {
            var ajaxbg = $("#Div1,#Div2");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnBalCompleteStyle() {
            var ajaxbg = $("#Div1,#Div2");
            ajaxbg.hide();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin: 5px 14px 5px 14px;">
        <div id="tabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#tabs-1">按日期统计 </a></li>
                <li style="display:none;"><a href="#tabs-2">按人员统计 </a></li>
                <li><a href="#tabs-3">按人员统计(完整) </a></li>
            </ul>
            <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">按日期统计</li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        选择日期：
                                    </td>
                                    <td>
                                        <input id="StartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_EndDate\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        至：
                                        <input id="EndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_StartDate\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td>
                                        城市:
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" AutoType="city"
                                            AutoParent="OrderApprovedReport.aspx?Type=city" />
                                    </td>
                                    <%--<td>
                                        人员:</td>
                                    <td><uc1:WebAutoComplete ID="wcthvpInventoryControl" CTLID="wcthvpInventoryControl"
                                            runat="server" EnableViewState="false" AutoType="hvpInventoryControl" AutoParent="OrderApprovedReport.aspx?Type=hvpInventoryControl" />
                                    </td>--%>
                                    <td>
                                        <asp:Button ID="btnBalSearch" runat="server" CssClass="btn primary" Text="查询" OnClientClick="BtnBalLoadStyle();SetControlValue();"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">日期审核详细</li>
                        <li>
                            <div id="divDateMain" runat="server" style="display: none;">
                                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                                    <ul>
                                        <li class="title">初始数据</li>
                                        <li>
                                            <div id="divHotelSys" runat="server">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                                    <ul>
                                        <li class="title">初审数据</li>
                                        <li>
                                            <div id="divHotelFirts" runat="server">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                                    <ul>
                                        <li class="title">复审数据</li>
                                        <li>
                                            <div id="divHotelCheck" runat="server">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                                    <ul>
                                        <li class="title">NS数据</li>
                                        <li>
                                            <div id="divHotelNS" runat="server">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <asp:GridView ID="gridViewCSReviewUserList" runat="server" AutoGenerateColumns="False"
                                BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                                DataKeyNames="ID" CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="DATE" HeaderText="日期">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ORDERNUM" HeaderText="订单数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTTRIAL" HeaderText="初审订单">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTNS" HeaderText="初审NS订单">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTRATE" HeaderText="初审完成率">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKORDERNUM" HeaderText="复审订单数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTRIAL" HeaderText="复审已完成">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKNS" HeaderText="复审NS订单">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKRATE" HeaderText="复审完成率">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NSPICKRATE" HeaderText="NS拾回率">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                        </li>
                    </ul>
                </div>
            </div>
            <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">按人员统计</li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        选择日期：
                                    </td>
                                    <td>
                                        <input id="StartDatePanel" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_EndDatePanel\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        至：
                                        <input id="EndDatePanel" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_StartDatePanel\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td style="width: 100px; text-align: right;">
                                        城市:
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="WebAutoComplete1" runat="server" CTLID="wctCity" AutoType="city"
                                            AutoParent="OrderApprovedReport.aspx?Type=city" />
                                    </td>
                                    <td style="padding-left: 50px;">
                                        <asp:Button ID="btnApprovSearch" runat="server" CssClass="btn primary" Text="查询"
                                            OnClientClick="BtnBalLoadStyle();SetControlValue();" OnClick="btnApprovSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">人员审核详细</li>
                        <li>
                            <div id="mainPanelDiv" runat="server" style="display: none;">
                                <div class="frame01">
                                    <ul>
                                        <li class="title">初始数据</li>
                                        <li>
                                            <div id="mainPanelDiv3" runat="server">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID"
                                CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="FIRSTSALES" HeaderText="人员">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTTORDERS" HeaderText="初审订单数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTNSRATE" HeaderText="初审NS数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTORDERS" HeaderText="复审订单数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKNSRATE" HeaderText="复审离店数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                        </li>
                    </ul>
                </div>
            </div>
            <div id="tabs-3" style="border: 1px solid #D5D5D5;">
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">按人员统计</li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        选择日期：
                                    </td>
                                    <td>
                                        <input id="AllStartDatePanel" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_AllEndDatePanel\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        至：
                                        <input id="AllEndDatePanel" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_AllStartDatePanel\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td style="width: 100px; text-align: right;">
                                        城市:
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctAllCity" runat="server" CTLID="wctAllCity" AutoType="city"
                                            AutoParent="OrderApprovedReport.aspx?Type=city" />
                                    </td>
                                    <td style="padding-left: 50px;">
                                        <asp:Button ID="btnAllApprovSearch" runat="server" CssClass="btn primary" Text="查询"
                                            OnClientClick="BtnBalLoadStyle();SetAllControlValue();" OnClick="btnAllApprovSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">人员审核详细</li>
                        <li>
                            <div id="Div4" runat="server" >
                                <div class="frame01">
                                    <ul>
                                        <li class="title">初始数据</li>
                                        <li>

                                            <div id="divAllData" runat="server">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <asp:GridView ID="gridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID"
                                CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="FIRSTSALES" HeaderText="人员">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTHOTELS" HeaderText="初审总酒店数量">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTORDERS" HeaderText="初审总订单数量">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTROOMNIGHT" HeaderText="间夜数量">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTRNS" HeaderText="初审NS订单数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField> 
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                            <asp:GridView ID="gridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID"
                                CssClass="GView_BodyCSS">
                                <Columns>
                                   <asp:BoundField DataField="FIRSTSALES" HeaderText="人员">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTHOTELS" HeaderText="复审总酒店数量">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTORDERS" HeaderText="复审总订单数量">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTROOMNIGHT" HeaderText="复审总间夜数量">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTOUTORDERS" HeaderText="复审离店总订单数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECHECKTOUTROOMNIGHT" HeaderText="复审离店总间夜数">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div id="Div1" class="pcbackground" style="display: none;">
        </div>
        <div id="Div2" class="pcprogressBar" style="display: none;">
            数据加载中，请稍等...</div>
        <asp:HiddenField ID="hidSelectedID" runat="server" />
        <script type="text/javascript">
            $(function () {
                //        $("#tabs").tabs();
                var sid = document.getElementById("<%=hidSelectedID.ClientID%>").value;
                if (sid == "" || sid == "0") {
                    $("#tabs").tabs();
                }
                else {
                    $('#tabs').tabs({ selected: sid, select: function (event, ui) { document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index } });
                }

                $('#tabs').bind('tabsselect', function (event, ui) {
                    document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index
                });

            });
        </script>
    </div>
    <asp:HiddenField ID="hidSelectCity" runat="server" />
    <asp:HiddenField ID="hidAllSelectCity" runat="server" />
    <asp:HiddenField ID="hidSelectBussiness" runat="server" />
</asp:Content>
