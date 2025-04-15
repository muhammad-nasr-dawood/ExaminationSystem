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
        IBranchRepo Branches { get; }
        IDepartmentRepo Departments { get; }
        ILocationRepo Locations { get; }

        IAuthRepo AuthRepo { get; }
        IBaseRepo<Staff> StaffRepo { get; }
        IBaseRepo<User> UserRepo { get; }

        IBaseRepo<Location> LocationRepo { get; }
        IBaseRepo<Department> DepartmentRepo { get; }
        IBaseRepo<Branch> BranchesRepo { get; }

        int Complete();
    }
}
