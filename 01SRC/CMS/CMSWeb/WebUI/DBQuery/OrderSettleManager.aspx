<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="OrderSettleManager.aspx.cs"  Title="酒店销售管理" Inherits="OrderSettleManager" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
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

        function SetControlStyle() {
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
        }

        function BtnSelectVal() {
            document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidSalesID.ClientID%>").value = document.getElementById("wctSales").value;
        }

        function SetWebAutoControl() {
            document.getElementById("wctHotel").value = document.getElementById("<%=hidHotelID.ClientID%>").value;
            document.getElementById("wctSales").text = document.getElementById("<%=hidSalesID.ClientID%>").value;
            BtnCompleteStyle();
        }

        function fn_Export(arg) {
            document.getElementById("<%=hidSelHotelID.ClientID%>").value = arg;
            document.getElementById("<%=btnExport.ClientID%>").click();
        }

        function fn_Tag(arg) {
            document.getElementById("<%=hidSelHotelID.ClientID%>").value = arg;
            BtnLoadStyle();
            document.getElementById("<%=btnTag.ClientID%>").click();
        }
    </script>

    <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">搜索现有酒店账单</li>
        <li>
            <table>
                <tr>
                    <td align="left">
                        酒店ID：
                    </td>
                    <td align="left">
                        <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel" AutoParent="OrderSettleManager.aspx?Type=hotel" />
                    </td>
                    <td align="left">
                        销售人员：
                    </td>
                    <td align="left">
                        <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server" AutoType="sales" AutoParent="OrderSettleManager.aspx?Type=sales" />
                    </td>
                    <td align="left">
                        月份：
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkMonth3" runat="server" Text="3" />
                        <asp:CheckBox ID="chkMonth4" runat="server" Text="4" />
                        <asp:CheckBox ID="chkMonth5" runat="server" Text="5" />
                        <asp:CheckBox ID="chkMonth6" runat="server" Text="6" />
                        <asp:CheckBox ID="chkMonth7" runat="server" Text="7" />
                        <asp:CheckBox ID="chkMonth8" runat="server" Text="8" />
                        <asp:CheckBox ID="chkMonth9" runat="server" Text="9" />
                        <asp:CheckBox ID="chkMonth10" runat="server" Text="10" />
                        <asp:CheckBox ID="chkMonth11" runat="server" Text="11" />
                        <asp:CheckBox ID="chkMonth12" runat="server" Text="12" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();BtnSelectVal()" onclick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame02">
        <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
        <asp:GridView ID="gridViewCSAPPContenList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" PageSize="20" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID"
                            onrowdatabound="gridViewCSAPPContenList_RowDataBound" CssClass="GView_BodyCSS">
                <Columns>

                    <asp:BoundField DataField="HOTELID" HeaderText="酒店ID"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" /></asp:BoundField>
                    <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%"/></asp:BoundField>
                    <asp:BoundField DataField="COMMISSION" HeaderText="总佣金" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%"/></asp:BoundField>
                    <asp:BoundField DataField="SALES" HeaderText="酒店销售" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%"/></asp:BoundField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="导出账单">
                      <ItemTemplate>
                        <div id="dvchkList3" runat="server" style="float:left"><input id="chkList3" type="checkbox" value="3" runat="server" checked="checked"/>3</div>
                        <div id="dvchkList4" runat="server" style="float:left"><input id="chkList4" type="checkbox" value="4" runat="server" checked="checked"/>4</div>
                        <div id="dvchkList5" runat="server" style="float:left"><input id="chkList5" type="checkbox" value="5" runat="server" checked="checked"/>5</div>
                        <div id="dvchkList6" runat="server" style="float:left"><input id="chkList6" type="checkbox" value="6" runat="server" checked="checked"/>6</div>
                        <div id="dvchkList7" runat="server" style="float:left"><input id="chkList7" type="checkbox" value="7" runat="server" checked="checked"/>7</div>
                        <div id="dvchkList8" runat="server" style="float:left"><input id="chkList8" type="checkbox" value="8" runat="server" checked="checked"/>8</div>
                        <div id="dvchkList9" runat="server" style="float:left"><input id="chkList9" type="checkbox" value="9" runat="server" checked="checked"/>9</div>
                        <div id="dvchkList10" runat="server" style="float:left"><input id="chkList10" type="checkbox" value="10" runat="server" checked="checked"/>10</div>
                        <div id="dvchkList11" runat="server" style="float:left"><input id="chkList11" type="checkbox" value="11" runat="server" checked="checked"/>11</div>
                        <div id="dvchkList12" runat="server" style="float:left"><input id="chkList12" type="checkbox" value="12" runat="server" checked="checked"/>12</div>
                        <div style="float:right">
                        <input id="btnListExport" type="button" value="导出" onclick="fn_Export('<%# DataBinder.Eval(Container.DataItem, "HOTELID") %>')"/>
                        </div>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="23%"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="标记账单">
                      <ItemTemplate>
                        <div id="dvchkCom3" runat="server" style="float:left"><input id="chkCom3" type="checkbox" value="3" runat="server"/>3</div>
                        <div id="dvchkCom4" runat="server" style="float:left"><input id="chkCom4" type="checkbox" value="4" runat="server"/>4</div>
                        <div id="dvchkCom5" runat="server" style="float:left"><input id="chkCom5" type="checkbox" value="5" runat="server"/>5</div>
                        <div id="dvchkCom6" runat="server" style="float:left"><input id="chkCom6" type="checkbox" value="6" runat="server"/>6</div>
                        <div id="dvchkCom7" runat="server" style="float:left"><input id="chkCom7" type="checkbox" value="7" runat="server"/>7</div>
                        <div id="dvchkCom8" runat="server" style="float:left"><input id="chkCom8" type="checkbox" value="8" runat="server"/>8</div>
                        <div id="dvchkCom9" runat="server" style="float:left"><input id="chkCom9" type="checkbox" value="9" runat="server"/>9</div>
                        <div id="dvchkCom10" runat="server" style="float:left"><input id="chkCom10" type="checkbox" value="10" runat="server"/>10</div>
                        <div id="dvchkCom11" runat="server" style="float:left"><input id="chkCom11" type="checkbox" value="11" runat="server"/>11</div>
                        <div id="dvchkCom12" runat="server" style="float:left"><input id="chkCom12" type="checkbox" value="12" runat="server"/>12</div>
                        <div style="float:right">
                        <input id="btnListExport" type="button" value="标记" onclick="fn_Tag('<%# DataBinder.Eval(Container.DataItem, "HOTELID") %>')"/>
                        </div>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="23%"></ItemStyle>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
        </asp:GridView>
        <div style="margin-left:10px;">
            <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
         </div>
    </div>
    </div>
    <asp:HiddenField ID="hidHotelID" runat="server" />
    <asp:HiddenField ID="hidSalesID" runat="server" />
    <asp:HiddenField ID="hidSelHotelID" runat="server" />
    <div style="display:none;">
        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" onclick="btnExport_Click" />
        <asp:Button ID="btnTag" runat="server" CssClass="btn primary" onclick="btnTag_Click" />
    </div>

     <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
</asp:Content>