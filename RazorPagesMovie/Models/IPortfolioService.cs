using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Models
{
    public interface IPortfolioService
    {
        Task DeleteAsync(int id);
        Portfolio Find(int id);
        Task<Portfolio> FindAsync(int id);
        IQueryable<Portfolio> GetAll(int? count = null, int? page = null);
        Task<Portfolio[]> GetAllAsync(int? count = null, int? page = null);
        Task SaveAsync(Portfolio portfolio);
    }
}
