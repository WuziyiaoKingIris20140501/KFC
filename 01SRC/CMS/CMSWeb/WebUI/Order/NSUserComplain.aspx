<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NSUserComplain.aspx.cs" Inherits="WebUI_Order_NSUserComplain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
            z-index: 20000;
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
            z-index: 20001;
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
            width: 1100px;
            height: 800px;
            position: absolute;
            padding: 1px;
            z-index: 10000;
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
        
        .Tb_BodyCSS tr
        {
            height: 30px;
        }
        .Tb_BodyCSS td
        {
            height: 30px;
            border-right: 1px #d5d5d5 solid;
            border-top: 1px #d5d5d5 solid;
            padding-left: 10px;
        }
        
        .Tb_BodyCSS
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1pxsolid#ccc;
        }
        
        .Tb_BodyCSS2 tr
        {
            height: 30px;
        }
        .Tb_BodyCSS2 td
        {
            height: 30px;
            border-right: 1px #d5d5d5 solid;
            border-top: 1px #d5d5d5 solid;
        }
        
        .Tb_BodyCSS2
        {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1pxsolid#ccc;
        }
        
        
        .popupPreView
        {
            width: 1100px;
            height: 800px;
            position: absolute;
            padding: 1px;
            z-index: 11000;
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
    <script language="javascript" type="text/javascript">
        document.onkeydown = function (e) {
            e = e ? e : window.event;
            var keyCode = e.which ? e.which : e.keyCode;
            if (keyCode == 27)
                invokeCloseList();
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

        function PopupArea(arg, content) {
            document.getElementById("<%=txtAffirmNum.ClientID%>").value = "";
            document.getElementById("<%=txtRemark.ClientID%>").innerHTML = "";

            document.getElementById("<%=hidOrderNum.ClientID%>").value = arg;
            document.getElementById("<%=hidContent.ClientID%>").value = content;
            document.getElementById("<%=btnDetails.ClientID%>").click();
        } 

        //显示弹出的层
        function invokeOpenList() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            BtnCompleteStyle();
             $.ajax({
                async: true,
                contentType: "application/json",
                url: "NSUserComplain.aspx/GetOperateHis",
                type: "POST",
                dataType: "json",
                data: "{FogOrderNum:'" + document.getElementById("<%=hidOrderNum.ClientID%>").value + "'}",
                success: function (data) {
                    var output = "<table class=\"Tb_BodyCSS\" style=\"border: 1px #d5d5d5 solid;padding: 1px; margin: -3px 0px 0px 5px; width: 99.4%; \"><tr style=\"background-color: #E9EBF2;\"><td align=\"center\">订单操作历史</td><td align=\"center\">订单状态</td><td align=\"center\">操作人</td><td align=\"center\">操作时间</td><td align=\"center\">操作备注</td></tr>";
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        output += "<tr><td align=\"center\">" + d[i].MEMO + "</td><td align=\"center\">" + d[i].MSG + "</td><td align=\"center\">" + d[i].OPERATOR + "</td><td align=\"center\">" + d[i].EVENTTIME + "</td><td align=\"center\">" + d[i].REMARK + "</td></tr>";
                    });
                    output += "</table>";
                    document.getElementById("<%=LmbarRemarkHistory.ClientID%>").innerHTML = output;
                },
                error: function (json) {

                }
            }); 
        }

        //隐藏弹出的层
        function invokeCloseList() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        //验证是否选择需要修改的状态
        function checkdropStatus() {
            var Status = document.getElementById("<%=dropStatus.ClientID%>");
            var index = Status.selectedIndex;
            var picTypeValue = Status.options[index].value;
            if (picTypeValue == "-1") {
                alert("请选择需要修改的状态!");
                BtnCompleteStyle();
                    return false;
                }
                return true;
            }

        function OnchangeStatus() {
            var ddl = document.getElementById("<%=dropStatus.ClientID%>");
            var index = ddl.selectedIndex;
            var picTypeValue = ddl.options[index].value;
            if (picTypeValue == "8") {
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
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <div class="frame01">
            <ul>
                <li class="title">NS用户申诉</li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        订单号：
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOrderNum" runat="server" Width="150px" MaxLength="100" />
                                    </td>
                                    <td align="right">
                                        订单创建：
                                    </td>
                                    <td align="left">
                                        <input id="orderCreateStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_orderCreateEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        <input id="orderCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_orderCreateStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td align="right">
                                        订单申诉：
                                    </td>
                                    <td align="left">
                                        <input id="orderUpdateStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_orderUpdateEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        <input id="orderUpdateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_orderUpdateStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        处理状态：
                                    </td>
                                    <td colspan="2" align="left">
                                        <asp:RadioButtonList ID="rdoProcess" runat="server" RepeatDirection="Vertical" RepeatColumns="8"
                                            RepeatLayout="Table">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td colspan="3" align="left">
                                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="查询"
                                                    OnClientClick="BtnLoadStyle()" OnClick="btnSearch_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
        <div class="frame02">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" PageSize="20" 
                        CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" CssClass="GView_BodyCSS"
                        OnRowDataBound="gridViewCSList_RowDataBound" OnPageIndexChanging="gridViewCSList_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="HotelName" HeaderText="酒店名称">
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="订单号">
                                <ItemTemplate>
                                    <a href="###" onclick="BtnLoadStyle();PopupArea('<%# DataBinder.Eval(Container.DataItem, "order_num") %>','<%# DataBinder.Eval(Container.DataItem, "content") %>')">
                                        <%# DataBinder.Eval(Container.DataItem, "order_num")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PriceCode" HeaderText="价格代码">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="content" HeaderText="用户申诉房号">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="create_time" HeaderText="申诉时间">
                                <ItemStyle HorizontalAlign="Center" Width="13%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="update_time" HeaderText="处理时间">
                                <ItemStyle HorizontalAlign="Center" Width="13%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="is_process" HeaderText="处理状态">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="operator" HeaderText="处理人">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
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
        </div>
        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
        <ContentTemplate>
        <div id="bgDiv2" class="bgDiv2">
        </div>
        <div id="popupDiv2" class="popupDiv2">
            <div style="width: 99%;margin: 0,0,0,0">
                <table width="100%">
                    <tr>
                        <td style="width: 60%" valign="top" align="right">
                            <br />
                            <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 3px; width: 100%">
                                <tr align="left">
                                    <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                        padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                        订单信息--[<asp:Label ID="lblOrderNum" runat="server" ></asp:Label>]
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="tbDetail" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                padding: 1px; margin: -3px 0px 0px 5px; width: 99.4%; height: 100%">
                                <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        预定酒店:
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:Label ID="lblHotelName" runat="server" Text="123456"/>
                                    </td>
                                    <td align="right">
                                        价格代码:
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:Label ID="lblPriceCode" runat="server" />
                                    </td>
                                    <td align="right">
                                        酒店负责销售:
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:Label ID="lblHotelLinkMan" runat="server" />
                                    </td>
                                </tr>
                                <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        预订人号码:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblPhone" runat="server" />
                                    </td>
                                    <td align="right">
                                        订单创建日期:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblCreateTime" runat="server" />
                                    </td>
                                    <td align="right">
                                        酒店销售电话:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblHotelLinkTel" runat="server" />
                                    </td>
                                </tr>
                                <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        入住人姓名:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblGuestName" runat="server" />
                                    </td>
                                    <td align="right">
                                        酒店供应商:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblVendor" runat="server" />
                                    </td>
                                    <td align="right">
                                        酒店电话:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblhotelTel" runat="server" />
                                    </td>
                                </tr>
                                <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        房型名称:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblRoomName" runat="server" />
                                    </td>
                                    <td align="right">
                                        订单总金额:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblBookTotalPrice" runat="server" />
                                    </td>
                                    <td align="right">
                                        审核电话:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblReviewTel" runat="server" />
                                    </td>
                                </tr>
                                <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        入住 - 离店:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblInOrOutDate" runat="server" />
                                    </td>
                                    <td align="right">
                                        返现券金额:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblTicketCode" runat="server" />
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                         <div >
                            <caption style="float: right; margin-right: -18px; margin-top: -225px;">
                                <img src="../../Styles/images/close.png" style="cursor: pointer" alt="关闭" title="关闭"
                                onclick="invokeCloseList()" />
                            </caption>
                        </div>
                    </tr>
                </table>
            </div>
            <div style="width: 99%; margin: 0,0,0,0;">
                <table width="100%">
                    <tr>
                        <td style="width: 60%" valign="top" align="right">
                            <br />
                            <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 3px; width: 100%">
                                <tr align="left">
                                    <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                        padding: 5px 5px 3px 10px;  font-weight: bold; color: White;">
                                        订单状态操作
                                    </td>
                                </tr>
                            </table>
                             <table runat="server" id="Table1" class="Tb_BodyCSS" style="border: 1px #d5d5d5 solid;
                                padding: 1px; margin: -3px 0px 0px 5px; width: 99.4%;height: 100%">
                                <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        当前HVP状态:
                                    </td>
                                    <td align="left" style="width: 25%;">
                                        <asp:Label ID="lblBookStatusOther" runat="server" />
                                    </td>
                                    <td align="right">
                                        当前供应商状态:
                                    </td>
                                    <td align="left" style="width: 25%;">
                                        <asp:Label ID="lblVendorStatus" runat="server" />
                                    </td>
                                </tr>
                                 <tr style="background-color: #E9EBF2;">
                                    <td align="right">
                                        修改状态:
                                    </td>
                                    <td align="left" style="width: 15%;" onchange="OnchangeStatus()">
                                       <select id="dropStatus" runat="server">
                                            <option value="-1">请选择</option>
                                            <option value="3">用户取消</option>
                                            <option value="9">CC取消</option>
                                            <option value="8">离店</option>
                                            <option value="5">No-Show</option>
                                        </select>
                                    </td>
                                    <td align="right">
                                        用户申诉房号:
                                    </td>
                                    <td align="left" style="width: 25%;">
                                        <asp:Label ID="lblNSRoomNum" runat="server" />
                                    </td>
                                </tr>


                                
                                 <tr>
                                    <td colspan="4">
                                        <div style="display: none;" id="INRoomNum" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;入住房号：&nbsp;<asp:TextBox ID="txtINRoomNum" runat="server"
                                                Width="150px" MaxLength="100" />
                                        </div>
                                        <div style="display: none;" id="AffirmNum" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;确认号：&nbsp;&nbsp;<asp:TextBox ID="txtAffirmNum"
                                                runat="server" Width="150px" MaxLength="100" />
                                        </div>
                                        <div style="display: none;" id="NSRemark" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NS原因：&nbsp;&nbsp;<asp:DropDownList ID="ddpNoShow"
                                                CssClass="noborder_inactive" runat="server" Width="250px" />
                                        </div>
                                        <div style="display: none;" id="CancleRemark" runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;取消原因：&nbsp;&nbsp;<asp:DropDownList ID="ddpCanelReson"
                                                CssClass="noborder_inactive" runat="server" Width="250px" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">
                                        操作备注:
                                    </td>
                                    <td colspan="3">
                                         <textarea id="txtRemark" runat="server" rows="3" cols="100" ></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                     <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
                                        <asp:Button ID="btnVerifyUpdate" runat="server" CssClass="btn primary" Width="75px" Text="确认修改" OnClientClick="BtnLoadStyle();return checkdropStatus();" OnClick="btnVerifyUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnVerifyNotUpdate" runat="server" CssClass="btn primary" Width="75px" Text="确认不修改" OnClientClick="BtnLoadStyle();" OnClick="btnVerifyNotUpdate_Click"/>
                                         </ContentTemplate>
        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                       </td>
                    </tr>
                </table>
            </div>
            <div style="width: 99%;  margin: 0,0,0,0;">
                <table width="100%">
                    <tr>
                        <td style="width: 60%" valign="top" align="right">
                            <br />
                            <table style="border: 0px; padding: 0px; margin: -15px 0px 0px 3px; width: 100%">
                                <tr align="left">
                                    <td align="left" valign="middle" style="background-color: #6379B2; height: 30px;
                                        padding: 5px 5px 3px 10px; font-weight: bold; color: White;">
                                        订单状态历史
                                    </td>
                                </tr> 
                            </table>
                            <div id="LmbarRemarkHistory" runat="server" style="width: 100%; height: 220px; overflow-y: auto;">

                            </div> 
                            </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="background" class="pcbackground" style="display: none;">
        </div>
        <div id="progressBar" class="pcprogressBar" style="display: none;">
            数据加载中，请稍等...</div>
        <asp:HiddenField ID="hidOrderNum" runat="server" />
        <asp:HiddenField ID="hidContent" runat="server" />
        <asp:HiddenField ID="hidBookStatusOther" runat="server" />
        
        <div style="display:none">
            <asp:Button ID="btnDetails" runat="server" CssClass="btn primary" Width="75px" Text="详情" OnClick="btnDetails_Click" />
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
