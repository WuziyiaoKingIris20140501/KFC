<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserOPInMap.aspx.cs" Title="用户热点图" Inherits="WebUI_UserGroup_UserOPInMap" %>
<html>
  <head>
    <title>酒店位置地图分布</title>
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>  
<meta http-equiv="X-UA-Compatible" content="chrome=1">
<link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
<link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />

<script type="text/javascript" src="http://maps.google.cn/maps/api/js?sensor=false"></script>
<%--<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> --%>
 <link rel="apple-touch-icon" href="http://www.hotelvp.com/icon/114x114.png" />
 <link rel="shortcut icon" href="http://www.hotelvp.com/favicon.ico" /> 

    <script type="text/javascript">
        var beaches = <%=arrData %>;
        var map;
        function initialize() 
        {  
             var myOptions ={    
                    zoom: 12,
                    center: new google.maps.LatLng(beaches[0][0], beaches[0][1]),    //第一个酒店所在位置 
                    mapTypeId: google.maps.MapTypeId.ROADMAP  
                }  
                map = new google.maps.Map(document.getElementById("map_canvas"),myOptions);
                setMarkers(map, beaches);
          }
          function setMarkers(map, locations) 
          {
             var image = '../../Images/reddot.png';
             for (var i = 0; i < locations.length; i++) {
                 var beach = locations[i];
                 var myLatLng = new google.maps.LatLng(beach[0], beach[1]);
                 var marker = new google.maps.Marker({
                     position: myLatLng,
                     map: map,
                     //shadow: shadow,
                     //icon: image,
                     //shape: shape,
                     //title: beach[0],
                     zIndex: beach[2],
                     icon: image
                 });
                 //attachSecretMessage(marker, i, (beach[0] +  beach[1]));
             }
         }

        // The five markers show a secret message when clicked
        // but that message is not within the marker's instance data
        function attachSecretMessage(marker, number,msg) {
            //var message = ["This", "is", "the", "secret", "message"];
            var infowindow = new google.maps.InfoWindow({ 
                //content: message[number]
                content:msg
          });
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
        }
    </script>
  </head>

  <body onload="initialize()">
    <form runat="server">
     <div style="text-align:center"> 
     <table align="center" border="0" width="100%" class="Table_BodyCSS" >
     <tr class="RowTitle"><td colspan="3"><asp:Literal Text="订单地理位置分布图" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>
     <tr>
        <td style="width:10%" class="tdcell">选择文件：</td>
        <td class="tdcell"><asp:FileUpload ID="OrderFlUpload" runat="server"  Width="500px" /></td>
        <td align="left"><asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="导入" onclick="btnSearch_Click"/></td>
      </tr>
      <tr>
        <td class="tdcell" colspan="3">
            <div id="messageContent" runat="server" style="color:red;width:800px;"></div>
        </td>
      </tr>
     </table> 
     </div>
     <div style="text-align:center"><br /></div>
    <div style="text-align:center"></div>
    <div id="map_canvas" style="width:100%; height:100%">
     </div> 
    </form>
 </body>

</html>
