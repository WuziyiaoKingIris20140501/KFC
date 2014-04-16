<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TicketUseDetail.aspx.cs" Inherits="WebUI_Ticket_TicketUseDetail" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
<div id="right">
<table align="center" border="0" width="100%" class="Table_BodyCSS"  >
     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>
     <tr class="RowTitle">
        <td colspan="2">
            <asp:Literal Text="优惠券使用详情" ID="lblTicketUseDetail" runat="server"></asp:Literal>
        </td>
     </tr>
     <tr>
        <td colspan="2">
            <table style="width:100%">
                <tr>
                    <td style="width:10%">券的面额（元）：</td>
                    <td>
                        <div >
                        <asp:DataList ID="DateList" runat="server" RepeatColumns="14"  Width="100%"  RepeatDirection="Horizontal" onitemcommand="DateList_ItemCommand" >
                                <ItemStyle HorizontalAlign="Justify" Width="35px" />
                                <ItemTemplate>               
                                <asp:Button   ID="btnDateSearch" runat="server" CssClass="btn primary"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"amount")%> '   CommandName="search"   
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"rulecode")%> ' 
                                        onclick="btnDateSearch_Click"/>
                            </ItemTemplate>
                        </asp:DataList>          
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
     <tr>
        <td  class="tdcell" style="vertical-align:top; width:50%">
        <table width="100%">
            <tr class="RowTitle"><td>优惠活动规则</td></tr>
            <tr>
                <td>
                    <asp:DetailsView ID="DetailsViewPackRule" runat="server" Height="100%" 
                    Width="100%" AutoGenerateRows="False" >
                    <Fields>
                        <asp:BoundField HeaderText="优惠活动规则ID：" HeaderStyle-CssClass="tdcell" ItemStyle-CssClass="tdcell" DataField="ID" ItemStyle-Height="20px" />
                        <asp:BoundField HeaderText="优惠活动代码："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField="PACKAGECODE" ItemStyle-Height="20px"  />
                        <asp:BoundField HeaderText="优惠活动名称：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell"  DataField="PACKAGENAME" ItemStyle-Height="20px"  />
                        <asp:BoundField HeaderText="可领用期："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField="STARTDATE_ENDDATE" ItemStyle-Height="20px"  />
                        <asp:BoundField HeaderText="优惠活动金额："  HeaderStyle-CssClass="tdcell"   ItemStyle-CssClass="tdcell" DataField ="AMOUNT" ItemStyle-Height="20px"/>
                        <asp:BoundField HeaderText="内含抵用券："   HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="TicketCount" ItemStyle-Height="20px"/>
                        <asp:BoundField HeaderText="可领用总次数："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="USERCNT" ItemStyle-Height="20px"/>
                        <asp:BoundField HeaderText="优惠使用城市："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="CITYID" ItemStyle-Height="20px"/>
                        <asp:BoundField HeaderText="用户组名称："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="USERGROUPID" ItemStyle-Height="20px"/>
                        <asp:BoundField HeaderText="优惠活动类型："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="PACKAGETYNM" ItemStyle-Height="20px"/>
                    </Fields>
                    </asp:DetailsView>            
                </td>
            </tr>
        </table>
        </td>
        <td class="tdcell" style="vertical-align:top;width:50%" >
            <table width="100%">
                <tr class="RowTitle"><td>使用规则</td></tr>
                <tr>
                    <td>
                     <asp:DetailsView ID="DetailsViewUseRule" runat="server" Height="100%" Width="100%" AutoGenerateRows="False">
                        <Fields>
                            <asp:BoundField HeaderText="使用规则号：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell"  DataField ="TICKETRULECODE" ItemStyle-Height="20px"/>
                            <asp:BoundField HeaderText="优惠使用规则名称：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="TICKETRULENAME" ItemStyle-Height="20px"/>
                            <asp:BoundField HeaderText="优惠可使用日期：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="STARTDATE_ENDDATE" ItemStyle-Height="20px"/>
                            <asp:BoundField HeaderText="优惠可使用时间：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="STARTTIME_ENDTIME" ItemStyle-Height="20px"/>
                            <asp:BoundField HeaderText="最低订单金额："  HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell"  DataField ="ORDAMT" ItemStyle-Height="20px"/>
                            <asp:BoundField HeaderText="优惠可使用城市：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell"  DataField ="CITYID" ItemStyle-Height="20px"/>                   
                            <asp:BoundField HeaderText="优惠可使用酒店：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell" DataField ="HOTELID" ItemStyle-Height="20px"/>
                            <asp:BoundField HeaderText="优惠使用描述：" HeaderStyle-CssClass="tdcell"  ItemStyle-CssClass="tdcell"  DataField ="TICKETRULEDESC" ItemStyle-Height="20px"/>
                        </Fields>
                    </asp:DetailsView>
                    </td>
                </tr>
            </table>   
            
        </td>
     </tr>
     <tr>
            <td colspan="2"  class="tdcell" style="height:100px;width:100%">
          
                <table style="width:100%;">
                    <tr class="RowTitle"><td colspan=4>券使用情况统计</td></tr>
                    <tr>
                        <td class="tdcell"  style="width:15%">总领用用户数：</td><td class="tdcell" style="width:35%"><asp:Label ID="lblAllPickUser" runat="server" Text=""></asp:Label></td>
                        <td class="tdcell" style="width:15%">总使用用户数：</td><td class="tdcell" style="width:35%"><asp:Label ID="lblAllUseUser" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="tdcell">产生订单数：预付：</td><td class="tdcell"><asp:Label ID="lblAllOrderCount" runat="server" Text=""></asp:Label></td>
                        <td class="tdcell">产生有效订单数：预付：</td><td class="tdcell"><asp:Label ID="lblAllEffectiveOrderCount" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
                 
            </td>
     </tr>
          </ContentTemplate>
    </asp:UpdatePanel>
     
     <tr>
        <td colspan="2">
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
             <tr class="RowTitle"><td colspan="4">优惠券使用详情</td></tr>
                <tr>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>
                    <td align="left" class="tdcell" colspan="4">
                        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/> 
                    </td>
                    </ContentTemplate>
                      <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                    </asp:UpdatePanel>
                </tr>
                <tr>
                    <td align="center" class="tdcell" colspan="4">
                        <asp:HiddenField ID="hidTicketCode" runat="server"/>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                        <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                        <asp:GridView ID="gridViewTicketUseInfo"  runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="gridViewTicketUseInfo_Sorting"
                            BackColor="White" PageSize="20" CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:BoundField DataField="TICKETUSERCODE" HeaderText="优惠券号码" >
                                <ItemStyle Width="5%" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="USERID" HeaderText="用户ID" >
                                <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CREATETIME" SortExpression="CREATETIME" HeaderText="领用日期" >
                                <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="已使用">
                                <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UseTime" SortExpression="UseTime" HeaderText="使用日期">
                                <ItemStyle Width="5%" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="Flag" SortExpression="Flag" HeaderText="券来源">
                                <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="订单号码" SortExpression="CNFNUM">
                                <ItemTemplate>
                                    <a onclick="OpenWnd('../DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID=<%#Eval("CNFNUM").ToString() %>','订单详情')" href="#" ><asp:Label ID="lblCNFNUM" runat="server"  Text='<%#Eval("CNFNUM").ToString()%>'></asp:Label></a>                                  
                                    </ItemTemplate>
                                <ItemStyle Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                        </asp:GridView>
                        <div style="margin-left:10px;">
                            <webdiyer:AspNetPager CssClass="paginator" UrlPaging="false" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                         </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
     </tr>
     
</table>
</div>
</asp:Content>

