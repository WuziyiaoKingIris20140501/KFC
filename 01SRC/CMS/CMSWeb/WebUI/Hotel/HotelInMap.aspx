<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelInMap.aspx.cs" Title="酒店地图" Inherits="WebUI_Hotel_HotelInMap" %>
<html>
  <head>
    <title>酒店位置地图分布</title>
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
                zoom: 10,
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

        /**********************************************************************/
        function checkValid() {              
            var inputRules = /^\+?[1-9][0-9]*$/;///^\d+(\.\d+)?$/; //只能是数字且不能为负数
            
            var fromPrice = document.getElementById("<%=txtFromPrice.ClientID%>").value;
            if(fromPrice!="")
            {
                if (inputRules.test(fromPrice) ==false) 
                {                             
                    alert("价格区间中的起始价格必须为大于0的数字！");
                    document.getElementById("<%=txtFromPrice.ClientID%>").focus();
                    return false;               
                }
            }

            var twoPrice = document.getElementById("<%=txtPrice.ClientID%>").value;
            if(twoPrice!="")
            {
                if (inputRules.test(twoPrice) == false) 
                {
                    alert("价格区间中的结束价格必须大于0的数字！");
                    document.getElementById("<%=txtPrice.ClientID%>").focus();
                    return false;               
                }
            }
           
            if (fromPrice!="" && twoPrice!="" &&(fromPrice>twoPrice))
            {
                alert("价格区间中的起始价格必须小于结束价格！");
                document.getElementById("<%=txtPrice.ClientID%>").focus();
                return false;            
            }

            var startDate = document.getElementById("<%=fromDate.ClientID%>").value;
            if (startDate == "") 
            { 
                alert("开始日期不能为空！");
                document.getElementById("<%=fromDate.ClientID%>").focus();
                return false;
            }
            
            var endDate = document.getElementById("<%=endDate.ClientID%>").value;
            if (endDate == "") 
            { 
                alert("结束日期不能为空！");
                document.getElementById("<%=endDate.ClientID%>").focus();
                return false;
            }

            if (startDate >endDate) 
            {
                alert("开始日期不能大于结束日期！");
                document.getElementById("<%=fromDate.ClientID%>").focus();
                return false;
            }
            return true;
        }    

        //================================================================
        function checkSelect0()
        {
            var obj0= document.getElementById("<%=chkListStar0.ClientID%>");
            var obj1= document.getElementById("<%=chkListStar1.ClientID%>");
            var obj2= document.getElementById("<%=chkListStar2.ClientID%>");
            var obj3= document.getElementById("<%=chkListStar3.ClientID%>");
            var obj4= document.getElementById("<%=chkListStar4.ClientID%>");
            if(obj0.checked)
            {
                obj1.checked =false;
                obj2.checked =false;
                obj3.checked =false;
                obj4.checked =false;
            }
            if ( obj1.checked ==true || obj2.checked == true || obj3.checked == true ||obj4.checked ==true )
            {            
                obj0.checked=false;
            } 
        }

        function checkSelect()
        {
            var obj0= document.getElementById("<%=chkListStar0.ClientID%>");
            var obj1= document.getElementById("<%=chkListStar1.ClientID%>");
            var obj2= document.getElementById("<%=chkListStar2.ClientID%>");
            var obj3= document.getElementById("<%=chkListStar3.ClientID%>");
            var obj4= document.getElementById("<%=chkListStar4.ClientID%>");            
            if ( obj1.checked ==true || obj2.checked == true || obj3.checked == true ||obj4.checked ==true )
            {            
                obj0.checked=false;
            } 
        }

    </script>   
  </head>

  <body onload="initialize();">
    <form runat="server">
     <div style="text-align:center"> 
     <table align="center" border="0" width="100%" class="Table_BodyCSS" >
     <tr class="RowTitle"><td colspan=6><asp:Literal Text="酒店地理位置分布图" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>
     <tr>
        <td style="width:10%" class="tdcell">所在城市：</td>
        <td style="width:10%" class="tdcell"><asp:DropDownList ID="ddlcityid" runat="server"></asp:DropDownList></td>
        <td style="width:10%" class="tdcell">酒店类型：</td>
        <td style="width:15%" class="tdcell" align="left"  style="width:15%" >
                    <asp:DropDownList ID="ddlLMBAR" runat="server">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="LMBAR">预付酒店</asp:ListItem>
                        <asp:ListItem Value="LMBAR2">现付酒店</asp:ListItem>
                    </asp:DropDownList>                
         </td> 
        <td style="width:10%" class="tdcell">星级</td>
        <td style="width:40%" class="tdcell">       
           <%-- <asp:CheckBoxList ID="chkListStar" runat="server" RepeatDirection="Horizontal"
                RepeatColumns="8" RepeatLayout="Table" CellSpacing="8">
             <asp:ListItem Value="">不限</asp:ListItem>
             <asp:ListItem Value="2">经济型</asp:ListItem>
             <asp:ListItem Value="3">三星</asp:ListItem>
             <asp:ListItem Value="4">四星</asp:ListItem>
             <asp:ListItem Value="5">五星</asp:ListItem>
            </asp:CheckBoxList>--%>
            <input id="chkListStar0" type="checkbox" runat="server" value="" onclick="checkSelect0()"  />不限
            <input id="chkListStar1" type="checkbox" runat="server" value="2" onclick="checkSelect()" />经济型
            <input id="chkListStar2" type="checkbox" runat="server" value="3" onclick="checkSelect()" />三星
            <input id="chkListStar3" type="checkbox" runat="server" value="4" onclick="checkSelect()" />四星
            <input id="chkListStar4" type="checkbox" runat="server" value="5" onclick="checkSelect()" />五星
        </td>

     </tr>
    <tr><td class="tdcell">价格区间：</td>
        <td class="tdcell" align="left">
          <asp:TextBox ID="txtFromPrice" runat="server"></asp:TextBox>
         </td>
         <td class="tdcell">
         <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
         </td> 
         <td class="tdcell">日期 &nbsp;&nbsp;从：</td> 
         <td class="tdcell" colspan="2">                
            <input id="fromDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>
         到： <input id="endDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/></td>             
      </tr>
         <tr><td colspan=6 align="center"><asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="return checkValid();" onclick="btnSearch_Click"/></td></tr>
     </table> 
     </div>
    <div style="text-align:center"> </div>
    <div id="map_canvas" style="width:100%; height:100%">       
     </div> 
    </form>
 </body>

</html>
