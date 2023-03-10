using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class IT_College_List_Model
    {
        public string Index_No { get; set; }
        public string College_Name { get; set; }
        public string College_Address { get; set; }
        public string Principal_Name { get; set; }
        public string Principal_Mobile { get; set; }
        public string Total_Students { get; set; }
        public string Total_Machines { get; set; }
        public string Total_Teachers { get; set; }
        public string IT_Teacher_Name { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
        public string IT_Teachers_MobileNumber1 { get; set; }
        public string IT_Teachers_Mobilenumber2 { get; set; }
        public Nullable<int> Active { get; set; }

    }
}