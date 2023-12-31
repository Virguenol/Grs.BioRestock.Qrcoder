﻿using System.ComponentModel.DataAnnotations;

namespace Grs.BioRestock.Transfer.Requests.Identity
{
    public class UpdateProfileRequest
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}