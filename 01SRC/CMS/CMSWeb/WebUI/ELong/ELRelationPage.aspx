<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ELRelationPage.aspx.cs"  Title="供应商酒店关联" Inherits="ELRelationPage" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>

    <script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("wctHotel").value = "";
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

    function SetAClickEvent() {
        $("#MainContent_AspNetPager2 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager2 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });

        $("#MainContent_AspNetPager1 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager1 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });
    }

    function SetControlValue() {
        document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
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
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">供应商酒店关联</li>
        <li>
            <table>
                <tr>
                    <td>HVP ID：</td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel" AutoParent="ELRelationPage.aspx?Type=hotel" />
                        <%--<input name="textfield" type="text" id="txtHVPID" runat="server" style="width:200px;" maxlength="200" value=""/>--%>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdbAll" GroupName="rdbOnline" runat="server" Text="不限制" Checked="true" />
                        <asp:RadioButton ID="rdbOn" GroupName="rdbOnline" runat="server" Text="已关联酒店"/>
                        <asp:RadioButton ID="rdbOff" GroupName="rdbOnline" runat="server" Text="未关联酒店"/>
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn primary" OnClientClick="SetControlValue();BtnLoadStyle()" onclick="btnSearch_Click" Text="搜索"/>
                    </td>
                    <td></td>
                </tr>
            </table>
        </li>
      </ul>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                <ContentTemplate>
        <div class="frame02">
         <div style="margin-left:10px;"><div id="messageContent" runat="server" style="color:red;width:800px;"></div></div>
         <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
         <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" onrowcancelingedit="gridViewCSReviewList_RowCancelingEdit" 
                        onrowediting="gridViewCSReviewList_RowEditing" onrowupdating="gridViewCSReviewList_RowUpdating" CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:TemplateField HeaderText="酒店名称">
                        <ItemTemplate>
                            <asp:Label ID="lblHOTELNM" runat="server"  Text='<%# Eval("HOTELNM")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Center" Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HVP ID">
                        <ItemTemplate>
                            <asp:Label ID="lblHVPID" runat="server"  Text='<%# Eval("HVPID")%>'></asp:Label>                                    
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Center" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSORC" runat="server"  Text='<%# Eval("SORC")%>'></asp:Label>                                    
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商">
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddlSupDDp" runat="server" DataSource='<%# ddlDDpbind()%>' DataValueField="SUPID" DataTextField="SUPNM">
                                    </asp:DropDownList>
                        </EditItemTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblSUPNM" runat="server"  Text='<%# Eval("SUPNM")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Center" Width="15%" />
                    </asp:TemplateField>
                    <%-- <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                     <asp:BoundField DataField="HVPID" HeaderText="HVP ID" ><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>--%>
                     <asp:TemplateField HeaderText="供应商ID">
                        <EditItemTemplate>
                                <asp:TextBox ID="txtELIDEdit" runat="server" Text='<%# Eval("ELID")%>' Width="50%" MaxLength="40"></asp:TextBox>
                                <%--<asp:Button ID="btnEUpdate" runat="server" Text="无供应商ID" CssClass="btn primary" onclick="btnEUpdate_Click" />--%>
                        </EditItemTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lbELIDEdit" runat="server"  Text='<%# Eval("ELID")%>'></asp:Label>
                        </ItemTemplate> 
                        <ItemStyle  Width="25%" HorizontalAlign="Center" />
                        </asp:TemplateField> 
                     <asp:CommandField ShowEditButton="True" HeaderText="编辑" EditText="编辑" UpdateText="更新" CancelText="取消">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                     </asp:CommandField>
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
        <asp:HiddenField ID="hidHotel" runat="server"/>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="AspNetPager2" />
    </Triggers>
    </asp:UpdatePanel>
</div>
</asp:Content>