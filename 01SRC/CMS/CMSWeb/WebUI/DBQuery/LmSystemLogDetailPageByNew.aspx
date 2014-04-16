<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LmSystemLogDetailPageByNew.aspx.cs" Inherits="WebUI_DBQuery_LmSystemOrderDetailPage" %>

<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
            left: 18%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            border: solid 2px #ff8300;
            z-index: 1001;
            display: none;
            background-color: White;
            top: 15%;
            left: 25%;
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
        //显示弹出的层
        function invokeOpenDiv() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeCloseDiv() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function Validate() {
            var RefundTime = document.getElementById("<%=lblRefundTime.ClientID%>").value;
            if (RefundTime == "") {
                BtnCompleteStyle();
                document.getElementById("RefundTimeError").innerHTML = "请选择退款时间!";
                return false;
            }
            var RefundAccount = document.getElementById("<%=lblRefundAccount.ClientID%>").value;
            if (RefundAccount == "") {
                BtnCompleteStyle();
                document.getElementById("lblRefundAccountError").innerHTML = "请输入退款账号!";
                return false;
            }
            var RefundEntryNumber = document.getElementById("<%=lblRefundEntryNumber.ClientID%>").value;
            if (RefundEntryNumber == "") {
                BtnCompleteStyle();
                document.getElementById("lblRefundEntryNumberError").innerHTML = "请输入退款流水号!";
                return false;
            }
            return true;
        }

        function OpenIssuePage() {
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
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=0&RID=' + document.getElementById("<%=lmlfog_order_num.ClientID%>").innerText + "&time=" + time, null, fulls);
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
            window.open('<%=ResolveClientUrl("~/WebUI/Hotel/HotelInfoManager.aspx")%>?hid=' + document.getElementById("<%=hidHotelID.ClientID%>").value + "&time=" + time, null, fulls);
        }

        function showDiv(obj) {
            if (obj == "tabs1") {
                if (document.getElementById("tabs1").style.display == "none") {
                    document.getElementById("tabs1").style.display = "";
                } else {
                    document.getElementById("tabs1").style.display = "none";
                }
            } else {
                if (document.getElementById("tabs2").style.display == "none") {
                    document.getElementById("tabs2").style.display = "";
                } else {
                    document.getElementById("tabs2").style.display = "none";
                }
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
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <div class="frame01">
            <ul>
                <li class="title">订单详情</li>
                <li>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 15%">
                                订单ID：
                            </td>
                            <td align="left" style="width: 35%">
                                <asp:Label ID="lmlfog_order_num" runat="server" />
                                <%--                                &nbsp;<input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" />--%>
                            </td>
                            <td align="right" style="width: 15%">
                                创建时间：
                            </td>
                            <td align="left" style="width: 35%">
                                <asp:Label ID="lmlcreate_time" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                酒店：
                            </td>
                            <td align="left">
                                <%--<asp:Label ID="lmlhotel_id" runat="server" />-<asp:Label ID="lmlhotel_name" runat="server" />--%>
                                <%--<a id="A1" href="#" runat="server" onclick="PopupHotelArea()"></a>--%>
                                <a id="A1" href="#" runat="server" onclick="PopupHotelArea()">
                                    <asp:Label ID="lmlhotel_id" runat="server" />-<asp:Label ID="lmlhotel_name" runat="server" /></a>
                                &nbsp;<asp:Label ID="lmlHotelSales" runat="server" Text="(销售人员:XX)" />
                            </td>
                            <td align="right">
                                城市：
                            </td>
                            <td align="left">
                                <asp:Label ID="lmlcity_id" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                房型：
                            </td>
                            <td>
                                <asp:Label ID="lmlroom_type_name" runat="server" />
                            </td>
                            <td align="right">
                                预定间数：
                            </td>
                            <td>
                                <asp:Label ID="lmlbook_room_num" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                价格代码：
                            </td>
                            <td>
                                <asp:Label ID="lmlprice_code" runat="server" />
                            </td>
                            <td align="right">
                                预定价格：
                            </td>
                            <td>
                                <asp:Label ID="lmlbook_total_price" runat="server" Text="预订总价格" />&nbsp;<asp:Label
                                    ID="lmlbook_price" runat="server" Text="单价" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span runat="server" id="spanPayStatusOrMethod" style="display: none;">支付状态：</span>
                            </td>
                            <td >
                                <span runat="server" id="lblPayStatusOrMethod" style="display: none;"></span>
                            </td>
                            <td align="right">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="line-height: 20px;">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                用户ID：
                            </td>
                            <td align="left">
                                <asp:Label ID="lmluser_id" runat="server" />
                            </td>
                            <td align="right">
                                登录手机号：
                            </td>
                            <td align="left">
                                <asp:Label ID="lmllogin_mobile" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                入住日期：
                            </td>
                            <td>
                                <asp:Label ID="lmlin_date" runat="server" />
                            </td>
                            <td align="right">
                                离店日期：
                            </td>
                            <td>
                                <asp:Label ID="lmlout_date" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                入住人：
                            </td>
                            <td>
                                <asp:Label ID="lmlguest_names" runat="server" />
                            </td>
                            <td align="right">
                                联系人方式：
                            </td>
                            <td>
                                <asp:Label ID="lmlcontact_tel" runat="server" />
                            </td>
                        </tr>
                        <tr style="line-height: 20px;">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                订单渠道：
                            </td>
                            <td align="left">
                                <asp:Label ID="lblOrder_channel" runat="server" />
                            </td>
                            <td align="right">
                                订单平台：
                            </td>
                            <td>
                                <asp:Label ID="lmlapp_platform" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                担保：
                            </td>
                            <td>
                                <asp:Label ID="lmisgua_name" runat="server" />
                            </td>
                            <td align="right">
                                券金额:
                            </td>
                            <td>
                                <asp:Label ID="lmlticket_amount" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                早餐：
                            </td>
                            <td align="left">
                                <asp:Label ID="lmlbreakfast_num" runat="server" />
                            </td>
                            <td align="right">
                                wifi：
                            </td>
                            <td align="left">
                                <asp:Label ID="lmlis_network" runat="server" />
                            </td>
                        </tr>
                    </table>
                </li>
                <li>
                    <div id="detailMessageContent" runat="server" style="color: red">
                    </div>
                </li>
                <li class="title" style="margin-top: 20px;">订单流程</li>
                <li>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 15%">
                                目前状态：
                            </td>
                            <td align="left" style="width: 35%">
                                <asp:Label ID="lblBook_status_other" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblRoomNumber" runat="server" /><a href="#" onclick="showDiv('tabs1')">查看详情</a>
                            </td>
                            <td align="right" style="width: 15%">
                                销售计划：
                            </td>
                            <td align="left" style="width: 35%">
                                <a href="#" onclick="showDiv('tabs2')">历史操作明细</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div id="tabs1" style="border: 1px solid #D5D5D5; display: none; margin-bottom: 10px;">
                        <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" AutoGenerateColumns="False"
                            BackColor="White" AllowPaging="True" CellPadding="4" CellSpacing="1" Width="100%"
                            EmptyDataText="没有数据" DataKeyNames="ID" OnPageIndexChanging="gridViewCSSystemLogDetail_PageIndexChanging"
                            PageSize="20" CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:BoundField DataField="OPERATOR" HeaderText="操作人" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTTIME" HeaderText="操作日期" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MEMO" HeaderText="操作内容" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARK" HeaderText="操作备注" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                        </asp:GridView>
                    </div>
                    <div id="tabs2" style="border: 1px solid #D5D5D5; display: none; margin-bottom: 10px;">
                        <div id="dvHisPlanInfoList" runat="server" style="color: Red;">
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <div id="bgDiv2" class="bgDiv2">
        </div>
        <div id="popupDiv2" class="popupDiv2">
            <div class="frame01">
                <ul>
                    <li class="title">退款操作</li>
                </ul>
                <ul>
                    <li style="padding-left: 0px;">
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr style="height: 35px; vertical-align: middle;">
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款方式:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;">
                                    <asp:Label runat="server" ID="lblRefundMethod"></asp:Label>
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款手机号:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;">
                                    <asp:Label runat="server" ID="lblRefundMobile"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 35px; vertical-align: middle;">
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款人:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;">
                                    <asp:Label runat="server" ID="lblRefundName"></asp:Label>
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款时间:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;">
                                    <input id="lblRefundTime" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                        runat="server" /><span style="color: Red" id="RefundTimeError"></span>
                                </td>
                            </tr>
                            <tr style="height: 35px; vertical-align: middle;">
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款金额:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;" colspan="3">
                                    <asp:Label runat="server" ID="lblRefundAmount"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 35px; vertical-align: middle;">
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款账号:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;">
                                    <asp:TextBox runat="server" ID="lblRefundAccount" Width="230"></asp:TextBox><span
                                        style="color: Red" id="lblRefundAccountError"></span>
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    流水号:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;">
                                    <asp:TextBox runat="server" ID="lblRefundEntryNumber" Width="230"></asp:TextBox><span
                                        style="color: Red" id="RefundEntryNumberError"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: center;">
                                    退款备注:
                                </td>
                                <td style="border-bottom: 1px solid #DCDCDC; text-align: left;" colspan="3">
                                    <textarea runat="server" id="txtRefundRemark" cols="70" rows="3"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <%--<input type="button" id="btnDivRenewPlan" runat="server" class="btn primary" value="确认" />--%>
                                            <asp:Button runat="server" ID="btnDivRenewPlan" CssClass="btn primary" Text="确认"
                                                OnClientClick="BtnLoadStyle();return Validate();" OnClick="btnDivRenewPlan_Click" />
                                            <input type="button" value="取消" class="btn" onclick="invokeCloseDiv()" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <asp:HiddenField ID="hidEventLMID" runat="server" />
    <asp:HiddenField ID="hidHotelID" runat="server" />
    <asp:HiddenField ID="HidFogOrderNum" runat="server" />
        <asp:HiddenField ID="HidPayMethod" runat="server" />
</asp:Content>
