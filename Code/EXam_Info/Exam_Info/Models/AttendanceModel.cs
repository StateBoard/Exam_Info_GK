using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class AttendanceModel
    {
        public int ID { get; set; }
        public string Index_No { get; set; }
        public string Batch { get; set; }
        public string Seat_No { get; set; }
        public string Name { get; set; }
        public string Mother_Name { get; set; }

        public string Attendance { get; set; }
        public string Attendance_Status { get; set; }
        public string Paper_ID { get; set; }
    }
}