<%@ Page Title="添加新用户" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddCmsUser.aspx.cs" Inherits="Security_Users_AddCmsUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="right">
    <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="<%$Resources:AddNewUserTitleLabel%>" ID="lblHeadTitle" runat="server"></asp:Literal> </li>
        <li>
     <table align="center" border="0" width="100%" class="Table_BodyCSS">
       <tr>
            <td class="tdcell"><asp:Label ID="lblUserAccount" runat="server" Text="<%$Resources:UserAccountLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtUserAccount" runat="server"></asp:TextBox><font color=red>*</font></td>
            <td class="tdcell"><asp:Label ID="lblUserDspNameLabel" runat="server" Text="<%$Resources:UserDspNameLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
       </tr>

       <tr>
            <td class="tdcell"><asp:Label ID="lblPwd" runat="server" Text="<%$Resources:PwdLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtPwd" runat="server"></asp:TextBox><font color=red>*</font></td>
            <td class="tdcell"><asp:Label ID="lblEmail" runat="server" Text="<%$Resources:EmailLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><font color=red>*</font></td>
       </tr> 
          
        <tr>
            <td class="tdcell"><asp:Label ID="lblHRID" runat="server" Text="<%$Resources:HRIDLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtHRID" runat="server"></asp:TextBox></td>
           <%-- <td class="tdcell"><asp:Label ID="lblTitle" runat="server" Text="<%$Resources:TitleLabel %>"></asp:Label></td>
           <td class="tdcell"><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>--%>
           <td class="tdcell"><asp:Label ID="lbUserManager" runat="server" Text="<%$Resources:UserManagerLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtUserManager" runat="server"></asp:TextBox></td>
       </tr>
       <tr>
            <td class="tdcell"><asp:Label ID="lblTel" runat="server" Text="<%$Resources:TelLabel %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtTel" runat="server"></asp:TextBox><font color=red>*</font></td>
            <td class="tdcell" id="tdRoL" runat="server"><asp:Label ID="Label1" runat="server" Text="用户角色"></asp:Label></td>
            <td class="tdcell" id="tdRoV" runat="server"><asp:DropDownList ID="ddpRole" CssClass="noborder_inactive" runat="server" Width="153px"/></td>
       </tr>
       <tr>
            <td  colspan=4 class="tdcell" align="center"> 
                <asp:Button ID="btnOK" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,OkText %>"  OnClientClick="return checkInput();"
                    onclick="btnOK_Click"/> &nbsp; &nbsp; &nbsp; &nbsp;    
                <input id="Reset" type="Reset" runat="server" class="btn" onclick="ResetContent()" value="<%$ Resources:MyGlobal,ResetText %>" />&nbsp; &nbsp; &nbsp; &nbsp;  
                <asp:Button ID="back" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,BackText %>" onclick="back_Click" />
            </td>
        </tr> 
    </table>
    </li>
      </ul>
    </div>
</div>
    <asp:HiddenField ID="hidActionType" runat="server"/>
     <script language="javascript" type="text/javascript">
         function ResetContent() {
             document.getElementById("<%=txtUserAccount.ClientID%>").value = "";
             document.getElementById("<%=txtUserName.ClientID%>").value = "";
             document.getElementById("<%=txtPwd.ClientID%>").value = "";
             document.getElementById("<%=txtEmail.ClientID%>").value = "";
             document.getElementById("<%=txtHRID.ClientID%>").value = "";
             document.getElementById("<%=txtTel.ClientID%>").value = "";
             document.getElementById("<%=ddpRole.ClientID%>").selectedIndex = 0;

         }

         function checkInput() {
             var userAccount = document.getElementById("<%=txtUserAccount.ClientID%>").value;
             if (userAccount == "") {
                 alert("账号不能为空！");
                 document.getElementById("<%=txtUserAccount.ClientID%>").focus();
                 return false;
             }

             var pwd = document.getElementById("<%=txtPwd.ClientID%>").value;
             var actionType = document.getElementById("<%=hidActionType.ClientID%>").value;
             if (actionType == "add" && pwd == "") {
                 alert("密码不能为空！");
                 document.getElementById("<%=txtPwd.ClientID%>").focus();
                 return false;
             }

             var userEmail = document.getElementById("<%=txtEmail.ClientID%>").value;
             if (userEmail == "") {
                 alert("邮箱不能为空！");
                 document.getElementById("<%=txtEmail.ClientID%>").focus();
                 return false;
             }

             var userTel = document.getElementById("<%=txtTel.ClientID%>").value;
             if (userTel == "") {
                 alert("电话不能为空！");
                 document.getElementById("<%=txtTel.ClientID%>").focus();
                 return false;
             } 
         }
    </script>
</asp:Content>

