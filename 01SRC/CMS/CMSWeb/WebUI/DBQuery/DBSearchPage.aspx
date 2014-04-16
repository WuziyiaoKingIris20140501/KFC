<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="DBSearchPage.aspx.cs"  Title="DB查询管理" Inherits="DBSearchPage" ValidateRequest="false" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

</script>
     <div style="margin: 5px 14px 5px 14px;" id="dvUDHotel" runat="server">
        <div id="tabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#tabs-1">Oracle </a></li>
                <li><a href="#tabs-2">Sql </a></li>
            </ul>
            <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                 <table width="100%">
                   <tr>
                    <td style="width:20%;vertical-align:top;text-align:left">
                      <div style="height:1123px; overflow:auto;margin-left:-15px;margin-top:-15px;">
                            <asp:TreeView ID="tvTableList" runat="server" Height="822px" Width="171px" 
                                onselectednodechanged="tvTableList_SelectedNodeChanged">
                              <SelectedNodeStyle BackColor="#0099FF" />
                            </asp:TreeView>
                      </div>
                    </td>
                    <td style="width:80%;vertical-align:top;text-align:left">
                            <table style="width:102%;margin-top:-15px;">
                                <tr style="width:102%;">
                                    <td style="height:120px;width:102%;background-color:#F2F2F2;">
                                        <div style="margin-left:20px;">
                                            <div style="float:left;"><asp:TextBox ID="txtManualAdd" runat="server" TextMode="MultiLine" Width="720px" Height="80px"></asp:TextBox></div>
                                            <div style="float:left;margin-left:10px;">
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="    查 询    " onclick="btnSearch_Click" /><br /><br />
                                                <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出EXCEL"  onclick="btnExport_Click"/>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="messageContent" runat="server" style="color:red;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:GridView ID="gridViewCSDBResultList" runat="server" BackColor="White" 
                                                                CellPadding="4" CellSpacing="1"  
                                                PageSize="50" AllowPaging="True"
                                                                Width="100%" EmptyDataText="没有数据"
                                                onpageindexchanging="gridViewCSDBResultList_PageIndexChanging"
                                                CssClass="GView_BodyCSS">
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <RowStyle CssClass="GView_ItemCSS" />                        
                                                    <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                            </asp:GridView>

                                            <asp:GridView ID="gridViewCSDBSearchList" runat="server" BackColor="White" 
                                                                CellPadding="4" CellSpacing="1"  
                                                PageSize="50" AllowPaging="True"
                                                                Width="100%" EmptyDataText="没有数据"
                                                onpageindexchanging="gridViewCSDBSearchList_PageIndexChanging"
                                                CssClass="GView_BodyCSS">
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <RowStyle CssClass="GView_ItemCSS" />                        
                                                    <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                            </asp:GridView>
                                        </div>
                                        <div id="save"></div>
                                    </td>
                                </tr>
                            </table>
                            <div class="frame01" style="display:none">
                              <ul>
                                <li class="title">DB查询管理</li>
                                <li>
                                    
                                    <table style="display:none;">
                                        <tr>
                                            <td style="vertical-align:top;text-align:left">
                                                
                                            </td>
                          
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rdbtnlist" runat="server" RepeatDirection="Horizontal" 
                                                    Width="362px" onselectedindexchanged="rdbtnlist_SelectedIndexChanged" AutoPostBack="true" >
                                                    <asp:ListItem Selected="True" Value="">无</asp:ListItem>
                                                    <asp:ListItem Value="order">订单</asp:ListItem>
                                                    <asp:ListItem Value="hotel">酒店</asp:ListItem>
                                                    <asp:ListItem Value="user">用户</asp:ListItem>
                                                    <%--<asp:ListItem Value="pay">支付</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="vertical-align:bottom;text-align:left">
                                                
                                            </td>
                                            <td style="vertical-align:bottom;text-align:left">
                                                 
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                                <li>
                                    
                                </li>
                              </ul>
                            </div>
                    </td>
                   </tr>
                   </table>
            </div>
            <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                <table width="100%">
                    <tr>
                        <td style="width: 20%; vertical-align: top; text-align: left">
                            <div style="height: 1123px; overflow: auto; margin-left: -15px; margin-top: -15px;">
                                <asp:TreeView ID="TreeViewSql" runat="server" Height="822px" Width="171px" OnSelectedNodeChanged="tvSqlTableList_SelectedNodeChanged">
                                    <SelectedNodeStyle BackColor="#0099FF" />
                                </asp:TreeView>
                            </div>
                        </td>
                        <td style="width: 80%; vertical-align: top; text-align: left">
                            <table style="width: 102%; margin-top: -15px;">
                                <tr style="width: 102%;">
                                    <td style="height: 120px; width: 102%; background-color: #F2F2F2;">
                                        <div style="margin-left: 20px;">
                                            <div style="float: left;">
                                                <asp:TextBox ID="txtSql" runat="server" TextMode="MultiLine" Width="720px" Height="80px"></asp:TextBox></div>
                                            <div style="float: left; margin-left: 10px;">
                                                <br />
                                                <asp:Button ID="btnSqlSearch" runat="server" CssClass="btn primary" Text="    查 询    "
                                                    OnClick="btnSqlSearch_Click" />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnSqlExport" runat="server" CssClass="btn primary" Text="导出EXCEL"
                                                    OnClick="btnSqlExport_Click" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="messageSqlContent" runat="server" style="color: red;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:GridView ID="gridViewSql" runat="server" BackColor="White" CellPadding="4" CellSpacing="1"
                                                PageSize="50" AllowPaging="True" Width="100%" EmptyDataText="没有数据" OnPageIndexChanging="gridViewSqlDBSearchList_PageIndexChanging"
                                                CssClass="GView_BodyCSS">
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <RowStyle CssClass="GView_ItemCSS" />
                                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
     </div>
     <asp:HiddenField ID="hidSelectedID" runat="server" />
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

        $("#ctabs").tabs();
    });
</script>
</asp:Content>