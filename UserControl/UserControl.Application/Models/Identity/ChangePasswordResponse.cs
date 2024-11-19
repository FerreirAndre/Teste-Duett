using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl.Application.Models.Identity
{
    public class ChangePasswordResponse 
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}
