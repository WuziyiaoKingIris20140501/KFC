﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OrderApprovedFax.aspx.cs" Inherits="OrderApprovedFax" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete" TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/AutoCptControl.ascx" TagName="AutoCptControl" TagPrefix="ac1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <script language="javascript" type="text/javascript">
        document.onkeydown = function (e) {
            e = e ? e : window.event;
            var keyCode = e.which ? e.which : e.keyCode;
            if (keyCode == 27)
                invokeCloseList();
        }

        function ClickEvent(selIndex) {
            BtnLoadStyle();
            document.getElementById("<%=HidSelIndex.ClientID%>").value = selIndex;
            document.getElementById("<%=btnLoadOrderList.ClientID%>").click();
        }

        function RecordPostion(obj) {
            var div1 = obj;
            var sx = document.getElementById("<%=dvscrollX.ClientID%>");
            var sy = document.getElementById("<%=dvscrollY.ClientID%>");
            sy.value = div1.scrollTop;
            sx.value = div1.scrollLeft;
        }

        function GetResultFromServer() {
            try {
                if (document.getElementById("<%=HidScrollValue.ClientID%>").value == '' || document.getElementById("<%=HidScrollValue.ClientID%>").value == 'NaN') {
                    document.getElementById("<%=HidScrollValue.ClientID%>").value = '0';
                }
                var sx = document.getElementById("<%=dvscrollX.ClientID%>");
                var sy = parseInt(document.getElementById("<%=dvscrollY.ClientID%>").value) + parseInt(document.getElementById("<%=HidScrollValue.ClientID%>").value);
                document.getElementById("<%=dvGridView.ClientID%>").scrollTop = sy;
                document.getElementById("<%=dvGridView.ClientID%>").scrollLeft = sx.value;
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '0';
                grLayout();
            }
            catch (e) { }
        }

        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            grLayout();
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
            AJMemoHis();
        }

        function AJMemoHis() {
            $.ajax({
                contentType: "application/json",
                url: "OrderApprovedFax.aspx/SetMemoVal",
                type: "POST",
                dataType: "json",
                data: "{strKey:'" + document.getElementById("<%=hidOrderID.ClientID%>").value + "'}",
                success: function (data) {
                    document.getElementById("<%=lbMemo1.ClientID%>").innerHTML = data.d;
                }
            });
        }

        //隐藏弹出的层
        function invokeCloseList() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
            GetResultFromServer();
            grLayout();
        }

        function LastOrNextByHotel(obj) {
            BtnLoadStyle();
            if (obj == '1') {
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '30';
            } else {
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '-30';
            }
            document.getElementById("<%=HidLastOrNextByHotel.ClientID%>").value = obj;
            document.getElementById("<%=btnReLoad.ClientID%>").click();
        }

        function SetSalesRoom(obj) {
            document.getElementById("wctorderApro").value = obj;
        }

        function SetControlValue() {
            document.getElementById("<%=hidSelectHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidSelectCity.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=hidSelectBussiness.ClientID%>").value = document.getElementById("wcthvpTagInfo").value;
            document.getElementById("<%=hidSelectSalesID.ClientID%>").value = document.getElementById("wctorderApro").value;
        }

        function OpenIssuePage(arg) {
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
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?BType=8&RID=' + arg + "&time=" + time, null, fulls);
        }

//        function openPrintPage(HID, OTD, OST, OID) {
//            grLayout();
////            var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
////            if (window.screen) {
////                var ah = screen.availHeight - 30;
////                var aw = screen.availWidth - 10;
////                fulls += ",height=" + ah;
////                fulls += ",innerHeight=" + ah;
////                fulls += ",width=" + aw;
////                fulls += ",innerWidth=" + aw;
////                fulls += ",resizable"
////            } else {
////                fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
////            }
//            var time = new Date();
//            //window.open("OrderApprovePrint.aspx?HID=" + escape(HID) + "&IND=" + escape(IND) + "&OTD=" + escape(OTD) + "&OST=" + escape(OST) + "&OID=" + escape(OID) + "&time=" + time, null, fulls);
//            window.showModalDialog("OrderApprovePrint.aspx?HID=" + escape(HID) + "&OTD=" + escape(OTD) + "&OST=" + escape(OST) + "&OID=" + escape(OID) + "&time=" + time, window, "dialogWidth:700px;dialogHeight:750px;center:yes;status:no;scroll:yes;help:no;");

