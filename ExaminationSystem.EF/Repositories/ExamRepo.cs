using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class ExamRepo: IExamRepo
    {
        private readonly ExaminationDBContext _dbContext;
        public ExamRepo(ExaminationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.SetExamSessionAsync(staffId, poolId, date, startingTime, endingTime, duration, returnValue);
        }
    














    }
}
