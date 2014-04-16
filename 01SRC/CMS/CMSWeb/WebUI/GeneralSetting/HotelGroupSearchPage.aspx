<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelGroupSearchPage.aspx.cs"  Title="酒店集团管理" Inherits="HotelGroupSearchPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function PopupArea(arg, type) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("HotelGroupDetail.aspx?ID=" + arg + "&TYPE=" + type + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=500px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=hiddenType.ClientID%>").value = type;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">酒店集团管理</li>
        <li style="text-align:right">
            <input type="button" id="btnAdd" class="btn primary" value="添加酒店集团"  onclick="PopupArea('', '1')" class="button" />
        </li>
        <li>
            <div id="messageContent" runat="server" style="color:red"></div><asp:HiddenField ID="refushFlag" runat="server"/><asp:HiddenField ID="hiddenType" runat="server"/>
        </li>
        <li>
            <div style="display:none"><asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" /></div>
        </li>
      </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSHGroupList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1"  PageSize="15" AllowPaging="True"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSHGroupList_RowDataBound"  OnRowEditing="gridViewCSHGroupList_RowEditing"
                            OnRowUpdating="gridViewCSHGroupList_RowUpdating" OnRowCancelingEdit="gridViewCSHGroupList_RowCancelingEdit" onpageindexchanging="gridViewCSHGroupList_PageIndexChanging" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="GROUPCODE" HeaderText="代码"><ItemStyle HorizontalAlign="Center" Width="5%" /></asp:BoundField>
                    <asp:BoundField DataField="GROUPNM" HeaderText="酒店集团名称" ><ItemStyle HorizontalAlign="Center" Width="20%" /></asp:BoundField>
                    <asp:BoundField DataField="GROUPDESC" HeaderText="酒店集团描述" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="50%"/></asp:BoundField>
                    <asp:BoundField DataField="GROUPTYPE" HeaderText="酒店集团类型" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:BoundField>
                    <asp:BoundField DataField="ONLINEDIS" HeaderText="在线状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="5%" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                      <ItemTemplate>
                      <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>', '0')">编辑</a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                  </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    <div id="save"></div>   
</div>
</asp:Content>