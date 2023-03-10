using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Exam_Info.Helper;
using Exam_Info.Models;
using OfficeOpenXml;
using PagedList;

namespace Exam_Info.Controllers
{
    public class StateController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        Common common = new Common();
        // GET: State
        public ActionResult State_Dashboard()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            if (login_model == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult State_Batch_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            return View();
        }
        [HttpPost]
        public ActionResult State_Batch_List(string division_name, string district_name, string taluka, string batch, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (district_name != "0" && district_name != "" && district_name != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + mm.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                if (division_name != null && division_name != "" && division_name != "0")
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division_name + ") and Batch = '" + batch + "'";
                }
                if (mm.Division_Code != 0 && division_name == "0" || division_name == "" || division_name == null)
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + mm.Division_Code + ") and Batch = '" + batch + "'";
                }
                if (district_name != "0" && district_name != "" && district_name != null)
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
        public ActionResult State_College_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            List<IT_College_List_Model> tbl_College_Registrations = db.Database.SqlQuery<IT_College_List_Model>("select Index_No  from Tbl_ITCOLLEGELIST A where A.Index_No not in (select Index_No from Tbl_College_Registration)").ToList();
            return View(tbl_College_Registrations);
        }
        [HttpPost]
        public ActionResult State_College_List(string division, string district, string taluka, string Excel, string type)
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
                    if (division != null && division != "" && division != "0")
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division + ") and B.Index_No is not null";
                    }
                    if (board_Model.Division_Code != 0 && division == null || division == "0" || division == "")
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
                    if (division != null && division != "" && division != "0")
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division + ") and B.Index_No is null";
                    }
                    if (board_Model.Division_Code != 0 && division == null || division == "0" || division == "")
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
        public ActionResult State_Edit_profile()
        {
                Tbl_State_Co_Ordinator_Registration state_Dc = new Tbl_State_Co_Ordinator_Registration();

                Login_Model login_model = common.Get_Login_Details();

                int temp = db.Tbl_State_Co_Ordinator_Registration.Where(x => x.Index_No == login_model.Index_No).Count();
                if (temp > 0)
                {
                    state_Dc = db.Tbl_State_Co_Ordinator_Registration.Where(x => x.Index_No == login_model.Index_No).FirstOrDefault();
                }
                return View(state_Dc);
            
           
        }

        [HttpPost]
        public ActionResult State_Edit_profile(Tbl_State_Co_Ordinator_Registration Update_State_Info)
        {
            try
            {
                db.Tbl_State_Co_Ordinator_Registration.Add(Update_State_Info);
                if (Update_State_Info.ID != 0)
                {
                    db.Entry(Update_State_Info).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Result = true, Response = "Record Updated Sucessfully...!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {

            }
            return View();
        }

       

        [HttpPost]
        public JsonResult Select_District(string div)
        {
            try
            {


                var model = db.Tbl_Code_Master.Where(x => x.division_code == div).Select(n => new
                {
                    n.district_name,
                    n.district_code

                }).Distinct().OrderBy(n => n.district_name).ToList();

                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "-Select District-", Value = "0" });
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        list.Add(new SelectListItem { Text = item.district_name.ToString(), Value = item.district_code.ToString() });
                    }
                }

                return Json(new SelectList(list, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception Ex)
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult State_Inspection()
        {
            return View();
        }
        [HttpPost]
        public ActionResult State_Inspection(string district, string taluka,string Excel)
        {
            try
            {
                string Temp = "", Query = "",  fileName="";
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
                    DataTable dt =common.ToDataTable(model);
                    dt.TableName = "Inspection";
                     fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "Inspection";
                    common.CreateExcelFile(dt, fileName);
                    
                }
                return Json(new { Result = true, Response = model,FileName= fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" +ex}, JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpGet]
        public ActionResult State_List(string division_name, string district_name, string taluka_name, int? page)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "";
                if (district_name != "0" && district_name != "" && district_name != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + district_name + "'";
                }
                if (taluka_name != "0" && taluka_name != "" && taluka_name != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka_name + "'";
                }
                if (division_name != null && division_name != "" && division_name != "0")
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division_name + ");";
                }
                if (board_Model.Division_Code != 0 && division_name == null || division_name == "0" || division_name == "")
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                if (district_name != "0" && district_name != "" && district_name != null)
                {
                    Query = "select A.* from Tbl_Login A where" + Temp;
                }
                var model = db.Database.SqlQuery<Tbl_Login>(Query).ToList().OrderBy(x => x.Index_No);
                List<Student_Model> list = new List<Student_Model>();
                foreach (var item in model)
                {
                    Student_Model sm = new Student_Model();
                    sm.Index_No = item.Index_No; sm.Name = item.Name; sm.Seat_No = item.Seat_No;
                    list.Add(sm);
                }
                return View(list.ToList().ToPagedList(page ?? 1, 100));
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" + ex }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult State_List(string division_name, string dist, string taluka, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
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
                if (division_name != null && division_name != "" && division_name != "0")
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division_name + ");";
                }
                if (board_Model.Division_Code != 0 && division_name == null || division_name == "0" || division_name == "")
                {
                    Query = "select A.* from Tbl_Login A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                if (dist != "0" && dist != "" && dist != null)
                {
                    Query = "select A.* from Tbl_Login A where" + Temp;
                }

                List<Tbl_Login> model = db.Database.SqlQuery<Tbl_Login>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "State_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "State_List";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" + ex }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Download_Excel()
        {
            return View();
        }

        //[HttpPost]
 

    


        public ActionResult State_Data_Test1()
        {
            return View();
        }
        [HttpPost]
        public JsonResult State_Data_Test1(string district, string taluka, string Excel)
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

        public ActionResult State_Data_Test2()
        {
            return View();
        }
        [HttpPost]
        public JsonResult State_Data_Test2(string district, string taluka, string Excel)
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
                    dt.TableName = "State_DTT2";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "State_DTT2";
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
        public ActionResult State_Data_Test3(string division_name, string district_name, string taluka_name, int? page)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "";
                if (district_name != "0" && district_name != "" && district_name != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + district_name + "'";
                }
                if (taluka_name != "0" && taluka_name != "" && taluka_name != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka_name + "'";
                }

                if (division_name != null && division_name != "" && division_name != "0")
                {
                    Query = "select A.* from Tbl_DTT3 A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division_name + ");";
                }
                if (board_Model.Division_Code != 0 && division_name == null || division_name == "0" || division_name == "")
                {
                    Query = "select A.* from Tbl_DTT3 A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                if (district_name != "0" && district_name != "" && district_name != null)
                {
                    Query = "select A.* from Tbl_DTT3 A where" + Temp;
                }

                var model = db.Database.SqlQuery<Tbl_DTT3>(Query).Count();

                return View(db.Database.SqlQuery<Tbl_DTT3>(Query).ToList().OrderBy(x => x.Index_No).ToPagedList(page ?? 1, 100));
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult State_Data_Test3(string division_name, string district, string taluka, string Excel)
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
                if (division_name != null && division_name != "" && division_name != "0")
                {
                    Query = "select A.* from Tbl_DTT3 A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + division_name + ");";
                }
                if (board_Model.Division_Code != 0 && division_name == null || division_name == "0" || division_name == "")
                {
                    Query = "select A.* from Tbl_DTT3 A where Substring(A.Index_No,1,2) IN (select district_code from Tbl_Code_Master where division_code = " + board_Model.Division_Code + ");";
                }
                if (district != "0" && district != "" && district != null)
                {
                    Query = "select A.* from Tbl_DTT3 A where" + Temp;
                }


                List<Tbl_DTT3> model = (List<Tbl_DTT3>)db.Database.SqlQuery<Tbl_DTT3>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "State_DTT3";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "State_DTT3";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}