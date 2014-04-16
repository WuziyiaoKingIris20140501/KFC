<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewLmSystemLogPageByNew.aspx.cs"  Title="LM订单历史查询" Inherits="ReviewLmSystemLogPageByNew" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtOrderID.ClientID%>").value = "";
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
        document.getElementById("<%=txtMobile.ClientID%>").value = ""; 
        document.getElementById("<%=dpInStart.ClientID%>").value = "";
        document.getElementById("<%=dpInEnd.ClientID%>").value = "";

        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("wctCity").value = "";
        document.getElementById("wctCity").text = "";

        document.getElementById("<%=ddpBookStatus.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPayStatus.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPayCode.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpTicket.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPlatForm.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpAprove.ClientID%>").selectedIndex = 0; 
    }

    function SetRbtListValue(arg) {
         var vRbtid=document.getElementById(arg);
         //得到所有radio
         var vRbtidList = vRbtid.getElementsByTagName("input");
         if (vRbtidList.length > 0)
         {
             vRbtidList[0].checked = true;
         }
     }

     function SetControlValue() {
         document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
         document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
     }

    function PopupArea(arg) {
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
        var retunValue = window.open("LmSystemLogDetailPageByNew.aspx?ID=" + arg + "&time=" + time, null, fulls);
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                 <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">查看订单操作历史</li>
        <li>
            <table>
                
                <tr>
                    <td align="right">
                        选择城市：
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddpCityList" runat="server" Width="150px" Height="25px"></asp:DropDownList>--%>
                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="ReviewLmSystemLogPageByNew.aspx?Type=city" />
                    </td>
                    <td align="right">
                        选择酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="ReviewLmSystemLogPageByNew.aspx?Type=hotel" />
                        <%--<asp:TextBox ID="txtHotelID" runat="server" Width="300px"  MaxLength="10"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        订单ID：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtOrderID" runat="server" style="width:348px;" maxlength="32" value=""/>
                    </td>
                   <td align="right">
                        登录手机号：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtMobile" runat="server" style="width:140px;" maxlength="15" value=""/>
                    </td>
                </tr>
                <tr>
                     <td align="right">
                        下单时间：
                    </td>
                    <td>
                        <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                   <td align="right">
                        入住时间：
                    </td>
                    <td>
                        <input id="dpInStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpInEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpInEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpInStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        价格代码：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPayCode" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                        <%--<asp:RadioButtonList ID="radioListPayCode" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                    </td>
                    <td align="right">
                        下单状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpBookStatus" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                        <%--<asp:RadioButtonList ID="radioListBookStatus" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                    </td>
                    <td align="right">
                        支付状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPayStatus" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                        <%--<asp:RadioButtonList ID="radioListPayStatus" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        使用优惠券：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpTicket" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                        <%--<asp:RadioButtonList ID="radioListTicket" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                    </td>
                    <td align="right">
                        &nbsp;应用平台：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPlatForm" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                        <%--<asp:RadioButtonList ID="radioListTicket" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                    </td>
                    <td align="right">
                        &nbsp;审核状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpAprove" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                        <%--<asp:RadioButtonList ID="radioListTicket" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlValue()" onclick="btnSearch_Click" />
                        <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        酒店名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtHotelName" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>--%>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>
        <div class="frame02">
             <%--<webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager>--%>
            <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                                CellPadding="4" CellSpacing="1" AllowSorting="true" OnSorting="gridViewCSReviewLmSystemLogList_Sorting" 
                                Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                                onrowdatabound="gridViewCSReviewLmSystemLogList_RowDataBound" 
                                onpageindexchanging="gridViewCSReviewLmSystemLogList_PageIndexChanging" PageSize="50"  CssClass="GView_BodyCSS">
                  <%--  <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                    BorderWidth="2px" CellPadding="4" CellSpacing="1" 
                    Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSReviewLmSystemLogList_RowDataBound" PageSize="50"  CssClass="GView_BodyCSS">--%>
                    <Columns>
                        <asp:HyperLinkField HeaderText="LM订单ID" DataNavigateUrlFields="EVENTLMID" DataNavigateUrlFormatString="LmSystemLogDetailPageByNew.aspx?ID={0}" 
                        Target="_blank" DataTextField="EVENTLMID"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                         <asp:BoundField DataField="EVENTID" HeaderText="FOG订单ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:BoundField DataField="PRICE_CODE" HeaderText="价格代码" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:HyperLinkField HeaderText="登录手机号" DataNavigateUrlFields="USERID" DataNavigateUrlFormatString="~/WebUI/UserGroup/UserDetailPage.aspx?ID={0}" 
                         Target="_blank" DataTextField="LOGINMOBILE"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                         <%--<asp:BoundField DataField="LOGINMOBILE" HeaderText="登录手机号" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>

                         <asp:BoundField DataField="BTPRICE" HeaderText="订单金额" SortExpression="BTPRICE"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:BoundField DataField="PAMOUNT" HeaderText="优惠券金额" SortExpression="PAMOUNT"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                         <asp:BoundField DataField="BOOKSTATUS" HeaderText="预订状态" SortExpression="BOOKSTATUS"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态" SortExpression="PAYSTATUS"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                         <asp:BoundField DataField="FOGCREATER" HeaderText="创建FOG订单" SortExpression="FOGCREATER"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:BoundField DataField="APPNOTIFYR" HeaderText="手机通知支付成功" SortExpression="APPNOTIFYR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:BoundField DataField="PAYNOTIFYR" HeaderText="支付中心通知" SortExpression="PAYNOTIFYR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                         <asp:BoundField DataField="FOGCANCELR" HeaderText="订单取消" SortExpression="FOGCANCELR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
        <%-- <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" 
 FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" width="99%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>--%>
        </div>
     </ContentTemplate>
    </asp:UpdatePanel>
</div>
<asp:HiddenField ID="hidHotel" runat="server"/>
<asp:HiddenField ID="hidCity" runat="server"/>
</asp:Content>