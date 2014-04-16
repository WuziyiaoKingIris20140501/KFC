<%@ Page Title="用户提现操作页面" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CashApplSearch.aspx.cs" Inherits="WebUI_CashBack_CashApplSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 
 <div class="frame01">
        <ul>
        <li class="title">提现申请搜索</li>
        <li>
            <table>
                <tr>
                    <td>手机号码：</td>
                    <td colspan=3><asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="11"/></td> 
                </tr>
                <tr>
                   <td>产生时间：</td>
                    <td><input id="dtStartCreateDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00'})" runat="server"/></td>
                    <td>至：<input id="dtEndCreateDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dtStartCreateDate\')}'})" runat="server"/><input type="checkbox" name="chkLimitCreateDate" id="chkLimitCreateDate" value="1" onclick="javascript:checkLimit('0');"/>不限制   </td>
                </tr>
                <tr>
                    <td>处理时间：</td>
                    <td><input id="dtStartProcessDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00'})" runat="server"/></td>
                    <td>至：<input id="dtEndProcessDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dtStartProcessDate\')}'})" runat="server"/><input type="checkbox" name="chkLimitProcessDate" id="chkLimitProcessDate" value="1" onclick="javascript:checkLimit('1');"/>不限制</td>                    
                </tr>
                <tr>
                    <td>申请提现方式：</td>
                    <td><asp:DropDownList ID="ddlAppMode" CssClass="noborder_inactive" runat="server" 
                            Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="1">现金返还</asp:ListItem>
                        <asp:ListItem Value="2">支付宝返还</asp:ListItem>
                        <asp:ListItem Value="3">手机充值</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <td>处理状态：
                        <asp:DropDownList ID="ddlProcessStatus" CssClass="noborder_inactive" 
                            runat="server" Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="0">已提交</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="2">已失败</asp:ListItem>
                        <asp:ListItem Value="3">已成功</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr> 
                 <tr>
                    <td><asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn primary" OnClientClick="return checkValid()"
                            onclick="btnSearch_Click" /></td>                     
                    <td><input type="button" value="重置" id="btnReset" class="btn" onclick="clearText();" />&nbsp;<asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/></td>
                </tr> 
            </table>
        </li>
    </ul>
</div>
 <div class="frame01">
        <ul>
        <li class="title">搜索结果列表</li>
        <li>
        <asp:GridView ID="gridViewCash"  runat="server" AutoGenerateColumns="False" BackColor="White"  AllowPaging="True" PageSize="20" CssClass="GView_BodyCSS" onpageindexchanging="gridViewCash_PageIndexChanging" >
            <Columns> 
                <asp:TemplateField HeaderText="申请ID">
                    <ItemTemplate>                                    
                      <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>       
                    </ItemTemplate>                                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="USER_ID" HeaderText="用户ID" >                      
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PICK_CASH_AMOUNT" HeaderText="提现申请金额" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="CASH_WAY" HeaderText="提现方式" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="APPLICATE_TIME" HeaderText="提现申请时间" >                                  
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="处理状态">
                    <ItemTemplate>                                    
                        <asp:Label ID="lblProcessStatus" runat="server" Text='<%#Eval("PROCESS_STATUS") %>'></asp:Label> 
                    </ItemTemplate>                                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="PROCESS_USERID" HeaderText="最终处理人" >                                    
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PROCESS_TIME" HeaderText="最终处理时间" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
            <PagerStyle HorizontalAlign="Right" />
            <RowStyle CssClass="GView_ItemCSS" />                        
            <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
        </asp:GridView>
        </li>
        </ul>
</div>
 <script language="javascript" type="text/javascript">
     function checkValid() {
         var phoneNumber = document.getElementById("<%=txtPhoneNumber.ClientID%>").value;
         var startCreateDate = document.getElementById("<%=dtStartCreateDate.ClientID%>").value;
         var endCreateDate = document.getElementById("<%=dtEndCreateDate.ClientID%>").value;
         var startProcessDate = document.getElementById("<%=dtStartProcessDate.ClientID%>").value;
         var endProcessDate = document.getElementById("<%=dtEndProcessDate.ClientID%>").value;

//         if ((phoneNumber != "") && (!phoneNumber.isMobile())) {
//             alert("您的手机号码格式输入不正确,必须为11位的有效号码！");
//             document.getElementById("<%=txtPhoneNumber.ClientID%>").focus();
//             return false;
//         }

//         if ((startCreateDate != "" && endCreateDate == "") || (startCreateDate == "" && endCreateDate != "")) {
//             alert("产生时间的开始时间和结束时间两个都必须输入或者两个都不输入！");
//             document.getElementById("<%=dtStartCreateDate.ClientID%>").focus();
//         }
//         if ((startCreateDate != "") && (endCreateDate != "") && (startCreateDate > endCreateDate)) {
//             alert("产生时间的开始时间必须小于结束时间！");
//             document.getElementById("<%=dtStartCreateDate.ClientID%>").focus();
//             return false;
//         }

//         if ((startProcessDate != "" && endProcessDate == "") || (startProcessDate == "" && endProcessDate != "")) {
//             alert("处理时间的开始时间和结束时间两个都必须输入或者两个都不输入！");
//             document.getElementById("<%=dtStartProcessDate.ClientID%>").focus();
//         }

//         if ((startProcessDate != "") && (endProcessDate != "") && (startProcessDate > endProcessDate)) {
//             alert("处理时间的开始时间必须小于结束时间！");
//             document.getElementById("<%=dtStartProcessDate.ClientID%>").focus();
//             return false;
//         }
         return true;
     }

     function clearText() {
         document.getElementById("<%=txtPhoneNumber.ClientID%>").value = "";
         document.getElementById("<%=dtStartCreateDate.ClientID%>").value = "";
         document.getElementById("<%=dtEndCreateDate.ClientID%>").value = "";
         document.getElementById("<%=dtStartProcessDate.ClientID%>").value = "";
         document.getElementById("<%=dtEndProcessDate.ClientID%>").value = "";
         document.getElementById("<%=ddlAppMode.ClientID%>").value = "";
         document.getElementById("<%=ddlProcessStatus.ClientID%>").value = "";
     }

     function checkLimit(v) {

         if (v == "0") {
             var objLimit = document.getElementById("chkLimitCreateDate");
             if (objLimit.checked == true) {
                 document.getElementById("<%=dtStartCreateDate.ClientID%>").disabled = true;
                 document.getElementById("<%=dtEndCreateDate.ClientID%>").disabled = true;
                 document.getElementById("<%=dtStartCreateDate.ClientID%>").value = "";
                 document.getElementById("<%=dtEndCreateDate.ClientID%>").value = "";
             }
             else {
                 document.getElementById("<%=dtStartCreateDate.ClientID%>").disabled = false;
                 document.getElementById("<%=dtEndCreateDate.ClientID%>").disabled = false;
             }

         }

         if (v == "1") {
             var objProcLimit = document.getElementById("chkLimitProcessDate");
             if (objProcLimit.checked == true) {
                 document.getElementById("<%=dtStartProcessDate.ClientID%>").disabled = true;
                 document.getElementById("<%=dtEndProcessDate.ClientID%>").disabled = true;

                 document.getElementById("<%=dtStartProcessDate.ClientID%>").value = "";
                 document.getElementById("<%=dtEndProcessDate.ClientID%>").value = "";
             }
             else {
                 document.getElementById("<%=dtStartProcessDate.ClientID%>").disabled = false;
                 document.getElementById("<%=dtEndProcessDate.ClientID%>").disabled = false;
             }
         }
     }

</script>

</asp:Content>

