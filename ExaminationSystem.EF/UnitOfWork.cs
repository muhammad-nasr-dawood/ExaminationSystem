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
        //public IStudentRepo Students {  get; }
        public IAuthRepo AuthRepo {  get; }
        public IBaseRepo<Staff> StaffRepo { get; }
        public IBaseRepo<User> UserRepo { get; }
        public IBaseRepo<Location> LocationRepo { get; }
        public IBaseRepo<Department> DepartmentRepo { get; }
        public IBranchRepo BranchesRepo { get; }
        public IStaffBranchManageRepo StaffBranchManageRepo { get; }
        public IBaseRepo<StaffBranchIntakeDepartmentCourseTeach> TeachingRepo {  get; }
        public IBaseRepo<Course> CoursesRepo { get; }
        public IBaseRepo<Student> StudentRepo { get; }
        public IBaseRepo<ProfileImage> ProfileImageRepo { get; }  



        public IBaseRepo<Staff> StaffRepo { get; }
        public IBaseRepo<Course> CourseRepo { get; }
        public IBaseRepo<Topic> TopicRepo { get; }

        

        public IBaseRepo<User> UserRepo { get; }

        public ILocationRepo LocationRepo { get; }






        public IBaseRepo<Student> StudentRepo { get; }
        public IBaseRepo<ProfileImage> ProfileImageRepo { get; }

       

        /*nasser*/
        public IPoolRepo PoolRepo { get; } // Added PoolRepo to UnitOfWork

        public IQuestionRepo QuestionRepo { get; } // Added QuestionRepo to UnitOfWork


        /*nasser*/


        public IBaseRepo<StaffBranchIntakeWorksFor> WorksForRepo { get; }
        public IBaseRepo<Intake> IntakeRepo { get; }

        public IBaseRepo<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudyRepo { get; }

        public UnitOfWork(ExaminationDBContext dbContext, 
            IBaseRepo<Student> studentRepo, 
            IAuthRepo authRepo, 
            IBaseRepo<Staff> staffRepo, 
            IBaseRepo<Location> locationRepo, 
            IBranchRepo branchRepo,
            IBaseRepo<Department> departmentRepo, 
            IBaseRepo<User> userRepo,
            IBaseRepo<StaffBranchIntakeDepartmentCourseTeach> teachRepo, 
            IBaseRepo<Course> coursesRepo,
            IBaseRepo<ProfileImage> profileImageRepo, 
            IStaffBranchManageRepo staffBranchManageRepo,
            IPoolRepo _poolRepo,
            IQuestionRepo _questionRepo,
            IBaseRepo<StaffBranchIntakeWorksFor> worksFor,
            IBaseRepo<Intake> intakeRepo,
            IBaseRepo<StudentIntakeBranchDepartmentStudy> studentIntakeBranchDepartmentStudyRepo
            )

        public IBaseRepo<StaffBranchIntakeWorksFor> WorksForRepo 
        { get; }

        public IBaseRepo<Intake> IntakeRepo  {get;}

        public UnitOfWork(ExaminationDBContext dbContext, IBaseRepo<Student> studentRepo, IAuthRepo authRepo, IBaseRepo<Staff> staffRepo,ILocationRepo locationRepo, IBranchRepo branchRepo, IBaseRepo<Department> departmentRepo, IBaseRepo<User> userRepo, IBaseRepo<StaffBranchIntakeDepartmentCourseTeach> teachRepo, IBaseRepo<Course> coursesRepo, IBaseRepo<ProfileImage> profileImageRepo,IStaffBranchManageRepo staffBranchManageRepo, IBaseRepo<Course> courseRepo,IBaseRepo<Topic> topicRepo,IBaseRepo<StaffBranchIntakeWorksFor> worksFor ,IBaseRepo<Intake> intake )
       
        {
            _dbContext = dbContext;
            //Students = students;
            StudentRepo = studentRepo;
            AuthRepo = authRepo;
            StaffRepo = staffRepo;
            LocationRepo = locationRepo;
            StaffBranchManageRepo = staffBranchManageRepo;

            StudentIntakeBranchDepartmentStudyRepo = studentIntakeBranchDepartmentStudyRepo;
            CourseRepo = courseRepo;
            TopicRepo = topicRepo;
           

            LocationRepo = locationRepo;
            BranchesRepo = branchRepo;
            DepartmentRepo = departmentRepo;
            UserRepo = userRepo;
            TeachingRepo = teachRepo;
            CoursesRepo = coursesRepo;
            ProfileImageRepo = profileImageRepo;

            /*nasser*/

            PoolRepo = _poolRepo; // Added PoolRepo to UnitOfWork
            QuestionRepo =  _questionRepo;



            WorksForRepo = worksFor;
            IntakeRepo = intakeRepo;
            WorksForRepo = worksFor;
            IntakeRepo = intake;
      

        }

         
        public int Complete()
        {
            return _dbContext.SaveChanges(); // will return number of rows affected
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.Message}", ex);
            }
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.Message}", ex);
            }
        }
    }
}
