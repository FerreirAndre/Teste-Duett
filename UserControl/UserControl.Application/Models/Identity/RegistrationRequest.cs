﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Cpf {  get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
