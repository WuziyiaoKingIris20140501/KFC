
//打开一个新窗口
var winOpen = null;
function OpenWin(url) {
    if (winOpen != null) {
        try {
            winOpen.close();

        } catch (e2) {

            winOpen = null;
        }

    }
    winOpen = window.open(url, "", "toolbar=no, menubar=no, location=no, directories=no,width=800,height=600,top=40,left=40");
}

//参数说明：url
//          WinName:打开窗口名称
var winWnd = null;
function OpenWnd(URL, WinName) {
    var width = screen.availWidth - 10;
    var height = screen.availHeight - 30;
    if (winWnd != null) {
        try {
            winWnd.close();

        } catch (e2) {

            winWnd = null;
        }

    }
    winWnd = window.open(URL, WinName, "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=" + width + ",height=" + height + ",top=0,left=0");
}

var win = null;
function OpenWnd600(URL) {
    if (win != null) {
        try {
            win.close();
        }
        catch (e2) {
            win = null;
        }
    }
    win = window.open(URL, "", "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=800,height=600,top=20,left=20");
}


//显示弹出的层
function invokeOpen() 
{
    document.getElementById("popupDiv").style.display = "block";
    //背景
    var bgObj = document.getElementById("bgDiv");
    bgObj.style.display = "block";
    bgObj.style.width = document.body.offsetWidth + "px";
    bgObj.style.height = screen.height + "px";

    //定义窗口
    //var msgObj=document.getElementById("popupDiv");
    //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
}

//隐藏弹出的层
function invokeClose() 
{
    document.getElementById("popupDiv").style.display = "none";
    document.getElementById("bgDiv").style.display = "none";
}

//去除空格
String.prototype.Trim = function () {
    var m = this.match(/^\s*(\S+(\s+\S+)*)\s*/);
    return (m == null) ? "" : m[1];
}
//判断输入的手机号是否正确
String.prototype.isMobile = function () {
    return (/^1[3|4|5|8][0-9]\d{8}$/.test(this.Trim()));
}

//String.prototype.Trim = function () { return Trim(this); }
//String.prototype.LTrim = function () { return LTrim(this); }
//String.prototype.RTrim = function () { return RTrim(this); }

////此处为独立函数
//function LTrim(str) {
//    var i;
//    for (i = 0; i < str.length; i++) {
//        if (str.charAt(i) != " " && str.charAt(i) != " ") break;
//    }
//    str = str.substring(i, str.length);
//    return str;
//}
//function RTrim(str) {
//    var i;
//    for (i = str.length - 1; i >= 0; i--) {
//        if (str.charAt(i) != " " && str.charAt(i) != " ") break;
//    }
//    str = str.substring(0, i + 1);
//    return str;
//}
//function Trim(str) {
//    return LTrim(RTrim(str));
//}