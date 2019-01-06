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
            if (!_context.Datas.Any())
            {
                _context.Datas.Add(new Entities.Data { Id = 1, Name = "SAM ELLIOTT PRINTS AND POSTERS 203213" });
                _context.Datas.Add(new Entities.Data { Id = 2, Name = "SKIING PRINTS AND POSTERS 203201" });
                _context.Datas.Add(new Entities.Data { Id = 3, Name = "LA DOLCE VITA PRINTS AND POSTERS 203175" });
                _context.Datas.Add(new Entities.Data { Id = 4, Name = "JOAN CRAWFORD PRINTS AND POSTERS 105958" });
                _context.Datas.Add(new Entities.Data { Id = 5, Name = "DUSTY SPRINGFIELD PRINTS AND POSTERS 203163" });
                _context.Datas.Add(new Entities.Data { Id = 6, Name = "RAMI MALEK PRINTS AND POSTERS 203164" });
                _context.SaveChanges();
            }
        }

        public IQueryable<Data> GetAll()
        {
            return _context.Datas.OrderByDescending(x => x.Id);
        }

        public void Add(string itemName)
        {
            int latestId = GetAll().FirstOrDefault().Id;
            _context.Datas.Add(new Data { Id = latestId + 1, Name = itemName });
            _context.SaveChanges();
        }
    }
}
