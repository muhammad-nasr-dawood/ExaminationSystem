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
            IQueryable<Location> locationsQuery = _dBContext.Locations;

           
            if (id.HasValue)
            {
               
                locationsQuery = locationsQuery
                    .Where(l => l.ZipCode == _dBContext.Branches
                        .Where(b => b.Id == id)
                        .Select(b => b.ZipCode)
                        .FirstOrDefault() || 
                    !_dBContext.Branches
                        .Any(b => b.ZipCode == l.ZipCode && b.IsDeleted == false)); 
            }
            else
            {
                locationsQuery = locationsQuery.Where(l => !_dBContext.Branches
                    .Any(b => b.ZipCode == l.ZipCode && b.IsDeleted == false));
            }

          
            var locations = await locationsQuery.ToListAsync();

            return locations;
        }


    }
}
