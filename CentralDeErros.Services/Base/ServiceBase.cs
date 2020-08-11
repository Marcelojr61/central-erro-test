using CentralDeErros.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CentralDeErros.Services.Base
{
    public class ServiceBase<T> : IDisposable where T : class
    {
        public CentralDeErrosDbContext Context { get; private set; }

        public ServiceBase(CentralDeErrosDbContext context)
        {
            Context = context;
        }

        public virtual IQueryable<T> List()
        {
            IQueryable<T> rsList = Context.Set<T>().AsQueryable();

            return rsList;
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> rsList = Context.Set<T>().Where(filter).AsQueryable();

            return rsList;
        }

        public virtual T Fetch(int id)
        {
            T rsFetch = Context.Set<T>().Find(id);

            return rsFetch;
        }

        public virtual T Fetch(Expression<Func<T, bool>> filter)
        {
            T rsFetch = Context.Set<T>().FirstOrDefault(filter);

            return rsFetch;
        }

        public virtual void Register(T register)
        {
            Context.Set<T>().Add(register);
            Context.SaveChanges();
        }

        public virtual T Update(T register)
        {
            Context.Set<T>().Update(register);
            Context.SaveChanges();

            return register;
        }

        public virtual void Delete(T register)
        {
            Context.Set<T>().Remove(register);
            Context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            T register = Fetch(id);

            if (register == null)
            {
                throw new Exception("O registro informado para exclusão não existe na base de dados");
            }

            Delete(register);
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context = null;
            }

        }
    }
}
