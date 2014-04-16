<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" MasterPageFile="~/Site.master"  CodeFile="CreateCashByOrder.aspx.cs"  Title="创建提现申请管理" Inherits="CreateCashByOrder" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<style type="text/css" >
.pcbackground { 
display: block; 
width: 100%; 
height: 100%; 
opacity: 0.4; 
filter: alpha(opacity=40); 
background:while; 
position: absolute; 
top: 0; 
left: 0; 
z-index: 2000; 
} 
.pcprogressBar { 
border: solid 2px #3A599C; 
background: white url("/images/progressBar_m.gif") no-repeat 10px 10px; 
display: block; 
width: 148px; 
height: 28px; 
position: fixed; 
top: 50%; 
left: 50%; 
margin-left: -74px; 
margin-top: -14px; 
padding: 10px 10px 10px 50px; 
text-align: left; 
line-height: 27px; 
font-weight: bold; 
position: absolute; 
z-index: 2001; 
}
</style>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=rbtBank.ClientID%>").checked = true;
        document.getElementById("rbtInBank").checked = true;
    }

    function SerRbtNameValue(arg, val) {
        if (val == "0") {
            var cur = document.getElementById("<%=hidCommonType.ClientID%>").value;
            if (cur != "" && cur == arg) {
                return;
            }
            if (arg == "0") {
                document.getElementById("<%=rbtBank.ClientID%>").checked = true;
                document.getElementById("<%=dvBank.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvBao.ClientID%>").style.display = 'none';
            }
            else if (arg == "1") {
                document.getElementById("<%=rbtTel.ClientID%>").checked = true;
                document.getElementById("<%=dvBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvTel.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvBao.ClientID%>").style.display = 'none';
                document.getElementById("<%=txtBackTel.ClientID%>").value = document.getElementById("<%=lbUserID.ClientID%>").innerHTML;
            }
            else if (arg == "2") {
                document.getElementById("<%=rbtBao.ClientID%>").checked = true;
                document.getElementById("<%=dvBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvBao.ClientID%>").style.display = 'block';
            }
            else {

            }
            document.getElementById("<%=hidCommonType.ClientID%>").value = arg;
        }
        else {
            var cur = document.getElementById("<%=hidInCommonType.ClientID%>").value;
            if (cur != "" && cur == arg) {
                return;
            }
            if (arg == "0") {
                document.getElementById("rbtInBank").checked = true; 
                document.getElementById("<%=dvInBank.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvInTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInBao.ClientID%>").style.display = 'none';
            }
            else if (arg == "1") {
                document.getElementById("rbtInTel").checked = true; 
                document.getElementById("<%=dvInBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInTel.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvInBao.ClientID%>").style.display = 'none';
                document.getElementById("<%=txtInBackTel.ClientID%>").value = document.getElementById("<%=lbInUserID.ClientID%>").innerText;
            }
            else if (arg == "2") {
                document.getElementById("rbtInBao").checked = true; 
                document.getElementById("<%=dvInBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInBao.ClientID%>").style.display = 'block';
            }
            else {

            }
            document.getElementById("<%=hidInCommonType.ClientID%>").value = arg;
        }
    }

    function PopupCashListArea(arg, val) {
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
        var userID = "";
        if (val == "0") {
            userID = document.getElementById("<%=lbUserID.ClientID%>").innerHTML;
        }
        else {
            userID = document.getElementById("<%=lbInUserID.ClientID%>").innerHTML;
        }
        window.open("../CashBack/CashChangeSearch.aspx?TYPE=" + arg + "&ID=" + userID + "&time=" + time, null, fulls);
    }

        function PopupCashBackListArea(val) {
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
        var userID = "";
        if (val == "0") {
            userID = document.getElementById("<%=lbUserID.ClientID%>").innerHTML;
        }
        else {
            userID = document.getElementById("<%=lbInUserID.ClientID%>").innerHTML;
        }
        window.open("../CashBack/CashApplOperateSearch.aspx?UID=" + userID + "&time=" + time, null, fulls);
    }

    function PopupUserArea(arg) {
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
        var userID = "";
        if (arg == "0") {
            userID = document.getElementById("<%=lbUserID.ClientID%>").innerHTML;
        }
        else {
            userID = document.getElementById("<%=lbInUserID.ClientID%>").innerHTML;
        }
        window.open("../UserGroup/UserDetailPage.aspx?TYPE=1&ID=" + userID + "&time=" + time, null, fulls);
    }

    function PopAreaCashDetail() {

        if (document.getElementById("<%=hidCashSN.ClientID%>").value == "") {
            document.getElementById("<%=messageContent.ClientID%>").innerText = "该订单号码无提现申请，请确认！";
            return;
        }
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
        window.open("../CashBack/CashApplProcess.aspx?id=" + document.getElementById("<%=hidCashSN.ClientID%>").value + "&time=" + time, null, fulls);
    }

    function RestLoadStyle(val) {
        if (val == "0") {
            var arg = document.getElementById("<%=hidCommonType.ClientID%>").value;
            if (arg == "0") {
                document.getElementById("<%=rbtBank.ClientID%>").checked = true;
                document.getElementById("<%=dvBank.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvBao.ClientID%>").style.display = 'none';
            }
            else if (arg == "1") {
                document.getElementById("<%=rbtTel.ClientID%>").checked = true;
                document.getElementById("<%=dvBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvTel.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvBao.ClientID%>").style.display = 'none';
            }
            else if (arg == "2") {
                document.getElementById("<%=rbtBao.ClientID%>").checked = true;
                document.getElementById("<%=dvBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvBao.ClientID%>").style.display = 'block';
            }
            else {

            }
            document.getElementById("<%=hidCommonType.ClientID%>").value = arg;
        }
        else {
            var cur = document.getElementById("<%=hidInCommonType.ClientID%>").value;
            if (cur == "0") {
                document.getElementById("rbtInBank").checked = true;
                document.getElementById("<%=dvInBank.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvInTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInBao.ClientID%>").style.display = 'none';
            }
            else if (cur == "1") {
                document.getElementById("rbtInTel").checked = true;
                document.getElementById("<%=dvInBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInTel.ClientID%>").style.display = 'block';
                document.getElementById("<%=dvInBao.ClientID%>").style.display = 'none';
            }
            else if (cur == "2") {
                document.getElementById("rbtInBao").checked = true;
                document.getElementById("<%=dvInBank.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInTel.ClientID%>").style.display = 'none';
                document.getElementById("<%=dvInBao.ClientID%>").style.display = 'block';
            }
            else {

            }
        }
        BtnCompleteStyle();
    }
    
    function BtnLoadStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        ajaxbg.show();
    }

    function BtnCompleteStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
    }

    function SetControlEnable(arg) {
        if (arg == "0") {
            document.getElementById("<%=txtBao.ClientID%>").value = document.getElementById("<%=lbUserID.ClientID%>").innerHTML;
        }
        else {
            document.getElementById("<%=txtInBao.ClientID%>").value = document.getElementById("<%=lbInUserID.ClientID%>").innerHTML;
        }
    }
