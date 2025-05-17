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
    public class StaffBranchManageRepo : BaseRepo<StaffBranchManage>,IStaffBranchManageRepo
    {
        private readonly ExaminationDBContext _dBContext;
        public StaffBranchManageRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
        

        public async Task<StaffBranchManage?> GetByBranchId(int branchId)
        {
            return await _dBContext.StaffBranchManages
                                   .Where(sbm => sbm.BranchId == branchId)
                                   .FirstOrDefaultAsync();
        }



    }
}
