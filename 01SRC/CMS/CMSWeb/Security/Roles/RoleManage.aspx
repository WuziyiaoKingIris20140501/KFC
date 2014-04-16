<%@ Page Title="角色管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RoleManage.aspx.cs" Inherits="Security_Roles_RoleManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<style type="text/css">
</style>--%>
  <!-----------for popup--------------->
    <div id="bgDiv" class="bgDiv">        
    </div>
    <div id="popupDiv" class="popupDiv" style="width:500px;height:150px">
    <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="新增角色" ID="Literal1" runat="server"></asp:Literal> </li>
        <li>
         <table width="100%" align="center" id="Table1">
             <tr><td colspan="2"><div id="messageContent" runat="server" style="color:red"></div></td></tr>
            <tr>
                <td style="width: 20%;" class="tdCellBlue">角色名</td>
                <td style="height: 30px" class="tdcell">
                    <asp:TextBox ID="txtRoleName" runat="server" Width="200px"></asp:TextBox>
                    <asp:CheckBox ID="chkIsAD" runat="server" Text="AD账户角色" />
                </td>
            </tr>

            <tr>
                <td colspan=2 align="center" >           
                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" onclick="btnSave_Click" />&nbsp; &nbsp;&nbsp;
                    <input type="button" value="关闭" id="btnClose" name="btnClose" class="btn" onclick="clearText();invokeClose();" /></td>
            </tr>
        </table>
        </li>
          </ul>
        </div>
    </div>
    <!---------------------------------->
    <div style ="display:none"><asp:TextBox ID="txtRoleID" runat="server" Width="98%"></asp:TextBox></div>
    <div>
    <div id="right">
    <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="<%$Resources:RoleManageTitle%>" ID="lbAdviceTitle" runat="server"></asp:Literal> </li>
        <li>
   <table align="center" border="0" width="100%" class="Table_BodyCSS">
            <tr>
                <td align="left">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <input runat="server" type="button" id="Add" class="btn primary" value="<%$ Resources:MyGlobal,NewText %>" onclick="AddNew()" /></td>
                           <%-- <td>
                                <input runat="server" type="button" id="Edit"  value="<%$ Resources:MyGlobal,EditText %>" onclick="EditItem()" /></td>
                            <td>
                                <asp:Button ID="Delete" runat="server" Text="<%$ Resources:MyGlobal,DeleteText %>" OnClick="Delete_Click" /></td>--%>
                        </tr>
                    </table>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:GridView ID="RoleGridView" runat="server" PageSize="15"
                        AutoGenerateColumns="False" Width="100%" OnRowCreated="RoleGridView_RowCreated"
                        OnRowDataBound="RoleGridView_RowDataBound"  CssClass="GView_BodyCSS" 
                        onrowdeleting="RoleGridView_RowDeleting" DataKeyNames="Role_ID">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="HeadCheckBox" runat="server" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="checkitem" runat="server" />
                                    <input type="hidden" name="Index" value='<%#DataBinder.Eval(Container.DataItem, "Role_ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Role_Name" HeaderText="<%$ Resources:RoleNameText%>">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <a onclick="javascript:UpdateItem('<%#Eval("Role_ID")%>','<%#Eval("Role_Name")%>','<%#Eval("IS_AD")%>')" style="cursor:pointer" >
                                        <font color="blue">
                                            <asp:Label ID="lblEdit" runat="server" Text="<%$Resources:MyGlobal,EditText %>"></asp:Label></font></a>                                        
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="Role_ID" HeaderText="ID" />
                            <asp:CommandField ShowDeleteButton="True" >

                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:CommandField>

                            <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <ItemTemplate>                                
                                  <a href='<%#Eval("Role_ID","RoleUser.aspx?roleid={0}&rolename=") + HttpUtility.UrlEncode(Eval("Role_Name").ToString()) %>'>
                                    <font color="blue">
                                        <asp:Label ID="lblAddToRole" runat="server" Text="增加用户"></asp:Label></font></a>                                        
                            </ItemTemplate>
                            </asp:TemplateField>
                         
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
    </div>
 <script language="javascript " type="text/javascript">
     function AddNew() {

         //OpenModelWin("AddRole.aspx", 500, 500);
         invokeOpen();
     }
     function EditItem() {
         var index = 0;
         var checkbox = document.getElementsByTagName("input");
         var i;
         for (i = 0; i < checkbox.length; i++) {
             if (checkbox[i].id.indexOf('checkitem') > 0) {
                 index++
                 if (checkbox[i].checked) {
                     break;
                 }
             }
         }
         if (i >= checkbox.length) {
             alert('<%=STR_SEL_MODIFY %>');
             return false;
         }
         var obj = document.getElementsByName("Index");
         obj[index - 1].value;

         OpenModelWin("AddRole.aspx?roleid=" + obj[index - 1].value, 150, 500);


     }

     function UpdateItem(roleid, rolename, chk) 
     {     
         document.getElementById("<%=txtRoleID.ClientID %>").value = roleid;
         document.getElementById("<%=txtRoleName.ClientID %>").value = rolename;
         if (chk == '1')
         {
            document.getElementById("<%=chkIsAD.ClientID %>").checked = true;
         }
         else
         {
            document.getElementById("<%=chkIsAD.ClientID %>").checked = false;
         }
         invokeOpen();
     }

     var win = null;
     function OpenModelWin(url, height, width) {
         var tp = (window.screen.availHeight - height) / 2;
         var lf = (window.screen.availWidth - width) / 2;
         if (win != null) {
             try {
                 win.close();
             }
             catch (e2) {
                 win = null;
             }
         }
         win = window.open(url, "window", "height =" + height + ",width =" + width + "px,top=" + tp + "px,left=" + lf + "px,toolbar=no,menubar=no, scrollbars=yes,  location=no,resizable=yes, status=no,z-look=yes,alwaysRaised=yes");

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
     function CheckSubmit() {
         var checkbox = document.getElementsByTagName("input");
         for (var i = 0; i < checkbox.length; i++) {
             if (checkbox[i].id.indexOf('checkitem') > 0) {
                 if (checkbox[i].checked) {
                     if (confirm("<%=STR_CONFIRM_DEL %>")) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 }
             }
         }
         alert('<%=STR_SEL_DEL %>');
         return false;
     }

     //清空
     function clearText() {
         document.getElementById("<%=txtRoleID.ClientID %>").value = "";
         document.getElementById("<%=txtRoleName.ClientID %>").value = "";         
     }

//     //==============================
//     function invokeOpen() {
//         document.getElementById("ElseAddressDiv").style.display = "block";
//         //背景
//         var bgObj = document.getElementById("bgDiv");
//         bgObj.style.display = "block";
//         bgObj.style.width = document.body.offsetWidth + "px";
//         bgObj.style.height = screen.height + "px";           
//     }

//     function invokeClose() {
//         document.getElementById("ElseAddressDiv").style.display = "none";
//         document.getElementById("bgDiv").style.display = "none";
//     }

    </script>
</asp:Content>

