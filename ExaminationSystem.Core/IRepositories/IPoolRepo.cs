using System;
using System.Collections.Generic;
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

        public Task<List<GetQuestionsResult>> Questions(int tId , byte o , byte t, byte lvl , int p , int l);


        public Task<List<GetPoolQuestionsResult>>PoolQuestions(int PoolId, int Page , int Limit ,byte QType , byte OTypre );

    }
}
