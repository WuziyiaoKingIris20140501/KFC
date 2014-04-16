<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewIssueManager.aspx.cs"  Title="Issue单管理" Inherits="ReviewIssueManager" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
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

        document.getElementById("<%=txtTitle.ClientID%>").value = ""; 
        document.getElementById("<%=txtRelatedID.ClientID%>").value = ""; 

        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";

        document.getElementById("<%=ddpStatusList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpAstoList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpRelated.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpIssueType.ClientID%>").selectedIndex = 0; 
    }

     function SetControlValue() {

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
        <li class="title">查看Issue单操作历史</li>
        <li>
            <table>
                 <tr>
                    <td align="right">
                        Title：
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="300px" MaxLength="150"/>
                    </td>
                    <td align="right">
                        状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                     <td align="right">
                        分类：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpIssueType" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Issue指派：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpAstoList" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                    <td align="right">
                        关联类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpRelated" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                    <td align="right">
                        总处理时长：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpActionTime" CssClass="noborder_inactive" runat="server" Width="150px"/>
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
                    <td align="right">
                        关联ID：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRelatedID" runat="server" Width="145px" MaxLength="30"/>
                    </td>
                </tr>
                 <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlValue();BtnLoadStyle();" onclick="btnSearch_Click" />
                        <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>
        <div class="frame02">
             <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSReviewList_RowDataBound" AllowSorting="true" OnSorting="gridViewCSReviewList_Sorting" PageSize="10"  CssClass="GView_BodyCSS">
                    <Columns>
                        <asp:BoundField DataField="IssueID" HeaderText="IssueID" ><ItemStyle HorizontalAlign="Center" Width="3%"/></asp:BoundField>
                        <asp:HyperLinkField HeaderText="标题" DataNavigateUrlFields="IssueID" DataNavigateUrlFormatString="SaveIssueManager.aspx?ID={0}" 
                        Target="_blank" DataTextField="Title"><ItemStyle HorizontalAlign="Center" Width="11%" /></asp:HyperLinkField>
                         <asp:BoundField DataField="Priority" HeaderText="优先级" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="IssueTypeNM" HeaderText="分类" ><ItemStyle HorizontalAlign="Center" Width="12%"/></asp:BoundField>
                         <asp:BoundField DataField="AssignUser" HeaderText="Issue指派" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                         <asp:BoundField DataField="DISStatus" HeaderText="状态" ><ItemStyle HorizontalAlign="Center" Width="4%" /></asp:BoundField>
                         <asp:BoundField DataField="RelatedTypeNM" HeaderText="关联类型" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="RelatedID" HeaderText="关联ID" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                         <asp:BoundField DataField="CreateTime" HeaderText="创建时间" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                         <asp:BoundField DataField="CreateUser" HeaderText="创建人" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="UpdateTime" HeaderText="最后修改时间" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                         <asp:BoundField DataField="UpdateUser" HeaderText="最后修改人" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="ActionTimes" HeaderText="总处理时长" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
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
<asp:HiddenField ID="hidCity" runat="server"/>
<asp:HiddenField ID="hidCommonList" runat="server"/>
</asp:Content>