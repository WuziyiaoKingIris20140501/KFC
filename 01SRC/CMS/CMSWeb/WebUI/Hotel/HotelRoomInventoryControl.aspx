<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="HotelRoomInventoryControl.aspx.cs" Title="房态管理" Inherits="HotelRoomInventoryControl" %>

<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <style type="text/css">
        .bgDiv2
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            right: 0px;
            background-color: #000000;
            filter: alpha(Opacity=80);
            -moz-opacity: 0.5;
            opacity: 0.5;
            z-index: 100;
            background-color: #000000;
            opacity: 0.6;
        }
        
        .popupDiv2
        {
            width: 240px;
            height: 185px;
            top: 55%;
            left: 45%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            text-align: center;
            border: solid 2px #ff8300;
            z-index: 10001;
            display: none;
            background-color: White;
        }
        .style1
        {
            width: 120px;
        }
        .style2
        {
            width: 253px;
        }
        .style3
        {
            width: 222px;
        }
        
        .important td:hover td
        {
            background: none;
        }
        .important td:hover
        {
            background-color: blue;
        }
        
        .important1 table:hover tr td
        {
            background: none;
        }
        .important1 table:hover
        {
            background-color: blue;
        }
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
    </style>
    <script type="text/javascript">
        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        function RenewRoomList(obj) {
            if (obj == "LMBAR2") {
                document.getElementById("<%=chkHotelRoomListLMBARDIV.ClientID%>").style.display = 'none';
                document.getElementById("<%=chkHotelRoomListLMBAR2DIV.ClientID%>").style.display = '';
            } else {
                document.getElementById("<%=chkHotelRoomListLMBAR2DIV.ClientID%>").style.display = 'none';
                document.getElementById("<%=chkHotelRoomListLMBARDIV.ClientID%>").style.display = '';
            }
        }

        function showDiv(priceCode, roomame, roomCode, twoPrice, roomnum, status, isreserve, effectDate) {
            //价格代码   房型名称  房型代码   价格   房量   状态  是否是保留房    生效时间

            document.getElementById("<%=lblDivRoomType.ClientID%>").innerHTML = roomame; //房型名称
            document.getElementById("<%=HiddenRoomType.ClientID%>").value = roomame;

            document.getElementById("<%=lblDivPrice.ClientID%>").innerHTML = twoPrice; //价格
            document.getElementById("<%=HiddenPrice.ClientID%>").value = twoPrice;

            document.getElementById("<%=HiddenPriceCode.ClientID%>").value = priceCode; //价格代码

            document.getElementById("<%=txtDivRoomCount.ClientID%>").value = roomnum; //房量

            if (status == "true") {
                showRoomDiv();
                document.getElementById("<%=dropDivStatusOpen.ClientID%>").checked = true;
            } else {
                closeRoomDiv();
                document.getElementById("<%=dropDivStatusClose.ClientID%>").checked = true;
            }

            if (isreserve == "0") {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = true;
            } else {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = false;
            }
            document.getElementById("<%=HiddenEffectDate.ClientID%>").value = effectDate;
            document.getElementById("<%=HiddenRoomCode.ClientID%>").value = roomCode;
            invokeOpen2();
        }

        //显示弹出的层
        function invokeOpen2() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
        }

        //隐藏弹出的层
        function invokeClose2() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function showManagerDiv() {
            document.getElementById("<%=managerTxtRoomCount.ClientID%>").style.display = "block";
            document.getElementById("<%=manegerCkReserve.ClientID%>").style.display = "block";
        }

        function closeManagerDiv() {
            document.getElementById("<%=managerTxtRoomCount.ClientID%>").style.display = "none";
            document.getElementById("<%=manegerCkReserve.ClientID%>").style.display = "none";
        }

        function showRoomDiv() {

            document.getElementById("<%=divRoomCount.ClientID%>").style.display = "block";
            document.getElementById("<%=divckReserve.ClientID%>").style.display = "block";
        }

        function closeRoomDiv() {

            document.getElementById("<%=divRoomCount.ClientID%>").style.display = "none";
            document.getElementById("<%=divckReserve.ClientID%>").style.display = "none";
        }

        function GetCheckBoxListValue() {
            document.getElementById("<%=btnRenewPlan.ClientID%>").value = '更新中...';
            var v = new Array();
            var chkPriceCodeVal;

            var chkPriceCode = document.getElementById("<%=rdLmbar2.ClientID%>");
            if (chkPriceCode.checked) {
                chkPriceCodeVal = chkPriceCode.value;
            }

            alert(chkPriceCodeVal);
            if (chkPriceCodeVal == "LMBAR2") {
                var CheckBoxList = document.getElementById("<%=chkHotelRoomListLMBAR2.ClientID %>");
                if (CheckBoxList.tagName == "TABLE") {
                    for (i = 0; i < CheckBoxList.rows.length; i++) {
                        for (j = 0; j < CheckBoxList.rows[i].cells.length; j++) {
                            if (CheckBoxList.rows[i].cells[j].childNodes[0]) {
                                if (CheckBoxList.rows[i].cells[j].childNodes[0].checked == true) {
                                    v.push(CheckBoxList.rows[i].cells[j].childNodes[1].innerHTML);
                                }
                            }
                        }
                    }
                }
                if (CheckBoxList.tagName == "SPAN") {
                    for (i = 0; i < CheckBoxList.childNodes.length; i++)
                        if (CheckBoxList.childNodes[i].tagName == "INPUT")
                            if (CheckBoxList.childNodes[i].checked == true) {
                                i++;
                                v.push(CheckBoxList.childNodes[i].innerHTML);
                            }
                }
            } else {
                var CheckBoxList = document.getElementById("<%=chkHotelRoomListLMBAR.ClientID %>");
                if (CheckBoxList.tagName == "TABLE") {
                    for (i = 0; i < CheckBoxList.rows.length; i++) {
                        for (j = 0; j < CheckBoxList.rows[i].cells.length; j++) {
                            if (CheckBoxList.rows[i].cells[j].childNodes[0]) {
                                if (CheckBoxList.rows[i].cells[j].childNodes[0].checked == true) {
                                    v.push(CheckBoxList.rows[i].cells[j].childNodes[1].innerHTML);
                                }
                            }
                        }
                    }
                }
                if (CheckBoxList.tagName == "SPAN") {
                    for (i = 0; i < CheckBoxList.childNodes.length; i++)
                        if (CheckBoxList.childNodes[i].tagName == "INPUT")
                            if (CheckBoxList.childNodes[i].checked == true) {
                                i++;
                                v.push(CheckBoxList.childNodes[i].innerHTML);
                            }
                }
            }
            if (v == "") {
                alert("请选择房型!");
                document.getElementById("<%=btnRenewPlan.ClientID%>").value = '更新计划';
                return false;
            }

            //             var dropStatusOpen = document.getElementById("<%=dropStatusOpen.ClientID%>").checked;
            //             if (dropStatusOpen) {
            //                 var RoomCount = document.getElementById("<%=txtRoomCount.ClientID%>").value;
            //                 if (RoomCount == "") {
            //                     alert("请输入需要更新房型的房量!");
            //                     document.getElementById("<%=btnRenewPlan.ClientID%>").value = '更新计划';
            //                     return false;
            //                 }
            //             }             
        }

        function ckButton() {
            document.getElementById("<%=btnDivRenewPlan.ClientID%>").value = '更新中...';
        }

        function clearText() {
            alert("wctHotel");
            document.getElementById('wctHotel').value = '更新中...';
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">酒店房态控制</li>
                    <li>
                        <table>
                            <tr>
                                <td>
                                    计划起止日期：
                                </td>
                                <td>
                                    <input id="planStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_planEndDate\')||\'2020-10-01\'}'})"
                                        runat="server" />
                                </td>
                                <td>
                                    至：
                                </td>
                                <td>
                                    <input id="planEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_planStartDate\')}',maxDate:'2020-10-01'})"
                                        runat="server" />
                                </td>
                                <td>
                                    选择酒店：
                                </td>
                                <td>
                                    <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel"
                                        AutoParent="HotelRoomInventoryControl.aspx?Type=hotel" />
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="选择" OnClientClick="BtnLoadStyle()"
                                                OnClick="btnSelect_Click" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" Visible="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
            <div id="background" class="pcbackground" style="display: none;">
            </div>
            <div id="progressBar" class="pcprogressBar" style="display: none;">
                数据加载中，请稍等...</div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="frame01" id="DivhotelDetails" runat="server" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">酒店信息<asp:Label ID="lblHotelDetails" runat="server" Text=""></asp:Label></li>
                </ul>
                <ul>
                    <li>
                        <table>
                            <tr style="border-left: 50px; vertical-align: text-bottom">
                                <td colspan="2">
                                    <%-- [8393] 上海东方航空宾馆--%>
                                    <div runat="server" visible="false">
                                        <asp:Label ID="lblHotelID" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblhotelName" runat="server" Text="" Width="300px"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    酒店联系人：
                                </td>
                                <td>
                                    <asp:Label ID="ContactMan" runat="server" Text="" Width="300px"></asp:Label>
                                </td>
                                <td style="width: 50px">
                                </td>
                                <td align="right">
                                    电话：
                                </td>
                                <td>
                                    <asp:Label ID="ContactTel" runat="server" Text="" Width="300px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    HotelVP酒店销售：
                                </td>
                                <td>
                                    <asp:Label ID="LinkMan" runat="server" Text="" Width="300px"></asp:Label>
                                </td>
                                <td style="width: 50px">
                                </td>
                                <td align="right">
                                    电话：
                                </td>
                                <td>
                                    <asp:Label ID="LinkTel" runat="server" Text="" Width="300px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </li>
                </ul>
            </div>
            <div class="frame01" id="DivPlanDetails" runat="server" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">计划信息<span style="float: right;"><span style="text-align: right;
                        color: White">█</span>无计划&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: #CDEBFF;">█</span>周末&nbsp;&nbsp;&nbsp;<span
                            style="text-align: right; color: #E6B9B6">█</span>满房&nbsp;&nbsp;&nbsp;<span style="text-align: right;
                                color: #E96928">█</span>CC操作满房&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: #999999">█</span>计划关闭&nbsp;&nbsp;&nbsp;<span
                                    style="text-align: right; color: Red">*</span>保留房&nbsp;&nbsp;&nbsp;</span></li>
                </ul>
                <ul>
                    <li style="display: none;">
                        <table width="95%">
                            <tr align="right">
                                <td>
                                    <table border="1" style="border-color: #D5D5D5; border-collapse: collapse;">
                                        <tr align="center">
                                            <td style="width: 60px">
                                                无计划
                                            </td>
                                            <td style="background-color: #E6B9B6; font-style: inherit; width: 60px">
                                                满房
                                            </td>
                                            <td style="background-color: #E96928; font-style: inherit; width: 80px">
                                                CC操作满房
                                            </td>
                                            <td style="background-color: #999999; font-style: inherit; width: 60px">
                                                计划关闭
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="right" style="margin-right: 128px">
                                <td>
                                    <table border="1" style="border-collapse: collapse;">
                                        <tr align="center">
                                            <td style="width: 60px;">
                                                <span style="color: Red; text-align: center; margin-top: auto;">*</span>保留房
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </li>
                    <li style="margin: 0px; padding-top: 0px;">
                        <div style="width: 1200px; overflow-x: auto; border: 1px   solid   white;">
                            <h4 id="lmbar2H" runat="server" style="padding-top: 0px; margin-top: 0px; margin-bottom: 0px;">
                                LMBAR2</h4>
                            <table border="1" style="border-color: #D5D5D5; border-collapse: collapse; padding-top: 0px"
                                cellpadding="0" cellspacing="0">
                                <%for (int i = 0; i < LMBAR2RoomList.Rows.Count; i++)
                                  {
                                      if (LMBAR2RoomList.Rows[i]["ROOMCODE"].ToString() == "")
                                      {
                                %>
                                <tr align="center" style="border-collapse: collapse">
                                    <%for (int j = 0; j < TimeList.Rows.Count; j++)
                                      {
                                          if (!string.IsNullOrEmpty(TimeList.Rows[j]["time"].ToString()))
                                          {
                                              if (DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Sunday")
                                              { 
                                    %>
                                    <td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#CDEBFF'"
                                        style="background-color: #CDEBFF; font-style: inherit; width: 100px; height: 40px;
                                        white-space: nowrap; border: solid #D5D5D5 1px;">
                                        &nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%
                                              }
                                              else
                                              { 
                                    %>
                                    <td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'"
                                        style="width: 100px; height: 40px; white-space: nowrap; border: solid #D5D5D5 1px;">
                                        &nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%
                                              }
                                          }
                                          else
                                          {
                                    %>
                                    <td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'"
                                        style="width: 100px; height: 40px; white-space: nowrap; border: solid #D5D5D5 1px;">
                                        &nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%
                                          }
                                      }%>
                                </tr>
                                <%}
                                      else
                                      { 
                                %>
                                <tr align="center">
                                    <td style="width: 100px; white-space: nowrap; border: solid #D5D5D5 1px;">
                                        <%=LMBAR2RoomList.Rows[i]["ROOMNM"].ToString() + "（" + LMBAR2RoomList.Rows[i]["ROOMCODE"].ToString()+")" %>
                                    </td>
                                    <%for (int j = 1; j < TimeList.Rows.Count; j++)
                                      {
                                    %><td style="border: solid #D5D5D5 1px; height: 40px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                        onmouseout="javacript:this.style.backgroundColor='white'">
                                        <%
                                          for (int l = 0; l < Lmbar2PlanList.Rows.Count; l++)
                                          {
                                              if (Lmbar2PlanList.Rows[l]["ROOMTYPECODE"].ToString() == LMBAR2RoomList.Rows[i]["ROOMCODE"].ToString() && DateTime.Parse(Lmbar2PlanList.Rows[l]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(TimeList.Rows[j]["time"].ToString()))
                                              {
                                        %>
                                        <table cellpadding="0" cellspacing="0" width="100%" style="border-bottom: none; height: 100%;"
                                            onclick="showDiv('LMBAR2','<%=LMBAR2RoomList.Rows[i]["ROOMNM"].ToString() %>','<%=LMBAR2RoomList.Rows[i]["ROOMCODE"].ToString() %>','<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%>','<%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%>','<%= Lmbar2PlanList.Rows[l]["STATUS"].ToString()%>','<%= Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString()%>','<%=Lmbar2PlanList.Rows[l]["EFFECTDATESTRING"].ToString() %>')">
                                            <%if (Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString() == "0")
                                              {
                                                  if (Lmbar2PlanList.Rows[l]["STATUS"].ToString() == "false")
                                                  {
                                            %>
                                            <tr>
                                                <td style="background-color: #999999; font-style: inherit; text-align: center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a16">
                                                                    <%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a17"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a15">￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                                  else if (Lmbar2PlanList.Rows[l]["ISROOMFUL"].ToString() == "1")
                                                  { 
                                            %>
                                            <tr>
                                                <td style="background-color: #E96928; font-style: inherit; text-align: center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#E96928'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a14">
                                                                    <%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a18"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a13">￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                                  else
                                                  { 
                                            %>
                                            <tr>
                                                <td style="background-color: #E6B9B6; font-style: inherit; text-align: center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#E6B9B6'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a14">
                                                                    <%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a18"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a13">￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                              }
                                              else
                                              {
                                                  if (Lmbar2PlanList.Rows[l]["STATUS"].ToString() == "false")
                                                  {
                                            %>
                                            <tr>
                                                <td style="background-color: #999999; font-style: inherit; text-align: center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a12">
                                                                    <%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a19"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a11">￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                                  else
                                                  { 
                                            %>
                                            <tr>
                                                <td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a10">
                                                                    <%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a20"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a9">￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                              } %>
                                        </table>
                                        <%
                                              }
                                          }
                                      } %>
                                    </td>
                                </tr>
                                <%
                                      }
                                  } %>
                            </table>
                            <h4 id="lmbarH" runat="server" style="padding-top: 0px; margin-top: 10px; margin-bottom: 0px;">
                                LMBAR</h4>
                            <table border="1" style="border-color: #D5D5D5; border-collapse: collapse;" cellpadding="0"
                                cellspacing="0">
                                <%for (int i = 0; i < LMBARRoomList.Rows.Count; i++)
                                  {
                                      if (LMBARRoomList.Rows[i]["ROOMCODE"].ToString() == "")
                                      {
                                %>
                                <tr align="center" style="border-collapse: collapse">
                                    <%for (int j = 0; j < TimeList.Rows.Count; j++)
                                      {
                                          if (!string.IsNullOrEmpty(TimeList.Rows[j]["time"].ToString()))
                                          {
                                              if (DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Sunday")
                                              { 
                                    %>
                                    <td style="background-color: #CDEBFF; font-style: inherit; width: 100px; height: 40px;
                                        white-space: nowrap; border: solid #D5D5D5 1px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                        onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                        &nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%
                                              }
                                              else
                                              { 
                                    %>
                                    <td style="width: 100px; height: 40px; white-space: nowrap; border: solid #D5D5D5 1px;"
                                        onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'">
                                        &nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%
                                              }
                                          }
                                          else
                                          {
                                    %>
                                    <td style="width: 100px; height: 40px; white-space: nowrap; border: solid #D5D5D5 1px;"
                                        onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'">
                                        &nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%
                                          }
                                      } %>
                                </tr>
                                <%}
                                      else
                                      { 
                                %>
                                <tr align="center">
                                    <td style="width: 100px; white-space: nowrap; border: solid #D5D5D5 1px;">
                                        <%=LMBARRoomList.Rows[i]["ROOMNM"].ToString() + "（" + LMBARRoomList.Rows[i]["ROOMCODE"].ToString() + ")"%>
                                    </td>
                                    <%for (int j = 1; j < TimeList.Rows.Count; j++)
                                      {
                                    %><td style="border: solid #D5D5D5 1px; height: 40px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                        onmouseout="javacript:this.style.backgroundColor='white'">
                                        <%
                                          for (int l = 0; l < LmbarPlanList.Rows.Count; l++)
                                          {
                                              if (LmbarPlanList.Rows[l]["ROOMTYPECODE"].ToString() == LMBARRoomList.Rows[i]["ROOMCODE"].ToString() && DateTime.Parse(LmbarPlanList.Rows[l]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(TimeList.Rows[j]["time"].ToString()))
                                              {
                                        %>
                                        <table cellpadding="0" cellspacing="0" width="100%" style="border-bottom: none; height: 100%"
                                            onclick="showDiv('LMBAR','<%=LMBARRoomList.Rows[i]["ROOMNM"].ToString() %>','<%=LMBARRoomList.Rows[i]["ROOMCODE"].ToString() %>','<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%>','<%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%>','<%= LmbarPlanList.Rows[l]["STATUS"].ToString()%>','<%= LmbarPlanList.Rows[l]["ISRESERVE"].ToString()%>','<%=LmbarPlanList.Rows[l]["EFFECTDATESTRING"].ToString() %>')">
                                            <%if (LmbarPlanList.Rows[l]["ROOMNUM"].ToString() == "0")
                                              {
                                                  if (LmbarPlanList.Rows[l]["STATUS"].ToString() == "false")
                                                  {
                                            %>
                                            <tr>
                                                <td style="background-color: #999999; font-style: inherit; text-align: center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a1">
                                                                    <%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a21"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a2">￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                                  else if (LmbarPlanList.Rows[l]["ISROOMFUL"].ToString() == "1")
                                                  { 
                                            %>
                                            <tr>
                                                <td style="background-color: #E96928; font-style: inherit; text-align: center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#E96928'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a25">
                                                                    <%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a26"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a27">￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                                  else
                                                  { 
                                            %>
                                            <tr>
                                                <td style="background-color: #E6B9B6; font-style: inherit; text-align: center; width: 100%"
                                                    onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#E6B9B6'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a3">
                                                                    <%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a22"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a4">￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                              }
                                              else
                                              {
                                                  if (LmbarPlanList.Rows[l]["STATUS"].ToString() == "false")
                                                  {
                                            %>
                                            <tr>
                                                <td style="background-color: #999999; font-style: inherit; text-align: center;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                    onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a5">
                                                                    <%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a23"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a6">￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                                  else
                                                  { 
                                            %>
                                            <tr>
                                                <td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'">
                                                    <table>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a7">
                                                                    <%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></a>
                                                                <%
                                                      if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                      {
                                                                %>
                                                                <a href="javascript:void(0)" id="a24"><span style="color: Red">*</span></a>
                                                                <%
                                                      }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <a href="javascript:void(0)" id="a8">￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%
                                                  }
                                              } %>
                                        </table>
                                        <%
                                              }
                                          }
                                      } %>
                                    </td>
                                </tr>
                                <%
                                      }
                                  } %>
                            </table>
                        </div>
                    </li>
                </ul>
            </div>
            <div id="bgDiv2" class="bgDiv2">
            </div>
            <div id="popupDiv2" class="popupDiv2">
                <table style="width: 240px; height: 185px">
                    <tr style="width: 210px" align="left">
                        <td>
                            <asp:Label ID="lblDivHotelName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="width: 210px" align="left">
                        <td>
                            <asp:Label ID="lblDivRoomType" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="width: 210px" align="left">
                        <td>
                            价格：<asp:Label ID="lblDivPrice" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="width: 210px" align="left">
                        <td>
                            状态:
                            <%-- <asp:DropDownList ID="dropDivStatus" runat="server" Width="150px">
                        <asp:ListItem Value="">请选择...</asp:ListItem>
                        <asp:ListItem Value="true">开启</asp:ListItem>
                        <asp:ListItem Value="false">关闭</asp:ListItem>
                    </asp:DropDownList>--%>
                            <input type="radio" runat="server" id="dropDivStatusOpen" name="dropDivStatus" value="开启"
                                onclick="showRoomDiv()" />开启&nbsp;&nbsp;&nbsp;<input type="radio" runat="server"
                                    id="dropDivStatusClose" name="dropDivStatus" value="关闭" onclick="closeRoomDiv()" />关闭
                        </td>
                    </tr>
                    <tr style="width: 210px" align="left">
                        <td>
                            <div id="divRoomCount" runat="server">
                                房量：
                                <asp:TextBox ID="txtDivRoomCount" runat="server" Width="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr style="width: 200px" align="left">
                        <td>
                            <div id="divckReserve" runat="server">
                                <asp:CheckBox ID="ckDivReserve" runat="server" Text="保留房" />
                            </div>
                        </td>
                    </tr>
                    <tr style="width: 210px">
                        <td>
                            <asp:Button ID="btnDivRenewPlan" runat="server" CssClass="btn primary" Text="更新计划"
                                OnClientClick="ckButton();" OnClick="btnDivRenewPlan_Click" />
                            <input type="button" value="取消" class="btn" onclick="invokeClose2()" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="frame01" id="DivLongPlanDetails" runat="server" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">长期计划批量更新</li>
                </ul>
                <ul>
                    <li>
                        <table width="95%">
                            <tr>
                                <td class="style1">
                                    开始日期:
                                </td>
                                <td class="style2">
                                    <input id="longPlanStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_longPlanEndDate\')||\'2020-10-01\'}'})"
                                        runat="server" />
                                </td>
                                <td class="style3">
                                    结束日期:
                                    <input id="longPlanEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_longPlanStartDate\')||\'2020-10-01\'}'})"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    价格代码：
                                </td>
                                <td colspan="3">
                                    <input type="radio" runat="server" id="rdLmbar2" name="RDPriceCode" checked="true"
                                        value="LMBAR2" onclick="RenewRoomList('LMBAR2')" />LMBAR2&nbsp;&nbsp;&nbsp;<input
                                            type="radio" runat="server" id="rdLmbar" name="RDPriceCode" value="LMBAR" onclick="RenewRoomList('LMBAR')" />LMBAR
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    选择房型：
                                </td>
                                <td colspan="3">
                                    <div id="chkHotelRoomListLMBAR2DIV" runat="server">
                                        <asp:CheckBoxList ID="chkHotelRoomListLMBAR2" runat="server" RepeatDirection="Vertical"
                                            RepeatColumns="8" RepeatLayout="Table" CellSpacing="8" />
                                    </div>
                                    <div id="chkHotelRoomListLMBARDIV" runat="server" style="display: none">
                                        <asp:CheckBoxList ID="chkHotelRoomListLMBAR" runat="server" RepeatDirection="Vertical"
                                            RepeatColumns="8" RepeatLayout="Table" CellSpacing="8" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    计划状态:
                                </td>
                                <td class="style2">
                                    <input type="radio" runat="server" id="dropStatusOpen" name="dropStatus" checked="true"
                                        value="开启" onclick="showManagerDiv()" />开启&nbsp;&nbsp;&nbsp;<input type="radio" runat="server"
                                            id="dropStatusClose" name="dropStatus" value="关闭" onclick="closeManagerDiv()" />关闭
                                </td>
                                <td class="style3">
                                    <div id="managerTxtRoomCount" style="display: block" runat="server">
                                        房量设置:
                                        <asp:TextBox ID="txtRoomCount" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div id="manegerCkReserve" style="display: block" runat="server">
                                        <asp:CheckBox ID="ckReserve" runat="server" Text="保留房" />
                                    </div>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRenewPlan" runat="server" CssClass="btn primary" Text="更新计划" OnClientClick="return GetCheckBoxListValue()"
                                                OnClick="btnRenewPlan_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
            <input type="hidden" runat="server" id="HiddenEffectDate" />
            <input type="hidden" runat="server" id="HiddenRoomCode" />
            <input type="hidden" runat="server" id="HiddenRoomType" />
            <input type="hidden" runat="server" id="HiddenPriceCode" />
            <input type="hidden" runat="server" id="HiddenPrice" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
