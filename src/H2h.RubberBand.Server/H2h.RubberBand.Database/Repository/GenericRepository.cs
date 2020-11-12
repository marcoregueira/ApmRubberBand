using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace H2h.RubberBand.Database.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<object> entities)
        {
            _dbContext.AddRange(entities);
        }
    }
}