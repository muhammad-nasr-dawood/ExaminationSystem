using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationSystem.Core.Models;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IStudentRepo: IBaseRepo<Student>
    {
        public List<Student> GetByName(string name);
    }
}
