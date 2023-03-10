using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class College_DashBoard_Model
    {
        public List<CoOrdinator_Display_Model> coOrdinator_Display_Model { get; set; }
        public List<Tbl_College_Registration> tbl_College_Registrations { get; set; }
    }
}