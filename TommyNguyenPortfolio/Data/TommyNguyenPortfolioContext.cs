using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TommyNguyenPortfolio.Models;

namespace TommyNguyenPortfolio.Data
{
    public class TommyNguyenPortfolioContext : DbContext
    {
        public TommyNguyenPortfolioContext (DbContextOptions<TommyNguyenPortfolioContext> options)
            : base(options)
        {
        }

        public DbSet<TommyNguyenPortfolio.Models.RecommendationDatabase> RecommendationDatabase { get; set; }

        public DbSet<TommyNguyenPortfolio.Models.PasswordTable> PasswordTable { get; set; }
    }
}
