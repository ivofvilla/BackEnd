using LivrosRpg.Dal.Context;
using LivrosRpg.Dal.Context.Interfaces;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LivrosRpg.Dal
{
    public abstract class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        BancoContexto context = new BancoContexto();

        public void Adicionar(TEntity obj)
        {
            context.Set<TEntity>().Add(obj);
        }

        public void Atualizar(TEntity obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Excluir(Func<TEntity, bool> predicate)
        {
            context.Set<TEntity>().Where(predicate).ForEach(del => context.Set<TEntity>().Remove(del));
        }

        public TEntity Find(params object[] key)
        {
            return context.Set<TEntity>().Find(key);
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public void SalvarTodos()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}