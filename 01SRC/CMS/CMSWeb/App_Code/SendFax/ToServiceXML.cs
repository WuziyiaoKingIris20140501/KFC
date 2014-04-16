using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ToServiceXML2 的摘要说明
/// </summary>
public class ToServiceXML
{
    private static string UserName = ConfigurationManager.AppSettings["SendFaxUserName"].ToString();
    private static string UserPwd = ConfigurationManager.AppSettings["SendFaxUserPwd"].ToString();
    public ToServiceXML()
    {
        //
    }
    public static string getSendFaxToServerXMLStr(string path, string fileName, string ContentType, string ClientTaskID, string FaxNumber)
    {
        string base64 = ToBase64.EncodingForFile(path);
        string sendFaxXml = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                      " +
                            "<FaxInfo xmlns=\"http://202.22.252.4\"                     " +
                            " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"        " +
                            " xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\">" +
                            "<SchemaVersion>1.1</SchemaVersion>                           " +
                            "<Login>                                                      " +
                            "<UserID>" + UserName + "</UserID>                                       " +
                            "<Password>" + UserPwd + "</Password>                                 " +
                            "</Login>                                                     " +
                            "<FaxOptions>                                                 " +
                            "<Resolution>high</Resolution>                                " +
                            "<Priority>0</Priority>                                       " +
                            "<FeedBackMode>0</FeedBackMode>                               " +
                            "<FaxToneDetectTimeout>17</FaxToneDetectTimeout>              " +
                            "<PromptVoiceChoice>-1</PromptVoiceChoice>                    " +
                            "<FaxSidFlag>0</FaxSidFlag>                                   " +
                            "<FaxSid>aafdasfdas</FaxSid>                                  " +
                            "<StartSecond1>-1</StartSecond1>                              " +
                            "<EndSecond1>-1</EndSecond1>                                  " +
                            "<StartSecond2>-1</StartSecond2>                              " +
                            "<EndSecond2>-1</EndSecond2>                                  " +
                            "</FaxOptions>                                              " +
                            "<SendTaskList>                                               " +
                            "<TotalNum>10</TotalNum>                                      ";
        System.Text.StringBuilder sendTaskStr = new System.Text.StringBuilder();
        for (int i = 8; i < 9; i++)
        {
            sendTaskStr.Append("<SendTask>" +
            "<ClientTaskID>" + ClientTaskID + "</ClientTaskID>" +
            "<FaxNumber>" + FaxNumber + "</FaxNumber>" +
            "</SendTask>");
        }
        sendFaxXml = sendFaxXml + sendTaskStr.ToString() + "</SendTaskList>" +
        "<DocumentList>                                               " +
        "     <FileNum>1</FileNum>                                    " +
        "<Document      FileName=\"" + fileName + "\"                     " +
        "ContentType=\"" + ContentType + "\"                           " +
        "EncodingType=\"Unicode\"                                      " +
        "DocumentExtension=\"" + ContentType + "\">                                   " +
         base64 +
        "</Document>                                                  " +
        "</DocumentList>                                              " +
        "</FaxInfo>                                                   ";
        return sendFaxXml;
    }

