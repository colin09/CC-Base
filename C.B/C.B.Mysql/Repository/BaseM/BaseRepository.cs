using C.B.Models.Data;
using C.B.MySql.Context;
using C.B.MySql.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace C.B.MySql.Repository.BaseM
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        //protected abstract string dbName { get; }
        // public BaseRepository(Key key)
        // {
        //     dbName = key as string;
        // }

        private DbContext _context ;

        protected BaseRepository(){
            _context = new MySqlContext();
        }
        /*
        private DbContext context
        {
            get
            {
                return new MySqlContext();
            }
        }*/

        public IQueryable<TEntity> Entities
        {
            get { return _context.Set<TEntity>(); }
        }

        public int InsertBatch(IEnumerable<TEntity> list)
        {
            _context.Set<TEntity>().AddRange(list);
            return _context.SaveChanges();
        }

        public int Insert(TEntity t)
        {
            try
            {
                _context.Set<TEntity>().Add(t);
                var result = _context.SaveChanges();
                System.Console.WriteLine($"Insert.result {result}, {typeof(TEntity)}");
                return result;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return 0;
        }

        public int Update(TEntity t)
        {
            //context.Set<TEntity>().Attach(t);
            _context.Entry(t).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var t = FirstOrDefault(id);
            if (t == null)
                return -1;
            t.IsDeleted = 1;
            t.UpdateTime = DateTime.Now;
            Update(t);
            return _context.SaveChanges();
        }

        public int Delete(TEntity t)
        {
            t.IsDeleted = 1;
            t.UpdateTime = DateTime.Now;
            Update(t);
            return _context.SaveChanges();
        }

        public TEntity FirstOrDefault(int id)
        {
            return _context.Set<TEntity>().Where(t => t.Id == id).FirstOrDefault();
        }

        
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda)
        {
            return _context.Set<TEntity>().Where(whereLambda).FirstOrDefault();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereLambda)
        {
            return _context.Set<TEntity>().Where(whereLambda).AsQueryable();
        }

        public IQueryable<TEntity> Where<S>(Pager pager, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderbyLambda, bool isAsc)
        {
            var result = _context.Set<TEntity>().Where<TEntity>(whereLambda).AsQueryable();
            pager.TotalCount = result.Count();
            if (isAsc)
                return result.OrderBy<TEntity, S>(orderbyLambda).Skip(pager.PageSize * (pager.PageIndex - 1)).Take(pager.PageSize).AsQueryable();
            else
                return result.OrderByDescending(orderbyLambda).Skip(pager.PageSize * (pager.PageIndex - 1)).Take(pager.PageSize).AsQueryable();
        }




        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) { _context.Dispose(); }
            }
            this.disposed = true;
        }

    }
}
