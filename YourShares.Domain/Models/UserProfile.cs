﻿using System;
using System.Collections.Generic;

namespace YourShares.RestApi.Models
{
    public partial class UserProfile
    {
        public Guid UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}