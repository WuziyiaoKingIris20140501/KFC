<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelBedType.aspx.cs"  Title="酒店床型管理" Inherits="HotelBedType" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<style type="text/css" >
.bgDivList
{
    display: none;   
    position:absolute;
    top: 0px;
    left: 0px;
    right:0px;
    background-color: #777;
    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);
    opacity: 0.6;
}

.popupDivList
{
    width: 600px;
    height:250px;
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
        function PopupArea(arg) {
            document.getElementById("<%=hidBedCode.ClientID%>").value = arg;
            document.getElementById("<%=hidAddType.ClientID%>").value = '1';
            document.getElementById("<%=btnUpdate.ClientID%>").click();
        }

        function SetAddType(arg) {
            document.getElementById("<%=hidAddType.ClientID%>").value = arg;
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
    </script>

    <div id="bgDiv" class="bgDivList"></div>
    <div id="popupDiv" class="popupDivList">
    <br />
          <div class="frame01">
          <ul>
            <li class="title" style="text-align:left">保存床型</li>
            <li>
                <table>
                <tr>
                    <td align="left">床型代码：</td>
                    <td align="left">
                        <asp:TextBox ID="txtBedCode" runat="server" Width="200px" MaxLength="5" /><span style="color: #AAAAAA">&nbsp;仅限5个字符内的字母+数字</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">床型名称：</td>
                    <td align="left">
                         <asp:TextBox ID="txtBedNM" runat="server" Width="200px" MaxLength="10" /><span style="color: #AAAAAA">&nbsp;仅限10个字符内的中文字符</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">床型标签：</td>
                    <td align="left">
                         <asp:CheckBoxList ID="chkBedTag" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="8" />
                    </td>
                </tr>
                <tr>
                    <td  align="left" colspan="2" >
                        <div id="detailMessageContent" runat="server" style="color:red"></div>
                    </td>
                </tr>
                <tr>
                    <td  align="center" colspan="2" >
                        <br />
                        <asp:Button ID="btnAdd" CssClass="btn primary" runat="server" Text="保存" OnClick="btnAdd_Click" />&nbsp;
                        <input type="button" id="btnCancelRoom"  class="btn" value="取消"  onclick="invokeCloselist();" />
                    </td>
                </tr>
                </table>
            </li>
          </ul>
          </div>
    </div>

    <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">床型管理</li>
        <li>
            <table width="95%">
                <tr>
                    <td align="left" style="width:5%">
                        关键字：
                    </td>
                    <td align="left" style="width:10%">
                        <asp:TextBox ID="txtKeyWord" runat="server" Width="200px" MaxLength="50" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="查找" onclick="btnSearch_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnAddBed" runat="server" CssClass="btn primary" Text="新增床型" OnClientClick="SetAddType('0')" onclick="btnAddBed_Click" />
                        <div style="display:none"><asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" onclick="btnUpdate_Click" /></div>
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="myGridView" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" AllowPaging="true" PageSize="50" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="HOTELID" 
                            onrowdatabound="myGridView_RowDataBound" OnPageIndexChanging="myGridView_PageIndexChanging" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="床型名称">
                      <ItemTemplate>
                      <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "BEDCD") %>')"><%# DataBinder.Eval(Container.DataItem, "BEDNM")%></a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BEDCD" HeaderText="床型代码"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" /></asp:BoundField>
                    <asp:BoundField DataField="BEDTG" HeaderText="床型标签"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
            <asp:HiddenField ID="hidBedCode" runat="server" />
            <asp:HiddenField ID="hidAddType" runat="server" />
    </div>
    </div>
</asp:Content>