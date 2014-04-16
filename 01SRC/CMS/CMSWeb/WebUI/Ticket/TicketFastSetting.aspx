<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TicketFastSetting.aspx.cs"
    Inherits="Ticket_TicketFastSetting" Title="快速设置优惠券" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="right">
        <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="优惠券操作" ID="lbRuleTitle" runat="server"></asp:Literal> </li>
        <li>
        <table align="center" border="0" width="100%" class="Table_BodyCSS">
            <tr>
                <td class="tdcell">
                    <asp:Label ID="Lable1" runat="server" Text="名称:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRuleName" runat="server" Width="96%" MaxLength="25"></asp:TextBox><font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    优惠券类型：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                <input type="hidden" id="hidPackageType" runat="server" />
                <%
                    if (this.hidPackageType.Value == "0") { 
                        %>
                            <input type="radio" name="RbtPackageType" id="rbtnOperate" value="0"  checked="checked" onclick="SerRbtNameValue('0')" />运营
                    <input type="radio" name="RbtPackageType" id="rbtMarket" value="1" onclick="SerRbtNameValue('1')" />市场
                    <input type="radio" name="RbtPackageType" id="rbtOther" value="2" onclick="SerRbtNameValue('2')" />其他 <font color="red">*</font>
                    
                        <%
                            }
                    else if (this.hidPackageType.Value == "1")
                    {
                     %>
                            <input type="radio" name="RbtPackageType" id="rbtnOperate" value="0" onclick="SerRbtNameValue('0')" />运营
                    <input type="radio" name="RbtPackageType" id="rbtMarket" value="1" checked="checked" onclick="SerRbtNameValue('1')" />市场
                    <input type="radio" name="RbtPackageType" id="rbtOther" value="2" onclick="SerRbtNameValue('2')" />其他 <font color="red">*</font>                    
                        <%
                    }
                    else
                    { 
                         %>
                            <input type="radio" name="RbtPackageType" id="rbtnOperate" value="0" onclick="SerRbtNameValue('0')" />运营
                    <input type="radio" name="RbtPackageType" id="rbtMarket" value="1" onclick="SerRbtNameValue('1')" />市场
                    <input type="radio" name="RbtPackageType" id="rbtOther" value="2" checked="checked"
                        onclick="SerRbtNameValue('2')" />其他 <font color="red">*</font>
                        <%
                    }
                     %>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    自定义优惠券号码：
                </td>
                <td colspan="3" style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtCustomNumber" runat="server" Text="" MaxLength="10" OnTextChanged="txtCustomNumber_TextChanged"
                    AutoPostBack="true" ></asp:TextBox><asp:CheckBox ID="chkCustomNumber" Text="使用自定义优惠券号码" runat="server" AutoPostBack="true" OnCheckedChanged="ChkCustomValue_Click"/>&nbsp;&nbsp;<asp:Label ID="lbCustomNumMsg" runat="server" Text="" ForeColor="red"/>
                    
                    
                    <%--<asp:TextBox ID="txtCustomNumber" runat="server" Text="" MaxLength="10" OnTextChanged="txtCustomNumber_TextChanged"
                        AutoPostBack="true"></asp:TextBox><input type="checkbox" runat="server" id="chkCustomNumber"
                            onchange="chkcustoClick(this)" />使用自定义优惠券号码（请输入10位数字）&nbsp;&nbsp;<asp:Label ID="lbCustomNumMsg"
                                runat="server" Text="" ForeColor="red" />--%>
                </td>
            </tr>
            <tr>
                <td class="tdcell" style="width: 15%;">
                    最早可使用日期:
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="fromDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width:154px"
                        runat="server" /><font color="red">*</font>
                </td>
                <td class="tdcell" style="width: 15%;">
                    最晚可使用日期:
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="endDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_fromDate\')}'})" style="width:154px"
                        runat="server" /><font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    最早可领用日期:
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="recipientFormDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width:154px"
                        runat="server" /><font color="red">*</font>
                </td>
                <td class="tdcell">
                    最晚可领用日期：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="recipientEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_recipientFormDate\')}'})" style="width:154px"
                        runat="server" /><font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    优惠券金额：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtAcount" runat="server"></asp:TextBox>元<font color="red">*</font>
                </td>
                <td class="tdcell">
                    最低订单金额：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtOrdAmt" runat="server"></asp:TextBox>元<font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    不同用户可领用次数：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtUserCount" runat="server"></asp:TextBox><font color="red">*</font>
                </td>
                <td class="tdcell">
                    同一用户可领用次数：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtUserRepCount" runat="server" Text="1"></asp:TextBox><font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    优惠券描述(展示在客户端)：
                </td>
                <td colspan="3"  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtRuleDesc" runat="server" TextMode="MultiLine" Width="96%" Rows="3"
                        MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    销售渠道:
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBSaleChannel" runat="server" Width="96%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="HOTELVP">HOTELVP</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td class="tdcell">
                    发放渠道：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBSaleChanel" runat="server" Width="90%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem>HOTELVP</asp:ListItem>
                    </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    使用平台：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBUsePlatForm" runat="server" Width="96%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="IOS">IOS</asp:ListItem>
                        <asp:ListItem>ANDROID</asp:ListItem>
                        <asp:ListItem>WAP</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td class="tdcell">
                    领用平台限制:
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBUsePlatFormListBox" runat="server" Width="90%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">无限制</asp:ListItem>
                        <asp:ListItem Value="IOS">IOS</asp:ListItem>
                        <asp:ListItem>ANDROID</asp:ListItem>
                        <asp:ListItem>WAP</asp:ListItem>
                    </asp:ListBox>
                </td>
            </tr>
            <tr class="tdcell">
                <td class="tdcell">
                    价格代码：
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBPriceCode" runat="server" Width="96%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="LMBAR">LMBAR</asp:ListItem>
                        <asp:ListItem Value="LMBAR2">LMBAR2</asp:ListItem>
                        <asp:ListItem Value="BAR">BAR</asp:ListItem>
                        <asp:ListItem Value="BARB">BARB</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td  class="tdcell">
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;"> 
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="新增" OnClientClick="return checkEmpty();" OnClick="btnAdd_Click"
                         /><br />
                    <font color="red"><strong>
                        <asp:Literal Text="(注意：新增后，不能修改也不能删除。) " ID="Literal1" runat="server"></asp:Literal></strong></font>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
            <tr>
                <td align="center" colspan="5" class="tdcell">
                    <asp:GridView ID="gridViewPackage" runat="server" AutoGenerateColumns="False" BackColor="White"
                        Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" OnRowDataBound="gridViewPackage_RowDataBound"
                        AllowPaging="True" OnPageIndexChanging="gridViewPackage_PageIndexChanging" CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="PACKAGECODE" HeaderText="代码" />
                            <asp:BoundField DataField="PACKAGENAME" HeaderText="名称" />
                            <asp:BoundField DataField="AMOUNT" HeaderText="总金额" />
                            <asp:BoundField DataField="STARTDATE" HeaderText="开始日期" />
                            <asp:BoundField DataField="ENDDATE" HeaderText="结束日期" />
                            <asp:BoundField DataField="CLIENTCODE" HeaderText="用户组" />
                            <asp:BoundField DataField="USECODE" HeaderText="使用平台" />
                            <asp:BoundField DataField="CHANELCODE" HeaderText="发放渠道" />
                            <asp:BoundField DataField="CITYID" HeaderText="发放城市ID" />
                            <asp:HyperLinkField HeaderText="详情" Text="详情" DataNavigateUrlFields="PACKAGECODE"
                                DataNavigateUrlFormatString="TicketFastSettingDetails.aspx?packagecode={0}"
                                Target="_blank" NavigateUrl="~/WebUI/Ticket/TicketFastSettingDetails.aspx">
                                <ItemStyle Width="5%" />
                            </asp:HyperLinkField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </li>
      </ul>
    </div>
    </div>
    <script type="text/javascript">
        function checkEmpty() {
            var PackageName = document.getElementById("<%=txtRuleName.ClientID%>").value;
            if (PackageName == "") {
                alert("优惠券名称不能为空！");
                document.getElementById("<%=txtRuleName.ClientID%>").focus();
                return false;
            }
            if (PackageName != "" && PackageName.length > 25) {
                alert("优惠券礼包名称的长度不能大于25个字符！");
                document.getElementById("<%=txtRuleName.ClientID%>").focus();
                return false;
            }
            var FromDate = document.getElementById("<%=fromDate.ClientID %>").value;
            if (FromDate == "") {
                alert("最早可使用日期不能为空！");
                document.getElementById("<%=fromDate.ClientID %>").focus();
                return false;
            }

            var EndDate = document.getElementById("<%=endDate.ClientID %>").value;
            if (EndDate == "") {
                alert("最晚可使用日期不能为空！");
                document.getElementById("<%=endDate.ClientID %>").focus();
                return false;
            }
            var recipientFromDate = document.getElementById("<%=recipientFormDate.ClientID %>").value;
            if (recipientFromDate == "") {
                alert("最早可领用日期不能为空！");
                document.getElementById("<%=recipientFormDate.ClientID %>").focus();
                return false;
            }

            var recipientEndDate = document.getElementById("<%=recipientEndDate.ClientID %>").value;
            if (recipientEndDate == "") {
                alert("最晚可领用日期不能为空！");
                document.getElementById("<%=recipientEndDate.ClientID %>").focus();
                return false;
            }

            var AllAcount = document.getElementById("<%=txtAcount.ClientID%>").value;
            if (AllAcount == "") {
                alert("优惠券金额不能为空！");
                document.getElementById("<%=txtAcount.ClientID%>").focus();
                return false;
            }

            if (AllAcount != "" && isNaN(AllAcount)) {
                alert("优惠券总金额必须为必须大于等于0的数字！");
                document.getElementById("<%=txtAcount.ClientID%>").focus();
                return false;
            }
            //var inputRulesAcount = /^\+?[1-9][0-9]*$/; ///^\d+(\.\d+)?$/; //只能是数字且不能为负数
            var inputRulesAcount = /^\d+$/;
            if (inputRulesAcount.test(AllAcount) == false) {
                alert(AllAcount);
                alert("优惠券总金额只能是数字且不能为负数");
                document.getElementById("<%=txtAcount.ClientID%>").focus();
                return false;
            }
            var AllAmount = document.getElementById("<%=txtOrdAmt.ClientID%>").value;
            if (AllAmount == "") {
                alert("最低订单金额不能为空！");
                document.getElementById("<%=txtOrdAmt.ClientID%>").focus();
                return false;
            } 
            if (AllAmount != "" && isNaN(AllAmount)) {
                alert("最低订单金额必须为必须大于等于0的数字！");
                document.getElementById("<%=txtOrdAmt.ClientID%>").focus();
                return false;
            }
            //var inputRules = /^\+?[1-9][0-9]*$/; ///^\d+(\.\d+)?$/; //只能是数字且不能为负数
            var inputRules = /^\d+$/;
            if (inputRules.test(AllAmount) == false) {
                alert("最低订单金额只能是数字且不能为负数");
                document.getElementById("<%=txtOrdAmt.ClientID%>").focus();
                return false;
            }

            var AllUserCount = document.getElementById("<%=txtUserCount.ClientID%>").value;
            if (AllUserCount == "") {
                alert("不同用户可领用次数不能为空！");
                document.getElementById("<%=txtUserCount.ClientID%>").focus();
                return false;
            }

            if (AllUserCount != "" && isNaN(AllUserCount)) {
                alert("不同用户可领用次数必须为必须大于等于0的数字！");
                document.getElementById("<%=txtUserCount.ClientID%>").focus();
                return false;
            }

            var AllUserRepCount = document.getElementById("<%=txtUserRepCount.ClientID%>").value;
            if (AllUserRepCount == "") {
                alert("同一用户可领用次数不能为空！");
                document.getElementById("<%=txtUserRepCount.ClientID%>").focus();
                return false;
            }

            if (AllUserRepCount != "" && isNaN(AllUserRepCount)) {
                alert("同一用户可领用次数必须为必须大于等于0的数字！");
                document.getElementById("<%=txtUserRepCount.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function chkcustoClick(chkCustomNumber) {
            alert(chkCustomNumber.checked);
            if (chkCustomNumber.checked) {
                document.getElementById("<%=txtCustomNumber.ClientID %>").disabled = "";
                document.getElementById("<%=txtCustomNumber.ClientID %>").foucs();
            }
            else {
                document.getElementById("<%=txtCustomNumber.ClientID %>").value = "";
                document.getElementById("<%=txtCustomNumber.ClientID %>").disabled = "disabled";
                document.getElementById("<%=lbCustomNumMsg.ClientID %>").value = "";

            }
        }
        function SerRbtNameValue(arg) {
            document.getElementById("<%=hidPackageType.ClientID %>").value = arg;
        }
    </script>
</asp:Content>
