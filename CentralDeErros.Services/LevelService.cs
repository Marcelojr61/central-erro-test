using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using CentralDeErros.Services.Interfaces;
using System;
using System.Linq;

namespace CentralDeErros.Services
{
    public class LevelService : ServiceBase<Level>, ILevelService
    {
        public LevelService(CentralDeErrosDbContext context) : base(context) 
        {
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("O registro informado para exclusão não existe na base de dados");
            }

            var relationship = Context.Errors.Count(x => x.LevelId == id);

            if (relationship > 0)
            {
                throw new Exception("O registro não pode ser excluído por ser relacionar com mais de um registro de error");
            }

            else

            {
                var register = Context.Levels.FirstOrDefault(x => x.Id == id);
                Context.Remove(register);
                Context.SaveChanges();
            }
        }

        public Level RegisterOrUpdate(Level level)
        {
            _ = level.Id == 0
                ? Context.Levels.Add(level)
                : Context.Levels.Update(level);

            Context.SaveChanges();

            return level;
        }
    }
}
