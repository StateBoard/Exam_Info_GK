using Exam_Info.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_Info.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public void Create_PDF(FileUploadModel model)
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            try
            {


                //Tbl_Upload_Ans_File_Data data = new Tbl_Upload_Ans_File_Data();



                Random r = new Random();

                PdfWriter.GetInstance(document, new System.IO.FileStream(Path.Combine(HttpRuntime.AppDomainAppPath, "Certificate_PDF/" + model.Index_No + ".pdf"), FileMode.Create));

                int totalfonts = FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                Font arial = FontFactory.GetFont("Arial", 18);
                Font arial2 = FontFactory.GetFont("Arial", 12);
                Font arial3 = FontFactory.GetFont("Arial", 12, BaseColor.RED);

                document.Open();

                iTextSharp.text.Image check1 = iTextSharp.text.Image.GetInstance(Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data/head.png"));
                check1.SpacingAfter = 20;
                document.Add(check1);

                Paragraph para = new Paragraph("\nOnline GK Exam  JULY 2022 - Completion Certificate", arial);
                para.Alignment = 1;
                para.SpacingAfter = 20;
                document.Add(para);

                Paragraph para1 = new Paragraph("\nThis is to certify that College Index Number : " + model.Index_No + "  has uploaded answer files Successfully", arial2);
                para1.Alignment = 3;
                para1.SpacingAfter = 30;
                document.Add(para1);

                Paragraph para2 = new Paragraph("\nDetails are :- ", arial2);
                document.Add(para2);

                Paragraph para3 = new Paragraph("Submission Date & Time : " + DateTime.Now, arial2);
                document.Add(para3);

                Paragraph para4 = new Paragraph("Submitted by : " + model.Teacher_Name + "(MOB.NO:-" + model.Mobile_No + "  ) ", arial2);
                document.Add(para4);

                Paragraph para5 = new Paragraph("Total Students : " + model.Total_Students + " ", arial2);
                document.Add(para5);

                Paragraph para6 = new Paragraph("Appeared Students :" + model.Total_Present_Student + "  ", arial2);
                document.Add(para6);

                Paragraph para7 = new Paragraph("Absent Students : " + model.Total_Absent_Student + " ", arial2);
                document.Add(para7);

                Paragraph para8 = new Paragraph("Upload Files : " + model.files + "", arial2);
                document.Add(para8);

                Paragraph para9 = new Paragraph("Remaining / Absent Files: " + model.Not_Upload_File + "", arial2);
                para9.SpacingAfter = 30;
                document.Add(para9);

                Paragraph para10 = new Paragraph("\nThank you for your co-operation.", arial2);
                para10.SpacingAfter = 50;
                document.Add(para10);

                iTextSharp.text.Image check = iTextSharp.text.Image.GetInstance(Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data/samadhan-sign 1.jpeg"));
                document.Add(check);

                Paragraph para11 = new Paragraph("\nSamadhan Dhane \nChief Co - ordinator \nOnline IT Exam", arial2);
                para11.SpacingAfter = 30;
                document.Add(para11);

                Paragraph para12 = new Paragraph("NOTE: Submit copy of this certificate along with attendance register at the time of submission. ", arial3);
                document.Add(para12);
                document.Close();

            }
            catch (Exception ex)
            {
                document.Close();
            }

        }
    }
}