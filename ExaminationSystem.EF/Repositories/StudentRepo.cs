using ExaminationSystem.Core.Models;
using ExaminationSystem.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class StudentRepo : BaseRepo<Student>, IStudentRepo
    {
        public StudentRepo(ExaminationDBContext dBContext) : base(dBContext)
        {
        }

        public List<Student> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
