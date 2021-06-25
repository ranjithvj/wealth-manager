using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Shared.ViewModels
{
    public class UserAuthInfoVM
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> ExposedClaims { get; set; }
    }
}
