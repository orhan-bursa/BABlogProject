using HS4_Blog_Project.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        //Task: Create metodu Asenkron olarak çalışacak.
        Task Create(T entity); //Asencron 

        Task Update(T entity); //Asencron 
        Task Delete(T entity); //Asencron   // veri tabanında silme işlemi yapmam, statüs ü pasife çekerim.

        Task<bool> Any(Expression<Func<T, bool>> expression); //Veri tabanında sorgu gibi düşün. bu metodu bir kayıt varsa true ,yoksa false donuek.

        Task<T> GetDefault(Expression<Func<T, bool>> expression); //Dinamik olarka where işlemini sağlar.
        //Id ye göre getir vb.

        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression); //Genre=5 olan POSTLARIn gelmesibni istiyoruz gibi.


        //HEM SELECT HEM DE ORDER BY yapabileceğimiz. Post,Yazar,Comment,Like ları birlikte çekmek için include etmek gerekir.Bir sorguya birden fazla tablo girecek yani eagerloading kullanaağız.
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
             Expression<Func<T, TResult>> selector,//SELECT
             Expression<Func<T, bool>> expression,//WHERE
             Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, //ORDER
             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); //JOIN
        //BACKEND E SP yazacakmış gibi bir çalışma yapıyoruz. İncluade eager loading e göre join, order by, where, select işlemleri yapılır.

        //çoklu dönecek.

        Task<List<TResult>> GetFilteredList<TResult>(
             Expression<Func<T, TResult>> selector,//SELECT
             Expression<Func<T, bool>> expression,//WHERE
             Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, //SIRALAMA
             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); //JOİN


    }
}
