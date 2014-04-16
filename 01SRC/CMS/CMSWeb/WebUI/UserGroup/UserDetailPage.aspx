<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="UserDetailPage.aspx.cs"  Title="用户详情" Inherits="UserDetailPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">

  <style type="text/css">
        ul.TabBarLevel1
        {
	        list-style:none;
	        margin:0;
	        padding:0;
	        height:29px;
	        background-image:url(../../Images/tab/tabbar_level1_bk.gif); 
        }
        ul.TabBarLevel1 li
        {
	        float:left;
	        padding:0;
	        height:29px;
	        margin-right:1px;
	        background:url(../../Images/tab/tabbar_level1_slice_left_bk.gif) left top no-repeat;
        }
        ul.TabBarLevel1 li a
        {
	        display:block;
	        line-height:29px;
	        padding:0 20px;
	        color:#333;
	        text-decoration:none;
	        background:url(../../Images/tab/tabbar_level1_slice_right_bk.gif) right top no-repeat;
	        white-space: nowrap;
        }
        ul.TabBarLevel1 li.Selected
        {
	        background:url(../../Images/tab/tabbar_level1_slice_selected_left_bk.gif) left top no-repeat;
        }
        ul.TabBarLevel1 li.Selected a
        {
            padding:0 20px;
	        background:url(../../Images/tab/tabbar_level1_slice_selected_right_bk.gif) right top no-repeat;
        }

        ul.TabBarLevel1 li a:link,ul.TabBarLevel1 li a:visited
        {
            padding:0 20px;
	        color:#333;
	        text-decoration:none;
        }
        ul.TabBarLevel1 li a:hover,ul.TabBarLevel1 li a:active
        {
            padding:0 20px;
	        color:#F30;
	        text-decoration:none;
        }
        ul.TabBarLevel1 li.Selected a:link,ul.TabBarLevel1 li.Selected a:visited
        {
            padding:0 20px;
	        color:#000;
	        text-decoration:none;
        }
        ul.TabBarLevel1 li.Selected a:hover,ul.TabBarLevel1 li.Selected a:active
        {
            padding:0 20px;
	        color:#F30;
	        text-decoration:none;
        }
        div.HackBox 
        {
          padding : 5px 5px ;
          border-left: 0px solid #6697CD;/*e0e0e0  6697CD*/
          border-right: 0px solid #6697CD;
          border-bottom: 0px solid #6697CD;
          width:600px;
          height:100%;
          display:none;
        }
    </style>

