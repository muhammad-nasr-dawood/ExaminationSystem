using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Topic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Course> Crs { get; set; } = new List<Course>();
}
