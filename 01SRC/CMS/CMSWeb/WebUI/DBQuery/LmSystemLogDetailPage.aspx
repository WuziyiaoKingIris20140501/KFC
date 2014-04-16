<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="LmSystemLogDetailPage.aspx.cs"  Title="LM订单历史明细" Inherits="LmSystemLogDetailPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {

    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">LM订单明细</li>
        <li>
            <table width="100%">
		        <tr><td style="width:10%">系统ID：</td><td style="width:40%"><asp:Label ID="lmlid" runat="server" /> </td><td style="width:10%">LM订单ID：</td><td style="width:40%"><asp:Label ID="lmlorder_num" runat="server"/></td></tr>
		        <tr><td>城市ID：</td><td><asp:Label ID="lmlcity_id" runat="server"/>  </td><td>酒店ID：</td><td><asp:Label ID="lmlhotel_id" runat="server" /> </td></tr>
		        <tr><td>酒店名称：</td><td><asp:Label ID="lmlhotel_name" runat="server" /> </td><td>入住日期：</td><td><asp:Label ID="lmlin_date" runat="server"/></td></tr>
		        <tr><td>预订房数量：</td><td><asp:Label ID="lmlbook_room_num" runat="server"/>  </td><td>入住人姓名列表：</td><td><asp:Label ID="lmlguest_names" runat="server"/></td></tr>
		        <tr><td>联系人姓名：</td><td><asp:Label ID="lmlcontact_name" runat="server" /> </td><td>联系人电话：</td><td><asp:Label ID="lmlcontact_tel" runat="server" /> </td></tr>
		        <tr><td>预定类型：</td><td><asp:Label ID="lmlbook_type" runat="server" /> </td><td>更新时间：</td><td><asp:Label ID="lmlupdate_time" runat="server"/>  </td></tr>
		        <tr><td>订单创建时间：</td><td><asp:Label ID="lmlcreate_time" runat="server" /> </td><td>用户ID：</td><td><asp:Label ID="lmluser_id" runat="server" /></td></tr>
		        <tr><td>预定状态：</td><td><asp:Label ID="lmlbook_status" runat="server" /> </td><td>支付状态：</td><td><asp:Label ID="lmlpay_status" runat="server"/></td></tr>
		        <tr><td>锁定时间</td><td><asp:Label ID="lmlhold_time" runat="server" /> </td><td>FOG订单ID：</td><td><asp:Label ID="lmlfog_order_num" runat="server" /></td></tr>
		        <tr><td>离店日期：</td><td><asp:Label ID="lmlout_date" runat="server" /> </td><td>订单备注：</td><td><asp:Label ID="lmlbook_remark" runat="server" /> </td></tr>
		        <tr><td>订单来源：</td><td><asp:Label ID="lmlbook_source" runat="server" /> </td><td>预定价格：</td><td><asp:Label ID="lmlbook_price" runat="server" /></td></tr>
		        <tr><td>预定房间代码：</td><td><asp:Label ID="lmlroom_type_code" runat="server"/>  </td><td>价格代码：</td><td><asp:Label ID="lmlprice_code" runat="server"/>  </td></tr>
		        <tr><td>取消订单原因：</td><td><asp:Label ID="lmlorder_cancle_reason" runat="server"/>  </td><td>预订总价格：</td><td><asp:Label ID="lmlbook_total_price" runat="server"/></td></tr>
		        <tr><td>登录手机号：</td><td><asp:Label ID="lmllogin_mobile" runat="server"/>  </td><td>延时时间分钟：</td><td><asp:Label ID="lmlovertime" runat="server"/>  </td></tr>
		        <tr><td>116114订单备注：</td><td><asp:Label ID="lmlmemo1" runat="server"/>  </td><td>预付完成状态：</td><td><asp:Label ID="lmllmbar_status" runat="server"/>  </td></tr>
		        <tr><td>支付方式：</td><td><asp:Label ID="lmlpay_method" runat="server" /> </td><td>支付方式描述：</td><td><asp:Label ID="lmlpay_methoddesc" runat="server" /> </td></tr>
		        <tr><td>促销描述：</td><td><asp:Label ID="lmlpro_desc" runat="server"/>  </td><td>促销内容：</td><td><asp:Label ID="lmlpro_content" runat="server"/>  </td></tr>
		        <tr><td>是否有免费网络：</td><td><asp:Label ID="lmlis_network" runat="server" /> </td><td>早餐数量：</td><td><asp:Label ID="lmlbreakfast_num" runat="server"/>  </td></tr>
		        <tr><td>优惠券权利CODE集合：</td><td><asp:Label ID="lmlticket_usercode" runat="server"/>  </td><td>优惠券金额集合：</td><td><asp:Label ID="lmlticket_amount" runat="server"/>  </td></tr>
		        <tr><td>优惠券数量集合：</td><td><asp:Label ID="lmlticket_count" runat="server"/>  </td><td>房型名称：</td><td><asp:Label ID="lmlroom_type_name" runat="server"/>  </td></tr>
		        <tr><td>是否保留房：</td><td><asp:Label ID="lmlis_reserve" runat="server"/>  </td><td>是否申请过发票：</td><td><asp:Label ID="lmlinvoice_flg" runat="server"/>  </td></tr>
		        <tr><td>对应发票号：</td><td><asp:Label ID="lmlinvoice_code" runat="server" /> </td><td>现付订单状态：</td><td><asp:Label ID="lmlbookstatusother" runat="server"/> </td></tr>
		        <tr><td>预订人电话：</td><td><asp:Label ID="lmlbook_person_tel" runat="server"/>  </td><td>FOG订单类型：</td><td><asp:Label ID="lmlfog_resvtype" runat="server" /> </td></tr>
		        <tr><td>FOG订单状态：</td><td><asp:Label ID="lmlfog_resvstatus" runat="server" /> </td><td>FOG订单审核状态：</td><td><asp:Label ID="lmlfog_auditstatus" runat="server"/>  </td></tr>
	        </table>
        </li>
        <li><div id="detailMessageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">LM订单历史明细</li>
        </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1"  
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSSystemLogDetail_RowDataBound" 
                            onpageindexchanging="gridViewCSSystemLogDetail_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="EVENTTYPE" HeaderText="操作类型" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="EVENTRESULT" HeaderText="操作结果" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="EVENTTIME" HeaderText="操作日期" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="LAG" HeaderText="时间差(总秒数)" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
</div>
<asp:HiddenField ID="hidEventLMID" runat="server"/>
</asp:Content>