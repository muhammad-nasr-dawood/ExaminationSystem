using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class ProfileImage
{
    public string ImageId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
