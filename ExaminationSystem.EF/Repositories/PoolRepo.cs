using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<List<GetArchivedPoolsResult>> ArchivedPools(int CourseId, int page, int limit, int ordder)
        {
            return await _dbContext.Procedures.GetArchivedPoolsAsync(CourseId, page, limit, ordder);
        }

        public async Task<List<ProcessedPoolsResult>> ProcessedPools(long StaffId)
        {
            return await _dbContext.Procedures.ProcessedPoolsAsync(StaffId);
        }

        public async Task<List<GetQuestionsResult>> Questions(int topicId, byte order, byte type, byte level, int page, int limit)
        {
             return await _dbContext.Procedures.GetQuestionsAsync(topicId,order,type,level , page,limit);
        }

        public async Task<List<TeachAtResult>> TeachAt(long StaffId)
        {
            return await _dbContext.Procedures.TeachAtAsync(StaffId);
        }

        public async Task<List<GetPoolQuestionsResult>> PoolQuestions(int PoolId, int Page, int Limit, byte QType, int OType)
        {
            return await _dbContext.Procedures.GetPoolQuestionsAsync(PoolId,Page,Limit,QType, OType);
        }

        public async Task<List<CreatePoolResult>> CreatePool(long staffId, int courseId, int deptId, int branchId)
        {
           return await _dbContext.Procedures.CreatePoolAsync(staffId, courseId, deptId, branchId);
        }

        public async Task<int> UsePool(long staffId, int srcPoolId, int destPoolId, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.UsePoolAsync(staffId, srcPoolId, destPoolId, returnValue);
        }

        public Task<int> RemoveQuestionFromPool(long staffId, int poolId, DataTable questionsIds, OutputParameter<int> returnValue)
        {
            return _dbContext.Procedures.RemoveQuestionFromPoolAsync(staffId, poolId, questionsIds, returnValue);
        }
    }
}

