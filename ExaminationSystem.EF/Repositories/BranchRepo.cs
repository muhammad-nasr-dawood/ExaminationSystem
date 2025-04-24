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


        public async Task<IEnumerable<Staff>> GetUnassignedStaffAsync(int branchId)
        {
            
            var unassignedStaff = await _dBContext.Staff
                .Where(s => !_dBContext.StaffBranchManages
                            .Any(sbm => sbm.StaffSsn == s.Ssn)) 
                .ToListAsync();

          
            var assignedStaff = await _dBContext.Staff
                .Where(s => _dBContext.StaffBranchManages
                            .Any(sbm => sbm.StaffSsn == s.Ssn && sbm.BranchId == branchId)) 
                .ToListAsync();

           
            if (!assignedStaff.Any())
            {
                return unassignedStaff; 
            }

            
            var allStaff = unassignedStaff.Concat(assignedStaff).Distinct().ToList(); 

            return allStaff;
        }






    }
}
