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
        public async Task<StaffBranchManage?> AddBranchManager(int branchId, long staffSsn)
        {
          
            var branchExists = await _dBContext.Branches.AnyAsync(b => b.Id == branchId);
            var staffExists = await _dBContext.Staff.AnyAsync(s => s.Ssn == staffSsn);

            if (!branchExists || !staffExists)
                return null;

            var activeIntakeId = await _dBContext.Intakes
                .Where(i => i.IsRunning == 1)
                .Select(i => i.Id)
                .FirstOrDefaultAsync();

            if (activeIntakeId == 0)
                return null;

         
            var existingAssignment = await _dBContext.StaffBranchManages
                .Where(sbm => sbm.BranchId == branchId)
                .FirstOrDefaultAsync();

            if (existingAssignment != null)
                _dBContext.StaffBranchManages.Remove(existingAssignment);

           
            var staffBranchManage = new StaffBranchManage
            {
                StaffSsn = staffSsn,
                BranchId = branchId,
                HiringDate = DateOnly.FromDateTime(DateTime.Now),
                IntakeId = activeIntakeId
            };

            _dBContext.StaffBranchManages.Add(staffBranchManage);
            return staffBranchManage;
        }

        public async Task<StaffBranchManage?> GetByBranchId(int branchId)
        {
            return await _dBContext.StaffBranchManages
                                   .Where(sbm => sbm.BranchId == branchId)
                                   .FirstOrDefaultAsync();
        }



    }
}
