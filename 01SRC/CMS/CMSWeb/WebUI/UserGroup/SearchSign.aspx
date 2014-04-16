<%@ Page Title="查询用户验证码" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SearchSign.aspx.cs" Inherits="WebUI_UserGroup_SearchSign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <table align="center" border="0" width="100%" class="Table_BodyCSS">
             <tr class="RowTitle" align="center"><td colspan="2"><asp:Literal Text="验证码查询" ID="lblSearchSign" runat="server"></asp:Literal> </td></tr> 
             <tr>
                 <td align="right" class="tdcell"  style="width:25%">手机号码：</td>
                 <td class="tdcell"  style="text-align:left; width:75%">                                                  
                    <asp:TextBox ID="txtMobile" runat="server" Width="200"></asp:TextBox>                                    
                    <asp:Button ID="btnSearch" CssClass="btn primary" runat="server" Text="查询" OnClientClick="javascript:return checkInput();" onclick="btnSearch_Click" />                   
                </td>                           
            </tr> 
            <tr>
                <td class="tdcell" style="width:10%" align="right"><strong>验证码：</strong></td>
                <td class="tdcell" style="width:10%">
                    <strong>
                    <font color="blue">
                        <asp:Label ID="lblSign" runat="server" Text=""></asp:Label>
                    </font>
                    </strong>
                </td>
            </tr>                    
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        function checkInput() {
            var mobile = document.getElementById("<%=txtMobile.ClientID%>").value;
            if (mobile == "") {
                alert("手机号码不能为空");
                document.getElementById("<%=txtMobile.ClientID%>").focus();
                return false;
            }
            else {
                reg = /^1[3,4,5,8]\d{9}$/gi;
                if (!reg.test(mobile)) {
                    alert("非法的手机号码");
                    document.getElementById("<%=txtMobile.ClientID%>").focus();
                    return false;
                }
            }
        }
    </script>


</asp:Content>

