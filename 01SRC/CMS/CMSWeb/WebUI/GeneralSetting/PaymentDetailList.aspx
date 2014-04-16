<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentDetailList.aspx.cs"  Title="支付方式管理明细" Inherits="PaymentDetailList" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <link href="../../Styles/Css.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body>
<form id="form1" runat="server">
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">修改支付方式</li>
        <li>支付方式名称：
          <input name="textfield" type="text" id="txtPaymentName" value="" runat="server" maxlength="32"/>
            支付方式代码：
          <input name="textfield" type="text" id="txtPaymentID" value="" runat="server" readonly="readonly" />
        </li>
        <li><div id="detailMessageContent" runat="server" style="color:red"></div></li>

        <li>
            <div class="frame02">
                 <asp:GridView ID="gridViewCSPaymentDetailList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSPaymentDetailList_RowDataBound"  OnRowEditing="gridViewCSPaymentDetailList_RowEditing"
                            OnRowUpdating="gridViewCSPaymentDetailList_RowUpdating" OnRowCancelingEdit="gridViewCSPaymentDetailList_RowCancelingEdit" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"  />
                    <asp:BoundField DataField="PAYMENTID" HeaderText="PAYMENTID" Visible="False"  />
                    <asp:BoundField DataField="PAYMENTCODE" HeaderText="PAYMENTCODE" Visible="False"  />

                    <asp:BoundField DataField="PLATMENTID" HeaderText="PLATMENTID" Visible="False"  />
                    <asp:BoundField DataField="PAYMENTNM" HeaderText="PAYMENTNM" Visible="False"  />
                    <asp:BoundField DataField="ONLINESTATUS" HeaderText="ONLINESTATUS" Visible="False"  />


                    <asp:BoundField DataField="PLATFORMNM" HeaderText="应用平台" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <%--<asp:BoundField DataField="PLATFORMCODE" HeaderText="支付方式代码" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                       <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="开放状态">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkOnline" runat="server" />
                            </ItemTemplate>
                      </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
            </div>
        </li>



        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="修改" onclick="btnUpdate_Click" />
            
            <%--<input type="button" value="修改" runat"server" style="width:80px;height:20px" onclick="btnSave_Click();window.returnValue=true;window.close();"/>--%>
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
        </li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidPaymentNo" runat="server"/>
</form>
</body>
</html>