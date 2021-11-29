using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;

namespace KermesseApp.Controllers
{
    public class Tbl_productosController : Controller
    {
        KERMESSEEntities db = new KERMESSEEntities();

        //GET: Tbl_productos
        public ActionResult tbl_productos()
        {
            return View(db.tbl_productos.Where(model => model.estado != 3));
        }


        public ActionResult VwGuardarProd() //Invokes List View
        {
            ViewBag.id_comunidad = new SelectList(db.tbl_comunidad.Where(model => model.estado != 3), "id_comunidad", "nombre");
            ViewBag.id_cat_producto = new SelectList(db.tbl_cat_producto.Where(model => model.estado != 3), "id_cat_producto", "nombre");
            return View(); //Returns New Prod Cat View
        }

        [HttpPost]
        public ActionResult GuardarProducto(tbl_productos tp) //Method that saves new item
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
                ModelState.Clear();
                return RedirectToAction("tbl_Productos");
            }

            ViewBag.id_comunidad = new SelectList(db.tbl_comunidad.Where(model => model.estado != 3), "id_comunidad", "nombre");
            ViewBag.id_cat_producto = new SelectList(db.tbl_cat_producto.Where(model => model.estado != 3), "id_cat_producto", "nombre");
            return View("tbl_Productos");

        }

        public ActionResult EliminarProd(int id)
        {
            tbl_productos tp = new tbl_productos();
            tp = db.tbl_productos.Find(id);
            this.LogicalDeleteProducto(tp);

            return View("tbl_Productos");
        }

        [HttpPost]
        public ActionResult LogicalDeleteProducto(tbl_productos tp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tp.estado = 3;
                    db.Entry(tp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ListProductos");
            }
            catch (Exception)
            {
                return View();
                throw;
            }
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
                ViewBag.id_comunidad = new SelectList(db.tbl_comunidad, "id_comunidad", "nombre");
                ViewBag.id_cat_producto = new SelectList(db.tbl_cat_producto, "id_cat_producto", "nombre");
                return View(tp);
            }
        }

        [HttpPost]
        public ActionResult ActualizarProducto(tbl_productos tp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tp.estado = 2;
                    db.Entry(tp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.id_comunidad = new SelectList(db.tbl_comunidad, "id_comunidad", "nombre");
                ViewBag.id_cat_producto = new SelectList(db.tbl_cat_producto, "id_cat_producto", "nombre");
                return RedirectToAction("tbl_Productos");
            }
            catch (Exception)
            {
                return View("tbl_Productos");
                throw;
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

    }
}