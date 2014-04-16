<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="DuplicateOrderDataWeekly.aspx.cs"  Title="重复订单原因周报查询" Inherits="DuplicateOrderDataWeekly" %>

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
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
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
        <li class="title">查看周数据</li>
        <li>
            <table>
                <tr style="display:none">
                     <td align="right">
                        下单时间：
                    </td>
                    <td>
                        <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                 <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();" onclick="btnSearch_Click" />
                        <input type="button" id="btnClear" class="btn" style="display:none" value="重置"  onclick="ClearClickEvent();" />
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
                    
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="Week" 
                    CssClass="GView_BodyCSS">
                    <Columns>
                         <asp:BoundField DataField="Week" HeaderText="Week" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="重复预订" HeaderText="重复预订" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="重复预订取消单占比%" HeaderText="重复预订取消单占比%" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="BAR_重复预订" HeaderText="BAR_重复预订" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="BAR_重复预订占比%" HeaderText="BAR_重复预订占比%" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="LM_重复预订" HeaderText="LM_重复预订" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="LM_重复预订占比%" HeaderText="LM_重复预订占比%" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
                
        </div>
        <div id="background" class="pcbackground" style="display: none; "></div> 
        <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
     </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>