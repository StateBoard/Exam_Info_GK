using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class FileUploadModel
    {
        public string Index_No { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        public string Teacher_Name { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please fill the valid  number")]
        public string Mobile_No { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email_Id { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of total students must be numeric")]
        public string Total_Students { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of total students must be numeric")]
        public string Total_Present_Student { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of total present students must be numeric")]
        public string Total_Absent_Student { get; set; }
        [Required(ErrorMessage = "No.of text files should be equal or greater than present student")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No.of total absent students must be numeric")]
        [Compare("Total_Present_Student", ErrorMessage = " No.of text files should be equal or greater than present student")]
        public string Text_Ans_File { get; set; }
        [Required(ErrorMessage = "This field is  required")]
        [Compare("Text_Ans_File", ErrorMessage = " No.of Uploaded files should be equal or greater than  no.of present student")]
        public string files { get; set; }

        public int Not_Upload_File { get; set; }

    }
}