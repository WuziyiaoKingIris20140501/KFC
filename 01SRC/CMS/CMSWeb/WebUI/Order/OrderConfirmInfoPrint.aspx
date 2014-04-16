<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderConfirmInfoPrint.aspx.cs" Inherits="OrderConfirmInfoPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <style type="text/css">
    #blackcircle { 
    position:absolute;
    width: 50px; 
    height: 50px; 
    background: black; 
    -moz-border-radius: 50px; 
    -webkit-border-radius: 50px; 
    border-radius: 50px; 
    z-index:5;
    margin-left:0px;
    position:absolute; left:190px; top:25px;
    
    } 

    #whitecircle { 
    width: 30px; 
    height: 30px; 
    background: white; 
    -moz-border-radius: 50px; 
    -webkit-border-radius: 50px; 
    border-radius: 50px; 
    z-index:10;
    position:absolute; left:190px; top:25px;
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 650px">
        <tr style="height: 20px">
            <td align="right" style="font-family: 黑体, monospace; mso-font-charset: 134; height: 20px;
                text-decoration: underline;">
                订单号：<asp:Label ID="lblOrderNumTip" runat="server" Text=""/>
            </td>
        </tr>
        <tr style="height: 55px">
            <td align="right" valign="middle" style="font-size: 24.0pt; font-family: 黑体, monospace;
                mso-font-charset: 134; height: 55px;">
                    <table>
                    <tr>
                        <td><img src="../../Images/hotelvp-logo.jpg" alt="logo" /></td>
                        <td style="font-size: 24.0pt; font-family: 黑体, monospace;
                mso-font-charset: 134; height: 55px;">今夜酒店特价 - 预订通知单</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                 电话：<span style="width: 10px">4001051419 </span>传真：02155344588
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 30px;">
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                TO： <asp:Label ID="lblHotelName" runat="server" Text=""/>（ID：<asp:Label ID="lblHotelID" runat="server" Text=""/>）
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" valign="top" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 30px;">
                <span style='margin-left: 40px'>电话：<asp:Label ID="lblHTel" runat="server" Text=""/><span style='width: 10px'> </span>传真：<asp:Label ID="lblHFax" runat="server" Text=""/></span>
            </td>
        </tr>
<%--        <tr style="height: 3px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: none; border-right: none; border-bottom: 2.0pt double black; border-left: none;
                height: 3px;">&nbsp;
            </td>
        </tr>--%>
        <tr style="height:45px">
             <td align="left" valign="middle" style="font-family:黑体, monospace;mso-font-charset:134;height:45px;border-top: 2.0pt double black;border-bottom: 2.0pt double black;">
                 <table style="width: 100%">
                     <tr style="height: 20px; width: 100%;" valign="middle">
                         <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                             width: 15%;">
                             订单号：
                         </td>
                         <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                             width: 40%;">
                             <asp:Label ID="lblOrderNum" runat="server" Text=""/>
                         </td>
                         <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                             width: 15%;">
                             发送时间：
                         </td>
                         <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                             width: 30%;">
                             <asp:Label ID="lblSystemDate" runat="server" Text=""/>
                         </td>
                     </tr>
                 </table>
             </td>
         </tr>
       <%-- <tr style="height: 3px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: 2.0pt double black; border-right: none; border-bottom: none; border-left: none;height: 3px;">&nbsp;
            </td>
        </tr>--%>
        
        <tr>
            <td>
                <table style="width: 100%;margin-top:12px;">
                    <tr style="height: 20px; width: 100%;">
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 15%;">
                            客人姓名：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 40%;">
                            <asp:Label ID="lblCustomerName" runat="server" Text=""/>
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 15%;">
                            客人电话：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 30%;">
                            <asp:Label ID="lblCustomerTel" runat="server" Text=""/>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            预订房型：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lblRoomTypeName" runat="server" Text=""/>
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            预订价格：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lblPriceCount" runat="server" Text=""/>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            入住日期：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lblInDate" runat="server" Text=""/>
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            离店日期：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lblOutDate" runat="server" Text=""/>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            预订房量：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lblRoomNum" runat="server" Text=""/>
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            入住天数：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lblInDayNum" runat="server" Text=""/>
                        </td>
                    </tr>
                </table>
            </td>
         </tr>
        <tr style="height: 10px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 10px;">
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr style="width: 100%">
                        <td style="width: 100%">
                            <table style="width: 100%; border: 1px black solid; padding: 1px; border-collapse: collapse;
                                border-spacing: 0;">
                                <tr height="33" style="mso-height-source: userset; height: 24.95pt">
                                    <td align="center" valign="middle" style='font-family: 黑体, monospace; mso-font-charset: 134;
                                        border-right: 1px black solid; border-top: 1px black solid; width: 40%'>
                                        入住日期
                                    </td>
                                    <td align="center" valign="middle" style='border: 0.5pt solid black; font-family: 黑体,monospace;
                                        border-right: 1px black solid; border-top: 1px black solid; width: 20%'>
                                        单价
                                    </td>
                                    <td align="center" valign="middle" style='border: 0.5pt solid black; font-family: 黑体,monospace;
                                        border-right: 1px black solid; border-top: 1px black solid; width: 20%'>
                                        总天数
                                    </td>
                                    <td align="center" valign="middle" style='font-family: 黑体, monospace; border-right: 1px black solid;
                                        border-top: 1px black solid; width: 20%'>
                                        早餐数
                                    </td>
                                </tr>
                                <tr height="33" style="mso-height-source: userset; height: 24.95pt">
                                    <td align="center" valign="middle" style='font-family: 黑体, monospace; mso-font-charset: 134;
                                        border-right: 1px black solid; border-top: 1px black solid; width: 40%'>
                                        <asp:Label ID="lblDetialInOutDT" runat="server" Text=""/>
                                    </td>
                                    <td align="center" valign="middle" style='border: 0.5pt solid black; font-family: 黑体,monospace;
                                        border-right: 1px black solid; border-top: 1px black solid; width: 20%'>
                                        <asp:Label ID="lblDetialTwoPrice" runat="server" Text=""/>
                                    </td>
                                    <td align="center" valign="middle" style='font-family: 黑体, monospace; border-right: 1px black solid;
                                        border-top: 1px black solid; width: 20%'>
                                        <asp:Label ID="lblDetialTotDays" runat="server" Text=""/>
                                    </td>
                                    <td align="center" valign="middle" style='font-family: 黑体, monospace; border-right: 1px black solid;
                                        border-top: 1px black solid; width: 20%'>
                                        <asp:Label ID="lblDetialBreaks" runat="server" Text=""/>
                                    </td>
                                </tr>
                                <tr height="33" style="mso-height-source: userset; height: 24.95pt">
                                    <td align="left" colspan="4" valign="middle" style='font-family: 黑体, monospace; mso-font-charset: 134;
                                        border-right: 1px black solid; border-top: 1px black solid; width: 40%;'>
                                        <span style="margin-left: 30px;">订单总价：<asp:Label ID="lblDetailPriceCount" runat="server" Text=""/> &nbsp;&nbsp;&nbsp;&nbsp; 支付方式: <asp:Label ID="lblPriceCode" runat="server" Text=""/></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
         </tr>
        <tr style="height: 20px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                订单至少保留两个小时，如过时不能保留订单，请事先致电
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                客人备注：<asp:Label ID="lblCusRemark" runat="server" Text=""/>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                预订备注：<asp:Label ID="lblOrderRemark" runat="server" Text=""/>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;border-bottom: 2.0pt double black; ">&nbsp;
            </td>
        </tr>
