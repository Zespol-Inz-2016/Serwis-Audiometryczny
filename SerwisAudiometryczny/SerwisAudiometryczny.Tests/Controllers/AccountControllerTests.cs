using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Models;
using System.Web.Mvc;
using System.Web;
using Moq;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.IO;



namespace SerwisAudiometryczny.Controllers.Tests
{
    /// <summary>
    /// Klasa obługująca testy konta użytkowników. 
    /// </summary>
    [TestClass()]
    public class AccountControllerTests
    {      
        AccountController controller;

        /// <summary>
        /// Test metody odpowiedzialnej za przygotowanie do logowania; 
        /// sprawdza czy zwracany widok nie jest null
        /// </summary>
        [TestMethod()]
        public void LoginTest()
        {
           
            controller = new AccountController();
            ViewResult view = controller.Login("//adres_serwera/sciezka_dostępu") as ViewResult;
            Assert.IsNotNull(view);
        }
          

        /// <summary>
        /// Test metody odpowiedzialnej za logowanie.
        /// Sprawdza czy model i widok zwrócony nie jest null.
        /// </summary>
        [TestMethod()]
        public async void LoginTest1()
        {
            var model = new LoginViewModel { ID = "2", Password = "somepass12" };
            controller = new AccountController();
            string url = "//adres_serwera/sciezka_dostępu";
            ViewResult view = await controller.Login(model, url) as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(model);

        }
        
        /// <summary>
        /// Test metody odpowiedzialnej za wylogowywanie.
        /// Testuje poprawność mocków przekazanych do konstruktora controllera oraz metodę wylogowującą
        /// </summary>
        [TestMethod()]
        public void LogOffTest()
           {
            var dbContext = new Mock<ApplicationDbContext>();
            var authenticationManager = new Mock<IAuthenticationManager>();
            authenticationManager.Setup(am => am.SignOut());
            authenticationManager.Setup(am => am.SignIn());
            var userStore = new Mock<CustomUserStore>(dbContext.Object);
            var obj1 = new Mock<ApplicationUserManager>(userStore.Object);
            var obj = new Mock<ApplicationSignInManager>(obj1.Object, authenticationManager.Object);

            controller = new AccountController(obj1.Object, obj.Object);
            obj1.VerifyAll();
            obj.VerifyAll();
            
            var result = controller.LogOff();
            Assert.IsNotNull(result); 

        }


        /// <summary>
        /// Test metody przygotowywującej edycję użytkownika.
        /// Weryfikacja mocków przekazanych do konstruktora oraz metody edytującej
        /// </summary>
        [TestMethod()]
        public void EditTest()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var authenticationManager = new Mock<IAuthenticationManager>();
            var userStore = new Mock<CustomUserStore>(dbContext.Object);
            var obj1 = new Mock<ApplicationUserManager>(userStore.Object);
            var obj = new Mock<ApplicationSignInManager>(obj1.Object, authenticationManager.Object);
           
            controller = new AccountController(obj1.Object, obj.Object);
            obj1.VerifyAll();
            obj.VerifyAll();
            var result= controller.Edit();
            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Test metody odpowiedzialnej za edycje użytkownika.
        /// Weryfikacja mocków przekazanych do konstruktora, sprawdzenie czy model edycji jest prawidłowo tworzony.
        /// </summary>
        [TestMethod()]
        public void EditTest1()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var authenticationManager = new Mock<IAuthenticationManager>();
            var userStore = new Mock<CustomUserStore>(dbContext.Object);
            var obj1 = new Mock<ApplicationUserManager>(userStore.Object);
            var obj = new Mock<ApplicationSignInManager>(obj1.Object, authenticationManager.Object);
            obj1.VerifyAll();
            obj.VerifyAll();

            controller = new AccountController(obj1.Object, obj.Object);   
                  
            var model = new AccountEditViewModel { Name="name", Password="somepass123", Address="adress", Email="user1234@gmail.com"};        
            Assert.IsNotNull(model);

        }
        /// <summary>
        /// Test metody zwracającej szczegóły użytkownika.
        /// </summary>
        [TestMethod()]
        public void Details()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var authenticationManager = new Mock<IAuthenticationManager>();
            var userStore = new Mock<CustomUserStore>(dbContext.Object);
            var obj1 = new Mock<ApplicationUserManager>(userStore.Object);
            var obj = new Mock<ApplicationSignInManager>(obj1.Object, authenticationManager.Object);
            controller = new AccountController(obj1.Object, obj.Object);
            obj1.VerifyAll();
            obj.VerifyAll();          
        }
       
   }
}