using System.ComponentModel.DataAnnotations;

namespace UserControl.UI.Models
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        [UserName]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
