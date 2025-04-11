using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IStudentRepo Students { get; }
        IAuthRepo AuthRepo { get; }
        IBaseRepo<Staff> StaffRepo { get; }

        int Complete();
    }
}
