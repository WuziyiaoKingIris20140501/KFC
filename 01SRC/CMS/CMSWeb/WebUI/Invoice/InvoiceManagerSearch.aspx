<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="InvoiceManagerSearch.aspx.cs"  Title="发票管理" Inherits="InvoiceManagerSearch" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtUSERID.ClientID%>").value = "";
        document.getElementById("<%=txtCNFNUM.ClientID%>").value = "";
        document.getElementById("<%=txtSENDCODE.ClientID%>").value = "";


        document.getElementById("<%=ddpSTATUS.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpAPPLYCHANEL.ClientID%>").selectedIndex = 0;


        document.getElementById("<%=dpApplyBeginDate.ClientID%>").value = "";
        document.getElementById("<%=dpApplyEndDate.ClientID%>").value = "";
        document.getElementById("<%=dpSendBeginDate.ClientID%>").value = "";
        document.getElementById("<%=dpSendEndDate.ClientID%>").value = "";
    }
    function PopupArea(arg) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("InvoiceManagerDetail.aspx?ID=" + arg + "&time=" + time, obj, "dialogWidth=900px;dialogHeight=450px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">发票管理</li>
        <li>
            <table>
        <tr>
            <td align="right">
                注册手机号：
            </td>
            <td style="width: 330px">
                <input name="textfield" type="text" id="txtUSERID" value="" style="width:210px;" runat="server" maxlength="40"/>
            </td>
            <td align="right" >
                订单号：
            </td>
            <td style="width: 330px">
                <input name="textfield" type="text" id="txtCNFNUM" value="" style="width:210px;" runat="server" maxlength="40"/>
            </td>
            <td align="right">
                邮寄单号：
            </td>
            <td>
                <input name="textfield" type="text" id="txtSENDCODE" value="" style="width:210px;" runat="server" maxlength="40"/>
            </td>
        </tr>
        <tr>
            <td align="right">
                发票状态：
            </td>
            <td>
                <asp:DropDownList ID="ddpSTATUS" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
            </td>
            <td align="right">
                申请渠道：
            </td>
            <td>
                <asp:DropDownList ID="ddpAPPLYCHANEL" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td align="right">
                 申请日期：
            </td>
            <td>
                <input id="dpApplyBeginDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpApplyEndDate\')||\'2020-10-01\'}'})" runat="server"/> 
                <input id="dpApplyEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpApplyBeginDate\')}',maxDate:'2020-10-01'})" runat="server"/>
            </td>
           <td align="right">
                 邮寄日期：
            </td>
            <td>
                <input id="dpSendBeginDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpSendEndDate\')||\'2020-10-01\'}'})" runat="server"/> 
                <input id="dpSendEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpSendBeginDate\')}',maxDate:'2020-10-01'})" runat="server"/>
            </td>
        </tr>
      </table>
        </li>
       <li class="button">
            &nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click"/>
            &nbsp;
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
             &nbsp;
            <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出" onclick="btnExport_Click"/>
        </li>
        <li>
            <div id="messageContent" runat="server" style="color:red"></div>
            <asp:HiddenField ID="refushFlag" runat="server"/>
        </li>
    </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSList_RowDataBound" onpageindexchanging="gridViewCSList_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false"><ItemStyle HorizontalAlign="Center"/></asp:BoundField>

                    <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="CNFNUM" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID={0}" 
                    Target="_blank" DataTextField="CNFNUM"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                    
                    <%--<asp:BoundField DataField="CNFNUM" HeaderText="订单号" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>

                    <asp:HyperLinkField HeaderText="注册手机号" DataNavigateUrlFields="URID" DataNavigateUrlFormatString="~/WebUI/UserGroup/UserDetailPage.aspx?ID={0}" 
                    Target="_blank" DataTextField="USERID"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>

                    <asp:BoundField DataField="RECEIVERNAME" HeaderText="收件人姓名"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <%--<asp:BoundField DataField="USERID" HeaderText="注册手机号"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                    <asp:BoundField DataField="APPLYCHANELNM" HeaderText="申请渠道"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="APPLYTIME" HeaderText="申请时间"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="INVOICEAMOUNT" HeaderText="发票金额" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="INVOICENUM" HeaderText="发票号" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="SENDNAME" HeaderText="邮寄方名称"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="SENDCODE" HeaderText="邮寄方单号"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="SENDTIME" HeaderText="确认邮寄时间"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="ONLINEDIS" HeaderText="状态"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="OPERATOR" HeaderText="操作人"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
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
</div>
<asp:HiddenField ID="hidType" runat="server"/>
</asp:Content>