using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using EWap.API.Model;

namespace HotelVp.EBOK.Domain.API
{
    public static class API
    {
        //// 合并酒店列表
        //public static Response<SearchHotelList> hotelList(string clientCode, string cityId, string keywords, List<int> starCode, string tradeArea, string brandName, PriceRange price, Location location, string checkInDate, string checkOutDate, int pageSize, int pageNum, Sort sort, string keywordsType, string reference)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"cityId\":\"" + cityId + "\"");
        //        if (!string.IsNullOrEmpty(keywords) && keywords.ToLower() != "null")
        //            body.Append(",\"keywords\":\"" + keywords + "\"");
        //        if (!string.IsNullOrEmpty(keywordsType) && keywordsType.ToLower() != "null")
        //            body.Append(",\"keywordsType\":\"" + keywordsType + "\"");
        //        if (!string.IsNullOrEmpty(keywordsType) && reference.ToLower() != "null")
        //            body.Append(",\"reference\":\"" + reference + "\"");
        //        if (starCode != null && starCode.Count > 0)
        //            body.Append(",\"starCode\":[" + string.Join(",", starCode) + "]");
        //        if (!string.IsNullOrEmpty(tradeArea) && tradeArea.ToLower() != "null")
        //            body.Append(",\"tradeArea\":\"" + tradeArea + "\"");
        //        if (!string.IsNullOrEmpty(brandName) && brandName.ToLower() != "null")
        //            body.Append(",\"brandName\":\"" + brandName + "\"");
        //        if (price != null)
        //            body.Append(",\"priceRange\":{\"from\":" + price.from + ",\"to\":" + price.to + "}");
        //        if (location != null)
        //        {
        //            body.Append(",\"location\":{\"latitude\":\"" + location.latitude + "\",\"longitude\":\"" + location.longitude + "\"");
        //            if (location.range != null)
        //                body.Append(",\"range\":{\"from\":" + location.range.from + ",\"to\":" + location.range.to + "}");
        //            body.Append("}");
        //        }
        //        if (!string.IsNullOrEmpty(checkInDate))
        //            body.Append(",\"checkInDate\":\"" + checkInDate + "\"");
        //        if (!string.IsNullOrEmpty(checkOutDate))
        //            body.Append(",\"checkOutDate\":\"" + checkOutDate + "\"");
        //        body.Append(",\"pageSize\":" + pageSize);
        //        body.Append(",\"pageNum\":" + pageNum);
        //        if (sort != null)
        //        {
        //            var st = ",\"sort\":{";
        //            if (!string.IsNullOrEmpty(sort.distance))
        //                st += (",\"distance\":\"" + sort.distance + "\"");
        //            if (!string.IsNullOrEmpty(sort.lowestPrice))
        //                st += (",\"lowestPrice\":\"" + sort.lowestPrice + "\"");
        //            if (!string.IsNullOrEmpty(sort.starCode))
        //                st += (",\"starCode\":\"" + sort.starCode + "\"");
        //            st += ("}");
        //            st = st.Replace("{,", "{");
        //            body.Append(st);
        //        }
        //        body.Append("}");
        //        var args = "";
        //        if (Setting.AppType == "1")
        //            args = "isTest=true&isToday=false";
        //        else if (Setting.AppType == "2")
        //            args = "isTest=true&isToday=true";

        //        var r = Request.Post(clientCode, Setting.SearchHost + "search/hotelList.json", body.ToString(), "1.2", args);
        //        return JsonUtility.Deserialize<Response<SearchHotelList>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<SearchHotelList>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////智能自动匹配搜索结果
        //public static Response<List<AutoCompleteResult>> autoComplete(string clientCode, string cityId, string keywords)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"cityId\":\"" + cityId + "\"");
        //        body.Append(",\"keywords\":\"" + keywords + "\"");
        //        //body.Append(",\"types\": [1,2,3] ");
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.SearchHost + "search/autoComplete.json", body.ToString(), "1.2", null);
        //        return JsonUtility.Deserialize<Response<List<AutoCompleteResult>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<AutoCompleteResult>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 酒店详情-LM+常规 2.9
        //public static Response<HotelDetail> hotelDetails(string clientCode, string hotelId, string imgSize, bool isShowLm, string inDate, string outDate, bool allowDawn)
        //{
        //    try
        //    {
        //        var args = string.Format("hotelId={0}&imgSize={1}&isShowLm={2}&allowDawn={3}", hotelId, imgSize, isShowLm, allowDawn);

