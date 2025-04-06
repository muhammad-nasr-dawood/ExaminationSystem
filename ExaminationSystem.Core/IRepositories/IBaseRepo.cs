using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IBaseRepo<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
