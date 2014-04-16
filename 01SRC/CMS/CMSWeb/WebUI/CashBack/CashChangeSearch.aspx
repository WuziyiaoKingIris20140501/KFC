<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CashChangeSearch.aspx.cs" Inherits="WebUI_CashBack_CashChangeSearch" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <style type="text/css" >
.bgDiv2
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

.popupDiv2
{
    width: 860px;
    height:640px;
    margin-top:50px;
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
<script type="text/javascript">
 //显示弹出的层
function invokeOpen2() 
{
    document.getElementById("popupDiv2").style.display = "block";
    //背景
    var bgObj = document.getElementById("bgDiv2");
    bgObj.style.display = "block";
    bgObj.style.width = document.body.offsetWidth + "px";
    bgObj.style.height = screen.height + "px";

    //定义窗口
    //var msgObj=document.getElementById("popupDiv");
    //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
}

//隐藏弹出的层
function invokeClose2() 
{
    document.getElementById("popupDiv2").style.display = "none";
    document.getElementById("bgDiv2").style.display = "none";
}

</script>
   <!-----------for popup--------------->
    <div id="bgDiv2" class="bgDiv2">        
    </div>
    <div id="popupDiv2" class="popupDiv2">           
        <br />
        <asp:GridView Width="99%" HorizontalAlign="Center" ID="myGridView"     
              runat="server"  CssClass="GView_BodyCSS"    
              AllowPaging="True" PageSize="20" 
            onpageindexchanging="myGridView_PageIndexChanging" >          
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
              <EmptyDataTemplate>             
                           没有用户返现与提现信息！                 
            </EmptyDataTemplate> 
        </asp:GridView><br />
        <table style="width:99%;" align="center">
            <tr>
                <td align="center"  style="height:22px;">
                   <%--<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click"/>&nbsp; &nbsp;&nbsp;--%>
                    <input type="button" value="关闭" id="Button3" name="btnClose" class="btn" onclick="invokeClose2();" />
                </td>
            </tr>
        </table>       
    </div> 
    <!---------------------------------->

<div id="right">
<div class="frame01">
        <ul>
        <li class="title">账号余额变动搜索</li>
        <li>
            <table>
                <tr>
                    <td>用户ID：</td>
                    <td colspan=3><asp:TextBox ID="txtUserID" runat="server" MaxLength="11"/></td> 
                </tr>
                <tr>
                    <td>产生时间：</td>
                    <td><input id="dtStartCreateDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00'})" runat="server"/></td>
                    <td>至：<input id="dtEndCreateDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dtStartCreateDate\')}'})" runat="server"/>
                           <input type="checkbox" name="chkLimitCreateDate" id="chkLimitCreateDate" value="1" onclick="javascript:checkLimit();"/>不限制
                    </td>
                </tr>               
                <tr>
                    <td>变动原因：</td>
                    <td><asp:DropDownList ID="ddlSourceType" CssClass="noborder_inactive" runat="server" Width="153px">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="1">返现券返现</asp:ListItem>
                        <asp:ListItem Value="2">订单返现</asp:ListItem>
                        <asp:ListItem Value="3">用户提现</asp:ListItem>
                        </asp:DropDownList>
                    </td>  
                                      
                    <td>
                        <div runat="server" id="divOrderStatus" style="display:none">变动状态：
                            <asp:DropDownList ID="DropDownList1" CssClass="noborder_inactive" runat="server" Width="153px">
                            <asp:ListItem Value="">不限制</asp:ListItem>                       
                            <asp:ListItem Value="0">审核中</asp:ListItem>                    
                            <asp:ListItem Value="1">已取消</asp:ListItem>
                            <asp:ListItem Value="2">已确认</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div runat="server" id="divTicketStatus" style="display:none">变动状态：
                            <asp:DropDownList ID="ddlStatus" CssClass="noborder_inactive" runat="server" Width="153px">
                            <asp:ListItem Value="">不限制</asp:ListItem>                       
                            <asp:ListItem Value="0">审核中</asp:ListItem>                    
                            <asp:ListItem Value="1">已取消</asp:ListItem>
                            <asp:ListItem Value="2">已确认</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div runat="server" id="divProcessStatus">处理状态：
                             <asp:DropDownList ID="ddlProcessStatus" CssClass="noborder_inactive" 
                                runat="server" Width="153px">
                            <asp:ListItem Value="">不限制</asp:ListItem>
                            <asp:ListItem Value="0">已提交</asp:ListItem>
                            <asp:ListItem Value="1">已审核</asp:ListItem>
                            <asp:ListItem Value="3">已成功</asp:ListItem>
                            <asp:ListItem Value="2">已失败</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>

                </tr> 
                <tr>
                    <td><asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn primary" 
                            onclick="btnSearch_Click" /></td>                     
                    <td><asp:Button ID="btnReset" runat="server" Text="重置"  CssClass="btn"/>&nbsp;<asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/></td>
                </tr>
            </table>
        </li>
    </ul>
</div>

<div class="frame01">
        <ul>
        <li class="title">搜索结果列表</li>
        <li>
        <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
        <asp:GridView ID="gridViewCash"  runat="server" AutoGenerateColumns="False" 
                BackColor="White" PageSize="20" CssClass="GView_BodyCSS" 
                onrowcommand="gridViewCash_RowCommand" 
                onrowdatabound="gridViewCash_RowDataBound" >
            <Columns>         
                <asp:BoundField DataField="ID" HeaderText="申请ID" Visible="False" >                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="SELTYPE" HeaderText="选择表" Visible="False" >                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="SOURCE_TYPE" HeaderText="变动来源类型" >                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SOURCE_CONTENT" HeaderText="变动来源内容" >                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField> 
                 <asp:TemplateField HeaderText="用户ID">               
                    <ItemTemplate>
                        <asp:LinkButton ID='linkBtn' runat="server"  CommandArgument='<%#Eval("SELTYPE")%>' CommandName="select" Text='<%# Eval("User_ID") %>'> </asp:LinkButton> 
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="SOURCE_AMOUNT" HeaderText="余额变动金额" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="CREATE_TIME" HeaderText="余额变动时间" >                                  
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField  DataField="STATUS"  HeaderText="余额变动状态" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="详情" Visible="false">               
                    <ItemTemplate>
                        <a href="#">详情</a>                       
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
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
        </li>
        </ul>
</div>
</div>
 <script language="javascript" type="text/javascript">
function checkLimit() {      
        var objLimit = document.getElementById("chkLimitCreateDate");
        if (objLimit.checked == true) {
            document.getElementById("<%=dtStartCreateDate.ClientID%>").disabled = true;
            document.getElementById("<%=dtEndCreateDate.ClientID%>").disabled = true;

            document.getElementById("<%=dtStartCreateDate.ClientID%>").value = "";
            document.getElementById("<%=dtEndCreateDate.ClientID%>").value = "";
        }
        else {
            document.getElementById("<%=dtStartCreateDate.ClientID%>").disabled = false;
            document.getElementById("<%=dtEndCreateDate.ClientID%>").disabled = false;
        }


    }
    </script>
</asp:Content>

