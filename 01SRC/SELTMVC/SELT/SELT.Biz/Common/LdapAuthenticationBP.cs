using System;
using System.Text;
using System.Collections;
using System.Web.Security;
using System.Configuration;

using System.Security.Principal;
using System.DirectoryServices;

namespace HotelVp.SELT.Domain.Biz
{
    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuthentication()
        {
            _path = "LDAP://" + ConfigurationManager.AppSettings["LdapAuthenticationPath"].ToString();
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public Hashtable GetUserInfo()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.AddRange(new string[] { "cn", "Mail", "mobile" });

            Hashtable userInfo = new Hashtable();

            try
            {
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return userInfo;
                }
                //Update the new path to the user in the directory.
                userInfo.Add("cn", (string)result.Properties["cn"][0]);
                userInfo.Add("mobile", (string)result.Properties["mobile"][0]);
                userInfo.Add("mail", (string)result.Properties["Mail"][0]);
            }
            catch (Exception ex)
            {
                return userInfo;
            }
            return userInfo;
        }

        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");

            //search.PropertiesToLoad.AddRange(new string[] { "memberOf", "Mail", "mobile" }); 用户的组  邮箱  电话

            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }

        public bool GetStatus()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("userAccountControl");

            try
            {
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                string statusNames = result.Properties["userAccountControl"][0].ToString();
                
                if ("512".Equals(statusNames))
                {
                    return true;
                }
                // 512 可用账户
                // 514 账户无效
                // 528 账户锁定
                // 8389120 密码过期

                return false;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error obtaining userAccountControl names. " + ex.Message);
            }
        }
    }
}
