using System.Collections.Generic;
using System.Threading.Tasks;
using Storage.API.Models;


namespace Storage.API.Data
{
    public interface IReelRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<Reel>> GetReels();
         Task<Reel> GetReel(int id);
         Task<Reel> GetReelCMnf(string cMnf);
         Task<Reel[]> GetCompare(int id);
         Task<Reel> RegisterReel(Reel reel);

    }
}