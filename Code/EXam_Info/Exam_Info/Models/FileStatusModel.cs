using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class FileStatusModel
    {
        public string Index_No { get; set; }
        public string Seat_No { get; set; }
        public string Uploaded_Seat_File { get; set; }
        public string File_Status { get; set; }
        public string Reason { get; set; }

    }
    public class FileStatus
    {
        public string Index_No { get; set; }
        public string Teacher_Name { get; set; }
        public string Mobile_No { get; set; }
        public string Email_Id { get; set; }
        public int Total_Students { get; set; }
        public int Total_Present_Student { get; set; }
        public int Total_Absent_Student { get; set; }
        public int Text_Ans_File { get; set; }
        public List<FileStatusModel> fileStatuses { get; set; }

    }


}