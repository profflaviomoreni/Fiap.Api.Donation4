using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.ViewModel
{
    public class LoginRequestVM
    {

        [Required(ErrorMessage = "Email é requerido")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "Senha é requerida")]
        public string Senha { get; set; }


        public LoginRequestVM()
        {
                
        }

        public LoginRequestVM(string emailUsuario, string senha)
        {
            EmailUsuario = emailUsuario;
            Senha = senha;
        }
    }
}