        //        if (!string.IsNullOrEmpty(inDate))
        //            args += "&inDate=" + inDate;
        //        if (!string.IsNullOrEmpty(outDate))
        //            args += "&outDate=" + outDate;

        //        if (Setting.AppType == "1")
        //            args += "&isTest=true&isToday=false";
        //        else if (Setting.AppType == "2")
        //            args += "&isTest=true&isToday=true";

        //        var r = Request.Get(clientCode, Setting.Host + "content/hotelDetails.json", args, "2.9");
        //        return JsonUtility.Deserialize<Response<HotelDetail>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<HotelDetail>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// comments  categoryId{ 200=好评 201=中评 202=差评 其他都返回所有结果} orderBy{0:按照评论来源交叉洗牌，其次时间降序1:按照评论时间降序2:按照评论来源升序，其次时间降序3:按照评论好评度升序，其次时间降序默认选择1}
        //public static Response<Comments> comments(string clientCode, string hotelId, int categoryId, int pageNum, int pageSize, int orderBy)
        //{
        //    try
        //    {
        //        var args = string.Format("hotelId={0}&categoryId={1}&pageNum={2}&pageSize={3}&orderBy={3}", hotelId, categoryId, pageNum, pageSize, orderBy);
        //        var r = Request.Get(clientCode, Setting.Host + "comment/comments.json", args, "2.1");
        //        return JsonUtility.Deserialize<Response<Comments>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<Comments>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 城市列表
        //public static Response<CityList> cityList(string clientCode, string reqCityType, string cityListVersion)
        //{
        //    try
        //    {
        //        var args = "";
        //        if (!string.IsNullOrEmpty(reqCityType))
        //            args += "&reqCityType=" + reqCityType;
        //        if (!string.IsNullOrEmpty(cityListVersion))
        //            args += "&cityListVersion=" + cityListVersion;
        //        var r = Request.Get(clientCode, Setting.Host + "content/cityList.json", args, "2.3");
        //        return JsonUtility.Deserialize<Response<CityList>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<CityList>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


        //// 单个城市详细信息
        //public static Response<CityDetail> cityDetail(string clientCode, string cityId)
        //{
        //    try
        //    {
        //        var args = "cityId=" + cityId;
        //        var r = Request.Get(clientCode, Setting.Host + "content/cityDetail.json", args, "2.3");
        //        return JsonUtility.Deserialize<Response<CityDetail>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<CityDetail>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 品牌列表查询
        //public static Response<BrandList> getBrandList(string clientCode, string cityId)
        //{
        //    try
        //    {
        //        var args = "cityId=" + cityId;
        //        var r = Request.Get(clientCode, Setting.Host + "content/getBrandList.json", args, "2.1");
        //        return JsonUtility.Deserialize<Response<BrandList>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<BrandList>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 查询某个城市所有的商圈
        //public static Response<TradeAreaList> tradeAreaList(string clientCode, string cityId, string areaListVersion)
        //{
        //    try
        //    {
        //        var args = "cityId=" + cityId;
        //        if (!string.IsNullOrEmpty(areaListVersion))
        //            args += "&areaListVersion=" + areaListVersion;
        //        var r = Request.Get(clientCode, Setting.Host + "content/tradeAreaList.json", args, "2.3");
        //        return JsonUtility.Deserialize<Response<TradeAreaList>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<TradeAreaList>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 手机号码登录验证码取得
        //public static Response<object> getVerifyCode(string clientCode, string loginMobile, string deviceId)
        //{
        //    try
        //    {
        //        var body = string.Format("\"loginMobile\":\"{0}\",\"deviceId\":\"{1}\",\"regChannel\":\"{2}\",\"useCodeParam\":\"{3}\",\"useCodeVersionParam\":\"{4}\"",
        //                                                             loginMobile,
        //                                                             deviceId,
        //                                                             clientCode,
        //                                                             Setting.UseCode,
        //                                                             Setting.UseCodeVersion);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "user/getVerifyCode.json", body.ToString(), "2.1", null);
        //        return JsonUtility.Deserialize<Response<object>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<object>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}


