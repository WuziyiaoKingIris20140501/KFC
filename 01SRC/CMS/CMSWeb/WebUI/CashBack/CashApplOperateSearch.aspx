<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CashApplOperateSearch.aspx.cs" Inherits="WebUI_CashBack_CashApplOperateSearch" %>

<%@ Register Assembly="HotelVp.ServiceControl" Namespace="HotelVp.ServiceControl"
    TagPrefix="cc1" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<style type="text/css" >
.pcbackground { 
display: block; 
width: 100%; 
height: 100%; 
opacity: 0.4; 
filter: alpha(opacity=40); 
background:while; 
position: absolute; 
top: 0; 
left: 0; 
z-index: 2000; 
} 
.pcprogressBar { 
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
    function SetChkAllStyle() {
        var chkObject = document.getElementById("<%=gridViewCash.ClientID%>");
        if (chkObject != null) {
            var chkInput = chkObject.getElementsByTagName("input");
            for (var i = 0; i < chkInput.length; i++) {
                if (chkInput[i].type == "checkbox") {
                    if (document.getElementById("chkAllItems").checked == true) {
                        chkInput[i].checked = true;
                    }
                    else {
                        chkInput[i].checked = false;
                    }
                }
            }
        }
    }

    function checkValidRemark(arg) {
        var remark = document.getElementById("<%=txtRemark.ClientID%>").value;
        if ((remark != "") && (remark.length > 150)) {
            document.getElementById("<%=MessageContent.ClientID%>").innerText = "处理备注中只能输入150个字！";
            document.getElementById("<%=txtRemark.ClientID%>").focus();
            return false;
        }

        if (arg == "0") {
            if (!ChkCashStatusList()) {
                var result = window.confirm("批量处理数据仅操作状态为已操作的申请，是否确定？");
                if (!result) {
                    return false
                }
            }
        }
        else {
            if (!ChkModCashStatusList()) {
                var result = window.confirm("批量处理数据仅操作状态为已提交的申请，是否确定？");
                if (!result) {
                    return false
                }
            }
        }
        BtnLoadStyle();
        return true;
    }

    function ChkModCashStatusList() {
        t = document.getElementById("<%=gridViewCash.ClientID%>");
        var cellNum = 7;
        var chkInput = 0;
        for (i = 1; i < t.rows.length; i++) {
            var chkInputs = t.rows[i].cells[chkInput].getElementsByTagName("input")[0];
            if (chkInputs != null && chkInputs.type == "checkbox" && chkInputs.checked == true) {
                if (t.rows[i].cells[cellNum].innerHTML != "已提交") {
                    return false;
                }
            }
        }
        return true;
    }

    function ChkCashStatusList() {
        t = document.getElementById("<%=gridViewCash.ClientID%>");
        var cellNum = 7;
        var chkInput = 0;
        for (i = 1; i < t.rows.length; i++) {
            var chkInputs = t.rows[i].cells[chkInput].getElementsByTagName("input")[0];
            if (chkInputs != null && chkInputs.type == "checkbox" && chkInputs.checked == true) {
                if (t.rows[i].cells[cellNum].innerHTML != "已操作") {
                    return false;
                }
            }
        }
        return true;
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
<asp:ScriptManager ID="ScriptManager1"  runat="server"></asp:ScriptManager>
<div id="right">
<div class="frame01">   
        <ul>
        <li class="title">提现申请搜索</li>
        <li>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  ChildrenAsTriggers="false">
        <ContentTemplate>
            <table>
                <tr>
                    <td>手机号码：</td>
                    <td><asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="11"/></td> 
                    <td>提现ID：<asp:TextBox ID="txtCashID" runat="server" Width="460px" MaxLength="1000"/></td> 
                </tr>
                <tr>
                   <td>产生时间：</td>
                    <td><input id="dtStartCreateDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00'})" runat="server"/></td>
                    <td>至：<input id="dtEndCreateDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dtStartCreateDate\')}'})" runat="server"/><input type="checkbox" name="chkLimitCreateDate" id="chkLimitCreateDate" value="0" onclick="javascript:checkLimit('0');"/>不限制                    
                    </td>
                </tr>
                <tr>
                    <td>处理时间：</td>
                    <td><input id="dtStartProcessDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00'})" runat="server"/></td>
                    <td>至：<input id="dtEndProcessDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dtStartProcessDate\')}'})" runat="server"/><input type="checkbox" name="chkLimitProcessDate" id="chkLimitProcessDate" value="1" onclick="javascript:checkLimit('1');"/>不限制</td>                    
                </tr>
            </table> 
            <table>
                <tr>
                    <td>申请提现方式：</td>
                    <td><asp:DropDownList ID="ddlAppMode" CssClass="noborder_inactive" runat="server" 
                            Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="1">现金返还</asp:ListItem>
                        <asp:ListItem Value="2">支付宝返还</asp:ListItem>
                        <asp:ListItem Value="3">手机充值</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <td>处理状态：
                        <asp:DropDownList ID="ddlProcessStatus" CssClass="noborder_inactive" 
                            runat="server" Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="0">已提交-所有</asp:ListItem>
                        <asp:ListItem Value="5">已提交-1次</asp:ListItem>
                        <asp:ListItem Value="6">已提交-多次</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="4">已操作</asp:ListItem>
                        <asp:ListItem Value="2">已失败</asp:ListItem>
                        <asp:ListItem Value="3">已成功</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>提现申请渠道：
                        <asp:DropDownList ID="ddlCashCanType" CssClass="noborder_inactive" 
                            runat="server" Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="1">用户提现</asp:ListItem>
                        <asp:ListItem Value="2">CMS提现</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>是否自动处理：
                        <asp:DropDownList ID="ddlOpeType" CssClass="noborder_inactive" runat="server" Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="2">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr> 
                 <tr>
                    <td><asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn primary" OnClientClick="BtnLoadStyle();" onclick="btnSearch_Click" /></td>
                    <td>
                        <input type="button" value="重置" id="btnReset" class="btn" onclick="clearText();" />
                         <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/>
                    </td>
                </tr> 
            </table>    
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="btnExport" />
        </Triggers>
     </asp:UpdatePanel>
        </li>
    </ul>
</div>
<div class="frame01">
        <ul>
        <li class="title">搜索结果列表</li>
        <li>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table >
                    <tr>
                        <td valign="top">
                            处理备注：
                        </td>
                        <td >
                            <asp:TextBox ID="txtRemark" runat="server" Width="400px" Height="50px"  MaxLength="60" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注（前端可见）：
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtUserRemark" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:DropDownList ID="ddpUserRemark" CssClass="noborder_inactive" runat="server" Width="153px"/>--%>

                            <cc1:CDropdownList ID="cddpUserRemark" runat="server" Width="250px" Height="20px" MaxLength="50"/>
                        </td>
                    </tr>
                </table>
                <table >
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="btnModify" runat="server" Text="已操作" CssClass="btn primary" OnClientClick="return checkValidRemark('1');"  onclick="btnModify_Click"/>
                            <asp:Button ID="btnOk" runat="server" Text="已成功" CssClass="btn primary"  OnClientClick="return checkValidRemark('0');"  onclick="btnOk_Click"/>
                            <asp:Button ID="btnFail" runat="server" Text="已失败" CssClass="btn primary" OnClientClick="return checkValidRemark('0');"  onclick="btnFail_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="MessageContent" runat="server" style="color:red;width:800px;"></div>
                            <div id="background" class="pcbackground" style="display: none; "></div> 
                            <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
                        </td>
                    </tr>
                </table>
                <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                <asp:HiddenField ID="hidMenuSpan" runat="server"/>
                <asp:HiddenField ID="hidStatusConfirm" runat="server"/>
                <asp:GridView ID="gridViewCash"  runat="server" AutoGenerateColumns="False" DataKeyNames="SN" BackColor="White" PageSize="100" CssClass="GView_BodyCSS" >
                    <Columns> 
                        <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate >
                            <input type="checkbox" name="chkAllItems" id="chkAllItems" onclick="SetChkAllStyle()"/>选择
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" name="chkItems" id="chkItems" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请ID">
                            <ItemTemplate>                                    
                                <a href="#" onclick="PopupArea('<%# Eval("SN") %>')"><%#Eval("ID") %></a>
                            </ItemTemplate>                                    
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="BACK_OWNER" HeaderText="申请人" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                       <asp:BoundField DataField="SN" HeaderText="CashSN" Visible="false">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField HeaderText="用户ID" DataNavigateUrlFields="USER_ID" DataNavigateUrlFormatString="~/WebUI/UserGroup/UserDetailPage.aspx?ID={0}&TYPE=1" 
                         Target="_blank" DataTextField="USER_ID"><ItemStyle HorizontalAlign="Center"/></asp:HyperLinkField>

                        <%--<asp:BoundField DataField="USER_ID" HeaderText="用户ID" >                      
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>--%>

                        <asp:BoundField DataField="PICK_CASH_AMOUNT" HeaderText="金额" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CASH_WAY" HeaderText="提现方式" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="SOURCECHANNEL" HeaderText="提现申请渠道" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="APPLICATE_TIME" HeaderText="提现申请时间" >                                  
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PROCESS_STATUS" HeaderText="处理状态" >                                    
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                     <%--   <asp:TemplateField HeaderText="处理状态">
                            <ItemTemplate>                                    
                                <asp:Label ID="lblProcessStatus" runat="server" Text='<%#Eval("PROCESS_STATUS") %>'></asp:Label> 
                            </ItemTemplate>                                    
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="IS_PUSH" HeaderText="备注" >                                    
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="REALAMOUNT" HeaderText="支出金额" >                                    
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CHARGE" HeaderText="手续费" >                                    
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PROCESS_USERID" HeaderText="最终处理人" >                                    
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PROCESS_TIME" HeaderText="最终处理时间" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OPE_TYPE" HeaderText="处理方式" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />                        
                    <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
                <div style="margin-left:10px;">
            <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
         </div>
            </ContentTemplate>
        </asp:UpdatePanel>  
        </li>
        </ul>
</div>
</div>
 <script language="javascript" type="text/javascript">

     function PopupArea(arg) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        window.location.href = "CashApplProcess.aspx?id=" + arg + "&menu=" + document.getElementById("<%=hidMenuSpan.ClientID%>").value + "&time=" + time;
    }

    function checkValid() 
    {
        var phoneNumber = document.getElementById("<%=txtPhoneNumber.ClientID%>").value;
        var startCreateDate = document.getElementById("<%=dtStartCreateDate.ClientID%>").value;
        var endCreateDate = document.getElementById("<%=dtEndCreateDate.ClientID%>").value;
        var startProcessDate = document.getElementById("<%=dtStartProcessDate.ClientID%>").value;
        var endProcessDate = document.getElementById("<%=dtEndProcessDate.ClientID%>").value;

//        if ((phoneNumber!="") && (!phoneNumber.isMobile()))
//        {
//            alert("您的手机号码格式输入不正确,必须为11位的有效号码！");
//            document.getElementById("<%=txtPhoneNumber.ClientID%>").focus();
//            return false;
//        }

//        if ((startCreateDate != "" && endCreateDate == "")||(startCreateDate == "" && endCreateDate != "")) 
//        {
//            alert("产生时间的开始时间和结束时间两个都必须输入或者两个都不输入！");
//            document.getElementById("<%=dtStartCreateDate.ClientID%>").focus();
//        }
//        if ((startCreateDate != "") && (endCreateDate != "") && (startCreateDate > endCreateDate)) 
//        {
//            alert("产生时间的开始时间必须小于结束时间！");
//            document.getElementById("<%=dtStartCreateDate.ClientID%>").focus();
//            return false;
//        }

//        if ((startProcessDate != "" && endProcessDate == "") || (startProcessDate == "" && endProcessDate != "")) {
//            alert("处理时间的开始时间和结束时间两个都必须输入或者两个都不输入！");
//            document.getElementById("<%=dtStartProcessDate.ClientID%>").focus();
//        }

//        if ((startProcessDate != "") && (endProcessDate != "") && (startProcessDate > endProcessDate)) {
//            alert("处理时间的开始时间必须小于结束时间！");
//            document.getElementById("<%=dtStartProcessDate.ClientID%>").focus();
//            return false;
//        }
        return true;
    }

    function clearText() 
    {
        document.getElementById("<%=txtPhoneNumber.ClientID%>").value = "";
        document.getElementById("<%=dtStartCreateDate.ClientID%>").value = "";
        document.getElementById("<%=dtEndCreateDate.ClientID%>").value = "";
        document.getElementById("<%=dtStartProcessDate.ClientID%>").value = "";
        document.getElementById("<%=dtEndProcessDate.ClientID%>").value = "";
        document.getElementById("<%=ddlAppMode.ClientID%>").value = "";
        document.getElementById("<%=ddlProcessStatus.ClientID%>").value = "";
        document.getElementById("<%=MessageContent.ClientID%>").innerText = "";
    }

    function checkLimit(v) {

        if (v == "0") 
        {
            var objLimit = document.getElementById("chkLimitCreateDate");
            if (objLimit.checked == true) {
                document.getElementById("<%=dtStartCreateDate.ClientID%>").disabled = true;
                document.getElementById("<%=dtEndCreateDate.ClientID%>").disabled = true;
                document.getElementById("<%=dtStartCreateDate.ClientID%>").value = "";
                document.getElementById("<%=dtEndCreateDate.ClientID%>").value = "";
            }
            else {
                document.getElementById("<%=dtStartCreateDate.ClientID%>").disabled = false;
                document.getElementById("<%=dtEndCreateDate.ClientID%>").disabled = false;
            }

        }

        if (v == "1") 
        {
            var objProcLimit = document.getElementById("chkLimitProcessDate");
            if (objProcLimit.checked == true) {
                document.getElementById("<%=dtStartProcessDate.ClientID%>").disabled = true;
                document.getElementById("<%=dtEndProcessDate.ClientID%>").disabled = true;

                document.getElementById("<%=dtStartProcessDate.ClientID%>").value = "";
                document.getElementById("<%=dtEndProcessDate.ClientID%>").value = "";
            }
            else {
                document.getElementById("<%=dtStartProcessDate.ClientID%>").disabled = false;
                document.getElementById("<%=dtEndProcessDate.ClientID%>").disabled = false;
            }        
        }        
    }
</script>
</asp:Content>

