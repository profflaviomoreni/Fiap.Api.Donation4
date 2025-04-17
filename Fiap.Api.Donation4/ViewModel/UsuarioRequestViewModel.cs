using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.ViewModel
{
    public class UsuarioRequestViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo 100 caracteres.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        [StringLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres.")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A regra é obrigatória.")]
        [StringLength(50, ErrorMessage = "A regra deve ter no máximo 50 caracteres.")]
        public string Regra { get; set; }
    }
}

