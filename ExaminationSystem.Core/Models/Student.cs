using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Student
{
    public long Ssn { get; set; }

    public string Faculty { get; set; } = null!;

    public int GradYear { get; set; }

    public decimal Gpa { get; set; }

    public virtual ICollection<MultiChoiceResponse> MultiChoiceResponses { get; set; } = new List<MultiChoiceResponse>();

    public virtual User SsnNavigation { get; set; } = null!;

    public virtual ICollection<StudentExamModel> StudentExamModels { get; set; } = new List<StudentExamModel>();

    public virtual ICollection<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; } = new List<StudentIntakeBranchDepartmentStudy>();

    public virtual ICollection<TrueFalseResponse> TrueFalseResponses { get; set; } = new List<TrueFalseResponse>();
}
