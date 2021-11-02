using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;

namespace KermesseApp.Controllers
{
    public class Tbl_cat_productoController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();

        // GET: Tbl_cat_producto
        public ActionResult tbl_catProducto() //Invokes List View
        {
            return View(db.tbl_cat_producto.ToList());
        }

        public ActionResult VwGuardarCatProd() //Invokes List View
        {
            return View(); //Returns New Prod Cat View
        }

        [HttpPost]
        public ActionResult GuardarCatProd(tbl_cat_producto tcp) //Method that saves new item
        {
            if(ModelState.IsValid)
            { 
            tbl_cat_producto tcProd = new tbl_cat_producto();
            tcProd.nombre = tcp.nombre;
            tcProd.descripcion = tcp.descripcion;
            tcProd.estado = 1;
            db.tbl_cat_producto.Add(tcProd); //Add new item
            db.SaveChanges(); //Saves to model registry 
            }
            ModelState.Clear();
            return View("tbl_catProducto");
        }

        public ActionResult EliminarCatProd(int id)
        {
            tbl_cat_producto tcp = new tbl_cat_producto();
            tcp = db.tbl_cat_producto.Find(id);
            db.tbl_cat_producto.Remove(tcp);

            db.SaveChanges();
            var list = db.tbl_cat_producto.ToList();

            return View("tbl_catProducto", list);
        }

        public ActionResult VerCatProd(int id)
        {
            var catProd = db.tbl_cat_producto.Where(x => x.id_cat_producto == id).First();
            return View(catProd);
        }

        public ActionResult EditarCatProd(int? id)
        {
            tbl_cat_producto tcp = db.tbl_cat_producto.Find(id);
            if (tcp == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(tcp);
            }
        }

        [HttpPost]
        public ActionResult ActualizarCatProd(tbl_cat_producto tcp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tcp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("tbl_catProducto");
            }
            catch
            {
                return View();
            }

        }
    }
}