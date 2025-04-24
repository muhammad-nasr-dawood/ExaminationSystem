using ExaminationSystem.Core;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        ExaminationDBContext _dbContext;
        public IStudentRepo Students {  get; }


        public IAuthRepo AuthRepo {  get; }

        public IBaseRepo<Staff> StaffRepo { get; }

        
        public IBaseRepo<User> UserRepo { get; }

        public IBaseRepo<Location> LocationRepo { get; }

        public IBaseRepo<Department> DepartmentRepo { get; }

        public IBranchRepo BranchesRepo { get; }

        public IStaffBranchManageRepo StaffBranchManageRepo { get; }

        public UnitOfWork(ExaminationDBContext dbContext, IStudentRepo students, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo, IBaseRepo<Location> locationRepo, IBranchRepo branchRepo, IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo,IStaffBranchManageRepo staffBranchManageRepo)
        {
            _dbContext = dbContext;
            Students = students;
            AuthRepo = authRepo;
            StaffRepo = staffRepo;
            StaffBranchManageRepo = staffBranchManageRepo;
           

            LocationRepo = locationRepo;    
            BranchesRepo = branchRepo;
            DepartmentRepo = departmentRepo;    
            UserRepo = userRepo;
        }

        public int Complete()
        {
            return _dbContext.SaveChanges(); // will return number of rows affected
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
