<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="UpdateAdviceStatus.aspx.cs" Inherits="WebUI_Feedback_UpdateAdviceStatus" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>查看规则详情</title> 
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
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=5&RID=' + document.getElementById("<%=hidAdviceID.ClientID%>").value + "&time=" + time, null, fulls);
        }
</script>
</head>   

<body>
  <form id="form1" runat="server">    
    <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <tr class="RowTitle"><td colspan=2><asp:Literal Text="<%$Resources:UserAdviceTitleLabel%>" ID="lbAdviceTitle" runat="server"></asp:Literal><asp:HiddenField ID="hidAdviceID" runat="server"/> </td></tr>
       <tr>
        <td class="tdcell"><asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:MobileNumberLabel %>"></asp:Label></td>
       <td class="tdcell"><asp:Label ID="txtMobileNumber" runat="server" Text="手机号码"></asp:Label></td>
       </tr>
       <tr>
        <td class="tdcell"><asp:Label ID="lbUserVer" runat="server" Text="版本号"></asp:Label></td>
       <td class="tdcell"><asp:Label ID="txtUserVer" runat="server" Text="版本号"></asp:Label></td>
       </tr>
       <tr>
            <td class="tdcell" style="width:30%;">           
           <asp:Label ID="lblPublishDate" runat="server" Text="<%$Resources:PublishDateLabel %>"></asp:Label></td>
           <td class="tdcell" style="width:70%;"><asp:Label ID="txtPublishDate" runat="server" Text="发布日期"></asp:Label></td>
       </tr>
       <tr>
            <td  class="tdcell" ><asp:Label ID="lblScore" runat="server" Text="<%$Resources:ScoreLabel %>"></asp:Label></td>
            <td class="tdcell" align="left"><asp:Label ID="txtScore" runat="server" Text="用户评分" ></asp:Label></td>
       </tr>

       <tr><td  class="tdcell" ><asp:Label ID="lblPlatForm" runat="server" Text="<%$Resources:PlatFormLabel %>"></asp:Label></td>
            <td class="tdcell" align="left"><asp:Label ID="txtPlatForm" runat="server" Text="注册平台"></asp:Label></td>
       </tr>        
       <tr>
            <td  class="tdcell" ><asp:Label ID="lblPrcStatus" runat="server" Text="<%$Resources:PrcStatusLabel %>"></asp:Label></td>
           <td class="tdcell" align="left"><asp:Label ID="txtPrcStatus" runat="server" Text="处理状态"></asp:Label></td></tr>    
          
        <tr>
            <td  class="tdcell" ><asp:Label ID="lblAdviceContent" runat="server" Text="<%$Resources:UserAdviceContent %>"></asp:Label></td>
           <td class="tdcell" align="left"><asp:Label ID="txtAdviceContent" runat="server" Text="处理状态"></asp:Label></td>
       </tr> 
       
       <tr>       
        <td  colspan=2 class="tdcell" align="center"> 
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" 
                Text="<%$ Resources:FlagHaveProcess %>" onclick="btnUpdate_Click"/> &nbsp; &nbsp; &nbsp; &nbsp;
              <input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" />&nbsp; &nbsp; &nbsp; &nbsp;
              <input id="btnClose" type="button" value="<%=strClose %>" class="btn" onclick="Close()" />

        </td>        
        </tr> 
    </table>
    <script language="javascript" type="text/javascript">
        function Close() {         
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
    </script>
 </form>
</body>

