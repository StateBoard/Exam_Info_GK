using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class Student_Model
    {
        public int id { get; set; }
        public string Index_No { get; set; }
        public string Seat_No { get; set; }
        public string Name { get; set; }
        public string Mother_Name { get; set; }
        public string Batch { get; set; }
        public string Stream { get; set; }
        public Nullable<int> Div { get; set; }
        public string Hand { get; set; }
        public Nullable<int> Reschedule_Status { get; set; }
        public string Reschedule_Batch { get; set; }
        public string Reschedule_Approve_BY { get; set; }
        public string Course { get; set; }
    }
}



