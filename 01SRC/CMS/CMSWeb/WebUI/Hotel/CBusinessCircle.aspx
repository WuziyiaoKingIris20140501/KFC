<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBusinessCircle.aspx.cs"  Title="酒店商圈管理" Inherits="CBusinessCircle" %>
<%@ Register src="../../UserControls/AutoCptControl.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-1.7.1.min.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-ui-1.8.18.custom.min.js")%>'></script>
    <script language="javascript" type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery.autocomplete.js")%>'></script>

    <link href="../../Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        function removeClick(e) {
            $(e).remove();

        }
        function PageClosed() {
            //window.returnValue = true;
            window.returnValue = document.getElementById("<%=hidUserGroupList.ClientID%>").value;// document.getElementById("<%=dvUserGroupList.ClientID%>").innerHTML;
            window.opener = null;
            window.close();
        }
        function InitialValue(arg) {
            document.getElementById("wctCity").value = arg;
            document.getElementById("wctCity").text = arg;
        }
        function BtnAddUserGroup() {
            var idVal = document.getElementById("wcthvpTagInfo").value;
            if (idVal.trim() == "") {
                return;
            }
            var btnid = "btnCommon_" + idVal.substring(0, idVal.indexOf("]"));
            var dpValue = document.getElementById("wcthvpTagInfo").value + "   ";
            if (btnid == "btnCommon_") {
                document.getElementById("wcthvpTagInfo").value = "";
                document.getElementById("wcthvpTagInfo").text = "";
                return;
            }

            if (document.getElementById(btnid) != null && document.getElementById(btnid) != "") {
                document.getElementById("wcthvpTagInfo").value = "";
                document.getElementById("wcthvpTagInfo").text = "";
                return;
            }
            var board = document.getElementById("<%=dvUserGroupList.ClientID%>");
            var e = document.createElement("input");
            e.type = "button";
            e.setAttribute("id", btnid);
            e.value = dpValue;
            e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
            e.onclick = function () {
                e.parentNode.removeChild(this);
            }
            board.appendChild(e);

            document.getElementById("wcthvpTagInfo").value = "";
            document.getElementById("wcthvpTagInfo").text = "";
        }


        function SetContronListVal() {
            var userboard = document.getElementById("<%=dvUserGroupList.ClientID%>");
            var useridList = "";
            for (i = 0; i < userboard.childNodes.length; i++) {
                useridList = useridList + userboard.childNodes[i].value + ','
            }
            document.getElementById("<%=hidUserGroupList.ClientID%>").value = useridList;
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="Div1">
    <div class="frame01">
      <ul>
        <li class="title">酒店商圈管理</li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <table>
                        <tr>
                            <td>
                                酒店所在商圈：
                            </td>
                            <td>
                                <uc1:WebAutoComplete ID="wcthvpTagInfo" runat="server" CTLID="wcthvpTagInfo" AutoType="hvptaginfo"
                                    AutoParent="CBusinessCircle.aspx?Type=hvptaginfo" />
                            </td>
                            <td>
                                <input id="btnAddUserGroup" type="button" value="添加" class="btn primary"
                                    onclick="BtnAddUserGroup()" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:HiddenField ID="hidUserGroupList" runat="server" />
                                <div id="dvUserGroupList" runat="server" />
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
            <ContentTemplate>
            <div id="Div2" runat="server" style="color:red"></div>
            <asp:HiddenField ID="hidDestinationID" runat="server"/>
            <asp:HiddenField ID="hidCity" runat="server"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li class="button">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Button ID="Button1" runat="server" CssClass="btn primary" Text="修改" OnClientClick="SetContronListVal()" onclick="btnUpdate_Click" />
            <input type="button" value="取消" class="btn" onclick="window.returnValue=null;window.close();"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
      </ul>
    </div>
   </div>
</form>
</body>
</html>