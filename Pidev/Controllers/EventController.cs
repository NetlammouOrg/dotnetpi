using Domain.Entity;
using Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pidev.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Http;
using Rotativa.MVC;
using Services;

namespace Pidev.Controllers
{
    public class EventController : Controller
    {
        ProjectModel ctx = new ProjectModel();
        //IActionService service = null;
        //public EventController()
        //{
        //    service = new ActionService();
        //}



        // GET: Event
        public ActionResult AllEvents()
        {
            var client = new RestClient("http://localhost:18080/pidev-web/rest/");
            var request = new RestRequest("actions", Method.GET);
            request.AddHeader("Content-type", "application/json");

            IRestResponse<List<action>> animals = client.Execute<List<action>>(request);

            //var res = ctx.action.ToList();

            return View(animals.Data);

        }


        public ActionResult PastEvent()
        {
            var client = new RestClient("http://localhost:18080/pidev-web/rest/");
            var request = new RestRequest("actionspast", Method.GET);
            request.AddHeader("Content-type", "application/json");


            IRestResponse<List<action>> animals = client.Execute<List<action>>(request);

            return View(animals.Data);

        }

        public ActionResult Map()
        {
            return View("Map");
        }

        public ActionResult Statistic()
        {
            return View("Statistic");
        }

        //pdf
        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("AllEvents")

            {
                FileName = Server.MapPath("~/Content/h.pdf")
            };
        }




        //Ajout et mailing
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(EventModel user)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://127.0.0.1:18080/");
            await Client.PostAsJsonAsync<EventModel>("pidev-web/rest/actions", user).ContinueWith((postTask) => postTask.Result.IsSuccessStatusCode);



            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("ikram.belkahla@esprit.tn"));  // replace with valid value 
            message.From = new MailAddress("ikram.belkahla@esprit.tn");  // replace with valid value
            message.Subject = "Event Created !";
            message.Body = string.Format(body, "Event Services", "ikram.belkahla@esprit.tn", "There is a new event on our platform .NETlammou, Take a look !");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "netlammou4gl2@gmail.com",  // replace with valid value
                    Password = "amouramour"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return RedirectToAction("AllEvents");
            }


        }

        //Delete stack
        public ActionResult Delete()
        {
            return View("Delete");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            var client = new RestClient("http://127.0.0.1:18080/pidev-web/rest/");

            var request = new RestRequest("actions/" + id, Method.DELETE);
            client.Execute(request);
            return RedirectToAction("AllEvents");
        }

        public ActionResult Update()
        {
            return View("Update");
        }
        [HttpPost]
        public void Update(int id, action product)
        {
            var client = new RestClient("http://127.0.0.1:18080/pidev-web/rest/");
            var request = new RestRequest("actions/" + id, Method.PUT);
            request.AddJsonBody(product);
            client.Execute(request);

        }






        /*
        //Delete
       public ActionResult Delete(int id)
        {
            return View("Delete");
        }

        // POST: Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                ActionService serv = new ActionService();
                serv.RemoveEvent(id);

                return RedirectToAction("AllEvents");
            }
            catch
            {
                return View();
            }
        }

        //delete mariem
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string ConfirmButton)
        {
            var ev = service.SearchEventById(id);
            if (ev == null)
                return View("Task not found");

            service.DeleteEvent(ev);
            service.SaveChange();

            return RedirectToAction("Index");

        }



        public ActionResult Delete(int id, FormCollection collection)
        {
            var evn = service.SearchEventById(id);

            if (evn == null)
                return View("Not Found");
            else
                return View(evn);

        }*/

    }

}
