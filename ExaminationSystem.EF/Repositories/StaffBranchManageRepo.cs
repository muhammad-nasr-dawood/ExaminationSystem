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
        public async Task<StaffBranchManage> AddBranchManager(int branchId, long staffSsn)
        {
            var branch = await _dBContext.Branches.FindAsync(branchId);
            var staff = await _dBContext.Staff.FindAsync(staffSsn);

            if (branch == null || staff == null)
            {
                return null;
            }

            var activeIntake = await _dBContext.Intakes
                .FirstOrDefaultAsync(intake => intake.IsRunning == 1);

            if (activeIntake == null)
                return null;

            
            var existingAssignment = await _dBContext.StaffBranchManages
                .FirstOrDefaultAsync(sbm => sbm.BranchId == branchId);

            if (existingAssignment != null)
                _dBContext.StaffBranchManages.Remove(existingAssignment);
              

            var staffBranchManage = new StaffBranchManage
            {
                StaffSsn = staffSsn,
                BranchId = branchId,
                HiringDate = DateOnly.FromDateTime(DateTime.Now),
                IntakeId = activeIntake.Id
            };

            _dBContext.StaffBranchManages.Add(staffBranchManage);

            return staffBranchManage;
        }


    }
}
