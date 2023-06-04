// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCWebshop.Data;

namespace MVCWebshop.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [EmailAddress, Display(Name = "Email address")]
            public string Email { get; set; }
            [Required, Display(Name = "First name")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string FirstName { get; set; }

            [Required, Display(Name = "Last name")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string LastName { get; set; }

            [Required, Display(Name = "Country")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Country { get; set; }

            [Required, Display(Name = "Postal Code")]
            [DataType(DataType.PostalCode)]
            public string PostalCode { get; set; }

            [Required, Display(Name = "City")]
            public string City { get; set; }

            [Required, Display(Name = "Street")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            public string Street { get; set; }

            [Required, Display(Name = "Number")]
            [Range(0, 99999, ErrorMessage = "Pick a correct number.")]
            public int Number { get; set; }

            [Display(Name = "Box Number")]
            public char BoxNumber { get; set; }

            [Required, Key, CreditCard, Display(Name = "Bank account nr.")]
            public string BankAccount { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Street = user.Street,
                Number = user.Number,
                BoxNumber = user.BoxNumber,
                City = user.City,
                PostalCode = user.PostalCode,
                Country = user.Country,
                BankAccount = user.BankAccount,
            
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.FirstName != user.FirstName)
                user.FirstName = Input.FirstName;
            if (Input.LastName != user.LastName)
                user.LastName = Input.LastName;
            if (Input.Email != user.Email)
                user.Email = Input.Email;
            if (Input.Street != user.Street)
                user.Street = Input.Street;
            if (Input.Number != user.Number)
                user.Number = Input.Number;
            if (Input.BoxNumber != user.BoxNumber)
                user.BoxNumber = Input.BoxNumber;
            if (Input.City != user.City)
                user.City = Input.City;
            if (Input.PostalCode != user.PostalCode)
                user.PostalCode = Input.PostalCode;
            if (Input.Country != user.Country)
                user.Country = Input.Country;
            if (Input.BankAccount != user.BankAccount)
                user.BankAccount = Input.BankAccount;
            
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
