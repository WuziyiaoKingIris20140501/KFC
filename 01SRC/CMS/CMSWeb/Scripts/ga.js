

//公共变量
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-25161554-3']);
_gaq.push(['_setDomainName', '.hotelvp.com']);
_gaq.push(['_setAllowHash', false]);
_gaq.push(['_setAllowLinker', true]);
_gaq.push(['_trackPageview']);
_gaq.push(['_trackPageLoadTime']);


//这段是自定义变量的追踪代码，在用户提交注册时调用。index从1开始计数
//注册成功和提交订单使用
function ga_setCustomVar(index, CustomerVar,strValue) {
    _gaq.push(['_setCustomVar',
      index,                // 显示是第几个自定义变量。此次我们主要加手机号、用户姓名、设备id(如果可读取).
      CustomerVar,          // 这个是自定变量的名称。
      strValue,             // This value of the custom variable.  Required parameter.即参数的值。默认可以为1.
      1                    // Sets the scope to visitor-level.  Optional parameter.
   ]);
}

//actionLabel:web_pagename_controlname_event
//有按钮动作的地方调用
function ga_trackEvent(actionLabel) {

    _gaq.push(['_trackEvent', 'web', 'click', actionLabel, 1]);

    //上面这段为追踪event的代码，在特定事件时发送。目前所有的按钮均需要调用。这几个参数分别为：
    //category (required)：The name you supply for the group of objects you want to track.
    //action (required)：A string that is uniquely paired with each category, and commonly used to define the type of user interaction for the web object.
    //label (optional)： An optional string to provide additional dimensions to the event data.
    //value (optional)：An integer that you can use to provide numerical data about the user event.
}

//每个页面都需要调用这个函数，每个页面载入时使用
//pagename:web_pagename
function ga_trackPageview(pagename)
{
    _gaq.push(['_trackPageview', pagename]);
}


//支付成功后使用该函数
function ga_order_pay(orderid,hotelname,totalprice,cityid,province,price,roomnums) {
    _gaq.push(['_addTrans',
    orderid,        // order ID - required
    hotelname,      // affiliation or store name:hotelname
    totalprice,     // total - required  ;总价
    '0.00',         // tax :税:0
    '0',            // shipping:运费:0
    cityid,         // city:城市ID
    province,       // state or province：省，市
    'China'         // country：china
  ]);

    // add item might be called for every item in the shopping cart
    // where your ecommerce engine loops through each item in the cart and
    // prints out _addItem for each
    _gaq.push(['_addItem',
    orderid,        // order ID - required:订单编号
    hotelname,      // SKU/code - required:hotelName
    'LM客房',       // product name:LM客房
    'LM',           // category or variation:LM
    price,          // unit price - required:单价
    roomnums        // quantity - required:房间数
  ]);

    _gaq.push(['_trackTrans']); //submits transaction to the Analytics servers
}


//应用google的ga.js
var ga = document.createElement('script');
ga.type = 'text/javascript';
ga.async = true;
ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
var s = document.getElementsByTagName('script')[0];
s.parentNode.insertBefore(ga, s);