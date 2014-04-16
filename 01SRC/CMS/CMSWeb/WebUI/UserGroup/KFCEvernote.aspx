<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="KFCEvernote.aspx.cs" Inherits="KFCEvernote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <table cellpadding="0" cellspacing="0" style="width:100%; height :900px; margin-top:5px" border="0" align="center" > 
    <tr>       
      <td style="padding:0 10px; vertical-align:top;text-align: left; width:100%;height :100%">
                    <iframe id="frame_map" name="frame_map" src="https://www.evernote.com/pub/kevinzhangqi/kfcsharednotes" frameborder="0"  width="100%"  scrolling="auto" style="height: 100%;"></iframe>
          </td>
    </tr>
   </table>
</asp:Content>

