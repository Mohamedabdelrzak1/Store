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

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trackChanges ?
                      await _context.Products.OrderBy(p => p.Name).Include(p => p.productBrand).Include(p => p.productType).ToListAsync() as IEnumerable<TEntity>
                    : await _context.Products.AsNoTracking().Include(p => p.productBrand).Include(p => p.productType).ToListAsync() as IEnumerable<TEntity>;
            }
            return trackChanges ?
                    await _context.Set<TEntity>().ToListAsync()
                  : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        } 
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {

            if (typeof(TEntity) == typeof(Product))
            {
                //return await _context.Products.Include(p => p.productBrand).Include(p => p.productType).FirstOrDefaultAsync(p => p.Id == id as int?)as TEntity;
                return await _context.Products.Where(p => p.Id==id as int?).Include(p => p.productBrand).Include(p => p.productType).FirstOrDefaultAsync(p => p.Id == id as int?)as TEntity;
            }
           return await _context.Set<TEntity>().FindAsync(id);
        }
       
        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) =>  _context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool trackChanges = false)
        {
         return await ApplaySpecifications(spec).ToListAsync();
        }

        public  async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplaySpecifications(spec).FirstOrDefaultAsync(); 
        }


        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplaySpecifications(spec).CountAsync();
        }

        private IQueryable<TEntity> ApplaySpecifications(ISpecifications<TEntity, TKey> spec)
        {
           return SpecificationEvaluation.GetQuery(_context.Set<TEntity>(), spec);
        }

        
    }
}
