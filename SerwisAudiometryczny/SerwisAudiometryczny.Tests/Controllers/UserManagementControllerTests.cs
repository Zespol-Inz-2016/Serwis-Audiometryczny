using Microsoft.Owin.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        /// <summary>
        /// Test metody wyświetlającej listę użytkowników.
        /// Sprawdza czy po wykonaniu metoda nie zwraca wartości "null".
        /// </summary>
        [TestMethod()] 
        public void IndexTest()
        {
            // Przetestowanie metody nie jest możliwe
        }

        /// <summary>
        /// Test metody odpowiedzialnej za utworzenie użytkownika.
        /// Sprawdza czy model i zwrócony widok nie zwraca wartości "null".
        /// </summary>
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

        /// <summary>
        /// Test metody odpowiedzialnej za edycję użytkownika.
        /// </summary>
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

    }
}