using Curso.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.Data;
using Microsoft.EntityFrameworkCore;
namespace Curso.Servicos
{
    public class LoginService: ILoginService
    {
        private readonly Curso.Data.DbContext _dbContext;
        public LoginService(Curso.Data.DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> RegisterService(string Usuario, string Emial, string Contraseña, string ConfContra)
        {
            var user = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Email == Emial);
            if (user != null) return false;
            if (Contraseña != ConfContra) return false;
            var NewUser = new Curso.Entidades.Usuarios
            {
                Nombre = Usuario,
                Email = Emial,
                ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(ConfContra),
                FechaRegistro = DateTime.Now
            };
            _dbContext.Usuarios.Add(NewUser);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
