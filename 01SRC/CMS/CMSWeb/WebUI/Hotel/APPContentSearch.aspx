<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="APPContentSearch.aspx.cs"  Title="APP内容审核" Inherits="APPContentSearch" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" type="text/javascript">
//    function PopupArea(arg) {
//        var obj = new Object();
//        obj.id = arg;
//        var time = new Date();
//        window.showModalDialog("APPContentDetail.aspx?ID=" + arg + "&CITY=" + document.getElementById("<%=ddpCityList.ClientID%>").options[document.getElementById("<%=ddpCityList.ClientID%>").selectedIndex].value + "&PLAT=" + document.getElementById("<%=ddpPlatform.ClientID%>").options[document.getElementById("<%=ddpPlatform.ClientID%>").selectedIndex].value + "&TYPE=" + document.getElementById("<%=ddpTypeList.ClientID%>").options[document.getElementById("<%=ddpTypeList.ClientID%>").selectedIndex].value + "&time=" + time, obj, "dialogWidth=1200px;dialogHeight=768px");
//    }

    function PopupArea(arg) {
        var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=3";    //定义弹出窗口的参数
        if (window.screen) {
            var ah = screen.availHeight - 80;
            var aw = screen.availWidth - 30;
            fulls += ",height=" + ah;
            fulls += ",innerHeight=" + ah;
            fulls += ",width=" + aw;
            fulls += ",innerWidth=" + aw;
            fulls += ",resizable"
        } else {
            fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
        }
        var time = new Date();

        window.location.href = "APPContentDetail.aspx?ID=" + arg + "&CITY=" + document.getElementById("<%=ddpCityList.ClientID%>").options[document.getElementById("<%=ddpCityList.ClientID%>").selectedIndex].value + "&PLAT=" + document.getElementById("<%=ddpPlatform.ClientID%>").options[document.getElementById("<%=ddpPlatform.ClientID%>").selectedIndex].value + "&TYPE=" + document.getElementById("<%=ddpTypeList.ClientID%>").options[document.getElementById("<%=ddpTypeList.ClientID%>").selectedIndex].value + "&VER=" + document.getElementById("<%=ddpServiceVer.ClientID%>").options[document.getElementById("<%=ddpServiceVer.ClientID%>").selectedIndex].value + "&time=" + time;
    }
    </script>
    <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">选择城市</li>
        <li>
            <table>
                <tr>
                    <td style="text-align:right;">
                        选择渠道：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpChannel" CssClass="noborder_inactive" runat="server" Enabled="false" Width="150px"/>
                    </td>
                    <td style="text-align:right;">
                        应用平台：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpPlatform" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                    <td style="text-align:right;">
                        选择版本：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpVersion" CssClass="noborder_inactive" runat="server" Enabled="false" Width="150px"/>
                    </td>
                </tr>
                <tr>
                     <td style="text-align:right;">
                        列表类型：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpTypeList" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                     <td style="text-align:right;">
                        选择城市：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                    <td style="text-align:right;">
                        Service版本：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpServiceVer" CssClass="noborder_inactive" runat="server" 
                             Width="150px" AutoPostBack="true" onselectedindexchanged="ddpServiceVer_SelectedIndexChanged"/>
                    </td>
                </tr>
            </table>
        </li>
        <li class="button">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" />
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
        <li></li>
      </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSAPPContenList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" AllowPaging="true" PageSize="50"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID" 
                            onrowdatabound="gridViewCSAPPContenList_RowDataBound" 
                            onpageindexchanging="gridViewCSAPPContenList_PageIndexChanging"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="HOTELID" HeaderText="酒店ID"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="酒店名称">
                      <ItemTemplate>
                      <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "HOTELID") %>')"><%# DataBinder.Eval(Container.DataItem, "HOTELNM")%></a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center"></ItemStyle>
                  </asp:TemplateField>

                  
               <%--     <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称"><ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" /></asp:BoundField>--%>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="封面图片">
                      <ItemTemplate>
                      <asp:Image ID="imgHotelPic" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "PICTURE") %>' Width="50%" Height="50%"/>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center"></ItemStyle>
                  </asp:TemplateField>
                    <%--<asp:BoundField DataField="PICTURE" HeaderText="封面图片" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>

                    <asp:BoundField DataField="TAGNM" HeaderText="酒店商圈" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="STARRATE" HeaderText="酒店星级" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="LOWPRICE" HeaderText="最低价格" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="LONGITUDE" HeaderText="经度" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="LATITUDE" HeaderText="纬度" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>

                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lbGRHotelNm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HOTELNM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" Visible="false" ><ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" /></asp:BoundField>--%>
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