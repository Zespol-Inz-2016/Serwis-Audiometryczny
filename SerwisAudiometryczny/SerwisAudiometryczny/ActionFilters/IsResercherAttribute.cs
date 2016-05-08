using Microsoft.AspNet.Identity;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisAudiometryczny.ActionFilters
{
    public class IsResercherAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            var db = ApplicationDbContext.Create();
            string currentUserId = httpContext.User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            return currentUser.Researcher;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
            filterContext.HttpContext.Response.StatusCode = 403;
        }
    }
}