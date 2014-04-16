<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CashApplProcess.aspx.cs" Inherits="WebUI_CashBack_CashApplProcess" %>

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
    <script language="javascript" type="text/javascript">
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
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=4&RID=' + document.getElementById("<%=hidCID.ClientID%>").value + "&time=" + time, null, fulls);
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<div class="frame01">
        <ul>
        <li class="title">提现申请详情</li>
        <li>
            <table style="width:100%">
                <tr>
                    <td style="width:40%">用户ID：<a id="AUserID" href="#" runat="server" style="cursor:pointer;color:Blue" onclick="PopupUserArea()"><asp:Label ID="lbl_User_ID" runat="server" Text="用户ID"></asp:Label></a></td>
                    <td style="width:30%">申请提现方式：<asp:Label ID="lbl_cash_way" runat="server" Text="申请提现方式"></asp:Label><asp:HiddenField ID="hidCashWayCode" runat="server" /></td> 
                    <td style="width:30%">提现渠道：<asp:Label ID="lbback_channel" runat="server" Text="提现渠道"></asp:Label></td>
                </tr>
                <tr>
                    <td>申请提现金额：<asp:Label ID="lbl_pick_cash_amount" runat="server" Text="申请提现金额"></asp:Label></td> 
                   <td>用户开户银行：<asp:Label ID="lbl_bank_name" runat="server" Text="用户开户银行"></asp:Label></td>
                    <td>分行/支行信：<asp:Label ID="lbl_bank_branch" runat="server" Text="分行/支行信"></asp:Label></td>
                    
                </tr>
                <tr>
                    <td>用户银行卡信息：<asp:Label ID="lbl_bank_card_number" runat="server" Text="用户银行卡号"></asp:Label></td>
                    <td>支付宝账号：<asp:Label ID="lbl_alipay_account" runat="server" Text="支付宝账号"></asp:Label></td>
                    <td>充值手机号码：<asp:Label ID="lbl_recharge_phone_number" runat="server" Text="充值手机号码"></asp:Label></td>                            
                </tr>

                <tr>
                    <td>账户姓名：<asp:Label ID="lbl_alipay_account_name" runat="server" Text="账户姓名"></asp:Label></td>
                    <td>支出金额：<asp:Label ID="lbl_realamount" runat="server" Text="支出金额"></asp:Label></td>
                    <td>手续费：<asp:Label ID="lbl_charge" runat="server" Text="手续费"></asp:Label></td>                            
                </tr>
                <tr>
                    <td>提现操作方式：<asp:Label ID="lbl_ope_type" runat="server" Text="提现操作方式"></asp:Label></td>
                </tr>
            </table>
        </li>
        <li></li>
    </ul>
