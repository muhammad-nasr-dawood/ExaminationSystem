using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class User
{
    public long Ssn { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string StreetNo { get; set; } = null!;

    public DateOnly Bd { get; set; }

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? ImageId { get; set; }

    public virtual ProfileImage? Image { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Location ZipCodeNavigation { get; set; } = null!;
}
