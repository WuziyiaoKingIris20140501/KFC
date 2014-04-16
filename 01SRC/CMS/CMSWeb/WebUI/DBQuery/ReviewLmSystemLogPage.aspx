<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewLmSystemLogPage.aspx.cs"  Title="LM订单历史查询" Inherits="ReviewLmSystemLogPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtOrderID.ClientID%>").value = "";
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
        document.getElementById("<%=chkCreateUnTime.ClientID%>").checked = true;  
    }
    function SetchkCreateUnTime() {
        if (document.getElementById("<%=chkCreateUnTime.ClientID%>").checked == true) {
        }
        else {
            document.getElementById("<%=dpCreateStart.ClientID%>").disabled = '';
            document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = '';
        }
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
        var retunValue = window.open("LmSystemLogDetailPage.aspx?ID=" + arg + "&time=" + time, null, fulls);
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">查看订单操作历史</li>
        <li>订单ID：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input name="textfield" type="text" id="txtOrderID" runat="server" style="width:320px;" maxlength="32" value=""/>
        </li>
        <li>创建时间：&nbsp;&nbsp;&nbsp;
          <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
          <input type="checkbox" name="checkbox" id="chkCreateUnTime" runat="server" onclick="SetchkCreateUnTime()"/>
          不限制
        </li>
        <li class="button">&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" />
            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
            <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/> 
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>

    <div class="frame02">
       <%-- <div id="result"></div>--%>
            
        <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1" AllowSorting="true" OnSorting="gridViewCSReviewLmSystemLogList_Sorting" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSReviewLmSystemLogList_RowDataBound" 
                            onpageindexchanging="gridViewCSReviewLmSystemLogList_PageIndexChanging" PageSize="50"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:HyperLinkField HeaderText="LM订单ID" DataNavigateUrlFields="EVENTLMID" DataNavigateUrlFormatString="LmSystemLogDetailPage.aspx?ID={0}" 
                    Target="_blank" DataTextField="EVENTLMID"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                     <asp:BoundField DataField="EVENTID" HeaderText="FOG订单ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="LOGINMOBILE" HeaderText="登录手机号" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="BOOKSOURCE" HeaderText="订单来源" SortExpression="BOOKSOURCE"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="BOOKSTATUS" HeaderText="预订状态" SortExpression="BOOKSTATUS"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态" SortExpression="PAYSTATUS"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    

                     <asp:BoundField DataField="FOGCREATER" HeaderText="创建FOG订单" SortExpression="FOGCREATER"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="APPNOTIFYR" HeaderText="手机通知支付成功" SortExpression="APPNOTIFYR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="PAYNOTIFYR" HeaderText="支付中心通知" SortExpression="PAYNOTIFYR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="FOGUNLOCKR" HeaderText="FOG订单解HOLD" SortExpression="FOGUNLOCKR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="FOGCANCELR" HeaderText="订单取消" SortExpression="FOGCANCELR"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
</div>
</asp:Content>