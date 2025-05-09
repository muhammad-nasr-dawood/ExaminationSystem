using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ExaminationSystem.EF.Repositories
{
    public class BranchRepo : BaseRepo<Branch>, IBranchRepo
    {
        
        private readonly ExaminationDBContext _dBContext;

        
        public BranchRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
            
            _dBContext = dBContext;
        }


        public IQueryable<Staff> GetUnassignedStaffQueryable(int branchId)
        {
            return _dBContext.Staff
                .Where(s =>
                    !_dBContext.StaffBranchManages.Any(m => m.StaffSsn == s.Ssn) ||
                    _dBContext.StaffBranchManages.Any(m => m.StaffSsn == s.Ssn && m.BranchId == branchId)
                );
        }







    }
}
