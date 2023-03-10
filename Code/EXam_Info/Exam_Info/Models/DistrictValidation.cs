using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class DistrictValidation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = " Index No is required")]
        [RegularExpression(@"^(\d{7})$", ErrorMessage = "No white space allowed")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Please fill the valid Index No")]
        public string Index_No { get; set; }
        [Required(ErrorMessage = " College Name is required")]
        public string College_Name { get; set; }
        [Required(ErrorMessage = " College Address is required")]
        public string College_Address { get; set; }
        [Required(ErrorMessage = "  Co-Ordinator Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Coordinator_Name { get; set; }
        [Required(ErrorMessage = " You must provide a mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        public string Coordinator_Mobile { get; set; }
        [Required(ErrorMessage = "Co-Ordinator Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Coordinator_Email { get; set; }
        [Required(ErrorMessage = "Co-Ordinator Eduction is required")]
        public string Coordinator_Eduction { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm_Password is required ")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password do not match.")]
        public string Confirm_Password { get; set; }
        [Required(ErrorMessage = " District Code is required")]
        public string District_Code { get; set; }

        public string Taluka_Code { get; set; }
        public List<string> College_Code1 { get; set; }
        public List<Tbl_ITCOLLEGELIST> College_List { get; set; }
        public Nullable<int> Active { get; set; }
    }
}