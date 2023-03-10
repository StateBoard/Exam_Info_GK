using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exam_Info.Helper;
using Exam_Info.Models;
using OfficeOpenXml;



namespace Exam_Info.Controllers
{
    public class CollegeController : Controller
    {
        GKJ_2022Entities db = new GKJ_2022Entities();
        Common common = new Common();
        char[] sep = new char[] { 'ß' };
        char[] back = new char[] { ' ' };
        string[] result2 = new string[20];
        string result;
        // GET: College
        public ActionResult College_DashBoard()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model mm = common.Get_Board_Details(login_model.Index_No);
            if (login_model == null)
            {
                return RedirectToAction("Login", "Login");
            }

            List<CoOrdinator_Display_Model> CoOr_Display = new List<CoOrdinator_Display_Model>();


            List<Tbl_Division_Co_Ordinator_Registration> tbl1 = db.Database.SqlQuery<Tbl_Division_Co_Ordinator_Registration>("select * from Tbl_Division_Co_Ordinator_Registration where  Active=1  and  substring(Index_No,1,2) in (select district_code from Tbl_Code_Master where division_code ='" + mm.Division_Code + "' )").ToList();
            foreach (Tbl_Division_Co_Ordinator_Registration item in tbl1)
            {
                CoOrdinator_Display_Model m = new CoOrdinator_Display_Model();
                m.Name = item.Coordinator_Name;
                m.Mobile_No = item.Coordinator_Mobile;
                m.Type = "Division Co -Ordinator";
                CoOr_Display.Add(m);
            }
            List<Tbl_State_Co_Ordinator_Registration> tbl = db.Tbl_State_Co_Ordinator_Registration.Where(a => a.Active == 1).ToList();
            foreach (Tbl_State_Co_Ordinator_Registration item in tbl)
            {
                CoOrdinator_Display_Model m = new CoOrdinator_Display_Model();
                m.Name = item.Coordinator_Name;
                m.Mobile_No = item.Coordinator_Mobile;
                m.Type = "State Co -Ordinator";
                CoOr_Display.Add(m);
            }

