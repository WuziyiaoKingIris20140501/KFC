<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelInfoManagerMap.aspx.cs" Title="酒店地图" Inherits="HotelInfoManagerMap" %>
<html>
  <head>
    <title>酒店位置经纬度地图</title>
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>  
<meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
 <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
  <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />

<script type="text/javascript" src="http://maps.google.cn/maps/api/js?sensor=false"></script>    
<%--<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> --%>
 <link rel="apple-touch-icon" href="http://www.hotelvp.com/icon/114x114.png" />
 <link rel="shortcut icon" href="http://www.hotelvp.com/favicon.ico" /> 
   <script language="javascript" type="text/javascript" src="../../Scripts/WdatePicker.js"></script>
<%-- <script type="text/javascript" charset="UTF-8"  src="../../Scripts/ga/ga.js"></script> --%>
  
    <script type="text/javascript">

        /**********************************************************************/
        //debugger;
        var beaches = <%=arrData %>;
        var map;
        function initialize() 
        {  
            var myOptions ={    
                zoom: 18,
                //center: new google.maps.LatLng(121.47370400, 31.23039300),    //上海的中心的经纬度
                center: new google.maps.LatLng(beaches[0][1], beaches[0][2]),    //第一个酒店所在位置
                mapTypeId: google.maps.MapTypeId.ROADMAP  
            }  
            map = new google.maps.Map(document.getElementById("map_canvas"),myOptions);
            setMarkers(map, beaches);
          }

          /** * Data for the markers consisting of a name, a LatLng and a zIndex for * the order in which these markers should display on top of each * other. */
//          var beaches = [  
//            ['Bondi Beach', -33.890542, 151.274856, 4],  
//            ['Coogee Beach', -33.923036, 151.259052, 5],  
//            ['Cronulla Beach', -34.028249, 151.157507, 3],  
//            ['Manly Beach', -33.80010128657071, 151.28747820854187, 2],  
//            ['Maroubra Beach', -33.950198, 151.259302, 1]
          //          ]; 

          function setMarkers(map, locations) 
          {
             for (var i = 0; i < locations.length; i++) {
                 var beach = locations[i];
                 var myLatLng = new google.maps.LatLng(beach[1], beach[2]);
                 var marker = new google.maps.Marker({
                     position: myLatLng,
                     map: map,
                     //shadow: shadow,
                     //icon: image,
                     //shape: shape,
                     title: beach[0],
                     zIndex: beach[3]
                 });
                 attachSecretMessage(marker, i,beach[0]);
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

  <body onload="initialize();">
    <form runat="server">
    <div style="text-align:center"> </div>
    <div id="map_canvas" style="width:100%; height:100%">
     </div> 
    </form>
 </body>

</html>
