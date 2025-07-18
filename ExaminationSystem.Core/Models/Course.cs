﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Core.Models;

public partial class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    public int Duration { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<IntakeDeptCourse> IntakeDeptCourses { get; set; } = new List<IntakeDeptCourse>();

    [InverseProperty("Course")]
    public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();

    [InverseProperty("Course")]
    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    [ForeignKey("CrsId")]
    [InverseProperty("Crs")]
    public virtual ICollection<Topic> Tops { get; set; } = new List<Topic>();
}