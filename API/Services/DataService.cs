using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IDataService
    {
        IQueryable<Data> GetAll();
        void Add(string itemName);
    }
    public class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Data> GetAll()
        {
            return _context.Datas.OrderByDescending(x => x.Id);
        }

        public void Add(string itemName)
        {
            _context.Datas.Add(new Data { Name = itemName });
            _context.SaveChanges();
        }
    }
}
