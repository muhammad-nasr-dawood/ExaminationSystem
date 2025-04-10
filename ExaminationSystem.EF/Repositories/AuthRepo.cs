using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ExaminationDBContext _dbContext;
        public AuthRepo(ExaminationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User ValidateLoginByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
