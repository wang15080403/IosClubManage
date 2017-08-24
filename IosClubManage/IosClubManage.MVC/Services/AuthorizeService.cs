using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IosClubManage.MVC.Models;
using IosClubManage.MVC.Services;

namespace IosClubManage.MVC.Controllers
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
             if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true) 
                 && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
             {
                 var user = UserService.GetUserFromCookie();
                 if (user == null)
                 {
                     filterContext.Result = new RedirectResult("~/User/Login");
                 }
                 //else
                 //{
                 //    var controller = filterContext.RouteData.Values["Controller"].ToString();
                 //    var action = filterContext.RouteData.Values["Action"].ToString();

                 //    if (!IsSystemAllowed(customer.CustomerId, controller, action))
                 //    {
                 //        filterContext.Result = new RedirectResult("~/Home/MainIndex");
                 //    }
                 //}
             }
        }       
    }
}