</script>
<div style="margin:5px 14px 5px 14px;">
        <div id="tabs" style="background:#FFFFFF;border: 0px solid #FFFFFF;">
	        <ul style="background:#FFFFFF;border: 0px solid #FFFFFF;">
		        <li><a href="#tabs-1">  根据订单创建  </a></li>
		        <li style="display:none"><a href="#tabs-2">   手工创建   </a></li>
	        </ul>
	        <div id="tabs-1" style="border: 1px solid #D5D5D5;">
		        <div>
                    <table>
                            <tr>
                                <td align="right" style="width:100px">订单号码：</td>
                                <td align="left"><asp:TextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="100"/><font color="red">*</font></td>
                                <td align="left"><asp:Button ID="btnSet" runat="server" CssClass="btn primary" Text="确定" OnClientClick="BtnLoadStyle();" onclick="btnSet_Click" /></td>
                                <td align="left">
                                    <input type="button" id="btnCheck" class="btn primary" style="display:none" value="查看已申请提现"  onclick="PopAreaCashDetail();" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px"></td>
                                <td align="left" colspan="3">
                                    <div id="messageContent" runat="server" style="color:red;width:100%;"></div>
                                </td>
                            </tr>
                    </table>
                    <table>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">订单号码：</td>
                                <td align="left"><asp:Label ID="lbOrderNo" runat="server" Text="" /></td>
                                <td align="right" style="width:120px">酒店名称：</td>
                                <td align="left"><asp:Label ID="lbHotelNM" runat="server" Text="" /></td>
                                <td align="right" style="width:120px">入住时间：</td>
                                <td align="left"><asp:Label ID="lbInDate" runat="server" Text="" /></td>
                                <td align="right" style="width:120px">联系人：</td>
                                <td align="left"><asp:Label ID="lbConPer" runat="server" Text="" /></td>
                            </tr>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">优惠券号码：</td>
                                <td align="left"><asp:Label ID="lbTiceketCode" runat="server" Text="" /></td>
                                <td align="right">返现金额：</td>
                                <td align="left"><asp:Label ID="lbBackAmount" runat="server" Text="" /></td>
                                <td align="right">订单总金额：</td>
                                <td align="left"><asp:Label ID="lbOrderAmount" runat="server" Text="" /></td>
                            </tr>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">用户ID：</td>
                                <td align="left"><asp:Label ID="lbUserID" runat="server" Text="" /></td>
                                <td align="right">用户可提现余额：</td>
                                <td align="left"><asp:Label ID="lbCanamount" runat="server" Text="" /></td>
                                <td align="right">返现审核中金额：</td>
                                <td align="left"><asp:Label ID="lbAuditamount" runat="server" Text="" /></td>
                                <td align="right">提现申请中金额：</td>
                                <td align="left"><asp:Label ID="lbPullingamount" runat="server" Text="" /></td>
                            </tr>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">申请提现金额：</td>
                                <td align="left"><asp:Label ID="lbBackCashAmount" runat="server" Text="" /></td>
                                <td align="right">申请提现方式：</td>
                                <td align="left" colspan="4">
                                    <input type="radio" name="RbtCashType" id="rbtBank" runat="server" value="0" checked="true" onclick="SerRbtNameValue('0','0')"/>银行转帐
                                    <input type="radio" name="RbtCashType" id="rbtTel" runat="server" value="1" onclick="SerRbtNameValue('1','0')"/>手机充值
                                    <input type="radio" name="RbtCashType" id="rbtBao" runat="server" value="2" onclick="SerRbtNameValue('2','0')"/>支付宝冲值
                                </td>
                            </tr>
                    </table>
                    <table>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <div id="dvBank" runat="server">
                                        <table>
                                             <tr>
                                                <td align="right" style="width:95px">开户人姓名：</td>
                                                <td align="left"><asp:TextBox ID="txtBankOwner" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                                <td align="right">分行/支行信息：</td>
                                                <td align="left"><asp:TextBox ID="txtBankBranch" runat="server" Width="300px" MaxLength="100"/><font color="red">*</font></td>
                                            </tr>
                                            <tr>
                                                <td style="height:5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="right">用户银行卡号：</td>
                                                <td align="left"><asp:TextBox ID="txtBankCardNumber" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                                <td align="right">用户开户银行：</td>
                                                <td align="left"><asp:TextBox ID="txtBankName" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div id="dvTel" runat="server" style="display:none">
                                        <table>
                                            <tr>
                                                <td align="right" style="width:95px">充值手机号码：</td>
                                                <td align="left"><asp:TextBox ID="txtBackTel" runat="server" Width="300px" MaxLength="20"/><font color="red">*</font></td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div id="dvBao" runat="server" style="display:none">
                                        <table>
                                            <tr>
                                                <td align="right" style="width:95px">用户支付宝：</td>
                                                <td align="left"><asp:TextBox ID="txtBao" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                                <td align="right"><input type="button" id="btnCopyUserID" class="btn primary" value="同用户手机号码"  onclick="SetControlEnable('0');" /></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width:95px">支付宝姓名：</td> 
                                                <td align="left"><asp:TextBox ID="txtBaoName" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">备注信息：</td>
                                <td align="left"><asp:TextBox ID="txtRemark" runat="server" Width="300px" MaxLength="60"/></td>
                            </tr>
                            <tr>
                                <td style="height:20px"></td>
                            </tr>
                    </table>
                </div>
                <div id="divBtnList" style="text-align:left;margin-left:10px" runat="server">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="创建提现申请" OnClientClick="BtnLoadStyle();" onclick="btnSave_Click" />
                </div>
	        </div>
	        <div id="tabs-2" style="border: 1px solid #D5D5D5;display:none">
		        <div>
                    <table>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">用户ID：</td>
                                <td align="left"><asp:TextBox ID="txtInUserID" runat="server" Width="300px" MaxLength="20"/><font color="red">*</font></td>
                                <td align="left"><asp:Button ID="btnInSet" runat="server" CssClass="btn primary" Text="确定" OnClientClick="BtnLoadStyle();" onclick="btnInSet_Click" /></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px"></td>
                                <td align="left" colspan="2">
                                    <div id="inMessageContent" runat="server" style="color:red;width:100%;"></div>
                                </td>
                            </tr>
                    </table>
                    <table>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">用户ID：</td>
                                <td align="left"><asp:Label ID="lbInUserID" runat="server" Text="" /></td>
                                <td align="right" style="width:120px">用户可提现余额：</td>
                                <td align="left"><asp:Label ID="lbInCanamount" runat="server" Text="" /></td>
                                <td align="right" style="width:120px">返现审核中金额：</td>
                                <td align="left"><asp:Label ID="lbInAuditamount" runat="server" Text="" /></td>
                                <td align="right" style="width:120px">提现申请中金额：</td>
                                <td align="left"><asp:Label ID="lbInPullingamount" runat="server" Text="" /></td>
                            </tr>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">申请提现金额：</td>
                                <td align="left"><asp:TextBox ID="txtInBackCashAmount" runat="server" Width="100px" MaxLength="4"/><font color="red">*</font></td>
                                <td align="right">申请提现方式：</td>
                                <td align="left" colspan="5">
                                    <input type="radio" name="RbtCashType" id="rbtInBank" value="0" checked="checked" onclick="SerRbtNameValue('0','1')"/>银行转帐
                                    <input type="radio" name="RbtCashType" id="rbtInTel" value="1" onclick="SerRbtNameValue('1','1')"/>手机充值
                                    <input type="radio" name="RbtCashType" id="rbtInBao" value="2" onclick="SerRbtNameValue('2','1')"/>支付宝充值
                                </td>
                            </tr>
                    </table>
                    <table>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <div id="dvInBank" runat="server">
                                        <table>
                                            <tr>
                                                <td align="right" style="width:95px">开户人姓名：</td>
                                                <td align="left"><asp:TextBox ID="txtInBankOwner" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                                <td align="right">分行/支行信息：</td>
                                                <td align="left"><asp:TextBox ID="txtInBankBranch" runat="server" Width="300px" MaxLength="100"/><font color="red">*</font></td>
                                            </tr>
                                            <tr>
                                                <td style="height:5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="right">用户银行卡号：</td>
                                                <td align="left"><asp:TextBox ID="txtInBankCardNumber" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                                <td align="right">用户开户银行：</td>
                                                <td align="left"><asp:TextBox ID="txtInBankName" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div id="dvInTel" runat="server" style="display:none">
                                        <table>
                                            <tr>
                                                <td align="right" style="width:95px">充值手机号码：</td>
                                                <td align="left"><asp:TextBox ID="txtInBackTel" runat="server" Width="300px" MaxLength="20"/><font color="red">*</font></td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div id="dvInBao" runat="server" style="display:none">
                                        <table>
                                            <tr>
                                                <td align="right" style="width:95px">用户支付宝：</td>
                                                <td align="left"><asp:TextBox ID="txtInBao" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                                <td align="right"><input type="button" id="btnInCopyUserID" class="btn primary" value="同用户手机号码"  onclick="SetControlEnable('1');" /></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width:95px">支付宝姓名：</td> 
                                                <td align="left"><asp:TextBox ID="txtInBaoName" runat="server" Width="300px" MaxLength="50"/><font color="red">*</font></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:5px"></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:100px">备注信息：</td>
                                <td align="left"><asp:TextBox ID="txtInReMark" runat="server" Width="300px" MaxLength="60"/></td>
                            </tr>
                             <tr>
                                <td style="height:20px"></td>
                            </tr>
                    </table>
                </div>
                <div id="div5" style="text-align:left;margin-left:10px" runat="server">
                    <asp:Button ID="btnInSave" runat="server" CssClass="btn primary" Text="创建提现申请" OnClientClick="BtnLoadStyle();" onclick="btnInSave_Click" />
                </div>
	        </div>
        </div>
</div>
<div id="background" class="pcbackground" style="display: none; "></div> 
<div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 

<asp:HiddenField ID="hidCashSN" runat="server"/>
<asp:HiddenField ID="hidUserID" runat="server"/>
<asp:HiddenField ID="hidSelectedID" runat="server"/>
<asp:HiddenField ID="hidInCommonType" runat="server"/>
<asp:HiddenField ID="hidCommonType" runat="server"/>

<script type="text/javascript">
    $(function () {
        //        $("#tabs").tabs();
        var sid = document.getElementById("<%=hidSelectedID.ClientID%>").value;
        if (sid == "" || sid == "0") {
            $("#tabs").tabs();
        }
        else {
            $('#tabs').tabs({ selected: sid, select: function (event, ui) { document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index } });
        }

        $('#tabs').bind('tabsselect', function (event, ui) {
            document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index
        });

    });
</script>
</asp:Content>