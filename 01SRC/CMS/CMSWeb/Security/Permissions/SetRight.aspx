<%@ Page Language="C#" AutoEventWireup="true"  AspCompat="true" CodeFile="SetRight.aspx.cs"   Inherits="Security_Permissions_SetRight" %>
<%@ Register TagPrefix="fluent" Namespace="Fluent" Assembly="Fluent.ListTransfer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--    <title><%=RightSettingTitle %></title>   --%>

    <script language="javascript" type="text/javascript">     
     function Clear()
     {
       form1.txtUserName.value="";
       form1.txtUserAccount.value="";     
     }     
     
    </script>
    <script language="javascript" type="text/javascript" src="../../Scripts/common.js"></script>
     <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />

</head>
<body>

    <script language="javascript" type="text/javascript">
function checkModuleName()
     {
        var modulName = document.getElementById("txtModuleName");
        if (modulName.value =="")
        {
            alert("<%=PromptSelectModuleName %>");            
            return false;
            modulName.focus();
        }
     }
     //选部门
     function dbchkdeptleft()
     {
         <%= listMove1.ClientMoveSelected %>;
     }
     function dbchkdeptright()
     {
         <%= listMove1.ClientMoveBackSelected %>;
     }
     //选角色
     function dbchkroleleft()
     {
         <%= listMove2.ClientMoveSelected %>;
     }
     function dbchkroleright()
     {
          <%= listMove2.ClientMoveBackSelected %>;
     }

     //选用户
     function dbchkuserleft()
     {
         <%= listMove3.ClientMoveSelected %>;
     }
     function dbchkuserright()
     {
          <%= listMove3.ClientMoveBackSelected %>;
     }

    </script>

    <form id="form1" runat="server">       
    <div id="right">
    <div class="frame01">
      <ul>
        <li class="title"><%=RightSettingTitle %> </li>
        <li>
        <table width="100%" align="center"  class="Table_BodyCSS">   
            <tr>
                <td width="15%" class="tdcell">
                    &nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server" Text="<%$Resources:ModuleNameLabel %>"></asp:Literal></td>
                <td colspan="3"  class="tdcell">
                    <asp:TextBox ID="txtModuleName" runat="server" Width="99%" ReadOnly="true" CssClass="textBlur"></asp:TextBox>
                </td>
            </tr>
             
            <tr>   
                <td colspan="4">
                    <div style="display:none">
                            <table>
                                <tr>
                                    <td class="tdcell">&nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server" Text="<%$Resources:OrgLabel %>"></asp:Literal></td>
                                    <td width="35%" height="150px">                                
                                            <asp:ListBox ID="listBoxFrom1" runat="server" Width="100%" Rows="12" SelectionMode="Multiple"></asp:ListBox>                               
                                    </td>
                                    <td align="center" valign="middle" width="15%" class="tdCellBlue">
                               
                                            <a href="#"><a href="#" onclick="<%= listMove1.ClientMoveSelected %>">
                                                <img border="0" src="../../Images/right.gif"></a><br>
                                            </a> href="#" onclick="<%= listMove1.ClientMoveBackSelected %>">
                                            <img border="0" src="../../Images/left.gif"></a><br>
                                
                                    </td>
                                    <td width="35%" height="150px">                               
                                            <asp:ListBox ID="listBoxTo1" runat="server" Width="100%" Rows="12"  SelectionMode="Multiple"></asp:ListBox>                               
                                    </td>
                                </tr>
                            </table>
                     </div>            
                </td>                 
            </tr>
           
            <tr>
                <td class="tdcell" width="10%">
                    &nbsp;&nbsp;<asp:Literal ID="Literal3" runat="server" Text="<%$Resources:RoleLabel %>"></asp:Literal></td>
                <td height="200px" class="tdcell"  width="40%" >
                    <asp:ListBox ID="listBoxFrom2" runat="server" Width="100%" Height="200px" Rows="15"  SelectionMode="Multiple"></asp:ListBox>
                </td>
                <td align="center" valign="middle" class="tdcell" width="10%" >
                    <a href="#" onclick="<%= listMove2.ClientMoveSelected %>">
                        <img border="0" src="../../Images/right.gif"></a><br>
                    <a href="#" onclick="<%= listMove2.ClientMoveBackSelected %>">
                        <img border="0" src="../../Images/left.gif"></a><br>
                </td>
                <td height="200px" class="tdcell" width="40%">
                    <asp:ListBox ID="listBoxTo2" runat="server" Width="100%" Height="200px" Rows="15"  SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>

               <tr>
                <td class="tdcell" width="10%">
                    &nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$Resources:UserListLabel %>"></asp:Literal></td>
                <td height="200px" class="tdcell"  width="40%" >
                    <asp:ListBox ID="listBoxFrom3" runat="server" Width="100%" Height="200px" Rows="15"  SelectionMode="Multiple"></asp:ListBox>
                </td>
                <td align="center" valign="middle" class="tdcell" width="10%" >
                    <a href="#" onclick="<%= listMove3.ClientMoveSelected %>">
                        <img border="0" src="../../Images/right.gif"></a><br>
                    <a href="#" onclick="<%= listMove3.ClientMoveBackSelected %>">
                        <img border="0" src="../../Images/left.gif"></a><br>
                </td>
                <td height="200px" class="tdcell" width="40%">
                    <asp:ListBox ID="listBoxTo3" runat="server" Width="100%" Height="200px" Rows="15"  SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>

           <%-- <tr>
                <td class="tdcell">
                    &nbsp;&nbsp;用户：</td>
                <td colspan="3">                   
                    <input type="text" id="txtUserName" readonly="readonly" class="textBlur" runat="server"
                        style="width: 80%" />
                    <input type="hidden" id="txtUserAccount" runat="server" />                    
                    <input type="button" class="ImgBgButton" id="btnChoose" value="选择"
                        onclick="OpenWin('../../Common/sel_user.aspx?FormType=MultiSelMember')" />
                    <input type="button" class="ImgBgButton" id="btnClear" value="重置" onclick="Clear();" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" align="center" class="tdcell">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="<%$Resources:MyGlobal,SaveText %>" OnClick="btnSave_Click" OnClientClick="return checkModuleName();"  />
                </td>
            </tr>
        </table>
        <fluent:ListMove ID="listMove1" runat="server" ListControlFrom="listBoxFrom1" ListControlTo="listBoxTo1"
            EnableClientSide="true">
        </fluent:ListMove>
        <fluent:ListMove ID="listMove2" runat="server" ListControlFrom="listBoxFrom2" ListControlTo="listBoxTo2"
            EnableClientSide="true">
        </fluent:ListMove> 
        <fluent:ListMove ID="listMove3" runat="server" ListControlFrom="listBoxFrom3" ListControlTo="listBoxTo3"
            EnableClientSide="true">
        </fluent:ListMove>      
        </li>
      </ul>
    </div>
</div>
    </form>
</body>
</html>
