using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny;
using SerwisAudiometryczny.Controllers;
using SerwisAudiometryczny.Models;
using System;
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
            var model= new LoginViewModel{Email="user123@gmail.com", Password="somepass12", RememberMe=false};
            controller = new AccountController();
            string url = "//adres_serwera/sciezka_dostępu";
            ViewResult view =await controller.Login(model,url) as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(model);

        }


     
        [TestMethod()]
        public async void VerifyCodeTest()
        {
            string provider="someProvider";
            string returnUrl = "//adres_serwera/sciezka_dostępu";
            bool rememberMe=true;
            var myModel = new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe };
            controller = new AccountController();
            ViewResult view = await controller.VerifyCode(myModel.Provider,myModel.ReturnUrl,myModel.RememberMe) as ViewResult ;
            Assert.IsNotNull(myModel);
            Assert.AreEqual(myModel, view);
           
        }

        [TestMethod()]
        public async void VerifyCodeTest1()
        {
            controller = new AccountController();
            string url = "//adres_serwera/sciezka_dostępu";

        }
        [TestMethod()]
        public void VerifyCodeCaseSuccess1()
        {
            controller = new AccountController();
            string url = "//adres_serwera/sciezka_dostępu";
            ViewResult view = controller.Login(url) as ViewResult;
            Assert.IsNotNull(view);
            VerifyCodeViewModel model = view.ViewData.Model as VerifyCodeViewModel;
            Assert.IsNotNull(model);
           
        }

        [TestMethod()]
        public void VerifyCodeCaseLockedOut1()
        {
            LoginViewModel model = new LoginViewModel();
            string url = "//adres_serwera/sciezka_dostępu";
        }

        [TestMethod()]
        public void VerifyCodeCaseRequiresVerification1()
        {
            VerifyCodeViewModel model;
      

        }
      

        [TestMethod()]
        public async void RegisterTest1()
        {
            String email = "user123@gmail.com";
            String password = "Pass1234";
            var model = new RegisterViewModel { Email = email, Password = password, ConfirmPassword = password };

        }
        public async void ConfirmEmailTest()
        {
            string id="123";
            string code="123331x3";
        }

        [TestMethod()]
        public void ResetPasswordTest()
        {
            AccountController controller = new AccountController();
            String value = null;
            controller = new AccountController();
            ViewResult view = controller.ResetPassword(value) as ViewResult;

        }

        [TestMethod()]
        public void ResetPasswordTest1()
        {
            AccountController controller = new AccountController();
            String value = null;
           ViewResult view= controller.ResetPassword(value) as ViewResult;          
        }


        [TestMethod()]
        public void ExternalLoginTest()
        {
            controller = new AccountController();
            string url = "//adres_serwera/sciezka_dostępu";
            string provider="someProvider";
            ViewResult view= controller.ExternalLogin(provider, url) as ViewResult;
            Assert.IsNotNull(view);
        }

        [TestMethod()]
        public async void SendCodeTest()
        { string url = "//adres_serwera/sciezka_dostępu";
            bool rememberMe = false;
        }

        [TestMethod()]
        public void SendCodeTest1()
        {
            string url = "//adres_serwera/sciezka_dostępu";
            ICollection<System.Web.Mvc.SelectListItem> providers = new List<System.Web.Mvc.SelectListItem>();
           
            bool rememberMe = false;
            var model = new SendCodeViewModel { Providers=providers, ReturnUrl=url, RememberMe=rememberMe};
            controller = new AccountController();
        }

        [TestMethod()]
        public void ExternalLoginCallbackTest()
        {
            
        }

        [TestMethod()]
        public void ExternalLoginConfirmationTest()
        {
            
        }

        [TestMethod()]
        public void LogOffTest()
        {
           
        }
      
    }
}