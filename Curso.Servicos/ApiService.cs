using Curso.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Servicos
{
    public class ApiService : IAPIService
    {
        private readonly string _ApiURl;
        public ApiService(IConfiguration configuration)
        {
            _ApiURl = configuration["UrlAPI"];
        }

        public string GetApiUrl()
        {
            return _ApiURl;
        }
    }
}
