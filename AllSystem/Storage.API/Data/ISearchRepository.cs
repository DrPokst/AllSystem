using System.Collections.Generic;
using System.Threading.Tasks;
using Storage.API.Helpers;
using Storage.API.Models;

namespace Storage.API.Data
{
    public interface ISearchRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PageList<Componentas>> GetComponents(ComponentParams componentParams);
         Task<Componentas> GetComponents(int id);
         Task<Componentas> RegisterComponents(Componentas componentas);
        Task<Photo> RegisterPhoto(Photo photo);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetPhotoCID(int Cid);
        Task<bool> MnFExists(string Mnf);
       
    }
}