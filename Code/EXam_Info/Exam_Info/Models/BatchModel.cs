using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Info.Models
{
    public class BatchModel
    {
        public string Reschedule_Batch { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<Batch_Record> Mark_Model { get; set; }
    }
    public class Batch_Record
    {
        public int ID { get; set; }
        public string Index_No { get; set; }
        public string Seat_No { get; set; }
        public string Name { get; set; }
        public string Batch { get; set; }
        public string Status { get; set; }
      
        public string Approved_By_Division { get; set; }
        public string Approved_By_OLE { get; set; }
        public string Letter { get; set; }
    }


}