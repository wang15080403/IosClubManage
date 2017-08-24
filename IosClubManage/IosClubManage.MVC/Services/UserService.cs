using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;
using IosClubManage.MVC.Models;
using IosClubManage.MVC.ViewModels;
using IosClubManage.MVC.DBHelper;

namespace IosClubManage.MVC.Services
{
    public static class UserService
    {
        public static User GetUserFromCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            string loginCookie = CookieHelper.GetCookie(cookieName);
            if (loginCookie != null)
            {
                User loginUser = new User();
                FormsAuthenticationTicket authticket = FormsAuthentication.Decrypt(loginCookie);
                var authdata = StringHelper.GetStrArray(authticket.UserData);
                loginUser.Id = Guid.Parse(authdata[0]);
                loginUser.Name = authdata[2];
                loginUser.UserCode = authdata[1];
                return loginUser;
            }
            return null;
        }


        /// <summary>
        /// 根据用户名（工号）、密码验证合法性
        /// </summary>        
        public static User Login(string LoginId, string password)
        {
            using (IosClubDbContext db = new IosClubDbContext())
            {

                var user = db.Users.Where(p => p.UserCode == LoginId).FirstOrDefault();

                if (user == null )
                {
                    return null;
                }
                return user;
            }
        }
        public static void SetAuthTicketCookie(User user, bool isRemeberMe)
        {
            string authData = user.Id.ToString() + "," + user.UserCode + "," + user.Name ;

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Name , DateTime.Now, DateTime.Now.AddDays(365), isRemeberMe, authData);
            string loginTicket = FormsAuthentication.Encrypt(authTicket);

            if (isRemeberMe)
            {
                CookieHelper.WriteCookie(FormsAuthentication.FormsCookieName, loginTicket, authTicket.Expiration);
            }
            else
            {
                CookieHelper.WriteCookie(FormsAuthentication.FormsCookieName, loginTicket);
            }
        }

        /// <summary>
        /// 设置登陆认证Cookie
        /// </summary>
        public static void LoginOut()
        {
            string cookieName = FormsAuthentication.FormsCookieName;

            FormsAuthentication.SignOut();
            CookieHelper.DelCookie(cookieName);
        }
    }
}