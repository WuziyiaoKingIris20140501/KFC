<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="PlatformSearchPage.aspx.cs"  Title="渠道管理" Inherits="PlatformSearchPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtPlatformName.ClientID%>").value = "";
        document.getElementById("<%=txtPlatformID.ClientID%>").value = "";
        document.getElementById("<%=messageContent.ClientID%>").innerText = "";
    }

    function SetchkRegistUnTime() {
        if (document.getElementById("<%=chkUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpStart.ClientID%>").value = "";
            document.getElementById("<%=dpEnd.ClientID%>").value = "";

            document.getElementById("<%=dpStart.ClientID%>").disabled = true;
            document.getElementById("<%=dpEnd.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpStart.ClientID%>").disabled = false;
            document.getElementById("<%=dpEnd.ClientID%>").disabled = false;
        }
    }
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div id="right">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">添加应用平台</li>
        <li>应用平台名称：
            <%--<asp:DropDownList ID="ddpChannelList" CssClass="noborder_inactive" runat="server">
                </asp:DropDownList> --%>
          <input name="textfield" type="text" id="txtPlatformName" value="" runat="server" maxlength="32"/><font color="red">*</font>&nbsp;
            应用平台代码：
          <input name="textfield" type="text" id="txtPlatformID" value="" runat="server" maxlength="13"/><font color="red">*</font>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" onclick="btnAdd_Click" />
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
            <%--<img src="../../images/button.gif" runat="server" width="92" height="21" align="absmiddle" onclick="SaveClickEvent()" style="cursor:pointer;"/>--%>&nbsp;新建项目默认为下线状态</li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    </ContentTemplate>
   </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">搜索现有应用平台</li>
        <li>应用平台名称：
          <label for="textfield"></label>
          <input type="text" name="textfield" id="txtSelPlatformName" runat="server" />
        </li>
        <li>创建日期：
          <%--<select name="" size="1" >
            <option>2011－11－07 至 2011－11－14</option>
          </select>--%>
         <%-- <input id="dpStart" class="Wdate" type="text" onfocus ="WdatePicker({maxDate:'#F{$dp.$D(\'dpEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpEnd" class="Wdate" type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'dpStart\')}',maxDate:'2020-10-01'})" runat="server"/>--%>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input id="dpStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpStart\')}',maxDate:'2020-10-01'})" runat="server"/>

          <input type="checkbox" name="checkbox" id="chkUnTime" runat="server" onclick="SetchkRegistUnTime()"/>
          不限制
          <label for="checkbox"></label>
        </li>
        <li>在线状态：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rdbAll" GroupName="rdbOnline" runat="server" Text="不限制" Checked="true" />
            <asp:RadioButton ID="rdbOnL" GroupName="rdbOnline" runat="server" Text="上线"/>
            <asp:RadioButton ID="rdbOff" GroupName="rdbOnline" runat="server" Text="下线"/>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" />
        </li>
        <%--<li class="button"><img src="../../images/button01.gif" runat="server" width="92" height="21" align="absmiddle" style="cursor:pointer;"/></li>--%>
      </ul>
    </div>
    </ContentTemplate>
   </asp:UpdatePanel>
   <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
      <ContentTemplate>
    <div class="frame02">
        <asp:GridView ID="gridViewCSPlatformList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSPlatformList_RowDataBound"  OnRowEditing="gridViewCSPlatformList_RowEditing"
                            OnRowUpdating="gridViewCSPlatformList_RowUpdating" OnRowCancelingEdit="gridViewCSPlatformList_RowCancelingEdit" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"  />
                    <asp:BoundField DataField="PLATFORMID" HeaderText="PLATFORMID" Visible="False"  />
                    <asp:BoundField DataField="PLATFORMNM" HeaderText="应用平台名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="PLATFORMCODE" HeaderText="应用平台代码" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <%--<asp:BoundField DataField="ONLINEDIS" HeaderText="在线状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                    <asp:BoundField DataField="ONLINESTATUS" HeaderText="在线CODE" Visible="False" />
                    <asp:BoundField DataField="CDTIME" HeaderText="创建时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="UDTIME" HeaderText="编辑时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="在线状态">
                        <ItemTemplate>
                           <asp:DropDownList ID="ddlOnline" runat="server" DataSource='<%# ddlOnlinebind()%>' DataValueField="ONLINESTATUS" DataTextField="ONLINEDIS" Enabled="false">
                                    </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CommandField HeaderText="选择" ShowSelectButton="True" />--%>
                    <asp:CommandField HeaderText="编辑" ShowEditButton="True"><ItemStyle HorizontalAlign="Center" /></asp:CommandField>
                    <%--<asp:CommandField HeaderText="删除" ShowDeleteButton="True" />--%>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    </ContentTemplate>
   </asp:UpdatePanel>
</div>
</asp:Content>