<script language="javascript" type="text/javascript">
    function checkInput() {
        var mobile = document.getElementById("<%=lbLoginMobile.ClientID%>").innerText;
        if (mobile == "") {
            document.getElementById("<%=detailMessageContent.ClientID%>").innerText="手机号码不能为空";
            return false;
        }
        else {
            reg = /^1[3,4,5,8]\d{9}$/gi;
            if (!reg.test(mobile)) {
                document.getElementById("<%=detailMessageContent.ClientID%>").innerText = "非法的手机号码";
                return false;
            }
        }
    }
    function PopupArea() {
        var time = new Date();
        window.location.href = "../CashBack/UserCashDetailPage.aspx?ID=" + document.getElementById("<%=hidUserID.ClientID%>").value + "&time=" + time;
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
        window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=3&RID=' + document.getElementById("<%=lbLoginMobile.ClientID%>").innerText + "&time=" + time, null, fulls);
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">用户资料</li>
        <li>
            <table width="100%">
                <tr>
                    <td style="text-align:right">用户ID：</td>
                    <td style="width:200px"><asp:Label ID="lbUserID" runat="server" Text="" /></td>
                    <td style="text-align:right">手机号码：</td>
                    <td style="width:200px"><asp:Label ID="lbLoginMobile" runat="server" Text="" />&nbsp;<input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" /></td>
                    <td style="text-align:right">验证码：</td>
                    <td><asp:Label ID="lbSignKey" runat="server" Text="" Font-Bold="true" ForeColor="Red" /></td>
                    <td><asp:Button ID="btnSearch" CssClass="btn primary" runat="server" Text="刷新验证码" OnClientClick="javascript:return checkInput();" onclick="btnSearch_Click" /></td>
                </tr>
                 <tr>
                    <td style="text-align:right">注册日期：</td>
                    <td><asp:Label ID="lbCreateDT" runat="server" Text="" /></td>
                    <td style="text-align:right">最近登录时间：</td>
                    <td><asp:Label ID="lbSignDate" runat="server" Text="" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">历史订单总数：</td>
                    <td><asp:Label ID="lbAllCount" runat="server" Text="" /></td>
                    <td style="text-align:right">成功订单数：</td>
                    <td><asp:Label ID="lbCompleCount" runat="server" Text="" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">注册渠道：</td>
                    <td><asp:Label ID="lbRegchanel" runat="server" Text="" /></td>
                    <td style="text-align:right">用户平台：</td>
                    <td><asp:Label ID="lbPlatform" runat="server" Text="" /></td>
                    <td style="text-align:right">用户版本：</td>
                    <td><asp:Label ID="lbVrsion" runat="server" Text="" /></td>
                </tr>
                <tr> 
                    <td style="text-align:right">Devicetoken：</td>
                    <td colspan="3"><asp:Label ID="lbDvtoken" runat="server" Text="" /></td>
                  <%--  <td style="text-align:right">用户发票地址：</td>
                    <td><asp:Label ID="lbInvoiceAdd" runat="server" Text="" /></td>--%>
                    <td style="text-align:right">现金账户余额：</td>
                    <td><asp:Label ID="lbUserCash" runat="server" Text="" /></td>
                    <td><a href="#" runat="server" onclick="PopupArea()">查看详情</a></td>
                </tr>
            </table>
        </li>
        <%--<li>
            <input id="btnBack" style="width:80px;height:20px" type="button" value="返回" onclick="javascript:window.history.back(-1);" />
        </li>--%>
        <li><div id="detailMessageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
   
    <div class="frame02">
          <div id="Whatever">
            <ul class="TabBarLevel1" id="TabPage1">
                <li id="Tab1" class="Selected"><a href="#" onclick="javascript:switchTab('TabPage1','Tab1');">                    
                    <asp:Literal  ID="Literal3"  Text="用户订单列表" runat="server"></asp:Literal> 
                </a></li>
                <li id="Tab2"><a href="#" onclick="javascript:switchTab('TabPage1','Tab2');">
                   <asp:Literal  ID="Literal4"  Text="用户优惠券列表" runat="server"></asp:Literal> 
                </a></li>
            </ul>
            <div id="cnt" style="height: 100%; text-align: center;">
                  <div id="dTab1" class="HackBox" style="display: block; width: 100%">
                    <asp:HiddenField ID="hidUserID" runat="server"/>
                    <asp:GridView ID="gridViewCSUserListDetail" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                                        CellPadding="4" CellSpacing="1" 
                                        Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                                        onrowdatabound="gridViewCSUserListDetail_RowDataBound" AllowSorting="true" OnSorting="gridViewCSUserListDetail_Sorting" 
                                        onpageindexchanging="gridViewCSUserListDetail_PageIndexChanging" PageSize="50"  CssClass="GView_BodyCSS">
                            <Columns>
                    
                                 <%--<asp:HyperLinkField HeaderText="LM订单ID" DataNavigateUrlFields="LMORDERID" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?ID={0}" 
                                Target="_blank" DataTextField="LMORDERID"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                                <asp:BoundField DataField="FOGORDERID" HeaderText="FOG订单ID" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="LOGINMOBILE" HeaderText="登录手机号" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="BOOKSOURCE" HeaderText="订单来源" ReadOnly="True" SortExpression="BOOKSOURCE"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="HOTELNAME" HeaderText="酒店名称" ReadOnly="True" SortExpression="HOTELNAME"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="ROOMTYPENAME" HeaderText="酒店房型" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="INDATE" HeaderText="入住日期" ReadOnly="True" SortExpression="INDATE"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="OUTDATE" HeaderText="离店日期" ReadOnly="True" SortExpression="OUTDATE"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="BOOKTOTALPRICE" HeaderText="订单金额" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="BOOKSTATUS" HeaderText="订单状态" ReadOnly="True" SortExpression="BOOKSTATUS"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态" ReadOnly="True" SortExpression="PAYSTATUS"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>

                                <asp:HyperLinkField HeaderText="LM订单ID" DataNavigateUrlFields="LMID" DataNavigateUrlFormatString="~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx?ID={0}" 
                                Target="_blank" DataTextField="LMID"><ItemStyle HorizontalAlign="Center" Width="5%" /></asp:HyperLinkField>
                                 <asp:BoundField DataField="FOGORDERID" HeaderText="FOG订单ID" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                                 <asp:BoundField DataField="LOGINMOBILE" HeaderText="登录手机号" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                 <asp:BoundField DataField="PRICECODE" HeaderText="价格代码" ><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                                 <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="21%"/></asp:BoundField>
                                 <asp:BoundField DataField="CREATETIME" HeaderText="创建时间" SortExpression="CREATETIME"><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                                 <asp:BoundField DataField="INDATE" HeaderText="入住日期" SortExpression="INDATE"><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                                 <asp:BoundField DataField="OUTDATE" HeaderText="离店日期" SortExpression="OUTDATE"><ItemStyle HorizontalAlign="Center" Width="6%" /></asp:BoundField>
                                 <asp:BoundField DataField="BTPRICE" HeaderText="金额" SortExpression="BTPRICE"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                                 <asp:BoundField DataField="TICKETAMOUNT" HeaderText="优惠券金额" SortExpression="TICKETAMOUNT"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                                 <asp:BoundField DataField="ORDERSTATUS" HeaderText="订单状态" SortExpression="ORDERSTATUS"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                                 <asp:BoundField DataField="FOGRESVSTATUS" HeaderText="确认状态" SortExpression="FOGRESVSTATUS"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                                 <asp:BoundField DataField="PAYSTATUS" HeaderText="支付状态" SortExpression="PAYSTATUS"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                                 <asp:BoundField DataField="FOGAUDITSTATUS" HeaderText="审核状态" SortExpression="FOGAUDITSTATUS"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                        </asp:GridView>
                   </div>
                    <div id="dTab2" class="HackBox" style="width: 100%">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="RowTitle"><td colspan="4" align=left><asp:Literal Text="未使用优惠券列表" ID="Literal1" runat="server"></asp:Literal> </td></tr>  
                            <tr>
                                <td align="center" class="tdcell" colspan="4">              
                                    <asp:GridView ID="gridViewNotUseTicket"  runat="server" AutoGenerateColumns="False" 
                                        BackColor="White"  AllowPaging="True" PageSize="50"  onpageindexchanging="gridViewNotUseTicket_PageIndexChanging"
                                        CssClass="GView_BodyCSS">
                                        <Columns>
                                            <asp:BoundField DataField="TICKETUSERCODE" HeaderText="优惠券码" >                      
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>                                           
                                            <asp:TemplateField HeaderText="优惠礼包">
                                                <ItemTemplate>                                                                             
                                                    <a onclick="OpenWnd('../Ticket/TicketUseDetail.aspx?packagecode=<%#Eval("PACKAGECODE").ToString() %>','优惠券详情')" href="#" ><asp:Label ID="lblpackage" runat="server"  Text='<%#Eval("PACKAGENAME").ToString()%>'></asp:Label></a>                                                   
                                                </ItemTemplate>                                    
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField> 
                                            <asp:BoundField DataField="TICKETAMT" HeaderText="优惠金额">                                    
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STARTDATE" HeaderText="最早使用日期" >                      
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>                                              
                                            <asp:BoundField DataField="ENDDATE" HeaderText="最晚使用日期" >
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>                                
                                            <asp:BoundField DataField="TICKETRULEDESC" HeaderText="优惠券使用说明">                                    
                                            <ItemStyle Width="30%" />
                                            </asp:BoundField>  
                                             <asp:BoundField DataField="STATUS" HeaderText="优惠券状态">                                    
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>        
                                        </Columns>
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                        <PagerStyle HorizontalAlign="Right" />
                                        <RowStyle CssClass="GView_ItemCSS" />                        
                                        <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                         <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="RowTitle"><td colspan="4" align=left><asp:Literal Text="已经使用/过期优惠券列表" ID="Literal2" runat="server"></asp:Literal> </td></tr>  
                            <tr>
                                <td align="center" class="tdcell" colspan="4">              
                                    <asp:GridView ID="gridViewUseTicket"  runat="server" AutoGenerateColumns="False" 
                                        BackColor="White"  AllowPaging="True" PageSize="20"  onpageindexchanging="gridViewUseTicket_PageIndexChanging"
                                        CssClass="GView_BodyCSS">
                                        <Columns>
                                            <asp:BoundField DataField="TICKETUSERCODE" HeaderText="优惠券码" >                      
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PACKAGENAME" HeaderText="优惠礼包" >                      
                                            <ItemStyle Width="20%" />
                                            </asp:BoundField>                                            
                                            <asp:BoundField DataField="TICKETAMT" HeaderText="优惠金额">                                    
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STARTDATE" HeaderText="最早使用日期" >                      
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>                                              
                                            <asp:BoundField DataField="ENDDATE" HeaderText="最晚使用日期" >
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>                                
                                            <asp:BoundField DataField="TICKETRULEDESC" HeaderText="优惠券使用说明">                                    
                                            <ItemStyle Width="20%" />
                                            </asp:BoundField>  
                                            <asp:BoundField DataField="STATUS" HeaderText="优惠券状态">                                    
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="订单号">
                                                <ItemTemplate>                                              
                                                <a onclick="OpenWnd('../DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID=<%#Eval("CNFNUM").ToString() %>','订单详情')" href="#" ><asp:Label ID="lblCNFUM" runat="server"  Text='<%#Eval("CNFNUM").ToString()%>'></asp:Label></a> 
                                                </ItemTemplate>                                    
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField> 

                                        </Columns>
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                        <PagerStyle HorizontalAlign="Right" />
                                        <RowStyle CssClass="GView_ItemCSS" />                        
                                        <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                </div>
             </div>
        </div>   
</div>
  </div>
<script type="text/javascript" language="javascript">
    var currentTab = "";
    function LoadTab() {
        if (currentTab == "")
            currentTab = "Tab1";
        switchTab('TabPage1', currentTab);
    }
    function switchTab(tabpage, tabid) {
        currentTab = tabid;

        if (currentTab == "Tab1") {
            document.getElementById("Tab1").className = "Selected";
            document.getElementById("Tab2").className = "";
        }
        else {
            document.getElementById("Tab1").className = "";
            document.getElementById("Tab2").className = "Selected";
        }

        var dvs = document.getElementById("cnt").getElementsByTagName("div");
        for (var i = 0; i < dvs.length; i++) {
            if (dvs[i].id == ('d' + tabid)) {
                dvs[i].style.display = 'block';
            }
            else {
                if (dvs[i].id.toLowerCase().indexOf("dtab") != -1)
                    dvs[i].style.display = 'none';
            }
        }
    }
    </script>
</asp:Content>