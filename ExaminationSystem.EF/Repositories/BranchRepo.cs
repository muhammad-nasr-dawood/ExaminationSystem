using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class BranchRepo:BaseRepo<Branch>,IBranchRepo
    {
        public BranchRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
        }
    }
}
