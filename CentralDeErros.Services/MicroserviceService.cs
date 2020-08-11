using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Linq;

namespace CentralDeErros.Services
{
    public class MicrosserviceService : ServiceBase<Microsservice>
    {
        public MicrosserviceService(CentralDeErrosDbContext context) : base(context) 
        {
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
            //geraTokenMicroservice = name... JTW ? KeyUser

            //Separar... 

            _ = microsservice.Id == 0
                ? Context.Microsservices.Add(microsservice)
                : Context.Microsservices.Update(microsservice);

            Context.SaveChanges();

            return microsservice;
        }


    }
}
