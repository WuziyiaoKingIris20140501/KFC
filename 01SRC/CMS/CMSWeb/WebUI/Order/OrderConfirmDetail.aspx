<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="OrderConfirmDetail.aspx.cs" Title="LM订单操作" Inherits="OrderConfirmDetail" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
<title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <link href="../../Styles/Css.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <link href="Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-1.7.1.min.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-ui-1.8.18.custom.min.js")%>'></script>
    <style type="text/css">
        .pcbackground
        {
            display: block;
            width: 100%;
            height: 100%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: while;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 2000;
        }
        .pcprogressBar
        {
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
         function BtnLoadStyle() {
             var ajaxbg = $("#background,#progressBar");
             ajaxbg.hide();
             ajaxbg.show();
         }

         function BtnCompleteStyle() {
             var ajaxbg = $("#background,#progressBar");
             ajaxbg.hide();
         }

         function BtnSendFaxLoadStyle() {
             var ajaxbg = $("#backgroundSendFax,#progressBarSendFax");
             ajaxbg.hide();
             ajaxbg.show();
         }

         function BtnSendFaxCompleteStyle() {
             var ajaxbg = $("#backgroundSendFax,#progressBarSendFax");
             ajaxbg.hide();
         }

         function PopupHotelArea() {
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
             window.open('<%=ResolveClientUrl("~/WebUI/Hotel/HotelInfoManager.aspx")%>?hid=' + document.getElementById("<%=hidHotelID.ClientID%>").value + "&time=" + time, null, fulls);
         }

         function PopupArea() {
             var time = new Date();
             //             window.open("OrderOperationPrint.aspx?ID=" + document.getElementById("<%=hidOrderID.ClientID%>").value + "&time=" + time, "NewWindow", "width=800,height=750,scrollbars=yes,resizable=no");
             window.showModalDialog("OrderOperationPrint.aspx?ID=" + document.getElementById("<%=hidOrderID.ClientID%>").value + "&time=" + time, window, "dialogWidth:800px;dialogHeight:750px;center:yes;status:no;scroll:yes;help:no;");
         }

         function ConfirmSendFax() {
             var result = window.confirm("确定发送传真？");
             if (result) {
                 return true;
                 BtnSendFaxLoadStyle();
             } else {
                 return false;
             }
         }

         function SetRefeshVal() {
             var parwin = window.dialogArguments;
             parwin.setOpenValue(document.getElementById("<%=hidOrderID.ClientID%>").value);
         }

         function OpenIssuePage() {
             var arg = document.getElementById("<%=hidOrderID.ClientID%>").value;
             var IsuNm = document.getElementById("<%=hidIssueNm.ClientID%>").value;
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
             window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=0&RID=' + arg + "&IsuNm=" + escape(IsuNm) + "&time=" + time, null, fulls);
         }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <div id="right">
        <table width="100%">
            <tr>
                <td style="width: 55%">
                    <div class="frame01">
                        <ul>
                            <li class="title">订单查询</li>
                            <li>
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="15%">
                                            订单ID：
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <asp:TextBox ID="txtOrderID" runat="server" Width="150px" MaxLength="100" />
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="查询" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div id="detailMessageContent" runat="server" style="color: red">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </li>
                            <li class="title">订单信息</li>
                            <li>
                                <table width="95%" runat="server" id="tbDetail">
                                    <tr>
                                        <td align="right" width="15%">
                                            预订时间：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbCREATE_TIME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            预订渠道：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbORDER_CHANNEL" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            价格代码：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbPRICE_CODE" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            订单状态：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbBOOK_STATUS" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            担保：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbIS_GUA" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            到店时间：
                                        </td>
                                        <td align="left">
                                            酒店规定：
                                            <asp:Label ID="lbRESV_GUA_HOLD_TIME" runat="server" />&nbsp;&nbsp; 用户选择：
                                            <asp:Label ID="lbUSER_HOLD_TIME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            担保制度：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbRESV_GUA_NM" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            取消制度：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbRESV_CXL_NM" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            支付状态：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbPAY_STATUS" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="background:#CAE1FF">
                                        <td align="right">
                                            酒店名称：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbHOTEL_NAME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="background:#CAE1FF">
                                        <td align="right">
                                            酒店电话：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbLINKTEL" runat="server" />
                                            &nbsp;&nbsp; 对应销售：
                                            <asp:Label ID="lblSalesMG" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            酒店传真：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbFax" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            入住人姓名：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbGUEST_NAMES" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            联系人姓名：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbCONTACT_NAME" runat="server" />
                                            &nbsp;&nbsp; 电话：
                                            <asp:Label ID="lbCONTACT_TEL" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            预订人电话：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbLOGIN_MOBILE" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            天数：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbOrderDays" runat="server" />天&nbsp;&nbsp;（<asp:Label ID="lbIN_DATE"
                                                runat="server" />
                                            至
                                            <asp:Label ID="lbOUT_DATE" runat="server" />）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            房型：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbROOM_TYPE_NAME" runat="server" />&nbsp;&nbsp;<asp:Label ID="lbBOOK_ROOM_NUM"
                                                runat="server" />间
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            到店时间：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbARRIVE_TIME" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            优惠券：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbTICKET_USERCODE" runat="server" />-<asp:Label ID="lbTICKET_PAGENM"
                                                runat="server" />-<asp:Label ID="lbTICKET_AMOUNT" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            补充说明：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbBOOK_REMARK" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            HVP订单号：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbORDER_NUM" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </li>
                        </ul>
                    </div>
                </td>
                <td style="width: 45%" valign="top">
                    <div class="frame01">
                        <ul>
                            <li class="title">入住详情</li>
                        </ul>
                    </div>
                    <div class="frame02">
                        <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" AutoGenerateColumns="False"
                            BackColor="White" AllowPaging="True" CellPadding="4" CellSpacing="1"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" OnRowDataBound="gridViewCSSystemLogDetail_RowDataBound"
                            OnPageIndexChanging="gridViewCSSystemLogDetail_PageIndexChanging" PageSize="10"
                            CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:BoundField DataField="INDATE" HeaderText="日期" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TWOPRICE" HeaderText="价格" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BREAKFAST" HeaderText="早餐" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                        </asp:GridView>
                    </div>
                    <div class="frame01">
                        <ul>
                            <li class="title">订单操作</li>
                            <li>
                                <table width="90%" runat="server" id="tbControl">
                                    <tr>
                                        <td align="left">
                                            订单状态：
                                        </td>
                                        <td align="left" style="width: 350px">
                                            <asp:DropDownList ID="ddpOrderStatus" CssClass="noborder_inactive" runat="server"
                                                Width="173px" OnSelectedIndexChanged="ddpOrderStatus_SelectedIndexChanged" AutoPostBack="true" />
                                            &nbsp;<asp:CheckBox ID="chkFollowUp" runat="server" Text="需跟进" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trCanlReson">
                                        <td align="left">
                                            取消原因：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddpCanelReson" CssClass="noborder_inactive" runat="server"
                                                Width="173px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="btnSet" runat="server" CssClass="btn primary" Text="确认修改" OnClientClick="BtnLoadStyle();"
                                                            OnClick="btnSet_Click" />&nbsp;
                                                        <asp:Button ID="btnUnlock" runat="server" CssClass="btn primary" Text="强制解锁" OnClientClick="BtnLoadStyle();"
                                                            OnClick="btnUnlock_Click" />&nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn primary" Text="打印订单"
                                                            OnClientClick="PopupArea()" />&nbsp;
                                                        <asp:Button ID="btnSendFax" runat="server" CssClass="btn primary" OnClientClick="return ConfirmSendFax();"
                                                            Text="发送传真" OnClick="btnSendFax_Click" Visible="false" />&nbsp;
                                                            <input type="button" class="btn primary" id="btnIssue" value='问题单' onclick="OpenIssuePage()" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div id="dvErrorInfo" runat="server" style="color: red">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <br />
                            </li>
                        </ul>
                    </div>

                    <div class="frame01">
                        <ul>
                            <li class="title">订单操作备注</li>
                            <li>
                            <table width="90%" runat="server" id="Table1">
                                    <tr>
                                        <td align="left" style="width: 100px">
                                            备注：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBOOK_REMARK" runat="server" TextMode="MultiLine" Width="300px"
                                                Height="30px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 100px">
                                            备注历史：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbMemo1" runat="server" />
                                        </td>
                                    </tr>
                             </table>
                             <br />
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <div id="backgroundSendFax" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBarSendFax" class="pcprogressBar" style="display: none;">
        传真发送中，请稍等...</div>
    <asp:HiddenField ID="hidOrderID" runat="server" />
    <asp:HiddenField ID="hidPriceCode" runat="server" />
    <asp:HiddenField ID="hidHotelID" runat="server" />
    <asp:HiddenField ID="hidIssueNm" runat="server" />
</form>
</body>
</html>