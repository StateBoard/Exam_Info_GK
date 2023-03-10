using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Info.Helper;
using Exam_Info.Models;
using PagedList;
using PagedList.Mvc;

namespace Exam_Info.Controllers
{
    public class DivisionController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        Common common = new Common();

        // GET: Division
        public ActionResult Division_Dashboard()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            if (login_model == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult Division_Batch_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["Division_Code"] = mm.Division_Code;
            return View();
        }
        [HttpPost]
        public ActionResult Division_Batch_List(string dist, string taluka, string batch, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (dist != "0" && dist != "" && dist != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + mm.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                if (mm.Division_Code != 0 && dist == "0" || dist == "" || dist == null)
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + mm.Division_Code + ") and Batch = '" + batch + "'";
                }
                else
                {
                    Query = "select A.* from Tbl_Login A where" + Temp + "and Batch = '" + batch + "'";
                }

                List<Student_Model> model = db.Database.SqlQuery<Student_Model>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "Division_Batch_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Division_Batch_List";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Division_College_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["Division_Code"] = mm.Division_Code;
            return View();
        }
        [HttpPost]
        public ActionResult Division_College_List(string district, string taluka, string Excel, string type)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (district != "0" && district != "" && district != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + district + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                if (type == "Complete")
                {
                    if (board_Model.Division_Code != 0 && district == null || district == "0" || district == "")
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ") and B.Index_No is not null";
                    }
                    if (district != null && district != "0" && district != "")
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where " + Temp + "and B.Index_No is not null";
                    }
                }

                if (type == "InComplete")
                {
                    if (board_Model.Division_Code != 0)
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ") and B.Index_No is null";
                    }
                    if (district != null && district != "0" && district != "")
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where " + Temp + "and B.Index_No is null";
                    }
                }
                List<IT_College_List_Model> model = db.Database.SqlQuery<IT_College_List_Model>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "College_Details";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "College_Details";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" + ex }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult Load_Taluka(string distCode)
        {
            List<SelectListItem> list = common.Get_Taluka_List(distCode);

            return Json(new SelectList(list, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult Get_District(string divCode)
        {
            List<SelectListItem> list = common.Get_District_List(divCode);

            return Json(new SelectList(list, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public ActionResult Division_Edit_profile()
        {
            Tbl_Division_Co_Ordinator_Registration dc = new Tbl_Division_Co_Ordinator_Registration();
            //string Index = Session["Index_No"].ToString();
            Login_Model login_model = common.Get_Login_Details();

            int temp = db.Tbl_Division_Co_Ordinator_Registration.Where(x => x.Index_No == login_model.Index_No).Count();
            if (temp > 0)
            {
                dc = db.Tbl_Division_Co_Ordinator_Registration.Where(x => x.Index_No == login_model.Index_No).FirstOrDefault();
            }
            return View(dc);
        }

        [HttpPost]
        public ActionResult Division_Edit_profile(Tbl_Division_Co_Ordinator_Registration Update_Division_Info)
        {
            try
            {
                db.Tbl_Division_Co_Ordinator_Registration.Add(Update_Division_Info);
                if (Update_Division_Info.ID != 0)
                {
                    db.Entry(Update_Division_Info).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Result = true, Response = "Record Updated Sucessfully...!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {

            }
            return View();
        }

        public ActionResult Division_Inspection()
        {
            //var div = Session["Division_Code"].ToString();
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);

            var model = db.Tbl_Code_Master.Where(x => x.division_code == mm.Division_Code.ToString()).Select(n => new
            {
                n.district_name,
                n.district_code

            }).Distinct().OrderBy(n => n.district_name).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select District--", Value = "0" });
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.district_name.ToString(), Value = item.district_code.ToString() });
                }
            }
            ViewBag.DistrictList = new SelectList(list, "Value", "Text");
            return View();
        }


        //public JsonResult Division_Inspection(string district, string taluka)
        //{
        //    var model = db.Tbl_Inspection.Where(x => x.Index_No.Substring(0, 2) == district && x.Index_No.Substring(2, 2) == taluka).AsEnumerable().Select((n, i) => new
        //    {
        //        Sr_No = n.ID = i + 1, Index_No = n.Index_No, System_No = n.SYS_No, OS_Name = n.OS_Name, RAM = n.Ram, n.HDD, n.MAC, n.Browser_Name, External_IP = n.Extn_IP, n.Screen_Res, n.IE_Version,
        //        n.Active,
        //    }).AsEnumerable().ToList();

        //    return Json(new { Result = true, Response = model }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult Division_Inspection(string district, string taluka, string Excel)
        {
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (district != "0" && district != "" && district != null)
                {

                    Temp += " and Substring(A.index_No,1,2)='" + district + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_Inspection A where A.active=1 " + Temp;

                List<Tbl_Inspection> model = db.Database.SqlQuery<Tbl_Inspection>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "Inspection";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Inspection";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Division_Data_Test1()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            var model = db.Tbl_Code_Master.Where(x => x.division_code == mm.Division_Code.ToString()).Select(n => new
            {
                n.district_name,
                n.district_code

            }).Distinct().OrderBy(n => n.district_name).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select District--", Value = "0" });
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.district_name.ToString(), Value = item.district_code.ToString() });
                }
            }
            ViewBag.DistrictList = new SelectList(list, "Value", "Text");
            return View();
        }
        [HttpPost]
        public JsonResult Division_Data_Test1(string district, string taluka, string Excel)
        {
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (district != "0" && district != "" && district != null)
                {

                    Temp += " and Substring(A.index_No,1,2)='" + district + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_DTT1 A where A.active=1 " + Temp;

                List<Tbl_DTT1> model = db.Database.SqlQuery<Tbl_DTT1>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "Div_DTT1";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Div_DTT1";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult Division_Data_Test1(string district, string taluka)
        //{
        //    var model = db.Tbl_DTT1.Where(x => x.Index_No.Substring(0, 2) == district && x.Index_No.Substring(2, 2) == taluka).AsEnumerable().Select((n, i) => new
        //    {
        //        Sr_No = n.ID = i + 1,
        //        Index_No = n.Index_No,
        //        Index_NoN = n.Index_NoN,
        //        System_No = n.SYS_No,
        //        n.MAC,
        //        n.Screen_Res,
        //        n.Screen_Res_Change,
        //        n.Read_Wrtite_Access,
        //        n.Processor,
        //        n.Active,
        //    }).AsEnumerable().ToList();

        //    return Json(new { Result = true, Response = model }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Division_Data_Test2()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            var model = db.Tbl_Code_Master.Where(x => x.division_code == mm.Division_Code.ToString()).Select(n => new
            {
                n.district_name,
                n.district_code

            }).Distinct().OrderBy(n => n.district_name).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select District--", Value = "0" });
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.district_name.ToString(), Value = item.district_code.ToString() });
                }
            }
            ViewBag.DistrictList = new SelectList(list, "Value", "Text");
            return View();
        }

        //    public JsonResult Division_Data_Test2(string district, string taluka)
        //    {
        //        var model = db.Tbl_DTT2.Where(x => x.Index_No.Substring(0, 2) == district && x.Index_No.Substring(2, 2) == taluka).AsEnumerable().Select((n, i) => new
        //        {
        //            Sr_No = n.ID = i + 1,
        //            Index_No = n.Index_No,
        //            Index_No_Old = n.Index_No_OLD,
        //            n.Read_Wrtite_Access,
        //            Date_Time_Set = n.datetime_set,
        //            n.MAC,
        //            n.Active,
        //        }).AsEnumerable().ToList();

        //        return Json(new { Result = true, Response = model }, JsonRequestBehavior.AllowGet);
        //    }
        [HttpPost]
        public JsonResult Division_Data_Test2(string district, string taluka, string Excel)
        {
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (district != "0" && district != "" && district != null)
                {

                    Temp += " and Substring(A.index_No,1,2)='" + district + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_DTT2 A where A.active=1 " + Temp;

                List<Tbl_DTT2> model = db.Database.SqlQuery<Tbl_DTT2>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "Div_DTT2";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Div_DTT2";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Division_Data_Test3(string dist, string taluka, int? page)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            List<SelectListItem> list = common.Get_List(board_Model);
            ViewBag.DistrictList = new SelectList(list, "Value", "Text");
            TempData["Division_Code"] = board_Model.Division_Code;

            try
            {
                string Temp = "", Query = "";
                if (dist != "0" && dist != "" && dist != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + dist + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                if (dist == "0" || dist == "" || dist == null)
                {
                    Query = "select A.* from Tbl_DTT3 A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                else
                {
                    Query = "select A.* from Tbl_DTT3 A where" + Temp;
                }

                return View(db.Database.SqlQuery<Tbl_DTT3>(Query).ToList().OrderBy(s => s.Index_No).ToPagedList(page ?? 1, 100));
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        [HttpPost]
        public JsonResult Division_Data_Test3(string district, string taluka, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (district != "0" && district != "" && district != null)
                {

                    Temp += " Substring(A.index_No,1,2)='" + district + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                if (district == "0" || district == "" || district == null)
                {
                    Query = "select A.* from Tbl_DTT3 A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                else
                {
                    Query = "select A.* from Tbl_DTT3 A where" + Temp;
                }

                List<Tbl_DTT3> model = db.Database.SqlQuery<Tbl_DTT3>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "Division_DTT3";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Division_DTT3";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Divi_Stud_Login(string dist, string taluka, int? page)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["Division_Code"] = mm.Division_Code;
            try
            {
                string Temp = "", Query = "";
                if (dist != "0" && dist != "" && dist != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + dist + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                if (mm.Division_Code != 0 && dist == "0" || dist == "" || dist == null)
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + mm.Division_Code + ");";
                }
                else
                {
                    Query = "select A.* from Tbl_Login A where" + Temp;
                }
                var model = db.Database.SqlQuery<Tbl_Login>(Query).ToList();
                List<Student_Model> list = new List<Student_Model>();
                foreach (var item in model)
                {
                    Student_Model sm = new Student_Model();
                    sm.Index_No = item.Index_No; sm.Name = item.Name; sm.Seat_No = item.Seat_No;
                    list.Add(sm);
                }
                return View(list.ToList().OrderBy(x => x.Index_No).OrderBy(x => x.Index_No).ToPagedList(page ?? 1, 100));
            }
            catch (Exception e)
            {

            }
            return View();
        }
        [HttpPost]
        public JsonResult Divi_Stud_Login(string dist, string taluka, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (dist != "0" && dist != "" && dist != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + dist + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                if (mm.Division_Code != 0 && dist == "0" || dist == "" || dist == null)
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + mm.Division_Code + ");";
                }
                else
                {
                    Query = "select A.* from Tbl_Login A where" + Temp;
                }

                List<Tbl_Login> model = (List<Tbl_Login>)db.Database.SqlQuery<Tbl_Login>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "Division_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Division_List";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Division_Reschedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Division_Reschedule(string batch)
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            if (db.Tbl_Reschedule_ApproveBy_Division.Any(x => x.Index_No == login_Model.Index_No))
            {
                var model = db.Tbl_Reschedule_Student.Where(x => x.Initial_Batch == batch && x.Division_Code == board_Model.Division_Code.ToString()).ToList().OrderBy(x => x.Seat_No);
                return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Result = false, Response = "You do not have permission to Reschedule Student!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Reschedule_Student(BatchModel model)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);

            try
            {
                if (model.Mark_Model.Count != 0)
                {
                    foreach (var item in model.Mark_Model)
                    {
                        if (item.Status == "checked")
                        {
                            if (db.Tbl_Reschedule_Student.Any(x => x.Seat_No == item.Seat_No))
                            {
                                var oldrecord = db.Tbl_Reschedule_Student.Where(x => x.Seat_No == item.Seat_No).FirstOrDefault();
                                oldrecord.Approved_By_Division = "Approved";

                                db.Tbl_Reschedule_Student.Attach(oldrecord);
                                db.Entry(oldrecord).Property(x => x.Approved_By_Division).IsModified = true;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return Json(new { Result = true, Message = "Reschedule Initiated!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = true, ex }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Check_Mark_Status(string dist, string taluka, string seat, int? page)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            List<SelectListItem> list = common.Get_List(board_Model);
            ViewBag.DistrictList = new SelectList(list, "Value", "Text");
            TempData["Division_Code"] = board_Model.Division_Code;

            try
            {
                string Temp = "", Query = "";
                if (dist != "0" && dist != "" && dist != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + dist + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                if (dist == "0" || dist == "" || dist == null)
                {
                    Query = "select A.* from Tbl_Final_Ans A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                else
                {
                    Query = "select A.* from Tbl_Final_Ans A where" + Temp;
                }

                if (seat != "0" || seat != "" || seat != null)
                {
                    var record = db.Tbl_Final_Ans.Where(x => x.Seat_No == seat).ToList();
                    List<Ans_Model> singleans = new List<Ans_Model>();
                    foreach (var item in record)
                    {
                        Ans_Model sm = new Ans_Model();
                        sm.Index_No = item.Index_No.ToString(); sm.Seat_No = item.Seat_No; sm.Paper_ID = item.Paper_ID;
                        if (item.Q1_Ans != null && item.Q1_Ans .Trim() != "") { sm.Q1_Ans = "*"; } else { sm.Q1_Ans  = "NULL"; }
                        if (item.Q_2Ans != null && item.Q_2Ans .Trim() != "") { sm.Q_2Ans = "*"; } else { sm.Q_2Ans  = "NULL"; }
                        if (item.Q_3Ans != null && item.Q_3Ans .Trim() != "") { sm.Q_3Ans = "*"; } else { sm.Q_3Ans  = "NULL"; }
                        if (item.Q_4Ans != null && item.Q_4Ans .Trim() != "") { sm.Q_4Ans = "*"; } else { sm.Q_4Ans  = "NULL"; }
                        if (item.Q_5Ans != null && item.Q_5Ans .Trim() != "") { sm.Q_5Ans = "*"; } else { sm.Q_5Ans  = "NULL"; }
                        if (item.Q_6Ans != null && item.Q_6Ans .Trim() != "") { sm.Q_6Ans = "*"; } else { sm.Q_6Ans  = "NULL"; }
                        if (item.Q_7Ans != null && item.Q_7Ans .Trim() != "") { sm.Q_7Ans = "*"; } else { sm.Q_7Ans  = "NULL"; }
                        if (item.Q_8Ans != null && item.Q_8Ans .Trim() != "") { sm.Q_8Ans = "*"; } else { sm.Q_8Ans  = "NULL"; }
                        if (item.Q_9Ans != null && item.Q_9Ans.Trim() != "")  { sm.Q_9Ans = "*"; } else { sm.Q_9Ans  = "NULL"; }
                        if (item.Q_10Ans != null && item.Q_10Ans.Trim() != "") { sm.Q_10Ans = "*";} else{sm.Q_10Ans  = "NULL"; }

                        if (item.Q_11Ans != null && item.Q_11Ans.Trim() != "") { sm.Q_11Ans= "*"; } else { sm.Q_11Ans = "NULL"; }
                        if (item.Q_12Ans != null && item.Q_12Ans.Trim() != "") { sm.Q_12Ans= "*"; } else { sm.Q_12Ans = "NULL"; }
                        if (item.Q_13Ans != null && item.Q_13Ans.Trim() != "") { sm.Q_13Ans= "*"; } else { sm.Q_13Ans = "NULL"; }
                        if (item.Q_14Ans != null && item.Q_14Ans.Trim() != "") { sm.Q_14Ans= "*"; } else { sm.Q_14Ans = "NULL"; }
                        if (item.Q_15Ans != null && item.Q_15Ans.Trim() != "") { sm.Q_15Ans= "*"; } else { sm.Q_15Ans = "NULL"; }
                        if (item.Q_16Ans != null && item.Q_16Ans.Trim() != "") { sm.Q_16Ans= "*"; } else { sm.Q_16Ans = "NULL"; }
                        if (item.Q_17Ans != null && item.Q_17Ans.Trim() != "") { sm.Q_17Ans= "*"; } else { sm.Q_17Ans = "NULL"; }
                        if (item.Q_18Ans != null && item.Q_18Ans.Trim() != "") { sm.Q_18Ans= "*"; } else { sm.Q_18Ans = "NULL"; }
                        if (item.Q_19Ans != null && item.Q_19Ans.Trim() != "") { sm.Q_19Ans= "*"; } else { sm.Q_19Ans = "NULL"; }
                        if (item.Q_20Ans != null && item.Q_20Ans.Trim() != "") { sm.Q_20Ans = "*"; } else { sm.Q_20Ans = "NULL"; }

                        if (item.Q_21Ans != null && item.Q_21Ans.Trim() != "") { sm.Q_21Ans= "*"; } else { sm.Q_21Ans = "NULL"; }
                        if (item.Q_22Ans != null && item.Q_22Ans.Trim() != "") { sm.Q_22Ans= "*"; } else { sm.Q_22Ans = "NULL"; }
                        if (item.Q_23Ans != null && item.Q_23Ans.Trim() != "") { sm.Q_23Ans= "*"; } else { sm.Q_23Ans = "NULL"; }
                        if (item.Q_24Ans != null && item.Q_24Ans.Trim() != "") { sm.Q_24Ans= "*"; } else { sm.Q_24Ans = "NULL"; }
                        if (item.Q_25Ans != null && item.Q_25Ans.Trim() != "") { sm.Q_25Ans= "*"; } else { sm.Q_25Ans = "NULL"; }
                        if (item.Q_26Ans != null && item.Q_26Ans.Trim() != "") { sm.Q_26Ans= "*"; } else { sm.Q_26Ans = "NULL"; }
                        if (item.Q_27Ans != null && item.Q_27Ans.Trim() != "") { sm.Q_27Ans= "*"; } else { sm.Q_27Ans = "NULL"; }
                        if (item.Q_28Ans != null && item.Q_28Ans.Trim() != "") { sm.Q_28Ans= "*"; } else { sm.Q_28Ans = "NULL"; }
                        if (item.Q_29Ans != null && item.Q_29Ans.Trim() != "") { sm.Q_29Ans= "*"; } else { sm.Q_29Ans = "NULL"; }
                        if (item.Q_30Ans != null && item.Q_30Ans.Trim() != "") { sm.Q_30Ans = "*"; } else { sm.Q_30Ans = "NULL"; }


                        if (item.Q_31Ans != null && item.Q_31Ans.Trim() != "") { sm.Q_31Ans= "*"; } else { sm.Q_31Ans = "NULL"; }
                        if (item.Q_32Ans != null && item.Q_32Ans.Trim() != "") { sm.Q_32Ans= "*"; } else { sm.Q_32Ans = "NULL"; }
                        if (item.Q_33Ans != null && item.Q_33Ans.Trim() != "") { sm.Q_33Ans= "*"; } else { sm.Q_33Ans = "NULL"; }
                        if (item.Q_34Ans != null && item.Q_34Ans.Trim() != "") { sm.Q_34Ans= "*"; } else { sm.Q_34Ans = "NULL"; }
                        if (item.Q_35Ans != null && item.Q_35Ans.Trim() != "") { sm.Q_35Ans= "*"; } else { sm.Q_35Ans = "NULL"; }
                        if (item.Q_36Ans != null && item.Q_36Ans.Trim() != "") { sm.Q_36Ans = "*"; } else { sm.Q_36Ans = "NULL"; }
                        if (item.Q_37Ans != null && item.Q_37Ans.Trim() != "") { sm.Q_37Ans = "*"; } else { sm.Q_37Ans = "NULL"; }
                        if (item.Q_38Ans != null && item.Q_38Ans.Trim() != "") { sm.Q_38Ans = "*"; } else { sm.Q_38Ans = "NULL"; }
                        if (item.Q_39Ans != null && item.Q_39Ans.Trim() != "") { sm.Q_39Ans = "*"; } else { sm.Q_39Ans = "NULL"; }
                        if (item.Q_40Ans != null && item.Q_40Ans.Trim() != "") { sm.Q_40Ans = "*"; } else { sm.Q_40Ans = "NULL"; }

                        if (item.Q_41Ans != null && item.Q_41Ans.Trim() != "") { sm.Q_41Ans = "*"; } else { sm.Q_41Ans = "NULL"; }
                        if (item.Q_42Ans != null && item.Q_42Ans.Trim() != "") { sm.Q_42Ans = "*"; } else { sm.Q_42Ans = "NULL"; }
                        if (item.Q_43Ans != null && item.Q_43Ans.Trim() != "") { sm.Q_43Ans = "*"; } else { sm.Q_43Ans = "NULL"; }
                        if (item.Q_44Ans != null && item.Q_44Ans.Trim() != "") { sm.Q_44Ans = "*"; } else { sm.Q_44Ans = "NULL"; }
                        if (item.Q_45Ans != null && item.Q_45Ans.Trim() != "") { sm.Q_45Ans = "*"; } else { sm.Q_45Ans = "NULL"; }
                        if (item.Q_46Ans != null && item.Q_46Ans.Trim() != "") { sm.Q_46Ans = "*"; } else { sm.Q_46Ans = "NULL"; }
                        if (item.Q_48Ans != null && item.Q_48Ans.Trim() != "") { sm.Q_48Ans = "*"; } else { sm.Q_48Ans = "NULL"; }
                        if (item.Q_49Ans != null && item.Q_49Ans.Trim() != "") { sm.Q_49Ans = "*"; } else { sm.Q_49Ans = "NULL"; }
                        if (item.Q_50Ans != null && item.Q_50Ans.Trim() != "") { sm.Q_50Ans = "*"; } else { sm.Q_50Ans = "NULL"; }

                        if (item.Q_51Ans != null && item.Q_51Ans.Trim() != "") { sm.Q_51Ans = "*"; } else { sm.Q_51Ans = "NULL"; }
                        if (item.Q_52Ans != null && item.Q_52Ans.Trim() != "") { sm.Q_52Ans = "*"; } else { sm.Q_52Ans = "NULL"; }
                        if (item.Q_53Ans != null && item.Q_53Ans.Trim() != "") { sm.Q_53Ans = "*"; } else { sm.Q_53Ans = "NULL"; }
                        if (item.Q_54Ans != null && item.Q_54Ans.Trim() != "") { sm.Q_54Ans = "*"; } else { sm.Q_54Ans = "NULL"; }
                        if (item.Q_55Ans != null && item.Q_55Ans.Trim() != "") { sm.Q_55Ans = "*"; } else { sm.Q_55Ans = "NULL"; }
                        if (item.Q_56Ans != null && item.Q_56Ans.Trim() != "") { sm.Q_56Ans = "*"; } else { sm.Q_56Ans = "NULL"; }
                        if (item.Q_57Ans != null && item.Q_57Ans.Trim() != "") { sm.Q_57Ans = "*"; } else { sm.Q_57Ans = "NULL"; }
                        if (item.Q_58Ans != null && item.Q_58Ans.Trim() != "") { sm.Q_58Ans = "*"; } else { sm.Q_58Ans = "NULL"; }
                        if (item.Q_59Ans != null && item.Q_59Ans.Trim() != "") { sm.Q_59Ans = "*"; } else { sm.Q_59Ans = "NULL"; }
                        if (item.Q_60Ans != null && item.Q_60Ans.Trim() != "") { sm.Q_60Ans = "*"; } else { sm.Q_60Ans = "NULL"; }

                        if (item.Q_61Ans != null && item.Q_61Ans.Trim() != "") { sm.Q_61Ans = "*"; } else { sm.Q_61Ans = "NULL"; }
                        if (item.Q_62Ans != null && item.Q_62Ans.Trim() != "") { sm.Q_62Ans = "*"; } else { sm.Q_62Ans = "NULL"; }
                        if (item.Q_63Ans != null && item.Q_63Ans.Trim() != "") { sm.Q_63Ans = "*"; } else { sm.Q_63Ans = "NULL"; }
                        if (item.Q_64Ans != null && item.Q_64Ans.Trim() != "") { sm.Q_64Ans = "*"; } else { sm.Q_64Ans = "NULL"; }
                        if (item.Q_65Ans != null && item.Q_65Ans.Trim() != "") { sm.Q_65Ans = "*"; } else { sm.Q_65Ans = "NULL"; }
                        if (item.Q_66Ans != null && item.Q_66Ans.Trim() != "") { sm.Q_66Ans = "*"; } else { sm.Q_66Ans = "NULL"; }
                        if (item.Q_67Ans != null && item.Q_67Ans.Trim() != "") { sm.Q_67Ans = "*"; } else { sm.Q_67Ans = "NULL"; }
                        if (item.Q_68Ans != null && item.Q_68Ans.Trim() != "") { sm.Q_68Ans = "*"; } else { sm.Q_68Ans = "NULL"; }
                        if (item.Q_69Ans != null && item.Q_69Ans.Trim() != "") { sm.Q_69Ans = "*"; } else { sm.Q_69Ans = "NULL"; }
                        if (item.Q_70Ans != null && item.Q_70Ans.Trim() != "") { sm.Q_70Ans = "*"; } else { sm.Q_70Ans = "NULL"; }

                        if (item.Q_71Ans != null && item.Q_71Ans.Trim() != "") { sm.Q_71Ans = "*"; } else { sm.Q_71Ans = "NULL"; }
                        if (item.Q_72Ans != null && item.Q_72Ans.Trim() != "") { sm.Q_72Ans = "*"; } else { sm.Q_72Ans = "NULL"; }
                        if (item.Q_73Ans != null && item.Q_73Ans.Trim() != "") { sm.Q_73Ans = "*"; } else { sm.Q_73Ans = "NULL"; }
                        if (item.Q_74Ans != null && item.Q_74Ans.Trim() != "") { sm.Q_74Ans = "*"; } else { sm.Q_74Ans = "NULL"; }
                        if (item.Q_75Ans != null && item.Q_75Ans.Trim() != "") { sm.Q_75Ans = "*"; } else { sm.Q_75Ans = "NULL"; }
                        if (item.Q_76Ans != null && item.Q_76Ans.Trim() != "") { sm.Q_76Ans = "*"; } else { sm.Q_76Ans = "NULL"; }
                        if (item.Q_77Ans != null && item.Q_77Ans.Trim() != "") { sm.Q_77Ans = "*"; } else { sm.Q_77Ans = "NULL"; }
                        if (item.Q_78Ans != null && item.Q_78Ans.Trim() != "") { sm.Q_78Ans = "*"; } else { sm.Q_78Ans = "NULL"; }
                        if (item.Q_79Ans != null && item.Q_79Ans.Trim() != "") { sm.Q_79Ans = "*"; } else { sm.Q_79Ans = "NULL"; }
                        if (item.Q_80Ans != null && item.Q_80Ans.Trim() != "") { sm.Q_80Ans = "*"; } else { sm.Q_80Ans = "NULL"; }

                        if (item.Q_81Ans != null && item.Q_81Ans.Trim() != "") { sm.Q_81Ans = "*"; } else { sm.Q_81Ans = "NULL"; }
                        if (item.Q_82Ans != null && item.Q_82Ans.Trim() != "") { sm.Q_82Ans = "*"; } else { sm.Q_82Ans = "NULL"; }
                        if (item.Q_83Ans != null && item.Q_83Ans.Trim() != "") { sm.Q_83Ans = "*"; } else { sm.Q_83Ans = "NULL"; }
                        if (item.Q_84Ans != null && item.Q_84Ans.Trim() != "") { sm.Q_84Ans = "*"; } else { sm.Q_84Ans = "NULL"; }
                        if (item.Q_85Ans != null && item.Q_85Ans.Trim() != "") { sm.Q_85Ans = "*"; } else { sm.Q_85Ans = "NULL"; }
                        if (item.Q_86Ans != null && item.Q_86Ans.Trim() != "") { sm.Q_86Ans = "*"; } else { sm.Q_86Ans = "NULL"; }
                        if (item.Q_87Ans != null && item.Q_87Ans.Trim() != "") { sm.Q_87Ans = "*"; } else { sm.Q_87Ans = "NULL"; }
                        if (item.Q_88Ans != null && item.Q_88Ans.Trim() != "") { sm.Q_88Ans = "*"; } else { sm.Q_88Ans = "NULL"; }
                        if (item.Q_89Ans != null && item.Q_89Ans.Trim() != "") { sm.Q_89Ans = "*"; } else { sm.Q_89Ans = "NULL"; }
                        if (item.Q_90Ans != null && item.Q_90Ans.Trim() != "") { sm.Q_90Ans = "*"; } else { sm.Q_90Ans = "NULL"; }

                        if (item.Q_91Ans != null && item.Q_91Ans .Trim() != "") { sm.Q_91Ans  = "*"; } else { sm.Q_91Ans  = "NULL"; }
                        if (item.Q_92Ans != null && item.Q_92Ans .Trim() != "") { sm.Q_92Ans  = "*"; } else { sm.Q_92Ans  = "NULL"; }
                        if (item.Q_93Ans != null && item.Q_93Ans .Trim() != "") { sm.Q_93Ans  = "*"; } else { sm.Q_93Ans  = "NULL"; }
                        if (item.Q_94Ans != null && item.Q_94Ans .Trim() != "") { sm.Q_94Ans  = "*"; } else { sm.Q_94Ans  = "NULL"; }
                        if (item.Q_95Ans != null && item.Q_95Ans .Trim() != "") { sm.Q_95Ans  = "*"; } else { sm.Q_95Ans  = "NULL"; }
                        if (item.Q_96Ans != null && item.Q_96Ans .Trim() != "") { sm.Q_96Ans  = "*"; } else { sm.Q_96Ans  = "NULL"; }
                        if (item.Q_97Ans != null && item.Q_97Ans .Trim() != "") { sm.Q_97Ans  = "*"; } else { sm.Q_97Ans  = "NULL"; }
                        if (item.Q_98Ans != null && item.Q_98Ans .Trim() != "") { sm.Q_98Ans  = "*"; } else { sm.Q_98Ans  = "NULL"; }
                        if (item.Q_99Ans != null && item.Q_99Ans .Trim() != "") { sm.Q_99Ans  = "*"; } else { sm.Q_99Ans  = "NULL"; }
                        if (item.Q_100Ans != null &&item.Q_100Ans.Trim() != "") { sm.Q_100Ans = "*"; } else { sm.Q_100Ans = "NULL"; }

                        singleans.Add(sm);
                        return View(singleans.ToPagedList(page ?? 1, 100));
                    }
                }
                List<Ans_Model> model = db.Database.SqlQuery<Ans_Model>(Query).ToList().OrderBy(s => s.Index_No).ToList();

                List<Ans_Model> ans = new List<Ans_Model>();
                foreach (var item in model)
                {
                    Ans_Model sm = new Ans_Model();
                    sm.Index_No = item.Index_No; sm.Batch = item.Batch; sm.Seat_No = item.Seat_No; sm.Paper_ID = item.Paper_ID;
                    if (item.Q1_Ans != null && item.Q1_Ans.Trim() != "") { sm.Q1_Ans = "*"; } else { sm.Q1_Ans = "NULL"; }
                    if (item.Q_2Ans != null && item.Q_2Ans.Trim() != "") { sm.Q_2Ans = "*"; } else { sm.Q_2Ans = "NULL"; }
                    if (item.Q_3Ans != null && item.Q_3Ans.Trim() != "") { sm.Q_3Ans = "*"; } else { sm.Q_3Ans = "NULL"; }
                    if (item.Q_4Ans != null && item.Q_4Ans.Trim() != "") { sm.Q_4Ans = "*"; } else { sm.Q_4Ans = "NULL"; }
                    if (item.Q_5Ans != null && item.Q_5Ans.Trim() != "") { sm.Q_5Ans = "*"; } else { sm.Q_5Ans = "NULL"; }
                    if (item.Q_6Ans != null && item.Q_6Ans.Trim() != "") { sm.Q_6Ans = "*"; } else { sm.Q_6Ans = "NULL"; }
                    if (item.Q_7Ans != null && item.Q_7Ans.Trim() != "") { sm.Q_7Ans = "*"; } else { sm.Q_7Ans = "NULL"; }
                    if (item.Q_8Ans != null && item.Q_8Ans.Trim() != "") { sm.Q_8Ans = "*"; } else { sm.Q_8Ans = "NULL"; }
                    if (item.Q_9Ans != null && item.Q_9Ans.Trim() != "") { sm.Q_9Ans = "*"; } else { sm.Q_9Ans = "NULL"; }
                    if (item.Q_10Ans != null && item.Q_10Ans.Trim() != "") { sm.Q_10Ans = "*"; } else { sm.Q_10Ans = "NULL"; }

                    if (item.Q_11Ans != null && item.Q_11Ans.Trim() != "") { sm.Q_11Ans = "*"; } else { sm.Q_11Ans = "NULL"; }
                    if (item.Q_12Ans != null && item.Q_12Ans.Trim() != "") { sm.Q_12Ans = "*"; } else { sm.Q_12Ans = "NULL"; }
                    if (item.Q_13Ans != null && item.Q_13Ans.Trim() != "") { sm.Q_13Ans = "*"; } else { sm.Q_13Ans = "NULL"; }
                    if (item.Q_14Ans != null && item.Q_14Ans.Trim() != "") { sm.Q_14Ans = "*"; } else { sm.Q_14Ans = "NULL"; }
                    if (item.Q_15Ans != null && item.Q_15Ans.Trim() != "") { sm.Q_15Ans = "*"; } else { sm.Q_15Ans = "NULL"; }
                    if (item.Q_16Ans != null && item.Q_16Ans.Trim() != "") { sm.Q_16Ans = "*"; } else { sm.Q_16Ans = "NULL"; }
                    if (item.Q_17Ans != null && item.Q_17Ans.Trim() != "") { sm.Q_17Ans = "*"; } else { sm.Q_17Ans = "NULL"; }
                    if (item.Q_18Ans != null && item.Q_18Ans.Trim() != "") { sm.Q_18Ans = "*"; } else { sm.Q_18Ans = "NULL"; }
                    if (item.Q_19Ans != null && item.Q_19Ans.Trim() != "") { sm.Q_19Ans = "*"; } else { sm.Q_19Ans = "NULL"; }
                    if (item.Q_20Ans != null && item.Q_20Ans.Trim() != "") { sm.Q_20Ans = "*"; } else { sm.Q_20Ans = "NULL"; }

                    if (item.Q_21Ans != null && item.Q_21Ans.Trim() != "") { sm.Q_21Ans = "*"; } else { sm.Q_21Ans = "NULL"; }
                    if (item.Q_22Ans != null && item.Q_22Ans.Trim() != "") { sm.Q_22Ans = "*"; } else { sm.Q_22Ans = "NULL"; }
                    if (item.Q_23Ans != null && item.Q_23Ans.Trim() != "") { sm.Q_23Ans = "*"; } else { sm.Q_23Ans = "NULL"; }
                    if (item.Q_24Ans != null && item.Q_24Ans.Trim() != "") { sm.Q_24Ans = "*"; } else { sm.Q_24Ans = "NULL"; }
                    if (item.Q_25Ans != null && item.Q_25Ans.Trim() != "") { sm.Q_25Ans = "*"; } else { sm.Q_25Ans = "NULL"; }
                    if (item.Q_26Ans != null && item.Q_26Ans.Trim() != "") { sm.Q_26Ans = "*"; } else { sm.Q_26Ans = "NULL"; }
                    if (item.Q_27Ans != null && item.Q_27Ans.Trim() != "") { sm.Q_27Ans = "*"; } else { sm.Q_27Ans = "NULL"; }
                    if (item.Q_28Ans != null && item.Q_28Ans.Trim() != "") { sm.Q_28Ans = "*"; } else { sm.Q_28Ans = "NULL"; }
                    if (item.Q_29Ans != null && item.Q_29Ans.Trim() != "") { sm.Q_29Ans = "*"; } else { sm.Q_29Ans = "NULL"; }
                    if (item.Q_30Ans != null && item.Q_30Ans.Trim() != "") { sm.Q_30Ans = "*"; } else { sm.Q_30Ans = "NULL"; }


                    if (item.Q_31Ans != null && item.Q_31Ans.Trim() != "") { sm.Q_31Ans = "*"; } else { sm.Q_31Ans = "NULL"; }
                    if (item.Q_32Ans != null && item.Q_32Ans.Trim() != "") { sm.Q_32Ans = "*"; } else { sm.Q_32Ans = "NULL"; }
                    if (item.Q_33Ans != null && item.Q_33Ans.Trim() != "") { sm.Q_33Ans = "*"; } else { sm.Q_33Ans = "NULL"; }
                    if (item.Q_34Ans != null && item.Q_34Ans.Trim() != "") { sm.Q_34Ans = "*"; } else { sm.Q_34Ans = "NULL"; }
                    if (item.Q_35Ans != null && item.Q_35Ans.Trim() != "") { sm.Q_35Ans = "*"; } else { sm.Q_35Ans = "NULL"; }
                    if (item.Q_36Ans != null && item.Q_36Ans.Trim() != "") { sm.Q_36Ans = "*"; } else { sm.Q_36Ans = "NULL"; }
                    if (item.Q_37Ans != null && item.Q_37Ans.Trim() != "") { sm.Q_37Ans = "*"; } else { sm.Q_37Ans = "NULL"; }
                    if (item.Q_38Ans != null && item.Q_38Ans.Trim() != "") { sm.Q_38Ans = "*"; } else { sm.Q_38Ans = "NULL"; }
                    if (item.Q_39Ans != null && item.Q_39Ans.Trim() != "") { sm.Q_39Ans = "*"; } else { sm.Q_39Ans = "NULL"; }
                    if (item.Q_40Ans != null && item.Q_40Ans.Trim() != "") { sm.Q_40Ans = "*"; } else { sm.Q_40Ans = "NULL"; }

                    if (item.Q_41Ans != null && item.Q_41Ans.Trim() != "") { sm.Q_41Ans = "*"; } else { sm.Q_41Ans = "NULL"; }
                    if (item.Q_42Ans != null && item.Q_42Ans.Trim() != "") { sm.Q_42Ans = "*"; } else { sm.Q_42Ans = "NULL"; }
                    if (item.Q_43Ans != null && item.Q_43Ans.Trim() != "") { sm.Q_43Ans = "*"; } else { sm.Q_43Ans = "NULL"; }
                    if (item.Q_44Ans != null && item.Q_44Ans.Trim() != "") { sm.Q_44Ans = "*"; } else { sm.Q_44Ans = "NULL"; }
                    if (item.Q_45Ans != null && item.Q_45Ans.Trim() != "") { sm.Q_45Ans = "*"; } else { sm.Q_45Ans = "NULL"; }
                    if (item.Q_46Ans != null && item.Q_46Ans.Trim() != "") { sm.Q_46Ans = "*"; } else { sm.Q_46Ans = "NULL"; }
                    if (item.Q_48Ans != null && item.Q_48Ans.Trim() != "") { sm.Q_48Ans = "*"; } else { sm.Q_48Ans = "NULL"; }
                    if (item.Q_49Ans != null && item.Q_49Ans.Trim() != "") { sm.Q_49Ans = "*"; } else { sm.Q_49Ans = "NULL"; }
                    if (item.Q_50Ans != null && item.Q_50Ans.Trim() != "") { sm.Q_50Ans = "*"; } else { sm.Q_50Ans = "NULL"; }

                    if (item.Q_51Ans != null && item.Q_51Ans.Trim() != "") { sm.Q_51Ans = "*"; } else { sm.Q_51Ans = "NULL"; }
                    if (item.Q_52Ans != null && item.Q_52Ans.Trim() != "") { sm.Q_52Ans = "*"; } else { sm.Q_52Ans = "NULL"; }
                    if (item.Q_53Ans != null && item.Q_53Ans.Trim() != "") { sm.Q_53Ans = "*"; } else { sm.Q_53Ans = "NULL"; }
                    if (item.Q_54Ans != null && item.Q_54Ans.Trim() != "") { sm.Q_54Ans = "*"; } else { sm.Q_54Ans = "NULL"; }
                    if (item.Q_55Ans != null && item.Q_55Ans.Trim() != "") { sm.Q_55Ans = "*"; } else { sm.Q_55Ans = "NULL"; }
                    if (item.Q_56Ans != null && item.Q_56Ans.Trim() != "") { sm.Q_56Ans = "*"; } else { sm.Q_56Ans = "NULL"; }
                    if (item.Q_57Ans != null && item.Q_57Ans.Trim() != "") { sm.Q_57Ans = "*"; } else { sm.Q_57Ans = "NULL"; }
                    if (item.Q_58Ans != null && item.Q_58Ans.Trim() != "") { sm.Q_58Ans = "*"; } else { sm.Q_58Ans = "NULL"; }
                    if (item.Q_59Ans != null && item.Q_59Ans.Trim() != "") { sm.Q_59Ans = "*"; } else { sm.Q_59Ans = "NULL"; }
                    if (item.Q_60Ans != null && item.Q_60Ans.Trim() != "") { sm.Q_60Ans = "*"; } else { sm.Q_60Ans = "NULL"; }

                    if (item.Q_61Ans != null && item.Q_61Ans.Trim() != "") { sm.Q_61Ans = "*"; } else { sm.Q_61Ans = "NULL"; }
                    if (item.Q_62Ans != null && item.Q_62Ans.Trim() != "") { sm.Q_62Ans = "*"; } else { sm.Q_62Ans = "NULL"; }
                    if (item.Q_63Ans != null && item.Q_63Ans.Trim() != "") { sm.Q_63Ans = "*"; } else { sm.Q_63Ans = "NULL"; }
                    if (item.Q_64Ans != null && item.Q_64Ans.Trim() != "") { sm.Q_64Ans = "*"; } else { sm.Q_64Ans = "NULL"; }
                    if (item.Q_65Ans != null && item.Q_65Ans.Trim() != "") { sm.Q_65Ans = "*"; } else { sm.Q_65Ans = "NULL"; }
                    if (item.Q_66Ans != null && item.Q_66Ans.Trim() != "") { sm.Q_66Ans = "*"; } else { sm.Q_66Ans = "NULL"; }
                    if (item.Q_67Ans != null && item.Q_67Ans.Trim() != "") { sm.Q_67Ans = "*"; } else { sm.Q_67Ans = "NULL"; }
                    if (item.Q_68Ans != null && item.Q_68Ans.Trim() != "") { sm.Q_68Ans = "*"; } else { sm.Q_68Ans = "NULL"; }
                    if (item.Q_69Ans != null && item.Q_69Ans.Trim() != "") { sm.Q_69Ans = "*"; } else { sm.Q_69Ans = "NULL"; }
                    if (item.Q_70Ans != null && item.Q_70Ans.Trim() != "") { sm.Q_70Ans = "*"; } else { sm.Q_70Ans = "NULL"; }

                    if (item.Q_71Ans != null && item.Q_71Ans.Trim() != "") { sm.Q_71Ans = "*"; } else { sm.Q_71Ans = "NULL"; }
                    if (item.Q_72Ans != null && item.Q_72Ans.Trim() != "") { sm.Q_72Ans = "*"; } else { sm.Q_72Ans = "NULL"; }
                    if (item.Q_73Ans != null && item.Q_73Ans.Trim() != "") { sm.Q_73Ans = "*"; } else { sm.Q_73Ans = "NULL"; }
                    if (item.Q_74Ans != null && item.Q_74Ans.Trim() != "") { sm.Q_74Ans = "*"; } else { sm.Q_74Ans = "NULL"; }
                    if (item.Q_75Ans != null && item.Q_75Ans.Trim() != "") { sm.Q_75Ans = "*"; } else { sm.Q_75Ans = "NULL"; }
                    if (item.Q_76Ans != null && item.Q_76Ans.Trim() != "") { sm.Q_76Ans = "*"; } else { sm.Q_76Ans = "NULL"; }
                    if (item.Q_77Ans != null && item.Q_77Ans.Trim() != "") { sm.Q_77Ans = "*"; } else { sm.Q_77Ans = "NULL"; }
                    if (item.Q_78Ans != null && item.Q_78Ans.Trim() != "") { sm.Q_78Ans = "*"; } else { sm.Q_78Ans = "NULL"; }
                    if (item.Q_79Ans != null && item.Q_79Ans.Trim() != "") { sm.Q_79Ans = "*"; } else { sm.Q_79Ans = "NULL"; }
                    if (item.Q_80Ans != null && item.Q_80Ans.Trim() != "") { sm.Q_80Ans = "*"; } else { sm.Q_80Ans = "NULL"; }

                    if (item.Q_81Ans != null && item.Q_81Ans.Trim() != "") { sm.Q_81Ans = "*"; } else { sm.Q_81Ans = "NULL"; }
                    if (item.Q_82Ans != null && item.Q_82Ans.Trim() != "") { sm.Q_82Ans = "*"; } else { sm.Q_82Ans = "NULL"; }
                    if (item.Q_83Ans != null && item.Q_83Ans.Trim() != "") { sm.Q_83Ans = "*"; } else { sm.Q_83Ans = "NULL"; }
                    if (item.Q_84Ans != null && item.Q_84Ans.Trim() != "") { sm.Q_84Ans = "*"; } else { sm.Q_84Ans = "NULL"; }
                    if (item.Q_85Ans != null && item.Q_85Ans.Trim() != "") { sm.Q_85Ans = "*"; } else { sm.Q_85Ans = "NULL"; }
                    if (item.Q_86Ans != null && item.Q_86Ans.Trim() != "") { sm.Q_86Ans = "*"; } else { sm.Q_86Ans = "NULL"; }
                    if (item.Q_87Ans != null && item.Q_87Ans.Trim() != "") { sm.Q_87Ans = "*"; } else { sm.Q_87Ans = "NULL"; }
                    if (item.Q_88Ans != null && item.Q_88Ans.Trim() != "") { sm.Q_88Ans = "*"; } else { sm.Q_88Ans = "NULL"; }
                    if (item.Q_89Ans != null && item.Q_89Ans.Trim() != "") { sm.Q_89Ans = "*"; } else { sm.Q_89Ans = "NULL"; }
                    if (item.Q_90Ans != null && item.Q_90Ans.Trim() != "") { sm.Q_90Ans = "*"; } else { sm.Q_90Ans = "NULL"; }

                    if (item.Q_91Ans != null && item.Q_91Ans .Trim() != "") { sm.Q_91Ans  = "*"; } else { sm.Q_91Ans  = "NULL"; }
                    if (item.Q_92Ans != null && item.Q_92Ans .Trim() != "") { sm.Q_92Ans  = "*"; } else { sm.Q_92Ans  = "NULL"; }
                    if (item.Q_93Ans != null && item.Q_93Ans .Trim() != "") { sm.Q_93Ans  = "*"; } else { sm.Q_93Ans  = "NULL"; }
                    if (item.Q_94Ans != null && item.Q_94Ans .Trim() != "") { sm.Q_94Ans  = "*"; } else { sm.Q_94Ans  = "NULL"; }
                    if (item.Q_95Ans != null && item.Q_95Ans .Trim() != "") { sm.Q_95Ans  = "*"; } else { sm.Q_95Ans  = "NULL"; }
                    if (item.Q_96Ans != null && item.Q_96Ans .Trim() != "") { sm.Q_96Ans  = "*"; } else { sm.Q_96Ans  = "NULL"; }
                    if (item.Q_97Ans != null && item.Q_97Ans .Trim() != "") { sm.Q_97Ans  = "*"; } else { sm.Q_97Ans  = "NULL"; }
                    if (item.Q_98Ans != null && item.Q_98Ans .Trim() != "") { sm.Q_98Ans  = "*"; } else { sm.Q_98Ans  = "NULL"; }
                    if (item.Q_99Ans != null && item.Q_99Ans .Trim() != "") { sm.Q_99Ans  = "*"; } else { sm.Q_99Ans  = "NULL"; }
                    if (item.Q_100Ans != null &&item.Q_100Ans.Trim() != "") { sm.Q_100Ans = "*"; } else { sm.Q_100Ans = "NULL"; }

                    ans.Add(sm);
                }
                return View(ans.ToPagedList(page ?? 1, 100));
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        [HttpPost]
        public ActionResult Check_Mark_Status(string dist, string taluka, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (dist != "0" && dist != "" && dist != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + dist + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                if (dist == "0" || dist == "" || dist == null)
                {
                    Query = "select A.* from Tbl_Final_Ans A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                else
                {
                    Query = "select A.* from Tbl_Final_Ans A where" + Temp;
                }

                List<Ans_Model> model = db.Database.SqlQuery<Ans_Model>(Query).ToList();
                List<Ans_Model> ans = new List<Ans_Model>();
                foreach (var item in model)
                {
                    Ans_Model sm = new Ans_Model();
                    sm.Index_No = item.Index_No; sm.Batch = item.Batch; sm.Seat_No = item.Seat_No; sm.Paper_ID = item.Paper_ID;
                    if (sm.Q1_Ans != null && sm.Q1_Ans.Trim() != "") {  item.Q1_Ans = "*"; } else { sm.Q1_Ans = "NULL"; }
                    if (sm.Q_2Ans != null && sm.Q_2Ans .Trim() != "") { item.Q_2Ans = "*"; } else { sm.Q_2Ans = "NULL"; }
                    if (sm.Q_3Ans != null && sm.Q_3Ans .Trim() != "") { item.Q_3Ans = "*"; } else { sm.Q_3Ans = "NULL"; }
                    if (sm.Q_4Ans != null && sm.Q_4Ans .Trim() != "") { item.Q_4Ans = "*"; } else { sm.Q_4Ans = "NULL"; }
                    if (sm.Q_5Ans != null && sm.Q_5Ans .Trim() != "") { item.Q_5Ans = "*"; } else { sm.Q_5Ans = "NULL"; }
                    if (sm.Q_6Ans != null && sm.Q_6Ans .Trim() != "") { item.Q_6Ans = "*"; } else { sm.Q_6Ans = "NULL"; }
                    if (sm.Q_7Ans != null && sm.Q_7Ans .Trim() != "") { item.Q_7Ans = "*"; } else { sm.Q_7Ans = "NULL"; }
                    if (sm.Q_8Ans != null && sm.Q_8Ans .Trim() != "") { item.Q_8Ans = "*"; } else { sm.Q_8Ans = "NULL"; }
                    if (sm.Q_9Ans != null && sm.Q_9Ans .Trim() != "") { item.Q_9Ans = "*"; } else { sm.Q_9Ans = "NULL"; }
                    if (sm.Q_10Ans != null && sm.Q_10Ans.Trim() != "") {item.Q_10Ans = "*"; } else {sm.Q_10Ans = "NULL"; }

                    if (sm.Q_11Ans!= null && sm.Q_11Ans.Trim() != "") { item.Q_11Ans= "*"; } else { sm.Q_11Ans= "NULL"; }
                    if (sm.Q_12Ans!= null && sm.Q_12Ans.Trim() != "") { item.Q_12Ans= "*"; } else { sm.Q_12Ans= "NULL"; }
                    if (sm.Q_13Ans!= null && sm.Q_13Ans.Trim() != "") { item.Q_13Ans= "*"; } else { sm.Q_13Ans= "NULL"; }
                    if (sm.Q_14Ans!= null && sm.Q_14Ans.Trim() != "") { item.Q_14Ans= "*"; } else { sm.Q_14Ans= "NULL"; }
                    if (sm.Q_15Ans!= null && sm.Q_15Ans.Trim() != "") { item.Q_15Ans= "*"; } else { sm.Q_15Ans= "NULL"; }
                    if (sm.Q_16Ans!= null && sm.Q_16Ans.Trim() != "") { item.Q_16Ans= "*"; } else { sm.Q_16Ans= "NULL"; }
                    if (sm.Q_17Ans!= null && sm.Q_17Ans.Trim() != "") { item.Q_17Ans= "*"; } else { sm.Q_17Ans= "NULL"; }
                    if (sm.Q_18Ans!= null && sm.Q_18Ans.Trim() != "") { item.Q_18Ans= "*"; } else { sm.Q_18Ans= "NULL"; }
                    if (sm.Q_19Ans!= null && sm.Q_19Ans.Trim() != "") { item.Q_19Ans= "*"; } else { sm.Q_19Ans= "NULL"; }
                    if (sm.Q_20Ans != null &&sm.Q_20Ans.Trim() != "") { item.Q_20Ans = "*"; } else {sm.Q_20Ans = "NULL"; }

                    if (sm.Q_21Ans!= null && sm.Q_21Ans.Trim() != "") { item.Q_21Ans= "*"; } else { sm.Q_21Ans= "NULL"; }
                    if (sm.Q_22Ans!= null && sm.Q_22Ans.Trim() != "") { item.Q_22Ans= "*"; } else { sm.Q_22Ans= "NULL"; }
                    if (sm.Q_23Ans!= null && sm.Q_23Ans.Trim() != "") { item.Q_23Ans= "*"; } else { sm.Q_23Ans= "NULL"; }
                    if (sm.Q_24Ans!= null && sm.Q_24Ans.Trim() != "") { item.Q_24Ans= "*"; } else { sm.Q_24Ans= "NULL"; }
                    if (sm.Q_25Ans!= null && sm.Q_25Ans.Trim() != "") { item.Q_25Ans= "*"; } else { sm.Q_25Ans= "NULL"; }
                    if (sm.Q_26Ans!= null && sm.Q_26Ans.Trim() != "") { item.Q_26Ans= "*"; } else { sm.Q_26Ans= "NULL"; }
                    if (sm.Q_27Ans!= null && sm.Q_27Ans.Trim() != "") { item.Q_27Ans= "*"; } else { sm.Q_27Ans= "NULL"; }
                    if (sm.Q_28Ans!= null && sm.Q_28Ans.Trim() != "") { item.Q_28Ans= "*"; } else { sm.Q_28Ans= "NULL"; }
                    if (sm.Q_29Ans!= null && sm.Q_29Ans.Trim() != "") { item.Q_29Ans= "*"; } else { sm.Q_29Ans= "NULL"; }
                    if (sm.Q_30Ans != null &&sm.Q_30Ans.Trim() != "") { item.Q_30Ans = "*"; } else {sm.Q_30Ans = "NULL"; }

                    if (sm.Q_31Ans!= null && sm.Q_31Ans.Trim() != "") { item.Q_31Ans= "*"; } else { sm.Q_31Ans= "NULL"; }
                    if (sm.Q_32Ans!= null && sm.Q_32Ans.Trim() != "") { item.Q_32Ans= "*"; } else { sm.Q_32Ans= "NULL"; }
                    if (sm.Q_33Ans!= null && sm.Q_33Ans.Trim() != "") { item.Q_33Ans= "*"; } else { sm.Q_33Ans= "NULL"; }
                    if (sm.Q_34Ans!= null && sm.Q_34Ans.Trim() != "") { item.Q_34Ans= "*"; } else { sm.Q_34Ans= "NULL"; }
                    if (sm.Q_35Ans!= null && sm.Q_35Ans.Trim() != "") { item.Q_35Ans= "*"; } else { sm.Q_35Ans= "NULL"; }
                    if (sm.Q_36Ans!= null && sm.Q_36Ans.Trim() != "") { item.Q_36Ans= "*"; } else { sm.Q_36Ans= "NULL"; }
                    if (sm.Q_37Ans!= null && sm.Q_37Ans.Trim() != "") { item.Q_37Ans= "*"; } else { sm.Q_37Ans= "NULL"; }
                    if (sm.Q_38Ans!= null && sm.Q_38Ans.Trim() != "") { item.Q_38Ans= "*"; } else { sm.Q_38Ans= "NULL"; }
                    if (sm.Q_39Ans!= null && sm.Q_39Ans.Trim() != "") { item.Q_39Ans= "*"; } else { sm.Q_39Ans= "NULL"; }
                    if (sm.Q_40Ans != null &&sm.Q_40Ans.Trim() != "") { item.Q_40Ans = "*"; } else {sm.Q_40Ans = "NULL"; }

                    if (sm.Q_41Ans != null && sm.Q_41Ans.Trim() != "") { item.Q_41Ans = "*"; } else { sm.Q_41Ans = "NULL"; }
                    if (sm.Q_42Ans != null && sm.Q_42Ans.Trim() != "") { item.Q_42Ans = "*"; } else { sm.Q_42Ans = "NULL"; }
                    if (sm.Q_43Ans != null && sm.Q_43Ans.Trim() != "") { item.Q_43Ans = "*"; } else { sm.Q_43Ans = "NULL"; }
                    if (sm.Q_44Ans != null && sm.Q_44Ans.Trim() != "") { item.Q_44Ans = "*"; } else { sm.Q_44Ans = "NULL"; }
                    if (sm.Q_45Ans != null && sm.Q_45Ans.Trim() != "") { item.Q_45Ans = "*"; } else { sm.Q_45Ans = "NULL"; }
                    if (sm.Q_46Ans != null && sm.Q_46Ans.Trim() != "") { item.Q_46Ans = "*"; } else { sm.Q_46Ans = "NULL"; }
                    if (sm.Q_47Ans != null && sm.Q_47Ans.Trim() != "") { item.Q_47Ans = "*"; } else { sm.Q_47Ans = "NULL"; }
                    if (sm.Q_48Ans != null && sm.Q_48Ans.Trim() != "") { item.Q_48Ans = "*"; } else { sm.Q_48Ans = "NULL"; }
                    if (sm.Q_49Ans != null && sm.Q_49Ans.Trim() != "") { item.Q_49Ans = "*"; } else { sm.Q_49Ans = "NULL"; }
                    if (sm.Q_50Ans != null && sm.Q_50Ans.Trim() != "") { item.Q_50Ans = "*"; } else { sm.Q_50Ans = "NULL"; }

                    if (sm.Q_51Ans != null && sm.Q_51Ans.Trim() != "") { item.Q_51Ans = "*"; } else { sm.Q_51Ans = "NULL"; }
                    if (sm.Q_52Ans != null && sm.Q_52Ans.Trim() != "") { item.Q_52Ans = "*"; } else { sm.Q_52Ans = "NULL"; }
                    if (sm.Q_53Ans != null && sm.Q_53Ans.Trim() != "") { item.Q_53Ans = "*"; } else { sm.Q_53Ans = "NULL"; }
                    if (sm.Q_54Ans != null && sm.Q_54Ans.Trim() != "") { item.Q_54Ans = "*"; } else { sm.Q_54Ans = "NULL"; }
                    if (sm.Q_55Ans != null && sm.Q_55Ans.Trim() != "") { item.Q_55Ans = "*"; } else { sm.Q_55Ans = "NULL"; }
                    if (sm.Q_56Ans != null && sm.Q_56Ans.Trim() != "") { item.Q_56Ans = "*"; } else { sm.Q_56Ans = "NULL"; }
                    if (sm.Q_57Ans != null && sm.Q_57Ans.Trim() != "") { item.Q_57Ans = "*"; } else { sm.Q_57Ans = "NULL"; }
                    if (sm.Q_58Ans != null && sm.Q_58Ans.Trim() != "") { item.Q_58Ans = "*"; } else { sm.Q_58Ans = "NULL"; }
                    if (sm.Q_59Ans != null && sm.Q_59Ans.Trim() != "") { item.Q_59Ans = "*"; } else { sm.Q_59Ans = "NULL"; }
                    if (sm.Q_60Ans != null && sm.Q_60Ans.Trim() != "") { item.Q_60Ans = "*"; } else { sm.Q_60Ans = "NULL"; }

                    if (sm.Q_61Ans != null && sm.Q_61Ans.Trim() != "") { item.Q_61Ans = "*"; } else { sm.Q_61Ans = "NULL"; }
                    if (sm.Q_62Ans != null && sm.Q_62Ans.Trim() != "") { item.Q_62Ans = "*"; } else { sm.Q_62Ans = "NULL"; }
                    if (sm.Q_63Ans != null && sm.Q_63Ans.Trim() != "") { item.Q_63Ans = "*"; } else { sm.Q_63Ans = "NULL"; }
                    if (sm.Q_64Ans != null && sm.Q_64Ans.Trim() != "") { item.Q_64Ans = "*"; } else { sm.Q_64Ans = "NULL"; }
                    if (sm.Q_65Ans != null && sm.Q_65Ans.Trim() != "") { item.Q_65Ans = "*"; } else { sm.Q_65Ans = "NULL"; }
                    if (sm.Q_66Ans != null && sm.Q_66Ans.Trim() != "") { item.Q_66Ans = "*"; } else { sm.Q_66Ans = "NULL"; }
                    if (sm.Q_67Ans != null && sm.Q_67Ans.Trim() != "") { item.Q_67Ans = "*"; } else { sm.Q_67Ans = "NULL"; }
                    if (sm.Q_68Ans != null && sm.Q_68Ans.Trim() != "") { item.Q_68Ans = "*"; } else { sm.Q_68Ans = "NULL"; }
                    if (sm.Q_69Ans != null && sm.Q_69Ans.Trim() != "") { item.Q_69Ans = "*"; } else { sm.Q_69Ans = "NULL"; }
                    if (sm.Q_70Ans != null && sm.Q_70Ans.Trim() != "") { item.Q_70Ans = "*"; } else { sm.Q_70Ans = "NULL"; }

                    if (sm.Q_71Ans!= null && sm.Q_71Ans.Trim() != "") { item.Q_71Ans = "*"; } else { sm.Q_71Ans= "NULL"; }
                    if (sm.Q_72Ans!= null && sm.Q_72Ans.Trim() != "") { item.Q_72Ans = "*"; } else { sm.Q_72Ans= "NULL"; }
                    if (sm.Q_73Ans!= null && sm.Q_73Ans.Trim() != "") { item.Q_73Ans = "*"; } else { sm.Q_73Ans= "NULL"; }
                    if (sm.Q_74Ans!= null && sm.Q_74Ans.Trim() != "") { item.Q_74Ans = "*"; } else { sm.Q_74Ans= "NULL"; }
                    if (sm.Q_75Ans!= null && sm.Q_75Ans.Trim() != "") { item.Q_75Ans = "*"; } else { sm.Q_75Ans= "NULL"; }
                    if (sm.Q_76Ans!= null && sm.Q_76Ans.Trim() != "") { item.Q_76Ans = "*"; } else { sm.Q_76Ans= "NULL"; }
                    if (sm.Q_77Ans!= null && sm.Q_77Ans.Trim() != "") { item.Q_77Ans = "*"; } else { sm.Q_77Ans= "NULL"; }
                    if (sm.Q_78Ans!= null && sm.Q_78Ans.Trim() != "") { item.Q_78Ans = "*"; } else { sm.Q_78Ans= "NULL"; }
                    if (sm.Q_79Ans!= null && sm.Q_79Ans.Trim() != "") { item.Q_79Ans = "*"; } else { sm.Q_79Ans= "NULL"; }
                    if (sm.Q_80Ans!= null && sm.Q_80Ans.Trim() != "") { item.Q_80Ans = "*"; } else { sm.Q_80Ans = "NULL"; }

                    if (sm.Q_81Ans != null && sm.Q_81Ans.Trim() != "") { item.Q_81Ans = "*"; } else { sm.Q_81Ans = "NULL"; }
                    if (sm.Q_82Ans != null && sm.Q_82Ans.Trim() != "") { item.Q_82Ans = "*"; } else { sm.Q_82Ans = "NULL"; }
                    if (sm.Q_83Ans != null && sm.Q_83Ans.Trim() != "") { item.Q_83Ans = "*"; } else { sm.Q_83Ans = "NULL"; }
                    if (sm.Q_84Ans != null && sm.Q_84Ans.Trim() != "") { item.Q_84Ans = "*"; } else { sm.Q_84Ans = "NULL"; }
                    if (sm.Q_85Ans != null && sm.Q_85Ans.Trim() != "") { item.Q_85Ans = "*"; } else { sm.Q_85Ans = "NULL"; }
                    if (sm.Q_86Ans != null && sm.Q_86Ans.Trim() != "") { item.Q_86Ans = "*"; } else { sm.Q_86Ans = "NULL"; }
                    if (sm.Q_87Ans != null && sm.Q_87Ans.Trim() != "") { item.Q_87Ans = "*"; } else { sm.Q_87Ans = "NULL"; }
                    if (sm.Q_88Ans != null && sm.Q_88Ans.Trim() != "") { item.Q_88Ans = "*"; } else { sm.Q_88Ans = "NULL"; }
                    if (sm.Q_89Ans != null && sm.Q_89Ans.Trim() != "") { item.Q_89Ans = "*"; } else { sm.Q_89Ans = "NULL"; }
                    if (sm.Q_90Ans != null && sm.Q_90Ans.Trim() != "") { item.Q_90Ans = "*"; } else { sm.Q_90Ans = "NULL"; }

                    if (sm.Q_91Ans  != null && sm.Q_91Ans .Trim() != "") { item.Q_91Ans  = "*"; } else { sm.Q_91Ans  = "NULL"; }
                    if (sm.Q_92Ans  != null && sm.Q_92Ans .Trim() != "") { item.Q_92Ans  = "*"; } else { sm.Q_92Ans  = "NULL"; }
                    if (sm.Q_93Ans  != null && sm.Q_93Ans .Trim() != "") { item.Q_93Ans  = "*"; } else { sm.Q_93Ans  = "NULL"; }
                    if (sm.Q_94Ans  != null && sm.Q_94Ans .Trim() != "") { item.Q_94Ans  = "*"; } else { sm.Q_94Ans  = "NULL"; }
                    if (sm.Q_95Ans  != null && sm.Q_95Ans .Trim() != "") { item.Q_95Ans  = "*"; } else { sm.Q_95Ans  = "NULL"; }
                    if (sm.Q_96Ans  != null && sm.Q_96Ans .Trim() != "") { item.Q_96Ans  = "*"; } else { sm.Q_96Ans  = "NULL"; }
                    if (sm.Q_97Ans  != null && sm.Q_97Ans .Trim() != "") { item.Q_97Ans  = "*"; } else { sm.Q_97Ans  = "NULL"; }
                    if (sm.Q_98Ans  != null && sm.Q_98Ans .Trim() != "") { item.Q_98Ans  = "*"; } else { sm.Q_98Ans  = "NULL"; }
                    if (sm.Q_99Ans  != null && sm.Q_99Ans .Trim() != "") { item.Q_99Ans  = "*"; } else { sm.Q_99Ans  = "NULL"; }
                    if (sm.Q_100Ans != null && sm.Q_100Ans.Trim() != "") { item.Q_100Ans = "*"; } else { sm.Q_100Ans = "NULL"; }
                    ans.Add(sm);                                                                        
                }

                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(ans);
                    dt.TableName = "Division_Mark_Status";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Division_Mark_Status";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        //public ActionResult Divi_Stud_Login()
        //{
        //    Login_Model login_model = common.Get_Login_Details();
        //    Board_Model mm = common.Get_Board_Details(login_model.Index_No);
        //    var model = db.Tbl_Code_Master.Where(x => x.division_code == mm.Division_Code.ToString()).Select(n => new
        //    {
        //        n.district_name,
        //        n.district_code

        //    }).Distinct().OrderBy(n => n.district_name).ToList();

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Text = "--Select District--", Value = "0" });
        //    if (model != null)
        //    {
        //        foreach (var item in model)
        //        {
        //            list.Add(new SelectListItem { Text = item.district_name.ToString(), Value = item.district_code.ToString() });
        //        }
        //    }
        //    ViewBag.DistrictList = new SelectList(list, "Value", "Text");
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult Divi_Stud_Login(string district, string taluka, string Excel, int? page)
        //{
        //    Login_Model login_model = common.Get_Login_Details();
        //    Board_Model mm = common.Get_Board_Details(login_model.Index_No);
        //    try
        //    {
        //        string Temp = "", Query = "", fileName = "";
        //        if (district != "0" && district != "" && district != null)
        //        {
        //            Temp += "  Substring(A.index_No,1,2)='" + district + "'";
        //        }
        //        if (taluka != "0" && taluka != "" && taluka != null)
        //        {
        //            Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
        //        }
        //        if (district == "0" && district == "" && district == null)
        //        {
        //            Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + mm.Division_Code + ");";
        //        }
        //        else
        //        {
        //            Query = "select A.* from Tbl_Login A where" + Temp;
        //        }

        //        List<Tbl_Login> model = db.Database.SqlQuery<Tbl_Login>(Query).ToList();
        //        var abc= model.ToList().ToPagedList(page ?? 1, 100);
        //        //List<Tbl_Login> model = (List<Tbl_Login>)db.Database.SqlQuery<Tbl_Login>(Query).ToList().ToPagedList(page ?? 1, 100);
        //        if (Excel == "1")
        //        {
        //            DataTable dt = common.ToDataTable(model);
        //            dt.TableName = "Div_DTT2";
        //            fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Div_DTT2";
        //            common.CreateExcelFile(dt, fileName);

        //        }
        //        return Json(new { Result = true, Response = abc, FileName = fileName }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
        //    }
        //}



    }
}