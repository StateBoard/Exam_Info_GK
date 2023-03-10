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
    public class DistrictController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        Common common = new Common();
        // GET: District
        public ActionResult District_DashBoard()
        {
            Login_Model login_model = common.Get_Login_Details();
            if (login_model == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }


        public ActionResult District_College_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            TempData["District_Name"] = board_Model.District_Name;
            return View();
        }
        [HttpPost]
        public ActionResult District_College_List( string district, string taluka, string Excel, string type)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (board_Model.District_Code != "0" && board_Model.District_Code != "" && board_Model.District_Code != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + board_Model.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                if (type == "Complete")
                {
                    if (board_Model.District_Code != null && board_Model.District_Code != "0" && board_Model.District_Code != "")
                    {
                        Query = "select A.*, B.* from Tbl_ITCOLLEGELIST A Left join Tbl_College_Registration B on A.Index_No = B.Index_No where " + Temp + "and B.Index_No is not null";
                    }
                }

                if (type == "InComplete")
                {
                    if (board_Model.District_Code != null && board_Model.District_Code != "0" && board_Model.District_Code != "")
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
        public ActionResult District_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["District_Name"] = mm.District_Name;
            return View();
        }

        [HttpPost]
        public JsonResult District_List(string taluka, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (mm.District_Code != "0" && mm.District_Code != "" && mm.District_Code != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + mm.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_Login A where" + Temp;

                List<Tbl_Login> model = db.Database.SqlQuery<Tbl_Login>(Query).ToList(); 
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "District_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_List";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult District_Edit_Profile()
        {
            Tbl_District_Co_Ordinator_Registration dc = new Tbl_District_Co_Ordinator_Registration();
            Login_Model login_model = common.Get_Login_Details();

            int temp = db.Tbl_District_Co_Ordinator_Registration.Where(x => x.Index_No == login_model.Index_No).Count();
            if (temp > 0)
            {
                dc = db.Tbl_District_Co_Ordinator_Registration.Where(x => x.Index_No == login_model.Index_No).FirstOrDefault();
            }

            return View(dc);
        }
        [HttpPost]
        public ActionResult District_Edit_Profile(Tbl_District_Co_Ordinator_Registration Update_District_Co_Info)
        {
            try
            {
                var college = "";
                db.Entry(Update_District_Co_Info).State = EntityState.Modified;
                for (int i = 0; i < Update_District_Co_Info.College_Code1.Count; i++)
                {
                    if (i == 0)
                    {
                        college += Update_District_Co_Info.College_Code1[i];

                    }
                    else
                    {
                        college += "," + Update_District_Co_Info.College_Code1[i];
                    }
                    Update_District_Co_Info.College_Code = college;
                    db.SaveChanges();
                }

                if (Update_District_Co_Info.ID != 0)
                {
                    var div = db.Tbl_Code_Master.Where(a => a.district_code == Update_District_Co_Info.District_Code).Select(n => n.division_code).FirstOrDefault();
                    Update_District_Co_Info.Division_Code = div;
                    db.Entry(Update_District_Co_Info).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Record Edited Successfully....!";
                }
            }
            catch (Exception e)
            {

            }
            return View();
        }

        public JsonResult Get_Taluka(string distCode)
        {
            var model = db.Tbl_Code_Master.Where(x => x.district_code == distCode).Select(n => new
            {
                n.taluka_name,
                n.taluka_code
            }).Distinct().OrderBy(n => n.taluka_name).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Select Taluka-", Value = "0" });
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.taluka_name.ToString(), Value = item.taluka_code.ToString() });
                }
            }
            return Json(new SelectList(list, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public ActionResult Show_College(string district, string taluka, Tbl_District_Co_Ordinator_Registration College_Code)
        {
            var model = College_Code.College_List = db.Tbl_ITCOLLEGELIST.Where(x => x.Index_No.Substring(0, 2) == district && x.Index_No.Substring(2, 2) == taluka).ToList<Tbl_ITCOLLEGELIST>();

            return Json(new { Result = true, Response = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult District_Inspection()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["District_Name"] = mm.District_Name;
            return View();
        }
        [HttpPost]
        public JsonResult Load_District(string distCode)
        {
            var model = db.Tbl_Code_Master.Where(x => x.district_name == distCode).Select(n => new
            {
                n.taluka_name,
                n.taluka_code
            }).Distinct().OrderBy(n => n.taluka_name).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select Taluka--", Value = "0" });
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.taluka_name.ToString(), Value = item.taluka_code.ToString() });
                }
            }
            return Json(new SelectList(list, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        [HttpPost]
        public JsonResult District_Inspection(string taluka, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (mm.District_Code != "0" && mm.District_Code != "" && mm.District_Code != null)
                {
                    Temp += " and Substring(A.index_No,1,2)='" + mm.District_Code + "'";
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
                    dt.TableName = "District_Inspection";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Inspection";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult District_Data_Test1()
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            TempData["District_Name"] = board_Model.District_Name;
            return View();
        }
        [HttpPost]
        public JsonResult District_Data_Test1(string taluka, string Excel)
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (board_Model.District_Code != "0" && board_Model.District_Code != "" && board_Model.District_Code != null)
                {
                    Temp += " and Substring(A.index_No,1,2)='" + board_Model.District_Code + "'";
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
                    dt.TableName = "District_DTT1";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_DTT1";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult District_Data_Test2()
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            TempData["District_Name"] = board_Model.District_Name;
            return View();
        }
        [HttpPost]
        public JsonResult District_Data_Test2(string taluka, string Excel)
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (board_Model.District_Code != "0" && board_Model.District_Code != "" && board_Model.District_Code != null)
                {
                    Temp += " and Substring(A.index_No,1,2)='" + board_Model.District_Code + "'";
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
                    dt.TableName = "District_DTT2";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_DTT2";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult District_Data_Test3()
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            TempData["District_Name"] = board_Model.District_Name;
            return View();
        }
        [HttpPost]
        public JsonResult District_Data_Test3(string taluka, string Excel)
        {
            Login_Model login_Model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_Model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (board_Model.District_Code != "0" && board_Model.District_Code != "" && board_Model.District_Code != null)
                {
                    Temp += " Substring(A.index_No,1,2)='" + board_Model.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_DTT3 A where " + Temp;

                List<Tbl_DTT3> model = db.Database.SqlQuery<Tbl_DTT3>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "District_DTT3";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_DTT3";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult District_Batch_List()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["District_Name"] = mm.District_Name;
            return View();
        }
        [HttpPost]
        public ActionResult District_Batch_List(string taluka, string batch, string Excel)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string Temp = "", Query = "", fileName = "";
                if (mm.District_Code != "0" && mm.District_Code != "" && mm.District_Code != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + mm.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_Login A where" + Temp + "and Batch = '" + batch + "'";

                List<Student_Model> model = db.Database.SqlQuery<Student_Model>(Query).ToList();
                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "District_Batch_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Batch_List";
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
        public ActionResult District_List(string taluka, int? page)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            TempData["District_Name"] = mm.District_Name;
            try
            {
                string Temp = "", Query = "";
                if (mm.District_Code != "0" && mm.District_Code != "" && mm.District_Code != null)
                {
                    Temp += " Substring(A.Index_No,1,2)='" + mm.District_Code + "'";
                }
                if (taluka != "0" && taluka != "" && taluka != null)
                {
                    Temp += " and Substring(A.Index_No,3,2)='" + taluka + "'";
                }
                Query = "select A.* from Tbl_Login A where" + Temp;

                var model = db.Database.SqlQuery<Tbl_Login>(Query).Count();
                return View(db.Database.SqlQuery<Student_Model>(Query).ToList().ToPagedList(page ?? 1, 100));
            }
            catch (Exception e)
            {

            }
            return View();
        }
    }
}