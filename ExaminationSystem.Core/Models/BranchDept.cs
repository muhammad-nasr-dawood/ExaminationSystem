﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Core.Models;

[PrimaryKey("BranchId", "DeptId", "IntakeId")]
public partial class BranchDept
{
    [Key]
    public int BranchId { get; set; }

    [Key]
    public int DeptId { get; set; }

    [Key]
    public int IntakeId { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("BranchDepts")]
    public virtual Branch Branch { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("BranchDepts")]
    public virtual Department Dept { get; set; }

    [ForeignKey("IntakeId")]
    [InverseProperty("BranchDepts")]
    public virtual Intake Intake { get; set; }

    [InverseProperty("BranchDept")]
    public virtual StaffBranchDepartmentManagement StaffBranchDepartmentManagement { get; set; }

    [InverseProperty("BranchDept")]
    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    [InverseProperty("BranchDept")]
    public virtual ICollection<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; } = new List<StudentIntakeBranchDepartmentStudy>();
}