<%--        <tr>
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: none; border-right: none; border-bottom: 2.0pt double black; border-left: none;">&nbsp;
            </td>
        </tr>--%>
        <tr style="height: 68px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 68px;">
                <table style="width: 100%">
                    <tr style="width: 100%; height: 34px">
                        <td style="height: 34px; width: 50%;">
                            [ ]本单确认
                        </td>
                        <td style="height: 34px; width: 50%;">
                            [ ]本单不确认
                        </td>
                    </tr>
                    <tr style="width: 100%; height: 34px">
                        <td style="height: 34px;width:50%">
                            <div style="float: left">
                                确认号：</div>
                            <div style="float: left; border-top: none; border-right: none; border-bottom: 1.0pt solid black;
                                border-left: none; width: 200px; height: 18px">
                            </div>
                        </td>
                        <td style="height: 34px;width:50%">
                            <div style="float: left;">
                                确认人：</div>
                            <div style="float: left; border-top: none; border-right: none; border-bottom: 1.0pt solid black;
                                border-left: none; width: 200px; height: 18px">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       <%-- <tr style="height: 3px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: 2.0pt double black; border-right: none; border-bottom: none; border-left: none;height: 3px">&nbsp;
            </td>
        </tr>--%>
        <tr style="height: 48px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 48px;border-top: 2.0pt double black;border-bottom: 2.0pt double black;">
                <span style="margin-left: 5px">担保信息：<asp:Label ID="lblIsGUA" runat="server" Text=""/></span>
            </td>
        </tr>
        <%--<tr style="height: 3px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: none; border-right: none; border-bottom: 2.0pt double black; border-left: none;height: 3px">&nbsp;
            </td>
        </tr>--%>
        <tr style="height: 38px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 38px;">
                <span style="margin-left: 5px">请在 <asp:Label ID="lblFullDT" runat="server" Text=""/> 满房的房型上画勾，谢谢合作！</span>
            </td>
        </tr>
        <tr style="height: 38px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 38px;">
                <span style="margin-left: 5px;" id="spRoom" runat="server"></span>
                <%--[ ]标准间-200元/晚&nbsp; &nbsp; &nbsp; &nbsp;[ ]大床房-300元/晚&nbsp;
                    &nbsp; &nbsp; &nbsp; [ ]商务套房-400元/晚&nbsp; &nbsp; &nbsp; &nbsp;--%>
            </td>
        </tr>
        <tr style="height: 38px">
            <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 48px;border-top: 2.0pt double black;">
                <span>房态如有变化，请致电房控专线：18001633253 / 18001633397</span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
