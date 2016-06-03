using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Controllers;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers.Tests
{
    [TestClass()]
    public class UserManagementControllerTests
    {
        [TestMethod()] 
        public void IndexTest()
        {
            UserManagementController controller = new UserManagementController();
            
            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
 
        }

        [TestMethod()] 
        public void CreateUserTest()
        {
            UserManagementController controller = new UserManagementController();
            ViewResult result = controller.CreateUser() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()] 
        public async void CreateUserTest1()
        {
            UserManagementController controller = new UserManagementController();
            UserCreateModelView mymodel = new UserCreateModelView { Name = "User", Email = "user@gmail.com", Administrator = true, Address = "adres", User = false, Researcher = false, Patient = false };
            ViewResult result = await controller.CreateUser(mymodel) as ViewResult;

            Assert.IsNotNull(mymodel);
            Assert.IsNotNull(result);
            
        }

        [TestMethod()] 
        public void EditUserTest()
        {
            int id = 1;
            UserManagementController controller = new UserManagementController();
            ViewResult view = controller.EditUser(id) as ViewResult;
            ApplicationUser user = new ApplicationUser();
            UserEditModelView mymodel = view.ViewData.Model as UserEditModelView;

            user.Name = "imie";
            user.Address = "adres";
            user.Email = "user@gmail.com";
            

            Assert.AreEqual(mymodel, view.ViewName);


        }

        [TestMethod()] //zle
        public void ResetUserPasswordTest()
        {
            UserManagementController controller = new UserManagementController();
            var mymodel = new UserEditModelView { Id = 1, Name = "name", Address = "adres", Email = "user1.@gmail.com", Password = "123" };
            ApplicationUser user = new ApplicationUser();
            RedirectToRouteResult result = controller.ResetUserPassword(mymodel) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()] 
        public void EditUserTest1()
        {
            var mymodel = new UserEditModelView { Id = 1, Name = "name", Email = "user@gmail.com", Address = "adres"};
            UserManagementController controller = new UserManagementController();
            RedirectToRouteResult result = controller.EditUser(mymodel) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}