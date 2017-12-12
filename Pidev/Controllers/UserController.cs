using Pidev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using RestSharp;
using Domain.Entity;

using System.Net;
using System.Net.Mail;
using Data;
using Service;
using Service.Pattern;
using Rotativa.MVC;

namespace Pidev.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        //ServiceUsercs su = new ServiceUsercs();
        //Service<user> ss = new Service<user>();
        ProjectModel ctx = new ProjectModel();
        // GET: User
        // public static List<User> users = new List<User>();
        public ActionResult Index()
        {


            var client = new RestClient("http://localhost:18080/pidev-web/rest/");
            var request = new RestRequest("userss", Method.GET);
            request.AddHeader("Content-type", "application/json");


            IRestResponse<List<user>> users = client.Execute<List<user>>(request);

            return View(users.Data);

        }

        public ActionResult Edit(int id)
        {
            var res = ctx.user.Where(x => x.id == id).FirstOrDefault();
            User u = new User();
            u.id = res.id;
            u.lastName = res.lastName;
            u.firstName = res.firstName;
            u.adress = res.adress;
            u.password = res.passWodrd;
            u.mailAdress = res.mailAdress;
            u.phoneNumber = res.phoneNumber;
            return View(u);
        }

        [HttpPost]
        public ActionResult Edit(User item)
        {
            var u = ctx.user.Where(x => x.id == item.id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                u.firstName = item.firstName;
                u.lastName = item.lastName;
                u.mailAdress = item.mailAdress;
                u.adress = item.adress;
                u.phoneNumber = item.phoneNumber;
                u.passWodrd = item.password;
                ctx.SaveChanges();

                return RedirectToAction("index");
            }
            else

                return View(u);
        }//end else



        [HttpGet]
        public ActionResult Create()
        {
            return View("create");
        }
        [HttpPost]
        public ActionResult Create(user item, HttpPostedFileBase urlImage)
        {
            user userx = new user();
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
                    //userx.picture = urlImage.FileName;
                    userx.firstName = item.firstName;
                    userx.lastName = item.lastName;
                    userx.adress = item.adress;
                    userx.mailAdress = item.mailAdress;
                    userx.phoneNumber = userx.phoneNumber;
                    userx.passWodrd = userx.passWodrd;
                    

                    /*Using Web Service*/

                    //HttpClient Client = new HttpClient();
                    //Client.BaseAddress = new Uri("http://localhost:18080/");
                    //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    ////HttpResponseMessage response = Client.GetAsync("pidev-web/rest/userss").Result;
                    //Client.PostAsJsonAsync<user>("pidev-web/rest/userss", user).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                    /*Using DbContext*/
                    ctx.user.Add(userx);
                    ctx.SaveChanges();

                    return RedirectToAction("Index");

                }//end if
                else
                {
                    ViewBag.disabled = true;
                    ViewBag.Error = "File type is not valid";
                    return RedirectToAction("Create");
                }
            }//end else


        }
      
        
        public ActionResult Sent()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Sent(Pidev.Models.EmailFormModel model) {
        //    MailMessage mm = new MailMessage("hajer.benali1@esprit.tn", model.To);
        //    mm.Subject = model.Subject;
        //    mm.Body = model.Body;
        //    mm.IsBodyHtml = false;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    NetworkCredential nc = new NetworkCredential("hajer.benali1@esprit.tn","hamdoula ");
        //    smtp.UseDefaultCredentials = true;
        //    smtp.Credentials = nc;
        //    smtp.Send(mm);
        //    ViewBag.Message = "Mail has been sent successfully";
        //    return View();






        //}
        /*   public ActionResult Details(int id)
           {


               return View(ss.GetuserById(id));
           }*/
        public ActionResult Details(int id)
        {
            var result = ctx.user.Where(x => x.id == id);
            if (result.Count() <= 0)
                return RedirectToAction("Index");

            return View(result.FirstOrDefault());
        }

        public ActionResult Delete(int id)
        {
          using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:18080/");
                var deleteTask = client.DeleteAsync("pidev-web/rest/userss/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult ExportPdf()
        {

            return new ActionAsPdf("Index")
            {

                FileName = Server.MapPath("~/Content/List.pdf")

            };

        }


    }
}
   
