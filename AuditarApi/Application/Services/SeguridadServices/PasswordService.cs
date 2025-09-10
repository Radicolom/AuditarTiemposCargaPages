using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SeguridadServices
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            // Genera un "hash" de la contraseña BD.
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Compara la contraseña que el usuario ingresa con el hash guardado en la BD.
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
