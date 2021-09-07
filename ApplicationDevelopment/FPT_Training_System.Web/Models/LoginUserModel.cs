using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Training_System.Web.Models
{
    public class LoginUserModel
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}