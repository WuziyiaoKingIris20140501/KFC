<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="PushInfoSearchDeatilPage.aspx.cs"  Title="Push消息管理" Inherits="PushInfoSearchDeatilPage" %>
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
width: 200px; 
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
        document.getElementById("progressBar").innerText = document.getElementById("<%=hidMsg.ClientID%>").value
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        ajaxbg.show();
    }

    function BtnCompleteStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        window.clearInterval(0);
    }

    function RemainTimeBtn() {
        BtnLoadStyle();
        var getTime = document.getElementById("<%=hidRemainSecond.ClientID%>").value;
        if (getTime <= 0) {
            BtnCompleteStyle();
            return;
        }
        var remSecond = getTime - 1;
        document.getElementById("<%=hidRemainSecond.ClientID%>").value = remSecond;
        if (remSecond == 0) {
            document.getElementById("<%=btnRefush.ClientID%>").click();
        }
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01">
              <ul>
                <li class="title">Push任务概述</li>
                <li>
                    <table>
                        <tr>
                            <td align="right">
                                Push任务标题：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPushTitle" runat="server" Width="500px" MaxLength="32" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Push任务类型：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbPushInfoType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                               <asp:Label ID="lbActionUrl" runat="server" Text = "WAP活动URL：" /> 
                            </td>
                            <td align="left">
                                <asp:Label ID="lbWapUrl" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Push信息正文：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPushContent" runat="server" Width="500" TextMode="MultiLine" Height="100" ReadOnly="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                发送目标：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbPushUsers" runat="server" /> <%--用户组-用户组ID-用户组名称   上传Excel列表--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                               Push发送状态：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbPustStatus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                               Push发送时间：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbPushAction" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                               Push发送量：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbPushCount" runat="server" /> <%--DeviceToken总数：XXX   成功发送：XXX    发送失败：XXX--%>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                
                            </td>
                            <td align="left">
                                <asp:HiddenField ID="hidRemainSecond" runat="server"/>
                                <asp:HiddenField ID="hidMsg" runat="server"/>
                                <asp:Button ID="btnSend" runat="server" CssClass="btn primary" Text="继续发送" OnClientClick="BtnLoadStyle();" onclick="btnSend_Click" />
                                <div id="dvRefush" style="display:none;"><asp:Button ID="btnRefush" runat="server" CssClass="btn primary" Text="刷新" onclick="btnRefush_Click" /></div>
                            </td>
                        </tr>
                        <tr><td colspan="2"><asp:HiddenField ID="hidTaskID" runat="server"/><asp:HiddenField ID="hidPushType" runat="server"/><asp:HiddenField ID="hidPushAllNum" runat="server"/></td></tr>
                    </table>
                </li>
              </ul>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSend" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
        <ContentTemplate>
            <div class="frame01">
                <ul>
                    <li class="title">Push任务详情</li>
                    <li>
                        <table>
                            <tr>
                                <td align="right">
                                    UserID：
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserID" runat="server" Width="200" MaxLength="20" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();" onclick="btnSearch_Click" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出" onclick="btnExport_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4"></td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
            <div class="frame02">
            <div id="messageContent" runat="server" style="color:red"></div>
                <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSReviewList_RowDataBound" PageSize="15" CssClass="GView_BodyCSS">
                    <Columns>
                            <asp:BoundField DataField="UserID" HeaderText="UserID" ><ItemStyle HorizontalAlign="Center" Width="20%"/></asp:BoundField>
                            <asp:BoundField DataField="DeviceToken" HeaderText="DeviceToken" ><ItemStyle HorizontalAlign="Center" Width="45%"/></asp:BoundField>
                            <asp:BoundField DataField="Action_Time" HeaderText="发送时间"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                            <asp:BoundField DataField="Result" HeaderText="发送结果"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
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
            <div id="progressBar" class="pcprogressBar" style="display: none; "></div> 
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</div>
</asp:Content>