//        }

        function openFaxPrintPage(HID) {
            grLayout();
            var time = new Date();
            window.showModalDialog("OrderApprovedFaxPrint.aspx?HID=" + escape(HID) + "&time=" + time, window, "dialogWidth:700px;dialogHeight:750px;center:yes;status:no;scroll:yes;help:no;");
        } 

        function grLayout() {
            var grid = document.getElementById("<%=gridViewCSList.ClientID%>");

            if (grid != null && grid.rows.length > 0) {
                if (grid.rows.length > 12) {
                    document.getElementById("grHeader").style.width = "98.5%";
                }
                document.getElementById("GrHeader0").style.width = grid.rows[1].cells[0].style.width;
                document.getElementById("GrHeader1").style.width = grid.rows[1].cells[1].style.width;
                document.getElementById("GrHeader2").style.width = grid.rows[1].cells[2].style.width;
                document.getElementById("GrHeader3").style.width = grid.rows[1].cells[3].style.width;
                document.getElementById("GrHeader4").style.width = grid.rows[1].cells[4].style.width;
                document.getElementById("GrHeader5").style.width = grid.rows[1].cells[5].style.width;
                document.getElementById("GrHeader6").style.width = grid.rows[1].cells[6].style.width;
                document.getElementById("GrHeader7").style.width = grid.rows[1].cells[7].style.width;
                document.getElementById("GrHeader8").style.width = grid.rows[1].cells[8].style.width;
            }
        }

        function SerRbtDDValue(arg) {
            document.getElementById("<%=hidFaxStatus.ClientID%>").value = arg;
        }

        function SetChkAllStyle() {
            var chkObject = document.getElementById("<%=gridViewCSList.ClientID%>");
            if (chkObject != null) {
                var chkInput = chkObject.getElementsByTagName("input");
                for (var i = 0; i < chkInput.length; i++) {
                    if (chkInput[i].type == "checkbox") {
                        if (document.getElementById("chkAllItems").checked == true) {
                            chkInput[i].checked = true;
                        }
                        else {
                            chkInput[i].checked = false;
                        }
                    }
                }
            }
        }

        function OpenPreviewFaxList(HID, OTSD, OTED, FST, OID, FNU) {
            grLayout();
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
            window.open("../Order/OrderApprovedFaxPreView.aspx?HID=" + escape(HID) + "&OTSD=" + escape(OTSD) + "&OTED=" + escape(OTED) + "&FST=" + escape(FST) + "&OID=" + escape(OID) + "&FNU=" + escape(FNU) + "&time=" + time, null, fulls);
        }


        function GridViewColor(GridViewId, NormalColor, AlterColor, HoverColor, SelectColor) {
            var AllRows = document.getElementById(GridViewId).getElementsByTagName("tr");
            //设置每一行的背景色和事件，循环从1开始而非0，可以避开表头那一行
            for (var i = 0; i < AllRows.length; i++) {
                //设定本行默认的背景色
                AllRows[i].style.background = NormalColor;
                //如果指定了鼠标指向的背景色，则添加onmouseover/onmouseout事件
                //处于选中状态的行发生这两个事件时不改变颜色
                if (HoverColor != "") {
                    AllRows[i].onmouseover = function () { if (!this.selected) this.style.background = HoverColor; }
                    AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = NormalColor; }
                }
                //如果指定了鼠标点击的背景色，则添加onclick事件
                //在事件响应中修改被点击行的选中状态
                if (SelectColor != "") {
                    AllRows[i].onclick = function () {
                        this.style.background = SelectColor;
                        this.selected = !this.selected;
                    }
                }
            }
        }

        function ClearODDateControl() {
            if (document.getElementById("<%=txtOrderID.ClientID%>").value.trim() != "") {
                document.getElementById("wctHotel").value = "";
                document.getElementById("wctHotel").text = "";
                document.getElementById("wctCity").value = "";
                document.getElementById("wctCity").text = "";
                document.getElementById("wcthvpTagInfo").value = "";
                document.getElementById("wcthvpTagInfo").text = "";
                document.getElementById("wctorderApro").value = "";
                document.getElementById("wctorderApro").text = "";
                document.getElementById("<%=OutStartDate.ClientID%>").value = "";
                document.getElementById("<%=OutEndDate.ClientID%>").value = "";
                document.getElementById("<%=ddpSort.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=chkListStar0.ClientID%>").checked = false;
                document.getElementById("<%=chkListStar1.ClientID%>").checked = false;
                document.getElementById("<%=chkListStar2.ClientID%>").checked = false;
                document.getElementById("<%=chkListStar4.ClientID%>").checked = false;

                document.getElementById("rbtnAll").checked = true;
                document.getElementById("rbtnBL").checked = false;
                document.getElementById("rbtnLL").checked = false;
                document.getElementById("<%=hidFaxStatus.ClientID%>").value = "";
            }
        }

        function ClearFNDateControl() {
            if (document.getElementById("<%=txtFaxNum.ClientID%>").value.trim() != "") {
                document.getElementById("wctHotel").value = "";
                document.getElementById("wctHotel").text = "";
                document.getElementById("wctCity").value = "";
                document.getElementById("wctCity").text = "";
                document.getElementById("wcthvpTagInfo").value = "";
                document.getElementById("wcthvpTagInfo").text = "";
                document.getElementById("wctorderApro").value = "";
                document.getElementById("wctorderApro").text = "";
                document.getElementById("<%=OutStartDate.ClientID%>").value = "";
                document.getElementById("<%=OutEndDate.ClientID%>").value = "";
                document.getElementById("<%=ddpSort.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=chkListStar0.ClientID%>").checked = false;
                document.getElementById("<%=chkListStar1.ClientID%>").checked = false;
                document.getElementById("<%=chkListStar2.ClientID%>").checked = false;
                document.getElementById("<%=chkListStar4.ClientID%>").checked = false;

                document.getElementById("rbtnAll").checked = true;
                document.getElementById("rbtnBL").checked = false;
                document.getElementById("rbtnLL").checked = false;
                document.getElementById("<%=hidFaxStatus.ClientID%>").value = "";
            }
        }
    </script>
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
            display: block;
            width: 100%;
            height: 135%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: #666666;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 100;
            display:none;
        }
        .popupDiv2
        {
            width: 560px;
            height: 580px;
            top: 55%;
            right: 0%;
            position: absolute;
            padding: 1px;
            z-index: 1001;
            display: none;
            background-color: White;
        }
        #txtRemark
        {
            height: 56px;
            width: 422px;
        }
        .style1
        {
            width: 34px;
            text-align: right;
        }
        .GView_ItemCSS
        {
            /*奇数行*/ /*background:url(../images/bg-frame0201.gif);*/
            background: white;
            line-height: 30px;
        }
        
        .GView_AlternatingItemCSS
        {
            /*偶数行*/
            background: #f6f6f6;
            border-top: 1px #e6e5e5 solid;
            border-bottom: 1px #e6e5e5 solid;
            line-height: 30px;
            border-bottom-color: #e6e5e5;
            border-top-color: #e6e5e5;
        }
        
        .lblLinkDetails
        {
            font-weight: bold;
            font-size: large;
        }
        
        .gvHideHeader
        {
            display:none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
        <div id="right">
            <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <div class="frame01">
                <ul>
                    <li class="title">订单审核任务表</li>
                    <li>
                        <table width="98%">
                            <tr align="left">
                                <td align="right">
                                    选择酒店：
                                </td>
                                <td align="left">
                                    <ac1:AutoCptControl ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" CTLLEN="300px"
                                        AutoParent="OrderApprovedFax.aspx?Type=hotel" />
                                </td>
                                <td align="right">
                                    城市：
                                </td>
                                <td align="left">
                                    <ac1:AutoCptControl ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" CTLLEN="150px"
                                        EnableViewState="false" AutoParent="OrderApprovedFax.aspx?Type=city" />
                                </td>
                                <td align="right">
                                    商圈：
                                </td>
                                <td align="left">
                                    <ac1:AutoCptControl ID="wcthvpTagInfo" runat="server" CTLID="wcthvpTagInfo" AutoType="hvptaginfo" CTLLEN="150px"
                                        EnableViewState="false" AutoParent="OrderApprovedFax.aspx?Type=tag" />
                                </td>
                                <td align="right">
                                    审核人员：
                                </td>
                                <td align="left">
                                    <ac1:AutoCptControl ID="wctorderApro" CTLID="wctorderApro" runat="server" CTLLEN="150px"
                                        EnableViewState="false" AutoType="orderApro" AutoParent="OrderApprovedFax.aspx?Type=orderApro" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td align="right">
                                    订单号：
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOrderID" runat="server" Width="300px" MaxLength="30" onblur="ClearODDateControl()"/>
                                </td>
                                <td align="right">
                                    传真编号：
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtFaxNum" runat="server" Width="150px" MaxLength="20" onblur="ClearFNDateControl()"/>
                                </td>
                                <td align="right">
                                    排序方式：
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddpSort" runat="server" Width="161px">
                                        <asp:ListItem Value=" ordercount desc ">销量排序</asp:ListItem>
                                        <asp:ListItem Value=" prop_name_zh asc ">名称排序</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr align="left">
                                <td align="right">
                                    离店日期：
                                </td>
                                <td align="left">
                                 <input id="OutStartDate" class="Wdate" type="text" style="width:129px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_OutEndDate\')||\'2020-10-01\'}'})"
                                        runat="server" />
                                    至：
                                    <input id="OutEndDate" class="Wdate" type="text" style="width:129px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_OutStartDate\')}',maxDate:'2020-10-01'})"
                                        runat="server" />
                                    <%--<input id="OutDateTime" class="Wdate" type="text" style="width:150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server" />--%>
                                </td>
                                <td align="right">
                                    传真状态：
                                </td>
                                <td align="left">
                                    <input type="radio" name="RbtFaxStatus" id="rbtnAll" value="" checked="checked" onclick="SerRbtDDValue('')"/>不限
                                    <input type="radio" name="RbtFaxStatus" id="rbtnBL" value="1" onclick="SerRbtDDValue('1')"/>已回
                                    <input type="radio" name="RbtFaxStatus" id="rbtnLL" value="0" onclick="SerRbtDDValue('0')"/>未回
                                </td>
                                <td align="right">
                                    订单状态：
                                </td>
                                <td align="left" colspan="2">
                                    <div style="float:left;margin-top:2px">
                                    <input id="chkListStar0" type="checkbox" runat="server" value=""/>未审
                                    <input id="chkListStar1" type="checkbox" runat="server" value="8"/>离店
                                    <%--<input id="chkListStar1" type="checkbox" runat="server" value="8-0"/>离店(初)
                                    <input id="chkListStar3" type="checkbox" runat="server" value="8-1"/>离店(复)--%>
                                    <input id="chkListStar2" type="checkbox" runat="server" value="5-0"/>NS(初)
                                    <input id="chkListStar4" type="checkbox" runat="server" value="5-1"/>NS(复)
                                    </div>
                                    <div style="float:right;margin-right:15px;">
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();SetControlValue();" OnClick="btnSelect_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="6">
                                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-left: 10px;">
                                                <div id="messageContent" runat="server" style="color: red; width: 400px;">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <table width="100%" style="height: 600px;">
                <tr>
                    <td style="width: 21%;" valign="top">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div class="frame01" style="width: 100%; height: 670px; margin-left: 5px;">
                                        <ul>
                                            <li class="title">酒店列表 <span style="float: right;"><span id="countNum" runat="server">0</span></span></li>
                                        </ul>
                                        <ul>
                                            <li style="padding: 0px 0px 0px 0px;">
<%--                                                <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>--%>
                                                <div id="dvGridView" style="overflow: auto; height: 655px; width: 100%;" runat="server"
                                                    onscroll="RecordPostion(this);">
                                                    <asp:GridView ID="gridHotelList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        Width="100%" DataKeyNames="PROP,CITYID,SALES_ACCOUNT,PROP_NAME_ZH,ADCUN,UADCUN,BCUN"
                                                        OnRowDataBound="gridHotelList_RowDataBound" CssClass="GView_BodyCSS">
                                                        <Columns>
                                                            <asp:BoundField DataField="CITYNM" HeaderText="CITYID" />
                                                            <asp:TemplateField>
                                                                <HeaderStyle Width="220px" />
                                                                <HeaderTemplate>
                                                                    酒店名称
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl02" runat="server" Text='<%#Eval("PROP_NAME_ZH").ToString().Length>16? Eval("PROP_NAME_ZH").ToString().Substring(0,15)+"...":Eval("PROP_NAME_ZH").ToString()%>'
                                                                        ToolTip='<%#Bind("PROP_NAME_ZH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CITYID" HeaderText="CITYID" Visible="false" />
                                                            <asp:BoundField DataField="isplan" HeaderText="是否有计划" Visible="false" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <RowStyle CssClass="GView_ItemCSS" />
                                                        <HeaderStyle CssClass="GView_HeaderCSS" />
                                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                        <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
                                                    </asp:GridView>
                                                    <div id="Popup" class="transparent" style="z-index: 200" runat="server">
                                                        <table border="0" cellpadding="0" style="font-size: x-small" width="200px">
                                                            <tr>
                                                                <td id="td1">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                    <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:1%;">
                    </td>
                    <td style="width: 78%;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="99%" class="">
                            <tr>
                                <td  align="left" valign="top">
                                    <table style="border:1px #d5d5d5 solid;width:100%;background-color:#F2F2F2;">
                                        <tr style="height:30px">
                                            <td align="left" >
                                                <div style="margin-left:25px;">
                                                    <b>酒店名称：<asp:Label ID="lbHotelNM" runat="server" Text="" /></b>
                                                </div>
                                            </td>
                                        </tr>
                                   </table>
                                    <table style="border:1px #d5d5d5 solid;width:100%;background-color:#F2F2F2;margin-top:-2px;">
                                        <tr>
                                            <td align="right">
                                                审核时间：
                                            </td>
                                            <td>
                                                <input type="radio" id="rdOrderVerifyTimeDay" name="rdOrderVerifyTime" runat="server" />&nbsp;日审
                                            &nbsp;&nbsp;<input type="radio" id="rdOrderVerifyTimeNight" name="rdOrderVerifyTime" runat="server" />&nbsp;夜审
                                            </td>
                                                       
                                            <td align="right">
                                                审核联系人：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOrderVerifyLinkMan" runat="server" Width="120px" MaxLength="20" />
                                            </td>
                                            <td align="right">
                                                审核电话： 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOrderVerifyLinkTel" runat="server" MaxLength="20" 
                                                    Width="120px" />
                                            </td>
                                            <td align="right">
                                                审核传真： 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOrderVerifyLinkFax" runat="server" MaxLength="20" 
                                                    Width="120px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                审核方式：
                                            </td>
                                            <td valign="top">
                                                <input type="radio" id="rdOrderVerifyTypeFax" name="rdOrderVerifyType" runat="server"/>&nbsp;传真
                                            &nbsp;&nbsp;<input type="radio" id="rdOrderVerifyTypeTel" name="rdOrderVerifyType" runat="server" />&nbsp;电话
                                            </td>
                                            <td align="right" valign="top">
                                                备注信息：
                                            </td>
                                            <td colspan="5">
                                                <textarea id="txtOrderVerifyRemark" runat="server" style="width:360px; height: 60px;"></textarea>
                                            </td>
                                        </tr>
                                   </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border:1px #d5d5d5 solid; background-color:#F2F2F2; padding:1px;height:35px;" align="left" valign="middle">
                                    <table style="margin-left:10px;width:98%">
                                            <tr>
                                                <td style="width:50%">
                                                    <asp:Button ID="btnSendFax" runat="server" Text="重发审核传真"  CssClass="btn" OnClick="btnSendFax_Click" Width="100px"/>
                                               
                                                    <asp:Button ID="btnPreFax" runat="server" Text="查看相关传真"  CssClass="btn" OnClick="btnPreFax_Click" Width="100px"/>
                                               
                                                    <asp:Button ID="btnPrint" runat="server" Text="打印审核传真"  CssClass="btn" OnClick="btnPrint_Click" Width="100px"/>
                                              
                                                    <asp:Button ID="btnUpdateLink" runat="server" Text="修改审核信息"  CssClass="btn" OnClick="btnUpdateLink_Click" Width="100px"/>
                                                </td>
                                                <%--<td align="right" style="width:50%;">
                                                    <div style="margin-right:20px;">
                                                        <input id="chkDbApprove" type="checkbox" runat="server" value="1"/><font style="font-size:15px;font-weight: bold;">复审</font>
                                                    </div>
                                                </td>--%>
                                            </tr>
                                     </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <div id="dvGridMessage" runat="server" style="color: red"></div>
                                    <table class="GView_BodyCSS" cellspacing="1" id="grHeader" cellpadding="4" border="1" style="margin-top:6px;background-color:White;width:100%;" rules="all">
                                            <tbody>
                                                <tr class="GView_HeaderCSS">
                                                    <th scope="col" align="center" id="GrHeader0" style="width:3%"><input type="checkbox" name="chkAllItems" id="chkAllItems" onclick="SetChkAllStyle()"/></th>
                                                    <th scope="col" align="center" id="GrHeader1" style="width:12%">订单号</th>
                                                    <th scope="col" align="center" id="GrHeader2" style="width:16%">房型</th>
                                                    <th scope="col" align="center" id="GrHeader3" style="width:8%">入住人姓名</th>
                                                    <th scope="col" align="center" id="GrHeader4" style="width:10%">入住-离店</th>
                                                    <th scope="col" align="center" id="GrHeader5" style="width:10%">预订手机号</th>
                                                    <th scope="col" align="center" id="GrHeader6" style="width:11%">预订确认号</th>
                                                    <th scope="col" align="center" id="GrHeader7" style="width:7%">审核状态</th>
                                                    <th scope="col" align="center" id="GrHeader8" style="width:23%">审核操作</th>
                                                </tr>
                                            </tbody>
                                        </table>
                                    <div style="width:100%; height: 505px;overflow-y: auto;margin-top:-1px" id="dvScroll">
                                        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                         CellPadding="4" CellSpacing="1" Width="100%" CssClass="GView_BodyCSS" onrowcommand="gridViewCSList_RowCommand" OnRowDataBound="gridViewCSList_RowDataBound" >
                                            <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <input type="checkbox" name="chkItems" id="chkItems" runat="server"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="ORDERID" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID={0}" 
                                                Target="_blank" DataTextField="ORDERID"><ItemStyle HorizontalAlign="Center" Width="12%" Wrap="true"/></asp:HyperLinkField>
                                                    <asp:BoundField DataField="ROOMNM" HeaderText="房型"><ItemStyle HorizontalAlign="Center" Width="16%" Wrap="true"/></asp:BoundField>
                                                    <asp:BoundField DataField="GTNAME" HeaderText="入住人姓名"><ItemStyle HorizontalAlign="Center"  Width="8%" Wrap="true"/></asp:BoundField>
                                                    <asp:BoundField DataField="INOUTDT" HeaderText="入住-离店" ><ItemStyle HorizontalAlign="Center"  Width="10%" Wrap="true"/></asp:BoundField>
                                                    <asp:BoundField DataField="LOGINMOBILE" HeaderText="预订手机号"><ItemStyle HorizontalAlign="Center"  Width="10%" Wrap="true"/></asp:BoundField>
                                                    <asp:BoundField DataField="HCONNU" HeaderText="预订确认号"><ItemStyle HorizontalAlign="Center" Width="11%" Wrap="true"/></asp:BoundField>
                                                    <asp:BoundField DataField="ORDERST" HeaderText="审核状态"><ItemStyle HorizontalAlign="Center"  Width="7%" Wrap="true"/></asp:BoundField>
                                                    <asp:TemplateField HeaderText="审核操作" ItemStyle-Width="23%">
                                                    <ItemTemplate>
                                                    <asp:Button ID="lkLeave" CssClass="btn primary" runat="server" Width="55px" Text="离店" OnClientClick="BtnLoadStyle();" CommandName="leave" CommandArgument='<%#String.Format("{0}_{1}",Eval("ORDERID"),Eval("HCONNU")) %>'/>
                                                    <asp:Button ID="lkNoshow" CssClass="btn" runat="server" Width="55px" Text="NS" OnClientClick="BtnLoadStyle();" CommandName="noshow" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>'/>
                                                    <asp:Button ID="lkRemark" CssClass="btn" runat="server" Width="55px" Text="备注" OnClientClick="BtnLoadStyle();" CommandName="remark" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>'/>
                                                    <input type="button" class="btn" id="btnIssue" style="width:65px" value='问题单' onclick="OpenIssuePage('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>')" />
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                            <PagerStyle HorizontalAlign="Right" />
                                            <RowStyle CssClass="GView_ItemCSS" />
                                            <HeaderStyle CssClass="gvHideHeader" />
                                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
            </table>
            <div id="background" class="pcbackground" style="display: none;">
            </div>
            <div id="progressBar" class="pcprogressBar" style="display: none;">
                数据加载中，请稍等...</div>
            <div id="bgDiv2" class="bgDiv2">
            </div>
            <div id="popupDiv2" class="popupDiv2">
                  <div class="frame01" style="width:99%;height:99%;margin-left:0px">
                  <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                   <ContentTemplate>
                   </ContentTemplate>
                    </asp:UpdatePanel>
                    <ul>
                        <li class="title" style="text-align:left">订单审核备注</li>
                        <li>
                        <table width="97%">
                                <tr>
                                    <td align="left" colspan="4">
                                        <font size="4px"><b><asp:Label ID="lbAproLable" runat="server" /></b></font>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trAction" runat="server">
                                    <td align="right">
                                        入住房号：
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInRoomID" runat="server" Width="180px" MaxLength="20"/><font color="red">*</font>
                                    </td>
                                    <td align="right">
                                        确认号：
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="txtActionID" runat="server" Width="180px" MaxLength="20"/>
                                    </td>
                                </tr>
                                <tr id="trNs" runat="server" style="display:none">
                                    <td align="right">
                                        NS原因：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:DropDownList ID="ddpNoShow" CssClass="noborder_inactive" runat="server" Width="250px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        备注：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:TextBox ID="txtBOOK_REMARK" runat="server" TextMode="MultiLine" Width="98.5%"
                                            Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="4" >
                                        <asp:Button ID="btnAddRemark" CssClass="btn primary" runat="server" Text="确认" OnClientClick="BtnLoadStyle();" OnClick="btnAddRemark_Click" />&nbsp;
                                        <input type="button" id="btnCancelRoom" class="btn" value="取消"  onclick="invokeCloseList();" />
                                    </td>
                                </tr>
                            </table>
                            <div id="detailMessageContent" runat="server" style="color: red"></div>
                        </li>
                        <li>
                            <div style="width:100%; height: 375px;overflow-y: auto">
                                <asp:Label ID="lbMemo1" runat="server" />
                            </div>
                        </li>
                    </ul>
                    <asp:HiddenField ID="hidSelectHotel" runat="server" />
                    <asp:HiddenField ID="hidSelectCity" runat="server" />
                    <asp:HiddenField ID="hidSelectBussiness" runat="server" />
                    <asp:HiddenField ID="hidSelectSalesID" runat="server" />
                    <asp:HiddenField ID="HidDdlSelectValue" runat="server" />

                    <asp:HiddenField ID="HidScrollValue" runat="server" />
                    <asp:HiddenField ID="dvscrollX" runat="server" />
                    <asp:HiddenField ID="dvscrollY" runat="server" />
                    <asp:HiddenField ID="HidSelIndex" runat="server" />
                    <asp:HiddenField ID="HidLastOrNextByHotel" runat="server" />
                    <input type="button" id="btnLoadOrderList" runat="server" onserverclick="btnLoadOrderList_Click" style="display: none;" />
                    <input type="button" id="btnReLoad" runat="server" onserverclick="btnReLoad_Click" style="display: none;" />
                    <asp:HiddenField ID="hidActionType" runat="server" />
                    <asp:HiddenField ID="hidOrderID" runat="server" />
                    <asp:HiddenField ID="hidFaxStatus" runat="server" />
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>