    public static string getSendFaxPHToServerXMLStr()
    {
        string base64 = ToBase64.EncodingForFile("D:\\webservice测试.doc");
        string sendFaxXml = "<?xml version=\"1.0\" encoding=\"gb2312\" ?>                                                       " +
                            "<FaxInfo xmlns=\"http://202.22.252.4\"                                                                                        " +
                            "   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"                                                                        " +
                            "   xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\">                                                                " +
                            "   <SchemaVersion>1.1</SchemaVersion>                                                                                             " +
                            "   <Login>                                                                                                                        " +
                            "   <UserID>00000001</UserID>                                       " +
                            "   <Password>1111111</Password>                                 " +
                            "   </Login>                                                                                                                       " +
                            "   <FaxOptions>                                                                                                                   " +
                            "   <Resolution>high</Resolution>                                                                                                  " +
                            "   <Priority>0</Priority>                                                                                                         " +
                            "   <FeedBackMode>0</FeedBackMode>                                                                                                 " +
                            "   <FaxToneDetectTimeout>17</FaxToneDetectTimeout>                                                                                " +
                            "   <PromptVoiceChoice>-1</PromptVoiceChoice>                                                                                      " +
                            "   <FaxSidFlag>0</FaxSidFlag>                                                                                                     " +
                            "  <FaxSid>aafdasfdas</FaxSid>                                                                                                     " +
                            "  <StartSecond1>-1</StartSecond1>                                                                                                 " +
                            "  <EndSecond1>-1</EndSecond1>                                                                                                     " +
                            "  <StartSecond2>-1</StartSecond2>                                                                                                 " +
                            "  <EndSecond2>-1</EndSecond2>                                                                                                     " +
                            "  </FaxOptions>                                                                                                                   " +
                            "  <SendTaskList>                                                                                                                  " +
                            "  	<TotalNum>3</TotalNum>                                                                                                         " +
                            "  	<SendTask>                                                                                                                     " +
                            "    	<ClientTaskID>1</ClientTaskID>                                                                                               " +
                            "    	<FaxNumber>02168416178</FaxNumber>                                                                                           " +
                            "       <GuestName></GuestName>" +
                            "       <GuestCompany></GuestCompany>" +
                            " 	 </SendTask>                                                                                                                   " +
                            "  	<SendTask>                                                                                                                     " +
                            "    	<ClientTaskID>2</ClientTaskID>                                                                                               " +
                            "    	<FaxNumber>02168416188</FaxNumber>                                                                                         " +
                            "       <GuestName></GuestName>" +
                            "       <GuestCompany></GuestCompany>" +
                            "   </SendTask>                                                                                                            " +
                            "   <SendTask>                                                                                                        " +
                            "    	<ClientTaskID>3</ClientTaskID>                                                                                               " +
                            "   	<FaxNumber>02151714674</FaxNumber>                                                                                           " +
                            "       <GuestName>1223</GuestName>" +
                            "       <GuestCompany>leo</GuestCompany>" +
                            "  	</SendTask>                                                                                                                    " +
                            "  </SendTaskList>                                                                                                                 " +
                            "  <DocumentList>                                                                                                                  " +
                            "      <FileNum>1</FileNum>                                                                                                        " +
                            " 			<Document ContentType=\"application/msword\" FileName=\"webservice测试.doc\" EncodingType=\"base64\" DocumentExtension=\"doc\">    " +
                            base64 +
                            "			</Document>                                                                                                                " +
                            "  </DocumentList>                                                                                                                 " +
                            " </FaxInfo>                                                                                                                       ";
        return sendFaxXml;
    }

    public static string getQueryResultForSendTaskXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                         " +
                            "<FaxInfo xmlns=\"http://202.22.252.4\"                        " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"         " +
                            "  xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\"> " +
                            " <QueryResultForSendTask>                                       " +
                            "    <Login>                                                     " +
                            "      <UserID>50060855</UserID>                                     " +
                            "      <Password>000000</Password>                               " +
                            "    </Login>" +
                            "  <FaxClientIDListFilter>0,1431</FaxClientIDListFilter> " +
                            "  <JobNoListFilter/> " +
                            "  <StartTimeFilter/>" +
                            "  <EndTimeFilter/> " +
                            "  </QueryResultForSendTask>   " +
                            "</FaxInfo>                                                      ";

