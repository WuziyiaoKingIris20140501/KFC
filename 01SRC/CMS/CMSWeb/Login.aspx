<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>   
<meta http-equiv="X-UA-Compatible" content="IE=9,chrome=1" />
<link href="Styles/Sites.css" rel="Stylesheet" type="text/css" />   
   <script type="text/javascript" language="javascript">
       function checkInput() {
           var phoneNumber = document.getElementById("txtUserID").value;
           if (phoneNumber == "") {
               document.getElementById("lblRegMsgPopup").innerText = " 请输入账户";
               return false;
           }
           var signKey = document.getElementById("txtPwd").value;
           if (signKey == "") {
               document.getElementById("lblRegMsgPopup").innerText = " 请输入密码";
               return false;
           }
           return true;
       }

       function changeImg(btn) //鼠标移入，更换图片        
       { btn.src = "Images/LgAcButton.jpg"; }
       function changeback(btn)  //鼠标移出，换回原来的图片        
       { btn.src = "Images/LgButton.jpg"; }
   </script>

   <style type="text/css" >
    .txtLg { 
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1) inset;
        transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;
        border: 1px solid #D4DAE8;
        border-radius:8px;
        color: #666666;
        display: inline-block;
        padding: 4px;
        font-size:31px;
        width:350px;
        height:35px;
    }
    </style>
    <style type="text/css" >
    input[type="text"], input[type="textarea"], input[type="select"] {
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1) inset;
        transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;
    }

    input[type="text"], input[type="textarea"], .uneditable-input {
        border: 1px solid #D4DAE8;
        border-radius:8px;
        color: #666666;
        display: inline-block;
        padding: 4px;
        font-size:31px;
        width:350px;
        height:35px;
    }
    </style>

    <title>用户登录</title>
</head>
<body style="background-color:#EDF0F5;">
      <form id="form1" runat="server">
      <div style="margin-left:0px;width:100%;height:100%;">
        <div style="background-color:#273b7c;height:80px;width:100%;">
            <div style="float:left;width:50%;height:80px;">
                <div style="margin-right:298px;text-align:right">
                    <b style="font-size:44px;color:#ffffff;line-height:60px;">K&nbsp;.&nbsp;F&nbsp;.&nbsp;C</b>
                    <p style="font-size:18px;color:#8697c2;margin-top:-10px;">Hotelvp&nbsp;&nbsp;&nbsp;&nbsp;Backend</p>
                </div>
            </div>
            <div style="float:right;width:50%;height:80px;"></div>
        </div>

        <div style="background-color:#EDF0F5;height:100%;width:100%;">
            <div style="float:left;width:50%">
                <div style="margin-top:40px;margin-right:80px;text-align:right">
                    <img src="Images/bglogin.jpg" alt="image" style="width:auto"/>
                </div>
            </div>
            <div style="float:right;width:50%">
                <div style="text-align:left;margin-left:60px;">
                    <div style="margin-top:40px;text-align:left;"><p style="font-size:36px;color:#000000;line-height:85px;">登录</p></div>
                    <div style="margin-top:-30px;text-align:left;"><asp:TextBox ID="txtUserID" runat="server" Width="350px" Height="35px"></asp:TextBox></div>
                    <div style="margin-top:10px;text-align:left;"><asp:TextBox ID="txtPwd" TextMode="Password" runat="server" Width="350px" Height="35px" CssClass="txtLg"></asp:TextBox></div>
                    <div style="margin-top:20px;text-align:left;">
                        <div style="height:100%;width:100%">
                        <div style="float:left;width:30%;text-align:left">
                            <asp:ImageButton runat="server" ImageUrl="Images/LgButton.jpg" CssClass="" ID="imgLogIn" OnClientClick="return checkInput();" OnClick="btnLogin_Click" onmouseout="changeback(this)" onmousedown="changeImg(this)"/>
                        </div>
                        <div style="float:right;width:70%;text-align:left;margin-top:20px">
                            <font style="font-size:18px;color:#6f7174;"><input type="checkbox" style="font-size:14px;color:#6f7174;margin-bottom:10px" runat="server" id="chkRemember"/>记住用户</font>
                            <font color="red" style="font-size:14px;"><asp:Label runat="server" id="lblRegMsgPopup"></asp:Label></font>
                        </div>
                        </div>
                    </div>
                    <div style="margin-top:93px;text-align:left;">
                        <div style="margin:0;padding:0; width:365px;height:1px;background-color:#6f7174;"></div>
                        <div style="margin-top:5px;">
                            <font style="font-size:14px;color:#6f7174;">忘记密码 | 创建新账户 请联系 </font><font style="font-size:14px;color:#273b7c;">Kevin.zhang@hotelvp.com</font>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
     </form>
</body>
</html>
