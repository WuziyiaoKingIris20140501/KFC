<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <b></b>
    <link href="Scripts/skin/WdatePicker.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/WdatePicker.js")%>'></script>

    <script language="javascript" type="text/javascript">
        function PopupArea(type, pc, pf, tk, oc) {
            var time = new Date();
            if (type == "0") {
                window.location.href = "./WebUI/DBQuery/LmOrderLogPage.aspx?State=0&PC=" + pc + "&PF=" + pf + "&TK=" + tk + "&OC=" + oc + "&DTStart=" + document.getElementById("<%=hidOrderStartDate.ClientID%>").value + "&DTEnd=" + document.getElementById("<%=hidOrderEndDate.ClientID%>").value;
            } else {
                window.location.href = "./WebUI/DBQuery/LmOrderLogPage.aspx?State=1&PC=" + pc + "&PF=" + pf + "&TK=" + tk + "&OC=" + oc + "&DTStart=" + document.getElementById("<%=hidStartDate.ClientID%>").value + "&DTEnd=" + document.getElementById("<%=hidEndDate.ClientID%>").value;
            }
        }

    </script>
    <div id="right" runat="server">
        <div class="frame01">
            <ul>
                <li class="title" style="display: none">&nbsp;&nbsp;
                    <asp:LinkButton runat="server" Text="昨夜" ID="lkOverallYesterDay" OnClick="lkOverallYesterDay_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" Text="今夜" ID="lkOverallToDay" OnClick="lkOverallToDay_Click" />
                </li>
                <li>
                    <input id="StartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_EndDate\')||\'2020-10-01\'}'})"
                        runat="server" />
                    至：
                    <input id="EndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_StartDate\')}',maxDate:'2020-10-01'})"
                        runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSeach" runat="server" Text="确定" CssClass="btn primary" OnClientClick="return checkValid();" onclick="btnSeach_Click" />
                </li>
            </ul>
        </div>
        <div style="display:none;" id="divMain" runat="server">
        <div class="frame01">
            <ul>
                <li class="title">渠道统计&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" Text="昨夜" ID="lkChannelYesterDay" OnClick="lkChannelYesterDay_Click"
                        Visible="false" />&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" Text="今夜" ID="lkChannelToDay" OnClick="lkChannelToDay_Click"
                        Visible="false" /></li>
                <li>
                    <table width="95%">
                        <tr>
                            <td style="width: 70%" align="left" valign="top">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:HiddenField ID="hidStartDate" runat="server" />
                                            <asp:HiddenField ID="hidEndDate" runat="server" />
                                            <asp:Label ID="Label1" runat="server" Text="" />
                                        </td>
                                        <td align="center" style="width: 180px">
                                            总订单（券）：<a id="A37" href="#" runat="server" onclick="PopupArea('1','ALL,','','','')"><asp:Label
                                                ID="Label2" runat="server" Text="0" /></a><a id="A38" href="#" runat="server" onclick="PopupArea('1','ALL','','1','')"><asp:Label
                                                    ID="Label3" runat="server" Text="0" /></a> 张
                                        </td>
                                        <td align="center" style="width: 200px">
                                            订单均价：<asp:Label ID="Label4" runat="server" Text="0" />
                                            元
                                        </td>
                                        <td align="right" style="width: 180px">
                                            总成功单（券）：<a id="A39" href="#" runat="server" onclick="PopupArea('1','ALL,','','','1,4,5,6,7,8')"><asp:Label
                                                ID="Label5" runat="server" Text="0" /></a><a id="A40" href="#" runat="server" onclick="PopupArea('1','ALL,','','1','1,4,5,6,7,8')"><asp:Label
                                                    ID="Label6" runat="server" Text="0" /></a> 张
                                        </td>
                                        <td align="center" style="width: 200px">
                                            订单均价：<asp:Label ID="Label9" runat="server" Text="0" />
                                            元
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 148px">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 148px">
                                        </td>
                                        <td align="center" style="width: 100px; border: maroon 3 double">
                                            总订单(券)
                                        </td>
                                        <td align="center" style="width: 100px">
                                            确认可住单(券)
                                        </td>
                                        <td align="center" style="width: 100px">
                                            待确认单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            CC取消单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            用户取消单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            其他(券)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜酒店特价IOS：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A41" href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','','')">
                                                <asp:Label ID="lblIOSChannelOrderAll" runat="server" Text="0" /></a> <a id="A91"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','')">
                                                    <asp:Label ID="lblIOSChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A42" href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','','4,5,6,7,8')">
                                                <asp:Label ID="lblIOSChannelAffirmOrder" runat="server" Text="0" /></a> <a id="A92"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblIOSChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A43" href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','','1')">
                                                <asp:Label ID="lblIOSChannelNotAffirmOrder" runat="server" Text="0" /></a> <a id="A93"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','1')">
                                                    <asp:Label ID="lblIOSChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A44" href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','','9')">
                                                <asp:Label ID="lblIOSChannelcc" runat="server" Text="0" /></a> <a id="A94" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','9')">
                                                    <asp:Label ID="lblIOSChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A1" href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','','3')">
                                                <asp:Label ID="lblIOSChannelOther" runat="server" Text="0" /></a> <a id="A95" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','3')">
                                                    <asp:Label ID="lblIOSChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A2" href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','','2,0')">
                                                <asp:Label ID="lblIOSrest" runat="server" Text="0" /></a> <a id="A96" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,IOS','','1','2,0')">
                                                    <asp:Label ID="lblIOSrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜酒店特价Android：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A45" href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','','')">
                                                <asp:Label ID="lblAdrChannelOrderAll" runat="server" Text="0" /></a> <a id="A97"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','1','')">
                                                    <asp:Label ID="lblAdrChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A46" href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','','4,5,6,7,8')">
                                                <asp:Label ID="lblADRChannelAffirmOrder" runat="server" Text="0" /></a> <a id="A98"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblADRChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A47" href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','','1')">
                                                <asp:Label ID="lblADRChannelNotAffirmOrder" runat="server" Text="0" /></a> <a id="A99"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','1','1')">
                                                    <asp:Label ID="lblADRChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A48" href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','','9')">
                                                <asp:Label ID="lblADRChannelcc" runat="server" Text="0" /></a> <a id="A100" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','1','9')">
                                                    <asp:Label ID="lblADRChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A3" href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','','3')">
                                                <asp:Label ID="lblADRChannelOther" runat="server" Text="0" /></a> <a id="A101" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','1','3')">
                                                    <asp:Label ID="lblADRChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A4" href="#" runat="server" onclick="PopupArea('1','HOTELVP,ANDROID','','','2,0')">
                                                <asp:Label ID="lblADRrest" runat="server" Text="0" /></a> <a id="A102" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,ANDROID','','1','2,0')">
                                                    <asp:Label ID="lblADRrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜酒店特价WP7：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A49" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','')">
                                                <asp:Label ID="lblWPChannelOrderAll" runat="server" Text="0" /></a> <a id="A103"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','')">
                                                    <asp:Label ID="lblWPChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A50" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','4,5,6,7,8')">
                                                <asp:Label ID="lblWPChannelAffirmOrder" runat="server" Text="0" /></a> <a id="A104"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblWPChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A51" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','1')">
                                                <asp:Label ID="lblWPChannelNotAffirmOrder" runat="server" Text="0" /></a> <a id="A105"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','1')">
                                                    <asp:Label ID="lblWPChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A52" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','9')">
                                                <asp:Label ID="lblWPChannelcc" runat="server" Text="0" /></a> <a id="A106" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','9')">
                                                    <asp:Label ID="lblWPChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A5" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','3')">
                                                <asp:Label ID="lblWPChannelOther" runat="server" Text="0" /></a> <a id="A107" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','3')">
                                                    <asp:Label ID="lblWPChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A6" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','2,0')">
                                                <asp:Label ID="lblWPrest" runat="server" Text="0" /></a> <a id="A108" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,WP','','1','2,0')">
                                                    <asp:Label ID="lblWPrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜酒店特价W8：</td>
                                        <td style="width: 100px" align="center">
                                            <a id="A212" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','')">
                                                <asp:Label ID="lblW8ChannelOrderAll" runat="server" Text="0" /></a> <a id="A213"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','')">
                                                    <asp:Label ID="lblW8ChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A214" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','4,5,6,7,8')">
                                                <asp:Label ID="lblW8ChannelAffirmOrder" runat="server" Text="0" /></a> <a id="A215"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblW8ChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A216" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','1')">
                                                <asp:Label ID="lblW8ChannelNotAffirmOrder" runat="server" Text="0" /></a> <a id="A217"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','1')">
                                                    <asp:Label ID="lblW8ChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A218" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','9')">
                                                <asp:Label ID="lblW8Channelcc" runat="server" Text="0" /></a> <a id="A219" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','9')">
                                                    <asp:Label ID="lblW8ChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A220" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','3')">
                                                <asp:Label ID="lblW8ChannelOther" runat="server" Text="0" /></a> <a id="A221" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WP','','1','3')">
                                                    <asp:Label ID="lblW8ChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A222" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WP','','','2,0')">
                                                <asp:Label ID="lblW8rest" runat="server" Text="0" /></a> <a id="A223" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,WP','','1','2,0')">
                                                    <asp:Label ID="lblW8restCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜酒店特价WAP：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A25" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','','')">
                                                <asp:Label ID="lblWAPChannelOrderAll" runat="server" Text="0" /></a><a id="A109"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','')">
                                                    <asp:Label ID="lblWAPChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A26" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','','4,5,6,7,8')">
                                                <asp:Label ID="lblWAPChannelAffirmOrder" runat="server" Text="0" /></a><a id="A110"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblWAPChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A27" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','','1')">
                                                <asp:Label ID="lblWAPChannelNotAffirmOrder" runat="server" Text="0" /><a id="A111"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','1')">
                                                    <asp:Label ID="lblWAPChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A28" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','','9')">
                                                <asp:Label ID="lblWAPChannelcc" runat="server" Text="0" /></a><a id="A112" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','9')">
                                                    <asp:Label ID="lblWAPChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A89" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','','3')">
                                                <asp:Label ID="lblWAPChannelOther" runat="server" Text="0" /></a><a id="A113" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','3')">
                                                    <asp:Label ID="lblWAPChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A90" href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','','2,0')">
                                                <asp:Label ID="lblWAPrest" runat="server" Text="0" /></a><a id="A114" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,WAP','','1','2,0')">
                                                    <asp:Label ID="lblWAPrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜酒店特价Pro：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A200" href="#" runat="server" onclick="PopupArea('1','HOTELVPPRO','','','')">
                                                <asp:Label ID="lblProChannelOrderAll" runat="server" Text="0" /></a><a id="A201"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','')">
                                                    <asp:Label ID="lblProChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A202" href="#" runat="server" onclick="PopupArea('1','HOTELVPPRO','','','4,5,6,7,8')">
                                                <asp:Label ID="lblProChannelAffirmOrder" runat="server" Text="0" /></a><a id="A203"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblProChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A204" href="#" runat="server" onclick="PopupArea('1','HOTELVPPRO','','','1')">
                                                <asp:Label ID="lblProChannelNotAffirmOrder" runat="server" Text="0" /><a id="A205"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','1')">
                                                    <asp:Label ID="lblProChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A206" href="#" runat="server" onclick="PopupArea('1','HOTELVPPRO','','','9')">
                                                <asp:Label ID="lblProChannelcc" runat="server" Text="0" /></a><a id="A207" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','9')">
                                                    <asp:Label ID="lblProChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A208" href="#" runat="server" onclick="PopupArea('1','HOTELVPPRO','','','3')">
                                                <asp:Label ID="lblProChannelOther" runat="server" Text="0" /></a><a id="A209" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,WAP','','1','3')">
                                                    <asp:Label ID="lblProChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A210" href="#" runat="server" onclick="PopupArea('1','HOTELVPPRO','','','2,0')">
                                                <asp:Label ID="lblProrest" runat="server" Text="0" /></a><a id="A211" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,WAP','','1','2,0')">
                                                    <asp:Label ID="lblProrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            今夜特价开房：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A179" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','')">
                                                <asp:Label ID="lblGETAROOMOrderAll" runat="server" Text="0" /></a><a id="A180" href="#"
                                                    runat="server" onclick="PopupArea('1','GETAROOM,','','1','')">
                                                    <asp:Label ID="lblGETAROOMOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A183" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','4,5,6,7,8')">
                                                <asp:Label ID="lblGETAROOMAffirmOrder" runat="server" Text="0" /></a><a id="A184"
                                                    href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblGETAROOMAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A187" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','1')">
                                                <asp:Label ID="lblGETAROOMNotAffirmOrder" runat="server" Text="0" /></a><a id="A188"
                                                    href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','1','1')">
                                                    <asp:Label ID="lblGETAROOMNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A191" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','9')">
                                                <asp:Label ID="lblGETAROOMChannelcc" runat="server" Text="0" /></a><a id="A192" href="#"
                                                    runat="server" onclick="PopupArea('1','GETAROOM,','','1','9')">
                                                    <asp:Label ID="lblGETAROOMChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A195" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','3')">
                                                <asp:Label ID="lblGETAROOMChannelOther" runat="server" Text="0" /></a><a id="A196"
                                                    href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','1','3')">
                                                    <asp:Label ID="lblGETAROOMChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A198" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','2,0')">
                                                <asp:Label ID="lblGETAROOMrest" runat="server" Text="0" /></a><a id="A199" href="#"
                                                    runat="server" onclick="PopupArea('1','GETAROOM,','','1','2,0')">
                                                    <asp:Label ID="lblGETAROOMrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td style="width: 148px">
                                            号码百事通：</td>
                                        <td style="width: 100px" align="center">
                                            <a id="A224" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','')">
                                                <asp:Label ID="lblHMBSTOrderAll" runat="server" Text="0" /></a><a id="A225" href="#"
                                                    runat="server" onclick="PopupArea('1','GETAROOM,','','1','')">
                                                    <asp:Label ID="lblHMBSTOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A226" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','4,5,6,7,8')">
                                                <asp:Label ID="lblHMBSTAffirmOrder" runat="server" Text="0" /></a><a id="A227"
                                                    href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblHMBSTAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A228" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','1')">
                                                <asp:Label ID="lblHMBSTNotAffirmOrder" runat="server" Text="0" /></a><a id="A229"
                                                    href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','1','1')">
                                                    <asp:Label ID="lblHMBSTNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A230" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','9')">
                                                <asp:Label ID="lblHMBSTChannelcc" runat="server" Text="0" /></a><a id="A231" href="#"
                                                    runat="server" onclick="PopupArea('1','GETAROOM,','','1','9')">
                                                    <asp:Label ID="lblHMBSTChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A232" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','3')">
                                                <asp:Label ID="lblHMBSTChannelOther" runat="server" Text="0" /></a><a id="A233"
                                                    href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','1','3')">
                                                    <asp:Label ID="lblHMBSTChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A234" href="#" runat="server" onclick="PopupArea('1','GETAROOM,','','','2,0')">
                                                <asp:Label ID="lblHMBSTrest" runat="server" Text="0" /></a><a id="A235" href="#"
                                                    runat="server" onclick="PopupArea('1','GETAROOM,','','1','2,0')">
                                                    <asp:Label ID="lblHMBSTrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            Qunar：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A53" href="#" runat="server" onclick="PopupArea('1','QUNAR,','','','')">
                                                <asp:Label ID="lblQUNarChannelOrderAll" runat="server" Text="0" /></a><a id="A115"
                                                    href="#" runat="server" onclick="PopupArea('1','QUNAR,','','1','')">
                                                    <asp:Label ID="lblQUNarChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A54" href="#" runat="server" onclick="PopupArea('1','QUNAR,','','','4,5,6,7,8')">
                                                <asp:Label ID="lblQUNarChannelAffirmOrder" runat="server" Text="0" /></a><a id="A116"
                                                    href="#" runat="server" onclick="PopupArea('1','QUNAR,','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblQUNarChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A55" href="#" runat="server" onclick="PopupArea('1','QUNAR,','','','1')">
                                                <asp:Label ID="lblQUNarChannelNotAffirmOrder" runat="server" Text="0" /></a><a id="A117"
                                                    href="#" runat="server" onclick="PopupArea('1','QUNAR,','','1','1')">
                                                    <asp:Label ID="lblQUNarChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A56" href="#" runat="server" onclick="PopupArea('1','QUNAR,','','','9')">
                                                <asp:Label ID="lblQUNarChannelcc" runat="server" Text="0" /></a><a id="A118" href="#"
                                                    runat="server" onclick="PopupArea('1','QUNAR,','','1','9')">
                                                    <asp:Label ID="lblQUNarChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A7" href="#" runat="server" onclick="PopupArea('1','QUNAR,','','','3')">
                                                <asp:Label ID="lblQUNarChannelOther" runat="server" Text="0" /></a><a id="A119" href="#"
                                                    runat="server" onclick="PopupArea('1','QUNAR,','','1','3')">
                                                    <asp:Label ID="lblQUNarChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A8" href="#" runat="server" onclick="PopupArea('1','QUNAR,','','','2,0')">
                                                <asp:Label ID="lblQUNarrest" runat="server" Text="0" /></a><a id="A120" href="#"
                                                    runat="server" onclick="PopupArea('1','QUNAR,','','1','2,0')">
                                                    <asp:Label ID="lblQUNarrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td style="width: 148px">
                                            116114：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A57" href="#" runat="server" onclick="PopupArea('1','116114,','','','')">
                                                <asp:Label ID="lbl11ChannelOrderAll" runat="server" Text="0" /></a><a id="A121" href="#"
                                                    runat="server" onclick="PopupArea('1','116114,','','1','')">
                                                    <asp:Label ID="lbl11ChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A58" href="#" runat="server" onclick="PopupArea('1','116114,','','','4,5,6,7,8')">
                                                <asp:Label ID="lbl11ChannelAffirmOrder" runat="server" Text="0" /></a><a id="A122"
                                                    href="#" runat="server" onclick="PopupArea('1','116114,','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lbl11ChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A59" href="#" runat="server" onclick="PopupArea('1','116114,','','','1')">
                                                <asp:Label ID="lbl11ChannelNotAffirmOrder" runat="server" Text="0" /></a><a id="A123"
                                                    href="#" runat="server" onclick="PopupArea('1','116114,','','1','1')">
                                                    <asp:Label ID="lbl11ChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A60" href="#" runat="server" onclick="PopupArea('1','116114,','','','9')">
                                                <asp:Label ID="lbl11Channelcc" runat="server" Text="0" /></a><a id="A124" href="#"
                                                    runat="server" onclick="PopupArea('1','116114,','','1','9')">
                                                    <asp:Label ID="lbl11ChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A9" href="#" runat="server" onclick="PopupArea('1','116114,','','','3')">
                                                <asp:Label ID="lbl11ChannelOther" runat="server" Text="0" /></a><a id="A125" href="#"
                                                    runat="server" onclick="PopupArea('1','116114,','','1','3')">
                                                    <asp:Label ID="lbl11ChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A10" href="#" runat="server" onclick="PopupArea('1','116114,','','','2,0')">
                                                <asp:Label ID="lbl11rest" runat="server" Text="0" /></a><a id="A126" href="#" runat="server"
                                                    onclick="PopupArea('1','116114,','','1','2,0')">
                                                    <asp:Label ID="lbl11restCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td style="width: 148px">
                                            MoJi：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A33" href="#" runat="server" onclick="PopupArea('1','MOJI,','','','')">
                                                <asp:Label ID="lblMJChannelOrderAll" runat="server" Text="0" /></a><a id="A127" href="#"
                                                    runat="server" onclick="PopupArea('1','MOJI,','','1','')">
                                                    <asp:Label ID="lblMJChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A34" href="#" runat="server" onclick="PopupArea('1','MOJI,','','','4,5,6,7,8')">
                                                <asp:Label ID="lblMJChannelAffirmOrder" runat="server" Text="0" /></a><a id="A128"
                                                    href="#" runat="server" onclick="PopupArea('1','MOJI,','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblMJChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A35" href="#" runat="server" onclick="PopupArea('1','MOJI,','','','1')">
                                                <asp:Label ID="lblMJChannelNotAffirmOrder" runat="server" Text="0" /></a><a id="A129"
                                                    href="#" runat="server" onclick="PopupArea('1','MOJI,','','1','1')">
                                                    <asp:Label ID="lblMJChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A36" href="#" runat="server" onclick="PopupArea('1','MOJI,','','','9')">
                                                <asp:Label ID="lblMJChannelcc" runat="server" Text="0" /></a><a id="A130" href="#"
                                                    runat="server" onclick="PopupArea('1','MOJI,','','1','9')">
                                                    <asp:Label ID="lblMJChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A11" href="#" runat="server" onclick="PopupArea('1','MOJI,','','','3')">
                                                <asp:Label ID="lblMJChannelOther" runat="server" Text="0" /></a><a id="A131" href="#"
                                                    runat="server" onclick="PopupArea('1','MOJI,','','1','3')">
                                                    <asp:Label ID="lblMJChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A12" href="#" runat="server" onclick="PopupArea('1','MOJI,','','','2,0')">
                                                <asp:Label ID="lblMJrest" runat="server" Text="0" /></a><a id="A132" href="#" runat="server"
                                                    onclick="PopupArea('1','MOJI,','','1','2,0')">
                                                    <asp:Label ID="lblMJrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="width: 148px">
                                            快捷酒店：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A236" href="#" runat="server" onclick="PopupArea('1','HOTELVPMAP','','','')">
                                                <asp:Label ID="lblHotelvpMapChannelOrderAll" runat="server" Text="0" /></a><a id="A237"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','')">
                                                    <asp:Label ID="lblHotelvpMapChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A238" href="#" runat="server" onclick="PopupArea('1','HOTELVPMAP','','','4,5,6,7,8')">
                                                <asp:Label ID="lblHotelvpMapChannelAffirmOrder" runat="server" Text="0" /></a><a id="A239"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblHotelvpMapChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A240" href="#" runat="server" onclick="PopupArea('1','HOTELVPMAP','','','1')">
                                                <asp:Label ID="lblHotelvpMapChannelNotAffirmOrder" runat="server" Text="0" /><a id="A241"
                                                    href="#" runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','1')">
                                                    <asp:Label ID="lblHotelvpMapChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A242" href="#" runat="server" onclick="PopupArea('1','HOTELVPMAP','','','9')">
                                                <asp:Label ID="lblHotelvpMapChannelcc" runat="server" Text="0" /></a><a id="A243" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','9')">
                                                    <asp:Label ID="lblHotelvpMapChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A244" href="#" runat="server" onclick="PopupArea('1','HOTELVPMAP','','','3')">
                                                <asp:Label ID="lblHotelvpMapChannelOther" runat="server" Text="0" /></a><a id="A245" href="#"
                                                    runat="server" onclick="PopupArea('1','HOTELVP,IOS','','1','3')">
                                                    <asp:Label ID="lblHotelvpMapChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A246" href="#" runat="server" onclick="PopupArea('1','HOTELVPMAP','','','2,0')">
                                                <asp:Label ID="lblHotelvpMaprest" runat="server" Text="0" /></a><a id="A247" href="#" runat="server"
                                                    onclick="PopupArea('1','HOTELVP,IOS','','1','2,0')">
                                                    <asp:Label ID="lblHotelvpMaprestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td style="width: 148px">
                                            其他：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A248" href="#" runat="server" onclick="PopupArea('1','Rests','','','')">
                                                <asp:Label ID="lblRestsChannelOrderAll" runat="server" Text="0" /></a><a id="A249"
                                                    href="#" runat="server" onclick="PopupArea('1','Rests','','1','')">
                                                    <asp:Label ID="lblRestsChannelOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A250" href="#" runat="server" onclick="PopupArea('1','Rests','','','4,5,6,7,8')">
                                                <asp:Label ID="lblRestsChannelAffirmOrder" runat="server" Text="0" /></a><a id="A251"
                                                    href="#" runat="server" onclick="PopupArea('1','Rests','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblRestsChannelAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A252" href="#" runat="server" onclick="PopupArea('1','Rests','','','1')">
                                                <asp:Label ID="lblRestsChannelNotAffirmOrder" runat="server" Text="0" /><a id="A253"
                                                    href="#" runat="server" onclick="PopupArea('1','Rests','','1','1')">
                                                    <asp:Label ID="lblRestsChannelNotAffirmOrderCode" runat="server" Text="(0)" /></a></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A254" href="#" runat="server" onclick="PopupArea('1','Rests','','','9')">
                                                <asp:Label ID="lblRestsChannelcc" runat="server" Text="0" /></a><a id="A255" href="#"
                                                    runat="server" onclick="PopupArea('1','Rests','','1','9')">
                                                    <asp:Label ID="lblRestsChannelccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A256" href="#" runat="server" onclick="PopupArea('1','Rests','','','3')">
                                                <asp:Label ID="lblRestsChannelOther" runat="server" Text="0" /></a><a id="A257" href="#"
                                                    runat="server" onclick="PopupArea('1','Rests','','1','3')">
                                                    <asp:Label ID="lblRestsChannelOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A258" href="#" runat="server" onclick="PopupArea('1','Rests','','','2,0')">
                                                <asp:Label ID="lblRestsrest" runat="server" Text="0" /></a><a id="A259" href="#" runat="server"
                                                    onclick="PopupArea('1','Rests','','1','2,0')">
                                                    <asp:Label ID="lblRestsrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 148px">
                                            总数（券）：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A21" href="#" runat="server" onclick="PopupArea('1','ALL,','','','')">
                                                <asp:Label ID="lblChannelOrderAllCount" runat="server" Text="0" /></a><a id="A29"
                                                    href="#" runat="server" onclick="PopupArea('1','ALL,','','1','')">
                                                    <asp:Label ID="lblChannelOrderAllCodeCount" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A22" href="#" runat="server" onclick="PopupArea('1','ALL,','','','4,5,6,7,8')">
                                                <asp:Label ID="lblChannelAffirmOrderCount" runat="server" Text="0" /></a><a id="A30"
                                                    href="#" runat="server" onclick="PopupArea('1','ALL,','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblChannelAffirmOrderCodeCount" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A23" href="#" runat="server" onclick="PopupArea('1','ALL,','','','1')">
                                                <asp:Label ID="lblChannelNotAffirmOrderCount" runat="server" Text="0" /></a><a id="A31"
                                                    href="#" runat="server" onclick="PopupArea('1','ALL,','','1','1')">
                                                    <asp:Label ID="lblChannelNotAffirmOrderCodeCount" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A24" href="#" runat="server" onclick="PopupArea('1','ALL,','','','9')">
                                                <asp:Label ID="lblChannelccCount" runat="server" Text="0" /></a><a id="A32" href="#"
                                                    runat="server" onclick="PopupArea('1','ALL,','','1','9')">
                                                    <asp:Label ID="lblChannelccCodeCount" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A13" href="#" runat="server" onclick="PopupArea('1','ALL,','','','3')">
                                                <asp:Label ID="lblChannelOtherCount" runat="server" Text="0" /></a><a id="A133" href="#"
                                                    runat="server" onclick="PopupArea('1','ALL,','','1','3')">
                                                    <asp:Label ID="lblChannelOtherCodeCount" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A14" href="#" runat="server" onclick="PopupArea('1','ALL,','','','2,0')">
                                                <asp:Label ID="lblrestCount" runat="server" Text="0" /></a><a id="A134" href="#"
                                                    runat="server" onclick="PopupArea('1','ALL,','','1','2,0')">
                                                    <asp:Label ID="lblrestCodeCount" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            %：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A15" href="#" runat="server" onclick="PopupArea('1','ALL,','','','')">
                                                <asp:Label ID="lblChannelOrderAllPercent" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A16" href="#" runat="server" onclick="PopupArea('1','ALL,','','','4,5,6,7,8')">
                                                <asp:Label ID="lblChannelAffirmOrderPercent" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A17" href="#" runat="server" onclick="PopupArea('1','ALL,','','','1')">
                                                <asp:Label ID="lblChannelNotAffirmOrderPercent" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A18" href="#" runat="server" onclick="PopupArea('1','ALL,','','','9')">
                                                <asp:Label ID="lblChannelccCodePercent" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A19" href="#" runat="server" onclick="PopupArea('1','ALL,','','','3')">
                                                <asp:Label ID="lblChannelOtherPercent" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A20" href="#" runat="server" onclick="PopupArea('1','ALL,','','','')">
                                                <asp:Label ID="lblrestPercent" runat="server" Text="0" /></a>
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
                <li class="title">订单统计&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" Text="昨夜" ID="lkOrderYesterDay" OnClick="lkOrderYesterDay_Click"
                        Visible="false" />&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" Text="今夜" ID="lkOrderToDay" OnClick="lkOrderToDay_Click"
                        Visible="false" /></li>
                <li>
                    <table width="95%">
                        <tr>
                            <td style="width: 70%" align="left" valign="top">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:HiddenField ID="hidOrderStartDate" runat="server" />
                                            <asp:HiddenField ID="hidOrderEndDate" runat="server" />
                                            <asp:Label ID="Label39" runat="server" Text="" />
                                        </td>
                                        <td align="center" style="width: 150px">
                                            总订单（券）：<a id="A61" href="#" runat="server" onclick="PopupArea('0','ALL','','','')"><asp:Label
                                                ID="Label40" runat="server" Text="0" /></a><a id="A62" href="#" runat="server" onclick="PopupArea('0','ALL','','1','')"><asp:Label
                                                    ID="Label41" runat="server" Text="0" /></a> 张
                                        </td>
                                        <td align="center" style="width: 200px">
                                            订单均价：<asp:Label ID="Label42" runat="server" Text="0" />
                                            元
                                        </td>
                                        <td align="right" style="width: 180px">
                                            总成功单（券）：<a id="A63" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','1,4,5,6,7,8|1,5')"><asp:Label
                                                ID="Label43" runat="server" Text="0" /></a><a id="A64" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','1','1,4,5,6,7,8|1,5')"><asp:Label
                                                    ID="Label44" runat="server" Text="0" /></a> 张
                                        </td>
                                        <td align="center" style="width: 200px">
                                            订单均价：<asp:Label ID="Label7" runat="server" Text="0" />
                                            元
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 148px">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 148px">
                                        </td>
                                        <td align="center" style="width: 100px; border: maroon 3 double">
                                            总订单(券)
                                        </td>
                                        <td align="center" style="width: 100px">
                                             确认可住单(券)
                                        </td>
                                        <td align="center" style="width: 100px">
                                            待确认单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            CC取消单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            用户取消单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            超时取消单(券)
                                        </td>
                                        <td align="center" style="width: 120px">
                                            其他(券)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            LMBAR：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A65" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','')">
                                                <asp:Label ID="lblLBOrderAll" runat="server" Text="0" /></a><a id="A147" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR','','1','')">
                                                    <asp:Label ID="lblLBOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A66" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','5')">
                                                <asp:Label ID="lblLBOrderAffirm" runat="server" Text="0" /></a><a id="A148" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR','','1','5')">
                                                    <asp:Label ID="lblLBOrderAffirmCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A67" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','1')">
                                                <asp:Label ID="lblLBOrderNotAffirmOrder" runat="server" Text="0" /></a><a id="A149"
                                                    href="#" runat="server" onclick="PopupArea('0','LMBAR','','1','1')">
                                                    <asp:Label ID="lblLBOrderNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A68" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','null')">
                                                <asp:Label ID="lblLBOrdercc" runat="server" Text="0" /></a><a id="A150" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR','','1','null')">
                                                    <asp:Label ID="lblLBOrderccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A135" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','4')">
                                                <asp:Label ID="lblLBOrderOther" runat="server" Text="0" /></a><a id="A151" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR','','1','4')">
                                                    <asp:Label ID="lblLBOrderOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A177" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','3')">
                                                <asp:Label ID="lblLBOrderTimeOut" runat="server" Text="0" /></a><a id="A178" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR','','1','3')">
                                                    <asp:Label ID="lblLBOrderTimeOutCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A136" href="#" runat="server" onclick="PopupArea('0','LMBAR','','','2,0')">
                                                <asp:Label ID="lblLBrest" runat="server" Text="0" /></a><a id="A152" href="#" runat="server"
                                                    onclick="PopupArea('0','LMBAR','','1','2,0')">
                                                    <asp:Label ID="lblLBrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            LMBAR2：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A69" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','')">
                                                <asp:Label ID="lblLB2OrderAll" runat="server" Text="0" /></a><a id="A153" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR2','','1','')">
                                                    <asp:Label ID="lblLB2OrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A70" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','4,5,6,7,8')">
                                                <asp:Label ID="lblLB2OrderAffirm" runat="server" Text="0" /></a><a id="A154" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR2','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblLB2OrderAffirmCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A71" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','1')">
                                                <asp:Label ID="lblLB2OrderNotAffirmOrder" runat="server" Text="0" /></a><a id="A155"
                                                    href="#" runat="server" onclick="PopupArea('0','LMBAR2','','1','1')">
                                                    <asp:Label ID="lblLB2OrderNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A72" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','9')">
                                                <asp:Label ID="lblLB2Ordercc" runat="server" Text="0" /></a><a id="A156" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR2','','1','9')">
                                                    <asp:Label ID="lblLB2OrderccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A137" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','3')">
                                                <asp:Label ID="lblLB2OrderOther" runat="server" Text="0" /></a><a id="A157" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR2','','1','3')">
                                                    <asp:Label ID="lblLB2OrderOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A181" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','null')">
                                                <asp:Label ID="lblLB2OrderTimeOut" runat="server" Text="0" /></a><a id="A182" href="#"
                                                    runat="server" onclick="PopupArea('0','LMBAR2','','1','null')">
                                                    <asp:Label ID="lblLB2OrderTimeOutCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A138" href="#" runat="server" onclick="PopupArea('0','LMBAR2','','','2,0')">
                                                <asp:Label ID="lblLB2rest" runat="server" Text="0" /></a><a id="A158" href="#" runat="server"
                                                    onclick="PopupArea('0','LMBAR2','','1','2,0')">
                                                    <asp:Label ID="lblLB2restCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            BAR/BARB：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A73" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','')">
                                                <asp:Label ID="lblBBOrderAll" runat="server" Text="0" /></a><a id="A159" href="#"
                                                    runat="server" onclick="PopupArea('0','BAR,BARB','','1','')">
                                                    <asp:Label ID="lblBBOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A74" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','4,5,6,7,8')">
                                                <asp:Label ID="lblBBOrderAffirm" runat="server" Text="0" /></a><a id="A160" href="#"
                                                    runat="server" onclick="PopupArea('0','BAR,BARB','','1','4,5,6,7,8')">
                                                    <asp:Label ID="lblBBOrderAffirmCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A75" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','1')">
                                                <asp:Label ID="lblBBOrderNotAffirmOrder" runat="server" Text="0" /></a><a id="A161"
                                                    href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','1','1')">
                                                    <asp:Label ID="lblBBOrderNotAffirmOrderCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A76" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','9')">
                                                <asp:Label ID="lblBB2Ordercc" runat="server" Text="0" /></a><a id="A162" href="#"
                                                    runat="server" onclick="PopupArea('0','BAR,BARB','','1','9')">
                                                    <asp:Label ID="lblBB2OrderccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A139" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','3')">
                                                <asp:Label ID="lblBBOrderOther" runat="server" Text="0" /></a><a id="A163" href="#"
                                                    runat="server" onclick="PopupArea('0','BAR,BARB','','1','3')">
                                                    <asp:Label ID="lblBBOrderOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A185" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','null')">
                                                <asp:Label ID="lblLBBOrderTimeOut" runat="server" Text="0" /></a><a id="A186" href="#"
                                                    runat="server" onclick="PopupArea('0','BAR,BARB','','1','null')">
                                                    <asp:Label ID="lblBBOrderTimeOutCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A140" href="#" runat="server" onclick="PopupArea('0','BAR,BARB','','','2,0')">
                                                <asp:Label ID="lblBBrest" runat="server" Text="0" /></a><a id="A164" href="#" runat="server"
                                                    onclick="PopupArea('0','BAR,BARB','','1','2,0')">
                                                    <asp:Label ID="lblBBrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            总计：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A77" href="#" runat="server" onclick="PopupArea('0','ALL','','','')">
                                                <asp:Label ID="lblOrderAllCount" runat="server" Text="0" /></a><a id="A165" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','','1','')">
                                                    <asp:Label ID="lblOrderAllCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A78" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','4,5,6,7,8|5')">
                                                <asp:Label ID="lblOrderAffirmCount" runat="server" Text="0" /></a><a id="A166" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','SUM','1','4,5,6,7,8|5')">
                                                    <asp:Label ID="lblOrderAffirmCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A79" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','1|1')">
                                                <asp:Label ID="lblOrderNotAffirmCount" runat="server" Text="0" /></a><a id="A167"
                                                    href="#" runat="server" onclick="PopupArea('0','ALL','SUM','1','1|1')">
                                                    <asp:Label ID="lblOrderNotAffirmCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A80" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','9|null')">
                                                <asp:Label ID="lblOrderccCount" runat="server" Text="0" /></a><a id="A168" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','SUM','1','9|null')">
                                                    <asp:Label ID="lblOrderccCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A141" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','3|4')">
                                                <asp:Label ID="lblOrderOtherCount" runat="server" Text="0" /></a><a id="A169" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','SUM','1','3|4')">
                                                    <asp:Label ID="lblOrderOtherCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A189" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','null|3')">
                                                <asp:Label ID="lblOrderTimeOutCount" runat="server" Text="0" /></a><a id="A190" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','SUM','1','null|3')">
                                                    <asp:Label ID="lblOrderTimeOutCode" runat="server" Text="(0)" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A142" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','2,0|2')">
                                                <asp:Label ID="lblrestCou" runat="server" Text="0" /></a><a id="A170" href="#" runat="server"
                                                    onclick="PopupArea('0','ALL','SUM','1','2,0|2')">
                                                    <asp:Label ID="lblrestCode" runat="server" Text="(0)" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            %：
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A81" href="#" runat="server" onclick="PopupArea('0','ALL','','','')">
                                                <asp:Label ID="lblOrderAllCountPercent" runat="server" Text="0" /></a><a id="A171"
                                                    href="#" runat="server" onclick="PopupArea('0','ALL','','1','')">
                                                    <asp:Label ID="lblOrderAllCodePercent" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A82" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','4,5,6,7,8|5')">
                                                <asp:Label ID="lblOrderAffirmPercent" runat="server" Text="0" /></a><a id="A172"
                                                    href="#" runat="server" onclick="PopupArea('0','ALL','SUM','1','4,5,6,7,8|5')">
                                                    <asp:Label ID="lblOrderAffirmPercentCode" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A83" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','1|1')">
                                                <asp:Label ID="lblOrderNotAffirmPercent" runat="server" Text="0" /></a><a id="A173"
                                                    href="#" runat="server" onclick="PopupArea('0','ALL','SUM','1','1|1')">
                                                    <asp:Label ID="lblOrderNotAffirmPercentCode" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A84" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','9|null')">
                                                <asp:Label ID="lblOrderccCountPercent" runat="server" Text="0" /></a><a id="A174"
                                                    href="#" runat="server" onclick="PopupArea('0','ALL','SUM','1','9|null')">
                                                    <asp:Label ID="lblOrderccCountPercentCode" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A143" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','3|4')">
                                                <asp:Label ID="lblOrderOtherPercent" runat="server" Text="0" /></a><a id="A175" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','SUM','1','3|4')">
                                                    <asp:Label ID="lblOrderOtherCodePercent" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A193" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','null|3')">
                                                <asp:Label ID="lblOrderTimeOutPercent" runat="server" Text="0" /></a><a id="A194"
                                                    href="#" runat="server" onclick="PopupArea('0','ALL','SUM','1','null|3')">
                                                    <asp:Label ID="lblOrderTimeOutCodePercent" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A144" href="#" runat="server" onclick="PopupArea('0','ALL','SUM','','2,0|2')">
                                                <asp:Label ID="lblrestCountPercent" runat="server" Text="0" /></a><a id="A176" href="#"
                                                    runat="server" onclick="PopupArea('0','ALL','SUM','1','2,0|2')">
                                                    <asp:Label ID="lblrestCodePercent" runat="server" Text="(0)" Visible="false" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px">
                                            BAR/BARB当晚入住：
                                        </td>
                                        <%--当晚入住   所有来源    订单状态  if create_date = in_date--%>
                                        <td style="width: 100px" align="center">
                                            <a id="A85" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','')">
                                                <asp:Label ID="lblOrderAllCKIN" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A86" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','4,5,6,7,8|5')">
                                                <asp:Label ID="lblOrderAffirmCKIN" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <a id="A87" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','1|1')">
                                                <asp:Label ID="lblOrderNotAffirmCKIN" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A88" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','9|null')">
                                                <asp:Label ID="lblOrderccCKIN" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A145" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','3|4')">
                                                <asp:Label ID="lblOrderOtherCKIN" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A197" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','null|3')">
                                                <asp:Label ID="lblOrderTimeOutCKIN" runat="server" Text="0" /></a>
                                        </td>
                                        <td style="width: 120px" align="center">
                                            <a id="A146" href="#" runat="server" onclick="PopupArea('0','ALL','CKIN','','2,0|2')">
                                                <asp:Label ID="lblrestCKIN" runat="server" Text="0" /></a>
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
                <li class="title">
                    <asp:Label ID="lbUserTitle" runat="server" Text="" /></li>
                <%-- <li>
            <table width="80%">
                <tr>
                    <td colspan="6"><asp:Label ID="lbTodayLoginDate" runat="server" Text="" />      今日登录用户：<asp:Label ID="lbTodayLoginAll" runat="server" Text="0" /> 名</td>
                </tr>
            </table>
            <br />
            <table width="80%">
                <tr>
                    <td colspan="6"><asp:Label ID="lbYesterDate" runat="server" Text="" />      总新增用户：<asp:Label ID="lbUserAll" runat="server" Text="0" /> 名</td>
                </tr>
            </table>
            <br />
            <table width="70%">
                <tr>
                    <td style="width:2%">IOS：</td>
                    <td style="width:5%"><asp:Label ID="lbIOSUR" runat="server" Text="0" /> 名</td>
                    <td style="width:2%">Android：</td>
                    <td style="width:5%"><asp:Label ID="lbANDUR" runat="server" Text="0" /> 名</td>
                    <td style="width:2%">Wap：</td>
                    <td style="width:5%"><asp:Label ID="lbWAPUR" runat="server" Text="0" /> 名</td>
                     <td style="width:2%">WP7：</td>
                    <td style="width:5%"><asp:Label ID="lbWP7" runat="server" Text="0" /> 名</td>
                    <td style="width:2%">其他：</td>
                    <td style="width:5%"><asp:Label ID="lbOther" runat="server" Text="0" /> 名</td>
                </tr>
            </table>
            <br />
        </li>--%>
                <li>
                    <table width="80%">
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbYesterDate" runat="server" Text="" />
                                登录用户：<asp:Label ID="lbTodayLoginAll" runat="server" Text="0" />
                                名（游客：<asp:Label ID="lbLgALLYK" runat="server" Text="0" />）,其中新增用户：<asp:Label ID="lbUserAll" runat="server" Text="0" />
                                名（游客：<asp:Label ID="lbXzALLYK" runat="server" Text="0" />）
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="70%">
                        <tr>
                            <td>
                            </td>
                            <td>
                                IOS
                            </td>
                            <td>
                                Android
                            </td>
                            <td>
                                Wap
                            </td>
                            <td>
                                WP7
                            </td>
                            <td>
                                W8
                            </td>
                            <td>
                                其他
                            </td>
                        </tr>
                        <tr>
                            <td>
                                登陆用户
                            </td>
                            <td>
                                <asp:Label ID="lblLogIOSUR" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbLogANDUR" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbLogWAPUR" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbLogWP7" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbLogW8" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbLogOther" runat="server" Text="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                新增用户
                            </td>
                            <td>
                                <asp:Label ID="lbIOSUR" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbANDUR" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbWAPUR" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbWP7" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbW8" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:Label ID="lbOther" runat="server" Text="0" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </li>
            </ul>
        </div>
        <div class="frame01">
            <ul>
                <li class="title">当前待处理审核单</li>
                <li>
                    <div id="dvPlanDetail" runat="server">
                    </div>
                    <%-- <table width="80%">
                <tr>
                   <td style="width:8%">酒店上下线计划：</td>
                    <td style="width:15%"><asp:Label ID="lbPlanOnOff" runat="server" Text="0" />条</td>
                    <td style="width:8%">修改目的地类型：</td>
                    <td style="width:15%"><asp:Label ID="lbPlanFtType" runat="server" Text="0" />条</td>
                    <td style="width:8%">修改酒店详情：</td>
                    <td style="width:15%"><asp:Label ID="lbPlanHotelInfo" runat="server" Text="0" />条</td>
                </tr>
            </table>--%>
                    <br />
                </li>
            </ul>
        </div>
        </div>
    </div>
    <br />
    <script type="text/javascript" language="javascript">
        function checkValid() {
            var startDate = document.getElementById("<%=StartDate.ClientID %>").value;
            var endDate = document.getElementById("<%=EndDate.ClientID %>").value;
            if (startDate == "") {
                alert("开始日期不能为空！");
                return false;
            }
            if (endDate == "") {
                alert("结束日期不能为空！");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
