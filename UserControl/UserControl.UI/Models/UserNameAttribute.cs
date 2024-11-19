using System;
using System.ComponentModel.DataAnnotations;

namespace UserControl.UI.Models
{
    internal class UserNameAttribute : ValidationAttribute
    {
        public UserNameAttribute() : base("User cannot contain spaces.") { }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var userName = value.ToString();

            if (userName.Contains(" "))
            {
                return false;
            }

            return true;
        }
    }
}