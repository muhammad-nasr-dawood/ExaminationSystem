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
        public IBaseRepo<Course> CourseRepo { get; }
        public IBaseRepo<Topic> TopicRepo { get; }


        public IBaseRepo<User> UserRepo { get; }

        public ILocationRepo LocationRepo { get; }

        public IBaseRepo<Department> DepartmentRepo { get; }

        public IBranchRepo BranchesRepo { get; }

        public IStaffBranchManageRepo StaffBranchManageRepo { get; }

        public IBaseRepo<StaffBranchIntakeDepartmentCourseTeach> TeachingRepo {  get; }

        public IBaseRepo<Course> CoursesRepo { get; }

        public IBaseRepo<Student> StudentRepo { get; }
        public IBaseRepo<ProfileImage> ProfileImageRepo { get; }  

        public UnitOfWork(ExaminationDBContext dbContext, IBaseRepo<Student> studentRepo, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo,ILocationRepo locationRepo, IBranchRepo branchRepo, IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo, IBaseRepo<StaffBranchIntakeDepartmentCourseTeach> teachRepo, IBaseRepo<Course> coursesRepo, IBaseRepo<ProfileImage> profileImageRepo,IStaffBranchManageRepo staffBranchManageRepo, IBaseRepo<Course> courseRepo,IBaseRepo<Topic> topicRepo)
       
        {
            _dbContext = dbContext;
            //Students = students;
            StudentRepo = studentRepo;
            AuthRepo = authRepo;
            StaffRepo = staffRepo;
            LocationRepo = locationRepo;
            StaffBranchManageRepo = staffBranchManageRepo;
            CourseRepo = courseRepo;
            TopicRepo = topicRepo;
           

            LocationRepo = locationRepo;    
            BranchesRepo = branchRepo;
            DepartmentRepo = departmentRepo;
            UserRepo = userRepo;
            TeachingRepo = teachRepo;
            CoursesRepo = coursesRepo;
            ProfileImageRepo = profileImageRepo;
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
