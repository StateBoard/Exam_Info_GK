﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam_Info.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GKJ_2022Entities : DbContext
    {
        public GKJ_2022Entities()
            : base("name=GKJ_2022Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Batch> Tbl_Batch { get; set; }
        public virtual DbSet<Tbl_Code_Master> Tbl_Code_Master { get; set; }
        public virtual DbSet<Tbl_College_Registration> Tbl_College_Registration { get; set; }
        public virtual DbSet<Tbl_District_Co_Ordinator_Details> Tbl_District_Co_Ordinator_Details { get; set; }
        public virtual DbSet<Tbl_District_Co_Ordinator_Registration> Tbl_District_Co_Ordinator_Registration { get; set; }
        public virtual DbSet<Tbl_Division_Co_Ordinator_Registration> Tbl_Division_Co_Ordinator_Registration { get; set; }
        public virtual DbSet<Tbl_DTT1> Tbl_DTT1 { get; set; }
        public virtual DbSet<Tbl_DTT2> Tbl_DTT2 { get; set; }
        public virtual DbSet<Tbl_Inspection> Tbl_Inspection { get; set; }
        public virtual DbSet<Tbl_Inspection_Basic_Info> Tbl_Inspection_Basic_Info { get; set; }
        public virtual DbSet<Tbl_Login1> Tbl_Login1 { get; set; }
        public virtual DbSet<Tbl_Mac> Tbl_Mac { get; set; }
        public virtual DbSet<Tbl_Password> Tbl_Password { get; set; }
        public virtual DbSet<Tbl_Reschedule> Tbl_Reschedule { get; set; }
        public virtual DbSet<Tbl_State_Co_Ordinator_Registration> Tbl_State_Co_Ordinator_Registration { get; set; }
        public virtual DbSet<Tbl_Student> Tbl_Student { get; set; }
        public virtual DbSet<Tbl_ITCOLLEGELIST> Tbl_ITCOLLEGELIST { get; set; }
        public virtual DbSet<Tbl_Reschedule_Student> Tbl_Reschedule_Student { get; set; }
        public virtual DbSet<Tbl_Reschedule_College> Tbl_Reschedule_College { get; set; }
        public virtual DbSet<Tbl_Attendance> Tbl_Attendance { get; set; }
        public virtual DbSet<Tbl_Attendance_Web> Tbl_Attendance_Web { get; set; }
        public virtual DbSet<Tbl_Login> Tbl_Login { get; set; }
        public virtual DbSet<Tbl_DTT3> Tbl_DTT3 { get; set; }
        public virtual DbSet<Tbl_Batch_Activation> Tbl_Batch_Activation { get; set; }
        public virtual DbSet<Admin_tbl> Admin_tbl { get; set; }
        public virtual DbSet<Tbl_41to45_Ans> Tbl_41to45_Ans { get; set; }
        public virtual DbSet<Tbl_Reschedule_ApproveBy_Division> Tbl_Reschedule_ApproveBy_Division { get; set; }
        public virtual DbSet<All_tbl_Final_Ans> All_tbl_Final_Ans { get; set; }
        public virtual DbSet<Tbl_100_Ans> Tbl_100_Ans { get; set; }
        public virtual DbSet<Tbl_11to15_Ans> Tbl_11to15_Ans { get; set; }
        public virtual DbSet<Tbl_1to5_Ans> Tbl_1to5_Ans { get; set; }
        public virtual DbSet<Tbl_21to25_Ans> Tbl_21to25_Ans { get; set; }
        public virtual DbSet<Tbl_26to30_Ans> Tbl_26to30_Ans { get; set; }
        public virtual DbSet<Tbl_31to35_Ans> Tbl_31to35_Ans { get; set; }
        public virtual DbSet<Tbl_36to40_Ans> Tbl_36to40_Ans { get; set; }
        public virtual DbSet<Tbl_46to50_Ans> Tbl_46to50_Ans { get; set; }
        public virtual DbSet<Tbl_51to55_Ans> Tbl_51to55_Ans { get; set; }
        public virtual DbSet<Tbl_56to60_Ans> Tbl_56to60_Ans { get; set; }
        public virtual DbSet<Tbl_61to65_Ans> Tbl_61to65_Ans { get; set; }
        public virtual DbSet<Tbl_66to70_Ans> Tbl_66to70_Ans { get; set; }
        public virtual DbSet<Tbl_6to10_Ans> Tbl_6to10_Ans { get; set; }
        public virtual DbSet<Tbl_71to75_Ans> Tbl_71to75_Ans { get; set; }
        public virtual DbSet<Tbl_76to80_Ans> Tbl_76to80_Ans { get; set; }
        public virtual DbSet<Tbl_81to85_Ans> Tbl_81to85_Ans { get; set; }
        public virtual DbSet<Tbl_86to90_Ans> Tbl_86to90_Ans { get; set; }
        public virtual DbSet<Tbl_91to95_Ans> Tbl_91to95_Ans { get; set; }
        public virtual DbSet<Tbl_96to97_Ans> Tbl_96to97_Ans { get; set; }
        public virtual DbSet<Tbl_98_Ans> Tbl_98_Ans { get; set; }
        public virtual DbSet<Tbl_99_Ans> Tbl_99_Ans { get; set; }
        public virtual DbSet<Tbl_Final_Ans> Tbl_Final_Ans { get; set; }
        public virtual DbSet<Tbl_Decode_Answer> Tbl_Decode_Answer { get; set; }
        public virtual DbSet<Tbl_Decoded_Answer_Sheets_Sada> Tbl_Decoded_Answer_Sheets_Sada { get; set; }
        public virtual DbSet<Tbl_Admin_Login> Tbl_Admin_Login { get; set; }
        public virtual DbSet<Tbl_File_Upload_Details> Tbl_File_Upload_Details { get; set; }
        public virtual DbSet<File_Upload_Reason> File_Upload_Reason { get; set; }
        public virtual DbSet<Tbl_Final_Ans_BK> Tbl_Final_Ans_BK { get; set; }
    }
}
