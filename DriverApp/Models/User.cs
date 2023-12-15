using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models
{
    public class User
    {
        public  int Id { get; set; }

        public  string Username { get; set; } = null!;

        public  string Phone { get; set; } = null!;

        public  string FullName { get; set; } = null!;
        
        public  string JWT { get; set; }
    }
}
