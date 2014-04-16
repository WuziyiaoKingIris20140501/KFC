<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="RouteStock.aspx.cs" Title="库存管理" Inherits="RouteStock" %>

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
        
        .Tb_BodyCSS2 tr
        {
            height: 50px;
        }
        .Tb_BodyCSS2 td
        {
            height: 50px;
            text-align:center;
            border-right: 1px #d5d5d5 solid;
            border-top: 1px #d5d5d5 solid;
        }
        
        .Tb_BodyCSS2
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1pxsolid#ccc;
            width:100%
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SaveCruiseID() {
            document.getElementById("<%=hidCruiseID.ClientID%>").value = document.getElementById("wctCruise").value;
            document.getElementById("<%=lbCruiseNM.ClientID%>").innerText = document.getElementById("wctCruise").value;
            document.getElementById("<%=lbCruiseNM.ClientID%>").innerHtml = document.getElementById("wctCruise").value;

            if (document.getElementById("wctCruise").value == "" || document.getElementById("<%=planEndDate.ClientID%>").value == "" || document.getElementById("<%=planStartDate.ClientID%>").value == "")
            {
                document.getElementById("<%=MessageContent.ClientID%>").innerHTML = "请选择航线和计划起止日期";
            }

            $.ajax({
                async: true,
                contentType: "application/json",
                url: "RouteStock.aspx/GetDataByJson",
                type: "POST",
                dataType: "json",
                data: "{CruiseID:'" + document.getElementById("<%=hidCruiseID.ClientID%>").value + "',SDtime:'" + document.getElementById("<%=planStartDate.ClientID%>").value + "',EDtime:'" + document.getElementById("<%=planEndDate.ClientID%>").value + "'}",
                success: function (data) {
                    document.getElementById("dvContent").innerHTML = data.d;
                },
                error: function (json) {

                }
            }); 
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

        function OpenClick(planid, routeid, plannum, plandt) {
            document.getElementById("<%=hidPlanID.ClientID%>").value =planid;
            document.getElementById("<%=hidPlanDTime.ClientID%>").value = plandt;
            document.getElementById("<%=hidRouteID.ClientID%>").value = routeid;
            document.getElementById("<%=hidPlanNumer.ClientID%>").value = plannum;
            document.getElementById("<%=txtInRoomID.ClientID%>").value = plannum;
            document.getElementById("<%=txtActionID.ClientID%>").value = "";
            invokeOpenList();
            AJMemoHis(routeid + "-" + planid);
        }

        function SaveCruisePlan() {
            BtnLoadStyle();
            $.ajax({
                contentType: "application/json",
                url: "RouteStock.aspx/SaveCruisePlan",
                type: "POST",
                dataType: "json",
                data: "{PlanID:'" + document.getElementById("<%=hidPlanID.ClientID%>").value + "',PlanDTime:'" + document.getElementById("<%=hidPlanDTime.ClientID%>").value + "',RouteID:'" + document.getElementById("<%=hidRouteID.ClientID%>").value + "',PlanNumer:'" + document.getElementById("<%=txtActionID.ClientID%>").value + "',OPlanNumer:'" + document.getElementById("<%=txtInRoomID.ClientID%>").value + "'}",
                success: function (data) {
                    document.getElementById("<%=MessageContent.ClientID%>").innerHTML = data.d;
                    invokeCloseList();
                    SaveCruiseID();
                    BtnCompleteStyle();
                }
            });
        }

        function AJMemoHis(arg) {
            $.ajax({
                contentType: "application/json",
                url: "RouteStock.aspx/SetMemoVal",
                type: "POST",
                dataType: "json",
                data: "{strKey:'" + arg + "'}",
                success: function (data) {
                    document.getElementById("<%=lbMemo1.ClientID%>").innerHTML = data.d;
                }
            });
        }

        //显示弹出的层
        function invokeOpenList() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            //            bgObj.style.width = document.body.offsetWidth + "px";
            //            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeCloseList() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }
    </script>
    <div class="frame01" style="margin: 8px 14px 5px 14px;">
        <ul>
            <li class="title">航线库存管理</li>
            <li>
                <table>
                    <tr>
                        <td align="left">
                            选择航线：
                        </td>
                        <td align="left">
                            <uc1:WebAutoComplete ID="wctCruise" runat="server" CTLID="wctCruise" AutoType="cruise"
                                AutoParent="RouteStock.aspx?Type=Cruise" />
                        </td>
                        <td>
                            计划起止日期：
                        </td>
                        <td>
                            <input id="planStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_planEndDate\')||\'2020-10-01\'}'})"
                                runat="server" />
                            至：
                            <input id="planEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_planStartDate\')}',maxDate:'2020-10-01'})"
                                runat="server" />
                        </td>
                        <td align="left">
<%--                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="选择" OnClientClick="SaveCruiseID()"
                                OnClick="btnSelect_Click" />--%>
                                <input type="button" id="btnClear" class="btn primary" value="选择" onclick="SaveCruiseID();" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td align="left" colspan="4">
                            <br />
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
    <table width="98%">
    <tr>
    <td>
    <div class="frame01" style="width: 100%; height: 600px;overflow-y:auto">
        <ul>
            <li class="title">
            <span style="float:left">航线名称：<asp:Label ID="lbCruiseNM" runat="server" Text="" /></span>
            <span style="float: right;">
                <span style="text-align: right; color: White">█ </span>无库存&nbsp;&nbsp;&nbsp;
                <span style="text-align: right; color: #CDEBFF;">█ </span>周末&nbsp;&nbsp;&nbsp;
                <span style="text-align: right; color: #E6B9B6">█ </span>售完&nbsp;&nbsp;&nbsp;
            </span>
            </li>
            <li style="width:100%">
                <div id="dvContent" style="width:100%"></div>
            </li>
        </ul>
    </div>
    </td>
    </tr>
    </table>
    <asp:HiddenField ID="hidSelectedID" runat="server" />
    <asp:HiddenField ID="hidCruiseID" runat="server" />


    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2">
            <div class="frame01" style="width:99%;height:99%;margin-left:0px">
            <ul>
                <li class="title" style="text-align:left">库存详情</li>
                <li>
                <table width="97%">
                        <tr>
                            <td align="right">
                                原库存：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInRoomID" runat="server" Width="180px" MaxLength="10" ReadOnly="true"/>
                            </td>
                            <td align="right">
                                修改库存：
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtActionID" runat="server" Width="180px" MaxLength="10"/><font color="red">*</font>
                            </td>
                        </tr>
                        <tr style="width:100%">
                            <td style="width:100%" align="center" colspan="4">
                            <br />
                                <input type="button" id="btnSave" class="btn primary" value="保存" onclick="SaveCruisePlan();" />&nbsp;&nbsp;&nbsp;
                                <input type="button" id="btnCannel" class="btn primary" value="取消" onclick="invokeCloseList();" />
                            </td>
                        </tr>   
                    </table>
                    <div id="detailMessageContent" runat="server" style="color: red"></div>
                </li>
                <li>
                    <div style="width:100%; height: 245px;overflow-y: auto">
                        <asp:Label ID="lbMemo1" runat="server" />
                    </div>
                </li>
            </ul>
            <asp:HiddenField ID="hidPlanID" runat="server" />
            <asp:HiddenField ID="hidPlanDTime" runat="server" />
            <asp:HiddenField ID="hidRouteID" runat="server" />
            <asp:HiddenField ID="hidPlanNumer" runat="server" />

        </div>
    </div>
</asp:Content>
