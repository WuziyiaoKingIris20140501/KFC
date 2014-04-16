<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelSalesManager.aspx.cs"  Title="酒店销售管理" Inherits="HotelSalesManager" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>

<style type="text/css" >
.bgDivList
{
    display: none;   
    position:absolute;
    top: 0px;
    left: 0px;
    right:0px;
    background-color: #777;
    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75)
    opacity: 0.6;
}

.popupDivList
{
    width: 900px;
    height:700px;
    margin-left:100px;
    position:absolute;
    padding: 1px;
    vertical-align: middle;
    text-align:center;
    border: solid 2px #ff8300;
    z-index: 10001;
    display: none;   
    background-color:White;
}
 </style>

    <script language="javascript" type="text/javascript">
        function SetControlStyle() {
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
        }

        function PopupArea(arg) {
            document.getElementById("<%=hidUserAccount.ClientID%>").value = arg;
            document.getElementById("<%=btnSubmit.ClientID%>").click();
        }

        //显示弹出的层
        function invokeOpenlist() {
            document.getElementById("popupDiv").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = document.body.offsetHeight + "px";
        }

        function BtnSelectHotel() {
            document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("WebAutoComplete").value;
        }

        //隐藏弹出的层
        function invokeCloselist() {
            document.getElementById("popupDiv").style.display = "none";
            document.getElementById("bgDiv").style.display = "none";
            document.getElementById("WebAutoComplete").value = "";
            document.getElementById("WebAutoComplete").text = "";
            document.getElementById("<%=detailMessageContent.ClientID%>").innerText = "";
        }

        function AddNewlist(val) {
            invokeOpenlist();
            SetWebAutoControl(val);
        }

        function SetWebAutoControl(val) {
            document.getElementById("WebAutoComplete").value = val;
            document.getElementById("WebAutoComplete").text = val;
        }
    </script>

    <div id="bgDiv" class="bgDivList"></div>
    <div id="popupDiv" class="popupDivList">
    <br />
          <div class="frame01">
          <br />
            <table align="center" width="100%">
                <tr>
                    <td style="width:5%" >姓名：</td>
                    <td style="width:15%">
                        <asp:Label ID="lbDspName" runat="server" />
                    </td>
                    <td style="width:5%">用户账号：</td>
                    <td style="width:15%">
                        <asp:Label ID="lbAccount" runat="server" />
                    </td>
                    <td style="width:5%">销售经理：</td>
                    <td style="width:15%">
                        <asp:Label ID="lbSaleManager" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td >电话：</td>
                    <td >
                        <asp:Label ID="lbTel" runat="server" />
                    </td>
                    <td >酒店数量：</td>
                    <td >
                        <asp:Label ID="lbHotelSum" runat="server" />
                    </td>
                </tr>
            </table>
          <br />
          <table align="center" width="100%">
            <tr>
                <td align="right" >选择酒店：</td>
                <td align="left" style="width:350px">
                    <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="HotelSalesManager.aspx" />
                </td>
                <td  align="left">
                    <asp:Button ID="btnAdd" CssClass="btn primary" runat="server" Text="添加酒店" OnClientClick="BtnSelectHotel()" OnClick="btnAdd_Click" />
                    <asp:HiddenField ID="hidHotelID" runat="server"/>
                    <div style="display:none"><asp:Button ID="btnSubmit" CssClass="btn primary" runat="server" Text="提交" OnClick="btnSubmit_Click" /></div>
                </td>
            </tr>
            <tr>
                <td align="right" >合同日期：</td>
                <td align="left">
                     <input id="dpStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                    <input id="dpEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                </td>
            </tr>
            <tr>
                <td  align="left" colspan="3" >
                    <div id="detailMessageContent" runat="server" style="color:red"></div>
                </td>
            </tr>
            </table>
          </div>
          <br />
          <div class="frame02">
          <asp:GridView Width="100%" HorizontalAlign="Center" ID="myGridView"
              runat="server" AutoGenerateColumns="False"  CssClass="GView_BodyCSS"
              AllowPaging="True" PageSize="12"  
              onpageindexchanging="myGridView_PageIndexChanging" 
                  onrowdeleting="myGridView_RowDeleting">
             <Columns>
                <asp:BoundField DataField="HOTELID" HeaderText="酒店ID">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称">
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <HeaderStyle Width="20%" />
                </asp:BoundField>
                 <asp:BoundField DataField="STARNM" HeaderText="星级">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle Width="5%" />
                </asp:BoundField>
                 <asp:BoundField DataField="AREANM" HeaderText="商圈">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <HeaderStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="STARTDTIME" HeaderText="合同开始日期">
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                    <HeaderStyle Width="12%" />
                </asp:BoundField>
                 <asp:BoundField DataField="ENDDTIME" HeaderText="合同结束日期">
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                    <HeaderStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="CTIME" HeaderText="设置时间">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" HeaderText="操作" DeleteText="删除" >
                    <ItemStyle HorizontalAlign="Center" Width="8%"/>
                </asp:CommandField>
            </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
              <EmptyDataTemplate>
                没有数据！
            </EmptyDataTemplate> 
        </asp:GridView>
          <br />
          <table style="width:99%;" align="center">
            <tr>
                <td align="center"  style="height:22px;">
                    <%--<input type="button" value="关闭" id="btnCLost" runat="server" name="btnClose" onclick="invokeCloselist();" />--%>
                    <asp:Button ID="btnCLose" runat="server" CssClass="btn" Text="关闭" OnClientClick="invokeCloselist()" onclick="btnCLose_Click" />
                </td>
            </tr>
        </table>
          </div>
    </div>

    <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">搜索现有销售人员</li>
        <li>
            <table>
                <tr>
                    <td align="left">
                        销售人员姓名：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSalesUser" Width="150px" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlStyle()" onclick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame02">
        <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
        <asp:GridView ID="gridViewCSAPPContenList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" PageSize="10" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="USERACCOUNT"
                            onrowdatabound="gridViewCSAPPContenList_RowDataBound" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="用户账户">
                      <ItemTemplate>
                      <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "USERACCOUNT") %>')"><%# DataBinder.Eval(Container.DataItem, "USERACCOUNT")%></a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="USERNM" HeaderText="用户名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" /></asp:BoundField>
                    <asp:BoundField DataField="SALESMANAGER" HeaderText="销售经理" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%"/></asp:BoundField>
                    <asp:BoundField DataField="TEL" HeaderText="电话" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%"/></asp:BoundField>
                    <asp:BoundField DataField="HOTELSUM" HeaderText="酒店数量" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"/></asp:BoundField>
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
    </div>
    <asp:HiddenField ID="hidUserAccount" runat="server" />
</asp:Content>