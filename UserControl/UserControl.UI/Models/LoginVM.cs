﻿using System.ComponentModel.DataAnnotations;

namespace UserControl.UI.Models
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password {  get; set; } = string.Empty;
    }
}