            var batch = db.Tbl_Batch_Activation.Where(x => x.Active == 1).ToList();
            if (batch != null)
            {
                foreach (var item in batch)
                {
                    if (item.Batch == "B1")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B1).FirstOrDefault();
                        TempData["B1Pass"] = pass;
                    }
                    if (item.Batch == "B2")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B2).FirstOrDefault();
                        //ViewBag.Msg= pass;
                        TempData["B2Pass"] = pass;
                    }
                    if (item.Batch == "B4")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B4).FirstOrDefault();
                        TempData["B4Pass"] = pass;
                    }
                    if (item.Batch == "B3")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B3).FirstOrDefault();
                        TempData["B3Pass"] = pass;
                    }
                    if (item.Batch == "B5")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B5).FirstOrDefault();
                        TempData["B5Pass"] = pass;
                    }
                    if (item.Batch == "B6")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B6).FirstOrDefault();
                        TempData["B6Pass"] = pass;
                    }
                    if (item.Batch == "B7")
                    {
                        var pass = db.Tbl_Password.Where(x => x.Index_No == login_model.Index_No).Select(n => n.B7).FirstOrDefault();
                        TempData["B7Pass"] = pass;
                    }
                }

            }
            College_DashBoard_Model co_model = new College_DashBoard_Model();
            co_model.tbl_College_Registrations = GetCollegeRecord();
            co_model.coOrdinator_Display_Model = CoOr_Display;



            return View(co_model);
        }

        public ActionResult College_Dash_Details()
        {
            var model = db.Admin_tbl.Where(x => x.Type == "College Dashboard" && x.Active == "1").ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);

        }
        public List<Tbl_College_Registration> GetCollegeRecord()
        {
            Login_Model login_model = common.Get_Login_Details();
            return db.Tbl_College_Registration.Where(m => m.Index_No == login_model.Index_No).OrderBy(m => m.Index_No).ToList();
        }
        [HttpGet]
        public ActionResult Ans_File_Upload()
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            if (login_model == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (db.Tbl_File_Upload_Details.Any(x => x.Index_No == login_model.Index_No))
            {
                return Redirect("../Certificate_PDF/" + login_model.Index_No + ".pdf");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Ans_File_Upload(Model2 m)
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

        public ActionResult UploadFile(Model2 m)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
            try
            {
                string path = Server.MapPath("~/AppFiles/Upload/");
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    //System.Threading.Thread.Sleep(1000);
                    HttpPostedFileBase file = files[i];
                    string[] name = file.FileName.Split('/');
                    file.SaveAs(path + "/" + name[1]);
                    ConvertTotxtFile(Server.MapPath("~/AppFiles/Upload/" + name[1] + ""));
                }
                return Json(new { Result = true, Response = "File Uploaded Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Something Went Wrong" + ex }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadNewFile()
        {
            try
            {
                string path = Server.MapPath("~/AppFiles/Upload/");
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    //System.Threading.Thread.Sleep(1000);
                    HttpPostedFileBase file = files[i];
                    string[] name = file.FileName.Split('/');
                    file.SaveAs(path + name[0]);
                    ConvertTotxtFile(Server.MapPath("~/AppFiles/Upload/" + name[0] + ""));
                }
                return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ConvertTotxtFile(string path)
        {
            string extension = path.Substring(path.LastIndexOf("."));
            string[] fn = path.Split('\\');
            string fname = fn[fn.Length - 1];

            // if (Path.GetFileName(sFile).StartsWith(letter))
            // {

            string fn1 = fname.Substring(0, fname.LastIndexOf("."));
            if (extension == ".ans")
            {

                string destinationPath = Server.MapPath("~/AppFiles/Ans_File_txt/" + fn1 + ".txt").ToString();
                using (StreamWriter sw = System.IO.File.CreateText(destinationPath))
                {
                    sw.Close();
                }

                TextWriter tw = new StreamWriter(@"" + destinationPath + "");
                // write a line of text to the file
                TextReader re = new StreamReader(path);
                string line = null;
                enc.Class1 ex = new enc.Class1();
                if ((line = re.ReadLine()) != null)
                {
                    string d;
                    // string[] filelines = System.IO.File.ReadAllLines(line);                       
                    //string d = ex.xencryption(line);
                    d = line;
                    tw.WriteLine(d);
                    while ((line = re.ReadLine()) != null)
                    {
                        if (line == "")
                        {
                            continue;
                        }

                        //cnt1++;
                        if (true)
                        {
                            string a = ex.xencryption(line);
                            d = ex.xencryption(line);
                            if (d == null)
                            {
                                d = " ";
                            }
                            else
                            {
                                result2 = d.Split(sep, StringSplitOptions.None);

                                result = d.Replace("ß", " ");
                               
                            }
                            tw.WriteLine(result);
                        }
                    }
                }
                tw.Close();
                InsertIntoDB(destinationPath);
            }
            else
            {

            }
        }
        [HttpGet]
        public JsonResult CheckAndVerify()
        {
            try
            {
                Login_Model login_model = common.Get_Login_Details();
                Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);
                List<FileStatusModel> filestatusmodel = new List<FileStatusModel>();
                filestatusmodel = db.Database.SqlQuery<FileStatusModel>(@"select distinct A.Index_No, A.Seat_No, B.Seat_No Uploaded_Seat_File, 
                                                                            Case When B.Seat_No is null then 'Not_Detected'
                                                                                 when B.Seat_No = A.Seat_No then 'Detected'
                                                                            	 end as File_Status
                                                                            from Tbl_Login A left join Tbl_Decode_Answer B on A.Seat_No = B.Seat_No where A.Index_No = '" + login_model.Index_No + "' order by A.Seat_NO").ToList();
                return Json(new { Result = true, Data = filestatusmodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Something went wrong !" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Obsolete]
        public ActionResult Final_Upload(FileStatus fileStatusModel, FileStatusModel REA)
        {
            try
            {


                Login_Model login_Model = common.Get_Login_Details();
                List<Tbl_Decode_Answer> tbl_Decoded_Answere_Data = db.Tbl_Decode_Answer.Where(a => a.index_no == login_Model.Index_No).ToList();
                foreach (var item in fileStatusModel.fileStatuses)
                {
                    if (!tbl_Decoded_Answere_Data.Any(a => a.seat_no == item.Seat_No))
                    {

                        if (item.Reason == "" || item.Reason == null || item.Reason == "0")
                        {
                            return Json(new { Result = false, Response = "Please Upload Ans File / Reason " + item.Seat_No }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                PDFController pdf = new PDFController();
                FileUploadModel file = new FileUploadModel();

                file.Index_No = login_Model.Index_No;
                file.Teacher_Name = fileStatusModel.Teacher_Name;
                file.Email_Id = fileStatusModel.Email_Id;
                file.Mobile_No = fileStatusModel.Mobile_No;
                file.Total_Students = fileStatusModel.Total_Students.ToString();
                file.Total_Present_Student = fileStatusModel.Total_Present_Student.ToString();
                file.Total_Absent_Student = fileStatusModel.Total_Absent_Student.ToString();
                int cnt = db.Tbl_Decode_Answer.Where(x => x.index_no == login_Model.Index_No).Distinct().Count();
                file.Text_Ans_File = cnt.ToString();
                file.files = fileStatusModel.Text_Ans_File.ToString();
                file.Not_Upload_File = fileStatusModel.Total_Absent_Student;


                pdf.Create_PDF(file);




                Tbl_File_Upload_Details tbl_file_upload = new Tbl_File_Upload_Details();

                tbl_file_upload.Index_No = login_Model.Index_No;
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                tbl_file_upload.IP_ADDRESS = myIP;
                tbl_file_upload.date_Time = DateTime.Now;
                tbl_file_upload.Teacher_Name = fileStatusModel.Teacher_Name;
                tbl_file_upload.Mobile_Number = fileStatusModel.Mobile_No;
                tbl_file_upload.Email_Id = fileStatusModel.Email_Id;
                tbl_file_upload.Total_Students = fileStatusModel.Total_Students;
                tbl_file_upload.Total_Present_Student = fileStatusModel.Total_Present_Student;
                tbl_file_upload.Total_Absent_Student = fileStatusModel.Total_Absent_Student;
                tbl_file_upload.Text_Ans_File = fileStatusModel.Text_Ans_File;

                db.Tbl_File_Upload_Details.Add(tbl_file_upload);
                db.SaveChanges();

                var arr = fileStatusModel.fileStatuses.ToArray();

                FileStatusModel obj = new FileStatusModel();
                File_Upload_Reason fileReason = new File_Upload_Reason();
                fileReason.Index_No = login_Model.Index_No;
                var res = arr.AsEnumerable();

                foreach (var ele in arr)
                {
                    var arr2 = res.ToArray();

                }
                //fileReason.Seat_No= arr.AsEnumerablemap(current => { return { Seat_No: current.Seat_No } });
                //fileReason.Seat_No = REA.Seat_No;
                //fileReason.File_Status = obj.File_Status;
                //fileReason.Reason = obj.Reason;
                db.File_Upload_Reason.Add(fileReason);
                db.SaveChanges();
                return Json(new { Result = true, Response = "../Certificate_PDF/" + login_Model.Index_No + ".pdf" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = true, Response = ex }, JsonRequestBehavior.AllowGet);
            }
        }
        private void InsertIntoDB(string filepath)
        {

            Login_Model login_Model = common.Get_Login_Details();
            //string[] filePaths = Directory.GetFiles(@textBox1.Text, "*txt");
            string line;
            char starting = '¶';
            //char ending = '£';
            char ending = '~';
            bool headerfound = false;
            bool headercomplete = false;
            int filecnt = 0;
            int linecounter = 0;
            if (!Directory.Exists(Server.MapPath("~/AppFiles/Done_Ans_File_txt")))
            {
                Directory.CreateDirectory(Server.MapPath("~/AppFiles/Done_Ans_File_txt"));
            }
            try
            {
                Tbl_Decode_Answer tbl_Decoded_Answere_Data = new Tbl_Decode_Answer();
                string headerphrase = "";
                string ansphrase = "";
                StreamReader file = new StreamReader(filepath);
                while ((line = file.ReadLine()) != null)
                {
                     linecounter ++;
                    char[] ch = new char[line.Length];
                    // Copy character by character into array 
                    for (int j = 0; j < line.Length; j++)
                    {
                        ch[j] = line[j];
                    }
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (ch[j] == starting)
                        {
                            headerfound = true;
                        }
                        if (headerfound && ch[j] != ending && headercomplete == false)
                        {
                            if (ch[j] == '¶')
                            {
                                continue;
                            }
                            headerphrase += ch[j];
                        }
                        if (ch[j] == ending)
                        {
                            headercomplete = true;
                        }
                        if (headercomplete)
                        {
                            //if (ch[j] == '£')
                            //{
                            //    continue;
                            //}
                            if (ch[j] == '~')
                            {
                                continue;
                            }
                            ansphrase += ch[j];
                        }
                    }
                    if (linecounter == 4)
                        tbl_Decoded_Answere_Data.seat_no = line;
                    if (linecounter == 6)
                        tbl_Decoded_Answere_Data.Paper_ID = line.Substring(1, 4);
                    if (linecounter == 7)

                        tbl_Decoded_Answere_Data.datetime = line;
                    if (ansphrase.Trim() == "")
                    {
                        headerfound = false;
                        headercomplete = false;
                        headerphrase = "";
                        ansphrase = "";
                        continue;
                    }
                    FeedData(headerphrase, ansphrase, tbl_Decoded_Answere_Data);
                    headerfound = false;
                    headercomplete = false;
                    headerphrase = "";
                    ansphrase = "";
                }
                tbl_Decoded_Answere_Data.index_no = login_Model.Index_No;
                db.Tbl_Decode_Answer.Add(tbl_Decoded_Answere_Data);
                db.SaveChanges();

                string ss = Server.MapPath("~/AppFiles/Done_Ans_File_txt/") + Path.GetFileName(filepath);

                if (!System.IO.File.Exists(Server.MapPath("~/AppFiles/Done_Ans_File_txt/") + Path.GetFileName(filepath)))
                {
                    System.IO.File.Copy(filepath, ss);

                }
                filecnt++;
                file.Close();
                System.IO.File.Delete(Server.MapPath("~/AppFiles/Ans_File_txt/") + Path.GetFileName(filepath));
            }
            catch (Exception exe)
            {

            }
        }

        public void FeedData(string header, string ans, Tbl_Decode_Answer tbl_Decoded_Answere_Data)
        {

            switch (header)
            {
                case "Index_No":
                    if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answere_Data.index_no == null)
                        tbl_Decoded_Answere_Data.index_no = ans;
                    break;
                case "SEAT_NO":
                    if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answere_Data.seat_no == null)
                        tbl_Decoded_Answere_Data.seat_no = ans;
                    break;

                case "DateTime":
                    if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answere_Data.datetime == null)
                        tbl_Decoded_Answere_Data.datetime = ans;
                    break;

                case "IP":
                    if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answere_Data.ip == null)
                        tbl_Decoded_Answere_Data.ip = ans;
                    break;

                case "PAPER_ID":
                    if ((ans != null && ans.Trim() != "") && tbl_Decoded_Answere_Data.Paper_ID == null)
                        tbl_Decoded_Answere_Data.Paper_ID = ans;
                    break;
               
                case "Q1":
                    if (tbl_Decoded_Answere_Data.Q1_Ans == "" || tbl_Decoded_Answere_Data.Q1_Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q1_Ans = ans;
                    }

                    break;
                case "Q2":
                    if (tbl_Decoded_Answere_Data.Q_2Ans == "" || tbl_Decoded_Answere_Data.Q_2Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_2Ans = ans;
                    }

                    break;
                case "Q3":
                    if (tbl_Decoded_Answere_Data.Q_3Ans == "" || tbl_Decoded_Answere_Data.Q_3Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_3Ans = ans;
                    }

                    break;
                case "Q4":
                    if (tbl_Decoded_Answere_Data.Q_4Ans == "" || tbl_Decoded_Answere_Data.Q_4Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_4Ans = ans;
                    }


                    break;
                case "Q5":
                    if (tbl_Decoded_Answere_Data.Q_5Ans == "" || tbl_Decoded_Answere_Data.Q_5Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_5Ans = ans;
                    }


                    break;
                case "Q6":
                    if (tbl_Decoded_Answere_Data.Q_6Ans == "" || tbl_Decoded_Answere_Data.Q_6Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_6Ans = ans;

                    }


                    break;
                case "Q7":
                    if (tbl_Decoded_Answere_Data.Q_7Ans == "" || tbl_Decoded_Answere_Data.Q_7Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_7Ans = ans;
                    }


                    break;
                case "Q8":
                    if (tbl_Decoded_Answere_Data.Q_8Ans == "" || tbl_Decoded_Answere_Data.Q_8Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_8Ans = ans;

                    }


                    break;
                case "Q9":
                    if (tbl_Decoded_Answere_Data.Q_9Ans == "" || tbl_Decoded_Answere_Data.Q_9Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_9Ans = ans;
                    }

                    break;
                case "Q10":
                    if (tbl_Decoded_Answere_Data.Q_10Ans == "" || tbl_Decoded_Answere_Data.Q_10Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_10Ans = ans;
                    }


                    break;
                case "Q11":
                    if (tbl_Decoded_Answere_Data.Q_11Ans == "" || tbl_Decoded_Answere_Data.Q_11Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_11Ans = ans;
                    }


                    break;
                case "Q12":
                    if (tbl_Decoded_Answere_Data.Q_12Ans == "" || tbl_Decoded_Answere_Data.Q_12Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_12Ans = ans;
                    }


                    break;
                case "Q13":
                    if (tbl_Decoded_Answere_Data.Q_13Ans == "" || tbl_Decoded_Answere_Data.Q_13Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_13Ans = ans;

                    }


                    break;
                case "Q14":
                    if (tbl_Decoded_Answere_Data.Q_14Ans == "" || tbl_Decoded_Answere_Data.Q_14Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_14Ans = ans;
                    }


                    break;
                case "Q15":
                    if (tbl_Decoded_Answere_Data.Q_15Ans == "" || tbl_Decoded_Answere_Data.Q_15Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_15Ans = ans;
                    }

                    break;
                case "Q16":
                    if (tbl_Decoded_Answere_Data.Q_16Ans == "" || tbl_Decoded_Answere_Data.Q_16Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_16Ans = ans;
                    }

                    break;
                case "Q17":
                    if (tbl_Decoded_Answere_Data.Q_17Ans == "" || tbl_Decoded_Answere_Data.Q_17Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_17Ans = ans;
                    }


                    break;
                case "Q18":
                    if (tbl_Decoded_Answere_Data.Q_18Ans == "" || tbl_Decoded_Answere_Data.Q_18Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_18Ans = ans;

                    }


                    break;
                case "Q19":
                    if (tbl_Decoded_Answere_Data.Q_19Ans == "" || tbl_Decoded_Answere_Data.Q_19Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_19Ans = ans;
                    }

                    break;
                case "Q20":
                    if (tbl_Decoded_Answere_Data.Q_20Ans == "" || tbl_Decoded_Answere_Data.Q_20Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_20Ans = ans;
                    }


                    break;
                case "Q21":
                    if (tbl_Decoded_Answere_Data.Q_21Ans == "" || tbl_Decoded_Answere_Data.Q_21Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_21Ans = ans;
                    }


                    break;
                case "Q22":
                    if (tbl_Decoded_Answere_Data.Q_22Ans == "" || tbl_Decoded_Answere_Data.Q_22Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_22Ans = ans;
                    }


                    break;
                case "Q23":
                    if (tbl_Decoded_Answere_Data.Q_23Ans == "" || tbl_Decoded_Answere_Data.Q_23Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_23Ans = ans;
                    }


                    break;
                case "Q24":
                    if (tbl_Decoded_Answere_Data.Q_24Ans == "" || tbl_Decoded_Answere_Data.Q_24Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_24Ans = ans;

                    }

                    break;
                case "Q25":
                    if (tbl_Decoded_Answere_Data.Q_25Ans == "" || tbl_Decoded_Answere_Data.Q_25Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_25Ans = ans;
                    }


                    break;
                case "Q26":
                    if (tbl_Decoded_Answere_Data.Q_26Ans == "" || tbl_Decoded_Answere_Data.Q_26Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_26Ans = ans;
                    }


                    break;
                case "Q27":
                    if (tbl_Decoded_Answere_Data.Q_27Ans == "" || tbl_Decoded_Answere_Data.Q_27Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_27Ans = ans;

                    }

                    break;
                case "Q28":
                    if (tbl_Decoded_Answere_Data.Q_28Ans == "" || tbl_Decoded_Answere_Data.Q_28Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_28Ans = ans;
                    }


                    break;
                case "Q29":
                    if (tbl_Decoded_Answere_Data.Q_29Ans == "" || tbl_Decoded_Answere_Data.Q_29Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_29Ans = ans;


                    }

                    break;
                case "Q30":
                    if (tbl_Decoded_Answere_Data.Q_30Ans == "" || tbl_Decoded_Answere_Data.Q_30Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_30Ans = ans;
                    }


                    break;
                case "Q31":
                    if (tbl_Decoded_Answere_Data.Q_31Ans == "" || tbl_Decoded_Answere_Data.Q_31Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_31Ans = ans;
                    }


                    break;
                case "Q32":
                    if (tbl_Decoded_Answere_Data.Q_32Ans == "" || tbl_Decoded_Answere_Data.Q_32Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_32Ans = ans;
                    }

                    break;
                case "Q33":
                    if (tbl_Decoded_Answere_Data.Q_33Ans == "" || tbl_Decoded_Answere_Data.Q_33Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_33Ans = ans;
                    }


                    break;
                case "Q34":
                    if (tbl_Decoded_Answere_Data.Q_24Ans == "" || tbl_Decoded_Answere_Data.Q_24Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_34Ans = ans;
                    }

                    {
                        tbl_Decoded_Answere_Data.Q_34Ans = ans;
                    }

                    break;
                case "Q35":
                    if (tbl_Decoded_Answere_Data.Q_35Ans == "" || tbl_Decoded_Answere_Data.Q_35Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_35Ans = ans;
                    }


                    break;
                case "Q36":
                    if (tbl_Decoded_Answere_Data.Q_36Ans == "" || tbl_Decoded_Answere_Data.Q_36Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_36Ans = ans;
                    }



                    break;
                case "Q37":
                    if (tbl_Decoded_Answere_Data.Q_37Ans == "" || tbl_Decoded_Answere_Data.Q_37Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_37Ans = ans;
                    }



                    break;
                case "Q38":
                    if (tbl_Decoded_Answere_Data.Q_38Ans == "" || tbl_Decoded_Answere_Data.Q_38Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_38Ans = ans;
                    }



                    break;
                case "Q39":
                    if (tbl_Decoded_Answere_Data.Q_39Ans == "" || tbl_Decoded_Answere_Data.Q_39Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_39Ans = ans;
                    }



                    break;
                case "Q40":
                    if (tbl_Decoded_Answere_Data.Q_40Ans == "" || tbl_Decoded_Answere_Data.Q_40Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_40Ans = ans;
                    }


                    break;
                case "Q41":
                    if (tbl_Decoded_Answere_Data.Q_41Ans == "" || tbl_Decoded_Answere_Data.Q_41Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_41Ans = ans;
                    }



                    break;
                case "Q42":
                    if (tbl_Decoded_Answere_Data.Q_42Ans == "" || tbl_Decoded_Answere_Data.Q_42Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_42Ans = ans;
                    }


                    break;
                case "Q43":
                    if (tbl_Decoded_Answere_Data.Q_43Ans == "" || tbl_Decoded_Answere_Data.Q_43Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_43Ans = ans;
                    }


                    break;
                case "Q44":
                    if (tbl_Decoded_Answere_Data.Q_44Ans == "" || tbl_Decoded_Answere_Data.Q_44Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_44Ans = ans;
                    }



                    break;
                case "Q45":
                    if (tbl_Decoded_Answere_Data.Q_45Ans == "" || tbl_Decoded_Answere_Data.Q_45Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_45Ans = ans;
                    }



                    break;
                case "Q46":
                    if (tbl_Decoded_Answere_Data.Q_46Ans == "" || tbl_Decoded_Answere_Data.Q_46Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_46Ans = ans;

                    }

                    break;
                case "Q47":
                    if (tbl_Decoded_Answere_Data.Q_47Ans == "" || tbl_Decoded_Answere_Data.Q_47Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_47Ans = ans;
                    }



                    break;
                case "Q48":
                    if (tbl_Decoded_Answere_Data.Q_48Ans == "" || tbl_Decoded_Answere_Data.Q_48Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_48Ans = ans;
                    }


                    break;
                case "Q49":
                    if (tbl_Decoded_Answere_Data.Q_49Ans == "" || tbl_Decoded_Answere_Data.Q_49Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_49Ans = ans;
                    }



                    break;
                case "Q50":
                    if (tbl_Decoded_Answere_Data.Q_50Ans == "" || tbl_Decoded_Answere_Data.Q_50Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_50Ans = ans;
                    }

                    break;
                case "Q51":
                    if (tbl_Decoded_Answere_Data.Q_51Ans == "" || tbl_Decoded_Answere_Data.Q_51Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_51Ans = ans;
                    }



                    break;
                case "Q52":
                    if (tbl_Decoded_Answere_Data.Q_52Ans == "" || tbl_Decoded_Answere_Data.Q_52Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_52Ans = ans;
                    }



                    break;
                case "Q53":
                    if (tbl_Decoded_Answere_Data.Q_53Ans == "" || tbl_Decoded_Answere_Data.Q_53Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_53Ans = ans;
                    }



                    break;
                case "Q54":
                    if (tbl_Decoded_Answere_Data.Q_54Ans == "" || tbl_Decoded_Answere_Data.Q_54Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_54Ans = ans;
                    }



                    break;
                case "Q55":
                    if (tbl_Decoded_Answere_Data.Q_55Ans == "" || tbl_Decoded_Answere_Data.Q_55Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_55Ans = ans;
                    }



                    break;
                case "Q56":
                    if (tbl_Decoded_Answere_Data.Q_56Ans == "" || tbl_Decoded_Answere_Data.Q_56Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_56Ans = ans;
                    }



                    break;
                case "Q57":
                    if (tbl_Decoded_Answere_Data.Q_57Ans == "" || tbl_Decoded_Answere_Data.Q_57Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_57Ans = ans;

                    }



                    break;
                case "Q58":
                    if (tbl_Decoded_Answere_Data.Q_58Ans == "" || tbl_Decoded_Answere_Data.Q_58Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_58Ans = ans;
                    }



                    break;
                case "Q59":
                    if (tbl_Decoded_Answere_Data.Q_59Ans == "" || tbl_Decoded_Answere_Data.Q_59Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_59Ans = ans;
                    }



                    break;
                case "Q60":
                    if (tbl_Decoded_Answere_Data.Q_60Ans == "" || tbl_Decoded_Answere_Data.Q_60Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_60Ans = ans;
                    }


                    break;
                case "Q61":
                    if (tbl_Decoded_Answere_Data.Q_61Ans == "" || tbl_Decoded_Answere_Data.Q_61Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_61Ans = ans;
                    }


                    break;
                case "Q62":
                    if (tbl_Decoded_Answere_Data.Q_62Ans == "" || tbl_Decoded_Answere_Data.Q_62Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_62Ans = ans;
                    }



                    break;
                case "Q63":
                    if (tbl_Decoded_Answere_Data.Q_63Ans == "" || tbl_Decoded_Answere_Data.Q_63Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_63Ans = ans;
                    }


                    break;
                case "Q64":
                    if (tbl_Decoded_Answere_Data.Q_64Ans == "" || tbl_Decoded_Answere_Data.Q_64Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_64Ans = ans;
                    }



                    break;
                case "Q65":
                    if (tbl_Decoded_Answere_Data.Q_65Ans == "" || tbl_Decoded_Answere_Data.Q_65Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_65Ans = ans;
                    }



                    break;
                case "Q66":
                    if (tbl_Decoded_Answere_Data.Q_66Ans == "" || tbl_Decoded_Answere_Data.Q_66Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_66Ans = ans;
                    }


                    break;
                case "Q67":
                    if (tbl_Decoded_Answere_Data.Q_67Ans == "" || tbl_Decoded_Answere_Data.Q_67Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_67Ans = ans;

                    }



                    break;
                case "Q68":
                    if (tbl_Decoded_Answere_Data.Q_68Ans == "" || tbl_Decoded_Answere_Data.Q_68Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_68Ans = ans;
                    }



                    break;
                case "Q69":
                    if (tbl_Decoded_Answere_Data.Q_69Ans == "" || tbl_Decoded_Answere_Data.Q_69Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_69Ans = ans;

                    }


                    break;
                case "Q70":
                    if (tbl_Decoded_Answere_Data.Q_70Ans == "" || tbl_Decoded_Answere_Data.Q_70Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_70Ans = ans;
                    }



                    break;
                case "Q71":
                    if (tbl_Decoded_Answere_Data.Q_71Ans == "" || tbl_Decoded_Answere_Data.Q_71Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_71Ans = ans;
                    }



                    break;
                case "Q72":
                    if (tbl_Decoded_Answere_Data.Q_72Ans == "" || tbl_Decoded_Answere_Data.Q_72Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_72Ans = ans;
                    }



                    break;
                case "Q73":
                    if (tbl_Decoded_Answere_Data.Q_73Ans == "" || tbl_Decoded_Answere_Data.Q_73Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_73Ans = ans;
                    }



                    break;
                case "Q74":
                    if (tbl_Decoded_Answere_Data.Q_74Ans == "" || tbl_Decoded_Answere_Data.Q_74Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_74Ans = ans;
                    }



                    break;
                case "Q75":
                    if (tbl_Decoded_Answere_Data.Q_75Ans == "" || tbl_Decoded_Answere_Data.Q_75Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_75Ans = ans;
                    }


                    break;
                case "Q76":
                    if (tbl_Decoded_Answere_Data.Q_76Ans == "" || tbl_Decoded_Answere_Data.Q_76Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_76Ans = ans;
                    }



                    break;
                case "Q77":
                    if (tbl_Decoded_Answere_Data.Q_77Ans == "" || tbl_Decoded_Answere_Data.Q_77Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_77Ans = ans;
                    }



                    break;
                case "Q78":
                    if (tbl_Decoded_Answere_Data.Q_78Ans == "" || tbl_Decoded_Answere_Data.Q_78Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_78Ans = ans;
                    }



                    break;
                case "Q79":
                    if (tbl_Decoded_Answere_Data.Q_79Ans == "" || tbl_Decoded_Answere_Data.Q_79Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_79Ans = ans;
                    }


                    break;
                case "Q80":
                    if (tbl_Decoded_Answere_Data.Q_80Ans == "" || tbl_Decoded_Answere_Data.Q_80Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_80Ans = ans;
                    }



                    break;
                case "Q81":
                    if (tbl_Decoded_Answere_Data.Q_81Ans == "" || tbl_Decoded_Answere_Data.Q_81Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_81Ans = ans;
                    }


                    break;
                case "Q82":
                    if (tbl_Decoded_Answere_Data.Q_82Ans == "" || tbl_Decoded_Answere_Data.Q_82Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_82Ans = ans;
                    }



                    break;
                case "Q83":
                    if (tbl_Decoded_Answere_Data.Q_83Ans == "" || tbl_Decoded_Answere_Data.Q_83Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_83Ans = ans;
                    }


                    break;
                case "Q84":
                    if (tbl_Decoded_Answere_Data.Q_84Ans == "" || tbl_Decoded_Answere_Data.Q_84Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_84Ans = ans;
                    }



                    break;
                case "Q85":
                    if (tbl_Decoded_Answere_Data.Q_85Ans == "" || tbl_Decoded_Answere_Data.Q_85Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_85Ans = ans;
                    }


                    break;
                case "Q86":
                    if (tbl_Decoded_Answere_Data.Q_86Ans == "" || tbl_Decoded_Answere_Data.Q_86Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_86Ans = ans;
                    }



                    break;
                case "Q87":
                    if (tbl_Decoded_Answere_Data.Q_87Ans == "" || tbl_Decoded_Answere_Data.Q_87Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_87Ans = ans;

                    }



                    break;
                case "Q88":
                    if (tbl_Decoded_Answere_Data.Q_88Ans == "" || tbl_Decoded_Answere_Data.Q_88Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_88Ans = ans;

                    }



                    break;
                case "Q89":
                    if (tbl_Decoded_Answere_Data.Q_89Ans == "" || tbl_Decoded_Answere_Data.Q_89Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_89Ans = ans;

                    }


                    break;
                case "Q90":
                    if (tbl_Decoded_Answere_Data.Q_90Ans == "" || tbl_Decoded_Answere_Data.Q_90Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_90Ans = ans;
                    }



                    break;
                case "Q91":
                    if (tbl_Decoded_Answere_Data.Q_91Ans == "" || tbl_Decoded_Answere_Data.Q_91Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_91Ans = ans;

                    }



                    break;
                case "Q92":
                    if (tbl_Decoded_Answere_Data.Q_92Ans == "" || tbl_Decoded_Answere_Data.Q_92Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_92Ans = ans;
                    }


                    break;
                case "Q93":
                    if (tbl_Decoded_Answere_Data.Q_93Ans == "" || tbl_Decoded_Answere_Data.Q_93Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_93Ans = ans;

                    }



                    break;
                case "Q94":
                    if (tbl_Decoded_Answere_Data.Q_94Ans == "" || tbl_Decoded_Answere_Data.Q_94Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_94Ans = ans;
                    }



                    break;
                case "Q95":
                    if (tbl_Decoded_Answere_Data.Q_95Ans == "" || tbl_Decoded_Answere_Data.Q_95Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_95Ans = ans;
                    }




                    break;
                case "Q96":
                    if (tbl_Decoded_Answere_Data.Q_96Ans == "" || tbl_Decoded_Answere_Data.Q_96Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_96Ans = ans;
                    }



                    break;
                case "Q97":
                    if (tbl_Decoded_Answere_Data.Q_97Ans == "" || tbl_Decoded_Answere_Data.Q_97Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_97Ans = ans;
                    }


                    break;
                case "Q98":
                    if (tbl_Decoded_Answere_Data.Q_98Ans == "" || tbl_Decoded_Answere_Data.Q_98Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_98Ans = ans;
                    }



                    break;
                case "Q99":
                    if (tbl_Decoded_Answere_Data.Q_99Ans == "" || tbl_Decoded_Answere_Data.Q_99Ans == null && ans != "")
                    {

                        tbl_Decoded_Answere_Data.Q_99Ans = ans;
                    }



                    break;
                case "Q100":
                    if (tbl_Decoded_Answere_Data.Q_100Ans == "" || tbl_Decoded_Answere_Data.Q_100Ans == null && ans != "")
                    {
                        tbl_Decoded_Answere_Data.Q_100Ans = ans;

                    }



                    break;
            }
        }

        public PartialViewResult ProgressView()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult File_Upload_Certificate()
        {
            Login_Model login_model = common.Get_Login_Details();
            ViewBag.Index_No = login_model.Index_No;
            return View();
        }

       
        public ActionResult Attendance_Page()
        {
            Login_Model login_model = common.Get_Login_Details();
            List<SelectListItem> list = common.Get_Batch_List();
            ViewBag.BatchList = new SelectList(list, "Value", "Text");
            //string query = "select Z.Index_No,Z.Batch,Z.Seat_No,Case when Z.Attendance is null then 'Absent' else 'Present' END Attendance  from(select *, (select top 1 B.Seat_No from Tbl_Attendance B where A.Seat_No = B.Seat_no and A.Batch = B.Batch) Attendance from Tbl_Login A where Index_No = '1317020') Z";
            return View();
        }
        [HttpPost]

        public ActionResult Attendance_Page(Tbl_Attendance_Web modal)
        {

            SqlConnection _Con;
            SqlCommand _Command;
            Login_Model login_model = common.Get_Login_Details();
            try
            {
                string Query = "";
               
                DataTable dt = new DataTable();
               
                _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
                Query = "select Z.Index_No,Z.Name,Z.Batch,Z.Paper_ID,Z.Seat_No,Case when Z.Attendance is null then 'Absent' else 'Present' END Attendance  from(select *, (select top 1 B.Seat_No from Tbl_Attendance B where A.Seat_No = B.Seat_no and A.Batch = B.Batch) Attendance from Tbl_Login A where Index_No = " + login_model.Index_No + ") Z";

               
                SqlDataAdapter a = new SqlDataAdapter(Query, _Con);
                a.Fill(dt);
                List<AttendanceModel> model = new List<AttendanceModel>();
                
               

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Batch"].ToString() == modal.Batch)
                    {
                        AttendanceModel m = new AttendanceModel();
                        m.Index_No = dt.Rows[i]["Index_No"].ToString();
                        m.Batch = dt.Rows[i]["Batch"].ToString();
                        m.Seat_No = dt.Rows[i]["Seat_No"].ToString();
                        m.Name = dt.Rows[i]["Name"].ToString();
                        m.Attendance = dt.Rows[i]["Attendance"].ToString();
                        m.Paper_ID = dt.Rows[i]["Paper_ID"].ToString();
                        model.Add(m);
                    }

                }


                return Json(new { Result = true, Response = model }, JsonRequestBehavior.AllowGet);
                _Con.Close();
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Unable to Fetch Record" + ex }, JsonRequestBehavior.AllowGet);
            }



        }


      
        [HttpPost]
        public JsonResult AttendanceStud(List<Tbl_Attendance_Web> Attendances)
        {
            try
            {
                int insertedRecords = 0;
                Tbl_Login attendance = new Tbl_Login();

                if (Attendances == null)
                {
                    Attendances = new List<Tbl_Attendance_Web>();
                }

               
                foreach (Tbl_Attendance_Web Attendanc in Attendances)
                {
                   
                    if (db.Tbl_Attendance_Web.Any(m => m.Roll_No == Attendanc.Roll_No && m.Batch == Attendanc.Batch ))
                    {

                        return Json(new { Result = true, Message = "Already Submitted" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                       
                        db.Tbl_Attendance_Web.Add(Attendanc);
                        db.SaveChanges();
                        insertedRecords++;
                    }


                }
                return Json(new { Result = true, Message = insertedRecords }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Print(string Batch)
        {
            Login_Model login_model = common.Get_Login_Details();
           
            TempData["Index_No"] = login_model.Index_No;
            return View(GetPrint(Batch));
        }

        IEnumerable<Tbl_Attendance_Web> GetPrint(string Batch)
        {
            

            return db.Tbl_Attendance_Web.Where(m => m.Batch == Batch).ToList().OrderBy(m => m.Roll_No);
        }

        public JsonResult GetPrductDetails()
        {
            var records = (from p in db.Tbl_Attendance_Web select p).ToList();
            return Json(new { Result = true, Response = records }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add_NewStudent()
        {
            Tbl_Login login_model = common.Get_College_Login_Details();

            TempData["Stream"] = "32";
            return View();
        }
        [HttpPost]
        public ActionResult Add_NewStudent(Tbl_Login modal)
        {

            Login_Model login_model = common.Get_Login_Details();

            for (int i = 0; i <= 50; i++)
            {
                if (modal.Hand != null)
                {
                    if (modal.Hand == "i")
                    {
                        modal.Hand = "i";
                    }
                }

            }

            modal.Index_No = login_model.Index_No;
            db.Tbl_Login.Add(modal);
            db.SaveChanges();
            return View("Add_NewStudent");
        }
        [HttpGet]
        public ActionResult Add_Student()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Add_Student(Tbl_Login modal)
        {

            if (modal.Stream == "97")
            {
                modal.Stream = "97";

            }
            else if (modal.Stream == "98")
            {
                modal.Stream = "98";
            }
            else
            {
                modal.Stream = "99";
            }

            //----------------------------

            for (int i = 0; i <= 50; i++)
            {
                if (modal.Hand != null)
                {
                    if (modal.Hand == "i")
                    {
                        modal.Hand = "i";
                    }
                }

            }


            db.Tbl_Login.Add(modal);
            db.SaveChanges();
            return View("Add_Student");
        }
        //IEnumerable<Tbl_College_Registration> GetCollegeRecord()
        //{
        //    Login_Model login_model = common.Get_Login_Details();
        //    if (login_model == null)
        //    {

        //    }
        //    return db.Tbl_College_Registration.Where(m => m.Index_No == login_model.Index_No).ToList().OrderBy(m => m.Index_No);
        //}
        public ActionResult College_Edit_Profile()
        {
            Tbl_College_Registration dc = new Tbl_College_Registration();

            Login_Model login_model = common.Get_Login_Details();

            int temp = db.Tbl_College_Registration.Where(x => x.Index_No == login_model.Index_No).Count();
            if (temp > 0)
            {
                dc = db.Tbl_College_Registration.Where(x => x.Index_No == login_model.Index_No).FirstOrDefault();
            }

            return View(dc);
        }

        [HttpPost]
        public ActionResult College_Edit_Profile(Tbl_College_Registration Update_College_Info)
        {
            try
            {
                db.Tbl_College_Registration.Add(Update_College_Info);
                if (Update_College_Info.ID != 0)
                {
                    db.Entry(Update_College_Info).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Result = true, Response = "Record Updated Sucessfully...!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {

            }
            return View();
        }

        public ActionResult College_Inspection()
        {
            return View(GetAllInspection());
        }
        IEnumerable<Tbl_Inspection> GetAllInspection()
        {
            Login_Model login_model = common.Get_Login_Details();
            return db.Tbl_Inspection.Where(m => m.Index_No == login_model.Index_No).ToList().OrderBy(m => m.Index_No);
            //return db.Tbl_Inspection.Where(m => m.Index_No == Index_Number).ToList().OrderBy(m => m.Index_No);
        }


        public void ExportToExcelV()
        {
            Login_Model login_model = common.Get_Login_Details();
            var res = db.Tbl_Inspection.Where(x => x.Index_No == login_model.Index_No).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");




            ws.Cells["E3"].Value = "College Inspection";

            ws.Cells["A6"].Value = "Index No";
            ws.Cells["B6"].Value = "System No";
            ws.Cells["C6"].Value = "OS Name";
            ws.Cells["D6"].Value = "RAM";
            ws.Cells["E6"].Value = "HDD";
            ws.Cells["F6"].Value = "MAC";
            ws.Cells["G6"].Value = "Browser Name";
            ws.Cells["H6"].Value = "External IP";
            ws.Cells["I6"].Value = "Screen Res";
            ws.Cells["J6"].Value = "IE Version";
            ws.Cells["K6"].Value = "Active";


            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Index_No;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.SYS_No;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.OS_Name;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Ram;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.HDD;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.MAC;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Browser_Name;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Extn_IP;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.Screen_Res;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.IE_Version;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Active;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }




        public ActionResult Data_Test1()
        {
            return View(GetAllData());
        }


        IEnumerable<Tbl_DTT1> GetAllData()
        {
            //string Index_Number = Session["Index_No"].ToString();
            Login_Model login_model = common.Get_Login_Details();
            return db.Tbl_DTT1.Where(m => m.Index_No == login_model.Index_No).ToList().OrderBy(m => m.Index_No);
        }
        public void College_DTT1()
        {
            Login_Model login_model = common.Get_Login_Details();
            var res = db.Tbl_DTT1.Where(x => x.Index_No == login_model.Index_No).ToList();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "Index No";
            ws.Cells["B1"].Value = "Index No New";
            ws.Cells["C1"].Value = "System No";
            ws.Cells["D1"].Value = "MAC";
            ws.Cells["E1"].Value = "Screen Resolution";
            ws.Cells["F1"].Value = "Screen Res Change";
            ws.Cells["G1"].Value = "Read/Write Access";
            ws.Cells["H1"].Value = "Processor";


            int rowStart = 2;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Index_No;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Index_NoN;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.SYS_No;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.MAC;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Screen_Res;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Screen_Res_Change;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Read_Wrtite_Access;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Processor;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public ActionResult Data_Test2()
        {

            return View(GetAllData2());
        }
        IEnumerable<Tbl_DTT2> GetAllData2()
        {
            Login_Model login_model = common.Get_Login_Details();
            return db.Tbl_DTT2.Where(m => m.Index_No == login_model.Index_No).ToList().OrderBy(m => m.Index_No);
        }

        public void College_DTT2()
        {
            Login_Model login_model = common.Get_Login_Details();
            var res = db.Tbl_DTT2.Where(x => x.Index_No == login_model.Index_No).ToList();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "Index No";
            ws.Cells["B1"].Value = "Index No Old";
            ws.Cells["C1"].Value = "Read/Write Access";
            ws.Cells["D1"].Value = "DateTime Set";
            ws.Cells["E1"].Value = "MAC";


            int rowStart = 2;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Index_No;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Index_No_OLD;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Read_Wrtite_Access;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.datetime_set;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.MAC;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public ActionResult Data_Test3()
        {
            return View(GetAllData3());
        }
        IEnumerable<Tbl_DTT3> GetAllData3()
        {
            Login_Model login_model = common.Get_Login_Details();
            return db.Tbl_DTT3.Where(m => m.Index_No == login_model.Index_No).ToList().OrderBy(m => m.Index_No);
        }

        public void College_DTT3()
        {
            Login_Model login_model = common.Get_Login_Details();
            var res = db.Tbl_DTT3.Where(x => x.Index_No == login_model.Index_No).ToList();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "Index No New";
            ws.Cells["B1"].Value = "Index No Old";
            ws.Cells["C1"].Value = "DateTime";
            ws.Cells["D1"].Value = "MAC";
            ws.Cells["E1"].Value = "Login";
            ws.Cells["F1"].Value = "QP";
            ws.Cells["G1"].Value = "Hit";


            int rowStart = 2;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Index_NoN;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Index_No;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Datetime;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.MAC;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Login;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.QP;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Hit;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public ActionResult Attendance()
        {
            return View(GetStudentData());
        }
        IEnumerable<Tbl_Login> GetStudentData()
        {
            Login_Model login_model = common.Get_Login_Details();
            return db.Tbl_Login.Where(m => m.Index_No == login_model.Index_No).ToList().OrderBy(m => m.Seat_No);
        }

        public ActionResult CollegeStudentList()
        {
            var model = GetStudentData();
            List<Student_Model> list = new List<Student_Model>();
            foreach (var item in model)
            {
                Student_Model sm = new Student_Model();
                sm.Index_No = item.Index_No; sm.Name = item.Name; sm.Seat_No = item.Seat_No; sm.Mother_Name = item.Mother_Name; sm.Stream = item.Stream;
                list.Add(sm);
            }
            return View(list);
        }

        //[HttpPost]
        //public JsonResult Get_Student(int Index_ID, string Checked)
        //{
        //    Login_Model login_model = common.Get_Login_Details();
        //    Tbl_Login std = db.Tbl_Login.Where(a => a.id == Index_ID).FirstOrDefault();

        //    try
        //    {
        //        std.Attendance = Checked;
        //        db.Tbl_Login.Attach(std);
        //        db.Entry(std).Property(x => x.Attendance).IsModified = true;
        //        db.SaveChanges();

        //        var count = db.Tbl_Login.Where(a => a.Attendance == null && a.Index_No == login_model.Index_No).Count();
        //        return Json(new { Result = "true", Message = count }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = true, ex }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public JsonResult Student_Remaining()
        //{
        //    Login_Model login_model = common.Get_Login_Details();
        //    int cnt = db.Tbl_Login.Where(a => a.Attendance == null && a.Index_No == login_model.Index_No).Count();
        //    if (cnt != 0)
        //    {
        //        return Json(new { Result = false, Message = "Fill all the student records...!" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { Result = true, Message = "Records Saved Successfully...!" }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        public ActionResult Reschedule()
        {
            return View();
        }
        public ActionResult Check_Reschedule()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Check_Reschedule(string batch)
        {
            Login_Model login_model = common.Get_Login_Details();
            var model = db.Tbl_Login.Where(x => x.Index_No == login_model.Index_No && x.Batch == batch).Select(n => new
            {
                n.Seat_No,
                n.Name,
                n.Batch,
            }).OrderBy(v => v.Seat_No).ToList();

            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult College_Reschedule_Report()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetReschedule()
        {
            Login_Model login_Model = common.Get_Login_Details();

            var model = db.Tbl_Reschedule_Student.Where(x => x.Index_No == login_Model.Index_No).OrderBy(v => v.Seat_No).ToList();
            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Reschedule(string batch)
        {
            Login_Model login_Model = common.Get_Login_Details();
            var model = db.Tbl_Login.Where(x => x.Batch == batch && x.Index_No == login_Model.Index_No).Select(n => new
            {
                n.id,
                n.Seat_No,
                n.Name,
                n.Batch
            }).OrderBy(v => v.Seat_No).ToList();

            return Json(new { Result = "true", Response = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Obsolete]
        public JsonResult Reschedule_Student(BatchModel model)
        {
            Login_Model login_model = common.Get_Login_Details();
            Board_Model board_Model = common.Get_Board_Details(login_model.Index_No);

            Tbl_Reschedule_College resch = new Tbl_Reschedule_College();
            try
            {
                if (model.Mark_Model.Count != 0)
                {
                    resch.Index_No = login_model.Index_No;
                    resch.Division_Code = board_Model.Division_Code.ToString();
                    resch.Date_Time = DateTime.Now;
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    resch.IP_Adress = myIP;


                    db.Tbl_Reschedule_College.Add(resch);
                    db.SaveChanges();
                    var id = db.Tbl_Reschedule_College.Where(x => x.Index_No == login_model.Index_No).FirstOrDefault();

                    string file = Path.GetExtension(model.File.FileName);
                    string Filename = id.ID + file;
                    model.File.SaveAs(Path.Combine(Server.MapPath("~/Reschedule_Files"), Filename));


                    foreach (var item in model.Mark_Model)
                    {
                        if (item.Status == "checked")
                        {
                            var data = db.Tbl_Reschedule_College.Where(x => x.Index_No == login_model.Index_No).FirstOrDefault();
                            Tbl_Reschedule_Student stdrsch = new Tbl_Reschedule_Student();
                            stdrsch.Record_ID = data.ID;
                            stdrsch.Index_No = login_model.Index_No;
                            stdrsch.Seat_No = item.Seat_No;
                            stdrsch.Name = item.Name;
                            stdrsch.Initial_Batch = item.Batch;
                            stdrsch.Status = item.Status;
                            stdrsch.Reschedule_Batch = model.Reschedule_Batch;
                            stdrsch.Approved_By_Division = "Pending";
                            stdrsch.Approved_By_OLE = "Pending";
                            stdrsch.Division_Code = board_Model.Division_Code.ToString();
                            db.Tbl_Reschedule_Student.Add(stdrsch);
                            db.SaveChanges();
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

        public ActionResult Reschedule_Report()
        {
            return View();
        }
    }
}