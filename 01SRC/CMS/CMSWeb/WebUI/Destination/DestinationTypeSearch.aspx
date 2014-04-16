<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="DestinationTypeSearch.aspx.cs"  Title="目的地类型管理" Inherits="DestinationTypeSearch" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtDestinationTypeNM.ClientID%>").value = "";
        document.getElementById("<%=ddpTypeList.ClientID%>").selectedIndex = 0;
    }
</script>
<script language="javascript" type="text/javascript">
    function PopupArea(arg) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("DestinationTypeDetail.aspx?ID=" + arg + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=400px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加目的地类型</li>
        <li>目的的类型名称：
          <input name="textfield" type="text" id="txtDestinationTypeNM" value="" runat="server" maxlength="12"/><font color="red">*</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            上级类型：
          <asp:DropDownList ID="ddpTypeList" CssClass="noborder_inactive" runat="server"></asp:DropDownList> 
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" onclick="btnAdd_Click" />
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
            <%--<img src="../../images/button.gif" runat="server" width="92" height="21" align="absmiddle" onclick="SaveClickEvent()" style="cursor:pointer;"/>--%>&nbsp;新建项目默认为下线状态</li>
        <li><div id="messageContent" runat="server" style="color:red"></div><asp:HiddenField ID="refushFlag" runat="server"/>
        </li>
      </ul>
    </div>
    <div class="frame01">
      <ul>
        <li class="title">目的地类型管理</li>
    </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSList_RowDataBound"  OnRowEditing="gridViewCSList_RowEditing"
                            OnRowUpdating="gridViewCSList_RowUpdating" OnRowCancelingEdit="gridViewCSList_RowCancelingEdit" 
                            onpageindexchanging="gridViewCSList_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                   <asp:BoundField DataField="TYPENM" HeaderText="名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="PARENTSNM" HeaderText="上级类型" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="UDTIME" HeaderText="修改时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="ONLINEDIS" HeaderText="状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                      <ItemTemplate>
                      <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>')">编辑</a>
                      </ItemTemplate>
                  </asp:TemplateField>



                   <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="在线状态">
                        <ItemTemplate>
                           <asp:DropDownList ID="ddlOnline" runat="server" DataSource='<%# ddlOnlinebind()%>' DataValueField="ONLINESTATUS" DataTextField="ONLINEDIS" Enabled="false">
                                    </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:CommandField HeaderText="选择" ShowSelectButton="True" />--%>
                    <%--<asp:CommandField HeaderText="编辑" ShowEditButton="True"><ItemStyle HorizontalAlign="Center" /></asp:CommandField>--%>
                    <%--<asp:CommandField HeaderText="删除" ShowDeleteButton="True" />--%>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    <div id="save" style="display:none;">
    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" /></div>   
</div>
</asp:Content>