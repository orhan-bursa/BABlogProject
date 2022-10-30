using HS4_Blog_Project.Domain.Entities;
using HS4_Blog_Project.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        /*CRUD işlemlerini yapacagımız metotları barındırdıgımız class'ımız.
         * Bu metotlar muhakkak DBSet yani veritabanındaki varlıkları, uygulama tarafındaki yani application tarafındaki karşılıklarına ve ORM gereği vertabanının uygulama tarafındaki karşılığı olan Context nesnemize uğramak zorunda. 
         * 
         * Bağımlılığa neden olan AppDbContext nesnemi Inject ediyorum.
         */
        private readonly AppDbContext _appDbContext;
        protected DbSet<T> table;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            table = _appDbContext.Set<T>(); // Database hangi tipe bürünmüşse T olarak buraya atıyoruz. Table'ın DbSet'ine atıyoruz. Buradaki metotlar tüm entityler için ortak bir şekilde çalışacağı için bunları yapıyoruz. Dependency Injection yaptık.
        }
        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task Create(T entity)
        {
            table.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {            
            await _appDbContext.SaveChangesAsync(); //Servis katmanında entity'sine göre Status pasif'e çekilecek. 
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(
                          Expression<Func<T, TResult>> selector, 
                          Expression<Func<T, bool>> expression, 
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, 
                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = null; //Select * from Post

            if (expression != null)
                query = query.Where(expression);

            if (include != null)
                query = include(query);

            if (orderby != null)
                return await orderby(query).Select(selector).FirstOrDefaultAsync();
            else
                return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(
                        Expression<Func<T, TResult>> selector, 
                        Expression<Func<T, bool>> expression, 
                        Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, 
                        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = null; //Select * from Post

            if (expression != null)
                query = query.Where(expression);

            if (include != null)
                query = include(query);

            if (orderby != null)
                return await orderby(query).Select(selector).ToListAsync();
            else
                return await query.Select(selector).ToListAsync();
        }

        public async Task Update(T entity)
        {
            _appDbContext.Entry<T>(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
