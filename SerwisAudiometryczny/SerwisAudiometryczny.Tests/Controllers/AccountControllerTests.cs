using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny;
using SerwisAudiometryczny.Controllers;
using SerwisAudiometryczny.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        AccountController controller;
        [TestMethod()]
        public void LoginTest()
        {
            controller = new AccountController();
            ViewResult view = controller.Login("//adres_serwera/sciezka_dostępu") as ViewResult;
            Assert.IsNotNull(view);
        }

        [TestMethod()]
        public async void LoginTest1()
        {
            var model = new LoginViewModel { Email = "user123@gmail.com", Password = "somepass12" };
            controller = new AccountController();
            string url = "//adres_serwera/sciezka_dostępu";
            ViewResult view = await controller.Login(model, url) as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(model);

        }

        [TestMethod()]
        public void LogOffTest()
        {
            controller = new AccountController();
            RedirectToRouteResult result = controller.LogOff() as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void EditTest()
        {
            controller = new AccountController();
            ViewResult view = controller.Edit() as ViewResult;
            AccountEditViewModel model = view.ViewData.Model as AccountEditViewModel;
            ApplicationUser user = new ApplicationUser();
            user.Name = "name";
            user.Address = "address";
            user.Email = "user123@gmail.com";
            model.Name = user.Name;
            model.Address = user.Address;
            model.Email = user.Address;
            Assert.AreEqual(model, view.ViewName);

        }


        [TestMethod()]
        public void EditTest1()
        {
            var model = new AccountEditViewModel { Name="name", Password="somepass123", Address="adress", Email="user1234@gmail.com"};
            controller = new AccountController();
            ViewResult view = controller.Edit(model) as ViewResult;
            Assert.AreEqual(model, view.ViewName);
        }

        [TestMethod()]
        public void Details()
        {
           
            var model = new AccountEditViewModel { Name = "name", Password = "somepass123", Address = "adress", Email = "user1234@gmail.com" };
            controller = new AccountController();
            ViewResult view = controller.Details() as ViewResult;
            Assert.IsNull(view);
            Assert.AreEqual(model, view.ViewName);
        }
       
   }
}