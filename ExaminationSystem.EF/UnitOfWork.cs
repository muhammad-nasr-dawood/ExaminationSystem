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

        public UnitOfWork(ExaminationDBContext dbContext, IStudentRepo students, IAuthRepo authRepo)
        {
            _dbContext = dbContext;
            Students = students;
            AuthRepo = authRepo;
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
