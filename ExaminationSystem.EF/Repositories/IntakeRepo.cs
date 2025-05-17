using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class IntakeRepo : BaseRepo<Intake>, IIntakeRepo
    {
        private readonly ExaminationDBContext _dBContext;
        public IntakeRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<int> GetActiveIntakeIdAsync()
        {
            return await _dBContext.Intakes
                .Where(i => i.IsRunning == 1)
                .Select(i => i.Id)
                .FirstOrDefaultAsync();
        }
    }

}
