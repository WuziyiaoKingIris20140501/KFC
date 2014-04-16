<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="offHotel.aspx.cs" Inherits="WebUI_Hotel_offHotel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <div>
     <table align="center" border="0" width="100%" class="Table_BodyCSS">
    <tr class="RowTitle" align="center"><td colspan="4"><asp:Literal Text="设置想下线的酒店" ID="lblOnTitle" runat="server"></asp:Literal> </td></tr>           
    <tr><td class="" width="10%">酒店ID</td><td width="90%">
        <asp:TextBox ID="txtHotelID" runat="server" Rows="30" TextMode="MultiLine" 
            Width="98%"></asp:TextBox></td></tr>
    <tr><td colspan=2 align="center">
        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn primary" 
            onclick="btnSave_Click" /></td></tr>
    </table>
    </div>
</asp:Content>

