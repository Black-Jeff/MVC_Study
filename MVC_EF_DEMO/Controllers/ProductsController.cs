using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_EF_DEMO.Models;

namespace MVC_EF_DEMO.Controllers
{
    public class ProductsController : Controller
    {
        private MyConnection db = new MyConnection();

        //// GET: Products
        //public ActionResult Index()
        //{
        //    return View(db.Products.ToList());
        //}

        // GET: Products
        public ActionResult Index(string Search_code, string Search_name, string Search_spec)
        {


            if (!string.IsNullOrEmpty(Search_code)
              || !string.IsNullOrEmpty(Search_name)
              || !string.IsNullOrEmpty(Search_spec))
            {
                var prod = from u in db.Products
                           where u.model_name.Contains(Search_code)
                           && u.product_name.Contains(Search_name)
                           && u.size.Contains(Search_spec)
                           select u;
                return View(prod.ToList());
            }
            else
            {
                var prod = from u in db.Products
                           //where 1 != 1
                           select u;
                return View(prod.ToList());
            }


        }
        

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsDB productsDB = db.Products.Find(id);
            if (productsDB == null)
            {
                return HttpNotFound();
            }
            return View(productsDB);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,price,model_name,product_name,size,counting_unit")] ProductsDB productsDB)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productsDB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productsDB);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsDB productsDB = db.Products.Find(id);
            if (productsDB == null)
            {
                return HttpNotFound();
            }
            return View(productsDB);
        }

        // POST: Products/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,price,model_name,product_name,size,counting_unit")] ProductsDB productsDB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productsDB);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsDB productsDB = db.Products.Find(id);
            if (productsDB == null)
            {
                return HttpNotFound();
            }
            return View(productsDB);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsDB productsDB = db.Products.Find(id);
            db.Products.Remove(productsDB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
