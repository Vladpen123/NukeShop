using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NukeShop.WebUI.Users;

namespace NukeShop.WebUI.Components.Account
{
    public partial class AccountViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountViewComponent(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {

            return View();
        }

    }
}