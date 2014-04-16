<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AutoCptControl.ascx.cs" Inherits="AutoCptControl" %>
<div>
<input id="<%=CTLID%>" type="text" style="width:<%=CTLLEN%>" onclick="this.select()"/>
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
            extraParams: { area: function () { return document.getElementById("<%=HiddenParms.ClientID%>").value; } },
            formatItem: function (data, i, total) {
                return data[0];
            },
            formatMatch: function (data, i, total) {
                return data[0];
            },
            formatResult: function (data) {
                return data[0];
            }
        }).result(function (event, item) {
            if (document.getElementById("<%=hiddenValue.ClientID%>").value != item) {
                if (document.getElementById("MainContent_dvAreaList") != null && document.getElementById("MainContent_dvAreaList") != "undefinded") {
                    document.getElementById("MainContent_dvAreaList").innerHTML = "";
                }
                if (document.getElementById("MainContent_hidBussList") != null && document.getElementById("MainContent_hidBussList") != "undefinded") {
                    document.getElementById("MainContent_hidBussList").value = "";
                }
            }
            document.getElementById("<%=hiddenValue.ClientID%>").value = item;
        });

    });
</script>
<asp:HiddenField ID="hiddenValue" runat="server"/>
<asp:HiddenField ID="hiddenCtlID" runat="server"/>
<asp:HiddenField ID="hiddenCtlLEN" runat="server"/>
<asp:HiddenField ID="HiddenStyle" runat="server"/> 
<asp:HiddenField ID="HiddenCityName" runat="server"/>
<asp:HiddenField ID="HiddenParms" runat="server"/>
