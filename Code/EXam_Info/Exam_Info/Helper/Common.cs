using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Exam_Info.Models;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Exam_Info.Helper
{
    public class Common
    {
        GKJ_2022Entities db = new GKJ_2022Entities();

        public  string Get_Year()
        {
            return "March_2022";
        }
        public Login_Model Get_Login_Details()
        {
            //string login_string= System.Web.HttpContext.Current.User.Identity.Name;
            string login_string = HttpContext.Current.User.Identity.Name;
            Login_Model login_model = JsonConvert.DeserializeObject<Login_Model>(login_string);
            return login_model;
        }
        public Tbl_Login Get_College_Login_Details()
        {
            string college_Id = HttpContext.Current.User.Identity.Name;
            Tbl_Login emp = JsonConvert.DeserializeObject<Tbl_Login>(college_Id);
            return emp;
        }
        public List<SelectListItem> Get_Batch_List()
        {
            var model = db.Tbl_Batch_Activation.Where(x => x.Active == 1).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select Batch--", Value = "0" });
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.Batch.ToString(), Value = item.Batch.ToString() });
                }
            }
            return list;
        }

        public List<SelectListItem> Get_List(Board_Model board_Model)
        {
            var model = db.Tbl_Code_Master.Where(x => x.division_code == board_Model.Division_Code.ToString()).Select(n => new
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
            return list;
        }

        public List<SelectListItem> Get_District_List(string board_Model)
        {
            var model = db.Tbl_Code_Master.Where(x => x.division_code == board_Model).Select(n => new
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
            return list;
        }

        public List<SelectListItem> Get_Taluka_List(string distCode)
        {
            var model = db.Tbl_Code_Master.Where(x => x.district_code == distCode).Select(n => new
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

            return list;
        }

        public void CreateExcelFile(DataTable dt_list, string filename)
        {
            try
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt_list);
                using (DataSet ds = ds1)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        string rootFolder = HttpContext.Current.Server.MapPath("~/State").ToString();
                        string fileName = @"" + filename + ".xlsx";

                        System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
                        using (ExcelPackage xp = new ExcelPackage(file))
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);
                                int rowstart = 1;
                                int colstart = 1;
                                int rowend = rowstart;
                                int colend = colstart + dt.Columns.Count;
                                rowend = rowstart + dt.Rows.Count;
                                ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                                int i = 1;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    i++;
                                    if (dc.DataType == typeof(decimal))
                                        ws.Column(i).Style.Numberformat.Format = "#0.00";
                                }
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                            }
                            xp.Save();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public  DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public Board_Model Get_Board_Details(string Index_No)
        {
            Board_Model model = new Board_Model();
            try
            {
                if (Index_No.Length == 7)
                {
                    var temp = db.Tbl_Code_Master.Where(a => a.district_code == Index_No.Substring(0, 2) && a.taluka_code == Index_No.Substring(2, 2)).FirstOrDefault();
                    if (temp != null)
                    {
                        model.Division_Code = Convert.ToInt32(temp.division_code);
                        model.Division_Name = temp.division_name;
                        model.District_Code = temp.district_code;
                        model.District_Name = temp.district_name;
                        model.Taluka_Code = temp.taluka_code;
                        model.Taluka_Name = temp.taluka_name;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return model;
        }

    }
}