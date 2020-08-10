using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Linq;

namespace CentralDeErros.Services
{
    public class MicrosserviceService : ServiceBase<Microsservice>
    {
        private readonly TokenService _tokenService;

        public MicrosserviceService(CentralDeErrosDbContext context, TokenService tokenService) : base(context) 
        {
            _tokenService = tokenService;
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("O registro informado para exclusão não existe na base de dados");
            }

            var relationship = Context.Errors.Count(x => x.MicrosserviceId == id);

            if (relationship > 0)
            {
                throw new Exception("O registro não pode ser excluído por ser relacionar com mais de um registro de error");
            }

            else

            {
                var register = Context.Microsservices.FirstOrDefault(x => x.Id == id);
                Context.Remove(register);
                Context.SaveChanges();
            }
        }

        public Microsservice RegisterOrUpdate(Microsservice microsservice)
        {
            _ = microsservice.Id == 0
                ? Context.Microsservices.Add(microsservice)
                : Context.Microsservices.Update(microsservice);

           // _tokenService.GenerateToken(microsservice);

            Context.SaveChanges();

            return microsservice;
        }


    }
}
