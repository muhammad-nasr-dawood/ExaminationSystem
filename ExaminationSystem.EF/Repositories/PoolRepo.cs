using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;

namespace ExaminationSystem.EF.Repositories
{
    public class PoolRepo : IPoolRepo
    {
        private readonly ExaminationDBContext _dbContext;

        public PoolRepo(ExaminationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ActivePoolsResult>> ActivePools(long StaffId)
        {
             return await _dbContext.Procedures.ActivePoolsAsync(StaffId);
        }

        public async Task<List<ProcessedPoolsResult>> ProcessedPools(long StaffId)
        {
            return await _dbContext.Procedures.ProcessedPoolsAsync(StaffId);
        }

        public async Task<List<TeachAtResult>> TeachAt(long StaffId)
        {
            return await _dbContext.Procedures.TeachAtAsync(StaffId);
        }
    

    }
}

