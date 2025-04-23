using ExaminationSystem.Core;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF.Repositories;
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

        public IBaseRepo<Branch> BranchesRepo { get; }

        /*nasser*/
        public IPoolRepo PoolRepo { get; } // Added PoolRepo to UnitOfWork

        public IQuestionRepo QuestionRepo { get; } // Added QuestionRepo to UnitOfWork

        /*nasser*/

        public UnitOfWork(ExaminationDBContext dbContext,
            IStudentRepo students, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo,
            IBaseRepo<Location> locationRepo, IBaseRepo<Branch> branchRepo, 
            IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo,
            IPoolRepo _poolRepo,
            IQuestionRepo _QuestionRepo)
        {
            _dbContext = dbContext;
            Students = students;
            AuthRepo = authRepo;
            StaffRepo = staffRepo;
            LocationRepo = locationRepo;    
            BranchesRepo = branchRepo;
            DepartmentRepo = departmentRepo;    
            UserRepo = userRepo;
            PoolRepo = _poolRepo;
            QuestionRepo = _QuestionRepo;
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
