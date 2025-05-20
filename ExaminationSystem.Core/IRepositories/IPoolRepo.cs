using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationSystem.Core.Models;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IPoolRepo
    {
        public Task<List<TeachAtResult>> TeachAt(long StaffId);

        public Task<List<ActivePoolsResult>> ActivePools(long StaffId);

        public Task<List<ProcessedPoolsResult>> ProcessedPools(long StaffId);

        public Task<List<GetArchivedPoolsResult>> ArchivedPools(int CourseId, int page, int limit, int ordder);

        public Task<List<GetQuestionsResult>> Questions(int tId, byte o, byte t, byte lvl, int p, int l);
        public Task<List<GetPoolQuestionsResult>>PoolQuestions(int PoolId, int Page , int Limit ,byte QType , int OType );


        public Task<List<CreatePoolResult>> CreatePool(long staffId, int courseId, int deptId, int branchId);


        public Task<int> UsePool(long staffId, int srcPoolId, int destPoolId, OutputParameter<int> returnValue);


        public Task<int> RemoveQuestionFromPool(long staffId, int poolId, DataTable questionsIds, OutputParameter<int> returnValue);


        public Task<int> AddQuestionsToPool(long staffId, int poolId, DataTable questionsIds, OutputParameter<int> returnValue);


        public Task<int> SetConfigurations(long staffId,Configuration config, DataTable excludedStdIds, OutputParameter<int> returnValue);


        public Task<int> UpdateConfigurationGrades(long staffId, int poolId, int gradeForDiff, int gradeForMed, int gradeForEasy, OutputParameter<int> returnValue);


        public Task<int> UpdateConfigurations(long staffId, int poolId, int noOfDiff, int noOfMed, int noOfEasy, int gradeForDiff, int gradeForMed, int gradeForEasy, int noOfModels, OutputParameter<int> returnValue);



        public Task<int> UpdateConfigurationStudentList(int poolId, long staffId, DataTable excludedStds, OutputParameter<int> returnValue);


        public Task<List<ActivePoolResult>> ActivePool(long? staffId, int? poolId, OutputParameter<int> returnValue);


        public Task<List<includedAndExcludedStudentsResult>> includedAndExcludedStudents(long staffId,int poolId, OutputParameter<int> returnValue );


        public Task<int> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration, OutputParameter<int> returnValue);


    }

}
