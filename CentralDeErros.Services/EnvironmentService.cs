using CentralDeErros.Core;
using CentralDeErros.Services.Base;
using System;
using System.Linq;

namespace CentralDeErros.Services
{
    public class EnvironmentService:ServiceBase<Model.Models.Environment>
    {
        public EnvironmentService(CentralDeErrosDbContext context) : base(context) 
        {
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("O registro informado para exclusão não existe na base de dados");
            }
            
            var relationship = Context.Errors.Count(x => x.EnviromentId == id);

            if (relationship > 0)
            {
                throw new Exception("O registro não pode ser excluído por ser relacionar com mais de um registro de error");
            } 

            else
            
            {
                var register = Context.Environments.FirstOrDefault(x => x.Id == id);
                Context.Remove(register);
                Context.SaveChanges();
            }
        }

        public Model.Models.Environment RegisterOrUpdate(Model.Models.Environment environment)
        {
            _ = environment.Id == 0 
                ? Context.Environments.Add(environment)
                : Context.Environments.Update(environment);
    
            Context.SaveChanges();

            return environment;
        }

    }
}
