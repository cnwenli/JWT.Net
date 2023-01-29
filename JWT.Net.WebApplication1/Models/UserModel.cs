using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Net.WebApplication1.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