        return sendFaxXML;
    }

    public static string getQueryResultForRecvTaskXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                         " +
                            "<FaxInfo xmlns=\"http://61.152.243.43\"                        " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"         " +
                            "  xsi:schemaLocation=\"http://61.152.243.43 sfax_schema.xsd\"> " +
                            " <QueryResultForRecvTask>                                       " +
                            " <Login>                                                        " +
                            "   <UserID>00000001</UserID>                                        " +
                            "   <Password>11</Password>                                  " +
                            " </Login>                                                       " +
                            " <FaxNumbersListFilter></FaxNumbersListFilter>                  " +
                            " <StartTimeFilter></StartTimeFilter>                            " +
                            " <EndTimeFilter></EndTimeFilter>                                " +
                            " </QueryResultForRecvTask>                                      " +
                            "</FaxInfo>                                                      ";
        return sendFaxXML;
    }

    public static string getQueryResultForRecvTaskZipXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                          " +
                            "<FaxInfo xmlns=\"http://202.22.252.4\"                         " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"          " +
                            "  xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\">  " +
                            "<QueryResultForRecvTaskZip>                                      " +
                            "  <Login>                                                        " +
                            "     <UserID>00000001</UserID>                                       " +
                            "     <Password>000000</Password>                                 " +
                            "  </Login>                                                       " +
                            "  <FaxNumbersListFilter></FaxNumbersListFilter>            " +
                            "  <StartTimeFilter>2008-01-01</StartTimeFilter>                      " +
                            "  <EndTimeFilter></EndTimeFilter>                          " +
                            "  </QueryResultForRecvTaskZip>                                   " +
                            "</FaxInfo>                                                       ";
        return sendFaxXML;
    }

    public static string getDeleteFileForSendTaskXMLstr()
    {
        string sengFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                       " +
                            "<FaxInfo xmlns=\"http://202.22.252.4\"                      " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"       " +
                            "  xsi:schemaLocation=\"http://202.22.252.4/fax_schema.xsd\">" +
                            "  <DeleteFileForSendTask>                                     " +
                            "  <Login>                                                     " +
                            "    <UserID>00000001</UserID>                                     " +
                            "    <Password>000000</Password>                               " +
                            "  </Login>                                                    " +
                            "  <JobNoListFilter></JobNoListFilter>                   " +
                            "  <StartTimeFilter>2007-01-01</StartTimeFilter>                   " +
                            "  <EndTimeFilter></EndTimeFilter>                       " +
                            "  </DeleteFileForSendTask>                                    " +
                            "</FaxInfo>                                                    ";
        return sengFaxXML;
    }

    public static string getDeleteFileForRecvTaskXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                         " +
                            "<FaxInfo xmlns=\"http://202.22.252.4\"                        " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"         " +
                            "  xsi:schemaLocation=\"http://202.22.252.4 /fax_schema.xsd\"> " +
                            "<DeleteFileForRecvTask>                                         " +
                            "  <Login>                                                       " +
                            "    <UserID>00000001</UserID>                                      " +
                            "    <Password>000000</Password>                                 " +
                            "  </Login>                                                      " +
                            "  <FaxNumberListFilter></FaxNumberListFilter>                   " +
                            "  <ServerTaskIDListFilter></ServerTaskIDListFilter>             " +
                            "  <StartTimeFilter>2007-01-01</StartTimeFilter>                           " +
                            "  <EndTimeFilter></EndTimeFilter>                               " +
                            "  </DeleteFileForRecvTask>                                      " +
                            "</FaxInfo>                                                      ";
        return sendFaxXML;
    }
    public static string addDeptInfoXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                        " +
                            "<DeptInfo xmlns=\"http://www.35fax.net\"                        " +
                            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"            " +
                            "xsi:schemaLocation=\"http://www.uniproud.com/fax_schema.xsd\">     " +
                            "<AddDeptInfo>                                                      " +
                            "<Login>                                                            " +
                            "<UserID>root</UserID>                                              " +
                            "<Password>35fax88</Password>                                        " +
                            "</Login>                                                           " +
                            "<DeptInfo>                                                         " +
                            "<DeptMgrNo>123321</DeptMgrNo>                                      " +
                            "<DeptMgrName>ceshi</DeptMgrName>                                  " +
                            "<Remark>xixihaha</Remark>                                            " +
                            "</DeptInfo>                                                        " +
                            "</AddDeptInfo>                                                     " +
                            "</DeptInfo>                                                        ";
        return sendFaxXML;
    }
    public static string modifyDeptInfoXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                               " +
                            "<DeptInfo xmlns=\"http://www.35fax.net\"                             " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"               " +
                            "  xsi:schemaLocation=\"http://www.uniproud.com/fax_schema.xsd\">        " +
                            "<ModifyDeptInfo>                                                      " +
                            "<Login>                                                               " +
                            "<UserID>root</UserID>                                                 " +
                            "<Password>35fax88</Password>                                           " +
                            "</Login>                                                              " +
                            "  <DeptInfo>                                                          " +
                            "      <DeptMgrNo>123321</DeptMgrNo>                                   " +
                            "      <DeptMgrName>testtest</DeptMgrName>                               " +
                            "      <Remark>heihei</Remark>                                         " +
                            "   </DeptInfo>                                                        " +
                            "</ModifyDeptInfo>                                                     " +
                            "</DeptInfo>                                                           ";
        return sendFaxXML;
    }
    public static string addUserInfoXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                            " +
                            "<UserInfo xmlns=\"http://www.35fax.net\"                            " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"              " +
                            "  xsi:schemaLocation=\"http://www.uniproud.com/fax_schema.xsd\">       " +
                            "<AddUserInfo>                                                        " +
                            "<Login>                                                              " +
                            "<UserID>root</UserID>                                                " +
                            "<Password>35fax88</Password>                                          " +
                            "</Login>                                                             " +
                            "  <UserInfo>                                                         " +
                            "      <UserId>leo</UserId>                                        " +
                            "      <UserName>leo</UserName>                                    " +
                            "      <Password>leo</Password>                                    " +
                            "<DeptMgrNo>123321</DeptMgrNo>                                        " +
                            "      <MobilePhone></MobilePhone>                              " +
                            "      <DidNumber>8888</DidNumber>                                  " +
                            "      <MobilePhone></MobilePhone>                              " +
                            "      <CommPhone></CommPhone>                                  " +
                            "      <CommFax></CommFax>                                      " +
                            "      <CommEmail></CommEmail>                                  " +
                            "      <Address></Address>                                      " +
                            "      <ZipCode></ZipCode>                                      " +
                            "      <SendAuthLevel>1</SendAuthLevel>                               " +
                            "<BEmail2Fax>1</BEmail2Fax>                                           " +
                            "      <BFax2Email>1</BFax2Email>                                     " +
                            "      <BFax2SMS>1</BFax2SMS>                                         " +
                            "      <UserAlias></UserAlias>                                  " +
                            "      <DetectFaxToneTime>17</DetectFaxToneTime>                       " +
                            "      <bSendFax>1</bSendFax>                                    " +
                            "<IsShowAllRecvFax>1</IsShowAllRecvFax>                          " +
                            "   </UserInfo>                                                       " +
                            "</AddUserInfo>                                                       " +
                            "</UserInfo>                                                          ";
        return sendFaxXML;
    }
    public static string getGetUserInfoXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>                        " +
                            "<UserInfo xmlns=\"http://202.22.252.4\"                     " +
                            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"         " +
                            "xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\"> " +
                            "<GetUserInfo>                                                 " +
                            "<Login>                                                       " +
                            "<UserID>tiddy</UserID>                                         " +
                            "<Password>111111</Password>                                   " +
                            "</Login>                                                      " +
                            "</GetUserInfo>                                                " +
                            "</UserInfo>                                                   ";
        return sendFaxXML;
    }

    public static string getChangePasswordXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                                 " +
                            "<UserInfo xmlns=\"http://202.22.252.4\"                               " +
                            "          xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"         " +
                            "          xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\"> " +
                            "<ChangePassword>                                                        " +
                            "  <Login>                                                               " +
                            "    <UserID>00000001</UserID>                                               " +
                            "    <Password>000000</Password>                                         " +
                            "  </Login>                                                              " +
                            "  <NewPassword>111111</NewPassword>                                     " +
                            "</ChangePassword>                                                       " +
                            "</UserInfo>                                                             ";
        return sendFaxXML;
    }

    public static string getModifyUserInfoXMLstr()
    {
        string sendFaxXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?>                        " +
                            "<UserInfo xmlns=\"http://202.22.252.4\"                      " +
                            "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"        " +
                            "  xsi:schemaLocation=\"http://202.22.252.4 sfax_schema.xsd\">" +
                            "  <ModifyUserInfo>                                             " +
                            "  <Login>                                                      " +
                            "     <UserID>00000001</UserID>                                     " +
                            "     <Password>000000</Password>                               " +
                            "  </Login>                                                     " +
                            "  <UserInfo>                                                   " +
                            "      <MobilePhone>string</MobilePhone>                        " +
                            "      <CommPhone>string</CommPhone>                            " +
                            "      <CommFax>string</CommFax>                                " +
                            "      <CommEmail>string</CommEmail>                            " +
                            "      <Address>string</Address>                                " +
                            "      <ZipCode>string</ZipCode>                                " +
                            "      <SendAuthLevel>1</SendAuthLevel>                         " +
                            "      <BFax2Email>1</BFax2Email>                               " +
                            "      <BEmail2Fax>1</BEmail2Fax>                               " +
                            "      <BFax2SMS>1</BFax2SMS>                                   " +
                            "      <UserAlias>string</UserAlias>                            " +
                            "      <PromptVoiceChoice> 1 </PromptVoiceChoice>               " +
                            "      <DetectFaxToneTime>1</DetectFaxToneTime>                 " +
                            "   </UserInfo>                                                 " +
                            "   </ModifyUserInfo>                                           " +
                            "</UserInfo>                                                    ";
        return sendFaxXML;
    }
}
