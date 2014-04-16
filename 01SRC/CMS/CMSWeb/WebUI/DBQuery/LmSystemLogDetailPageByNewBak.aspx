<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="LmSystemLogDetailPageByNewBak.aspx.cs"  Title="LM订单历史明细" Inherits="LmSystemLogDetailPageByNew" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {

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
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">系统相关信息</li>
        <li>
            <table width="100%">
                <tr><td align="right" style="width:15%">系统ID：</td><td align="left" style="width:35%"><asp:Label ID="lmlid" runat="server" /> </td><td align="right" style="width:15%">LM订单ID：</td><td align="left" style="width:35%"><asp:Label ID="lmlorder_num" runat="server"/></td></tr>
                <tr><td  align="right">订单来源：</td><td align="left"><asp:Label ID="lmlbook_source" runat="server" /> </td> <td align="right" style="display:none">预定类型：</td><td style="display:none"><asp:Label ID="lmlbook_type" runat="server" /> </td><td align="right">用户预定平台：</td><td><asp:Label ID="lmlapp_platform" runat="server"/></td></tr>
            </table>
            <br />
        </li>
        <li class="title">订单详情</li>
        <li>
            <table width="100%">
                <tr><td align="right" style="width:15%">FOG订单ID：</td><td align="left" style="width:35%"><asp:Label ID="lmlfog_order_num" runat="server" />&nbsp;<input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" /></td><td align="right" style="width:15%">订单创建时间：</td><td align="left" style="width:35%"><asp:Label ID="lmlcreate_time" runat="server" /> </td></tr>
                <tr><td align="right">价格代码：</td><td><asp:Label ID="lmlprice_code" runat="server"/></td><td align="right">城市ID：</td><td><asp:Label ID="lmlcity_id" runat="server"/> </td></tr>
                <tr><td align="right">酒店：</td><td colspan="3"><a id="A1" href="#" runat="server" onclick="PopupHotelArea()"><asp:Label ID="lmlhotel_id" runat="server" />-<asp:Label ID="lmlhotel_name" runat="server" /></a></td></tr>
                <tr><td align="right">房型名称：</td><td><asp:Label ID="lmlroom_type_name" runat="server"/> </td><td align="right">预定价格：</td><td><asp:Label ID="lmlbook_price" runat="server" /></td></tr>
                <tr><td align="right">优惠券金额集合：</td><td><asp:Label ID="lmlticket_amount" runat="server"/>  </td><td align="right">预订总价格：</td><td><asp:Label ID="lmlbook_total_price" runat="server"/></td></tr>
                <tr><td align="right">入住日期：</td><td><asp:Label ID="lmlin_date" runat="server"/></td><td align="right">离店日期：</td><td><asp:Label ID="lmlout_date" runat="server" /> </td></tr>
                <tr><td align="right">入住人姓名列表：</td><td><asp:Label ID="lmlguest_names" runat="server"/></td><td align="right">预订人电话：</td><td><asp:Label ID="lmlbook_person_tel" runat="server"/></td></tr>
                <tr><td align="right">联系人姓名：</td><td><asp:Label ID="lmlcontact_name" runat="server" /> </td><td align="right">联系人电话：</td><td><asp:Label ID="lmlcontact_tel" runat="server" /> </td></tr>
                <tr><td align="right">是否担保单：</td><td><asp:Label ID="lmisgua_name" runat="server" /> </td><td align="right"></td><td></td></tr>
            </table>
            <br />
        </li>
        <li class="title">订单当前状态</li>
        <li>
            <table width="100%">
                <tr><td align="right" style="width:15%">预定状态：</td><td align="left" style="width:35%"><asp:Label ID="lmlbook_status" runat="server" /> </td><td align="right" style="width:15%">支付状态：</td><td align="left" style="width:35%"><asp:Label ID="lmlpay_status" runat="server"/></td></tr>
                <tr><td align="right">FOG订单状态：</td><td align="left"><asp:Label ID="lmlfog_resvstatus" runat="server" /> </td><td align="right">FOG订单审核状态：</td><td align="left"><asp:Label ID="lmlfog_auditstatus" runat="server"/>  </td></tr>
                <tr><td align="right">FOG订单类型：</td><td align="left"><asp:Label ID="lmlfog_resvtype" runat="server" /> </td><td align="right">取消订单原因：</td><td align="left"><asp:Label ID="lmlorder_cancle_reason" runat="server"/>  </td></tr>
                <tr><td align="right"></td><td align="left"></td><td align="right">取消订单时间：</td><td align="left"><asp:Label ID="lmlorder_cancle_time" runat="server"/>  </td></tr>
            </table>
            <br />
        </li>
         <li class="title">Quick Survey</li>
        <li>
            <table width="100%">
                <tr><td align="right" style="width:15%">用户评分：</td><td align="left" style="width:35%"><asp:Label ID="lbl_survey_score" runat="server" /> </td></tr>
                <tr><td align="right">对应问题：</td><td align="left"><asp:Label ID="lbl_survey_question" runat="server" /> </td><td></td><td></td></tr>
                <tr><td align="right">用户回答：</td><td align="left"><asp:Label ID="lbl_survey_feedback" runat="server" /> </td><td></td><td></td></tr>
            </table>
            <br />
        </li>
        <li class="title">其他订单信息</li>
        <li>
            <table width="100%">
		        <tr><td align="right" style="width:15%">预订房数量：</td><td align="left" style="width:35%;"><asp:Label ID="lmlbook_room_num" runat="server"/> </td><td align="right" style="width:15%">优惠券数量集合：</td><td align="left" style="width:35%"><asp:Label ID="lmlticket_count" runat="server"/>  </td></tr></tr>
		        <tr><td align="right">用户ID：</td><td align="left"><asp:Label ID="lmluser_id" runat="server" /></td><td  align="right">锁定时间：</td><td align="left"><asp:Label ID="lmlhold_time" runat="server" /> </td></tr>
		        <tr><td align="right">订单备注：</td><td align="left"><asp:Label ID="lmlbook_remark" runat="server" /> </td><td align="right">更新时间：</td><td align="left"><asp:Label ID="lmlupdate_time" runat="server"/> </td></tr>
		        <tr><td align="right">预定房间代码：</td><td align="left"><asp:Label ID="lmlroom_type_code" runat="server"/>  </td><td  align="right">优惠券权利CODE集合：</td><td align="left"><asp:Label ID="lmlticket_usercode" runat="server"/>  </td></tr>
		        <tr><td align="right">登录手机号：</td><td align="left"><asp:Label ID="lmllogin_mobile" runat="server"/>  </td><td  align="right">延时时间分钟：</td><td align="left"><asp:Label ID="lmlovertime" runat="server"/>  </td></tr>
		        <tr><td align="right">116114订单备注：</td><td align="left"><asp:Label ID="lmlmemo1" runat="server"/>  </td><td  align="right">预付完成状态：</td><td align="left"><asp:Label ID="lmllmbar_status" runat="server"/>  </td></tr>
		        <tr><td align="right">支付方式：</td><td align="left"><asp:Label ID="lmlpay_method" runat="server" /> </td><td  align="right">支付方式描述：</td><td align="left"><asp:Label ID="lmlpay_methoddesc" runat="server" /> </td></tr>
		        <tr><td align="right">促销描述：</td><td align="left"><asp:Label ID="lmlpro_desc" runat="server"/>  </td><td  align="right">促销内容：</td><td align="left"><asp:Label ID="lmlpro_content" runat="server"/>  </td></tr>
		        <tr><td align="right">是否有免费网络：</td><td align="left"><asp:Label ID="lmlis_network" runat="server" /> </td><td  align="right">早餐数量：</td><td align="left"><asp:Label ID="lmlbreakfast_num" runat="server"/>  </td></tr>
		        <tr><td align="right">是否即时订单：</td><td align="left"><asp:Label ID="lmlis_reserve" runat="server"/>  </td><td  align="right">是否申请过发票：</td><td align="left"><asp:Label ID="lmlinvoice_flg" runat="server"/>  </td></tr>
		        <tr><td align="right">对应发票号：</td><td align="left"><asp:Label ID="lmlinvoice_code" runat="server" /> </td><td  align="right">现付订单状态：</td><td align="left"><asp:Label ID="lmlbookstatusother" runat="server"/> </td></tr>
	        </table>
        </li>
        <li><div id="detailMessageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div style="margin: 5px 14px 5px 14px;" id="dvUDHotel" runat="server">
        <div id="tabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#tabs-1">订单历史明细 </a></li>
                <li><a href="#tabs-2">订单计划明细 </a></li>
            </ul>
            <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1"  
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSSystemLogDetail_RowDataBound" 
                            onpageindexchanging="gridViewCSSystemLogDetail_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="MEMO" HeaderText="状态名称" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true"  /></asp:BoundField>
                    <asp:BoundField DataField="MSG" HeaderText="状态值" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="55%" Wrap="true"  /></asp:BoundField>
                    <asp:BoundField DataField="OPERATOR" HeaderText="操作方" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true" /></asp:BoundField>
                    <asp:BoundField DataField="EVENTTIME" HeaderText="操作日期" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true" /></asp:BoundField>
                    <asp:BoundField DataField="LAG" HeaderText="时间差(总秒数)" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="15%" Wrap="true" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
            </div>
            <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                <asp:GridView ID="gridViewPlan" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1"  
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewPlan_RowDataBound" 
                            onpageindexchanging="gridViewPlan_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="OPENM" HeaderText="操作名称" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true" /></asp:BoundField>
                    <asp:BoundField DataField="OPEMSG" HeaderText="操作内容" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="65%" Wrap="true"/></asp:BoundField>
                    <asp:BoundField DataField="OPERATOR" HeaderText="操作方" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true" /></asp:BoundField>
                    <asp:BoundField DataField="EVENTTIME" HeaderText="操作日期" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="15%" Wrap="true" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
            </div>
            </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });
    </script>
</div>
<asp:HiddenField ID="hidEventLMID" runat="server"/>
<asp:HiddenField ID="hidHotelID" runat="server"/>
</asp:Content>