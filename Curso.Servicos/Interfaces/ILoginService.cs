using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Servicos.Interfaces
{
    public interface ILoginService
    {
        Task<bool> RegisterService(string Usuario, string Emial, string Contraseña, string ConfContra);
    }
}
