﻿using C.B.Models.Data;
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

        private DbContext context
        {
            get
            {
                return new MySqlContext();
            }
        }

        public IQueryable<TEntity> Entities
        {
            get { return context.Set<TEntity>(); }
        }

        public int InsertBatch(IEnumerable<TEntity> list)
        {
            context.Set<TEntity>().AddRange(list);
            return context.SaveChanges();
        }

        public int Insert(TEntity t)
        {
            context.Set<TEntity>().Add(t);
            return context.SaveChanges();
        }

        public int Update(TEntity t)
        {
            //context.Set<TEntity>().Attach(t);
            context.Entry(t).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Delete(string id)
        {
            var t = ReadOne(id);
            if (t == null)
                return -1;
            t.IsDeleted = true;
            t.UpdateTime = DateTime.Now;
            Update(t);
            return context.SaveChanges();
        }

        public int Delete(TEntity t)
        {
            t.IsDeleted = true;
            t.UpdateTime = DateTime.Now;
            Update(t);
            return context.SaveChanges();
        }

        public TEntity ReadOne(string id)
        {
            return context.Set<TEntity>().Where(t => t.Id == id).FirstOrDefault();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereLambda)
        {
            return context.Set<TEntity>().Where(whereLambda).AsQueryable();
        }

        public IQueryable<TEntity> Page<S>(Pager pager, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderbyLambda, bool isAsc)
        {
            var result = context.Set<TEntity>().Where<TEntity>(whereLambda).AsQueryable();
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
                if (disposing) { context.Dispose(); }
            }
            this.disposed = true;
        }

    }
}
