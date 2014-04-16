<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebAutoComplete.ascx.cs" Inherits="WebAutoComplete" %>
<div>
<input id="<%=CTLID%>" type="text" style="width:347px" onclick="this.select()" />
</div>
<script type="text/javascript">
    $().ready(function () {
        if (document.getElementById("<%=HiddenStyle.ClientID%>").value == "1") {
            document.getElementById("<%=CTLID%>").disabled = true;
        }
        else {
            document.getElementById("<%=CTLID%>").disabled = false;
        }

        $("#<%=CTLID%>").autocomplete('<%=AutoParent%>', {
            delay: 10,
            width: 350,
            minChars: 1,
            max: 50,
            autoFill: false,
            mustMatch: false,
            matchContains: false,
            scrollHeight: 220,
            formatItem: function (data, i, total) {
                return data[0];
            },
            formatMatch: function (data, i, total) {
                return data[0];
            },
            formatResult: function (data) {
                return data[0];
            }
        }).result(function (event, item) { document.getElementById("<%=hiddenValue.ClientID%>").value = item; });

    });
</script>
<asp:HiddenField ID="hiddenValue" runat="server"/>
<asp:HiddenField ID="hiddenCtlID" runat="server"/>
<asp:HiddenField ID="HiddenStyle" runat="server"/> 
<asp:HiddenField ID="HiddenCityName" runat="server"/> 
