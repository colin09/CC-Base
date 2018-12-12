using C.B.Models.Data;
using C.B.MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace C.B.MySql.Repository.BaseM
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {

        #region 属性
        IQueryable<TEntity> Entities { get; }
        #endregion

        #region  -- CRUD  --

        int Insert(TEntity t);
        int InsertBatch(IEnumerable<TEntity> list);

        int Update(TEntity t);

        int Delete(int id);

        int Delete(TEntity t);

        TEntity FirstOrDefault(int id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda);

        #endregion




        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereLambda);

        IQueryable<TEntity> Where<S>(Pager pager, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderbyLambda, bool isAsc);



    }
}
