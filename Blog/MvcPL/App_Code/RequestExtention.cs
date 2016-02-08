using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcPL.App_Code
{
    public static class RequestExtention
    {
        public static string GetCurrentUserLogin(this HttpRequestBase request)
        {
            //TODO change this method to User.Identity.Name
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return null;
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket?.Name;
        }
    }
}