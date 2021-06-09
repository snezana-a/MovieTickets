﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Identity
{
    public class ManageRoles
    {
        public List<AppUser> Admins { get; set; }
        public List<AppUser> Users { get; set; }
        public Role UserRole { get; set; } 
    }

    public enum Role
    {
        Admin,
        Customer
    }
}