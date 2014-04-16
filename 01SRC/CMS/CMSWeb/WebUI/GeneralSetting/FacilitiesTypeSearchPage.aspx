<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="FacilitiesTypeSearchPage.aspx.cs"  Title="服务设施类别管理" Inherits="FacilitiesTypeSearchPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtFtName.ClientID%>").value = "";
        document.getElementById("<%=txtFtCode.ClientID%>").value = "";
        document.getElementById("<%=messageContent.ClientID%>").innerHTML = "";
    }
</script>
<script language="javascript" type="text/javascript">
    function PopupArea(arg) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("FacilitiesTypeDetail.aspx?ID=" + arg + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=400px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加服务设施类别</li>
        <li>服务设施类别名称：
          <input name="textfield" type="text" id="txtFtName" value="" runat="server" maxlength="12"/><font color="red">*</font>&nbsp;
            服务设施类别代码：
          <input name="textfield" type="text" id="txtFtCode" value="" runat="server" maxlength="12"/><font color="red">*</font>&nbsp;
            排序：
          <asp:Label ID="lbFtSeq" runat="server" Text="" />
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" onclick="btnAdd_Click" />
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
            &nbsp;新建项目默认为下线状态
            <div style="display:none">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="search" onclick="btnSearch_Click" />
            </div>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div><asp:HiddenField ID="refushFlag" runat="server"/>
        </li>
      </ul>
    </div>
    <div class="frame01">
      <ul>
        <li class="title">酒店服务设施类别管理</li>
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
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" PageSize="20"  AllowPaging="True" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="FTID" 
                            onrowdatabound="gridViewCSList_RowDataBound"  onpageindexchanging="gridViewCSList_PageIndexChanging" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="FTID" HeaderText="ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="FTNM" HeaderText="服务设施类别名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="FTCODE" HeaderText="服务设施类别代码" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <%--<asp:BoundField DataField="FTSEQ" HeaderText="排序" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                    <asp:BoundField DataField="ONLINEDIS" HeaderText="状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="排序">
                        <ItemTemplate>
                           <asp:TextBox ID="txtSeqList" runat="server" Text='<%# Eval("FTSEQ")%>' Width="30px" Enabled="false" MaxLength="3" Style="text-align:right"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UDTIME" HeaderText="最后修改时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="CDTIME" HeaderText="创建时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                      <ItemTemplate>
                      <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "FTID") %>')">编辑</a>
                      </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
          </asp:GridView>
          <br />
    </div>
</div>
</asp:Content>