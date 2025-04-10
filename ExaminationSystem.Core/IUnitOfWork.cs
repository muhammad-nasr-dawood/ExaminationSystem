using ExaminationSystem.Core.IRepositories;
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

        int Complete();
    }
}
