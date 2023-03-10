using Exam_Info.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Exam_Info.Controllers
{
    public class AdminController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        // GET: Admin
        public ActionResult AdminPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminPage(Admin_tbl model)
        {
            if (model != null && model.Heading != null && model.Heading != "0" && model.Heading != "")
            {
                try
                {
                    if (model.Id != 0 && model.Heading != null && model.Heading != "0" && model.Heading != "")
                    {
                        var oldrecord = db.Admin_tbl.Where(x => x.Id == model.Id).FirstOrDefault();
                        oldrecord.Heading = model.Heading;
                        if (model.File != null)
                        {
                            string file1 = Path.GetExtension(model.File.FileName);
                            string path = Server.MapPath(oldrecord.Extension);
                            FileInfo file3 = new FileInfo(path);

                            if (file3.Exists)//check file exsit or not  
                            {
                                file3.Delete();
                            }

                            string Filename1 = oldrecord.Id + file1;
                            model.Extension = "../Attachments/" + Filename1;
                            oldrecord.Extension = model.Extension;
                            model.File.SaveAs(Path.Combine(Server.MapPath("~/Attachments/"), Filename1));
                        }
                        db.Admin_tbl.Attach(oldrecord);
                        db.Entry(oldrecord).Property(x => x.Heading).IsModified = true;
                        db.Entry(oldrecord).Property(x => x.Extension).IsModified = true;
                        db.SaveChanges();
                        return Json(new { Result = true, Response = "Record Edited Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    model.Active = "1";
                    db.Admin_tbl.Add(model);
                    db.SaveChanges();
                    var data = db.Admin_tbl.Where(x => x.Heading == model.Heading).FirstOrDefault();
                    if (model.File != null)
                    {
                        string file = Path.GetExtension(model.File.FileName);

                        string Filename = data.Id + file;
                        model.Extension = "../Attachments/" + Filename;
                        model.File.SaveAs(Path.Combine(Server.MapPath("~/Attachments/"), Filename));
                        db.Admin_tbl.Attach(model);
                        db.Entry(model).Property(x => x.Extension).IsModified = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Response = e }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = true, Response = "Record Added Successfully" }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
        public JsonResult GetRecord(string type)
        {
            if (type != null && type != "0" && type != "")
            {
                var rec = db.Admin_tbl.Where(x => x.Type == type).ToList();
                return Json(new { Result = true, Response = rec }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var record = db.Admin_tbl.ToList();
                return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetStudentRecord(int id)
        {
            var record = db.Admin_tbl.Where(x => x.Id == id).FirstOrDefault();
            return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DelStudenRecord(int id)
        {
            try
            {
                var oldrecord = db.Admin_tbl.Where(x => x.Id == id).FirstOrDefault();
                if (oldrecord.Active == "1")
                {
                    oldrecord.Active = "0";
                }
                else { oldrecord.Active = "1"; }
                db.Admin_tbl.Attach(oldrecord);
                db.Entry(oldrecord).Property(x => x.Active).IsModified = true;
                db.SaveChanges();
                return Json(new { Result = true, Response = "Deactivated successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Failed To Deactivate Record." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Log_Dash_Details()
        {
            var model = db.Admin_tbl.Where(x => x.Type == "Home Dashboard" && x.Active == "1").ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult College_Dash_Details()
        {
            var model = db.Admin_tbl.Where(x => x.Type == "College Dashboard" && x.Active == "1").ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult District_Dash_Details()
        {
            var model = db.Admin_tbl.Where(x => x.Type == "District Dashboard" && x.Active == "1").ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Division_Dash_Details()
        {
            var model = db.Admin_tbl.Where(x => x.Type == "Division Dashboard" && x.Active == "1").ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult State_Dash_Details()
        {
            var model = db.Admin_tbl.Where(x => x.Type == "State Dashboard" && x.Active == "1").ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Batch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Batch(Tbl_Batch_Activation model)
        {
            if (model != null)
            {
                try
                {
                    if (model.ID != 0)
                    {
                        var oldrecord = db.Tbl_Batch_Activation.Where(x => x.ID == model.ID).FirstOrDefault();
                        oldrecord.Batch = model.Batch;
                        db.Tbl_Batch_Activation.Attach(oldrecord);
                        db.Entry(oldrecord).Property(x => x.Batch).IsModified = true;
                        db.SaveChanges();
                        return Json(new { Result = true, Response = "Record Edited Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    model.Active = 1;
                    db.Tbl_Batch_Activation.Add(model);
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }
            }
            return Json(new { Result = true, Response = "Batch Added successfully." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBatch()
        {
            var record = db.Tbl_Batch_Activation.ToList();
            return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBatchRecord(int id)
        {
            var record = db.Tbl_Batch_Activation.Where(x => x.ID == id).FirstOrDefault();
            return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DelBatchRecord(int id)
        {
            try
            {
                var oldrecord = db.Tbl_Batch_Activation.Where(x => x.ID == id).FirstOrDefault();
                if (oldrecord.Active == 1)
                {
                    oldrecord.Active = 0;
                }
                else { oldrecord.Active = 1; }
                db.Tbl_Batch_Activation.Attach(oldrecord);
                db.Entry(oldrecord).Property(x => x.Active).IsModified = true;
                db.SaveChanges();
                return Json(new { Result = true, Response = "Deactivated successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Failed To Deactivate Record." }, JsonRequestBehavior.AllowGet);
            }
        }

       
            public JsonResult GetPrductDetails()
        {
            var records = (from p in db.Admin_tbl select p).ToList();
            return Json(new { Result = true, Response = records }, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetProdByID(int ID)
        {
            try
            {
                var record = db.Admin_tbl.Where(x => x.Id == ID).FirstOrDefault();
                return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Cannot Fetch" }, JsonRequestBehavior.AllowGet);
            }

        }
      
        public JsonResult DeleteById(int ID)
        {
            try
            {
                var Record = db.Admin_tbl.Where(x => x.Id == ID).FirstOrDefault();
                if (Record.Active == "1")
                {
                    Record.Active = "0";
                }
                else {
                    Record.Active = "1";
                }
                db.Admin_tbl.Attach(Record);
                db.Entry(Record).Property(x => x.Active).IsModified = true;
                db.SaveChanges();
                if(Record.Active=="1")
                {
                    return Json(new { Result = true, Response = "Activated successfully." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = true, Response = "Deactivated successfully." }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Fail To deactivate Record." }, JsonRequestBehavior.AllowGet);
            }
        }
        public string Uploadfile(HttpPostedFileBase file)

        {

            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)

            {

                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".pdf") || extension.ToLower().Equals(".docx") || extension.ToLower().Equals(".png"))

                {

                    try

                    {



                        path = Path.Combine(Server.MapPath("~/Attachments"), random + Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "~/Attachments/" + random + Path.GetFileName(file.FileName);



                        //    ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception ex)

                    {

                        path = "-1";

                    }

                }

                else

                {

                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

                }

            }



            else

            {

                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";

            }



            return path;

        }

        
    }
}