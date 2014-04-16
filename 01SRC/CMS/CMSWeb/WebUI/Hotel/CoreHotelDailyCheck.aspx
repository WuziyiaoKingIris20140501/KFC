<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="CoreHotelDailyCheck.aspx.cs"  Title="核心酒店每日检查" Inherits="CoreHotelDailyCheck" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    function SetControlStyle() {
        document.getElementById("<%=messageContent.ClientID%>").innerText = "";
        document.getElementById("<%=messageContent2.ClientID%>").innerText = "";
    }

    function SetCountVal(arg) {
        document.getElementById("<%=lbHotelCount.ClientID%>").innerText = arg;
    }

    function SetCountVal2(arg) {
        document.getElementById("<%=lbHotelCount2.ClientID%>").innerText = arg;
    }

    function SetWebAutoControl() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("WebAutoComplete").value;
    }

    function SetWebAutoControl2() {
        document.getElementById("<%=hidHotelID2.ClientID%>").value = document.getElementById("wctHotel").value;
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
    <div style="margin:5px 14px 5px 14px;">
        <div id="tabs" style="background:#FFFFFF;border: 0px solid #FFFFFF;">
	        <ul style="background:#FFFFFF;border: 0px solid #FFFFFF;">
		        <li><a href="#tabs-1">  共通核心酒店信息  </a></li>
		        <li><a href="#tabs-2">  个人核心酒店信息  </a></li>
	        </ul>
	        <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                    <div class="frame01">
                         <ul>
                    <li class="title">检查范围</li>
                    <li>
                      <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        <table>
                       <tr>
                            <td align="right" style="height:20px">
                                酒店组名称：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbHotelGroup" runat="server" Text="" />
                            </td>
                       </tr>
                       <tr>
                            <td align="right" style="height:20px">
                                共包含酒店数：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbHotelCount" runat="server" Text="" />
                            </td>
                       </tr>
                       <tr>
                            <td align="right" style="height:20px">
                                酒店组创建人：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbHotelCrePer" runat="server" Text="" />
                            </td>
                       </tr>
                       <tr>
                            <td align="right" style="height:20px">
                                添加新酒店：
                            </td>
                            <td align="left">
                                <table>
                                    <tr>
                                        <td>
                                            <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="CoreHotelDailyCheck.aspx?Type=hotel" />
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                            <asp:Button ID="btnAdd" CssClass="btn primary" runat="server" Text="添加" OnClientClick="SetWebAutoControl()" OnClick="btnAdd_Click" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                       </tr>
                        </table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </li>
                    <li>
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                        <div id="messageContent" runat="server" style="color:red"></div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                   </li>
                  </ul>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                    <div class="frame02">
                        <div style="margin:15px"><asp:Label ID="lbDTime" runat="server" Text="yyyy/mm/dd" /> 核心酒店上线情况：<br /></div>
                        <asp:GridView ID="gridViewCSAPPContenList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                            CellPadding="4" CellSpacing="1" 
                                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID" onrowdeleting="gridViewCSAPPContenList_RowDeleting"
                                            onrowdatabound="gridViewCSAPPContenList_RowDataBound" CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="HOTELID" HeaderText="酒店ID"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" /></asp:BoundField>
                                    <asp:BoundField DataField="ONLINES" HeaderText="今日上线"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="HVPMG" HeaderText="HVP负责销售"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                                    <asp:BoundField DataField="LINKPER" HeaderText="酒店联系人"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                                    <asp:BoundField DataField="LINKTEL" HeaderText="酒店联系电话"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                                    <asp:CommandField ShowDeleteButton ="True" HeaderText="操作" DeleteText="删除" >
                                        <ItemStyle HorizontalAlign="Center" Width="5%"/>
                                    </asp:CommandField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                            </asp:GridView>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
	        <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                    <div class="frame01">
                         <ul>
                    <li class="title">检查范围</li>
                    <li>
                      <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        <table>
                       <tr>
                            <td align="right" style="height:20px">
                                酒店组名称：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbHotelGroup2" runat="server" Text="" />
                            </td>
                       </tr>
                       <tr>
                            <td align="right" style="height:20px">
                                共包含酒店数：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbHotelCount2" runat="server" Text="" />
                            </td>
                       </tr>
                       <tr>
                            <td align="right" style="height:20px">
                                酒店组创建人：
                            </td>
                            <td align="left">
                                <asp:Label ID="lbHotelCrePer2" runat="server" Text="" />
                            </td>
                       </tr>
                       <tr>
                            <td align="right" style="height:20px">
                                添加新酒店：
                            </td>
                            <td align="left">
                                <table>
                                    <tr>
                                        <td>
                                            <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="CoreHotelDailyCheck.aspx?Type=hotel" />
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                            <asp:Button ID="btnAdd2" CssClass="btn primary" runat="server" Text="添加" OnClientClick="SetWebAutoControl2()" OnClick="btnAdd2_Click" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                       </tr>
                        </table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </li>
                    <li>
                        <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                        <div id="messageContent2" runat="server" style="color:red"></div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                   </li>
                  </ul>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                    <div class="frame02">
                        <div style="margin:15px"><asp:Label ID="lbDTime2" runat="server" Text="yyyy/mm/dd" /> 核心酒店上线情况：<br /></div>
                        <asp:GridView ID="gridViewCSAPPContenList2" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                            CellPadding="4" CellSpacing="1" 
                                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID" onrowdeleting="gridViewCSAPPContenList2_RowDeleting"
                                            onrowdatabound="gridViewCSAPPContenList2_RowDataBound" CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="HOTELID" HeaderText="酒店ID"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" /></asp:BoundField>
                                    <asp:BoundField DataField="ONLINES" HeaderText="今日上线"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="HVPMG" HeaderText="HVP负责销售"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                                    <asp:BoundField DataField="LINKPER" HeaderText="酒店联系人"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                                    <asp:BoundField DataField="LINKTEL" HeaderText="酒店联系电话"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                                    <asp:CommandField ShowDeleteButton ="True" HeaderText="操作" DeleteText="删除" >
                                        <ItemStyle HorizontalAlign="Center" Width="5%"/>
                                    </asp:CommandField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                            </asp:GridView>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidHotelGroupID2" runat="server"/>
    <asp:HiddenField ID="hidHotelID2" runat="server"/>
    <asp:HiddenField ID="hidHotelGroupID" runat="server"/>
    <asp:HiddenField ID="hidHotelID" runat="server"/>
    <asp:HiddenField ID="hidSelectedID" runat="server"/>
    <script type="text/javascript">
        $(function () {
            //        $("#tabs").tabs();
            var sid = document.getElementById("<%=hidSelectedID.ClientID%>").value;
            if (sid == "" || sid == "0") {
                $("#tabs").tabs();
            }
            else {
                $('#tabs').tabs({ selected: sid, select: function (event, ui) { document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index } });
            }

            $('#tabs').bind('tabsselect', function (event, ui) {
                document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index
            });

        });
</script>
</asp:Content>