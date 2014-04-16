<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceManagerDetail.aspx.cs"  Title="发票管理明细" Inherits="InvoiceManagerDetail" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        function OpenIssuePage() {
            var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                fulls += ",height=" + ah;
                fulls += ",innerHeight=" + ah;
                fulls += ",width=" + aw;
                fulls += ",innerWidth=" + aw;
                fulls += ",resizable"
            } else {
                fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
            }
            var time = new Date();
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=2&RID=' + document.getElementById("<%=lbCNFNUM.ClientID%>").innerText + "&time=" + time, null, fulls);
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 350px;
        }
        .style3
        {
            text-align:right;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">发票管理明细</li>
        <li>
           <table>
        <tr>
            <td class="style3">
                订单号：
            </td>
            <td class="style1">
                <asp:Label ID="lbCNFNUM" runat="server" Text="" />
            </td>
            <td class="style3">
                状态：
            </td>
            <td class="style1">
                <asp:Label ID="lbStatus" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                注册手机：
            </td>
            <td>
                <asp:Label ID="lbUSERID" runat="server" Text="" />
            </td>
            <td class="style3">
                联系电话：
            </td>
            <td>
                <asp:Label ID="lbCONTRACTTEL" runat="server" Text="" />
            </td>
        </tr>
         <tr>
            <td class="style3">
                申请渠道：
            </td>
            <td>
                <asp:Label ID="lbAPPLYCHANEL" runat="server" Text="" />
            </td>
            <td class="style3">
                申请时间：
            </td>
            <td>
                <asp:Label ID="lbAPPLYTIME" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                收件人姓名：
            </td>
            <td>
                <asp:Label ID="lbRECEIVERNAME" runat="server" Text="" />
            </td>
            <td class="style3">
                发票抬头：
            </td>
            <td>
                <asp:Label ID="lbINVOICEHEAD" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                发票内容：
            </td>
            <td colspan="3">
                <asp:Label ID="lbINVOICEBODY" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                邮寄地址：
            </td>
            <td>
                <asp:Label ID="lbINVOICEADDR" runat="server" Text="" />
            </td>
            <td class="style3">
                邮寄时间：
            </td>
            <td>
                <asp:Label ID="lbSENDTIME" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                邮政编码：
            </td>
            <td>
                <asp:Label ID="lbZIPCODE" runat="server" Text="" />
            </td>
            <td class="style3">
                发票金额：
            </td>
            <td>
                <asp:Label ID="lbINVOICEAMOUNT" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                邮寄方名称：
            </td>
            <td>
                <asp:Label ID="lbSENDNAME" runat="server" Text="" />
                <input name="textfield" type="text" id="txtSENDNAME" value="" style="width:200px;" visible="false" runat="server" maxlength="200"/>
            </td>
             <td class="style3">
                邮寄单号：
            </td>
            <td>
                <asp:Label ID="lbSENDCODE" runat="server" Text="" />
                <input name="textfield" type="text" id="txtSENDCODE" value="" style="width:200px;" visible="false" runat="server" maxlength="40"/>
            </td>
        </tr>
        <tr>
           <td class="style3">
                发票号：
            </td>
            <td>
                <asp:Label ID="lbINVOICENUM" runat="server" Text="" />
                <input name="textfield" type="text" id="txtINVOICENUM" value="" style="width:200px;" visible="false" runat="server" maxlength="40"/>
            </td>
            <td class="style3">
                操作人：
            </td>
            <td>
                <asp:Label ID="lbOPERATOR" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                操作备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtOPERATORMEMO" runat="server" TextMode="MultiLine" Width="665px" Height="65px"></asp:TextBox>
            </td>
        </tr>
      </table>
        </li>
        <li><div id="detailMessageContent" runat="server" style="color:red;width:800px"></div></li>
        <li>
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="修改" onclick="btnUpdate_Click" />
            <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="撤销" onclick="btnBack_Click" />
            &nbsp;<input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" />
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/> 
        </li>
        <li></li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidInvoiceID" runat="server"/>
<asp:HiddenField ID="hidOnlineStatus" runat="server"/>
</form>
</body>
</html>