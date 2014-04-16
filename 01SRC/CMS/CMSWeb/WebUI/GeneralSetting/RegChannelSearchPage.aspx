<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="RegChannelSearchPage.aspx.cs"  Title="渠道管理" Inherits="RegChannelSearchPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function ClearClickEvent(){ 
        document.getElementById("<%=txtRegChannelName.ClientID%>").value = "";
        document.getElementById("<%=txtRegChannelID.ClientID%>").value = "";
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加注册渠道</li>
        <li>注册渠道名称：
            <%--<asp:DropDownList ID="ddpChannelList" CssClass="noborder_inactive" runat="server">
                </asp:DropDownList> --%>
          <input name="textfield" type="text" id="txtRegChannelName" value="" runat="server" maxlength="32"/><font color="red">*</font>&nbsp;
            注册渠道代码：
          <input name="textfield" type="text" id="txtRegChannelID" value="" runat="server" maxlength="13"/><font color="red">*</font>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" onclick="btnAdd_Click" />
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
            <%--<img src="../../images/button.gif" runat="server" width="92" height="21" align="absmiddle" onclick="SaveClickEvent()" style="cursor:pointer;"/>--%>&nbsp;新建项目默认为下线状态</li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame01">
      <ul>
        <li class="title">搜索现有注册渠道</li>
        <li>注册渠道名称：
          <label for="textfield"></label>
          <input type="text" name="textfield" id="txtSelRegChannelName" runat="server" />
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

          <input type="checkbox" name="checkbox" id="chkUnTime" runat="server"/>
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
    
    <div class="frame02">
        <asp:GridView ID="gridViewCSRegChannelList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSRegChannelList_RowDataBound"  OnRowEditing="gridViewCSRegChannelList_RowEditing"
                            OnRowUpdating="gridViewCSRegChannelList_RowUpdating" OnRowCancelingEdit="gridViewCSRegChannelList_RowCancelingEdit" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"  />
                    <asp:BoundField DataField="REGCHANELID" HeaderText="REGCHANELID" Visible="False"  />
                    <asp:BoundField DataField="REGCHANELNM" HeaderText="注册渠道名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="REGCHANELCODE" HeaderText="注册渠道代码" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
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
    <div id="save"><%--<asp:Button ID="btnSave" runat="server" Width="80px" Text="保存" onclick="btnSave_Click" /><img src="../../images/button02.gif" width="92" height="21" />--%></div>   

</div>

</asp:Content>

