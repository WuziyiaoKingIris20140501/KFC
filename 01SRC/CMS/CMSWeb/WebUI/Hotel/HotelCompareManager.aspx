<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelCompareManager.aspx.cs"  Title="酒店数据同步检查" Inherits="HotelCompareManager" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

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
    height:100%;
    margin-left:150px;
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
            document.getElementById("<%=dvSearch.ClientID%>").style.display = "none";
            document.getElementById("<%=dvSearchUn.ClientID%>").style.display = "";
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";

            document.getElementById("<%=btnPopDiv.ClientID%>").disabled = true;

            document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
        }

        function PopupArea(arg,city) {
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
            window.location.href = "APPContentDetail.aspx?ID=" + arg + "&CITY=" + city + "&PLAT=IOS" + "&TYPE=1" + "&time=" + time;
        }

        //显示弹出的层
        function invokeOpenlist() {
            document.getElementById("popupDiv").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = document.body.offsetHeight + "px";

            //定义窗口
            //var msgObj=document.getElementById("popupDiv");
            //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
        }

        //隐藏弹出的层
        function invokeCloselist() {
            document.getElementById("popupDiv").style.display = "none";
            document.getElementById("bgDiv").style.display = "none";
        }

        function AddNewlist(val) {
            invokeOpenlist();
            SetWebAutoControl(val);
        }

        function SetWebAutoControl(val) {
            document.getElementById("WebAutoComplete").value = val;
            document.getElementById("WebAutoComplete").text = val;
        }

        function SetChkAllStyle() {
            var chkObject = document.getElementById("<%=gridViewCSAPPContenList.ClientID%>");
            if (chkObject != null) {
                var chkInput = chkObject.getElementsByTagName("input");
                for (var i = 0; i < chkInput.length; i++) {
                    if (chkInput[i].type == "checkbox") {
                        if (document.getElementById("chkAllItems").checked == true) {
                            chkInput[i].checked = true;
                        }
                        else {
                            chkInput[i].checked = false;
                        }
                    }
                }
            }

        }
    </script>
<%--<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
   --%>
    <div id="bgDiv" class="bgDivList"></div>
    <div id="popupDiv" class="popupDivList">
    <br />
          <div class="frame01">
          <br />
          <table width="100%" align="center" >
            <tr>
                <td style="width:12%;" >选择酒店：</td>
                <td style="width:28%;" >
                    <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="HotelCompareManager.aspx?Type=hotel" />
                </td>
                <td style="width:12%;" >项目类型：</td>
                <td style="width:28%;" >
                    <asp:DropDownList ID="ddpAppIgnore" CssClass="noborder_inactive" runat="server" Width="150px"/>
                </td>
            </tr>
            <tr>
                <td  align="left" colspan="4" >
                    <asp:Button ID="btnAdd" CssClass="btn primary" runat="server" Text="添加" OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td  align="left" colspan="4" >
                    <div id="detailMessageContent" runat="server" style="color:red"></div>
                </td>
            </tr>
            </table>
          </div>
          <br />
          <div class="frame02">
          <asp:GridView Width="100%" HorizontalAlign="Center" ID="myGridView"
              runat="server" AutoGenerateColumns="False"  CssClass="GView_BodyCSS"
              AllowPaging="True" PageSize="15"  
              onpageindexchanging="myGridView_PageIndexChanging" 
                  onrowdeleting="myGridView_RowDeleting">
             <Columns>
                <asp:BoundField DataField="HOTELID" HeaderText="酒店ID">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <HeaderStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称">
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <HeaderStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="TYPE" HeaderText="项目类型ID" Visible="false">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IGTYPE" HeaderText="项目类型">
                <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="CREATETM" HeaderText="设置时间">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="CREATEPR" HeaderText="设置人">
                <ItemStyle Width="15%" HorizontalAlign="Center" />
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
        <li class="title">审查范围</li>
        <li>
            <table width="98%">
<%--                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                            <ContentTemplate>--%>
                <tr>
                    <td align="left" style="width:7%">
                        选择城市：
                    </td>
                    <td align="left" style="width:17%">
                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="HotelCompareManager.aspx?Type=city" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="left" style="width:7%">
                        选择酒店：
                    </td>
                    <td align="left" style="width:17%">
                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="HotelCompareManager.aspx?Type=hotel" />
                    </td>
                </tr>
<%--                 </ContentTemplate>
                </asp:UpdatePanel>--%>
            </table>
        </li>
<%--        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server" >
                <ContentTemplate>--%>
        <li>
            <table width="98%">
                <tr>
                    <td style="width:7%"></td>
                    <td align="left" style="width:10%">
                        <div id="dvSearch" runat="server"><asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="开始检查" OnClientClick="SetControlStyle()" onclick="btnSearch_Click" /></div>
                        <div id="dvSearchUn" style="display:none" runat="server"><asp:Button ID="btnSearchUn" runat="server" CssClass="btn primary" Enabled="false" Text="正在审查请稍候" /></div>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPopDiv" runat="server" CssClass="btn primary" Text="配置免审查项目" onclick="btnPopSearch_Click" />
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
<%--        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPopDiv" />
        </Triggers>
        </asp:UpdatePanel>--%>
      </ul>
    </div>
<%--    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server" >
                <ContentTemplate>--%>
    <div class="frame01">
      <ul>
        <li class="title"><asp:Label ID="lbToDay" runat="server" Text="" /></li>
        <li>
            <asp:Label ID="lbCondition" runat="server" Text="" />
        </li>
        <li></li>
      </ul>
    </div>
<%--    </ContentTemplate>
                </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>--%>
    <div class="frame02">
        <div id="dvGridBtn" runat="server" >
        <asp:Button ID="btnHubVal" CssClass="btn primary" runat="server" Text="采用Hubs1值" OnClick="btnHubVal_Click" />
        <asp:Button ID="btnUnCheck" CssClass="btn primary" runat="server" Text="标记免审查" OnClick="btnUnCheck_Click" />
        </div>
        <asp:GridView ID="gridViewCSAPPContenList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID" onrowdeleting="gridViewCSAPPContenList_RowDeleting"
                            onrowdatabound="gridViewCSAPPContenList_RowDataBound" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate >
                            <input type="checkbox" name="chkAllItems" id="chkAllItems" onclick="SetChkAllStyle()"/>全/反选
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" name="chkItems" id="chkItems" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="HOTELID" HeaderText="酒店ID"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" /></asp:BoundField>
                    <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                    <asp:BoundField DataField="TYPENM" HeaderText="信息类型" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"/></asp:BoundField>
                    <asp:BoundField DataField="HVPVAL" HeaderText="Hotelvp值" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="34%" /></asp:BoundField>
                    <asp:BoundField DataField="HUBVAL" HeaderText="HUBS1值" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="34%" /></asp:BoundField>
                    <%--<asp:CommandField ShowDeleteButton ="True" HeaderText="操作" DeleteText="标记免审查" >
                        <ItemStyle HorizontalAlign="Center" Width="10%"/>
                    </asp:CommandField>--%>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
<%--         </ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
<asp:HiddenField ID="hidHotel" runat="server"/>
<asp:HiddenField ID="hidCity" runat="server"/>
</asp:Content>