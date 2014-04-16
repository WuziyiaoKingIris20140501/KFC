<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ModifyPwd.aspx.cs" Inherits="Security_Permissions_ModifyPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

  <table align="center" border="0" width="100%" class="Table_BodyCSS">
            
         <tr class="RowTitle"><td colspan="4" align="center" class="tdcell"><asp:Literal Text="<%$Resources:ModifyPasswordTitle%>" ID="lblHeadTitle" runat="server"></asp:Literal> </td></tr>

         <tr><td colspan=2></td></tr>
         <tr style="background-color:White;color:Red">
            <td align="center" colspan=2><asp:Label ID="lblRegMsgPopup" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right" style ="width:45%" class="tdcell"><asp:Literal Text="<%$Resources:UserAccountText%>" ID="Literal1" runat="server"></asp:Literal> </td>

            <td align="left" style ="width:55%" class="tdcell">
            <asp:Label ID="txtUserID" runat="server" Text=""></asp:Label>
            <%--<asp:TextBox ID="txtUserID"  runat="server"  Width="200"  ReadOnly="true"></asp:TextBox>--%>
            </td>
        </tr>           
        
        <tr>
            <td align="right"   class="tdcell"><asp:Literal Text="<%$Resources:OldPasswordText%>" ID="Literal3" runat="server"></asp:Literal> </td>
            <td align="left"   class="tdcell"><asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="200"></asp:TextBox></td>
        </tr>  
         
        <tr>
            <td align="right"   class="tdcell"><asp:Literal Text="<%$Resources:NewPasswordText%>" ID="Literal2" runat="server"></asp:Literal> </td>
            <td align="left"   class="tdcell"><asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="200"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"   class="tdcell"><asp:Literal Text="<%$Resources:ConfirmPasswordText%>" ID="Literal4" runat="server"></asp:Literal> </td>
            <td align="left"   class="tdcell"><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="200"></asp:TextBox></td>
        </tr>
        <tr><td align="center" colspan="2" class="tdcell">
            <asp:Button ID="btnOk" runat="server" CssClass="btn primary" Text="<%$Resources:OkText%>"  
                OnClientClick="return checkInput();" onclick="btnOk_Click" /> </td></tr>      
   
     </table>

      <script type="text/javascript" language="javascript">
          function checkInput() {

              var phoneNumber = document.getElementById("<%=txtUserID.ClientID %>").innerText;
              if (phoneNumber == "") {
                  document.getElementById("<%=lblRegMsgPopup.ClientID %>").innerText = "<%=PromptInpuAccount %>";
                  document.getElementById("<%=txtUserID.ClientID %>").focus();
                  return false;
              }
              var oldPassword = document.getElementById("<%=txtOldPassword.ClientID %>").value;
              if (oldPassword == "") {
                  document.getElementById("<%=lblRegMsgPopup.ClientID %>").innerText = "<%=PromptInputOldPwd %>";
                  document.getElementById("<%=txtOldPassword.ClientID %>").focus();
                  return false;
              }

              var newPassword = document.getElementById("<%=txtNewPassword.ClientID %>").value;
              if (newPassword == "") {
                  document.getElementById("<%=lblRegMsgPopup.ClientID %>").innerText = "<%=PromptInputNewPwd %>";
                  document.getElementById("<%=txtNewPassword.ClientID %>").focus();
                  return false;
              }

              var confirmPassword = document.getElementById("<%=txtConfirmPassword.ClientID %>").value;
              if (confirmPassword == "") {
                  document.getElementById("<%=lblRegMsgPopup.ClientID %>").innerText = "<%=PromptInputConfirmPwd %>";
                  document.getElementById("<%=txtConfirmPassword.ClientID %>").focus();
                  return false;
              }

              if (newPassword != confirmPassword) {
                  document.getElementById("<%=lblRegMsgPopup.ClientID %>s").innerText = "<%=PromptPwdNotConsistent %>";                 
                  return false;
              }
              return true;
          }

       
   </script>  
</asp:Content>

