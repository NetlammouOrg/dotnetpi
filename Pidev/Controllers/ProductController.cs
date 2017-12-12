
//using Data;
//using Domain.Entity;
//using Pidev.Models;
//using ServicePattern;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Web;
//using System.Web.Mvc;

//namespace Pidev.Controllers
//{
//    public class ProductController : Controller
//    {
        

//        IProduct ip = null;
//        String exttension;
//        public ProductController()
//        {
//            ip = new Products();
//            exttension = null;
//        }

//        ProjectModel ctx = new ProjectModel();
//        // GET: Product
//        public ActionResult Index()
//        {
//            List<product> p;
//            List<ProductViewModels> ListeVM = new List<ProductViewModels>();
//            HttpClient Client = new HttpClient();
//            Client.BaseAddress = new Uri("http://localhost:18080/");
//            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            HttpResponseMessage response = Client.GetAsync("pi_deux-web/api/products").Result;
//            if (response.IsSuccessStatusCode)
//            {
//                p = response.Content.ReadAsAsync<IEnumerable<product>>().Result.ToList();
//                foreach (product x in p)
//                {
//                    ProductViewModels prdt = new ProductViewModels();
//                    prdt.Id = x.id;
//                    prdt.Name = x.name;
//                    prdt.Categorie = x.Categorie;
//                    prdt.Description = x.description;
//                    ListeVM.Add(prdt);
//                }
//            }
//            else
//            {
//                ViewBag.result = "error";
//            }
//            //return View();   
//            return View(ListeVM);
            
//        }
//        public ActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Create(ProductViewModels item, HttpPostedFileBase urlImage)
//        {
//            exttension = Path.GetExtension(urlImage.FileName);
//            if (exttension.Equals("jpg") || exttension.Equals("png") || exttension.Equals("jpeg") || exttension.Equals("gif") || exttension.Equals(".jpg") || exttension.Equals(".png") || exttension.Equals(".jpeg") || exttension.Equals(".gif"))
//            {
//                String path = Server.MapPath("~/Content/" + urlImage.FileName);
//                urlImage.SaveAs(path);
//                string pic = urlImage.FileName;
//                product p = new product //itialiseur d'objet
//                {
//                name = item.Name,
//                picture = urlImage.FileName,
//                Categorie = item.Categorie,
//                description = item.Description,
//                quantity = item.Quantity,
//                user = null
//            };
//                ip.CreateProduct(p);
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                ViewBag.Message = "Extension image invalid";
//            }
//            return View();


//        }

//        public ActionResult Delete(int id)
//        {
//            /*    var result = ctx.product.Where(x => x.id == id);
//                ctx.product.Remove(result.FirstOrDefault());
//                ctx.SaveChanges();
//                return RedirectToAction("Index");*/
//            var e = ip.GetProdByID(id);
//                ip.DeleteProduct(e);
//            return RedirectToAction("Index");
//        }


//        // GET: Event/Edit/5
//        public ActionResult Edit(int id)
//        {
//            product p = ip.GetProdByID(id);
//            ProductViewModels pvm = new ProductViewModels();
//            pvm.Name = p.name;
//            pvm.Categorie = p.Categorie;
//            pvm.Description = p.description;
            
//            return View(pvm);
//        }

//        // POST: Event/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, ProductViewModels prd, HttpPostedFileBase urlImage)
//        {
//            try
//            {
//                product p = ip.GetProdByID(id);
//                //Event e = es.GetEventByID(id);

//                p.name = prd.Name.ToString();
//                //p.picture = prd.FileName;
//                p.Categorie = prd.Categorie;
//                //p.description = prd.description;
//                //p.quantity = prd.quantity;
//                p.user = null;


//                ip.UpdateProduct(p);




//                return RedirectToAction("Index", "product");
//            }
//            catch
//            {
//                return RedirectToAction("Edit", "product");
//            }
//        }

//        public ActionResult Details (int id, HttpPostedFileBase urlImage)
//        {
//            product p = ip.GetProdByID(id);
//            ProductViewModels pvm = new ProductViewModels();
//            pvm.Name = p.name;
//            pvm.Description = p.description;
//            pvm.Image = p.picture;
//            return View(pvm);
//        }



//    }
    
//}
