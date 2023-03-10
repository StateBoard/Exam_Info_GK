using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class Board_Model
    {
        public int Division_Code { get; set; }
        public string Division_Name { get; set; }
        public string District_Code { get; set; }
        public string District_Name { get; set; }

        public string Taluka_Code { get; set; }
        public string Taluka_Name { get; set; }
    }
}