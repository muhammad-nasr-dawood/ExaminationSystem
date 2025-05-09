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
    public class LocationRepo : BaseRepo<Location>, ILocationRepo
    {
        private readonly ExaminationDBContext _dBContext;
        public LocationRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<List<Location>> GetLocationsWithNoBranchAndIsDeletedAsync(int? id = null)
        {
            var locationsWithBranches = _dBContext.Branches
                .Where(b => !b.IsDeleted)
                .Select(b => b.ZipCode);

            IQueryable<Location> query = _dBContext.Locations;

            if (id.HasValue)
            {
                var branchZip = await _dBContext.Branches
                    .Where(b => b.Id == id)
                    .Select(b => b.ZipCode)
                    .FirstOrDefaultAsync();

                query = query.Where(l => l.ZipCode == branchZip || !locationsWithBranches.Contains(l.ZipCode));
            }
            else
            {
                query = query.Where(l => !locationsWithBranches.Contains(l.ZipCode));
            }

            return await query.AsNoTracking().ToListAsync();
        }



    }
}