        //// 手机号码登录验证码验证
        //public static Response<CheckVerifyResult> checkVerifyCode(string clientCode, string loginMobile, string signKey)
        //{
        //    try
        //    {
        //        var body = string.Format("\"loginMobile\":\"{0}\",\"signKey\":\"{1}\"",
        //                                                            loginMobile,
        //                                                            signKey);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "user/checkVerifyCode.json", body.ToString(), "2.2", null);
        //        return JsonUtility.Deserialize<Response<CheckVerifyResult>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<CheckVerifyResult>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 用户信息
        //public static Response<UserInfo> userInfo(string clientCode, string userId)
        //{
        //    try
        //    {
        //        var body = string.Format("\"userId\":\"{0}\",\"status\":0", userId);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/userInfo.json", body, "2.3");
        //        return JsonUtility.Deserialize<Response<UserInfo>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<UserInfo>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 创建订单 2.7
        //public static Response<OrderCreate> orderCreate(string clientCode, string loginMobile, string cityId, string hotelId, string inDateString, string outDateString, int bookRoomNum, string guestNames, string contactName, string contactTel, string roomTypeCode, string roomTypeName, string bookRemark, double bookPrice, string priceCode, string bookPersonTel, string payMethod, string resvStatus, string chargeDesc, string userId, bool isVisitor, int breakfastNum, bool hasNet, double ticketAmount, string ticketUsercode, bool isGua, string resvGua, string resvGuaDesc, string resvGuaHoldTime, string userHoldTime, string resvCxl, string resvCxlDesc, string bank, string creditCard, string validityPeriod, string cvv2, string userName, string credentialsType, string credentialsNum)
        //{
        //    try
        //    {
        //        var body = string.Format(
        //                         "\"loginMobile\":\"{0}\"," +
        //                         "\"cityId\":\"{1}\"," +
        //                         "\"hotelId\":\"{2}\"," +
        //                         "\"inDateString\":\"{3}\"," +
        //                         "\"outDateString\":\"{4}\"," +
        //                         "\"bookRoomNum\":{5}," +
        //                         "\"guestNames\":\"{6}\"," +
        //                         "\"contactName\":\"{7}\"," +
        //                         "\"contactTel\":\"{8}\"," +
        //                         "\"roomTypeCode\":\"{9}\"," +
        //                         "\"roomTypeName\":\"{10}\"," +
        //                         "\"bookRemark\":\"{11}\"," +
        //                         "\"bookPrice\":{12}," +
        //                         "\"clientCodeParam\":\"{13}\"," +
        //                         "\"useCodeParam\":\"{14}\"," +
        //                         "\"useCodeVersionParam\":\"{15}\"," +
        //                         "\"priceCode\":\"{16}\"," +
        //                         "\"bookPersonTel\":\"{17}\"," +
        //                         "\"payMethod\":\"{18}\"," +
        //                         "\"resvStatus\":\"{19}\"," +
        //                         "\"chargeDesc\":\"{20}\"",
        //                         loginMobile,
        //                         cityId,
        //                         hotelId,
        //                         inDateString,
        //                         outDateString,
        //                         bookRoomNum.ToString(),
        //                         guestNames.ToString(),
        //                         contactName,
        //                         contactTel,
        //                         roomTypeCode,
        //                         roomTypeName,
        //                         " ",
        //                         bookPrice.ToString(),
        //                         clientCode,
        //                         Setting.UseCode,
        //                         Setting.UseCodeVersion,
        //                         priceCode,
        //                         loginMobile,
        //                         "0",
        //                         resvStatus,
        //                         chargeDesc);

        //        if (isVisitor)
        //        {
        //            body += ",\"userId\":\"" + userId + "\"";
        //            body += ",\"isVisitor\":true";
        //        }

        //        body += ",\"priceInfo\":[";
        //        body += "{";
        //        body += string.Format("\"date\":\"{0}\",\"bookPrice\":{1},\"breakfastNum\":{2},\"hasNet\":{3}", inDateString, bookPrice, breakfastNum, hasNet.ToString().ToLower());
        //        body += "}";
        //        body += "]";

