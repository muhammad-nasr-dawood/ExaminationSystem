using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IExamRepo
    {

        public Task<int> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration, OutputParameter<int> returnValue);


    }
}
