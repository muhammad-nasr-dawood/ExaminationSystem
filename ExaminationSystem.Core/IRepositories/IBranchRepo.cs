﻿using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IBranchRepo:IBaseRepo<Branch>
    {
        IQueryable<Staff> GetUnassignedStaffQueryable(int branchId);



    }
}
