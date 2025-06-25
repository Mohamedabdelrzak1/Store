using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorys
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)  =>  trackChanges ?  
                                                             await _context.Set<TEntity>().ToListAsync()                   //True
                                                           : await _context.Set<TEntity>().AsNoTracking().ToListAsync();  //False
        public async Task<TEntity?> GetByIdAsync(TKey id) => await _context.Set<TEntity>().FindAsync(id);
       
        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) =>  _context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);





    }
}
