using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NukeShop.WebUI.Users
{
    public class User : IdentityUser
    {   
        public string? FIO { get; set; }
        public DateTime RegistrationDate { get; set; }


    }
}