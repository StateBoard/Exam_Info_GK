using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class CollegeRegistrationValidation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = " Index No is required")]
        [RegularExpression(@"^(\d{7})$", ErrorMessage = "No white space allowed")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Please fill the valid Index No")]
        public string Index_No { get; set; }
        [Required(ErrorMessage = " College Name is required")]
        public string College_Name { get; set; }
        [Required(ErrorMessage = " College Address is required")]
        [StringLength(300)]
        public string College_Address { get; set; }
        [Required(ErrorMessage = " Principal  Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Principal_Name { get; set; }
        [Required(ErrorMessage = " You must provide a mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        public string Principal_Mobile { get; set; }
        [Required(ErrorMessage = " Please provide no.of total students")]
        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of total students must be numeric")]
        public string Total_Students { get; set; }
        [Required(ErrorMessage = " Please provide no.of  total machines")]
        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of total machines must be numeric")]
        public string Total_Machines { get; set; }
        [Required(ErrorMessage = " Please provide no.of  total teachers")]
        [MaxLength(4)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of   total teachers must be numeric")]
        public string Total_Teachers { get; set; }
        [Required(ErrorMessage = "  IT Teacher Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string IT_Teacher_Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm_Password is required ")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password do not match.")]
        public string Confirm_Password { get; set; }
        [Required(ErrorMessage = " You must provide a mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        public string IT_Teachers_MobileNumber1 { get; set; }
        [Required(ErrorMessage = "IT_Teachers_Mobilenumber2")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        public string IT_Teachers_Mobilenumber2 { get; set; }

        public Nullable<int> Active { get; set; }

    }
}