</div>--%>
    <div id="bankCardDiv" style="display: none;" runat="server">
        <div class="frame01">
            <ul>
                <li class="title">返现申请详情</li>
                <li>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 25%">
                                用户ID：<a id="AUserID_bank" href="#" runat="server" style="cursor: pointer; color: Blue"
                                    onclick="PopupUserArea('1')"><asp:Label ID="lbl_User_ID_bank" runat="server" Text="用户ID"></asp:Label></a>
                            </td>
                            <td style="width: 25%">
                                申请提现方式：<asp:Label ID="lbl_cash_way_bank" runat="server" Text="申请提现方式"></asp:Label><asp:HiddenField
                                    ID="hidCashWayCode_bank" runat="server" />
                            </td>
                            <td style="width: 25%">
                                申请提现金额：<asp:Label ID="lbl_pick_cash_amount_bank" runat="server" Text="申请提现金额"></asp:Label>
                            </td>
                            <td style="width: 25%">
                                提现渠道：<asp:Label ID="lbback_channel_bank" runat="server" Text="提现渠道"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                用户开户银行：<asp:Label ID="lbl_bank_name_bank" runat="server" Text="用户开户银行"></asp:Label>
                            </td>
                            <td>
                                分行/支行信息：<asp:Label ID="lbl_bank_branch_bank" runat="server" Text="分行/支行信"></asp:Label>
                            </td>
                            <td>
                                用户银行卡信息：<asp:Label ID="lbl_bank_card_number_bank" runat="server" Text="用户银行卡号"></asp:Label>
                            </td>
                            <td>
                                提现操作方式：<asp:Label ID="lbl_ope_type_bank" runat="server" Text="提现操作方式"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </li>
                <li></li>
            </ul>
        </div>
    </div>
    <div id="AlipayPortDiv" style="display: none;" runat="server">
        <div class="frame01">
            <ul>
                <li class="title">返现申请详情</li>
                <li>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 25%">
                                用户ID：<a id="AUserID_alipay" href="#" runat="server" style="cursor: pointer; color: Blue"
                                    onclick="PopupUserArea('2')"><asp:Label ID="lbl_User_ID_alipay" runat="server" Text="用户ID"></asp:Label></a>
                            </td>
                            <td style="width: 25%">
                                申请提现方式：<asp:Label ID="lbl_cash_way_alipay" runat="server" Text="申请提现方式"></asp:Label><asp:HiddenField
                                    ID="hidCashWayCode_alipay" runat="server" />
                            </td>
                            <td style="width: 25%">
                                申请提现金额：<asp:Label ID="lbl_pick_cash_amount_alipay" runat="server" Text="申请提现金额"></asp:Label>
                            </td>
                            <td style="width: 25%">
                                提现渠道：<asp:Label ID="lbback_channel_alipay" runat="server" Text="提现渠道"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                支付宝账号：<asp:Label ID="lbl_Alipay_Port" runat="server" Text="支付宝账号"></asp:Label>
                            </td>
                            <td colspan="2" style="white-space: nowrap; overflow: hidden;">
                                支付宝账户名：<span id="AlipayNameDiv" runat="server"><asp:Label ID="lbl_Alipay_port_name"
                                    runat="server" Text="支付宝账户名"></asp:Label></span>
                            </td>
                            <td>
                                提现操作方式：<asp:Label ID="lbl_ope_type_alipay" runat="server" Text="提现操作方式"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </li>
                <li></li>
            </ul>
        </div>
    </div>
    <div id="MobilePortDiv" style="display: none;" runat="server">
        <div class="frame01">
            <ul>
                <li class="title">返现申请详情</li>
                <li>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 25%">
                                用户ID：<a id="AUserID_mobile" href="#" runat="server" style="cursor: pointer; color: Blue"
                                    onclick="PopupUserArea('3')"><asp:Label ID="lbl_User_ID_mobile" runat="server" Text="用户ID"></asp:Label></a>
                            </td>
                            <td style="width: 25%">
                                申请提现方式：<asp:Label ID="lbl_cash_way_mobile" runat="server" Text="申请提现方式"></asp:Label><asp:HiddenField
                                    ID="hidCashWayCode_mobile" runat="server" />
                            </td>
                            <td style="width: 25%">
                                申请提现金额：<asp:Label ID="lbl_pick_cash_amount_mobile" runat="server" Text="申请提现金额"></asp:Label>
                            </td>
                            <td style="width: 25%">
                                提现渠道：<asp:Label ID="lbback_channel_mobile" runat="server" Text="提现渠道"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                用户手机号码：<asp:Label ID="lbl_recharge_phone_number_mobile" runat="server" Text="用户手机号码"></asp:Label>
                            </td>
                            <td>
                                提现操作方式：<asp:Label ID="lbl_ope_type_alipay_mobile" runat="server" Text="提现操作方式"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </li>
                <li></li>
            </ul>
        </div>
    </div>
    <div class="frame01" style="display: none;">
        <ul>
            <li class="title">快捷操作链接</li>
            <li>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 33%; text-align: center">
                            <input type="button" id="EbankPort" class="btn primary" value="网银接口" onclick="javascript:window.open('https://ebank.sdb.com.cn/corporbank/logon_basic.jsp')" />
                        </td>
                        <%--<td style="width:33%;text-align:center"><input type="button" id="MobilePort" class="btn primary" value ="手机充值接口" /></td> 
                    <td style="width:33%;text-align:center"><input type="button" id="AlipayPort" class="btn primary" value ="支付宝" onclick="javascript:window.open('https://www.alipay.com/')" /></td>                               --%>
                        <%--                    <td style="width:33%;text-align:center"><asp:Button ID="EbankPort" runat="server" Text="网银接口" CssClass="btn primary" OnClientClick="return checkRechargePort('','');"  onclick="EbankPort_Click"/></td>--%>
                        <td style="width: 33%; text-align: center">
                            <asp:Button ID="MobilePort" runat="server" Text="手机充值接口" CssClass="btn primary" OnClientClick="return checkRechargePort('','');"
                                OnClick="MobilePort_Click" />
                        </td>
                        <td style="width: 33%; text-align: center">
                            <asp:Button ID="AlipayPort" runat="server" Text="支付宝" CssClass="btn primary" OnClientClick="return checkRechargePort('','');"
                                OnClick="AlipayPort_Click" />
                        </td>
                    </tr>
                </table>
            </li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">提现申请操作 [当前处理人：<asp:Label ID="lbl_process_userid" runat="server"
                Text="处理人"></asp:Label>]</li>
            <li>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; display: none;">
                            处理流程说明：
                        </td>
                        <td style="width: 40%; display: none;">
                            <table width="100%">
                                <tr>
                                    <%-- <td><font color="blue">已提交——>已审核——>已操作——>已成功 or 已失败</font></td>--%>
                                    <td>
                                        <asp:Label ID="lblFlowText" runat="server" Text="已提交——>已审核——>已操作——>已成功 or 已失败"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hidMenuSpan" runat="server" />
                            <asp:HiddenField ID="hidCashType" runat="server" />
                            <asp:HiddenField ID="hidProcessStatus" runat="server" />
                            <asp:HiddenField ID="hidSOURCECHANNEL" runat="server" />
                        </td>
                        <td style="width: 10%; display: none;">
                            处理人：
                        </td>
                        <td style="width: 40%; display: none;">
                            <%--<asp:Label ID="lbl_process_userid" runat="server" Text="处理人"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">
                            处理备注：
                        </td>
                        <td colspan="3">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRemark" runat="server" Width="99%"
                                MaxLength="60"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注（前端可见）：
                        </td>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td style="width: 90%">
                                        <asp:TextBox ID="txtUserRemark" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEditProcessRemark" runat="server" Text="保存" CssClass="btn primary"
                                            OnClick="btnEditProcessRemark_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="text-align: center;">
                            <table style="text-align: center; width: 100%">
                                <tr>
                                    <td id="tdChkPush" runat="server" style="display: none; width: 15%">
                                        <asp:CheckBox ID="chkPush" runat="server" Text="发送Push提醒" />
                                    </td>
                                    <td align="right" style="width: 15%">
                                        <asp:Button ID="btnOk" runat="server" Text="确定" CssClass="btn primary" OnClientClick="BtnLoadStyle();return checkValid();"
                                            OnClick="btnOk_Click" />
                                    </td>
                                    <td align="center" style="width: 15%">
                                        <asp:Button ID="btnFail" runat="server" Text="已失败" CssClass="btn primary" OnClientClick="return checkValid();"
                                            OnClick="btnFail_Click" />
                                    </td>
                                    <td align="center" style="width: 15%">
                                        <input type="button" runat="server" id="EbankByPort" class="btn primary" value="网银接口"
                                            onclick="javascript:window.open('https://ebank.sdb.com.cn/corporbank/logon_basic.jsp')" />
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <input type="button" class="btn" value="返回提现列表" onclick="PopupArea()" />
                                        &nbsp;&nbsp;
                                        <input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" />
                                        <asp:HiddenField ID="hidCID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="background" class="pcbackground" style="display: none;">
            </div>
            <div id="progressBar" class="pcprogressBar" style="display: none;">
                数据加载中，请稍等...</div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="frame01">
        <ul>
            <li class="title">提现申请操作历史</li>
            <li>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gridViewCash" runat="server" AutoGenerateColumns="False" BackColor="White"
                            AllowPaging="True" PageSize="20" CssClass="GView_BodyCSS" OnPageIndexChanging="gridViewCash_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HANDLE_STATUS" HeaderText="处理结果">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HANDLE_TIME" HeaderText="处理时间">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HANDLER" HeaderText="处理人">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HANDLE_REMARK" HeaderText="备注">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HANDLE_PUSH" HeaderText="是否Push">
                                    <ItemStyle HorizontalAlign="Center" />
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
    <script language="javascript" type="text/javascript">
        function checkValid() {
            if (document.getElementById("<%=HidPort.ClientID%>").value != "2" && document.getElementById("<%=HidPort.ClientID%>").value != "3") {
                var remark = document.getElementById("<%=txtRemark.ClientID%>").value;
                if ((remark != "") && (remark.length > 150)) {
                    alert("处理备注中只能输入150个字！");
                    document.getElementById("<%=txtRemark.ClientID%>").focus();
                    BtnCompleteStyle();
                    return false;
                }
            } else {
                if (document.getElementById("<%=HidPort.ClientID%>").value == "2") {
                    if (window.confirm('确认使用支付宝返还吗？')) {
                        return true;
                    } else {
                        BtnCompleteStyle();
                        return false;
                    }
                } else {
                    if (window.confirm('确认使用手机充值吗？')) {
                        return true;
                    } else {
                        BtnCompleteStyle();
                        return false;
                    }
                }
            }
            return true;
        }

        function PopupArea() {
            var time = new Date();
            window.location.href = "CashApplOperateSearch.aspx?sendFlag=1&menu=" + document.getElementById("<%=hidMenuSpan.ClientID%>").value + "&time=" + time;
        }

        function PopupUserArea(obj) {
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
            var userID = '';
            if (obj == '1') {
                userID = document.getElementById("<%=lbl_User_ID_bank.ClientID%>").innerText;
            } else if (obj == '2') {
                userID = document.getElementById("<%=lbl_User_ID_alipay.ClientID%>").innerText;
            } else {
                userID = document.getElementById("<%=lbl_User_ID_mobile.ClientID%>").innerText;
            }
            window.open("../UserGroup/UserDetailPage.aspx?TYPE=1&ID=" + userID + "&time=" + time, null, fulls);
        }

        function EditAlipayName() {
            document.getElementById("<%=AlipayNameDiv.ClientID%>").innerHTML = "";
            document.getElementById("<%=AlipayNameDiv.ClientID%>").innerHTML = "<input type=\"text\" id=\"txtAlipayName\"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"button\" id=\"btnEditAlipayNameOK\" class=\"btn primary\" value=\"确认\" onclick=\"EditAlipayNameOK();\" />";
        }
        function EditAlipayNameOK() {
            BtnLoadStyle();
            document.getElementById("<%=HidAlipayName.ClientID%>").value = document.getElementById("txtAlipayName").value;
            document.getElementById("<%=AlipayNameDiv.ClientID%>").innerHTML = "";
            document.getElementById("<%=AlipayNameDiv.ClientID%>").innerHTML = document.getElementById("<%=HidAlipayName.ClientID%>").value;

            document.getElementById("<%=btnEditAlipayName.ClientID%>").click();
            //document.getElementById("<%=btnOk.ClientID%>").disabled = "";
        }   
    </script>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="button" id="btnEditAlipayName" runat="server" onserverclick="btnEditAlipayName_Click"
                style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HidSN" runat="server" />
    <asp:HiddenField ID="HidFlowBtn" runat="server" />
    <asp:HiddenField ID="HidPort" runat="server" />
    <asp:HiddenField ID="HidAlipayName" runat="server" />
    <asp:HiddenField ID="Hidlbl_charge" runat="server" />
    <asp:HiddenField ID="Hidlbl_realamount" runat="server" />
    <asp:HiddenField ID="Hidlbl_alipay_account_name" runat="server" />
</asp:Content>
