<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OrderOperateStatus.aspx.cs" Inherits="WebUI_Order_OrderOperateStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
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
    </style>
    <script type="text/javascript">
        function hideButton() {
            document.getElementById("<%=btnOk.ClientID%>").disabled = "disabled";
            document.getElementById("<%=dropStatus.ClientID%>").disabled = "disabled";
            document.getElementById("<%=ddlReturnTicket.ClientID%>").disabled = "disabled";
            document.getElementById("<%=txtRemark.ClientID%>").disabled = "disabled";
        }

        function lowButton() {
            document.getElementById("<%=btnOk.ClientID%>").disabled = "";
            document.getElementById("<%=dropStatus.ClientID%>").disabled = "";
            document.getElementById("<%=ddlReturnTicket.ClientID%>").disabled = "";
            document.getElementById("<%=txtRemark.ClientID%>").disabled = "";
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
    <div id="right">
        <div class="frame01">
            <ul>
                <li class="title">订单信息</li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div style="width: 99%; height: 99%; margin: 0,0,0,0">
                                <table style="width: 99%;">
                                    <tr style="width: 100%; height: 35px; vertical-align: middle;">
                                        <td style="border-bottom: 1px solid #DCDCDC; border-collapse: collapse;" colspan="2">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left" style="width: 250px;">
                                                        订单号：
                                                        <asp:TextBox ID="txtFogOrderNum" runat="server" Width="150px" MaxLength="100" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <div id="background" class="pcbackground" style="display: none;">
                                                                </div>
                                                                <div id="progressBar" class="pcprogressBar" style="display: none;">
                                                                    数据加载中，请稍等...</div>
                                                                <asp:Button ID="btnlock" runat="server" CssClass="btn primary" Text="查询" OnClientClick="BtnLoadStyle();return checkValid();"
                                                                    OnClick="btnlock_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                            </table>
                                        </td>
                                        <td style="border-bottom: 1px solid #DCDCDC; border-collapse: collapse;" colspan="2">
                                        </td>
                                    </tr>
                                    <tr style="width: 100%; height: 35px; vertical-align: middle;">
                                        <td style="text-align: right; width: 8%;">
                                            预定酒店：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtHotelName" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 8%;">
                                            价格代码：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtPriceCode" runat="server" Text=""></asp:Label>
                                            <span runat="server" id="spanPriceCode" style="display: none;"></span>
                                        </td>
                                    </tr>
                                    <tr style="height: 35px; vertical-align: middle;">
                                        <td style="text-align: right; width: 8%;">
                                            预订人号码：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtLoginMobile" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 8%;">
                                            订单创建日期：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtCreateTime" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 35px; vertical-align: middle;">
                                        <td style="text-align: right; width: 8%;">
                                            入住人姓名：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtGuestNames" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 8%;">
                                            酒店供应商：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtVender" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 35px; vertical-align: middle;">
                                        <td style="text-align: right; width: 8%;">
                                            房型名称：
                                        </td>
                                        <td>
                                            <asp:Label ID="txtRoomName" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 8%;">
                                            订单总金额：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtBookTotalPrice" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 35px; vertical-align: middle;">
                                        <td style="text-align: right; width: 8%;">
                                            入住-离店：
                                        </td>
                                        <td>
                                            <asp:Label ID="txtINOrOutDate" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 8%;">
                                            返现券金额：
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:Label ID="txtPromotionAmount" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
        <div class="frame01">
            <ul>
                <li class="title">订单状态操作</li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr style="line-height: 35px;">
                                    <td style="width: 48%">
                                        <%--<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>--%>
                                        当前HVP状态：<asp:Label ID="lblHVPStatus" runat="server" Text=""></asp:Label>
                                        <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                    <td style="width: 48%">
                                        <%--<asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>--%>
                                        当前供应商状态：&nbsp;&nbsp;<asp:Label ID="lblVendorStatus" runat="server" Text=""></asp:Label>
                                        <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                                <tr style="line-height: 35px;">
                                    <td style="width: 48%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;修改状态：
                                        <select id="dropStatus" runat="server" onchange="OnchangeStatus()">
                                            <option value="">请选择</option>
                                            <option value="8">离店</option>
                                            <option value="5">No-Show</option>
                                            <option value="9">CC取消</option>
                                        </select>
                                    </td>
                                    <td style="width: 48%">
                                        是否反券：&nbsp;&nbsp;
                                        <select id="ddlReturnTicket" runat="server">
                                            <option value="">请选择</option>
                                            <option value="true">是</option>
                                            <option value="false">否</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr style="line-height: 35px;">
                                    <td style="width: 48%">
                                        <div style="display: none;" id="INRoomNum" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;入住房号：&nbsp;<asp:TextBox ID="txtINRoomNum" runat="server"
                                                Width="150px" MaxLength="100" />
                                        </div>
                                    </td>
                                </tr>
                                <tr style="line-height: 35px;">
                                    <td style="width: 48%">
                                        <div style="display: none;" id="AffirmNum" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;确认号：&nbsp;&nbsp;<asp:TextBox ID="txtAffirmNum"
                                                runat="server" Width="150px" MaxLength="100" />
                                        </div>
                                    </td>
                                </tr>
                                <tr style="line-height: 35px;">
                                    <td style="width: 48%">
                                        <div style="display: none;" id="NSRemark" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NS原因：&nbsp;&nbsp;<asp:DropDownList ID="ddpNoShow"
                                                CssClass="noborder_inactive" runat="server" Width="250px" />
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="line-height: 35px;">
                                    <td style="width: 48%">
                                        <div style="display: none;" id="CancleRemark" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;取消原因：&nbsp;&nbsp;<asp:DropDownList ID="ddpCanelReson"
                                                CssClass="noborder_inactive" runat="server" Width="250px" />
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;操作备注：
                                        <textarea id="txtRemark" runat="server" rows="3" cols="150"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnOk" runat="server" CssClass="btn primary" Text="确认修改" OnClientClick="BtnLoadStyle();return checkValidUpdate();"
                                            OnClick="btnOk_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
        <div class="frame01">
            <ul>
                <li class="title">订单状态历史</li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" AutoGenerateColumns="False"
                                BackColor="White" AllowPaging="True" CellPadding="4" CellSpacing="1" Width="100%"
                                EmptyDataText="没有数据" DataKeyNames="ID" OnRowDataBound="gridViewCSSystemLogDetail_RowDataBound"
                                PageSize="50" CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="MEMO" HeaderText="订单操作" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MSG" HeaderText="订单状态" ReadOnly="True" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OPERATOR" HeaderText="操作人" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EVENTTIME" HeaderText="操作日期" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REMARK" HeaderText="操作备注" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="operateresult" HeaderText="操作结果" ReadOnly="True">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
    </div>
    <script type="text/javascript">
        function checkValid() {
            if (document.getElementById("<%=txtFogOrderNum.ClientID%>").value == "") {
                alert("请输入订单号！");
                document.getElementById("<%=txtFogOrderNum.ClientID%>").focus();
                BtnCompleteStyle();
                return false;
            }
            return true;
        }

        function OnchangeStatus() {
            var ddl = document.getElementById("<%=dropStatus.ClientID%>");
            var index = ddl.selectedIndex;
            var picTypeValue = ddl.options[index].value;
            if (picTypeValue == "18") {
                document.getElementById("<%=INRoomNum.ClientID%>").style.display = "none";
                document.getElementById("<%=AffirmNum.ClientID%>").style.display = "none";
                document.getElementById("<%=NSRemark.ClientID%>").style.display = "none";
                document.getElementById("<%=CancleRemark.ClientID%>").style.display = "none";
            }
            else if (picTypeValue == "8") {
                document.getElementById("<%=INRoomNum.ClientID%>").style.display = "";
                document.getElementById("<%=AffirmNum.ClientID%>").style.display = "";
                document.getElementById("<%=NSRemark.ClientID%>").style.display = "none";
                document.getElementById("<%=CancleRemark.ClientID%>").style.display = "none";
            } else if (picTypeValue == "5") {
                document.getElementById("<%=INRoomNum.ClientID%>").style.display = "none";
                document.getElementById("<%=AffirmNum.ClientID%>").style.display = "none";
                document.getElementById("<%=NSRemark.ClientID%>").style.display = "";
                document.getElementById("<%=CancleRemark.ClientID%>").style.display = "none";
            } else if (picTypeValue == "9") {
                document.getElementById("<%=INRoomNum.ClientID%>").style.display = "none";
                document.getElementById("<%=AffirmNum.ClientID%>").style.display = "none";
                document.getElementById("<%=NSRemark.ClientID%>").style.display = "none";
                document.getElementById("<%=CancleRemark.ClientID%>").style.display = "";
            } else {
                document.getElementById("<%=INRoomNum.ClientID%>").style.display = "none";
                document.getElementById("<%=AffirmNum.ClientID%>").style.display = "none";
                document.getElementById("<%=NSRemark.ClientID%>").style.display = "none";
                document.getElementById("<%=CancleRemark.ClientID%>").style.display = "none";
            }
        }

        function checkValidUpdate() {
            var ddlTicket = document.getElementById("<%=ddlReturnTicket.ClientID%>");
            var ddl = document.getElementById("<%=dropStatus.ClientID%>");
            var index = ddl.selectedIndex;
            var picTypeValue = ddl.options[index].value;
            if (picTypeValue == "") {
                alert("请选择修改状态！");
                document.getElementById("<%=dropStatus.ClientID%>").focus();
                BtnCompleteStyle();
                return false;
            }
            else if (picTypeValue == "8") {
                if (document.getElementById("<%=INRoomNum.ClientID%>").value == "") {
                    alert("请输入入住房号！");
                    document.getElementById("<%=INRoomNum.ClientID%>").focus();
                    BtnCompleteStyle();
                    return false;
                }
                else if (document.getElementById("<%=AffirmNum.ClientID%>").value == "") {
                    alert("请输入确认号！");
                    document.getElementById("<%=AffirmNum.ClientID%>").focus();
                    BtnCompleteStyle();
                    return false;
                }
            } else if (ddlTicket.options[ddlTicket.selectedIndex].value == "") {
                alert("请选择是否需要返券！");
                BtnCompleteStyle();
                return false;
            }
            else if (document.getElementById("<%=txtRemark.ClientID%>").value == "") {
                alert("请输入操作备注！");
                document.getElementById("<%=txtRemark.ClientID%>").focus();
                BtnCompleteStyle();
                return false;
            }



            return true;
        }
    </script>
</asp:Content>
