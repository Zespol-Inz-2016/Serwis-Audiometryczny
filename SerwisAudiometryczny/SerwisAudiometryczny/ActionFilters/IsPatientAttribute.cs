using Microsoft.AspNet.Identity;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisAudiometryczny.ActionFilters
{
    public class IsPatientAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            var db = ApplicationDbContext.Create();
            int currentUserId = httpContext.User.Identity.GetUserId<int>();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            return currentUser.Patient;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
            filterContext.HttpContext.Response.StatusCode = 403;
        }
    }
}