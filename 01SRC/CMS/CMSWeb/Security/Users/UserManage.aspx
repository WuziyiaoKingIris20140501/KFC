<%@ Page Title="用户管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="Security_Users_UserManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="right">
    <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="用户管理" ID="lbAdviceTitle" runat="server"></asp:Literal> </li>
        <li>
            <table cellpadding="0" cellspacing="0" align="center" width="98%">
                <tr>
                    <td align="left">
                       <div style=" margin:5px 0 5px 5px">
                        <asp:TextBox ID="sch_cond" runat="server" AutoCompleteType="disabled"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn primary" runat="server" Text="查询" onclick="btnSearch_Click" />
                        <asp:Button ID="btnAdd" CssClass="btn primary" runat="server"  UseSubmitBehavior="False" Text="新增" onclick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" CssClass="btn primary" runat="server"  Text="删除" onclick="btnDelete_Click"  Visible="false"/>  
                        </div>
                    </td>
                </tr>   
                   
                <tr>
                    <td  align="center">
                        <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            PageSize="15" OnRowDataBound="UserGridView_RowDataBound" Width="100%" CssClass="GView_BodyCSS">                       
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="HeadCheckBox" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkitem" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle  Width="5px" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:BoundField DataField="User_Account" HeaderText="用户帐号">
                                    <HeaderStyle Width="15%"/>
                                    <ItemStyle HorizontalAlign="Center"  Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="User_DspName" HeaderText="显示名称">
                                    <HeaderStyle  Width="15%"/>
                                    <ItemStyle HorizontalAlign="Center"  Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="User_HRID" HeaderText="员工号">
                                    <HeaderStyle Width="10%"/>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="User_Email" HeaderText="邮件地址">
                                    <HeaderStyle  Width="15%"/>
                                    <ItemStyle HorizontalAlign="Center"  Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="User_Title" HeaderText="职  称">
                                    <HeaderStyle  Width="10%"/>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="User_SalesManager" HeaderText="销售经理">
                                    <HeaderStyle  Width="10%"/>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="User_Tel" HeaderText="电  话">
                                    <HeaderStyle  Width="15%"/>
                                    <ItemStyle HorizontalAlign="Center"  Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="User_Active" HeaderText="是否有效">
                                    <HeaderStyle Width="10%"/>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                            </Columns>

                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />                        
                            <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />

                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </li>
      </ul>
    </div>
</div>
         <script language="javascript " type="text/javascript">
             function CheckSubmit() {
                 var checkbox = document.getElementsByTagName("input");
                 for (var i = 0; i < checkbox.length; i++) {
                     if (checkbox[i].id.indexOf('checkitem') > 0) {
                         if (checkbox[i].checked) {
                             if (confirm("确定删除吗？")) {
                                 return true;
                             }
                             else {
                                 return false;
                             }
                         }
                     }
                 }
                 alert('删除成功!');
                 return false;
             }
             function CheckEdit() {
                 var checkbox = document.getElementsByTagName("input");
                 for (var i = 0; i < checkbox.length; i++) {
                     if (checkbox[i].id.indexOf('checkitem') > 0) {
                         if (checkbox[i].checked) {
                             return true;
                         }
                     }
                 }
                 alert('修改成功！');
                 return false;
             }
             function yy_ClickCheckItem(ck) {
                 var checkbox = document.getElementsByTagName("input");
                 for (var i = 0; i < checkbox.length; i++) {
                     if (checkbox[i].id.indexOf('checkitem') > 0 && ck.checked) {
                         checkbox[i].checked = true;
                     }
                     else if (checkbox[i].id.indexOf('checkitem') > 0 && ck.checked == false) {
                         checkbox[i].checked = false;
                     }
                 }
             }
    </script>
</asp:Content>

