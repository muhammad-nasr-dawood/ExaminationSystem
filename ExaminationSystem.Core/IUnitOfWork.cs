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
        //IStudentRepo Students { get; }
        //IBaseRepo<Student> StudentRepo { get; }
        IAuthRepo AuthRepo { get; }
        IBaseRepo<Staff> StaffRepo { get; }
        IBaseRepo<Student> StudentRepo { get; }
        IBaseRepo<User> UserRepo { get; }

        IBaseRepo<Location> LocationRepo { get; }
        IBaseRepo<Department> DepartmentRepo { get; }
       
        IBaseRepo<StaffBranchIntakeDepartmentCourseTeach> TeachingRepo { get; }

        IBaseRepo<ProfileImage> ProfileImageRepo { get; }
        IStaffBranchManageRepo StaffBranchManageRepo { get; }
        IBranchRepo BranchesRepo { get; }

        IBaseRepo<Course> CoursesRepo { get; }
        int Complete();
    }
}
