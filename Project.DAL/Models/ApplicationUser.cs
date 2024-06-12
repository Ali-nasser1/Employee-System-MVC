﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; 
namespace Project.DAL.Models
{
	public class ApplicationUser : IdentityUser 
	{
        public string FName { get; set; }
        public string LName { get; set; }
        public  bool IsAgree { get; set; }

    }
}
