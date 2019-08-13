using System.Collections.Generic;

namespace Authorization.Models
{
    public class SRJUsuario
    {
        public int id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }

        public SRJUsuario Get(string login, string senha)
        {
            List<SRJUsuario> usuarios = new List<SRJUsuario>
            {
                new SRJUsuario {id = 1, Login = "MXMEduardo@gmail.com", Nome = "Eduardo", Senha = "dudu", Role = "Admin"},
                new SRJUsuario {id = 1, Login = "Anderson@gmail.com", Nome = "Anderson", Senha = "anderson", Role = "Usuario"},
                new SRJUsuario {id = 1, Login = "Valentina@gmail.com", Nome = "Valentina", Senha = "valentina", Role = "Supervisor"}
            };

            return usuarios.Find(x => x.Login == login && x.Senha == senha);
        }
    }
}