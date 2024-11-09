using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace MvcSocial.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Login", ResourceType = typeof(Resources.Models.User))]
        public string Login { get; set; }
        [Display(Name = "DateOfCreation", ResourceType = typeof(Resources.Models.User))]
        public string DateOfCreation { get; set; }

        public List<User> Friends { get; set; } = [];

        public override string ToString()
        {
            return $"{Login}, {DateOfCreation}";
        }
    }

}
