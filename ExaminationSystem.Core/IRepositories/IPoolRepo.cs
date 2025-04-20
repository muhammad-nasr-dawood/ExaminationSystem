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


    }
}
