<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="OrderAutoRefreshPage.aspx.cs"  Title="预付订单自动提示查询" Inherits="OrderAutoRefreshPage" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" type="text/javascript">
    function RemainTimeBtn() {
        var getTime = document.getElementById("<%=hidRemainSecond.ClientID%>").value;
        var remSecond = getTime - 1;
        document.getElementById("<%=hidRemainSecond.ClientID%>").value = remSecond;
        if (remSecond == 0) {
            document.getElementById("<%=btnRefush.ClientID%>").click();
        }
        if (document.getElementById("<%=hidPlay.ClientID%>").value == "1")
        {
            document.getElementById("btnPlay").click();
            document.getElementById("btnStop").click();
        }
    }
</script>

<script type="text/javascript">
    if (document.all) {
        document.write(' <OBJECT id="Player"');
        document.write(' classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"');
        document.write(' width=0 height=0 > <param name="URL" value="http://www.hotelvp.com/Images/Sent.wav" /> <param name="AutoStart" value="false" /></OBJECT>');
    }
    else {
        document.write(' <OBJECT id="Player"');
        document.write(' type="application/x-ms-wmp"');
        document.write(' src= "http://www.hotelvp.com/Images/Sent.wav" width=0 height=0> <param name="AutoStart" value="false" /></OBJECT>');
    }
     </script>

<div id="right">
    <div class="frame01">
      <ul>
        <li class="title">查看订单操作历史</li>
      </ul>
    </div>

    <div class="frame02">
        <div style="display:none;">
        <input type="button" id="btnPlay" value="Play" class="btn primary" onclick="Player.controls.play();"/>
        <input type="button" id="btnStop" value="Stop" class="btn" onclick="Player.controls.stop();"/>
        <asp:Button ID="btnRefush" runat="server" CssClass="btn primary" Text="Refresh" onclick="btnRefush_Click" />
        </div>
        <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    CssClass="GView_BodyCSS">
                    <Columns>
                        <asp:HyperLinkField HeaderText="LM订单ID" DataNavigateUrlFields="LMID" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?ID={0}" 
                        Target="_blank" DataTextField="LMID"><ItemStyle HorizontalAlign="Center" Width="5%" /></asp:HyperLinkField>
                         <asp:BoundField DataField="FOGORDERID" HeaderText="FOG订单ID" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:HyperLinkField HeaderText="登录手机号" DataNavigateUrlFields="LOGINMOBILE" DataNavigateUrlFormatString="~/WebUI/UserGroup/UserDetailPage.aspx?ID={0}&TYPE=1" 
                         Target="_blank" DataTextField="LOGINMOBILE"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:HyperLinkField>
                         <asp:BoundField DataField="PRICECODE" HeaderText="价格代码" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="18%"/></asp:BoundField>
                         <asp:BoundField DataField="CREATETIME" HeaderText="创建时间"><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                         <asp:BoundField DataField="INDATE" HeaderText="入住日期"><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                         <asp:BoundField DataField="OUTDATE" HeaderText="离店日期"><ItemStyle HorizontalAlign="Center" Width="6%" /></asp:BoundField>
                         <asp:BoundField DataField="BTPRICE" HeaderText="金额"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="TICKETAMOUNT" HeaderText="优惠券金额"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="ORDERSTATUS" HeaderText="预付订单状态"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="ORDERSTATUSOTHER" HeaderText="现付订单状态"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                         <asp:BoundField DataField="FOGRESVSTATUS" HeaderText="确认状态"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="FOGRESVTYPE" HeaderText="订单状态"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态" Visible="false"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="FOGAUDITSTATUS" HeaderText="审核状态"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="SALESMG" HeaderText="销售人员" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
    </div>
</div>
<asp:HiddenField ID="hidMaxOrderNo" runat="server"/>
<asp:HiddenField ID="hidRemainSecond" runat="server"/>
<asp:HiddenField ID="hidRefreshWav" runat="server"/>
<asp:HiddenField ID="hidPlay" runat="server"/>
</asp:Content>