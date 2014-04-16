<%@ Page Title="新增用户到角色" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RoleUser.aspx.cs" Inherits="Security_Roles_RoleUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <style type="text/css">
        ul.TabBarLevel1
        {
	        list-style:none;
	        margin:0;
	        padding:0;
	        height:29px;
	        background-image:url(../../Images/tab/tabbar_level1_bk.gif); 
        }
        ul.TabBarLevel1 li
        {
	        float:left;
	        padding:0;
	        height:29px;
	        margin-right:1px;
	        background:url(../../Images/tab/tabbar_level1_slice_left_bk.gif) left top no-repeat;
        }
        ul.TabBarLevel1 li a
        {
	        display:block;
	        line-height:29px;
	        padding:0 20px;
	        color:#333;
	        text-decoration:none;
	        background:url(../../Images/tab/tabbar_level1_slice_right_bk.gif) right top no-repeat;
	        white-space: nowrap;
        }
        ul.TabBarLevel1 li.Selected
        {
	        background:url(../../Images/tab/tabbar_level1_slice_selected_left_bk.gif) left top no-repeat;
        }
        ul.TabBarLevel1 li.Selected a
        {
            padding:0 20px;
	        background:url(../../Images/tab/tabbar_level1_slice_selected_right_bk.gif) right top no-repeat;
        }

        ul.TabBarLevel1 li a:link,ul.TabBarLevel1 li a:visited
        {
            padding:0 20px;
	        color:#333;
	        text-decoration:none;
        }
        ul.TabBarLevel1 li a:hover,ul.TabBarLevel1 li a:active
        {
            padding:0 20px;
	        color:#F30;
	        text-decoration:none;
        }
        ul.TabBarLevel1 li.Selected a:link,ul.TabBarLevel1 li.Selected a:visited
        {
            padding:0 20px;
	        color:#000;
	        text-decoration:none;
        }
        ul.TabBarLevel1 li.Selected a:hover,ul.TabBarLevel1 li.Selected a:active
        {
            padding:0 20px;
	        color:#F30;
	        text-decoration:none;
        }
        div.HackBox 
        {
          padding : 5px 5px ;
          border-left: 0px solid #6697CD;/*e0e0e0  6697CD*/
          border-right: 0px solid #6697CD;
          border-bottom: 0px solid #6697CD;
          width:600px;
          height:600px;
          display:none;
        }
    </style>
        
        <table style="width:100%">
            <tr class="RowTitle"><td colspan=2 align="center"><asp:Literal Text="<%$ Resources:RoleNameLabel%>" runat="server"></asp:Literal> -> <asp:Literal Text="" ID="Literal2" runat="server"></asp:Literal></td></tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="Whatever">
            <ul class="TabBarLevel1" id="TabPage1">
                <li id="Tab1" class="Selected"><a href="#" onclick="javascript:switchTab('TabPage1','Tab1');">                    
                    <asp:Literal  ID="Literal3"  Text="<%$ Resources:IncludeMemeberLabel%>" runat="server"></asp:Literal> 
                </a></li>
                <li id="Tab2"><a href="#" onclick="javascript:switchTab('TabPage1','Tab2');">
                   <asp:Literal  ID="Literal4"  Text="<%$ Resources:UnIncludeMemberLabel%>" runat="server"></asp:Literal> 
                </a></li>
            </ul>
            <div id="cnt" style="height: 600px; text-align: center;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="dTab1" class="HackBox" style="display: block; width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table align="center" border="0" width="100%" class="Table_BodyCSS">
                                        
                                        <tr>
                                            <td>
                                                <asp:GridView ID="SmartGridView1" runat="server" AllowPaging="True"
                                                    EnableViewState="True"  AutoGenerateColumns="False" Width="100%" CssClass="GView_BodyCSS" 
                                                    EmptyDataText="No data" onpageindexchanging="SmartGridView1_PageIndexChanging" PageSize="16">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="HeadCheckBox" runat="server" onclick="yy_ClickCheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="checkitem1" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="USER_Account" HeaderText="<%$ Resources:UserAccountLabel%>" SortExpression="USER_Account">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="USER_DspName" HeaderText="<%$ Resources:UserNameLabel%>" SortExpression="USER_DspName">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="User_Tel" HeaderText="电话号码" SortExpression="User_Tel">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <RowStyle CssClass="GView_ItemCSS" />                        
                                                        <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr class="title">
                                            <td align="left" style="height: 30px">
                                                <asp:Button ID="Delete" runat="server" CssClass="btn primary" Text="<%$ Resources:RemoveLabel%>" OnClick="Delete_Click" />
                                                 <input id="btnBack" type="button" onclick="window.location.href='RoleManage.aspx'" class="btn" runat="server" value="返回" /> 
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="dTab2" class="HackBox" style="width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table align="center" border="0" width="100%" class="Table_BodyCSS">
                                        <tr>
                                            <td valign="bottom" style="height: 27px">
                                                <table>
                                                    <tr>
                                                        <td style="height: 30px">
                                                        </td>
                                                        <td style="height: 30px">                                                           
                                                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:UserAccountLabel%>"></asp:Label>
                                                            <asp:TextBox ID="txtAccount" SkinID="txtchange" runat="server"></asp:TextBox></td>
                                                        <td style="height: 30px">
                                                        </td>
                                                        <td style="height: 30px">                                                            
                                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:UserNameLabel%>"></asp:Label>
                                                            <asp:TextBox ID="txtDspName" SkinID="txtchange" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 30px">
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,FindText%>" OnClick="btnSearch_Click" OnClientClick="javascript:switchTab('TabPage1','Tab2');" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="SmartGridView2" runat="server" AllowPaging="True"   AutoGenerateColumns="False" CssClass="GView_BodyCSS" 
                                                    Width="100%" EnableViewState="True" EmptyDataText="No data" onpageindexchanging="SmartGridView2_PageIndexChanging" PageSize="16">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="HeadCheckBox2" runat="server" onclick="yy_ClickCheckAll2(this)" />
                                                            </HeaderTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="checkitem2" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="USER_Account" HeaderText="<%$ Resources:UserAccountLabel%>" SortExpression="USER_Account">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="USER_DspName" HeaderText="<%$ Resources:UserNameLabel%>" SortExpression="USER_DspName">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="User_Tel" HeaderText="电话号码" SortExpression="User_Tel">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <RowStyle CssClass="GView_ItemCSS" />                        
                                                        <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr class="title">
                                            <td align="left" style="height: 30px">
                                                <asp:Button ID="Add" runat="server" Text="<%$ Resources:AddLabel%>" CssClass="btn primary" OnClick="Add_Click" 
                                                    OnClientClick="javascript:switchTab('TabPage1','Tab2');" />
                                                     <input id="btnBack2" type="button" onclick="window.location.href='RoleManage.aspx'"  runat="server" class="btn" value="返回" /> 
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
  <script type="text/javascript" language="javascript">
      var currentTab = "";
      function LoadTab() {
          if (currentTab == "")
              currentTab = "Tab1";
          switchTab('TabPage1', currentTab);
      }
      function switchTab(tabpage, tabid) {
          currentTab = tabid;
          if (currentTab == "Tab1") {
              document.getElementById("Tab1").className = "Selected";
              document.getElementById("Tab2").className = "";
          }
          else {
              document.getElementById("Tab1").className = "";
              document.getElementById("Tab2").className = "Selected";
          }
          document.getElementById(tabid).className = "Selected";
          var dvs = document.getElementById("cnt").getElementsByTagName("div");
          for (var i = 0; i < dvs.length; i++) {
              if (dvs[i].id == ('d' + tabid)) {
                  dvs[i].style.display = 'block';
              }
              else {
                  if (dvs[i].id.toLowerCase().indexOf("dtab") != -1)
                      dvs[i].style.display = 'none';
              }
          }
      }
      function yy_ClickCheckAll(ck) {
          var checkbox = document.getElementsByTagName("input");
          for (var i = 0; i < checkbox.length; i++) {
              if (checkbox[i].id.indexOf('checkitem1') > 0 && ck.checked) {
                  checkbox[i].checked = true;
              }
              else if (checkbox[i].id.indexOf('checkitem1') > 0 && ck.checked == false) {
                  checkbox[i].checked = false;
              }
          }
      }
      function yy_ClickCheckAll2(ck) {
          var checkbox = document.getElementsByTagName("input");
          for (var i = 0; i < checkbox.length; i++) {
              if (checkbox[i].id.indexOf('checkitem2') > 0 && ck.checked) {
                  checkbox[i].checked = true;
              }
              else if (checkbox[i].id.indexOf('checkitem2') > 0 && ck.checked == false) {
                  checkbox[i].checked = false;
              }
          }
      }
    </script>
</asp:Content>

