<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ConsultRoomUserOrWorkPage.aspx.cs"
    Title="询房用户管理" Inherits="ConsultRoomUserOrWorkPage" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ClearClickEvent() {

            document.getElementById("<%=messageContent.ClientID%>").innerHTML = "";
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

        function BtnBalLoadStyle() {
            var ajaxbg = $("#Div3,#Div4");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnBalCompleteStyle() {
            var ajaxbg = $("#Div3,#Div4");
            ajaxbg.hide();
        }

        function BtnOKCompleteStyle() {
            BtnCompleteStyle();
            document.getElementById("<%=dvBtnOK.ClientID%>").style.display = "none";
            document.getElementById("<%=dvBtnSty.ClientID%>").style.display = "";

            document.getElementById("wctCity").disabled = true;
            document.getElementById("wctTag").disabled = true;
            document.getElementById("wctHotel").disabled = true;
            document.getElementById("wctSales").disabled = true;
            document.getElementById("<%=ddpConrultType.ClientID%>").disabled = true;
        }


        function BtnDelCompleteStyle() {
            BtnCompleteStyle();
            document.getElementById("<%=btnReLoad.ClientID%>").click();
        }

        function BtnADDCompleteStyle() {
            BtnCompleteStyle();
            ChkBtnStyle();
            document.getElementById("<%=btnReLoad.ClientID%>").click();
        }

        function AddModel(arg) {
            document.getElementById("<%=ddpConrultType.ClientID%>").disabled = false;
            document.getElementById("wctCity").disabled = false;
            document.getElementById("wctTag").disabled = false;
            document.getElementById("wctHotel").disabled = false;
            document.getElementById("wctSales").disabled = false;
            if (arg == '1') {
                document.getElementById("<%=dvAddRule.ClientID%>").style.display = "";
                document.getElementById("<%=ddpConrultType.ClientID%>").selectedIndex = 2;
                document.getElementById("<%=tdCity.ClientID%>").style.display = "none";
                document.getElementById("<%=tdTag.ClientID%>").style.display = "none";
                document.getElementById("<%=tdSales.ClientID%>").style.display = "none";
                document.getElementById("<%=tdHotel.ClientID%>").style.display = "";
            }
            else {
                document.getElementById("<%=dvAddRule.ClientID%>").style.display = "none";
            }
        }

        function checkConrultType(val) {
            if ("0" == val) {
                document.getElementById("<%=tdCity.ClientID%>").style.display = "";
                document.getElementById("<%=tdTag.ClientID%>").style.display = "none";
                document.getElementById("<%=tdHotel.ClientID%>").style.display = "none";
                document.getElementById("<%=tdSales.ClientID%>").style.display = "none";
            } else if ("1" == val) {
                document.getElementById("<%=tdCity.ClientID%>").style.display = "none";
                document.getElementById("<%=tdTag.ClientID%>").style.display = "";
                document.getElementById("<%=tdHotel.ClientID%>").style.display = "none";
                document.getElementById("<%=tdSales.ClientID%>").style.display = "none";
            } else if ("2" == val) {
                document.getElementById("<%=tdCity.ClientID%>").style.display = "none";
                document.getElementById("<%=tdTag.ClientID%>").style.display = "none";
                document.getElementById("<%=tdHotel.ClientID%>").style.display = "";
                document.getElementById("<%=tdSales.ClientID%>").style.display = "none";
            } else if ("3" == val) {
                document.getElementById("<%=tdCity.ClientID%>").style.display = "none";
                document.getElementById("<%=tdTag.ClientID%>").style.display = "none";
                document.getElementById("<%=tdHotel.ClientID%>").style.display = "none";
                document.getElementById("<%=tdSales.ClientID%>").style.display = "";
            }

            ChkBtnStyle();
        }

        function ChkBtnStyle() {
            document.getElementById("<%=dvBtnOK.ClientID%>").style.display = "";
            document.getElementById("<%=dvBtnSty.ClientID%>").style.display = "none";

            document.getElementById("<%=ddpConrultType.ClientID%>").disabled = false;
            document.getElementById("wctCity").disabled = false;
            document.getElementById("wctTag").disabled = false;
            document.getElementById("wctHotel").disabled = false;
            document.getElementById("wctSales").disabled = false;
        }

        function SetUserHid() {
            document.getElementById("<%=hidSelecUserID.ClientID%>").value = document.getElementById("wctUser").value;
        }

        function PopupDetailArea(arg) {
            document.getElementById("<%=hidSelecID.ClientID%>").value = arg;
            document.getElementById("<%=btnRefresh.ClientID%>").click();
        }

        function PopupDelArea(arg) {
            document.getElementById("<%=hidConID.ClientID%>").value = arg;
            BtnLoadStyle();
            document.getElementById("<%=btnDel.ClientID%>").click();
        }

        function PopupaddDelArea(arg, type) {
            document.getElementById("<%=hidAddDelHTID.ClientID%>").value = arg;
            document.getElementById("<%=hidALDelTP.ClientID%>").value = type;
            BtnLoadStyle();
            document.getElementById("<%=btnAddDel.ClientID%>").click();
        }

        function SetAddAutoVal(arg) {
            if (document.getElementById("<%=ddpConrultType.ClientID%>").selectedIndex == 0) {
                document.getElementById("<%=hidAddAutoVal.ClientID%>").value = document.getElementById("wctCity").value;
            }
            else if (document.getElementById("<%=ddpConrultType.ClientID%>").selectedIndex == 1) {
                document.getElementById("<%=hidAddAutoVal.ClientID%>").value = document.getElementById("wctTag").value;
            }
            else if (document.getElementById("<%=ddpConrultType.ClientID%>").selectedIndex == 2) {
                document.getElementById("<%=hidAddAutoVal.ClientID%>").value = document.getElementById("wctHotel").value;
            }
            else if (document.getElementById("<%=ddpConrultType.ClientID%>").selectedIndex == 3) {
                document.getElementById("<%=hidAddAutoVal.ClientID%>").value = document.getElementById("wctSales").value;
            }
        }


        function divClick(Cval, EXD, hotels, obj) {
            document.getElementById("<%=HidDivName.ClientID%>").value = obj;
            document.getElementById("<%=DivClickValue.ClientID%>").value = Cval;
            document.getElementById("<%=CompletedConsultHotels.ClientID%>").value = EXD;
            document.getElementById("<%=SumConsultHotels.ClientID%>").value = hotels;
            document.getElementById("<%=Button1.ClientID%>").click();
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
            var str = document.getElementById("<%=DivClickValue.ClientID%>").value;
            window.open('<%=ResolveClientUrl("~/WebUI/Hotel/HotelConsultingRoomTable.aspx")%>?salesID=' + str + '&time=' + time, '_blank');
        }
    </script>
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
        .graph
        {
            position: relative; /* IE is dumb */
            width: 100%;
            border: 1px solid #339966;
            padding: 0px;
            background: #E9E9E9;
        }
        .graph .bar
        {
            display: block;
            position: relative;
            background: #339966;
            text-align: center;
            color: #333;
            height: 1.5em;
            line-height: 1.5em;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin: 5px 14px 5px 14px;">
        <div id="tabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#tabs-1">每日工作情况 </a></li>
                <li><a href="#right">房控任务分配 </a></li>
            </ul>
            <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">房控工作管理</li>
                        <li>选择日期：<input id="txtStartDate" class="Wdate" type="text" style="width: 150px"
                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_EndDate\')||\'2020-10-01\'}'})"
                            runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnBalSearch" runat="server" CssClass="btn primary" Text="查询" OnClientClick="BtnBalLoadStyle();"
                                OnClick="btnBalSearch_Click" />
                        </li>
                    </ul>
                </div>
                <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                    <ul>
                        <li class="title">当日房控员工</li>
                        <li>
                            <%-- <div style="margin: 5px 5px 5px 5px; float: left;">
                                <table style="background-color: #E9E9E9">
                                    <tr>
                                        <td>
                                            <img src="../../Styles/images/peopleIco.png" style="width: 25px; height: 28px;" />
                                        </td>
                                        <td>
                                            Youlanda.Hang
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="border-top: 1px solid #D5D5D5; text-align: center;">
                                            95/120
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <div id="ToDayConsultPeople" runat="server">
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="frame01" style="margin-top: 5px; margin-left: 5px;">
                    <ul>
                        <li class="title">当日房控询房情况</li>
                        <li>
                            <div id="MainDiv" style="display: none;" runat="server">
                                <div>
                                    <div style="float: left;">
                                        所有酒店情况：</div>
                                    <div id="DivProgressBar" runat="server" class="graph" style="width: 850px; float: left;"></div>
                                    <div style="float: left; margin-left: 20px;">
                                        <input type="button" id="btnSearchDetails" runat="server" class="btn primary" onclick="PopupHotelArea()"
                                            value="查看明细" style="display: none;" />
                                    </div>
                                    <br />
                                    <br />
                                    <div>
                                        <div style="float: left; margin-left: 25px;">
                                            分布细则：</div>
                                        <div id="DIVDistributionRules" runat="server" style="float: left; width: 80%;">
                                            <%--<table style="width:90%;border-collapse: collapse;" border="1" cellpadding="0" cellspacing="0">
                                        <tr style="border: 1px solid #666666;line-height:40px;">
                                            <td style="width:150px;text-align:center;border: 1px solid #666666;">
                                                上海-特价
                                            </td>
                                            <td style="border: 1px solid #666666;">
                                                <div class="graph" style="width: 97%;margin-left:10px;margin-right:10px;">
                                                    <strong class="bar" style="width: 20%;"><span style="padding-left: 230px;">172/236</span></strong></div>
                                            </td>
                                        </tr>
                                       <tr style="border: 1px solid #666666;line-height:40px;">
                                            <td style="width:150px;text-align:center;border: 1px solid #666666;">
                                                上海-特价
                                            </td>
                                            <td style="border: 1px solid #666666;">
                                                <div class="graph" style="width: 97%;margin-left:10px;margin-right:10px;">
                                                    <strong class="bar" style="width: 20%;"><span style="padding-left: 230px;">172/236</span></strong></div>
                                            </td>
                                        </tr>
                                    </table>--%>
                                        </div>
                                    </div>
                                </div>
                        </li>
                    </ul>
                </div>
                <div id="Div3" class="pcbackground" style="display: none;">
                </div>
                <div id="Div4" class="pcprogressBar" style="display: none;">
                    数据加载中，请稍等...</div>
            </div>
            <div id="right" style="border: 1px solid #D5D5D5;">
                <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table width="100%" id="tdSearch" runat="server">
                            <tr>
                                <td>
                                    <div class="frame01">
                                        <ul>
                                            <li class="title">询房用户管理</li>
                                            <li>
                                                <table>
                                                    <tr>
                                                        <td align="left">
                                                            选择用户：
                                                        </td>
                                                        <td align="left">
                                                            <uc1:WebAutoComplete ID="wctUser" CTLID="wctUser" runat="server" AutoType="user"
                                                                AutoParent="ConsultRoomUserPage.aspx?Type=user" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle();SetUserHid()"
                                                                        OnClick="btnSearch_Click" Text="搜索" />
                                                                    <div style="display: none">
                                                                        <asp:Button ID="btnReLoad" runat="server" CssClass="btn primary" OnClick="btnReLoad_Click"
                                                                            Text="搜索" /></div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                                                <ContentTemplate>
                                                                    <div style="margin-left: 10px;">
                                                                        <div id="messageContent" runat="server" style="color: red; width: 400px;">
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td align="right" style="width: 57%; display: none;">
                                                            <input id="btnAddModel" type="button" value="添加条件" style="width: 80px; height: 25px;"
                                                                onclick="AddModel('1')" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table width="100%" id="tbAll" runat="server">
                            <tr>
                                <td style="width: 40%" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <div class="frame01">
                                                <ul>
                                                    <li class="title">
                                                        <div>
                                                            <div style="float: left; text-align: left">
                                                                已绑条件</div>
                                                            <div style="float: right; text-align: right; margin-right: 10px">
                                                                酒店总数：<asp:Label ID="lbUserHotelNum" runat="server" Text="" /></div>
                                                        </div>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="left">
                                                                </td>
                                                                <td style="width: 50%" align="right">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="frame02" style="height: 240px; overflow: auto" id="dvroomMainGridList"
                                                runat="server">
                                                <asp:GridView ID="gridViewCSReviewUserList" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                                                    DataKeyNames="ID" CssClass="GView_BodyCSS">
                                                    <%--onrowdeleting="gridViewCSReviewUserList_RowDeleting"--%>
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CONSULTTYPE" HeaderText="类型">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="内容">
                                                            <ItemTemplate>
                                                                <a href="#" id="afPopupArea" onclick="PopupDetailArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>')">
                                                                    <font color="blue">
                                                                        <%# DataBinder.Eval(Container.DataItem, "CONSULTVAL")%></font></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="HTSUM" HeaderText="酒店数量">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <%--<asp:CommandField ShowDeleteButton="true" DeleteText="删除" HeaderText="编辑">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>--%>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                                                            <ItemTemplate>
                                                                <a href="#" id="afPopupArea" onclick="PopupDelArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>')">
                                                                    <font color="blue">删除</font></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <RowStyle CssClass="GView_ItemCSS" />
                                                    <HeaderStyle CssClass="GView_HeaderCSS" />
                                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                </asp:GridView>
                                            </div>
                                            <div style="display: none;">
                                                <asp:Button ID="btnRefresh" runat="server" CssClass="btn primary" OnClick="btnRefush_Click"
                                                    Text="btnRefresh" />
                                                <asp:Button ID="btnDel" runat="server" CssClass="btn primary" OnClick="btnDel_Click"
                                                    Text="btnDel" />
                                                <asp:Button ID="btnAddDel" runat="server" CssClass="btn primary" OnClick="btnAddDel_Click"
                                                    Text="btnAddDel" />
                                            </div>
                                            <div class="frame02" style="height: 210px; overflow: auto" id="dvroomDetailGridList"
                                                runat="server">
                                                <asp:GridView ID="roomDetailGridView" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                                                    CssClass="GView_BodyCSS">
                                                    <Columns>
                                                        <asp:BoundField DataField="CONSULTVAL" HeaderText="酒店名称">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <RowStyle CssClass="GView_ItemCSS" />
                                                    <HeaderStyle CssClass="GView_HeaderCSS" />
                                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                </asp:GridView>
                                            </div>
                                            <div id="background" class="pcbackground" style="display: none;">
                                            </div>
                                            <div id="progressBar" class="pcprogressBar" style="display: none;">
                                                数据加载中，请稍等...</div>
                                            <asp:HiddenField ID="hidUserID" runat="server" />
                                            <asp:HiddenField ID="hidSelecUserID" runat="server" />
                                            <asp:HiddenField ID="hidSelecID" runat="server" />
                                            <asp:HiddenField ID="hidConID" runat="server" />
                                            <asp:HiddenField ID="hidAddAutoVal" runat="server" />
                                            <asp:HiddenField ID="hidAddDelHTID" runat="server" />
                                            <asp:HiddenField ID="hidALDelHT" runat="server" />
                                            <asp:HiddenField ID="hidALDelTP" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 60%" valign="top">
                                    <div class="frame01" id="dvAddRule" runat="server" style="height: 100px">
                                        <%--style="display:none"  --%>
                                        <ul>
                                            <li class="title">添加条件</li>
                                            <li>
                                                <table>
                                                    <tr>
                                                        <td align="left">
                                                            类型：
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddpConrultType" CssClass="noborder_inactive" runat="server"
                                                                Width="150px" onchange="checkConrultType(this.value);" />
                                                        </td>
                                                        <td align="left" id="tdCity" runat="server" style="display: none">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        城市：
                                                                    </td>
                                                                    <td>
                                                                        <uc1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" AutoType="city"
                                                                            AutoParent="ConsultRoomUserPage.aspx?Type=city" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" id="tdTag" runat="server" style="display: none">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        商圈：
                                                                    </td>
                                                                    <td>
                                                                        <uc1:WebAutoComplete ID="wctTag" runat="server" CTLID="wctTag" AutoType="tag" AutoParent="ConsultRoomUserPage.aspx?Type=tag" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" id="tdHotel" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        酒店：
                                                                    </td>
                                                                    <td>
                                                                        <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel"
                                                                            AutoParent="ConsultRoomUserPage.aspx?Type=hotel" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" id="tdSales" runat="server" style="display: none">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        销售：
                                                                    </td>
                                                                    <td>
                                                                        <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server" AutoType="sales" AutoParent="ConsultRoomUserPage.aspx?Type=sales" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        
                                                        <td>
                                                            <div id="dvBtnOK" runat="server">
                                                                <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnOK" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle();SetAddAutoVal();"
                                                                            OnClick="btnOK_Click" Text="查询" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div id="dvBtnSty" runat="server" style="display: none;">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()"
                                                                                        OnClick="btnAdd_Click" Text="绑定" />
                                                                                    <asp:Button ID="btnCanel" runat="server" CssClass="btn primary" OnClientClick="ChkBtnStyle()"
                                                                                        OnClick="btnCanel_Click" Text="取消" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </li>
                                        </ul>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <div class="frame01" id="dvYuji" runat="server">
                                                <ul>
                                                    <li class="title">
                                                        <div>
                                                            <div style="float: left; text-align: left">
                                                            </div>
                                                            <div style="float: right; text-align: right; margin-right: 10px">
                                                                预计酒店总数：<asp:Label ID="lbAddTotal" runat="server" Text="" /></div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="frame02" style="height: 350px; overflow: auto" id="dvRuleHotel" runat="server">
                                                <asp:GridView ID="addHotelView" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID"
                                                    OnRowDeleting="addHotelView_RowDeleting" CssClass="GView_BodyCSS">
                                                    <Columns>
                                                        <asp:BoundField DataField="HOTELNM" HeaderText="酒店ID-名称">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BANDNM" HeaderText="已绑定人员" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <%-- <asp:CommandField ShowDeleteButton="true" DeleteText="删除" HeaderText="编辑">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>--%>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                                                            <ItemTemplate>
                                                                <a href="#" id="addRoomPopupArea" onclick="PopupaddDelArea('<%# DataBinder.Eval(Container.DataItem, "HOTELID") %>','<%# DataBinder.Eval(Container.DataItem, "CTL")%>')">
                                                                    <font color="blue">
                                                                        <%# DataBinder.Eval(Container.DataItem, "CTL")%></font></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <RowStyle CssClass="GView_ItemCSS" />
                                                    <HeaderStyle CssClass="GView_HeaderCSS" />
                                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--</div>--%>
            </div>
        </div>
        <div id="Div1" class="pcbackground" style="display: none;">
        </div>
        <div id="Div2" class="pcprogressBar" style="display: none;">
            数据加载中，请稍等...</div>
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
    </div>
    <input type="button" id="Button1" runat="server" onclick="BtnBalLoadStyle();" onserverclick="Button1_Click" style="display: none;" />
    <asp:HiddenField ID="SumConsultHotels" runat="server" />
    <asp:HiddenField ID="CompletedConsultHotels" runat="server" />
    <asp:HiddenField ID="DivClickValue" runat="server" />
    <asp:HiddenField ID="HidDivName" runat="server" />
    <asp:HiddenField ID="EndDate" runat="server" />
</asp:Content>
