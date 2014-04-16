<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ReciveFaxSearch.aspx.cs"
    Title="接收传真管理" Inherits="ReciveFaxSearch" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
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
        
        .Tb_BodyCSS tr
        {
            height: 30px;
        }
        .Tb_BodyCSS td
        {
            height: 30px;
            border-right: 1px #d5d5d5 solid;
            border-top: 1px #d5d5d5 solid;
        }
        
        .Tb_BodyCSS
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1pxsolid#ccc;
        }
        
        .flipy
        {
            -moz-transform: scaleY(-1);
            -webkit-transform: scaleY(-1);
            -o-transform: scaleY(-1);
            transform: scaleY(-1); /*IE*/
            filter: FlipH FlipV;
            -moz-transform: rotate(180deg);
            -webkit-transform: rotate(180deg);
            -o-transform: rotate(180deg);
        }
    </style>
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script language="javascript" type="text/javascript">
        function IsConfirm() {
            if (confirm("您确认该传真属于广告/垃圾传真而无需处理么？")) {
                document.getElementById("<%=btnDeleteFax.ClientID%>").click();
            }
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

        function SetImagePreview(arg, index) {
            BtnLoadStyle();
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
            document.getElementById("<%=dvfaxImage.ClientID%>").style.display = "";
            var output = "";
            document.getElementById("<%=hidImageSrc.ClientID%>").value = arg;
            document.getElementById("<%=hidSelectIndex.ClientID%>").value = index;

            if (arg.indexOf(",") != -1) {
                var arrmp = arg.split(",");
                for (i = 0; i < arrmp.length; i++) {
                    if (arrmp[i] != "") {
                        output += "<input type=\"image\" src='" + arrmp[i] + "' onclick=\"return false;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1100px;\"/><br/>";
                    }
                }
            }
            else {
                output += "<input type=\"image\" src='" + arg + "' onclick=\"return false;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1100px;\"/>";
            }

            document.getElementById("<%=dvfaxImage.ClientID%>").innerHTML = output;

            if (window.addEventListener) {
                window.addEventListener('DOMMouseScroll', wheelFax, false); //给firefox添加鼠标滚动事件
            }

            BtnCompleteStyle();
        }

        function SetBindImagePreview(arg, index) {
            BtnLoadStyle();
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
            document.getElementById("<%=dvBindfaxImage.ClientID%>").style.display = "";
            var output = "";
            document.getElementById("<%=hidBindImageSrc.ClientID%>").value = arg;
            document.getElementById("<%=hidBindSelectIndex.ClientID%>").value = index;

            if (arg.indexOf(",") != -1) {
                var arrmp = arg.split(",");
                for (i = 0; i < arrmp.length; i++) {
                    if (arrmp[i] != "") {
                        output += "<input type=\"image\" src='" + arrmp[i] + "' onclick=\"return false;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:890px;\"/><br/>";
                    }
                }
            }
            else {
                output += "<input type=\"image\" src='" + arg + "' alt=\"\" onclick=\"return false;\" onmousewheel=\"return bbimg(this)\" style=\"width:100%;height:890px;\"/>";
            }

            document.getElementById("<%=dvBindfaxImage.ClientID%>").innerHTML = output;
            if (window.addEventListener) {
                window.addEventListener('DOMMouseScroll', wheelBindFax, false); //给firefox添加鼠标滚动事件
            }
            BtnCompleteStyle();
        }

        function SetBindImageVerifyPreview(arg, index) {
            BtnLoadStyle();
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
            document.getElementById("<%=dvBindfaxImageVerify.ClientID%>").style.display = "";
            var output = "";
            document.getElementById("<%=hidBindImageSrc.ClientID%>").value = arg;
            document.getElementById("<%=hidBindSelectIndex.ClientID%>").value = index;

            if (arg.indexOf(",") != -1) {
                var arrmp = arg.split(",");
                for (i = 0; i < arrmp.length; i++) {
                    if (arrmp[i] != "") {
                        output += "<input type=\"image\" src='" + arrmp[i] + "' onclick=\"return false;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1300px;\"/><br/>";
                    }
                }
            }
            else {
                output += "<input type=\"image\" src='" + arg + "' alt=\"\" onclick=\"return false;\" onmousewheel=\"return bbimg(this)\" style=\"width:100%;height:1300px;\"/>";
            }

            document.getElementById("<%=dvBindfaxImageVerify.ClientID%>").innerHTML = output;
            if (window.addEventListener) {
                window.addEventListener('DOMMouseScroll', wheelBindFax, false); //给firefox添加鼠标滚动事件
            }
            BtnCompleteStyle();
        }

        function bbimg(o) {
            var zoom = parseInt(o.style.zoom, 10) || 100;
            zoom += event.wheelDelta / 12;
            if (zoom > 0) o.style.zoom = zoom + '%';
            return false;
        }

        function wheelFax(event) {
            var delta = 0;
            var div = document.getElementById("<%=dvfaxImage.ClientID%>");
            if (!event) /**//* For IE. */
            {
                event = window.event;
            }
            if (event.wheelDelta) {
                //让一个是IE
                //if(event.srcElement.tagName=="IMG" && div.contains(event.srcElement))
                //{
                //event.srcElement.width+=event.wheelDelta/15;
                // return false;
                //}
            }
            else if (event.detail) {
                //如果是firefox
                var allImg = div.childNodes
                var isThis = false; //现判断鼠标中仑的元素是不是包含在那个div里面
                for (i = 0; i < allImg.length; i++) {
                    if (allImg[i] == event.target) {
                        isThis = true;
                    }
                }
                if (isThis && event.target.tagName == "INPUT") {
                    $(event.target).width(event.target.width + event.detail * 12);
                    $(event.target).height(event.target.height + event.detail * 12);
                    event.returnValue = false;
                }
            }
            return true;
        }

        function wheelBindFax(event) {
            var delta = 0;
            var div = document.getElementById("<%=dvBindfaxImage.ClientID%>");
            if (!event) /**//* For IE. */
            {
                event = window.event;
            }
            if (event.wheelDelta) {
                //让一个是IE
                //if(event.srcElement.tagName=="IMG" && div.contains(event.srcElement))
                //{
                //event.srcElement.width+=event.wheelDelta/15;
                // return false;
                //}
            }
            else if (event.detail) {
                //如果是firefox
                var allImg = div.childNodes
                var isThis = false; //现判断鼠标中仑的元素是不是包含在那个div里面
                for (i = 0; i < allImg.length; i++) {
                    if (allImg[i] == event.target) {
                        isThis = true;
                    }
                }
                if (isThis && event.target.tagName == "INPUT") {
                    $(event.target).width(event.target.width + event.detail * 12);
                    $(event.target).height(event.target.height + event.detail * 12);
                    event.returnValue = false;
                }
            }
            return true;
        }

        function RotateLeftImage() {
            var div = document.getElementById("<%=dvfaxImage.ClientID%>");
            var lis = div.childNodes;
            for (var i = 0; i < lis.length; i++) {
                if (lis.item(i).className == "") {
                    lis.item(i).setAttribute("class", "flipy");
                    lis.item(i).setAttribute("className", "flipy");
                } else {
                    lis.item(i).setAttribute("class", "");
                    lis.item(i).setAttribute("className", "");
                }

                lis.item(i).onmousewheel = function () { return bbimg(this) };
            }
        }

        function RotateRightImage() {
            var div = document.getElementById("<%=dvBindfaxImage.ClientID%>");
            var lis = div.childNodes;
            for (var i = 0; i < lis.length; i++) {
                if (lis.item(i).className == "") {
                    lis.item(i).setAttribute("class", "flipy");
                    lis.item(i).setAttribute("className", "flipy");
                } else {
                    lis.item(i).setAttribute("class", "");
                    lis.item(i).setAttribute("className", "");
                }

                lis.item(i).onmousewheel = function () { return bbimg(this) };
            }
        }

        function changeImg(btn, arg) //鼠标移入，更换图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock_press.png";
            }
            else {
                btn.src = "../../Images/clock_press.png";
            }
        }
        function changeback(btn, arg)  //鼠标移出，换回原来的图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock.png";
            }
            else {
                btn.src = "../../Images/clock.png";
            }
        }


        function RotateRightVerifyImage() {
            var div = document.getElementById("<%=dvBindfaxImageVerify.ClientID%>");
            var lis = div.childNodes;
            for (var i = 0; i < lis.length; i++) {
                if (lis.item(i).className == "") {
                    lis.item(i).setAttribute("class", "flipy");
                    lis.item(i).setAttribute("className", "flipy");
                } else {
                    lis.item(i).setAttribute("class", "");
                    lis.item(i).setAttribute("className", "");
                }

                lis.item(i).onmousewheel = function () { return bbimg(this) };
            }
        }

        function changeImgVerify(btn, arg) //鼠标移入，更换图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock_press.png";
            }
            else {
                btn.src = "../../Images/clock_press.png";
            }
        }
        function changebackVerify(btn, arg)  //鼠标移出，换回原来的图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock.png";
            }
            else {
                btn.src = "../../Images/clock.png";
            }
        }


       
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div style="margin: 5px 14px 5px 14px;" id="dvUDHotel" runat="server">
        <div id="tabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#tabs-1">未知传真绑定 </a></li>
                <li><a href="#tabs-2">已绑定传真处理 </a></li>
                <li><a href="#tabs-3">审核回传查看 </a></li>
            </ul>
            <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                <div style="width: 100%; margin: -2px -5px -5px -5px">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 101%; height: 50px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;">
                                <tr style="width: 100%;">
                                    <td align="right" style="width: 10%">
                                        接收时间：
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <input id="dpCreateStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <input id="dpCreateEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td align="left" style="width: 60%">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 100.7%; display: none; margin: 8px -5px -5px -3px" id="tbData"
                                runat="server">
                                <tr>
                                    <td style="width: 30%;" align="left" valign="top">
                                        <div style="width: 100%; height: 990px; overflow-y: auto">
                                            <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                                                DataKeyNames="ID" CssClass="GView_BodyCSS" OnRowDataBound="gridViewCSReviewLmSystemLogList_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="CallInNum" HeaderText="传真号码">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:BoundField>
                                                    <%--<asp:BoundField DataField="HotelName" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="25%"/></asp:BoundField>--%>
                                                    <asp:BoundField DataField="RecDate" HeaderText="接收时间">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <RowStyle CssClass="GView_ItemCSS" />
                                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                <SelectedRowStyle BackColor="#E9E9E9" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td style="width: 70%; height: 100%" align="left" valign="top">
                                        <table style="width: 100%; height: 50px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;
                                            margin-left: 8px">
                                            <tr>
                                                <td align="right" style="width: 15%">
                                                    二维码编号：
                                                </td>
                                                <td align="left" style="width: 85%">
                                                    <div style="float: left">
                                                        <asp:TextBox ID="txtUNBarCode" runat="server" Width="150px" MaxLength="30" />
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                &nbsp;&nbsp;<asp:Button ID="btn" runat="server" CssClass="btn primary" Text="绑定传真"
                                                                    OnClientClick="BtnLoadStyle();" OnClick="btnSet_Click" />
                                                                &nbsp;&nbsp;<input type="button" id="btnDelete" class="btn primary" value="无需处理"
                                                                    onclick="IsConfirm()" />
                                                                <input type="button" id="btnDeleteFax" class="btn primary" runat="server" style="display: none;"
                                                                    onserverclick="btnDeleteFax_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div style="float: left; margin-left: 10px">
                                                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                                            <ContentTemplate>
                                                                <div id="messageContent" runat="server" style="color: red">
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                                <td>
                                                    <img alt="" style="width: 30px; height: 30px" src="../../Images/clock.png" title="传真向右旋转90°"
                                                        onmousedown="changeImg(this, '1')" onmouseout="changeback(this, '1')" onclick="RotateLeftImage()" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: 925px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;
                                            margin-left: 8px; margin-top: 13px">
                                            <tr>
                                                <td colspan="2">
                                                    <div id="dvfaxImage" runat="server" style="width: 100%; height: 925px; display: none;
                                                        overflow-y: auto;">
                                                        <%--<asp:Image runat="server" ID="imgPre" Width="100%" Height="920px"/>--%>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                <div style="width: 100%; margin: -2px -5px -5px -5px">
                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 101%; height: 50px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;">
                                <tr style="width: 100%;">
                                    <td align="right" style="width: 10%">
                                        业务类型：
                                    </td>
                                    <td align="right" style="width: 10%">
                                        <asp:DropDownList ID="ddpLinkType" CssClass="noborder_inactive" runat="server" Width="160px" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                        订单号：
                                    </td>
                                    <td align="right" style="width: 10%">
                                        <asp:TextBox ID="txtOrderID" runat="server" Width="150px" MaxLength="30" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                        接收时间：
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <input id="dpBindCreateStart" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpBindCreateEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <input id="dpBindCreateEnd" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpBindCreateStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:Button ID="btnBindSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();"
                                            OnClick="btnBindSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 100.7%; display: none; margin: 8px -5px -5px -3px" id="tbBindData"
                                runat="server">
                                <tr>
                                    <td style="width: 30%;" align="left" valign="top">
                                        <div style="width: 100%; height: 990px; overflow-y: auto">
                                            <asp:GridView ID="gridViewBindList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID"
                                                CssClass="GView_BodyCSS" OnRowDataBound="gridViewBindList_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="CallInNum" HeaderText="传真号码">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RecDate" HeaderText="接收时间">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <RowStyle CssClass="GView_ItemCSS" />
                                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                <SelectedRowStyle BackColor="#E9E9E9" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td style="width: 70%; height: 100%" align="left" valign="top">
                                        <table style="width: 100%; height: 80px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;
                                            margin-left: 8px">
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    请重新选择绑定目标：
                                                </td>
                                                <td colspan="3">
                                                    <div style="float: left; margin-left: 10px">
                                                        <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Always" runat="server">
                                                            <ContentTemplate>
                                                                <div id="bindmessageContent" runat="server" style="color: red">
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    绑定类型：
                                                </td>
                                                <td align="right" style="width: 15%">
                                                    <asp:DropDownList ID="ddpReLinkType" CssClass="noborder_inactive" runat="server"
                                                        Width="160px" />
                                                </td>
                                                <td align="right" style="width: 15%">
                                                    二维码编号：
                                                </td>
                                                <td align="left" style="width: 50%">
                                                    <div style="float: left">
                                                        <asp:TextBox ID="txtBindOrderID" runat="server" Width="150px" MaxLength="30" />
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="确定"
                                                                    OnClientClick="BtnLoadStyle();" OnClick="btnSave_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                                <td>
                                                    <img alt="" style="width: 30px; height: 30px" src="../../Images/clock.png" title="传真向右旋转90°"
                                                        onmousedown="changeImg(this, '1')" onmouseout="changeback(this, '1')" onclick="RotateRightImage()" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: 895px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;
                                            margin-left: 8px; margin-top: 13px">
                                            <tr>
                                                <td colspan="2">
                                                    <div id="dvBindfaxImage" runat="server" style="width: 100%; height: 895px; display: none;
                                                        overflow-y: auto;">
                                                        <%--<asp:Image runat="server" ID="imgBindPre" Width="100%" Height="890px"/>--%>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="tabs-3" style="border: 1px solid #D5D5D5;">
                <div style="width: 100%; margin: 8px -5px -5px -5px">
                    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 101%; height: 50px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;">
                                <tr style="width: 100%;">
                                    <td align="right" style="width: 10%">
                                        业务类型：
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <asp:DropDownList ID="ddpLinkTypeVerify" CssClass="noborder_inactive" runat="server"
                                            Width="160px" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                        接收时间：
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <input id="dpBindCreateStartVerify" class="Wdate" type="text" style="width: 150px"
                                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpBindCreateEndVerify\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <input id="dpBindCreateEndVerify" class="Wdate" type="text" style="width: 150px"
                                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpBindCreateStartVerify\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:Button ID="btnBindSearchVerify" runat="server" CssClass="btn primary" Text="搜索"
                                            OnClientClick="BtnLoadStyle();" OnClick="btnBindSearchVerify_Click" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                    </td>
                                    <td align="left" style="width: 10%">
                                    </td>
                                    <td align="right">
                                        <img alt="" style="width: 30px; height: 30px" src="../../Images/clock.png" title="传真向右旋转90°"
                                            onmousedown="changeImgVerify(this, '1')" onmouseout="changebackVerify(this, '1')"
                                            onclick="RotateRightVerifyImage()" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 100.7%; display: none; margin: 0px -5px -5px -3px" id="tbBindDataVerify"
                                runat="server">
                                <tr>
                                    <td style="width: 30%;" align="left" valign="top">
                                        <div style="width: 100%; height: 990px; overflow-y: auto">
                                            <asp:GridView ID="gridViewBindListVerify" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                                                DataKeyNames="ID" CssClass="GView_BodyCSS" OnRowDataBound="gridViewBindListVerify_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="CallInNum" HeaderText="传真号码">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RecDate" HeaderText="接收时间">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <RowStyle CssClass="GView_ItemCSS" />
                                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                <SelectedRowStyle BackColor="#E9E9E9" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td style="width: 70%; height: 100%" align="left" valign="top">
                                        <table style="width: 100%; height: 895px; border: 1px #d5d5d5 solid; background-color: #F2F2F2;
                                            margin-left: 8px; margin-top: 0px; overflow-y: auto;">
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <div id="dvBindfaxImageVerify" runat="server" style="width: 100%; height: 895px;
                                                        display: none; overflow-y: auto;">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div id="background" class="pcbackground" style="display: none;">
                </div>
                <div id="progressBar" class="pcprogressBar" style="display: none;">
                    数据加载中，请稍等...</div>
                <asp:HiddenField ID="hidSelectIndex" runat="server" />
                <asp:HiddenField ID="hidImageSrc" runat="server" />
                <asp:HiddenField ID="hidBindSelectIndex" runat="server" />
                <asp:HiddenField ID="hidBindImageSrc" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hidSelectedID" runat="server" />
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