        //        if (ticketAmount > 0 && !string.IsNullOrEmpty(ticketUsercode))
        //        {
        //            body += ",\"ticketUsercode\":\"" + ticketUsercode + "\"";
        //            body += ",\"ticketAmount\":" + ticketAmount;
        //        }
        //        if (isGua)
        //        {
        //            body += ",\"isGua\":true";
        //            body += ",\"resvGua\":\"" + resvGua + "\"";
        //            body += ",\"resvGuaDesc\":\"" + resvGuaDesc + "\"";
        //            body += ",\"resvGuaHoldTime\":\"" + resvGuaHoldTime + "\"";
        //            body += ",\"resvCxl\":\"" + resvCxl + "\"";
        //            body += ",\"resvCxlDesc\":\"" + resvCxlDesc + "\"";
        //            body += ",\"bank\":\"" + bank + "\"";
        //            body += ",\"creditCard\":\"" + creditCard + "\"";
        //            body += ",\"validityPeriod\":\"" + validityPeriod + "\"";
        //            body += ",\"cvv2\":\"" + cvv2 + "\"";
        //            body += ",\"userName\":\"" + userName + "\"";
        //            body += ",\"credentialsType\":\"" + credentialsType + "\"";
        //            body += ",\"credentialsNum\":\"" + credentialsNum + "\"";
        //        }
        //        if (!string.IsNullOrEmpty(userHoldTime))
        //            body += ",\"userHoldTime\":\"" + userHoldTime + "\"";
        //        body = "{" + body + "}";

        //        var args = "";
        //        if (Setting.AppType == "1")
        //            args = "isTest=true&isToday=false";
        //        else if (Setting.AppType == "2")
        //            args = "isTest=true&isToday=true";

        //        var r = Request.Post(clientCode, Setting.Host + "deal/orderCreate.json", body, "2.8", args);
        //        return JsonUtility.Deserialize<Response<OrderCreate>>(r);

        //    }
        //    catch (TimeoutException)
        //    {

        //        return new Response<OrderCreate>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 取消订单 
        //public static Response<object> orderCancel(string clientCode, string loginMobile, string cnfnum, string userId)
        //{
        //    try
        //    {
        //        var body = string.Format("\"loginMobile\":\"{0}\",\"orderNum\":\"{1}\",\"reason\":\"{2}\",\"userId\":\"{3}\"", loginMobile, cnfnum, "客人取消订单", userId);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "deal/orderCancel.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<object>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<object>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 删除订单
        //public static Response<object> orderDelete(string clientCode, string loginMobile, string cnfnum)
        //{
        //    try
        //    {
        //        var body = string.Format("\"orderNums\":[\"{0}\"],\"loginMobile\":\"{1}\"", cnfnum, loginMobile);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.SearchHost + "deal/orderDelete.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<object>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<object>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 订单列表 2.0
        //public static Response<List<Order>> orderList(string clientCode, string loginMobile, int pageSize, int pageNum, string beginDate, string endDate)
        //{

