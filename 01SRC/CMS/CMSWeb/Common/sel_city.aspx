<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sel_city.aspx.cs" Inherits="Com_sel_city" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>城市列表</title>    
     <link href="../Styles/Sites.css" type="text/css" rel="Stylesheet" />
</head>   
<body>
 
<script language="javascript" type="text/javascript">
    function Clear() {
        document.getElementById("txtCityName").value = "";
        document.getElementById("txtCityID").value = "";
    }

    function Close() {
        window.close();
    }

    var FormType = "<%=FormType %>";
    var CityName = "";
    var CityID = "";

    var Cur = 0;
    var pObj = window.parent.opener.form1;

    String.prototype.trim = function ()    //除去字符串左右的空格的函数
    {
        return this.replace(/(^\s+)|\s+$/g, "");
    }


    function GetValue() {
        var v = document.forms[0];
        for (var a = 0; a < v.elements.length; a++) {
            if (v.elements[a].type == "checkbox") {
                if (v.elements[a].checked == true) {
                    Cur = 1;
                }
            }
        }


        if (Cur == 1) {
            var row = document.getElementById("myGridView").rows;
            var len = row.length;
            if (len == 17) {
                for (var i = 1; i < len; i++) {
                    //var chk = row[i].cells[0].firstChild;
                    var chk = row[i].cells[0].children[0];
                    if (chk.checked == true) {
                        CityName += "," + chk.getAttribute("p2").trim();
                        CityID += "," + chk.getAttribute("p1").trim();
                    }
                }
            }
            if (len < 17) {
                for (var j = 0; j < len; j++) {
                    //var chk1 = row[j].cells[0].firstChild;
                    var chk1 = row[j].cells[0].children[0];
                    if (chk1.checked == true) {
                        CityName += "," + chk1.getAttribute("p2").trim();
                        CityID += "," + chk1.getAttribute("p1").trim();
                    }
                }
            }
            var lenName = CityName.length;
            var lenCityID = CityID.length;

            if (FormType == "MultiSelCity")//复选，多选人
            {
                //pObj.txtCityName.value = CityName.substring(1, lenName);
                //pObj.txtCityID.value = CityID.substring(1, lenCityID);
                window.parent.opener.document.getElementById("MainContent_txtCityID").value = CityID.substring(1, lenCityID);            
            }

            window.close();
        }
        else {
            alert("请选择一个酒店！");
        }
    }

    //  点击全选   
    function ClickCheckAllItem(ck) {
        var checkboxHead = document.getElementById("HeadCheckBox");
        var checkbox = document.getElementsByName("checkitem");
        if (checkboxHead.checked == true) {
            for (var i = 0; i < checkbox.length; i++) {
                checkbox[i].checked = true;
            }
        }
        else {
            for (var i = 0; i < checkbox.length; i++) {
                checkbox[i].checked = false;
            }
        }
    } 
    </script>
    <form id="form1" runat="server">
    <br />    
               <table width="98%" align="center" border="0" class="Table_BodyCSS" >
            <tr>
                <td style="width:12%;" class="tdcell" >城市名称</td>
                <td style="width:28%;" class="tdcell" >
                    <asp:TextBox ID="txtCityName" runat="server" CssClass="textBlurNew" SkinID="txtchange" Width="99%"></asp:TextBox>
                </td>                    
                <td style="width:12%;"  class="tdcell" >城市ID</td>
                <td style="width:28%;" class="tdcell"><asp:TextBox ID="txtCityID" runat="server" CssClass="textBlurNew" SkinID="txtchange"  Width="99%"></asp:TextBox></td>
                
            </tr>
            <tr>                
                <td  align="center" colspan="4" class="tdcell">
                    <asp:Button ID="btnSearch" CssClass="btn primary" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    <input id="btnClear" type="button" value="清空" class="btn" onclick="Clear()" />
                </td>
            </tr>
        </table><br />
        <asp:GridView Width="99%" HorizontalAlign="Center" ID="myGridView" 
        runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" PageSize="15"  CssClass="GView_BodyCSS"
        onpageindexchanging="myGridView_PageIndexChanging">
           <%-- <PagerTemplate>
                <table width="100%" >
                    <tr>
                        <td align="left" style="width: 49%;"><%=STR_TOTALREC1%><font color="red"><%=Rows %></font><%=STR_TOTALREC2 %><%=STR_PGNO %><font color="red"><%#((GridView)Container.NamingContainer).PageIndex + 1 %></font>/<font color="red"><%# ((GridView)Container.NamingContainer).PageCount %></font></td>
                        <td align="right" style="width: 50%;">
                            <asp:LinkButton ID="lbtnFirstPage" runat="server" CommandArgument="First" CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex !=0 %>"><%=STR_FIRSTPG%></asp:LinkButton>
                            <asp:LinkButton ID="lbtnPreviousPage" runat="server" CommandArgument="Prev" CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex !=0 %>"><%=STR_PREPG %></asp:LinkButton>
                            <asp:LinkButton ID="lbtnNextPage" runat="server" CommandArgument="Next" CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount -1 %>"><%=STR_NEXTPG %></asp:LinkButton>
                            <asp:LinkButton ID="lbtnLastPage" runat="server" CommandArgument="Last" CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount -1 %>"><%=STR_LASTPG %></asp:LinkButton> 
                        </td>
                    </tr>
                </table>
            </PagerTemplate>--%>                 
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderStyle HorizontalAlign="Center" Width="4%" />
                    <ItemTemplate>
                        <input id="checkitem" name="checkitem"  type="checkbox"  p1="<%#Eval("CITY_ID") %>" p2="<%#Eval("NAME_CN") %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <input id="HeadCheckBox" type="checkbox"  onclick="ClickCheckAllItem(this)"  />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NAME_CN" HeaderText="城市名称">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="城市ID" DataField="CITY_ID">
                    <HeaderStyle HorizontalAlign="Center" Width="30%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
             
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
              
              <EmptyDataTemplate>
              <table width="100%"  border=0 cellpadding=0 cellspacing=0>
                    <tr  class="thhead">
                            <td   style="width:10%;" class="tdcell"  align="center">选择</td>
                            <td style="width:30%" class="tdcell" align="center">城市名称</td>
                            <td style="width:30%" class="tdcell" align="center">城市ID</td>                           
                    </tr>
                   <tr>
                            <td   colspan="4" class="tdcell" align="center">没有满足条件的城市信息！</td>
                    </tr> 
                </table>
            </EmptyDataTemplate> 
        </asp:GridView><br />
        <table style="width:99%;" align="center">
            <tr>
                <td align="center"  style="height:22px;">
                    <input id="btnOK" type="button" value="确定" class="btn primary" onclick="GetValue()" />
                    <input id="btnClose()" type="button" value="关闭" class="btn" onclick="Close()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
