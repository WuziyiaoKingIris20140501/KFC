<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="RouteControl.aspx.cs" Title="航线管理" Inherits="RouteControl" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/AutoCptControl.ascx" TagName="WebAutoComplete"
    TagPrefix="ac1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
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
            top: 40%;
            left: 40%;
            margin-left: -74px;
            margin-top: -14px;
            padding: 10px 10px 10px 50px;
            text-align: left;
            line-height: 27px;
            font-weight: bold;
            position: absolute;
            z-index: 2001;
        }
        .bgDiv2
        {
            opacity: 0.5;
            filter: alpha(opacity=50);
            background: #666666;
            z-index: 100;
            display: none;
            bottom: 0;
            left: 0;
            position: fixed;
            right: 0;
            top: 0;
        }
        .popupDiv2
        {
            width: 560px;
            height: 380px;
            position: absolute;
            padding: 1px;
            z-index: 10000;
            display: none;
            background-color: White;
            top: 25%;
            left: 40%;
            margin-left: -150px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: -50px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&
                    document.compatMode=='CSS1Compat') ?
                    documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                    document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&
                    document.compatMode=='CSS1Compat') ?
                    documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                    document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SaveCruiseID() {
            document.getElementById("<%=hidCruiseID.ClientID%>").value = document.getElementById("wctCruise").value;
        }

        function ChangeModel() {
            document.getElementById("<%=dvSBtn.ClientID%>").style.display = "";
            document.getElementById("<%=hidCruiseID.ClientID%>").value = "";
            document.getElementById("<%=txtDestination.ClientID%>").value = ""; 
            document.getElementById("<%=txtShipNM.ClientID%>").value = "";
            document.getElementById("<%=txtDays.ClientID%>").value = "";
            document.getElementById("<%=txtPort.ClientID%>").value = "";

            document.getElementById("<%=MessageContent.ClientID%>").innerText = "";
            document.getElementById("<%=lbCruiseNM.ClientID%>").innerText = "";
            document.getElementById("<%=lbCruiseNM.ClientID%>").innerHTML = ""; 
            document.getElementById("wctCruise").value = "";
            document.getElementById("wctCruise").text = "";
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
    </script>
    <div class="frame01" style="margin: 8px 14px 5px 14px;">
        <ul>
            <li class="title">航线基础信息管理</li>
            <li>
                <table>
                    <tr>
                        <td align="left">
                            选择航线：
                        </td>
                        <td align="left">
                            <uc1:WebAutoComplete ID="wctCruise" runat="server" CTLID="wctCruise" AutoType="cruise"
                                AutoParent="RouteControl.aspx?Type=Cruise" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="选择" OnClientClick="SaveCruiseID()"
                                OnClick="btnSelect_Click" />
                        </td>
                        <td align="left" style="margin-left: 30px">
                            <%--   --%>
                            <input type="button" id="btnClear" class="btn primary" value="新建航线" onclick="ChangeModel();" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td align="left" colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr style="">
                        <td align="left">
                            航线名称：
                        </td>
                        <td align="left" colspan="3">
                            <asp:Label ID="lbCruiseNM" runat="server" Text="" />
                        </td>
                    </tr>
                </table>
            </li>
            <li>
                <div id="MessageContent" runat="server" style="color: red; width: 800px;">
                </div>
            </li>
        </ul>
    </div>

    <div style="margin: 5px 14px 5px 14px;" id="dvCRHotel" runat="server">
        <div id="ctabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#ctabs-1" id="alLabel">航线基础信息 </a></li> //
            </ul>
            <div id="ctabs-1" style="border: 1px solid #D5D5D5;">
                <table cellspacing="2" cellpadding="1" width="95%">
                    <tr>
                        <td align="right">
                            航线中文名（出游地）：
                        </td>
                        <td>
                            <asp:TextBox ID="txtDestination" runat="server" Width="150px" MaxLength="10" />&nbsp;<font color="red">*</font>
                        </td>
                        <td align="right">
                            游轮公司名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtShipNM" runat="server" Width="150px" MaxLength="10" />&nbsp;<font color="red">*</font>
                        </td>
                        <td align="right">
                            天数：
                        </td>
                        <td>
                            <asp:TextBox ID="txtDays" runat="server" Width="150px" MaxLength="10" />&nbsp;<font color="red">*</font>
                        </td>
                        <td align="right">
                            出发母港：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPort" runat="server" Width="150px" MaxLength="10" />&nbsp;<font color="red">*</font>
                        </td>
                    </tr>
                </table>
                <table width="95%">
                    <tr>
                        <td>
                            <div id="dvCsave" style="text-align: left; margin-left: 40%">
                                <div id="background" class="pcbackground" style="display: none;">
                                </div>
                                <div id="progressBar" class="pcprogressBar" style="display: none;">
                                    数据加载中，请稍等...</div>

                                    <br />
                                    <div id="dvSBtn" runat="server" style="">
                                <asp:Button ID="btnCreateHL" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetCHControlVal();BtnLoadStyle();"
                                    OnClick="btnCreateHL_Click" />&nbsp;<font color="red">*</font><span style="color: #AAAAAA">为必填字段，请注意填写</span>
                                    </div>

                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidSelectedID" runat="server" />
    <asp:HiddenField ID="hidCruiseID" runat="server" />
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

            $("#ctabs").tabs();
        });
    </script>
</asp:Content>
