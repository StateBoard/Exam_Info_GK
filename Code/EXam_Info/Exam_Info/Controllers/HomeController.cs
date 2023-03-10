using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Exam_Info.Helper;
using Exam_Info.Models;
using Newtonsoft.Json;

namespace Exam_Info.Controllers
{
    public class HomeController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        Common common = new Common();
        char[] sep = new char[] { 'ß' };
        char[] back = new char[] { ' ' };
        string[] result2 = new string[20];
        string result;

        public ActionResult Index()
        {
            var model = db.Tbl_Login.Where(a => a.Index_No == "1205001").FirstOrDefault();
  
            if (model != null)
            {
                string json = JsonConvert.SerializeObject(model);
                FormsAuthentication.SetAuthCookie(json, false);
                //return Json(new { Result = true, Resopnce = "Login Successful", Redirect = "../Division/InchargeRegistrationReport" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Model2 m)
        {
            string path = Server.MapPath("~/AppFiles/Upload/");
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                //System.Threading.Thread.Sleep(1000);
                HttpPostedFileBase file = files[i];
                string[] name = file.FileName.Split('/');
                file.SaveAs(path + name[1]);
                ViewBag.Progress = "" + i + "/" + files.Count + " files uploaded.";
                TempData["Prog"] = "" + i + "/" + files.Count + " files uploaded.";
            }
            return Json(files.Count + " Files Uploaded!");
        }

        //public ActionResult UploadFile(Model2 m)
        //{
        //    try
        //    {
        //        string path = Server.MapPath("~/AppFiles/Upload/");
        //        HttpFileCollectionBase files = Request.Files;
        //        for (int i = 0; i < files.Count; i++)
        //        {
        //            //System.Threading.Thread.Sleep(1000);
        //            HttpPostedFileBase file = files[i];
        //            string[] name = file.FileName.Split('/');
        //            file.SaveAs(path + name[1]);
        //            ConvertTotxtFile(Server.MapPath("~/AppFiles/Upload/" + name[1] + ""));
        //        }
        //        return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult UploadNewFile()
        //{
        //    try
        //    {
        //        string path = Server.MapPath("~/AppFiles/Upload/");
        //        HttpFileCollectionBase files = Request.Files;
        //        for (int i = 0; i < files.Count; i++)
        //        {
        //            //System.Threading.Thread.Sleep(1000);
        //            HttpPostedFileBase file = files[i];
        //            string[] name = file.FileName.Split('/');
        //            file.SaveAs(path + name[0]);
        //            ConvertTotxtFile(Server.MapPath("~/AppFiles/Upload/" + name[0] + ""));
        //        }
        //        return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public void ConvertTotxtFile(string path)
        //{
        //    string extension = path.Substring(path.LastIndexOf("."));
        //    string[] fn = path.Split('\\');
        //    string fname = fn[fn.Length - 1];

        //    // if (Path.GetFileName(sFile).StartsWith(letter))
        //    // {

        //    string fn1 = fname.Substring(0, fname.LastIndexOf("."));
        //    if (extension == ".ans")
        //    {

        //        string destinationPath = Server.MapPath("~/AppFiles/Ans_File_txt/" + fn1 + ".txt").ToString();
        //        using (StreamWriter sw = System.IO.File.CreateText(destinationPath))
        //        {
        //            sw.Close();
        //        }

        //        TextWriter tw = new StreamWriter(@"" + destinationPath + "");
        //        // write a line of text to the file
        //        TextReader re = new StreamReader(path);
        //        string line = null;
        //        enc.Class1 ex = new enc.Class1();
        //        if ((line = re.ReadLine()) != null)
        //        {
        //            string d;
        //            // string[] filelines = System.IO.File.ReadAllLines(line);                       
        //            //string d = ex.xencryption(line);
        //            d = line;
        //            tw.WriteLine(d);
        //            while ((line = re.ReadLine()) != null)
        //            {
        //                if (line == "")
        //                {
        //                    continue;
        //                }

        //                //cnt1++;
        //                if (true)
        //                {
        //                    string a = ex.xencryption(line);
        //                    d = ex.xencryption(line);
        //                    if (d == null)
        //                    {
        //                        d = " ";
        //                    }
        //                    else
        //                    {
        //                        result2 = d.Split(sep, StringSplitOptions.None);

        //                        result = d.Replace("ß", " ");
        //                        //i++; //result1 = d.Remove('');
        //                        //  r1 = result2[0];
        //                        //  r2 = result2[1];
        //                        //  //r2 = r2.ToLower();
        //                        //result = r1 + " " + r2;
        //                    }
        //                    tw.WriteLine(result);
        //                }
        //            }
        //        }
        //        tw.Close();
        //        InsertIntoDB(destinationPath);
        //    }
        //    else
        //    {

        //    }
        //}

        //public JsonResult CheckAndVerify()
        //{
        //    try
        //    {
        //        Tbl_Login emp = common.Get_College_Login_Details();
        //        List<FileStatusModel> filestatusmodel = new List<FileStatusModel>();
        //        filestatusmodel = db.Database.SqlQuery<FileStatusModel>(@"select distinct A.Index_No, A.Seat_No, B.Seat_No Uploaded_Seat_File, 
        //                                                                    Case When B.Seat_No is null then 'Not_Detected'
        //                                                                         when B.Seat_No = A.Seat_No then 'Detected'
        //                                                                    	 end as File_Status
        //                                                                    from Tbl_Login A left join Tbl_Decoded_Answer_Sheets_Sada B on A.Seat_No = B.Seat_No where A.Index_No = '" + emp.Index_No + "' order by A.Seat_NO").ToList();
        //        return Json(new { Result = true, Data = filestatusmodel }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = false, Response = "Something went wrong !" }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //private void InsertIntoDB(string filepath)
        //{
        //    //string[] filePaths = Directory.GetFiles(@textBox1.Text, "*txt");
        //    string line;
        //    char starting = '¶';
        //    char ending = '£';
        //    bool headerfound = false;
        //    bool headercomplete = false;
        //    int filecnt = 0;
        //    if (!Directory.Exists(Server.MapPath("~/AppFiles/Done_Ans_File_txt")))
        //    {
        //        Directory.CreateDirectory(Server.MapPath("~/AppFiles/Done_Ans_File_txt"));
        //    }
        //    try
        //    {
        //        Tbl_Decoded_Answer_Sheets_Sada tbl_Decoded_Answer_Sheets = new Tbl_Decoded_Answer_Sheets_Sada();
        //        string headerphrase = "";
        //        string ansphrase = "";
        //        StreamReader file = new StreamReader(filepath);
        //        while ((line = file.ReadLine()) != null)
        //        {
        //            char[] ch = new char[line.Length];
        //            // Copy character by character into array 
        //            for (int j = 0; j < line.Length; j++)
        //            {
        //                ch[j] = line[j];
        //            }
        //            for (int j = 0; j < line.Length; j++)
        //            {
        //                if (ch[j] == starting)
        //                {
        //                    headerfound = true;
        //                }
        //                if (headerfound && ch[j] != ending && headercomplete == false)
        //                {
        //                    if (ch[j] == '¶')
        //                    {
        //                        continue;
        //                    }
        //                    headerphrase += ch[j];
        //                }
        //                if (ch[j] == ending)
        //                {
        //                    headercomplete = true;
        //                }
        //                if (headercomplete)
        //                {
        //                    if (ch[j] == '£')
        //                    {
        //                        continue;
        //                    }
        //                    ansphrase += ch[j];
        //                }
        //            }
        //            if (ansphrase.Trim() == "")
        //            {
        //                headerfound = false;
        //                headercomplete = false;
        //                headerphrase = "";
        //                ansphrase = "";
        //                continue;
        //            }
        //            FeedData(headerphrase, ansphrase, tbl_Decoded_Answer_Sheets);
        //            headerfound = false;
        //            headercomplete = false;
        //            headerphrase = "";
        //            ansphrase = "";
        //        }

        //        db.Tbl_Decoded_Answer_Sheets_Sada.Add(tbl_Decoded_Answer_Sheets);
        //        db.SaveChanges();

        //        string ss = Server.MapPath("~/AppFiles/Done_Ans_File_txt/") + Path.GetFileName(filepath);

        //        if (!System.IO.File.Exists(Server.MapPath("~/AppFiles/Done_Ans_File_txt/") + Path.GetFileName(filepath)))
        //        {
        //            System.IO.File.Copy(filepath, ss);

        //        }
        //        filecnt++;
        //        file.Close();
        //        System.IO.File.Delete(Server.MapPath("~/AppFiles/Ans_File_txt/") + Path.GetFileName(filepath));
        //    }
        //    catch (Exception exe)
        //    {

        //    }
        //}

        //public void FeedData(string header, string ans, Tbl_Decoded_Answer_Sheets_Sada tbl_Decoded_Answer_Sheets)
        //{

        //    switch (header)
        //    {
        //        case "SEAT_NO":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Seat_No == null)
        //                tbl_Decoded_Answer_Sheets.Seat_No = ans;
        //            break;
        //        case "LOGIN_MSG":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Login_Status == null)
        //                tbl_Decoded_Answer_Sheets.Login_Status = ans;
        //            break;
        //        case "PAPER_ID":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Paper_ID == null)
        //                tbl_Decoded_Answer_Sheets.Paper_ID = ans;
        //            break;
        //        case "EXAM_TIME":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Exam_Time == null)
        //                tbl_Decoded_Answer_Sheets.Exam_Time = ans;
        //            break;
        //        case "Q1ANS1":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS1 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS1 = ans;
        //            break;
        //        case "Q1ANS2":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS2 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS2 = ans;
        //            break;
        //        case "Q1ANS3":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS3 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS3 = ans;
        //            break;
        //        case "Q1ANS4":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS4 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS4 = ans;
        //            break;
        //        case "Q1ANS5":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS5 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS5 = ans;
        //            break;
        //        case "Q1ANS6":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS6 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS6 = ans;
        //            break;
        //        case "Q1ANS7":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS7 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS7 = ans;
        //            break;
        //        case "Q1ANS8":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS8 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS8 = ans;
        //            break;
        //        case "Q1ANS9":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS9 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS9 = ans;
        //            break;
        //        case "Q1ANS10":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANS10 == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS10 = ans;
        //            break;
        //        case "Q1ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q1ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q1ANS1 = ans;
        //            break;
        //        case "Q2ANS1":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS1 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS1 = ans;
        //            break;
        //        case "Q2ANS2":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS2 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS2 = ans;
        //            break;
        //        case "Q2ANS3":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS3 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS3 = ans;
        //            break;
        //        case "Q2ANS4":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS4 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS4 = ans;
        //            break;
        //        case "Q2ANS5":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS5 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS5 = ans;
        //            break;
        //        case "Q2ANS6":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS6 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS6 = ans;
        //            break;
        //        case "Q2ANS7":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS7 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS7 = ans;
        //            break;
        //        case "Q2ANS8":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS8 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS8 = ans;
        //            break;
        //        case "Q2ANS9":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS9 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS9 = ans;
        //            break;
        //        case "Q2ANS10":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANS10 == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS10 = ans;
        //            break;
        //        case "Q2ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q2ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANSWERTIME = ans;
        //            break;
        //        case "Q3ANS1":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS1 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS2":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS2 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS3":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS3 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS4":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS4 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS5":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS5 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS6":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS6 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS7":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS7 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS8":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS8 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS9":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS9 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANS10":
        //            {
        //                if (ans != "~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q3ANS10 = ans;
        //                }
        //                break;
        //            }
        //        case "Q3ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q3ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q2ANS1 = ans;
        //            break;
        //        case "Q4ANS1":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS1 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS2":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS2 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS3":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS3 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS4":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS4 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS5":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS5 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS6":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS6 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS7":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS7 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS8":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS8 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS9":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS9 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANS10":
        //            {
        //                if (ans != "~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q4ANS10 = ans;
        //                }
        //                break;
        //            }
        //        case "Q4ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q4ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q4ANSWERTIME = ans;
        //            break;
        //        case "Q5ANS1":
        //            {
        //                if (ans != "~~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q5ANS1 = ans;
        //                }
        //                break;
        //            }
        //        case "Q5ANS2":
        //            {
        //                if (ans != "~~~~~~" && ans != "")
        //                {
        //                    tbl_Decoded_Answer_Sheets.Q5ANS2 = ans;
        //                }
        //                break;
        //            }
        //        case "Q5ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q5ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q5ANSWERTIME = ans;
        //            break;
        //        case "Q6ANS1":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q6ANS1 == null)
        //                tbl_Decoded_Answer_Sheets.Q6ANS1 = ans;
        //            break;
        //        case "Q6ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q6ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q6ANSWERTIME = ans;
        //            break;
        //        case "Q7ANS1":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS1 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS1 = ans;
        //            break;
        //        case "Q7ANS2":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS2 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS2 = ans;
        //            break;
        //        case "Q7ANS3":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS3 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS3 = ans;
        //            break;
        //        case "Q7ANS4":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS4 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS4 = ans;
        //            break;
        //        case "Q7ANS5":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS5 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS5 = ans;
        //            break;
        //        case "Q7ANS6":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS6 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS6 = ans;
        //            break;
        //        case "Q7ANS7":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS7 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS7 = ans;
        //            break;
        //        case "Q7ANS8":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANS8 == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANS8 = ans;
        //            break;
        //        case "Q7ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q7ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q7ANSWERTIME = ans;
        //            break;
        //        case "Q8ANS1":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q8ANS1 == null)
        //                tbl_Decoded_Answer_Sheets.Q8ANS1 = ans;
        //            break;
        //        case "Q8ANS2":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q8ANS2 == null)
        //                tbl_Decoded_Answer_Sheets.Q8ANS2 = ans;
        //            break;
        //        case "Q8ANS3":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q8ANS3 == null)
        //                tbl_Decoded_Answer_Sheets.Q8ANS3 = ans;
        //            break;
        //        case "Q8ANS4":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q8ANS4 == null)
        //                tbl_Decoded_Answer_Sheets.Q8ANS4 = ans;
        //            break;
        //        case "Q8ANSWER":
        //            if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answer_Sheets.Q8ANSWERTIME == null)
        //                tbl_Decoded_Answer_Sheets.Q8ANSWERTIME = ans;
        //            break;
        //    }
        //}
    }
}