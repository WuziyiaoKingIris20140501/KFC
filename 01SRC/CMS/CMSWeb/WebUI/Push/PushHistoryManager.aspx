<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="PushHistoryManager.aspx.cs"  Title="Push消息管理" Inherits="PushHistoryManager" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

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
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtPushTitle.ClientID%>").value = "";
        document.getElementById("<%=txtPushContent.ClientID%>").value = "";
        document.getElementById("<%=dpSendStart.ClientID%>").value = "";
        document.getElementById("<%=dpSendEnd.ClientID%>").value = "";
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
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01" id="dvAdd">
              <ul>
                <li class="title">历史消息查询</li>
                <li>
                    <table>
                        <tr>
                            <td align="right">
                                Push任务标题：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPushTitle" runat="server" Width="500px" MaxLength="32" /><font color="#AAAAAA"> * 限制输入32个中文字符</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Push信息正文：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPushContent" runat="server" Width="500" MaxLength="32" /><font color="#AAAAAA"> * 限制输入32个中文字符</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                发送日期：
                            </td>
                            <td align="left">
                                <input id="dpSendStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpSendEnd\')||\'2020-10-01\'}'})" runat="server"/>
                                <input id="dpSendEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpSendStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();" onclick="btnSearch_Click" />
                                <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                    </table>
                </li>
              </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
        <ContentTemplate>
            <div class="frame01">
                <ul>
                <li class="title">Push任务列表</li>
                </ul>
            </div>
            <div class="frame02">
                <%--<div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>--%>
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSReviewList_RowDataBound" PageSize="15" AllowPaging="true" 
                    onpageindexchanging="gridViewCSReviewList_PageIndexChanging" CssClass="GView_BodyCSS">
                    <Columns>
                            <asp:BoundField DataField="TID" HeaderText="ID" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                            <asp:BoundField DataField="PTITLE" HeaderText="消息标题" ><ItemStyle HorizontalAlign="Center" Width="45%"/></asp:BoundField>
                            <asp:BoundField DataField="ALLNUM" HeaderText="总条数"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                            <asp:BoundField DataField="SUCNUM" HeaderText="成功发送条数"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                            <asp:BoundField DataField="SEDTIME" HeaderText="发送时间"><ItemStyle HorizontalAlign="Center" Width="17%" /></asp:BoundField>
                            <asp:HyperLinkField HeaderText="查看详情" DataNavigateUrlFields="TID" DataNavigateUrlFormatString="PushSearchDeatilPage.aspx?ID={0}" 
                        Target="_blank" DataTextField="MODIFY"><ItemStyle HorizontalAlign="Center" Width="8%" /></asp:HyperLinkField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
                <%--         <div style="margin-left:10px;">
                    <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                </div>--%>
            </div>
            <div id="background" class="pcbackground" style="display: none; "></div> 
            <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>