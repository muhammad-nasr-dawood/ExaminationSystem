using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        ExaminationDBContext _dbContext;
        public BaseRepo(ExaminationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
    }
}
