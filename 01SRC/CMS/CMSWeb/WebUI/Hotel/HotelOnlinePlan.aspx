<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelOnlinePlan.aspx.cs"  Title="销售计划查询" Inherits="HotelOnlinePlan" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete" TagPrefix="uc1" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        
        document.getElementById("<%=messageContent.ClientID%>").innerHTML = "";
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

    function SetHidValue() {
        document.getElementById("<%=hidCityID.ClientID%>").value = document.getElementById("wctCity").value;
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
        document.getElementById("<%=hidAreaID.ClientID%>").value = document.getElementById("wctTag").value;
    }

</script>

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
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div id="right">
    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
    <table width="100%" id="tdSearch" runat="server" >
    <tr>
        <td>
             <div class="frame01">
              <ul>
                <li class="title">询房用户管理</li>
                <li>
                    <table>
                        <tr>
                            <td align="left">
                                城市：
                            </td>
                            <td align="left">
                                 <uc1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" AutoType="city" AutoParent="HotelOnlinePlan.aspx?Type=city" />
                            </td>
                            <td align="right" style="width:100px">
                                酒店：
                            </td>
                            <td align="left">
                                 <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel" AutoParent="HotelOnlinePlan.aspx?Type=hotel" />
                            </td>
                            <td align="right" style="width:100px">
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle();SetHidValue()" onclick="btnSearch_Click" Text="搜索"/>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                商圈：
                            </td>
                            <td align="left">
                                 <uc1:WebAutoComplete ID="wctTag" runat="server" CTLID="wctTag" AutoType="tag" AutoParent="HotelOnlinePlan.aspx?Type=tag" />
                            </td>
                            <td align="right">
                                日期：
                            </td>
                            <td align="left">
                                 <input id="dpEffectDate" class="Wdate" type="text" style="width:150px" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="5">
                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                <div style="margin-left:10px;"><div id="messageContent" runat="server" style="color:red;width:400px;"></div></div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </li>
              </ul>
            </div>
        </td>
    </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
    <ContentTemplate>
            <div class="frame01">
            <ul>
                <li class="title">
                    <asp:Label ID="lbInfo" runat="server" Text="" />
                </li>
            </ul>
            </div>
            <div class="frame02">
            <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                <asp:GridView ID="gridViewCSReviewUserList" runat="server" AutoGenerateColumns="False" BackColor="White" PageSize="15" onrowdatabound="gridViewCSReviewUserList_RowDataBound" 
                                CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" CssClass="GView_BodyCSS">
                    <Columns>
                            <asp:BoundField DataField="HOTELID" HeaderText="ID" Visible = "false" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                            <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="18%" /></asp:BoundField>
                            <asp:BoundField DataField="STAR" HeaderText="星级" ><ItemStyle HorizontalAlign="Center" Width="5%" /></asp:BoundField>
                            <asp:BoundField DataField="ROOMNM" HeaderText="房型" ><ItemStyle HorizontalAlign="Center" Width="13%"/></asp:BoundField>
                            <asp:BoundField DataField="AREANM" HeaderText="商圈" ><ItemStyle HorizontalAlign="Center" Width="13%"/></asp:BoundField>
                            <asp:BoundField DataField="EFFDT" HeaderText="开卖时间" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                            <asp:BoundField DataField="PRICE" HeaderText="价格" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                            <asp:BoundField DataField="ROOMNU" HeaderText="房量" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                            <asp:BoundField DataField="LINK" HeaderText="联系人-电话" ><ItemStyle HorizontalAlign="Center" Width="13%"/></asp:BoundField>
                            <asp:BoundField DataField="SALES" HeaderText="销售" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                            <asp:BoundField DataField="ROOMCL" HeaderText="房控" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                            <asp:BoundField DataField="REMARK" HeaderText="备注" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
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
            </div>
            <div id="background" class="pcbackground" style="display: none; "></div>
            <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div>
            <asp:HiddenField ID="hidCityID" runat="server"/>
            <asp:HiddenField ID="hidHotelID" runat="server"/>
            <asp:HiddenField ID="hidAreaID" runat="server"/>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>