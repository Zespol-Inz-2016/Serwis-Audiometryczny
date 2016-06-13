﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Klasa prezentująca model dla index.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Zmienna czy użytkownik ma hasło.
        /// </summary>
        public bool HasPassword { get; set; }
        /// <summary>
        /// Lista loginów użytkowników.
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }
        /// <summary>
        /// Zmienna numer telefonu użytkownika.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Zmienna czy jest właczona dwustopniowa autoryzacja, nie zaimplementowane.
        /// </summary>
        public bool TwoFactor { get; set; }
        /// <summary>
        /// Zmienna czy po zalogowaniu mamy pamietać przeglądarkę.
        /// </summary>
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }
    /// <summary>
    /// Klasa będąca modelem do zmiany hasła.
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// Zmienna z nowym hasłem.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        /// <summary>
        /// Zmienna potwierdzająca nowe hasło.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Klasa będąca modelem do zmiany hasła przez użytkownika.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Zmienna ze starym hasłem.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }
        /// <summary>
        /// Zmienna z nowym hasłem.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        /// <summary>
        /// Zmienna potwierdzająca nowe hasło.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Model numeru telefonu.
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// Zmienna numer telefonu.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
    /// <summary>
    /// Model weryfikacji numeru telefonu.
    /// </summary>
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        /// Zmienna numer kierunkowy.
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        /// <summary>
        /// Zmienna numer telefonu.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    /// <summary>
    /// Klasa związana z dwustopniową autoryzacją, nie wykorzystywana w systemie. Można użyć w przyszłości.
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}