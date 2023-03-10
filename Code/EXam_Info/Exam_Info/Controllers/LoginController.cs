using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Exam_Info.Models;
using Newtonsoft.Json;

namespace Exam_Info.Controllers
{
    public class LoginController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginDashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login_Model login_Model)
        {
            try
            {
                Login_Model login_model = new Login_Model();


                if (login_Model.Login_Type == "Admin")
                {
                    var Admin_Model = db.Tbl_Admin_Login.Where(model => model.Index_No == login_Model.Index_No && model.Password == login_Model.Password).FirstOrDefault();
                    if (Admin_Model != null)
                    {
                        login_model.Index_No = login_Model.Index_No;
                        login_model.Login_Type = "Admin";
                        string json = JsonConvert.SerializeObject(login_model);
                        FormsAuthentication.SetAuthCookie(json, false);

                        return RedirectToAction("AdminPage", "Admin");

                    }
                    else
                    {
                        TempData["Msg"] = "Invalid Index_No or Password";
                    }
                }

                if (login_Model.Login_Type == "College")
                {
                    var College_Model = db.Tbl_College_Registration.Where(model => model.Index_No == login_Model.Index_No && model.Password == login_Model.Password).FirstOrDefault();
                    if (College_Model != null)
                    {
                        login_model.Index_No = login_Model.Index_No;
                        login_model.Login_Type = "College";
                        string json = JsonConvert.SerializeObject(login_model);
                        FormsAuthentication.SetAuthCookie(json, false);

                        return RedirectToAction("College_DashBoard", "College");

                    }
                    else
                    {
                        TempData["Msg"] = "Invalid Index_No and Password";
                    }
                }

                if (login_Model.Login_Type == "District")
                {
                    if (db.Tbl_District_Co_Ordinator_Registration.Any(model => model.Index_No == login_Model.Index_No && model.Active == 0))
                    {
                        TempData["Msg"] = "Please Contact State board For Login Credentials";
                        return View();
                    }
                    else
                    {
                        var District_Model = db.Tbl_District_Co_Ordinator_Registration.Where(model => model.Index_No == login_Model.Index_No && model.Password == login_Model.Password).FirstOrDefault();
                        if (District_Model != null)
                        {
                            login_model.Index_No = login_Model.Index_No;
                            login_model.Login_Type = "District";
                            string json = JsonConvert.SerializeObject(login_model);
                            FormsAuthentication.SetAuthCookie(json, false);
                            //Session["Index_No"] = District_Model.Index_No;
                            //Session["District"] = District_Model.District_Code;
                            return RedirectToAction("District_DashBoard", "District");

                        }
                        else
                        {
                            TempData["Msg"] = "Invalid Index No & Password!";
                        }
                    }
                }
                if (login_Model.Login_Type == "Division")
                {
                    if (db.Tbl_Division_Co_Ordinator_Registration.Any(model => model.Index_No == login_Model.Index_No && model.Active == 0))
                    {
                        TempData["Msg"] = "Please Contact  State Board  For Login Credentials";
                        return View();
                    }
                    else
                    {
                        var Division_Model = db.Tbl_Division_Co_Ordinator_Registration.Where(model => model.Index_No == login_Model.Index_No && model.Password == login_Model.Password).FirstOrDefault();
                        if (Division_Model != null)
                        {
                            login_model.Index_No = login_Model.Index_No;
                            login_model.Login_Type = "Division";
                            string json = JsonConvert.SerializeObject(login_model);
                            FormsAuthentication.SetAuthCookie(json, false);
                            //Session["Index_No"] = Division_Model.Index_No;
                            //var div = db.Tbl_Code_Master.Where(a => a.district_code == Division_Model.Index_No.Substring(0,2)).Select(n => n.division_code).FirstOrDefault();
                            //Session["Division_Code"] = div;
                            return RedirectToAction("Division_Dashboard", "Division");

                        }
                        else
                        {
                            TempData["Msg"] = "Invalid Index_No and Password";
                        }
                    }
                }


                if (login_Model.Login_Type == "State")
                {
                    if (db.Tbl_State_Co_Ordinator_Registration.Any(model => model.Index_No == login_Model.Index_No && model.Active == 0))
                    {
                        TempData["Msg"] = "Please Contact  State board  For Login Credentials";
                        return View();
                    }
                    else
                    {
                        var State_Model = db.Tbl_State_Co_Ordinator_Registration.Where(model => model.Index_No == login_Model.Index_No && model.Password == login_Model.Password).FirstOrDefault();
                        if (State_Model != null)
                        {

                            login_model.Index_No = login_Model.Index_No;
                            login_model.Login_Type = "State";
                            string json = JsonConvert.SerializeObject(login_model);
                            FormsAuthentication.SetAuthCookie(json, false);
                            return RedirectToAction("State_Dashboard", "State");

                        }
                        else
                        {
                            TempData["Msg"] = "Invalid Index_No and Password";
                        }

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Msg"] = "Login Failed" + e;

            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Reset_Password()
        {
            return View();
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}