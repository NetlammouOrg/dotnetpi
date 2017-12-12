using Data;

using Domain.Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Pidev.Models;
using Services;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;
using static iTextSharp.text.Font;

namespace Pidev.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        ServiceArticle serviceArticle = new ServiceArticle();
        ProjectModel ctx = new ProjectModel();

        // GET: Article
        
        public ActionResult Index()
        {  
            List<article> a ;
            List<ArticleViewModels> listView = new List<ArticleViewModels>();
            //using (ProjectModel ctx = new ProjectModel())
            //{               
            //    a = ctx.article.ToList();

            //    foreach (article x in a)
            //    {
            //        ArticleViewModels art = new ArticleViewModels();
            //        art.Id = x.id;
            //        art.Name = x.name;
            //        art.Image = x.image;
            //        art.Body = x.body;
            //        art.Date = DateTime.Now;
            //        listView.Add(art);
            //    }
            //}

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("pidev-web/rest/article").Result;
            if(response.IsSuccessStatusCode)
            {
                a = response.Content.ReadAsAsync<IEnumerable<article>>().Result.ToList();
                foreach (article x in a)
                {
                    ArticleViewModels art = new ArticleViewModels();
                    art.Id = x.id;
                    art.Name = x.name;
                    art.Image = x.image;
                    art.Body = x.body;
                    art.Date = DateTime.Now;
                    listView.Add(art);
                }
            }
            else
            {
                ViewBag.result = "error";
            }
            
            return View(listView);
        }
        public ActionResult Edit(int id)
        {
            ArticleViewModels art = new ArticleViewModels();
            var result = ctx.article.Where(x => x.id == id);
        
            art.Id = result.FirstOrDefault().id;
            art.Name = result.FirstOrDefault().name;
            art.Body = result.FirstOrDefault().body;
            art.Image = result.FirstOrDefault().image;

            if (result.Count() <= 0)
                  return RedirectToAction("index");

            return View(art);
        }


        public ActionResult Details (int id)
        {
            ArticleViewModels art = new ArticleViewModels();
            var result = ctx.article.Where(x => x.id == id);
            ViewBag.imageName = "~/Content/" + result.FirstOrDefault().image;

            art.Id = result.FirstOrDefault().id;
            art.Name = result.FirstOrDefault().name;
            art.Body = result.FirstOrDefault().body;
            art.Image = result.FirstOrDefault().image;
            var t =  result.FirstOrDefault().date;


            if (result.Count() <= 0)
                return RedirectToAction("index");

            return View(art);
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModels item, HttpPostedFileBase urlImage)
        {
            var result = ctx.article.Where(x => x.id == item.Id);
            result.FirstOrDefault().name = item.Name;
            
            result.FirstOrDefault().body = item.Body;
           
            //Import Image
            if (urlImage == null || urlImage.ContentLength == 0)
            {
                ViewBag.disabled = true;
                ViewBag.Error = "Please select a valid image ";
                return View("Edit");
            }//end if
            else
            {
                if (urlImage.FileName.ToLower().EndsWith("jpg") || urlImage.FileName.ToLower().EndsWith("png")|| urlImage.FileName.ToLower().EndsWith("jepg"))
                {
                    string pathImage = Server.MapPath("~/Content/" + urlImage.FileName);
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }//end if
                    result.FirstOrDefault().image = urlImage.FileName;
                    ctx.SaveChanges();
                    urlImage.SaveAs(pathImage);
                  }//end if
                else
                {
                    ViewBag.disabled = true;
                    ViewBag.Error = "File type is not valid";
                    return View(item);
                }
            }//end else

            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            var result = ctx.article.Where(x => x.id == id);
            ctx.article.Remove(result.FirstOrDefault());
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModels item, HttpPostedFileBase urlImage)
        {
            article articlex = new article();
            //Import Image
            if (urlImage == null || urlImage.ContentLength == 0)
            {
                ViewBag.disabled = true;
                ViewBag.Error = "Please select a valid image ";
                return View("Create");
            }//end if
            else
            {
                if (ModelState.IsValid && urlImage.FileName.ToLower().EndsWith("jpg") || urlImage.FileName.ToLower().EndsWith("png") || urlImage.FileName.ToLower().EndsWith("jepg"))
                {
                    string pathImage = Server.MapPath("~/Content/" + urlImage.FileName);
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }//end if
                    urlImage.SaveAs(pathImage);
                    
                        articlex.body = item.Body;
                        articlex.name = item.Name;
                        articlex.image = urlImage.FileName;
                        articlex.date =  DateTime.Now; ;
                        articlex.favourite = false;
                        articlex.other = "v";
                        articlex.user = null;
                       
                    /*Using Web Service*/

                    //HttpClient Client = new HttpClient();
                    //Client.BaseAddress = new Uri("http://localhost:18080/");
                    //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    ////HttpResponseMessage response = Client.GetAsync("pidev-web/rest/article").Result;
                    //Client.PostAsJsonAsync<article>("pidev-web/rest/article", articlex).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                    /*Using DbContext*/
                    //ctx.article.Add(articlex);
                    //ctx.SaveChanges();
                    /*Using designe pattern*/
                    serviceArticle.Add(articlex);
                    serviceArticle.Commit();

                    return RedirectToAction("index");
                  
                }//end if
                else
                {
                    ViewBag.disabled = true;
                    ViewBag.Error = "File type is not valid";
                    return RedirectToAction("Create");
                }
            }//end else
         }

        public FileResult CreatePdf(int id)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            
            //file name to be created   
            string strPDFFileName = string.Format("article" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            //Document doc = new Document();
            //doc.SetMargins(0f, 0f, 0f, 0f);
            Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
           
            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(5);
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Server.MapPath("~/Content/" + strPDFFileName);

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            //doc.Add(AddContentToPDF(tableLayout,id,doc));
            var result = ctx.article.Where(x => x.id == id).FirstOrDefault();
            //set the article's body into paragraphe
            Paragraph body = new Paragraph(result.body);
            Paragraph title = new Paragraph(result.name);

            float[] headers = { 50, 24, 45, 35, 50 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  
            string pathImage = Server.MapPath("~/Content/" + result.image);
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(pathImage);
            //Style
            Font font = new Font(FontFamily.HELVETICA, 12, Font.BOLD);
            title.Font = font;
            //Alignement elments

            PdfPCell cell = new PdfPCell();
            cell.AddElement(body);

            body.Alignment = Element.BODY;
            body.Alignment=Element.ALIGN_MIDDLE;

            jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            doc.SetMargins(20f, 20f, 20f, 20f);
            title.SpacingBefore = 20;
            title.SpacingAfter = 20;
            title.Alignment = 1 ;
            //Add elements to the document

            doc.Add(new Paragraph(100, "\u00a0"));
            doc.Add(title);
            doc.Add(new Paragraph(50, "\u00a0"));
            doc.Add(jpg);
            doc.Add(new Paragraph(100, "\u00a0"));
            doc.Add(body);
            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }

        protected PdfPTable AddContentToPDF(PdfPTable tableLayout, int id,Document doc)
        {
            var result = ctx.article.Where(x => x.id == id).FirstOrDefault();
            //set the article's body into paragraphe
            Paragraph paragraph = new Paragraph(result.body);

            float[] headers = { 50, 24, 45, 35, 50 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  
            string pathImage = Server.MapPath("~/Content/" + result.image);
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(pathImage);
            doc.Add(paragraph);
            doc.Add(jpg);

            tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0)))) {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header 
            AddCellToHeader(tableLayout, "Id");
            AddCellToHeader(tableLayout,"Title");
            AddCellToHeader(tableLayout, "Body");
            AddCellToHeader(tableLayout, "image");
            AddCellToHeader(tableLayout, "Date");
            

            ////Add body  

            //foreach (var emp in articles)
            //{

            //    AddCellToBody(tableLayout, emp.id.ToString());
            //    AddCellToBody(tableLayout, emp.name);
            //    AddCellToBody(tableLayout, emp.body);
            //    AddCellToBody(tableLayout, emp.image);
            //    AddCellToBody(tableLayout, emp.date.ToString());

            //}

            return tableLayout;
        }
        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
    });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
             {
                HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
              });
        }


        public ActionResult Chart()
        {
            return View();
        }

    }
}