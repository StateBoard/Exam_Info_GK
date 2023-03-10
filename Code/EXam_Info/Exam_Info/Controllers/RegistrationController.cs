using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Info.Helper;
using Exam_Info.Models;

namespace Exam_Info.Controllers
{
    public class RegistrationController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        Common common = new Common();
        // GET: Registration
        public ActionResult College_Registration()
        {

            return View();
        }
        [HttpPost]
        public JsonResult Save_College_Registration(Tbl_College_Registration model)
        {
           
            try
            {
                if (model.Index_No == null) { return Json(new { Result = false, Response = "Please Enter Index No." }, JsonRequestBehavior.AllowGet); }
                if (model.College_Name == null) { return Json(new { Result = false, Response = "Please Enter College Name." }, JsonRequestBehavior.AllowGet); }
                if (model.College_Address == null) { return Json(new { Result = false, Response = "Please Enter College Address." }, JsonRequestBehavior.AllowGet); }
                if (model.Principal_Name == null) { return Json(new { Result = false, Response = "Please Enter Principal Name." }, JsonRequestBehavior.AllowGet); }
                if (model.Principal_Mobile == null) { return Json(new { Result = false, Response = "Please Enter Principal_Mobile." }, JsonRequestBehavior.AllowGet); }
                if (model.Total_Students == null) { return Json(new { Result = false, Response = "Please Enter Total Students." }, JsonRequestBehavior.AllowGet); }
                if (model.Total_Machines == null) { return Json(new { Result = false, Response = "Please Enter Total_Machines." }, JsonRequestBehavior.AllowGet); }
                if (model.Total_Teachers == null) { return Json(new { Result = false, Response = "Please Enter Total_Teachers." }, JsonRequestBehavior.AllowGet); }
                if (model.IT_Teacher_Name == null) { return Json(new { Result = false, Response = "Please Enter IT_Teacher_Name." }, JsonRequestBehavior.AllowGet); }
                if (model.IT_Teachers_MobileNumber1 == null) { return Json(new { Result = false, Response = "Please Enter IT_Teachers_MobileNumber1." }, JsonRequestBehavior.AllowGet); }
                if (model.IT_Teachers_Mobilenumber2 == null) { return Json(new { Result = false, Response = "Please Enter IT_Teachers_Mobilenumber2." }, JsonRequestBehavior.AllowGet); }
                if (model.Password == null) { return Json(new { Result = false, Response = "Please Enter Password." }, JsonRequestBehavior.AllowGet); }
                if (model.Confirm_Password == null) { return Json(new { Result = false, Response = "Please Enter Confirm_Password." }, JsonRequestBehavior.AllowGet); }
             

                db.Tbl_College_Registration.Add(model);
                db.SaveChanges();
                return Json(new { Result = true, Response = "Record Suceessfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = false, Response = "Failed"+ex }, JsonRequestBehavior.AllowGet);
            }
        }


        //----------------------------------------------------------------------------------------------------------------
        [HttpGet]
          public ActionResult District_Co_Ordinator_Registration()
            {
                return View();
            }

        [HttpPost]

        public JsonResult Save_District_Co_Ordinator__Registration(Tbl_District_Co_Ordinator_Registration model)
        {
            Board_Model board_Model = common.Get_Board_Details(model.Index_No);

            try
            {
                if (ModelState.IsValid)
                {

                    if (model.Index_No == null) { return Json(new { Result = false, Response = "Please Enter Index No." }, JsonRequestBehavior.AllowGet); }
                    if (model.College_Name == null) { return Json(new { Result = false, Response = "Please Enter College Name." }, JsonRequestBehavior.AllowGet); }
                    if (model.College_Address == null) { return Json(new { Result = false, Response = "Please Enter College Address." }, JsonRequestBehavior.AllowGet); }
                    if (model.Coordinator_Name == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Name." }, JsonRequestBehavior.AllowGet); }
                    if (model.Coordinator_Mobile == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Mobile." }, JsonRequestBehavior.AllowGet); }
                    if (model.Coordinator_Email == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Email." }, JsonRequestBehavior.AllowGet); }
                    if (model.Coordinator_Eduction == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Eduction." }, JsonRequestBehavior.AllowGet); }
                    if (model.District_Code == null) { return Json(new { Result = false, Response = "Please Enter District Code." }, JsonRequestBehavior.AllowGet); }
                    if (model.Taluka_Code == null) { return Json(new { Result = false, Response = "Please Enter Taluka Code." }, JsonRequestBehavior.AllowGet); }
                    if (model.Password == null) { return Json(new { Result = false, Response = "Please Enter Password." }, JsonRequestBehavior.AllowGet); }
                    if (model.Confirm_Password == null) { return Json(new { Result = false, Response = "Please Enter Confirm_Password." }, JsonRequestBehavior.AllowGet); }

                    int cnt = db.Tbl_District_Co_Ordinator_Registration.Where(s => s.Index_No == model.Index_No).Count();
                    if (cnt != 0)
                    {
                        return Json(new { Result = false, Response = "Record Already Exists!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Active = 0;
                        model.Division_Code = board_Model.Division_Code.ToString();
                        db.Tbl_District_Co_Ordinator_Registration.Add(model);
                        db.SaveChanges();

                    }
                }
                return Json(new { Result = true, Response = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }
        //-----------------------------------------------------------------------------------------------


        [HttpGet]
        public ActionResult Division_Co_Ordinator_Registration()
        {
            return View();

        }

        [HttpPost]

        public JsonResult Save_Division_Co_Ordinator_Registration(Tbl_Division_Co_Ordinator_Registration model)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Index_No == null) { return Json(new { Result = false, Response = "Please Enter Index No." }, JsonRequestBehavior.AllowGet); }
                if (model.College_Name == null) { return Json(new { Result = false, Response = "Please Enter College Name." }, JsonRequestBehavior.AllowGet); }
                if (model.College_Address == null) { return Json(new { Result = false, Response = "Please Enter College Address." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Name == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Name." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Mobile == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Mobile." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Email == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Email." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Eduction == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Eduction." }, JsonRequestBehavior.AllowGet); }
                if (model.Password == null) { return Json(new { Result = false, Response = "Please Enter Password." }, JsonRequestBehavior.AllowGet); }
                if (model.Confirm_Password == null) { return Json(new { Result = false, Response = "Please Enter Confirm_Password." }, JsonRequestBehavior.AllowGet); }

                    int cnt = db.Tbl_Division_Co_Ordinator_Registration.Where(s => s.Index_No == model.Index_No).Count();
                    if (cnt != 0)
                    {
                        return Json(new { Result = false, Response = "Record Already Exists!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Active = 0;
                        db.Tbl_Division_Co_Ordinator_Registration.Add(model);
                        db.SaveChanges();

                    }
                }
                return Json(new { Result = true, Response = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }

        //--------------------------------------------------------------------------------------------------


        [HttpGet]
        public ActionResult State_Co_Ordinator_Registration()
        {
            return View();
        }

        [HttpPost]

        public JsonResult Save_State_Co_Ordinator_Registration(Tbl_State_Co_Ordinator_Registration model)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Index_No == null) { return Json(new { Result = false, Response = "Please Enter Index No." }, JsonRequestBehavior.AllowGet); }
                if (model.College_Name == null) { return Json(new { Result = false, Response = "Please Enter College Name." }, JsonRequestBehavior.AllowGet); }
                if (model.College_Address == null) { return Json(new { Result = false, Response = "Please Enter College Address." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Name == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Name." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Mobile == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Mobile." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Email == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Email." }, JsonRequestBehavior.AllowGet); }
                if (model.Coordinator_Eduction == null) { return Json(new { Result = false, Response = "Please Enter Coordinator_Eduction." }, JsonRequestBehavior.AllowGet); }
                if (model.Password == null) { return Json(new { Result = false, Response = "Please Enter Password." }, JsonRequestBehavior.AllowGet); }
                if (model.Confirm_Password == null) { return Json(new { Result = false, Response = "Please Enter Confirm_Password." }, JsonRequestBehavior.AllowGet); }

                    int cnt = db.Tbl_State_Co_Ordinator_Registration.Where(s => s.Index_No == model.Index_No).Count();
                    if (cnt != 0)
                    {
                        return Json(new { Result = false, Response = "Record Already Exists!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Active = 0;
                        db.Tbl_State_Co_Ordinator_Registration.Add(model);
                        db.SaveChanges();

                    }
                }
                return Json(new { Result = true, Response = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }
        //--------------------------------------------------------------------------------------------

    }
}