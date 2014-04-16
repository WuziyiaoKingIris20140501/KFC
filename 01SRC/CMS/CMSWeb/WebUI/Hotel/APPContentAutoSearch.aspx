<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="APPContentAutoSearch.aspx.cs"  Title="APP酒店自动审查" Inherits="APPContentAutoSearch" %>
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
    height:660px;
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
            window.location.href = "APPContentDetail.aspx?ID=" + arg + "&CITY=" + city + "&PLAT=IOS" + "&TYPE=1" + "&VER=2" + "&time=" + time;
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
    </script>

    <div id="bgDiv" class="bgDivList"></div>
    <div id="popupDiv" class="popupDivList">
    <br />
          <div class="frame01">
          <br />
          <table width="100%" align="center" >
            <tr>
                <td style="width:12%;" >选择酒店：</td>
                <td style="width:28%;" >
                    <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="APPContentAutoSearch.aspx" />
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
                <tr>
                    <td align="left" style="width:7%">
                        选择城市：
                    </td>
                    <td align="left" style="width:17%">
                        <asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server" Width="150px"/>
                    </td>
                    <td align="left" style="width:9%">
                        Service版本：
                    </td>
                     <td align="left" style="width:17%">
                        <asp:DropDownList ID="ddpServiceVer" CssClass="noborder_inactive" runat="server" 
                             Width="150px" onselectedindexchanged="ddpServiceVer_SelectedIndexChanged" AutoPostBack="true"/>
                    </td>
                    <td align="left" style="width:10%">
                        <div id="dvSearch" runat="server"><asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="开始审查" OnClientClick="SetControlStyle()" onclick="btnSearch_Click" /></div>
                        <div id="dvSearchUn" style="display:none" runat="server"><asp:Button ID="btnSearchUn" runat="server" CssClass="btn primary" Enabled="false" Text="正在审查请稍候" /></div>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPopDiv" runat="server" CssClass="btn primary" Text="配置免审核项目" onclick="btnPopSearch_Click" />
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame01">
      <ul>
        <li class="title"><asp:Label ID="lbToDay" runat="server" Text="" /></li>
      </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSAPPContenList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID" onrowdeleting="gridViewCSAPPContenList_RowDeleting"
                            onrowdatabound="gridViewCSAPPContenList_RowDataBound" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="CITYNM" HeaderText="城市名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" /></asp:BoundField>
                    <asp:BoundField DataField="HOTELID" HeaderText="酒店ID"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="酒店名称">
                      <ItemTemplate>
                      <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "HOTELID") %>','<%# DataBinder.Eval(Container.DataItem, "CITYID") %>')"><%# DataBinder.Eval(Container.DataItem, "HOTELNM")%></a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%"></ItemStyle>
                    </asp:TemplateField>

                    <%--<asp:BoundField DataField="HOTELNM" HeaderText="酒店名称"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" /></asp:BoundField>--%>
                    <asp:BoundField DataField="ERRMSG" HeaderText="错误信息" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:CommandField ShowDeleteButton ="True" HeaderText="忽略错误" DeleteText="忽略本错误" >
                        <ItemStyle HorizontalAlign="Center" Width="8%"/>
                    </asp:CommandField>
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