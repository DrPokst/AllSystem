using System.Collections.Generic;
using System.Threading.Tasks;
using Storage.API.Models;
using Microsoft.EntityFrameworkCore;
using Storage.API.Helpers;
using System.ComponentModel;
using System.Linq;

namespace Storage.API.Data
{
    public class SearchRepository : ISearchRepository
    {
        private DataContext _context;

        public SearchRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Componentas> GetComponents(int id)
        {
            var componentass = await _context.Componentass.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return componentass;
        }

        public async Task<PageList<Componentas>> GetComponents(ComponentParams componentParams)
        {
            var componentass = _context.Componentass.Include(p => p.Photos).AsQueryable();

            if (componentParams.Size != null)
            {
                componentass = componentass.Where(u => u.Size == componentParams.Size);
            }

            if (componentParams.Type != null)
            {
                componentass = componentass.Where(u => u.Type == componentParams.Type);
            }

            return await PageList<Componentas>.CreateAsync(componentass, componentParams.PageNumber, componentParams.PageSize);
        }

        public async Task<Componentas> RegisterComponents(Componentas componentas)
        {   
            
            await _context.Componentass.AddAsync(componentas);
            await _context.SaveChangesAsync();

            return componentas;

        }
         public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> MnFExists(string Mnf)
        {
            
           if (await _context.Componentass.AnyAsync(x => x.Mnf == Mnf))
            return true;
            
            return false;
        }

       
        
    }
}