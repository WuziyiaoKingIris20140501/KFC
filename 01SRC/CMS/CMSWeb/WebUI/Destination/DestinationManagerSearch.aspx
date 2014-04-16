<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="DestinationManagerSearch.aspx.cs"  Title="目的地管理" Inherits="DestinationManagerSearch" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtDestinationName.ClientID%>").value = "";
        document.getElementById("<%=ddpCityList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpTypeList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=txtAddress.ClientID%>").value = ""; 
        document.getElementById("<%=txtTelST.ClientID%>").value = "";
        document.getElementById("<%=txtTelLG.ClientID%>").value = "";
        document.getElementById("<%=txtLatitude.ClientID%>").value = "";
        document.getElementById("<%=txtLongitude.ClientID%>").value = "";

        document.getElementById("wctCity").value = "";
        document.getElementById("wctCity").text = "";
    }
</script>
<script language="javascript" type="text/javascript">
    function PopupArea(arg) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("DestinationManagerDetail.aspx?ID=" + arg + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=400px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=btnRefesh.ClientID%>").click();
        }
    }

    function SetControlValue() {
        document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
    }

    function SetSelControlValue() {
        document.getElementById("<%=hidSelCity.ClientID%>").value = document.getElementById("wctSelCity").value;
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加目的地</li>
      </ul>
      <ul>
        
        <li>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>
            <table>
                <tr>
                    <td align="right">
                        目的地名称：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtDestinationName" value="" style="width:210px;" runat="server" maxlength="12"/><font color="red">*</font>
                    </td>
                    <td align="right" style="width:60px;">
                        城市：
                    </td>
                    <td>
                        <div style="display:none;"><asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server"></asp:DropDownList></div>
                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="DestinationManagerSearch.aspx?Type=city" />
                    </td>
                    <td align="right"  style="width:60px;">
                        类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpTypeList" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        地址：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtAddress" value="" style="width:210px;" runat="server" maxlength="32"/>
                    </td>
                    <td align="right">
                        电话：
                    </td>
                    <td colspan="2">
                        <input name="textfield" type="text" id="txtTelST" value="" style="width:40px;" runat="server" maxlength="4"/>
                        <input name="textfield" type="text" id="txtTelLG" value="" style="width:150px;" runat="server" maxlength="12"/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                         经度：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtLongitude" value="" style="width:150px;" runat="server" maxlength="12"/><font color="red">*</font>
                    </td>
                    <td align="right">
                         纬度：
                   </td>
                    <td colspan="3">
                         <input name="textfield" type="text" id="txtLatitude" value="" style="width:150px;" runat="server" maxlength="12"/><font color="red">*</font>
                    </td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                 <ContentTemplate>
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" OnClientClick="SetControlValue()" onclick="btnAdd_Click" />
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
            &nbsp;<font color="#AAAAAA">电话号码可为空  新建项目默认为下线状态</font>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                 <ContentTemplate>
            <div id="messageContent" runat="server" style="color:red"></div>
            <asp:HiddenField ID="refushFlag" runat="server"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
      </ul>
    </div>
     <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                 <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">目的地管理</li>
        <li>
            <table>
                <tr>
                    <td align="right">
                        城市：
                    </td>
                    <td>
                        <div style="display:none"><asp:DropDownList ID="ddpSelCity" CssClass="noborder_inactive" runat="server"></asp:DropDownList></div>
                        <uc1:WebAutoComplete ID="wctSelCity" CTLID="wctSelCity" runat="server" AutoType="city" AutoParent="DestinationManagerSearch.aspx?Type=city" />
                    </td>
                    <td align="right" style="width:60px;">
                        类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpSelType" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
                    </td>
                    <td align="right" style="width:60px;">
                        状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
                    </td>
                    <td align="right" style="width:100px;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetSelControlValue()" onclick="btnSearch_Click"/>
                    </td>
                </tr>
            </table>
        </li>
        <li></li>
    </ul>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                 <ContentTemplate>
    <div class="frame02">
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSList_RowDataBound"  OnRowEditing="gridViewCSList_RowEditing"
                            OnRowUpdating="gridViewCSList_RowUpdating" OnRowCancelingEdit="gridViewCSList_RowCancelingEdit" 
                            onpageindexchanging="gridViewCSList_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                   <asp:BoundField DataField="DESTINATIONNM" HeaderText="名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="CITYNM" HeaderText="城市" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="TYPENM" HeaderText="类型" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="TELEPHONE" HeaderText="电话" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="ADDRESSNM" HeaderText="地址" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="LONGITUDE" HeaderText="经度" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="LATITUDE" HeaderText="纬度" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="ONLINEDIS" HeaderText="状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="UDTIME" HeaderText="修改时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                      <ItemTemplate>
                      <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>')">编辑</a>
                      </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    <asp:HiddenField ID="hidSelCity" runat="server"/>
    <asp:HiddenField ID="hidCity" runat="server"/>
   <div id="save" style="display:none"><asp:Button ID="btnRefesh" runat="server" CssClass="btn primary" Text="搜索" onclick="btnRefesh_Click"/></div> 
   </ContentTemplate>
            </asp:UpdatePanel>
</div>
</asp:Content>