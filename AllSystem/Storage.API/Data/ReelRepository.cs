using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.API.Data
{
    public class ReelRepository : IReelRepository
    {
        private DataContext _context;

        public ReelRepository(DataContext context) => _context = context;
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public async Task<Reel[]> GetCompare(int id)
        {   
            int a = 0;
            Reel[] y = new Reel[0];
            Reel[] d = new Reel[0];

            var componentass = await _context.Componentass.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            for (int i = 1; i < 2000; i++)
            {
                var reel = await _context.Reels.Include(p => p.Photos2).FirstOrDefaultAsync(u => u.Id == i);

                if (reel != null)
                {
                    y = y.Concat(new Reel[] { reel }).ToArray();
                }
                

            }
            for (int k = 0; k < y.Length; k++)
            {

                if (y[k].CMnf == componentass.Mnf)
                    {
                    d = d.Concat(new Reel[] { y[k] }).ToArray();
                }
            }

            return d;
        }

        public async Task<Reel> GetReel(int id)
        {
           var reel = await _context.Reels.Include(p => p.Photos2).FirstOrDefaultAsync(u => u.Id == id);

            return reel;
        }

        public async Task<Reel> GetReelCMnf(string cMnf)
        {
           var reel = await _context.Reels.Include(p => p.Photos2).FirstOrDefaultAsync(u => u.CMnf == cMnf);

            return reel;
        }

        public async Task<IEnumerable<Reel>> GetReels()
        {
            var reels = await _context.Reels.Include(p => p.Photos2).ToListAsync();
           
            return reels;
        }


        public async Task<Reel> RegisterReel(Reel reel)
        {
            await _context.Reels.AddAsync(reel);
            await _context.SaveChangesAsync();

            return reel;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}