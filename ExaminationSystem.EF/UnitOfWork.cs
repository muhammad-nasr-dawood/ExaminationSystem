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
        public IBranchRepo Branches { get; }
        public IDepartmentRepo Departments { get; }
        public ILocationRepo Locations { get; }

        public IAuthRepo AuthRepo {  get; }

        public IBaseRepo<Staff> StaffRepo { get; }

        public UnitOfWork(ExaminationDBContext dbContext, IStudentRepo students, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo, IBranchRepo branches, IDepartmentRepo departments, ILocationRepo locations)
        public IBaseRepo<User> UserRepo { get; }

        public IBaseRepo<Location> LocationRepo { get; }

        public IBaseRepo<Department> DepartmentRepo { get; }

        public IBaseRepo<Branch> BranchesRepo { get; }

        public UnitOfWork(ExaminationDBContext dbContext, IStudentRepo students, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo, IBaseRepo<Location> locationRepo, IBaseRepo<Branch> branchRepo, IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo)
        {
            _dbContext = dbContext;
            Students = students;
            AuthRepo = authRepo;
            StaffRepo = staffRepo;
            Branches = branches;
            Departments = departments;
            Locations = locations;

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
