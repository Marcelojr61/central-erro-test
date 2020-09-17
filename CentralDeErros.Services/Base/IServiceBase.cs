using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CentralDeErros.Services.Base
{
    public interface IServiceBase<T>
    {
        IQueryable<T> List();

        IQueryable<T> List(Expression<Func<T, bool>> filter);

        T Fetch(int id);

        T Fetch(Expression<Func<T, bool>> filter);

        void Register(T register);

        T Update(T register);

        void Delete(T register);

        void Delete(int id);

        void Dispose();
    }
}
