<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HotelTagInfoSearchPage.aspx.cs" Inherits="WebUI_GeneralSetting_HotelTagInfoSearchPage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
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
            height: 620px;
            top: 15%;
            left: 28%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            border: solid 2px #ff8300;
            z-index: 1001;
            display: none;
            background-color: White;
            top: 15%;
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
        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        function SetControlValueByBtn() {
            //将选择的城市  记录到隐藏域中
            document.getElementById("<%=HidCityName.ClientID%>").value = document.getElementById("wctCity").value;
        }
        //显示弹出的层
        function invokeOpen() {
            document.getElementById("wctDivCity").value = "";
            document.getElementById("<%=txtDviTagName.ClientID%>").value = "";
            document.getElementById("<%=txtDivLongitude.ClientID%>").value = "";
            document.getElementById("<%=txtDivLatitude.ClientID%>").value = "";

            document.getElementById("<%=ddlDivTypeId.ClientID %>").value = "";
            document.getElementById("<%=ddlDivStatus.ClientID %>").value = "";

            document.getElementById("<%=txtDivPinyinLong.ClientID%>").value = "";
            document.getElementById("<%=txtDivPinyinShort.ClientID%>").value = "";

            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeClose() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function PopupArea(id, tagName, cityID, cityName, type, status, Longitude, Latitude, pinyinLong, pinyingShort) {
            invokeOpen();

            document.getElementById("<%=hidStatus.ClientID%>").value = "Edit";
            document.getElementById("<%=hidSelectID.ClientID%>").value = id;
            document.getElementById("wctDivCity").value = "[" + cityID + "]" + cityName;
            document.getElementById("<%=txtDviTagName.ClientID%>").value = tagName;
            document.getElementById("<%=txtDivLongitude.ClientID%>").value = Longitude;
            document.getElementById("<%=txtDivLatitude.ClientID%>").value = Latitude;

            document.getElementById("<%=ddlDivTypeId.ClientID %>").value = type;
            document.getElementById("<%=ddlDivStatus.ClientID %>").value = status;

            document.getElementById("<%=txtDivPinyinLong.ClientID%>").value = pinyinLong;
            document.getElementById("<%=txtDivPinyinShort.ClientID%>").value = pinyingShort;
        }

        function SetControlValue() {
            document.getElementById("<%=messageContent.ClientID%>").innerHTML = "";
            var city = document.getElementById("wctDivCity").value; //城市
            if (city == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请选择城市...";
                return false;
            }
            if (city != "") {
                alert(city);
                alert(city.indexOf("["));
                alert(city.indexOf("]"));
                if (city.indexOf("[") < 0 || city.indexOf("]") < 0) {
                    document.getElementById("<%=messageContent.ClientID%>").innerHTML = "选择城市不合法，请修改...";
                    return false;
                }
            }
            var tagName = document.getElementById("<%=txtDviTagName.ClientID%>").value; //商圈名称
            if (tagName == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请输入商圈名称...";
                return false;
            }
            var Longitude = document.getElementById("<%=txtDivLongitude.ClientID%>").value; //经度
            if (Longitude == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请输入经度...";
                return false;
            }
            var Latitude = document.getElementById("<%=txtDivLatitude.ClientID%>").value; //纬度
            if (Latitude == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请输入纬度...";
                return false;
            }

            var selectType = document.getElementById("<%=ddlDivTypeId.ClientID %>");
            var selectTypeID = selectType.options[selectType.selectedIndex].value; //商圈类型
            if (selectTypeID == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请选择商圈类型...";
                return false;
            }

            var selectStatus = document.getElementById("<%=ddlDivStatus.ClientID %>");
            var selectStatusID = selectStatus.options[selectStatus.selectedIndex].value; //商圈状态
            if (selectStatusID == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请选择商圈状态...";
                return false;
            }

            var Longitude = document.getElementById("<%=txtDivPinyinLong.ClientID%>").value; //商圈全拼
            if (Longitude == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请输入商圈全拼...";
                return false;
            }
            var Latitude = document.getElementById("<%=txtDivPinyinShort.ClientID%>").value; //商圈简拼
            if (Latitude == "") {
                document.getElementById("<%=messageContent.ClientID%>").innerHTML = "请输入商圈简拼...";
                return false;
            }

            //将选择的城市  记录到隐藏域中
            document.getElementById("<%=hidSelectCity.ClientID%>").value = document.getElementById("wctDivCity").value;

            return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">商圈管理</li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        城市:
                                    </td>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city"
                                            EnableViewState="false" AutoParent="HotelTagInfoSearchPage.aspx?Type=city" />
                                    </td>
                                    <td>
                                        商圈名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();SetControlValueByBtn();"
                                            OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="新增" OnClientClick="invokeOpen()" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="frame02">
                    <asp:GridView ID="gridViewTagInfoList" runat="server" AutoGenerateColumns="False"
                        BackColor="White" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                        AllowPaging="True" PageSize="50" DataKeyNames="ID" OnRowDataBound="gridViewTagInfoList_RowDataBound"
                        CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="Tag_Name" HeaderText="商圈名称">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CITY_NAME" HeaderText="城市名称" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LONGITUDE" HeaderText="经度" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LATITUDE" HeaderText="纬度" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PINYIN_LONG" HeaderText="全拼" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PINYIN_SHORT" HeaderText="简拼" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                                <ItemTemplate>
                                    <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>','<%# DataBinder.Eval(Container.DataItem, "Tag_Name") %>','<%# DataBinder.Eval(Container.DataItem, "CITY_ID") %>','<%# DataBinder.Eval(Container.DataItem, "CITY_NAME") %>','<%# DataBinder.Eval(Container.DataItem, "TYPE_ID") %>','<%# DataBinder.Eval(Container.DataItem, "STATUS") %>',
                                    '<%# DataBinder.Eval(Container.DataItem, "LONGITUDE") %>','<%# DataBinder.Eval(Container.DataItem, "LATITUDE") %>','<%# DataBinder.Eval(Container.DataItem, "PINYIN_LONG") %>','<%# DataBinder.Eval(Container.DataItem, "PINYIN_SHORT") %>')">
                                        编辑</a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                    <div style="margin-left: 10px;">
                        <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb" ID="AspNetPager1"
                            runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                            CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页"
                            ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%"
                            ShowPageIndexBox="always" AlwaysShow="true" Width="100%" LayoutType="Table" OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2">
        <div class="frame01">
            <ul>
                <li class="title">酒店计划</li>
            </ul>
            <ul>
                <li style="padding-left: 0px;">
                    <table>
                        <tr>
                            <td>
                                城市：
                            </td>
                            <td>
                                <uc1:WebAutoComplete ID="wctDivCity" CTLID="wctDivCity" runat="server" AutoType="city"
                                    EnableViewState="false" AutoParent="HotelTagInfoSearchPage.aspx?Type=city" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商圈：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDviTagName" runat="server" Width="350"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                经度：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDivLongitude" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                纬度：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDivLatitude" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商圈类型：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDivTypeId" runat="server">
                                    <asp:ListItem Value="">请选择...</asp:ListItem>
                                    <asp:ListItem Value="1">商圈</asp:ListItem>
                                    <asp:ListItem Value="2">行政区</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                商圈状态：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDivStatus" runat="server">
                                    <asp:ListItem Value="">请选择...</asp:ListItem>
                                    <asp:ListItem Value="1">上线</asp:ListItem>
                                    <asp:ListItem Value="0">下线</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商圈全拼：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDivPinyinLong" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                商圈简拼：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDivPinyinShort" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td colspan="4">
                                <div style="margin-left: 10px;">
                                    <div id="messageContent" runat="server" style="color: red; width: 400px;">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnDivAdd" runat="server" CssClass="btn primary" Text="确定" OnClientClick="return SetControlValue()"
                                    OnClick="btnDivAdd_Click" />
                                <input type="button" value="取消" class="btn" onclick="invokeClose()" />
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
    <asp:HiddenField ID="HidCityName" runat="server" />
    <asp:HiddenField ID="hidSelectCity" runat="server" />
    <asp:HiddenField ID="hidSelectID" runat="server" />
    <asp:HiddenField ID="hidStatus" runat="server" />
</asp:Content>
