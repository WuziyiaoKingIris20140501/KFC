

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HotelBlackListManager.aspx.cs" Inherits="WebUI_Hotel_HotelBlackListManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <style type="text/css">
        .pcbackground
        {
            display: block;
            width: 100%;
            height: 100%;
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
            width: 800px;
            height: 280px;
            top: 25%;
            left: 23%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            border: solid 2px #ff8300;
            z-index: 1001;
            display: none;
            background-color: White;
        }
        .style1
        {
            width: 34px;
            text-align: right;
        }
    </style>
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript">
        function btnConfirm(obj) {
            if (confirm('该酒店已存在' + obj + '中，还要继续添加吗？')) {
                document.getElementById("<%=btnUpdateHotelBlack.ClientID%>").click();
            } else {
                invokeClose2();
            }
        }

        function btnSeachClick() {
            BtnLoadStyle();
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }

        //显示弹出的层
        function invokeOpen2() {
            document.getElementById('wctHotelDiv').value = '';
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
        }

        //显示弹出的层
        function invokeOpen3() {
            document.getElementById('wctHotelDiv').value = document.getElementById("<%=HidHotelValue.ClientID%>").value;
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
        }

        //隐藏弹出的层
        function invokeClose2() {
            document.getElementById("<%=HidHotelValue.ClientID%>").value = '';
            document.getElementById("<%=MessageContentDiv.ClientID%>").innerHTML = '';

            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
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

        function chkAddHotelBlack() {
            if (document.getElementById('wctHotelDiv').value == '') {
                alert('请选择酒店!');
                document.getElementById('wctHotelDiv').focus();
                return false;
            }
            var dropDownList = document.getElementById("<%=DropChannelDiv.ClientID %>"); //获取DropDownList控件
            var dropDownListValue = dropDownList.options[dropDownList.selectedIndex].value; //获取选择项的值
            if (dropDownListValue == '') {
                alert('请选择分销渠道!');
                dropDownList.focus();
                return false;
            }
            BtnLoadStyle();
            return true;
        }

        function SetControlValue() {
            document.getElementById("<%=HidHotelValue.ClientID%>").value = document.getElementById("wctHotel").value;
        }

        function SetControlValue2() {
            document.getElementById("wctHotel").value = document.getElementById("<%=HidHotelValue.ClientID%>").value;
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">分销渠道库存过滤管理</li>
                    <li>
                        <table width="100%">
                            <tr>
                                <td style="width: 70px; text-align: right;">
                                    分销渠道：
                                </td>
                                <td style="width: 100px;">
                                    <asp:DropDownList ID="DropChannel" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 70px; text-align: right;">
                                    酒店：
                                </td>
                                <td style="width: 370px;">
                                    <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel"
                                        AutoParent="HotelBlackListManager.aspx?Type=hotel" />
                                </td>
                                <td style="width: 70px;">
                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();SetControlValue();"
                                                OnClick="btnSearch_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: auto; float: right; margin-right: 30px;">
                                    <input type="button" id="btnAdd"  class="btn primary" runat="server" onclick="invokeOpen2()" value="添加" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="frame01" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">分销渠道库存过滤名单</li>
                    <li style="padding: 0px;">
                        <asp:GridView ID="gridHotelList" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Width="100%" DataKeyNames="id,is_black" OnRowDataBound="gridHotelList_RowDataBound" PageSize="20" 
                            CssClass="GView_BodyCSS" OnRowDeleting="gridHotelList_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="hotel_id" HeaderText="酒店ID" />
                                <asp:BoundField DataField="prop_name_zh" HeaderText="酒店名称" />
                                <asp:BoundField DataField="source" HeaderText="分销渠道" />
                                <asp:BoundField DataField="is_black" HeaderText="过滤类型" />
                                <asp:BoundField DataField="create_time" HeaderText="操作时间" />
                                <asp:BoundField DataField="create_user" HeaderText="操作人" />
                                <asp:TemplateField ShowHeader="true" HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="删除" OnClientClick='<%#  "if (!confirm(\"你确定要删除" + Eval("prop_name_zh").ToString() + "吗?\")){ return false;}else{ BtnLoadStyle(); return true;}"%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
                        </asp:GridView>
                        <div style="margin-left: 10px;">
                            <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb" ID="AspNetPager1"
                                runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                                CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页"
                                ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%"
                                ShowPageIndexBox="always" AlwaysShow="true" Width="100%" LayoutType="Table" OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </li>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2">
        <div class="frame01">
            <ul>
                <li class="title">添加过滤</li>
            </ul>
            <ul>
                <li style="padding-left: 0px;">
                    <table style="width: 100%;">
                        <tr style="height: 35px; vertical-align: middle;">
                            <td class="style1" style="border-bottom: 1px solid #DCDCDC;">
                                酒店:
                            </td>
                            <td style="width: 190px; border-bottom: 1px solid #DCDCDC;">
                                <uc1:WebAutoComplete ID="wctHotelDiv" CTLID="wctHotelDiv" runat="server" AutoType="hotel"
                                    AutoParent="HotelBlackListManager.aspx?Type=hotel" />
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border-bottom: 1px solid #DCDCDC;">
                                渠道:
                            </td>
                            <td style="border-bottom: 1px solid #DCDCDC;">
                                <asp:DropDownList ID="DropChannelDiv" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border-bottom: 1px solid #DCDCDC;">
                                过滤类型:
                            </td>
                            <td style="border-bottom: 1px solid #DCDCDC;" colspan="3">
                                <asp:Label ID="lblDivPrice" runat="server" Text="黑名单过滤"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 210px; border-bottom: 1px solid #DCDCDC;">
                            <td colspan="4" align="center">
                                <asp:Button ID="btnAddHotelBlack" runat="server"  CssClass="btn primary" Text="确认" OnClientClick="return chkAddHotelBlack();"
                                    OnClick="btnAddHotelBlack_Click" />
                                <input type="button" value="取消" class="btn" onclick="invokeClose2()" />
                            </td>
                        </tr>
                    </table>
                </li>
                <li>
                    <div id="MessageContentDiv" runat="server" style="color: red;">
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <asp:HiddenField ID="HidHotelValue" runat="server" />
    <input type="button" id="btnUpdateHotelBlack" runat="server" onserverclick="btnUpdateHotelBlack_Click" style="display: none;" />
</asp:Content>
