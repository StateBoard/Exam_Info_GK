using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class DivisionValidation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Fill valid Index_No ")]
        [RegularExpression(@"^(\d{7})$", ErrorMessage = "No white space allowed")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Please fill the valid Index No")]
        public string Index_No { get; set; }
        [Required(ErrorMessage = "Please fill valid College_Name")]
        public string College_Name { get; set; }
        [Required(ErrorMessage = "Please fill valid College_Address")]
        [StringLength(300)]
        public string College_Address { get; set; }
        [Required(ErrorMessage = "  Co-Ordinator Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Coordinator_Name { get; set; }
        [Required(ErrorMessage = "Please fill valid Coordinator_Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please fill the valid  number")]
        public string Coordinator_Mobile { get; set; }
        [Required(ErrorMessage = "Please fill valid Coordinator_Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Coordinator_Email { get; set; }
        [Required(ErrorMessage = "Please fill valid Coordinator_Education")]
        public string Coordinator_Eduction { get; set; }
        [Required(ErrorMessage = "Please fill valid Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please fill valid Confirm_Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password do not match.")]
        public string Confirm_Password { get; set; }
        public Nullable<int> Active { get; set; }
    }
}