<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="OrderConfirmInfo.aspx.cs"  Title="订单确认管理" Inherits="OrderConfirmInfo" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<%@ Register src="../../UserControls/AutoCptControl.ascx" tagname="WebAutoComplete" tagprefix="ac1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
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
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function SetWebAutoControl() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("WebAutoComplete").value;
        document.getElementById("<%=hidCityID.ClientID%>").value = document.getElementById("wctCity").value;
    }

//    function ClearClickEvent() {
//        var param = ''
//        param += 'UserGroupNM:{' + document.getElementById("<%=hidHotelID.ClientID%>").value + '}'
//        $.ajax({
//            type: "POST",   //访问WebService使用Post方式请求
//            contentType: "application/json", //WebService 会返回Json类型
//            url: "OrderConfirmInfo.aspx/btnCloseClick", //调用WebService的地址和方法名称组合 ---- WsURL/方法名
//            data: "{parm:'" + param + "'}", //这里是要传递的参数，格式为 data: "{paraName:paraValue}",下面将会看到       
//            dataType: 'json',
//            success: function (result) {     //回调函数，result，返回值
//                //alert(result.d);
//            }
//        });
//    }

//    function close() {
//        alert('关闭~~');

//        var param = ''
//        param += 'UserGroupNM:{' + document.getElementById("<%=hidHotelID.ClientID%>").value + '}'
//        $.ajax({
//            type: "POST",   //访问WebService使用Post方式请求
//            contentType: "application/json", //WebService 会返回Json类型
//            url: "OrderConfirmInfo.aspx/btnCloseClick", //调用WebService的地址和方法名称组合 ---- WsURL/方法名
//            data: "{parm:'" + param + "'}", //这里是要传递的参数，格式为 data: "{paraName:paraValue}",下面将会看到       
//            dataType: 'json',
//            success: function (result) {     //回调函数，result，返回值
//                //alert(result.d);
//            }
//        });
//    }
    //    window.onbeforeunload = close;

    function PopupArea(arg) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        window.showModalDialog("OrderConfirmDetail.aspx?ID=" + arg + "&time=" + time, window, "dialogWidth=1200px;dialogHeight=680px");
        document.getElementById("<%=btnUnlock.ClientID%>").click();
    }

    function setOpenValue(returnValue) {
        document.getElementById("<%=hidOrderID.ClientID%>").value = returnValue;
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

     function ClearDateControl() {
         if (document.getElementById("<%=txtOrderID.ClientID%>").value.trim() != "") {
             document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
             document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
             document.getElementById("<%=ddpHotelConfirm.ClientID%>").selectedIndex = 0;

             var vpChkid = document.getElementById("<%=chklBStatusOther.ClientID%>");
             //得到所有radio
             var vpChkidList = vpChkid.getElementsByTagName("INPUT");
             for (var i = 0; i < vpChkidList.length; i++) {
                 vpChkidList[i].checked = true;
             }
         }
     }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">订单确认</li>
          <li>
           <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <table width="98%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        城市：
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server" Width="100px" />--%>
                        <ac1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" CTLLEN="130px" AutoType="city" AutoParent="OrderConfirmInfo.aspx?Type=city" />
                    </td>
                    <td align="right">
                        排序：
                    </td>
                    <td>
                         <asp:DropDownList ID="ddpSort" CssClass="noborder_inactive" runat="server" Width="130px" />
                    </td>
                    <td align="right">
                        下单时间：
                    </td>
                    <td colspan="3">
                        <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td><br /></td>
                </tr>
                <tr>
                    <td align="right">
                        操作状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpControlStatus" CssClass="noborder_inactive" runat="server" Width="130px" />
                    </td>
                    <td align="right">
                        传真发送：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpFaxStatus" CssClass="noborder_inactive" runat="server" Width="130px" />
                    </td>
                    <td align="right">
                        确认状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpHotelConfirm" CssClass="noborder_inactive" runat="server" Width="130px" />
                    </td>
                    <td align="right">
                        订单状态：
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddpBStatusOther" CssClass="noborder_inactive" runat="server" Width="100px" />--%>
                        <asp:CheckBoxList ID="chklBStatusOther" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                    </td>
                </tr>
                <tr>
                    <td><br /></td>
                </tr>
              </table>
              <table width="98%">
                <tr>
                    <td style="width:6px"></td>
                    <td align="right">
                        酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="OrderConfirmInfo.aspx?Type=hotel" />
                    </td>
                    <td align="right">
                        用户：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server" Width="150px" MaxLength="100" />
                    </td>
                    <td align="right">
                        订单：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderID" runat="server" Width="150px" MaxLength="100" onkeyup="ClearDateControl()" onkeypress="ClearDateControl()" onkeydown="ClearDateControl()" />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索订单" OnClientClick="SetWebAutoControl();BtnLoadStyle();" onclick="btnSearch_Click" />
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         <asp:Button ID="btnRefresh" runat="server" CssClass="btn primary" Text="刷新列表" OnClientClick="BtnLoadStyle();" onclick="btnRefresh_Click" />
                         </ContentTemplate>
                         </asp:UpdatePanel>
                         
                    </td>
                    <td>
                        <div style="display:none">
                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         <asp:Button ID="btnUnlock" runat="server" CssClass="btn primary" Text="解锁" onclick="btnUnlock_Click" />
                         </ContentTemplate>
                         </asp:UpdatePanel>
                         </div>
                    </td>
                </tr>
                <tr>
                    <td><br /></td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
          </li>
      </ul>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
    <ContentTemplate>
    <asp:HiddenField ID="hidCityID" runat="server"/>
    <asp:HiddenField ID="hidHotelID" runat="server"/>
    <asp:HiddenField ID="hidOrderID" runat="server"/>
    <div class="frame02">
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="订单号">
                      <ItemTemplate >
                        <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ORDERID") %>')"><%# DataBinder.Eval(Container.DataItem, "ORDERID") %></a>
                      </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:HyperLinkField HeaderText="订单号" DataNavigateUrlFields="ORDERID" DataNavigateUrlFormatString="OrderOperation.aspx?ID={0}" 
                     Target="_blank" DataTextField="ORDERID"><ItemStyle HorizontalAlign="Center"  Width="5%" /></asp:HyperLinkField>--%>
                     <asp:BoundField DataField="CITYNM" HeaderText="城市" ><ItemStyle HorizontalAlign="Center"  Width="10%"/></asp:BoundField>
                     <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center"  Width="15%" /></asp:BoundField>
                     <asp:BoundField DataField="GUESTNAMES" HeaderText="入住人姓名" ><ItemStyle HorizontalAlign="Center" Width="8%" /></asp:BoundField>
                     <asp:BoundField DataField="LOGINMOBILE" HeaderText="预订人电话"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                     <asp:BoundField DataField="CREATETIME" HeaderText="订单创建时间" ><ItemStyle HorizontalAlign="Center"  Width="15%"/></asp:BoundField>
                     <asp:BoundField DataField="MODIFYST" HeaderText="操作状态" ><ItemStyle HorizontalAlign="Center"  Width="7%" /></asp:BoundField>
                     <asp:BoundField DataField="MODIFYPE" HeaderText="最后操作人" ><ItemStyle HorizontalAlign="Center" Width="8%" /></asp:BoundField>
                     <asp:BoundField DataField="FAXSEND" HeaderText="传真发送"><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                     <asp:BoundField DataField="HOTELDIS" HeaderText="酒店确认状态"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                     <asp:BoundField DataField="ORDECTIME" HeaderText="订单创建时间" Visible="false"><ItemStyle HorizontalAlign="Center"/></asp:BoundField>
                     <asp:BoundField DataField="BOOKSTATUS" HeaderText="酒店确认状态" Visible="false"><ItemStyle HorizontalAlign="Center"/></asp:BoundField>
                     <asp:BoundField DataField="REDDIS" HeaderText="系统时间" Visible="false"><ItemStyle HorizontalAlign="Center"/></asp:BoundField>
                     <asp:BoundField DataField="FOLLOWUP" HeaderText="需跟进"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
          </asp:GridView>
    </div>
     <div id="background" class="pcbackground" style="display: none; "></div> 
     <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>