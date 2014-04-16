<%@ Page Title="菜单管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuManage.aspx.cs" Inherits="Security_Permissions_MenuManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  
<!-----------for popup--------------->
    <div id="bgDiv" class="bgDiv">        
    </div>
    <div id="popupDiv" class="popupDiv">  
     <div class="frame01">
      <ul>
        <li>
          <table width="100%" align="center" id="Table1" class="Table_BodyCSS">     
            <tr>
                <td align="right" width="30%" class="tdcell">
                    <asp:Label runat="server" ID="lblMenuName"  Text="<%$Resources:MenuNameColon%>"></asp:Label></td>
                <td  align="left" class="tdcell" >
                    <asp:TextBox ID="txtMenuName" runat="server" Width="60%" AutoCompleteType="Disabled" ></asp:TextBox><font color="#ff0000">*</font>
                </td>
            </tr>           
            <tr >
                <td align="right" width="30%" class="tdcell"><asp:Label runat="server" ID="lblUrlPath"  Text="<%$Resources:UrlPathColon%>"></asp:Label></td>
                <td  align="left" class="tdcell"><asp:TextBox  ID="txtUrlPath" runat="server" Width="60%" AutoCompleteType="Disabled" /></td>
            </tr>
            <tr >
                <td align="right" width="30%" class="tdcell"><asp:Label runat="server" ID="lblParentID"  Text="<%$Resources:ParentIDColon%>"></asp:Label></td>
                <td  align="left">
                    <asp:DropDownList ID="ddlParentID" runat="server" Width="60%" BackColor="White" ></asp:DropDownList><font color="#ff0000">*</font>
                </td>
            </tr>
            <tr >
                <td align="right" width="30%" class="tdcell"><asp:Label runat="server" ID="lblOrderID"  Text="<%$Resources:OrderColon%>"></asp:Label></td>
                <td align="left" class="tdcell" >
                    <asp:TextBox  ID="txtOrderID" runat="server" Width="60%" AutoCompleteType="Disabled" /><font color="#ff0000">*</font>
                </td>
            </tr>
            <tr >
                <td align="right" width="30%" class="tdcell"><asp:Label runat="server" ID="lblLevel" Text="<%$Resources:LevelColon%>"></asp:Label></td>
                <td  align="left" class="tdcell"><asp:DropDownList ID="ddlLevel" runat="server" Width="60%">
                    <asp:ListItem Value="1">一级</asp:ListItem>
                    <asp:ListItem Value="2">二级</asp:ListItem>
                    </asp:DropDownList>
                    <font color="#ff0000">*</font>
                </td>
            </tr>

            <tr >
                <td align="right" width="30%" class="tdcell"><asp:Label runat="server" ID="Label1" Text="<%$Resources:IsLimitColon%>"></asp:Label></td>
                <td  align="left" class="tdcell">
                    <asp:RadioButtonList ID="radioListLimit" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="<%$Resources:ddlLimitText %>" Selected=True></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$Resources:ddlNoLimitText %>"></asp:ListItem>                    
                    </asp:RadioButtonList>                                     
                </td>
            </tr>
              <tr >
                <td align="right" width="30%" class="tdcell"><asp:Label runat="server" ID="Label2" Text="<%$Resources:IsDisplayColon%>"></asp:Label></td>
                <td  align="left" class="tdcell">
                    <asp:RadioButtonList ID="RadioListDisplay" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="<%$Resources:ddlDisplayText %>" Selected=True></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$Resources:ddlNoDisplayText %>"></asp:ListItem>                    
                    </asp:RadioButtonList>                                    
                </td>
            </tr>
             <tr>
                <td colspan=2 align="center" >           
                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="<%$Resources:MyGlobal,SaveText%>" OnClientClick="return checkEmpty();" onclick="btnSave_Click" />&nbsp; &nbsp;&nbsp;
                    <input type="button" value="<%=strClose %>" id="btnClose" name="btnClose" class="btn" onclick="clearText();invokeClose();" /></td>
            </tr>
        </table>
        </li>
          </ul>
        </div>
    </div> 
    <!---------------------------------->
     <div>
      <div id="right">
    <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="<%$Resources:MenuManageTitle%>" ID="lbAdviceTitle" runat="server"></asp:Literal> </li>
        <li>
    <table cellpadding="0" cellspacing="0" align="center" width="100%">
            <tr>
                <td align="left">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <input runat="server" type="button" id="btnAdd" class="btn primary" value="<%$ Resources:MyGlobal,NewText %>" onclick="AddNew()" />&nbsp;&nbsp;                               
                            </td>                         
                        </tr>
                    </table>
                </td>
            </tr>        
            <tr>
                <td class="tdcell">
                    <asp:GridView ID="MenuGridView" runat="server" CssClass="GView_BodyCSS" 
                        AllowSorting="True" Width="100%"
                        AutoGenerateColumns="False" OnRowCreated="MenuGridView_RowCreated"
                        PageSize="15" onrowcancelingedit="MenuGridView_RowCancelingEdit" 
                        onrowediting="MenuGridView_RowEditing" 
                        onrowupdating="MenuGridView_RowUpdating" DataKeyNames="Menu_ID" 
                        onrowdeleting="MenuGridView_RowDeleting">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="HeadCheckBox" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="checkitem" runat="server"></asp:CheckBox>
                                    <input type="hidden" value='<%#DataBinder.Eval(Container.DataItem, "Menu_ID")%>' name="guid" />
                                </ItemTemplate>
                                <ItemStyle Width="1%" HorizontalAlign="Center"/>
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:TemplateField>
                         
                            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                   <asp:Label ID="lblMenuID" runat="server"  Text='<%# Eval("Menu_ID")%>'></asp:Label>                                    
                               </ItemTemplate>
                               <ItemStyle  HorizontalAlign="Center" Width="3%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:ParentIDLabel%>" HeaderStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                     <asp:DropDownList ID="ddlParentIDEdit" runat="server" Width="98%" 
                                         BackColor="White" DataSourceID="ObjectDataSource1" DataTextField="Menu_Name" 
                                         DataValueField="Menu_ID" SelectedValue='<%# Eval("Parent_MenuId") %>' ></asp:DropDownList>
                                     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                         OldValuesParameterFormatString="original_{0}" SelectMethod="GetFirstMenu" 
                                         TypeName="Menu"></asp:ObjectDataSource>
                               </EditItemTemplate> 
                               <ItemTemplate>
                                   <asp:Label ID="lblParentMenuIdEdit" runat="server"  Text='<%# Eval("Parent_MenuNM")%>'></asp:Label>                                    
                               </ItemTemplate>
                                <ItemStyle  Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="<%$Resources:MenuNameLabel%>" HeaderStyle-HorizontalAlign="Center">
                               <EditItemTemplate>
                                     <asp:TextBox ID="txtMenuNameEdit" runat="server" Text='<%# Eval("Menu_Name")%>' Width="98%"></asp:TextBox>
                               </EditItemTemplate> 
                               <ItemTemplate>
                                   <asp:Label ID="lblMenuNameEdit" runat="server"  Text='<%# Eval("Menu_Name")%>'></asp:Label>                                    
                               </ItemTemplate> 
                               <ItemStyle  Width="15%" HorizontalAlign="Center" />
                             </asp:TemplateField> 

                             <asp:TemplateField HeaderText="<%$Resources:UrlPathLabel%>" HeaderStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                  <asp:TextBox ID="txtMenuUrlEdit" runat="server" Text='<%# Eval("Menu_Url")%>' Width="98%" ></asp:TextBox>
                               </EditItemTemplate>  
                               <ItemTemplate>
                                   <asp:Label ID="lblMenuUrlEdit" runat="server"  Text='<%# Eval("Menu_Url")%>'></asp:Label>
                               </ItemTemplate>
                                <ItemStyle Width="38%"  HorizontalAlign="Center" />
                             </asp:TemplateField>                           

                            <asp:TemplateField HeaderText="<%$Resources:MenuNumberLabel%>" HeaderStyle-HorizontalAlign="Center">
                               <EditItemTemplate>
                                  <asp:TextBox ID="txtMenuOrderIDEdit" runat="server" Text='<%# Eval("Menu_OrderID")%>' Width="90%" ></asp:TextBox>
                               </EditItemTemplate>   
                              <ItemTemplate>
                                   <asp:Label ID="lblMenuOrderIDEidt" runat="server"  Text='<%# Eval("Menu_OrderID")%>'></asp:Label>
                               </ItemTemplate>
                               <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:LevelLabel%>" HeaderStyle-HorizontalAlign="Center">                            
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlLevelEdit" runat="server" Width="98%" 
                                        SelectedValue='<%# Eval("Menu_Level") %>'>
                                        <asp:ListItem Value="1">一级</asp:ListItem>
                                        <asp:ListItem Value="2">二级</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMenuLevelEdit" runat="server" Text='<%# Eval("Menu_Level").ToString()=="1"?"一级":"二级" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:IsLimitLabel%>" HeaderStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="radioListLimitEdit" runat="server" 
                                        SelectedValue='<%# Eval("Menu_Limit") %>'>
                                        <asp:ListItem Value="1" Text="<%$Resources:ddlLimitText %>"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="<%$Resources:ddlNoLimitText %>"></asp:ListItem>                    
                                    </asp:RadioButtonList>  
                                </EditItemTemplate>

                                <ItemTemplate>
                                       <asp:Label ID="lblMenuLimitEdit" runat="server" Text='<%# Eval("Menu_Limit").ToString()=="True" || Eval("Menu_Limit").ToString()=="1" ?STR_YES:STR_NO %>'></asp:Label>
                                </ItemTemplate>  
                                                            
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:IsDisplayLabel%>" HeaderStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="RadioListDisplayEdit" runat="server" 
                                        SelectedValue='<%# Eval("Menu_Show") %>'>
                                        <asp:ListItem Value="1" Text="<%$Resources:ddlDisplayText %>"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="<%$Resources:ddlNoDisplayText %>"></asp:ListItem>                    
                                    </asp:RadioButtonList>
                                </EditItemTemplate>
                                <ItemTemplate>                                   
                                         <asp:Label ID="lblMenuDisplayEdit" runat="server" Text='<%# Eval("Menu_Show").ToString()=="1"?STR_YES:STR_NO %>'></asp:Label>
                                </ItemTemplate>                               
                                <ItemStyle Width="4%"  HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" >
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" >
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:CommandField>
                        </Columns>

                       <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />                        
                        <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />

                    </asp:GridView>
                </td>
            </tr>          
        </table>
        </li>
          </ul>
        </div>
    </div>
    </div>
    <script language="javascript " type="text/javascript">
        function AddNew()
        {
             invokeOpen();
        }       
       
        //清空
        function clearText() {
            document.getElementById("<%=txtMenuName.ClientID %>").value = "";
            document.getElementById("<%=txtUrlPath.ClientID %>").value = "";
            document.getElementById("<%=ddlParentID.ClientID %>").value = "";
            document.getElementById("<%=txtOrderID.ClientID %>").value = "";
            document.getElementById("<%=ddlLevel.ClientID %>").value = "";

        }

        //判断是否输入为空
        function checkEmpty() 
        {
            var menuName = document.getElementById("<%=txtMenuName.ClientID %>").value;          
            var parentID = document.getElementById("<%=ddlParentID.ClientID %>").value;
            var orderID = document.getElementById("<%=txtOrderID.ClientID %>").value;          
            var level = document.getElementById("<%=ddlLevel.ClientID %>").value ;

            if (menuName == "") {
                alert("菜单名称不能为空！");
                document.getElementById("<%=txtMenuName.ClientID %>").focus();
                return false;
            }
            if (parentID == "") {
                alert("请选择父级节点！");
                document.getElementById("<%=ddlParentID.ClientID %>").focus();
                return false;
            }
            
            if (orderID == "") {
                alert("菜单排序不能为空！");
                document.getElementById("<%=txtOrderID.ClientID %>").focus();
                return false;
            }

            if (isNaN(orderID)==true) {
                alert("菜单排序必须输入为数字！");
                document.getElementById("<%=txtOrderID.ClientID %>").focus(); 
                return false;
            }

            if (level == "") {
                alert("请选择所在级别！");
                document.getElementById("<%=ddlLevel.ClientID %>").focus();
                return false;
            }
            return true;
        
        }

       

    </script>
</asp:Content>

