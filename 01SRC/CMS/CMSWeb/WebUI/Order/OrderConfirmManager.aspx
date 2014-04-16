<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="OrderConfirmManager.aspx.cs"  Title="订单审核管理" Inherits="OrderConfirmManager" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<%@ Register src="../../UserControls/AutoCptControl.ascx" tagname="WebAutoComplete" tagprefix="ac1" %>

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

.bgDivList
{
    display: none;   
    position:absolute;
    top: 0px;
    left: 0px;
    right:0px;
    background-color: #777;
    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);
    opacity: 0.6;
}

.popupDivList
{
    width: 600px;
    height:400px;
    margin-left:150px;
    position:absolute;
    padding: 1px;
    vertical-align: middle;
    text-align:center;
    border: solid 2px #ff8300;
    z-index: 10001;
    display: none;   
    background-color:White;
}
</style>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>

<script language="javascript" type="text/javascript">
    function SetWebAutoControl() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("WebAutoComplete").value;
        document.getElementById("<%=hidCityID.ClientID%>").value = document.getElementById("wctCity").value;
    }

    function setOpenValue(returnValue) {
        document.getElementById("<%=hidOrderID.ClientID%>").value = returnValue;
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

     function ClearDateControl() {
         if (document.getElementById("<%=txtOrderID.ClientID%>").value.trim() != "") {
             document.getElementById("<%=dpLeaveStart.ClientID%>").value = "";
             document.getElementById("<%=dpLeaveEnd.ClientID%>").value = "";
             document.getElementById("<%=ddpFogAuditstatus.ClientID%>").selectedIndex = 4;
         }
     }

     function OpenIssuePage(arg) {
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
         window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?BType=8&RID=' + arg + "&time=" + time, null, fulls);
     }

     //显示弹出的层
     function invokeOpenlist() {
         document.getElementById("popupDiv").style.display = "block";
         //背景
         var bgObj = document.getElementById("bgDiv");
         bgObj.style.display = "block";
         bgObj.style.width = document.body.offsetWidth + "px";
         bgObj.style.height = document.body.offsetHeight + "px";
     }

     //隐藏弹出的层
     function invokeCloselist() {
         document.getElementById("popupDiv").style.display = "none";
         document.getElementById("bgDiv").style.display = "none";
     }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>

    <div id="bgDiv" class="bgDivList"></div>
    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
    <ContentTemplate>
    <div id="popupDiv" class="popupDivList">
    <br />
          <div class="frame01">
            <ul>
                <li class="title" style="text-align:left">订单审核备注</li>
                <li>
                <table width="90%">
                        <tr id="trAction" runat="server">
                            <td align="left">
                                酒店确认号：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtActionID" runat="server" Width="120px" MaxLength="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                备注：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBOOK_REMARK" runat="server" TextMode="MultiLine" Width="300px"
                                    Height="50px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                备注历史：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbMemo1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <div id="detailMessageContent" runat="server" style="color: red">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" >
                                <br />
                                <asp:Button ID="btnAddRemark" CssClass="btn primary" runat="server" Text="保存" OnClick="btnAddRemark_Click" />&nbsp;
                                <input type="button" id="btnCancelRoom" class="btn" value="取消"  onclick="invokeCloselist();" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </li>
            </ul>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
  <div id="right">
    <div class="frame01">
      <ul>
          <li class="title">订单审核</li>
          <li>
           <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        城市：
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server" Width="100px" />--%>
                        <ac1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" CTLLEN="145px" AutoType="city" AutoParent="OrderConfirmManager.aspx?Type=city" />
                    </td>
                    <td align="right">
                        排序：
                    </td>
                    <td>
                         <asp:DropDownList ID="ddpSort" CssClass="noborder_inactive" runat="server" Width="100px" />
                    </td>
                    <td align="right">
                        订单审核状态：
                    </td>
                    <td>
                         <asp:DropDownList ID="ddpFogAuditstatus" CssClass="noborder_inactive" runat="server" Width="100px" />
                    </td>
                    <td align="right">
                        离店时间：
                    </td>
                    <td colspan="3">
                        <input id="dpLeaveStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpLeaveEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpLeaveEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpLeaveStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td><br /></td>
                </tr>
                <%--<tr>
                    <td align="right">
                        操作状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpControlStatus" CssClass="noborder_inactive" runat="server" Width="100px" />
                    </td>
                    <td align="right">
                        传真发送：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpFaxStatus" CssClass="noborder_inactive" runat="server" Width="100px" />
                    </td>
                    <td align="right">
                        确认状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpHotelConfirm" CssClass="noborder_inactive" runat="server" Width="100px" />
                    </td>
                    <td align="right">
                        订单状态：
                    </td>
                    <td>
                        <asp:CheckBoxList ID="chklBStatusOther" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                    </td>
                </tr>--%>
              </table>
              <table width="95%">
                <tr>
                    <td align="right">
                        酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="OrderConfirmManager.aspx?Type=hotel" />
                    </td>
                    <td align="right">
                        用户：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server" Width="150px" MaxLength="100" />
                    </td>
                    <td align="right">
                        订单：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderID" runat="server" Width="150px" MaxLength="100" onkeyup="ClearDateControl()" onkeypress="ClearDateControl()" onkeydown="ClearDateControl()"/>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索订单" OnClientClick="SetWebAutoControl();BtnLoadStyle();" onclick="btnSearch_Click" />
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         <asp:Button ID="btnRefresh" runat="server" CssClass="btn primary" Text="刷新列表" OnClientClick="BtnLoadStyle();" onclick="btnRefresh_Click" />
                         </ContentTemplate>
                         </asp:UpdatePanel>
                         
                    </td>
                    <%--<td>
                        <div style="display:none">
                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         <asp:Button ID="btnUnlock" runat="server" Width="80px" Height="20px" Text="解锁" CssClass="button" onclick="btnUnlock_Click" />
                         </ContentTemplate>
                         </asp:UpdatePanel>
                         </div>
                    </td>--%>
                </tr>
                <tr>
                    <td><br /></td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
          </li>
          <li>
             <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <div id="dvErrorInfo" runat="server" style="color: red"/>
                </ContentTemplate>
             </asp:UpdatePanel>
          </li>
      </ul>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
    <ContentTemplate>
    <asp:HiddenField ID="hidCityID" runat="server"/>
    <asp:HiddenField ID="hidHotelID" runat="server"/>
    <asp:HiddenField ID="hidOrderID" runat="server"/>
    <asp:HiddenField ID="hidLogKey" runat="server"/>
    <asp:HiddenField ID="hidActionType" runat="server"/>
    <div class="frame02">
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" AllowPaging="true" PageSize="50" 
                            Width="100%" EmptyDataText="没有数据" CssClass="GView_BodyCSS" onpageindexchanging="gridViewCSList_PageIndexChanging" 
                             onrowcommand="gridViewCSList_RowCommand">
                <Columns>
                    <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="订单号">
                      <ItemTemplate >
                        <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>')"><%# DataBinder.Eval(Container.DataItem, "ORDERID") %></a>
                      </ItemTemplate>
                    </asp:TemplateField>
                   <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="ORDERID" DataNavigateUrlFormatString="OrderOperation.aspx?ID={0}" 
                     Target="_blank" DataTextField="ORDERID"><ItemStyle HorizontalAlign="Center"  Width="5%" /></asp:HyperLinkField>--%>
                     <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="ORDERID" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID={0}" 
                    Target="_blank" DataTextField="ORDERID"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:HyperLinkField>

                     <%--<asp:BoundField DataField="ORDERID" HeaderText="订单号" ><ItemStyle HorizontalAlign="Center"  Width="5%"/></asp:BoundField>--%>
                     <asp:BoundField DataField="ORDERST" HeaderText="审核状态" ><ItemStyle HorizontalAlign="Center"  Width="6%"/></asp:BoundField>
                     <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center"  Width="15%" /></asp:BoundField>
                     <asp:BoundField DataField="AUDITPERTEL" HeaderText="审核联系人" ><ItemStyle HorizontalAlign="Center" Width="7%" /></asp:BoundField>
                     <%--<asp:BoundField DataField="AUDITTEL" HeaderText="审核电话"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>--%>
                     <asp:BoundField DataField="GTNAME" HeaderText="入住人姓名" ><ItemStyle HorizontalAlign="Center"  Width="7%"/></asp:BoundField>
                     <asp:BoundField DataField="INOUTDT" HeaderText="入住 - 离店" ><ItemStyle HorizontalAlign="Center"  Width="8%" /></asp:BoundField>
                     <asp:BoundField DataField="HCONNU" HeaderText="酒店确认号" ><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:BoundField>
                     <asp:BoundField DataField="HVPNM" HeaderText="HVP负责人"><ItemStyle HorizontalAlign="Center" Width="15%"/></asp:BoundField>
                     <asp:TemplateField HeaderText="审核操作" ItemStyle-Width="25%">
                     <ItemTemplate>
                        <asp:Button ID="lkLeave" CssClass="btn primary" runat="server" Text="离店" CommandName="leave" CommandArgument='<%#String.Format("{0}_{1}",Eval("ORDERID"),Eval("HCONNU")) %>'/>
                        <asp:Button ID="lkNoshow" CssClass="btn primary" runat="server" Text="No-Show" CommandName="noshow" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>'/>
                        <%--<asp:Button ID="lkQuest"  runat="server" Text="问题单" ForeColor="Red" CommandName="quest" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>'/>--%>
                        <input type="button" class="btn primary" id="btnIssue" value='问题单' onclick="OpenIssuePage('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>')" />
                        <asp:Button ID="lkRemark" CssClass="btn primary" runat="server" Text="备注" CommandName="remark" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>'/>
                     </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
          </asp:GridView>
    </div>
     <div id="background" class="pcbackground" style="display: none; "></div> 
     <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>