        //    try
        //    {
        //        var body = string.Format("\"loginMobile\":\"{0}\",\"chooseMonth\":\"{1}\",\"pageNo\":{2},\"pageSize\":{3}",
        //                                                 loginMobile,
        //                                                  0,
        //                                                  pageNum,
        //                                                  pageSize
        //                                                  );
        //        if (!string.IsNullOrEmpty(beginDate))
        //            body += ",\"beginDate\":\"" + beginDate + "\"";
        //        if (!string.IsNullOrEmpty(endDate))
        //            body += ",\"endDate\":\"" + endDate + "\"";
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/orderList.json", body, "2.8");
        //        return JsonUtility.Deserialize<Response<List<Order>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<Order>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 订单详情 2.8
        //public static Response<OrderDetail> orderDetails(string clientCode, string cnfnum)
        //{
        //    try
        //    {
        //        var body = string.Format("\"orderNum\":\"{0}\"", cnfnum);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/orderDetails.json", body, "2.8");
        //        return JsonUtility.Deserialize<Response<OrderDetail>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<OrderDetail>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 游客订单列表 2.8
        //public static Response<List<VisitorOrder>> visitorOrderList(string clientCode, string orderNums, string userId)
        //{
        //    try
        //    {
        //        var body = string.Format("\"orderNums\":\"{0}\",\"userId\":\"{1}\"", orderNums, userId);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/visitorOrderList.json", body, "2.8");
        //        return JsonUtility.Deserialize<Response<List<VisitorOrder>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<VisitorOrder>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 优惠券查询  2.1
        //public static Response<List<Ticket>> ticketList(string clientCode, string userId, string status)
        //{
        //    try
        //    {
        //        var body = string.Format("\"userId\":\"{0}\"", userId);
        //        if (!string.IsNullOrEmpty(status))
        //            body += ",\"status\":\"" + status + "\"";
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/ticketList.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<List<Ticket>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<Ticket>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 领用优惠券
        //public static Response<TicketSave> ticketSave(string clientCode, string userId, string packageCode)
        //{
        //    try
        //    {
        //        var body = string.Format("\"userId\":\"{0}\",\"packageCode\":\"{1}\",\"clientCodeParam\":\"{2}\",\"useCodeParam\":\"{3}\"", userId, packageCode, clientCode, Setting.UseCode);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "promotion/ticketSave.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<TicketSave>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<TicketSave>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static Response<TicketSave> ticketSave2(string clientCode, string userId, string packageCode)
        //{
        //    try
        //    {
        //        var body = string.Format("\"userId\":\"{0}\",\"packageCode\":\"{1}\",\"clientCodeParam\":\"{2}\",\"useCodeParam\":\"{3}\",\"isEncode\":true", userId, packageCode, clientCode, Setting.UseCode);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "promotion/ticketSave.json", body, "2.2");
        //        return JsonUtility.Deserialize<Response<TicketSave>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<TicketSave>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 领用并检测优惠券是否可用
        //public static Response<TicketSave> ticketSaveAndCheck(string clientCode, string userId, string packageCode, string ordamt, string cityId, string hotelId, string priceCode)
        //{
        //    try
        //    {
        //        var body = string.Format("\"userId\":\"{0}\",\"packageCode\":\"{1}\",\"clientCodeParam\":\"{2}\",\"useCodeParam\":\"{3}\",\"ordamt\":\"{4}\",\"cityId\":\"{5}\",\"hotelId\":\"{6}\",\"priceCode\":\"{7}\"", userId, packageCode, clientCode, Setting.UseCode, ordamt, cityId, hotelId, priceCode);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "promotion/ticketSaveAndCheck.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<TicketSave>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<TicketSave>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////优惠券详情
        //public static Response<List<TicketDetail>> ticketDetail(string clientCode, string loginMobile, string ticketUserCodes)
        //{
        //    try
        //    {
        //        var body = string.Format("\"loginMobile\":\"{0}\",\"ticketUserCodes\":\"{1}\"", loginMobile, ticketUserCodes);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/ticketDetail.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<List<TicketDetail>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<TicketDetail>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 本次订单可用优惠券
        //public static Response<List<TicketAvailable>> ticketAvailableList(string clientCode, string userId, string ordamt, string cityId, string hotelId)
        //{
        //    try
        //    {
        //        var body = string.Format("\"userId\":\"{0}\",\"ordamt\":\"{1}\",\"clientCodeParam\":\"{2}\",\"useCodeParam\":\"{3}\",\"cityId\":\"{4}\",\"hotelId\":\"{5}\",\"useStatus\":\"{6}\"", userId, ordamt, clientCode, Setting.UseCode, cityId, hotelId, "1");
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/ticketAvailableList.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<List<TicketAvailable>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<TicketAvailable>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////现金账户查询
        //public static Response<CashUser> cashUser(string clientCode, string loginMobile)
        //{
        //    try
        //    {
        //        var body = string.Format("\"loginMobile\":\"{0}\"", loginMobile);
        //        body = "{" + body + "}";
        //        var r = Request.Post(clientCode, Setting.Host + "content/cashUser.json", body, "2.1");
        //        return JsonUtility.Deserialize<Response<CashUser>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<CashUser>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////现金账户列表
        //public static Response<List<Cash>> cashList(string clientCode, string loginMobile, string type, string status)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"loginMobile\":\"" + loginMobile + "\"");
        //        if (!string.IsNullOrEmpty(type))
        //            body.Append(",\"type\":\"" + type + "\"");
        //        if (!string.IsNullOrEmpty(status))
        //            body.Append(",\"status\":\"" + status + "\"");
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.Host + "content/cashList.json", body.ToString(), "2.1");
        //        return JsonUtility.Deserialize<Response<List<Cash>>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<List<Cash>>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////现金账户详情
        //public static Response<CashDetail> cashDetail(string clientCode, string loginMobile, string sn)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"loginMobile\":\"" + loginMobile + "\"");
        //        body.Append(",\"sn\":\"" + sn + "\"");
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.Host + "content/cashDetail.json", body.ToString(), "2.1");
        //        return JsonUtility.Deserialize<Response<CashDetail>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<CashDetail>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////提现申请
        //public static Response<CashApplyResult> cashApply(string clientCode, string loginMobile, string applyType, string bankCardNumber, string alipayAccount, string userName, string rechargePhoneNumber, double amount, string bankBranch, string bankCardOwner)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"loginMobile\":\"" + loginMobile + "\"");
        //        body.Append(",\"applyType\":" + applyType);
        //        if (!string.IsNullOrEmpty(bankCardNumber))
        //        {
        //            body.Append(",\"bankCardNumber\":\"" + bankCardNumber + "\"");
        //            body.Append(",\"bankBranch\":\"" + bankBranch + "\"");
        //            body.Append(",\"bankCardOwner\":\"" + bankCardOwner + "\"");
        //        }
        //        if (!string.IsNullOrEmpty(alipayAccount))
        //        {
        //            body.Append(",\"alipayAccount\":\"" + alipayAccount + "\"");
        //            body.Append(",\"userName\":\"" + userName + "\"");
        //        }
        //        if (!string.IsNullOrEmpty(rechargePhoneNumber))
        //        {
        //            body.Append(",\"rechargePhoneNumber\":\"" + rechargePhoneNumber + "\"");

        //        }
        //        body.Append(",\"amount\":" + amount);
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.Host + "deal/cashApply.json", body.ToString(), "2.2");
        //        return JsonUtility.Deserialize<Response<CashApplyResult>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<CashApplyResult>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 担保银行和证件类型
        //public static Response<BankDetail> getBanks(string clientCode)
        //{
        //    try
        //    {
        //        var r = Request.Get(clientCode, Setting.Host + "content/getBanks.json", null, "2.3");
        //        return JsonUtility.Deserialize<Response<BankDetail>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<BankDetail>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ////校验信用卡
        //public static Response<object> checkCreditCard(string clientCode, string bank, string creditCard, string validityPeriod, string cvv2, string userName, string credentialsType, string credentialsNum)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"bank\":\"" + bank + "\"");
        //        body.Append(",\"creditCard\":\"" + creditCard + "\"");
        //        body.Append(",\"validityPeriod\":\"" + validityPeriod + "\"");
        //        body.Append(",\"cvv2\":\"" + cvv2 + "\"");
        //        body.Append(",\"userName\":\"" + userName + "\"");
        //        body.Append(",\"credentialsType\":\"" + credentialsType + "\"");
        //        body.Append(",\"credentialsNum\":\"" + credentialsNum + "\"");
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.Host + "deal/checkCreditCard.json", body.ToString(), "2.5");
        //        return JsonUtility.Deserialize<Response<object>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<object>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //// 取得UID
        //public static Response<VisitorInfo> getUid(string clientCode, string deviceId)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"deviceId\":\"" + deviceId + "\"");
        //        body.Append(",\"regChannel\":\"" + clientCode + "\"");
        //        body.Append(",\"useCodeParam\":\"" + Setting.UseCode + "\"");
        //        body.Append(",\"useCodeVersionParam\":\"" + Setting.UseCodeVersion + "\"");
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.Host + "user/getUid.json", body.ToString(), "2.1");
        //        return JsonUtility.Deserialize<Response<VisitorInfo>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<VisitorInfo>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static Response<object> invoiceSave(string clientCode, string cnfnum, string invoiceAmount, string invoiceHead, string invoiceBody, string invoiceAddr, string contractTel, string receiverName, string userId, string loginMobile, string zipcode)
        //{
        //    try
        //    {
        //        var body = new StringBuilder();
        //        body.Append("{");
        //        body.Append("\"cnfnum\":\"" + cnfnum + "\"");
        //        body.Append(",\"invoiceAmount\":\"" + invoiceAmount + "\"");
        //        body.Append(",\"invoiceHead\":\"" + invoiceHead + "\"");
        //        body.Append(",\"invoiceBody\":\"" + invoiceBody + "\"");
        //        body.Append(",\"invoiceAddr\":\"" + invoiceAddr + "\"");
        //        body.Append(",\"contractTel\":\"" + contractTel + "\"");
        //        body.Append(",\"receiverName\":\"" + receiverName + "\"");
        //        body.Append(",\"userId\":\"" + userId + "\"");
        //        body.Append(",\"loginMobile\":\"" + loginMobile + "\"");
        //        body.Append(",\"zipcode\":\"" + zipcode + "\"");
        //        body.Append("}");
        //        var r = Request.Post(clientCode, Setting.Host + "deal/invoiceSave.json", body.ToString(), "2.2");
        //        return JsonUtility.Deserialize<Response<object>>(r);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return new Response<object>() { timeOut = true };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
   
    }
}
