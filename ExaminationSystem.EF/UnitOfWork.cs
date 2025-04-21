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
        //public IStudentRepo Students {  get; }

        public IAuthRepo AuthRepo {  get; }

        public IBaseRepo<Staff> StaffRepo { get; }

        public IBaseRepo<User> UserRepo { get; }

        public IBaseRepo<Location> LocationRepo { get; }

        public IBaseRepo<Department> DepartmentRepo { get; }

        public IBaseRepo<Branch> BranchesRepo { get; }

        public IBaseRepo<Student> StudentRepo { get; }

        public UnitOfWork(ExaminationDBContext dbContext, IBaseRepo<Student> studentRepo, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo, IBaseRepo<Location> locationRepo, IBaseRepo<Branch> branchRepo, IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo)
        {
            _dbContext = dbContext;
            //Students = students;
            StudentRepo = studentRepo;
            AuthRepo = authRepo;
            StaffRepo = staffRepo;
            LocationRepo = locationRepo;
            BranchesRepo = branchRepo;
            DepartmentRepo = departmentRepo;
            UserRepo = userRepo;
        }


        //public UnitOfWork(ExaminationDBContext dbContext, IAuthRepo authRepo,IBaseRepo<Student> _StudentRepo, IBaseRepo<Staff> staffRepo, IBaseRepo<Location> locationRepo, IBaseRepo<Branch> branchRepo, IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo)
        //{
        //    StudentRepo = _StudentRepo;
        //    _dbContext = dbContext;
        //    //Students = students;
        //    AuthRepo = authRepo;
        //    StaffRepo = staffRepo;
        //    LocationRepo = locationRepo;
        //    BranchesRepo = branchRepo;
        //    DepartmentRepo = departmentRepo;
        //    UserRepo = userRepo;
        //}

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
