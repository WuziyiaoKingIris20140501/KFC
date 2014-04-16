<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="APPContentDetail.aspx.cs"  Title="APP内容审核详细" Inherits="APPContentDetail" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
<%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
    <script type="text/javascript" src="http://maps.google.cn/maps/api/js?sensor=false"></script>
    <script language="javascript" type="text/javascript" charset="utf-8">
        var beaches =  <%=arrData %>;
        var map;
        function initialize() {
            var myOptions = {
                zoom: 13,
                center: new google.maps.LatLng(beaches[0][1], beaches[0][2]),    //第一个酒店所在位置
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
            setMarkers(map, beaches);
        }

        function setMarkers(map, locations) {
            for (var i = 0; i < locations.length; i++) {
                var beach = locations[i];
                var myLatLng = new google.maps.LatLng(beach[1], beach[2]);
                var marker = new google.maps.Marker({
                    position: myLatLng,
                    map: map,
                    title: beach[0],
                    zIndex: beach[3]
                });
                attachSecretMessage(marker, i, beach[0]);
            }
        }

        function attachSecretMessage(marker, number, msg) {
            var infowindow = new google.maps.InfoWindow({
                content: msg
            });
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
        }

        $().ready(function () {
            initialize();
        });
    </script>

     <br />
    <div class="frame01">
      <ul>
        <li class="button" >
            <table width="90%">
                <tr>
                    <td align="left" style="width:50%"><asp:Button ID="btnTopPre" runat="server" CssClass="btn primary" Text="上一个" onclick="btnPrevious_Click" />&nbsp;</td>
                    <td align="right" style="width:50%"><asp:Button ID="btnTopNext" runat="server" CssClass="btn primary" Text="下一个" onclick="bntNext_Click" /></td>
                </tr>
            </table>
        </li>
      </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">信息缺失检查</li>
            <li><div id="MessageContent" runat="server" style="color:red"></div></li>
        </ul>
    </div>
    <table style="width:100%">
        <tr>
            <td style="width:30%;text-align:left">
                <div style="zoom:1; border:1px #d5d5d5 solid; padding:1px; margin:0px 14px 5px 11px;height:160px;" >
                    <ul>
                        <li class="title" style="background:url(../../images/bg-frame01.gif); height:20px; padding:9px 0px 0px 10px; font-weight:bold;">酒店基本信息</li>
                        <li style="padding:10px 0px 0px 18px;">
                            <table width="90%" style="height:80%">
                                <tr>
                                    <td align="right" style="width:35%;height:35px">
                                        酒店ID：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbHotelID" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="height:35px">
                                        酒店名称：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbHotelNM" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="height:35px">
                                        所属酒店集团：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbHotelGroup" runat="server" Text="" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
            </td>
            <td style="width:70%;">
                <div style="zoom:1; border:1px #d5d5d5 solid; padding:1px; margin:0px 14px 5px 11px;height:160px;" >
                    <ul>
                        <li class="title" style="background:url(../../images/bg-frame01.gif); height:20px; padding:9px 0px 0px 10px; font-weight:bold;">酒店级促销信息</li>
                        <li style="padding:10px 0px 0px 18px;">
                            <table width="97%">
                                <tr>
                                    <td id="ProImageReview" runat="server" style="width:310px;height:109px;" rowspan="2">
                                    </td>
                                    <td align="left" valign="top" style="font-weight:bold;height:20%">
                                        <asp:Label ID="lbProDes" runat="server" Text="促销标题" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="height:80%">
                                        <asp:Label ID="lbProCont" runat="server" Text="促销内容" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li></li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    
    <div class="frame01">
        <ul>
            <li class="title">酒店图片</li>
            <li >
                <table>
                    <tr>
                        <td id="tdImage" runat="server">
                            <div id="ImageReview" runat="server" style="height:200px; width:1100px; white-space:nowrap; overflow-x:auto;"></div>
                        </td>
                    </tr>
                </table>
            </li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">酒店房型</li>
        </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSAPPContenRoomList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ROOMID" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ROOMNM" HeaderText="房型名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>
                    <asp:BoundField DataField="ROOMCODE" HeaderText="房型代码"><ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" /></asp:BoundField>
                    <asp:BoundField DataField="BEDNM" HeaderText="床型名称" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="BOOKTYPE" HeaderText="预订类型" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="BREAKFAST" HeaderText="早餐数量" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="FTPLINE" HeaderText="免费宽带" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="NETPRICE" HeaderText="网络价" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="VPPRICE" HeaderText="今夜酒店特价" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="PROTITLE" HeaderText="促销标题" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="PROCONT" HeaderText="促销内容" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">酒店地图与周边</li>
            <li>
                <table>
                    <tr>
                        <td align="left" valign="top"><div id="map_canvas" style="width:950px;height:400px;"></div></td>
                        <td align="left" valign="top">
                            <table style="font-size:12px;font-family:Verdana, Geneva, sans-serif;">
                                <tr>
                                    <td style="width:30%;vertical-align:top;">
                                        酒店地址：
                                    </td>
                                    <td style="width:70%;vertical-align:top;">
                                        <asp:Label ID="lbAddress" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;">
                                        酒店经度：
                                    </td>
                                    <td style="vertical-align:top;">
                                        <asp:Label ID="lbLongitude" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;">
                                        酒店纬度：
                                    </td>
                                    <td style="vertical-align:top;">
                                        <asp:Label ID="lbLatitude" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddpFacilitiesList" CssClass="noborder_inactive" 
                                            runat="server" Width="100%" 
                                            onselectedindexchanged="ddpFacilitiesList_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="6">地铁</asp:ListItem>
                                            <asp:ListItem Value="4">机场</asp:ListItem>
                                            <asp:ListItem Value="5">火车</asp:ListItem>
                                            <asp:ListItem Value="7">车站</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ListBox ID="lbxFacilitiesList" runat="server" CssClass="noborder_inactive" Height="290px" Width="220px"></asp:ListBox>
                                    </td>
                                </tr>
                                 
                            </table>
                        </td>
                    </tr>
                </table>
            </li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">酒店概况</li>
            <li><asp:Label ID="lbHotelDes" runat="server" Text="" /></li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">小贴士</li>
            <li><asp:Label ID="lbHotelAppr" runat="server" Text="" /></li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">酒店服务</li>
            <li><asp:Label ID="lbHotelService" runat="server" Text="" /></li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">商务设施</li>
            <li><asp:Label ID="lbBusses" runat="server" Text="" /></li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
        <ul>
            <li class="title">客服电话</li>
            <li><asp:Label ID="lbCustomTel" runat="server" Text="" /></li>
            <li></li>
        </ul>
    </div>
    <div class="frame01">
      <ul>
        <li class="button" >
            <table width="90%">
                <tr>
                    <td align="left" style="width:50%"><asp:Button ID="btnPrevious" runat="server" CssClass="btn primary" Text="上一个" onclick="btnPrevious_Click" />&nbsp;</td>
                    <td align="right" style="width:50%"><asp:Button ID="bntNext" runat="server" CssClass="btn primary" Text="下一个" onclick="bntNext_Click" /></td>
                </tr>
            </table>
        </li>
      </ul>
    </div>
    <br />
<asp:HiddenField ID="hidHotelID" runat="server"/>
<asp:HiddenField ID="hidCITYID" runat="server"/>
<asp:HiddenField ID="hidTYPEID" runat="server"/>
<asp:HiddenField ID="hidPLATID" runat="server"/>
<asp:HiddenField ID="hidVERID" runat="server"/>
</asp:Content>