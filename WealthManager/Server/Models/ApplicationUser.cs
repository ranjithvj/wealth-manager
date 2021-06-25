using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WealthManager.Server.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Name { get; set; }
    }
}
