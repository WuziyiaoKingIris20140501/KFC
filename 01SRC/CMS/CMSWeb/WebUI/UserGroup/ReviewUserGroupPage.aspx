<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewUserGroupPage.aspx.cs"  Title="查看用户组" Inherits="ReviewUserGroupPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtUserGroupNM.ClientID%>").value = "";
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
        document.getElementById("<%=txtUserCountFrom.ClientID%>").value = "";
        document.getElementById("<%=txtUserCountTo.ClientID%>").value = "";

        document.getElementById("<%=chkCreateUnTime.ClientID%>").checked = true;
        document.getElementById("<%=chkUserCountUn.ClientID%>").checked = true;

        document.getElementById("<%=dpCreateStart.ClientID%>").disabled = true;
        document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = true;

        document.getElementById("<%=txtUserCountFrom.ClientID%>").disabled = true;
        document.getElementById("<%=txtUserCountTo.ClientID%>").disabled = true;
    }

    function SetchkCreateUnTime() {
        if (document.getElementById("<%=chkCreateUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
            document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";

            document.getElementById("<%=dpCreateStart.ClientID%>").disabled = true;
            document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpCreateStart.ClientID%>").disabled = false;
            document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = false;
        }
    }

    function SetchkUserCountUn() {
        if (document.getElementById("<%=chkUserCountUn.ClientID%>").checked == true) {
            document.getElementById("<%=txtUserCountFrom.ClientID%>").value = "";
            document.getElementById("<%=txtUserCountTo.ClientID%>").value = "";

            document.getElementById("<%=txtUserCountFrom.ClientID%>").disabled = true;
            document.getElementById("<%=txtUserCountTo.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=txtUserCountFrom.ClientID%>").disabled = false;
            document.getElementById("<%=txtUserCountTo.ClientID%>").disabled = false;
        }
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
        var retunValue = window.open("UserGroupDetailPage.aspx?ID=" + arg + "&time=" + time, null, fulls);
        //window.location.href = "ReviewUserGroupDetailPage.aspx?ID=" + arg + "&time=" + time;
    }
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div id="right">
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">查看用户组</li>
        <li>用户组名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input name="textfield" type="text" id="txtUserGroupNM" runat="server" style="width:450px;" maxlength="32" value=""/>
            
        </li>
        <li>创建时间：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
          <input type="checkbox" name="checkbox" id="chkCreateUnTime" runat="server" onclick="SetchkCreateUnTime()"/>
          不限制
        </li>
        
        <li>用户数量：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input name="textfield" type="text" id="txtUserCountFrom" value="" runat="server" maxlength="4" style="width:40px;"/>

            到
            <input name="textfield" type="text" id="txtUserCountTo" value="" runat="server" maxlength="4" style="width:40px;"/>
            <input type="checkbox" name="checkbox" id="chkUserCountUn" runat="server" onclick="SetchkUserCountUn()"/>
              不限制
            <label for="checkbox"></label>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" />

            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
        </li>
      </ul>
    </div>
      </ContentTemplate>
   </asp:UpdatePanel>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>
    <div class="frame01"><ul><li><div id="messageContent" runat="server" style="color:red;"></div></li></ul></div>
    <div class="frame02">
       <%-- <div id="result"></div>--%>
            
        <asp:GridView ID="gridViewCSReviewUserGroupList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSReviewUserGroupList_RowDataBound"  OnRowEditing="gridViewCSReviewUserGroupList_RowEditing"
                            OnRowUpdating="gridViewCSReviewUserGroupList_RowUpdating" OnRowCancelingEdit="gridViewCSReviewUserGroupList_RowCancelingEdit" 
                            onpageindexchanging="gridViewCSReviewUserGroupList_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                     <asp:BoundField DataField="ID" HeaderText="用户组ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                     <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="用户组名">
                        <ItemTemplate>
                           <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>')"><%# DataBinder.Eval(Container.DataItem, "USERGROUPNAME")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:HyperLinkField HeaderText="用户组名" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="UserGroupDetailPage.aspx?ID={0}" 
                    Target="_blank" DataTextField="USERGROUPNAME"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>

                     <asp:BoundField DataField="CREATETIME" HeaderText="创建时间" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="USERCOUNT" HeaderText="用户数" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="REGCHANELNM" HeaderText="注册渠道" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="SUBMITORDER" HeaderText="历史订单" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
            </asp:GridView>
    </div>
   </ContentTemplate>
   </asp:UpdatePanel>
</div>
</asp:Content>