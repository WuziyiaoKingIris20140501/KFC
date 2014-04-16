<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelFacilitiesSearch.aspx.cs"  Title="酒店服务设施管理" Inherits="HotelFacilitiesSearch" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function PopupArea(arg, type) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("HotelFacilitiesDetail.aspx?ID=" + arg + "&TYPE=" + type + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=350px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }
    }

    function ClearClickEvent() {
        document.getElementById("<%=txtServiceName.ClientID%>").value = "";
        document.getElementById("<%=ddpFtTypeList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=MessageContent.ClientID%>").innerHTML = "";
        document.getElementById("<%=refushFlag.ClientID%>").value = "";
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加酒店服务设施</li>
        <li>服务名称：
          <input name="textfield" type="text" id="txtServiceName" value="" runat="server" maxlength="12"/><font color="red">*</font>&nbsp;
            服务设施类别：
          <asp:DropDownList ID="ddpFtTypeList" CssClass="noborder_inactive" runat="server" 
                Width="120px" onselectedindexchanged="ddpFtTypeList_SelectedIndexChanged" AutoPostBack="true"/>&nbsp;
            排序：
          <asp:Label ID="lbFtSeq" runat="server" Text="" />
        </li>
        
        <li class="button">
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" onclick="btnAdd_Click" />&nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" OnClientClick="ClearClickEvent()" onclick="btnClear_Click" />
        </li>
        <li><div id="MessageContent" runat="server" style="color:red;width:800px;"></div></li>
      </ul>
     </div>
     <div class="frame01">
      <ul>
        <li class="title">酒店服务设施管理</li>
        <li>
            服务设施类别：
          <asp:DropDownList ID="ddpSelFtTypeList" CssClass="noborder_inactive" runat="server" Width="120px"/>
          &nbsp;&nbsp;
          <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="筛选" onclick="btnSearch_Click" />
        </li>
        <li></li>
      </ul>
    </div>
    <div class="frame02">
        <div style="text-align:right">
            <%--<input type="button" id="btnModifySeq" style="width:80px;height:20px;" value="调整顺序"  onclick="SetSeqEvent()" />--%>
            <asp:Button ID="btnModifySeq" runat="server" CssClass="btn primary" Text="调整顺序" onclick="btnModifySeq_Click" />
            <div id="dvUpdateSeq" style="display:none" runat="server">
                <asp:Button ID="btnUpdateSeq" runat="server" CssClass="btn primary" Text="保存" onclick="btnUpdateSeq_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnUpdateCal" runat="server" CssClass="btn" Text="取消" onclick="btnUpdateCal_Click" />
            </div>
        </div>
          <asp:GridView ID="gridViewCSServiceList" runat="server" 
            AutoGenerateColumns="False" BackColor="White" CellPadding="4" CellSpacing="1"
                    Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSServiceList_RowDataBound"
            CssClass="GView_BodyCSS">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                <asp:BoundField DataField="FACILITIESNM" HeaderText="名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                <asp:BoundField DataField="FTTYPENM" HeaderText="类别" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                <asp:BoundField DataField="ONLINEDIS" HeaderText="状态"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="排序">
                        <ItemTemplate>
                           <asp:TextBox ID="txtSeqList" runat="server" Text='<%# Eval("FTSEQ")%>' Width="30px" Enabled="false" MaxLength="3" Style="text-align:right"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="CDTIME" HeaderText="创建时间" ReadOnly="True">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                    <ItemTemplate>
                        <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>', '0')">编辑</a>
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
    <asp:HiddenField ID="refushFlag" runat="server"/>
   </div>
</asp:Content>