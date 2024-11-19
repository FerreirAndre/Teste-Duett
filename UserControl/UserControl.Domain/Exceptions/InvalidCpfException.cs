using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl.Domain.Exceptions
{
    public class InvalidCpfException : Exception
    {
        public InvalidCpfException() : base($"the cpf is invalid.") 
        {
            
        }
    }
}
