<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="CohortAnalysisDataWeekly.aspx.cs"  Title="Cohort分析数据查询" Inherits="CohortAnalysisDataWeekly" %>

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
.dvGridScroll
{
scrollbar-face-color: #FF0000; 
scrollbar-shadow-color: #FF0000;
scrollbar-highlight-color: #FFBABA; 
scrollbar-3dlight-color: #000000;
scrollbar-darkshadow-color: #000000; 
scrollbar-track-color: #8C8C8C;
scrollbar-arrow-color: #FFFFFF;
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
                    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" Width="1215px">
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="2400px" EmptyDataText="没有数据" DataKeyNames="日期" 
                    CssClass="GView_BodyCSS">
                    <Columns>
                         <asp:BoundField DataField="日期" HeaderText="日期" ><ItemStyle HorizontalAlign="Center" Width="3%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户数" HeaderText="新用户数" ><ItemStyle HorizontalAlign="Center" Width="2%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户且下毛单" HeaderText="新用户且下毛单" ><ItemStyle HorizontalAlign="Center" Width="3%"/></asp:BoundField>
                         <asp:BoundField DataField="下单总用户" HeaderText="下单总用户" ><ItemStyle HorizontalAlign="Center" Width="3%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户且下成功单" HeaderText="新用户且下成功单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户2-7天回访" HeaderText="新用户2-7天回访" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户8-14天回访" HeaderText="新用户8-14天回访" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户15-30天回访" HeaderText="新用户15-30天回访" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户31-90天回访" HeaderText="新用户31-90天回访" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户2-7天下毛单" HeaderText="新用户2-7天下毛单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户2-7天下成功单" HeaderText="新用户2-7天下成功单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户8-14天下毛单" HeaderText="新用户8-14天下毛单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户8-14天下成功单" HeaderText="新用户8-14天下成功单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户15-30天下毛单" HeaderText="新用户15-30天下毛单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户15-30天下成功单" HeaderText="新用户15-30天下成功单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户31-90天下毛单" HeaderText="新用户31-90天下毛单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="新用户31-90天下成功单" HeaderText="新用户31-90天下成功单" ><ItemStyle HorizontalAlign="Center" Width="4%"/></asp:BoundField>
                         <asp:BoundField DataField="老用户" HeaderText="老用户" ><ItemStyle HorizontalAlign="Center" Width="2%"/></asp:BoundField>
                         <asp:BoundField DataField="下毛单老用户" HeaderText="下毛单老用户" ><ItemStyle HorizontalAlign="Center" Width="3%"/></asp:BoundField>
                         <asp:BoundField DataField="下成功单老用户" HeaderText="下成功单老用户" ><ItemStyle HorizontalAlign="Center" Width="3%"/></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
                </asp:Panel>
        </div>
        <div id="background" class="pcbackground" style="display: none; "></div> 
        <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
     </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>