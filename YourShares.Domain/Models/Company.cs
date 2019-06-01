﻿using System;

namespace YourShares.Domain.Models
{
    public class Company
    {
        // Empty constructor for EF
        public Guid Id { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Capital { get; set; }
        public long? TotalShares { get; set; }
        public long? OptionPoll { get; set; }
        public Guid AdminId { get; set; }
    }
}