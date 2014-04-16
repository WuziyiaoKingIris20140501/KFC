<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoogleMapIn.aspx.cs" Inherits="GoogleMapIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
    <%--<link href="Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <%--<script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
    <%--AIzaSyDfsexkOEc-mnuogfFfM0cpIG3LmKsip6s  ABQIAAAAqa3ImUbVOR4nfQco5wcRqxQCi4xPx0sqPHl6Fh9cen2jWYjyohSl-yJ6EG9sZKZ0j31ITiBp8gnIuw --%>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=AIzaSyDfsexkOEc-mnuogfFfM0cpIG3LmKsip6s&amp;hl=zh-CN" type="text/javascript"></script>
	<script type="text/javascript">
	    var map = null;
	    var geocoder = null;
	    var markersArray = new Array();
	    $(function () {
	        //	        getAirport("zh");
	        $("#searchBtn").click(function () {
	            showAddress();
	        });
	    });
	    function showAddress() {
	        var searchKey = document.getElementById("address").value;
	        var point;
	        var marker;
	        var baseIcon = new GIcon(G_DEFAULT_ICON);
	        baseIcon.shadow = "http://www.google.com/mapfiles/shadow50.png";
	        baseIcon.iconSize = new GSize(20, 34);
	        baseIcon.shadowSize = new GSize(37, 34);
	        baseIcon.iconAnchor = new GPoint(9, 34);
	        baseIcon.infoWindowAnchor = new GPoint(9, 2);

	        if (!geocoder) {
	            geocoder = new GClientGeocoder();
	        }
	        var delAllResult = function () {
	            for (var i = 0; i < markersArray.length; i++) {
	                map.removeOverlay(markersArray[i]);
	            }
	            markersArray = new Array();
	        }

	        geocoder.getLocations(searchKey, function (result) {
	            delAllResult();
	            if (result.Status.code == G_GEO_SUCCESS) {
	                map.setZoom(10);
	                for (var i = 0; i < result.Placemark.length; i++) {
	                    var p = result.Placemark[i].Point.coordinates;

	                    $("#longitude").val(p[0]);
	                    $("#lat").val(p[1]);

	                    point = new GLatLng(p[1], p[0]);
	                    map.setCenter(point, 14);

	                    var letter = String.fromCharCode("A".charCodeAt(0) + i);
	                    var letteredIcon = new GIcon(baseIcon);
	                    letteredIcon.image = "http://www.google.com/mapfiles/marker" + letter + ".png";
	                    marker = new GMarker(point, { icon: letteredIcon, title: result.Placemark[i].address, clickable: false });
	                    //添加鼠标单击标记事件	      
	                    GEvent.addListener(marker, "click", function (overlay, latlng) {
	                        $("#longitude").val(latlng.lng());
	                        $("#lat").val(latlng.lat());
	                    });
	                    markersArray.push(marker);
	                    map.addOverlay(marker);
	                }
	            } else {
	                alert('不能找到： "' + searchKey + '"，' + result.Status.code);
	            }
	        });
	    }
	    function initGoogleMap() {
	        if (GBrowserIsCompatible()) {
	            //获取经纬度初始值
	            var lng = $("#longitude").val();
	            var lat = $("#lat").val();
	            var address = $("#address").val();
	            var city = "";
	            map = new GMap2(document.getElementById("googlemap"));
	            //默认显示中国
	            var center = new GLatLng(35, 106);
	            map.setCenter(center, 4);
	            if ("" != lng && "" != lat) {
	                //如果经纬度不为空，根据经纬度定位地图
	                var center = new GLatLng(lat, lng);
	                map.setCenter(center, 14);
	                var marker = new GMarker(center, { draggable: false, clickable: false });
	                map.addOverlay(marker);
	            } else {
	                geocoder = new GClientGeocoder();
	                if ("" == address && "" != city) {
	                    geocoder.getLatLng(city, function (latlng) {
	                        if (latlng) {
	                            map.setCenter(latlng, 10);
	                        }
	                    });
	                } else {
	                    geocoder.getLatLng(city + address, function (latlng) {
	                        if (latlng) {
	                            map.setCenter(latlng, 14);
	                        }
	                    });
	                }
	            }
	            //添加导航控件
	            map.addControl(new GSmallMapControl());
	            //添加地址搜索栏
	            //map.addControl(new google.maps.LocalSearch(), new GControlPosition(G_ANCHOR_BOTTOM_RIGHT, new GSize(10,20)));

	            //设置地图标记的图标
	            var blueIcon = new GIcon(G_DEFAULT_ICON);
	            blueIcon.image = "http://sc.hubs1.net/resource/images/default/mark.png";
	            // 设置 GMarkerOptions 对象
	            markerOptions = { icon: blueIcon, clickable: false };
	            var tempMarker = null;
	            //添加鼠标单击地图事件	      
	            GEvent.addListener(map, "click", function (overlay, latlng) {
	                //当前点击地图的坐标点
	                var newMarker = new GMarker(latlng, markerOptions);
	                //删除前一次点击的坐标点          	
	                if (tempMarker != null) map.removeOverlay(tempMarker);
	                map.addOverlay(newMarker);
	                //更新临时坐标点
	                tempMarker = newMarker;
	                $("#longitude").val(latlng.lng());
	                $("#lat").val(latlng.lat());
	            });
	        }
	    }

	    function SetInitialData(lng, lat) {
	        $("#longitude").val(lng);
	        $("#lat").val(lat);
	    }

	    function windowClose() {
	        var lng = $("#longitude").val();
	        var lat = $("#lat").val();
	        var ret = lng + "," + lat
	        window.returnValue = ret;
            window.close();
	    }
	</script>

</head>
<body onload="initGoogleMap()">
    <form id="form1" runat="server">
    <div id="set5" style="width: 850px; height: 620px; margin-left: 20px; margin-top:10px; float: left;">
        <table width="100%">
            <tr>
                <td align="left">
                    请输入关键字：<input type="text" id="address" value="" style="width:265px;"/>
                    <input type="button" id="searchBtn" class="btn primary" value="搜索"/><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="googlemap" style="height: 525px; width: 100%"></div>
                </td>
            </tr>
        </table>
		<table width="100%">
            <tr>
                <td  align="left">
                    经度:
                </td>
                <td>
                    <input readonly="readonly" type="text" id="longitude" value="" />
                </td>
                <td  align="left">
                    纬度:
                </td>
                <td>
                    <input readonly="readonly" type="text" id="lat" value="" />
                </td>
                <td align="right">
                    <input type="button" id="btnCancel" class="btn" value="取消" onclick="window.close();" />
                    &nbsp;
                    <input type="button" id="btnSave" class="btn primary" value="确认" onclick="windowClose();" />
                </td>
            </tr>
        </table>
	</div>
    </form>
</body>
</html>
