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

        public async Task<int> RemoveQuestionFromPool(long staffId, int poolId, DataTable questionsIds, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.RemoveQuestionFromPoolAsync(staffId, poolId, questionsIds, returnValue);
        }

        public async Task<int> AddQuestionsToPool(long staffId, int poolId, DataTable questionsIds, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.AddQuestionsToPoolAsync(staffId, poolId, questionsIds, returnValue);
        }

        public async Task<int> SetConfigurations(long staffId, Configuration config, DataTable excludedStdIds, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.SetConfigurationsAsync(staffId, config.PoolId, config.NoOfDifficult, config.NoOfMedium, config.NoOfEasy,
                config.GradeForDifficult, config.GradeForMedium , config.GradeForEasy, config.NoOfModels,config.Date,config.StartingTime,config.EndingTime,
                config.Duration, excludedStdIds, returnValue);
        }

        public async Task<int> UpdateConfigurationGrades(long staffId, int poolId, int gradeForDiff, int gradeForMed, int gradeForEasy, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.UpdateConfigurationGradesAsync(staffId, poolId, gradeForDiff, gradeForMed, gradeForEasy, returnValue);
        }

        public async Task<int> UpdateConfigurations(long staffId, int poolId, int noOfDiff, int noOfMed, int noOfEasy, int gradeForDiff, int gradeForMed, int gradeForEasy, int noOfModels, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.UpdateConfigurationsAsync(staffId, poolId, noOfDiff, noOfMed, noOfEasy, gradeForDiff, gradeForMed, gradeForEasy, noOfModels, returnValue);
        }

        public async Task<int> UpdateConfigurationStudentList(int poolId, long staffId, DataTable excludedStds, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.UpdateConfigurationStudentListAsync(poolId, staffId, excludedStds, returnValue);
        }

        public async  Task<List<ActivePoolResult>> ActivePool(long? staffId, int? poolId, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.ActivePoolAsync(staffId, poolId, returnValue);
        }

        public async Task<List<includedAndExcludedStudentsResult>> includedAndExcludedStudents(long staffId,int poolId, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.includedAndExcludedStudentsAsync(staffId, poolId,returnValue);
        }

        public async Task<int> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration, OutputParameter<int> returnValue)
        {
            return await _dbContext.Procedures.SetExamSessionAsync(staffId, poolId, date, startingTime, endingTime, duration, returnValue);
        }

    }
}

