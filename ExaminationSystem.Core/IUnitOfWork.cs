using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IStudentRepo Students { get; }
        IAuthRepo AuthRepo { get; }
        IBaseRepo<Staff> StaffRepo { get; }
        IBaseRepo<User> UserRepo { get; }

        IBaseRepo<Location> LocationRepo { get; }
        IBaseRepo<Department> DepartmentRepo { get; }
        IBaseRepo<Branch> BranchesRepo { get; }


        IPoolRepo PoolRepo { get; } // Added PoolRepo to UnitOfWork



        int Complete();
    }
}
