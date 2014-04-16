<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewOverTimeOrder.aspx.cs"  Title="超时单管理" Inherits="ReviewOverTimeOrder" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<style type="text/css" >
.pcbackground { 
display: block; 
width: 100%; 
height: 100%; 
opacity: 0.4; 
filter: alpha(opacity=40); 
background:while; 
position: absolute; 
top: 0; 
left: 0; 
z-index: 2000; 
} 
.pcprogressBar { 
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
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("<%=ddpPriceCode.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
    }

     function SetControlValue() {
         document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
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
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
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
                        选择酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="ReviewOverTimeOrder.aspx?Type=hotel" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        价格代码：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPriceCode" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                     <td align="right">
                        创建时间：
                    </td>
                    <td>
                        <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                 <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlValue();BtnLoadStyle();" onclick="btnSearch_Click" />
                        <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/>
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    </ContentTemplate>
     <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>
        <div class="frame02">
             <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSReviewList_RowDataBound" AllowSorting="true" OnSorting="gridViewCSReviewList_Sorting" PageSize="10"  CssClass="GView_BodyCSS">
                    <Columns>
                        <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="fog_order_num" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID={0}" 
                        Target="_blank" DataTextField="fog_order_num"><ItemStyle HorizontalAlign="Center" Width="6%" /></asp:HyperLinkField>
                         <asp:BoundField DataField="login_mobile" HeaderText="登陆手机号" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="contact_name" HeaderText="联系人" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="hotel_id" HeaderText="酒店ID" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="hotel_name" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="11%"/></asp:BoundField>
                         <asp:BoundField DataField="linktel" HeaderText="酒店联系电话" ><ItemStyle HorizontalAlign="Center" Width="8%" /></asp:BoundField>
                         <asp:BoundField DataField="price_code" HeaderText="价格代码" ><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                         <asp:BoundField DataField="book_status_other_nm" HeaderText="订单状态" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                         <asp:BoundField DataField="in_date" HeaderText="入住时间" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="create_time" HeaderText="创建时间" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                         <asp:BoundField DataField="update_time" HeaderText="确认时间" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                         <asp:BoundField DataField="timeD_diff" HeaderText="时差" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                         <asp:BoundField DataField="cancel_reason" HeaderText="取消原因" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
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
        <div id="background" class="pcbackground" style="display: none; "></div> 
        <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
     </ContentTemplate>
    </asp:UpdatePanel>
</div>
<asp:HiddenField ID="hidHotel" runat="server"/>
<asp:HiddenField ID="hidCommonList" runat="server"/>
</asp:Content>