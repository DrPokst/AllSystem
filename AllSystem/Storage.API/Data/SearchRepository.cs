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
            var componentass = await _context.Componentass.Include(p => p.Photos)
                                                          .Include(h => h.History)
                                                          .FirstOrDefaultAsync(u => u.Id == id);

            return componentass;
        }
        public async Task<IEnumerable<Componentas>> GetMnfs()
        {
            var componentass = _context.Componentass.AsQueryable();

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

            if (componentParams.Mnf != null)
            {
                componentass = from u in componentass
                               where u.Mnf.StartsWith(componentParams.Mnf)
                               select u;
   

            }
            if (componentParams.Nominal != null)
            {
                componentass = from u in componentass
                               where u.Nominal.StartsWith(componentParams.Nominal)
                               select u;
            }
            if (componentParams.BuhNr != null)
            {
                componentass = componentass.Where(u => u.BuhNr == componentParams.BuhNr);
            }

            if (!string.IsNullOrEmpty(componentParams.OrderBy))
            {
                switch (componentParams.OrderBy)
                {
                    case "created":
                        componentass = componentass.OrderByDescending(u => u.Created);
                        break;
                    default:
                        componentass = componentass.OrderBy(u => u.Id);
                        break;
                }
            }

            return await PageList<Componentas>.CreateAsync(componentass, componentParams.PageNumber, componentParams.PageSize);
        }

        public async Task<Componentas> RegisterComponents(Componentas componentas)
        {   
            
            await _context.Componentass.AddAsync(componentas);
            await _context.SaveChangesAsync();

            return componentas;

        }
        public async Task<Photo> RegisterPhoto(Photo photo)
        {

            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();

            return photo;

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

        public async Task<Photo> GetPhotoCID(int Cid)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.ComponentasId == Cid);
            return photo;
        }

        public async Task<History> RegisterHistory(History history)
        {
            await _context.History.AddAsync(history);
            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<IEnumerable<History>> GetHistory()
        {
            var history = _context.History.AsQueryable();

            return history;
        }
    }
}