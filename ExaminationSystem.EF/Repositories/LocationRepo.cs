using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class LocationRepo : BaseRepo<Location>, ILocationRepo
    {
        public LocationRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
        }
    }
}
