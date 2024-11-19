using System.ComponentModel.DataAnnotations;

namespace UserControl.UI.Models
{
    public class UserVM
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
