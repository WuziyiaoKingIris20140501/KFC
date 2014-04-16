<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="RouteStockII.aspx.cs" Title="库存管理" Inherits="RouteStockII" %>

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
            height: 140%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: #666666;
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
        
        .GView_BodyCSS td:hover
        {
            background-color:#DDDDDD;
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
        function BindTable() {
            document.getElementById("dvContent").innerHTML = document.getElementById("<%=hidContent.ClientID%>").value;
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

        function AClick(arg) {
            alert(arg);
        }

        function OpenClick(planid, roomnm, plannum) {
            document.getElementById("<%=hidPlanID.ClientID%>").value =planid;
            document.getElementById("<%=hidRouteID.ClientID%>").value = roomnm;

            document.getElementById("<%=hidPlanNumer.ClientID%>").value = plannum;
            document.getElementById("<%=txtInRoomID.ClientID%>").value = plannum;

            document.getElementById("<%=txtActionID.ClientID%>").value = "";
            document.getElementById("<%=lbInfo.ClientID%>").innerHTML = document.getElementById("<%=hidShipNM.ClientID%>").value + "  " + document.getElementById("<%=hidBoatNM.ClientID%>").value + "  " + document.getElementById("<%=hidRouteID.ClientID%>").value;
            document.getElementById("<%=lbInfo.ClientID%>").innerText = document.getElementById("<%=hidShipNM.ClientID%>").value + "  " + document.getElementById("<%=hidBoatNM.ClientID%>").value + "  " + document.getElementById("<%=hidRouteID.ClientID%>").value;

            invokeOpenList();
            AJMemoHis(planid);
        }


        function AJMemoHis(arg) {
            $.ajax({
                contentType: "application/json",
                url: "RouteStockII.aspx/SetMemoVal",
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

        function invokeOpenList2() {
            document.getElementById("popupDiv3").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            //            bgObj.style.width = document.body.offsetWidth + "px";
            //            bgObj.style.height = screen.height + "px";
            //BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeCloseList() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function invokeCloseList2() {
            document.getElementById("popupDiv3").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        String.prototype.padLeft=function(b,c){
        var d=this;
        while(d.length<b)
        {
        d=c+d;
        }
        return d;
        }

        function AddCruisePlan() {
            
            document.getElementById("<%=hidAction.ClientID%>").value = "0";
            document.getElementById("<%=txtShip.ClientID%>").innerHTML = document.getElementById("<%=hidShipNM.ClientID%>").value;
            document.getElementById("<%=txtShip.ClientID%>").innerText = document.getElementById("<%=hidShipNM.ClientID%>").value;
            document.getElementById("<%=txtBoat.ClientID%>").innerHTML = document.getElementById("<%=hidBoatNM.ClientID%>").value;
            document.getElementById("<%=txtBoat.ClientID%>").innerText = document.getElementById("<%=hidBoatNM.ClientID%>").value;

            var board = document.getElementById("dvRoomlist");
            var strs = document.getElementById("<%=hidBoatNMList.ClientID%>").value.split(',');

            var tbRoomLt = "";
            tbRoomLt += "<table>";
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    var dpValue = strs[i] + ":";
                    tbRoomLt += "<tr>";
                    tbRoomLt += "<td align='right'>";
                    tbRoomLt += "<span>";
                    tbRoomLt += dpValue;
                    tbRoomLt += "</span>";
                    tbRoomLt += "</td>";
                    tbRoomLt += "<td align='left'>";
                    var btnid = "txt_" + i.toString();
                    tbRoomLt += "<input type='text' id='" + btnid + "' value='0' onfocus='this.select();'>";
                    tbRoomLt += "</span>";
                    tbRoomLt += "</td>";
                    tbRoomLt += "</tr>";
                }
            }
            tbRoomLt += "</table>";
            document.getElementById("dvRoomlist").innerHTML = tbRoomLt;

            invokeOpenList2();
        }

        function SaveCruisePlan() {

            if (parseInt(document.getElementById("<%=txtActionID.ClientID%>").value) == document.getElementById("<%=txtActionID.ClientID%>").value && parseInt(document.getElementById("<%=txtActionID.ClientID%>").value) >= 0 && parseInt(document.getElementById("<%=txtActionID.ClientID%>").value) <= 9999) {
               
            }
            else {
                document.getElementById("<%=detailMessageContent.ClientID%>").innerHTML = "房量请输入0-9999整数";
                return;
            }

            BtnLoadStyle();
            $.ajax({
                contentType: "application/json",
                url: "RouteStockII.aspx/SaveCruisePlan",
                type: "POST",
                dataType: "json",
                data: "{PlanID:'" + document.getElementById("<%=hidPlanID.ClientID%>").value +  "',PlanNumer:'" + document.getElementById("<%=txtActionID.ClientID%>").value + "',OPlanNumer:'" + document.getElementById("<%=hidPlanNumer.ClientID%>").value + "'}",
                success: function (data) {
                    document.getElementById("<%=MessageContent.ClientID%>").innerHTML = data.d;
                    invokeCloseList();
                    setTimeout("dvRefresh()", 1500);
                }
            });
        }

        function dvRefresh() {
            document.getElementById("<%=btnSearch.ClientID%>").click(); 
        }

        function CrCruisePlan(){
            if (document.getElementById("<%=dpCreateStart.ClientID%>").value == "") {
                document.getElementById("<%=dvAddMsg.ClientID%>").innerHTML = "请选择航次";
                return;
            }

            var plannum = "";
            var txtObject = document.getElementById("dvRoomlist");
            var txtInput = txtObject.getElementsByTagName("input");
            var txtLength = txtInput.length;
            for (var i = 0; i < txtLength; i++) {
                if (parseInt(txtInput[i].value) == txtInput[i].value && parseInt(txtInput[i].value) >= 0 && parseInt(txtInput[i].value) <= 9999) { 
                    plannum = plannum + txtInput[i].value + ",";
                }
                else {
                    document.getElementById("<%=dvAddMsg.ClientID%>").innerHTML = "房量请输入0-9999整数";
                    return;
                }
            }

            BtnLoadStyle();
            $.ajax({
                contentType: "application/json",
                url: "RouteStockII.aspx/CrCruisePlan",
                type: "POST",
                dataType: "json",
                data: "{Action:'" + document.getElementById("<%=hidAction.ClientID%>").value + "',BoatID:'" + document.getElementById("<%=hidBoatID.ClientID%>").value + "',CreateStart:'" + document.getElementById("<%=dpCreateStart.ClientID%>").value + "',PlanNumer:'" + plannum + "'}",
                success: function (data) {
                    document.getElementById("<%=MessageContent.ClientID%>").innerHTML = data.d;
                    invokeCloseList2();
                    setTimeout("dvRefresh()", 1500);
                }
            });
        }
    </script>
    <div class="frame01" style="margin: 8px 14px 5px 14px;">
        <ul>
            <li class="title">航线库存管理</li>
            <li>
                <table>
                    <tr>
                        <td align="left">
                            船公司：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdlShips" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" RepeatColumns="10" onselectedindexchanged="rdlShips_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            船只：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdlBoats" runat="server" RepeatDirection="Horizontal" RepeatColumns="10" />
                        </td>
                   </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="刷新" OnClientClick="BtnLoadStyle();" OnClick="btnSearch_Click" />
                            <%--<asp:Button ID="Button2" runat="server" CssClass="btn primary" Text="添加" OnClick="btnAdd_Click" />   --%>
                            <input type="button" id="btnAdd" class="btn primary" value="添加" onclick="AddCruisePlan();" />
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
            <span style="float:left">库存详情<asp:Label ID="lbCruiseNM" runat="server" Text="" /></span>
            <span style="float: right;display:none">
                <span style="text-align: right; color: White">█ </span>无库存&nbsp;&nbsp;&nbsp;
                <span style="text-align: right; color: #CDEBFF;">█ </span>周末&nbsp;&nbsp;&nbsp;
                <span style="text-align: right; color: #E6B9B6">█ </span>售完&nbsp;&nbsp;&nbsp;
            </span>
            </li>
            <li style="width:100%">
                <div id="dvContent" style="width:100%">
                </div>
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
                                船信息：
                            </td>
                            <td align="left" colspan="3">
                                <asp:Label ID="lbInfo" runat="server"/>
                            </td>
                        </tr>
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

            <asp:HiddenField ID="hidContent" runat="server" />

            <asp:HiddenField ID="hidAction" runat="server" />
            <asp:HiddenField ID="hidShipNM" runat="server" />
            <asp:HiddenField ID="hidBoatNMList" runat="server" />
            <asp:HiddenField ID="hidBoatNM" runat="server" />
            <asp:HiddenField ID="hidBoatID" runat="server" />
        </div>
    </div>


    <div id="popupDiv3" class="popupDiv2">
            <div class="frame01" style="width:99%;height:99%;margin-left:0px">
            <ul>
                <li class="title" style="text-align:left">库存详情</li>
                <li>
                <table width="97%">
                    <tr style="width:100%">
                        <td style="width:40%" valign="top">
                            <table>
                                <tr>
                                    <td align="right">
                                        船公司：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="txtShip" runat="server" />
                                    </td>
                                     </tr>
                                     <tr>
                                    <td align="right">
                                        船只：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="txtBoat" runat="server" />
                                    </td>
                                    </tr>
                                     <tr>
                                    <td align="right">
                                        航次：
                                    </td>
                                    <td align="left">
                                        <input id="dpCreateStart" class="Wdate" type="text" style="width: 120px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                            runat="server" /><font color="red">*</font>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:60%;" valign="top" align="center">
                            <table>
                                <tr>
                                    <td>
                                        <div id="dvRoomlist" style="width:100%; height: 245px;overflow-y: auto"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="dvAddMsg" runat="server" style="color: red"></div>
                                    </td>
                                </tr>
                                <tr style="width:100%">
                                    <td style="width:100%" align="center">
                                    <br />
                                        <input type="button" id="btnSavePlan" class="btn primary" value="保存" onclick="CrCruisePlan();" />&nbsp;&nbsp;&nbsp;
                                        <input type="button" id="btnCancel" class="btn primary" value="取消" onclick="invokeCloseList2();" />
                                    </td>
                                </tr>   
                            </table>
                        </td>
                    </tr>
                    </table>
                </li>
            </ul>
        </div>
    </div>

    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
    数据加载中，请稍等...</div>
</asp:Content>
