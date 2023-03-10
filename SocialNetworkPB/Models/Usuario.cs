using System.ComponentModel.DataAnnotations;

namespace SocialNetworkPB.Models
{
    public class Usuario
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
