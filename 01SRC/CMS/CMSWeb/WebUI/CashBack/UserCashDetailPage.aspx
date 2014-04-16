<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="UserCashDetailPage.aspx.cs"  Title="现金账户详情" Inherits="UserCashDetailPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
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
    }
</script>
    <div id="bgDiv" class="bgDivList"></div>
    <div id="popupDiv" class="popupDivList">
    <br />
        <div class="frame01">
            <ul>
            <li class="title" style="text-align:left">现金账户余额变动详情</li>
            </ul>
        </div>
        <div class="frame02">
            <asp:GridView Width="100%" HorizontalAlign="Center" ID="myGridView" EmptyDataText="没有数据"
                runat="server" AutoGenerateColumns="False"  CssClass="GView_BodyCSS"
                PageSize="10">
                <Columns>
                <asp:BoundField DataField="CHREASON" HeaderText="余额变动原因" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="CHAMOUNT" HeaderText="余额变动金额" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="CHDTIME" HeaderText="余额变动时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="CHTYPE" HeaderText="余额变动状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
            </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
            </asp:GridView>
        </div>
        <br />
          <div class="frame01">
              <ul>
                <li class="title" style="text-align:left">现金账户余额变动历史</li>
              </ul>
          </div>
          <div class="frame02">
          <asp:GridView Width="100%" HorizontalAlign="Center" ID="myHistoryGridView" AllowPaging="True" EmptyDataText="没有数据"
              runat="server" AutoGenerateColumns="False"  CssClass="GView_BodyCSS" onrowdatabound="myHistoryGridView_RowDataBound" 
              onpageindexchanging="myHistoryGridView_PageIndexChanging" PageSize="15" >
             <Columns>
                <asp:BoundField DataField="CHTYPE" HeaderText="操作" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="CHDTIME" HeaderText="更新时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="REMARK" HeaderText="备注" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="UPUSER" HeaderText="最后操作人" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
            </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
        </asp:GridView>
        <br />
          <table style="width:99%;" align="center">
            <tr>
                <td align="center"  style="height:22px;">
                    <input type="button" value="关闭" id="btnCLost" class="btn" name="btnClose" onclick="invokeCloselist();" />
                </td>
            </tr>
        </table>
        </div>
    </div>

  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">用户概述</li>
        <li>
            <table>
                <tr>
                    <td style="text-align:right">用户ID：</td>
                    <td style="width:200px"><asp:Label ID="lbUserID" runat="server" Text="" /></td>
                    <td style="text-align:right">手机号码：</td>
                    <td style="width:200px"><asp:Label ID="lbLoginMobile" runat="server" Text="" /></td>
                    <td style="text-align:right">注册时间：</td>
                    <td style="width:200px"><asp:Label ID="lbSignIn" runat="server" Text="" /></td>
                </tr>
                 <tr>
                    <td style="text-align:right">注册渠道：</td>
                    <td><asp:Label ID="lbRegchanel" runat="server" Text="" /></td>
                    <td style="text-align:right">用户平台：</td>
                    <td><asp:Label ID="lbPlatform" runat="server" Text="" /></td>
                </tr>
                 <tr>
                    <td style="text-align:right">历史订单总数：</td>
                    <td><asp:Label ID="lbAllCount" runat="server" Text="" /></td>
                    <td style="text-align:right">成功订单数：</td>
                    <td><asp:Label ID="lbCompleCount" runat="server" Text="" /></td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
     <div class="frame01">
      <ul>
        <li class="title">现金账户余额</li>
        <li>
            <table>
                <tr>
                    <td style="text-align:right">可领用金额：</td>
                    <td style="width:200px"><asp:Label ID="lbUseCash" runat="server" Text="" /></td>
                    <td style="text-align:right">领用申请中：</td>
                    <td style="width:200px"><asp:Label ID="lbCashApl" runat="server" Text="" /></td>
                    <td style="text-align:right">审核中金额：</td>
                    <td style="width:200px"><asp:Label ID="lbCashVer" runat="server" Text="" /></td>
                </tr>
            </table>
        </li>
        <li><div id="Div1" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame01">
    <ul><li class="title">现金账户明细</li></ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSUserListDetail" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSUserListDetail_RowDataBound" onrowdeleting="gridViewCSUserListDetail_RowDeleting"
                            onpageindexchanging="gridViewCSUserListDetail_PageIndexChanging" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="CHREASON" HeaderText="余额变动原因" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="CHAMOUNT" HeaderText="余额变动金额" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="25%" /></asp:BoundField>
                    <asp:BoundField DataField="CHDTIME" HeaderText="余额变动时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="20%" /></asp:BoundField>
                    <asp:BoundField DataField="CHTYPE" HeaderText="余额变动状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="15%" /></asp:BoundField>
                    <asp:CommandField ShowDeleteButton ="True" HeaderText="详情" DeleteText="查看详情" >
                        <ItemStyle HorizontalAlign="Center" Width="15%"/>
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
            </asp:GridView>
    </div>
</div>
<asp:HiddenField ID="hidUserID" runat="server"/>
<asp:HiddenField ID="hidPkey" runat="server"/>
<asp:HiddenField ID="hidSelectType" runat="server"/>
</asp:Content>