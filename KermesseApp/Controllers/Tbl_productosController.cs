using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;

namespace KermesseApp.Controllers
{
    public class Tbl_productosController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();

        // GET: Tbl_productos
        public ActionResult tbl_productos()
        {
            return View(db.tbl_productos.ToList());
        }


        public ActionResult VwGuardarProd() //Invokes List View
        {
            //var id_comunidad = db.tbl_comunidad;
            //ViewBag.tbl_comunidad = new SelectList(id_comunidad, "nombre");

            ViewBag.id_comunidad = new SelectList(db.tbl_comunidad, "id_comunidad", "nombre");

            //var id_cat_producto = db.tbl_cat_producto;
            //ViewBag.tbl_cat_producto = new SelectList(id_cat_producto, "nombre");

            ViewBag.id_cat_producto = new SelectList(db.tbl_cat_producto, "id_cat_producto", "nombre");
            return View(); //Returns New Prod Cat View
        }

        [HttpPost]
        public ActionResult GuardarProd(tbl_productos tp) //Method that saves new item
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tbl_productos tProd = new tbl_productos();
                    tProd.id_comunidad = tp.id_comunidad;
                    tProd.id_cat_producto = tp.id_cat_producto;
                    tProd.nombre = tp.nombre;
                    tProd.desc_presentacion = tp.desc_presentacion;
                    tProd.cantidad = tp.cantidad;
                    tProd.precio_venta = tp.precio_venta;
                    tProd.estado = 1;
                    db.tbl_productos.Add(tProd); //Add new item
                    db.SaveChanges(); //Saves to model registry 
                }

                ModelState.Clear();
                return RedirectToAction("tbl_Productos");
            }
            catch
            {
                return RedirectToAction("tbl_Productos");
            }
        }

        public ActionResult EliminarProd(int id)
        {
            tbl_productos tp = new tbl_productos();
            tp = db.tbl_productos.Find(id);
            db.tbl_productos.Remove(tp);

            db.SaveChanges();
            var list = db.tbl_productos.ToList();

            return View("tbl_Productos", list);
        }

        public ActionResult VerProd(int id)
        {
            var Prod = db.tbl_productos.Where(x => x.id_producto == id).First();
            return View(Prod);
        }

        public ActionResult EditarProd(int? id)
        {
            tbl_productos tp = db.tbl_productos.Find(id);
            if (tp == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(tp);
            }
        }

        [HttpPost]
        public ActionResult ActualizarProd(tbl_productos tp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("tbl_Productos");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]

        public ActionResult FiltrarProd(String cadena)
        {
            if (String.IsNullOrEmpty(cadena))
            {
                var list = db.tbl_productos.ToList();
                return View("tbl_Productos", list);
            }
            else
            {
                var listaFiltrada = db.tbl_productos.Where(x => x.nombre.Contains(cadena) || x.desc_presentacion.Contains(cadena));
                return View("tbl_Productos", listaFiltrada);
            }
        }

        public ActionResult VerRptProd(String tipo)
        {
            
        }
    }
}